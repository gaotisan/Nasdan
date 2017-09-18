using System;
using Nasdan.Core.Neo4j;
using Nasdan.Core.Representation;
using System.Linq;
using Nasdan.Core.Senses;
using Proto;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Nasdan.Core.Actors
{
    internal partial class ActorManager : Manager
    {


        public ActorManager(Neo4jManager neo4j) : base(neo4j)
        {

        }


        public string Serialize(object obj)
        {
            string result = string.Empty;
            if (obj != null)
            {
                result = JsonConvert.SerializeObject(obj);
            }
            return result;
        }
        public object Deserialize(string json, Type type)
        {
            object result = JsonConvert.DeserializeObject(json, type);
            return result;
        }

        public Manager GetManager(_P p)
        {
            Assembly assembly = this.GetAssembly(p.ManagerAssemblyPath);
            var typeManager = assembly.GetType(p.ManagerType);
            var manager = (Manager)System.Activator.CreateInstance(typeManager, this.Neo4j);
            return manager;
        }



        public Assembly GetAssembly(string path)
        {
            Assembly assembly;
            if (string.IsNullOrEmpty(path))
            {
                assembly = typeof(ActorManager).GetTypeInfo().Assembly;
            }
            else
            {
                //Get from the Path
                assembly = typeof(ActorManager).GetTypeInfo().Assembly;
            }
            return assembly;
        }


        public object GetParameter1(_P p)
        {
            object result = null;
            if (!string.IsNullOrEmpty(p.Argument1Json))
            {
                Assembly assembly = this.GetAssembly(p.Argument1AssemblyPath);
                var typeParameter = assembly.GetType(p.Argument1Type);
                result = this.Deserialize(p.Argument1Json, typeParameter);
            }
            return result;
        }
        public object GetParameter2(_P p)
        {
            object result = null;
            if (!string.IsNullOrEmpty(p.Argument2Json))
            {
                Assembly assembly = this.GetAssembly(p.Argument2AssemblyPath);
                var typeParameter = assembly.GetType(p.Argument2Type);
                result = this.Deserialize(p.Argument2Json, typeParameter);
            }
            return result;
        }

        public void Execute(_N<_P> nP)
        {
            try
            {
                //Get Method Info
                var manager = this.GetManager(nP.Data);
                var method = manager.GetType().GetMethod(nP.Data.FunctionName);
                var parameter1 = this.GetParameter1(nP.Data);
                var parameter2 = this.GetParameter2(nP.Data);
                var parameters = new object[] { parameter1, parameter2 };
                var result = method.Invoke(manager, parameters);
                //Hay 3 posibilidades
                //1º) El método no devuelve nada
                //2º) El metodo invocado devuelve un tipo Node<T>
                //3º) Devuelve una clase pero que no es de tipo Node<T>                                                 
                if (string.IsNullOrEmpty(nP.Data.ResultType))
                {
                    //1º) El método no devuelve nada
                }
                else
                {
                    //Is a function (Value expected)
                    var assembly = this.GetAssembly(nP.Data.ResultAssemblyPath);
                    var resultType = assembly.GetType(nP.Data.ResultType);
                    if (resultType.GetGenericTypeDefinition() == typeof(_N<>))
                    {
                        //2º) El metodo invocado devuelve un tipo Node<T>                        
                        var reference = result.GetType().GetProperty("Reference").GetValue(result);
                        long resultId = (long)reference.GetType().GetProperty("Id").GetValue(reference);
                        //DELETE _P (Process)
                        var query = this.Neo4j.Client.Cypher
                        .OptionalMatch($"(p:_P)<-[l:_L]-(s:_S)")
                        .Where($"ID(p) = {{idParam}}")
                        .WithParam("idParam", nP.Reference.Id)
                        .Delete("l,p")
                        .Return(s => s.As<Neo4jClient.Node<_S>>()); //GET SELF   
                        var self = query.Results.Single();
                        //Connect new Graph (result) with SELF
                        this.Neo4j.Client.Cypher
                        .Match($"(s:_S)")
                        .Where($"ID(s) = {{idParam1}}")
                        .WithParam("idParam1", self.Reference.Id)
                        .Match($"(n)")
                        .Where($"ID(n) = {{idParam2}}")
                        .WithParam("idParam2", resultId)
                        .Create("(s)-[r:_Q]->(n)")
                        .ExecuteWithoutResults();
                    }
                    else
                    {
                        //3º) Devuelve una clase pero que NO es de tipo Node<T>   
                    }
                }
                //Tell a Self que ponga Labels de lenguaje una vez acabado.
                SelfActor.Tell(SelfActor.MessageActions.label);
            }
            catch (Exception error)
            {
                this.Neo4j.Client.Cypher
                    .Match($"(n:_P)")
                    .Where($"ID(n) = {{idParam}}")
                    .WithParam("idParam", nP.Reference.Id)
                    .Create($"(e:_E {{error}})")
                    .WithParam("error", error)
                    .Create("(n)-[:_L]->(e)")
                    .ExecuteWithoutResults();
                //Creamos un nodo Excepción y lo almacenamos como resultado
                //(SELF)-[_P]->(_E) Error
            }
        }



        public void Receive(ImageMessage msg)
        {
            //Grafo 1: (_Input)-[:_R]->(ImageMessage)  
            //Grafo 2: (ImageMessage)-[:_R]->(_SELF)
            //Grafo 3: (_SELF)-[:_R]->(_P)
            _I iFrame = new _I();
            _S self = new _S();
            var query = this.Neo4j.Client.Cypher
                //Nodes: _I, _S, ImageMessage, _P
                .Create($"(i:_I {{iFrame}})")
                .Create($"(m:ImageMessage {{msg}})")
                .Create($"(s:_S)")
                .WithParams(new { iFrame, msg })
                .Create("(i)-[:_L]->(s)")
                .Create("(s)<-[:_Q]-(m)")                 
                .Return((i, m, s) =>
                   new
                   {
                       input = i.As<Neo4jClient.Node<_I>>(),
                       message = m.As<Neo4jClient.Node<ImageMessage>>(),
                       self = s.As<Neo4jClient.Node<_S>>()
                   }
               );
            var result = query.Results.Single();
            //Create Process to Exceute
            _P pToExecute = new _P();
            pToExecute.ManagerType = "Nasdan.Core.Senses.SensesManager";
            pToExecute.FunctionName = "Process";
            pToExecute.Argument1Json = this.Serialize(result.self);
            pToExecute.Argument1Type = result.self.GetType().ToString();
            pToExecute.Argument2Json = this.Serialize(result.message);
            pToExecute.Argument2Type = result.message.GetType().ToString();
            pToExecute.ResultType = "Nasdan.Core.Representation._N`1";
            var query2 = this.Neo4j.Client.Cypher
                 .Create($"(p:_P {{pToExecute}})")
                 .WithParams(new { iFrame, msg, pToExecute })
                 //Edges                 
                 .Create("(s)-[p:_P {{pToExecute}}]->(m)")
                 .WithParams(new { pToExecute })
                 .Return(p => p.As<Neo4jClient.Node<_P>>());
            var _pNode = query2.Results.Single();
            var _p = ((Neo4jClient.Node<_P>)_pNode).to_N<_P>();

            SelfActor.Tell(_p);
        }


    }
}