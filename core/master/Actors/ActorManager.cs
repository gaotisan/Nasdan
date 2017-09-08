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

        public object GetParameter(_P p)
        {
            object result = null;
            if (!string.IsNullOrEmpty(p.ArgumentJson))
            {
                Assembly assembly = this.GetAssembly(p.ArgumentAssemblyPath);
                var typeParameter = assembly.GetType(p.ArgumentType);
                result = this.Deserialize(p.ArgumentJson, typeParameter);
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
                var parameter = this.GetParameter(nP.Data);
                var parameters = new object[] { parameter };
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
                        long id = (long)reference.GetType().GetProperty("Id").GetValue(reference); 
                        //.PropertyType.GetProperty("Id").GetValue(result);                                         
                        this.Neo4j.Client.Cypher
                        .Match($"(p)")
                        .Where($"ID(p) = {{idParam}}")
                        .WithParam("idParam", nP.Reference.Id)
                        .Delete("(p)")
                        .ExecuteWithoutResults();
                        /* 
                        .Match($"(n)")
                        .Where($"ID(n) = {{idResult}}")
                        .WithParam("idResult", id)
                        .Create($"(s)-[:_P {{_pSerialized}}]->(n)")
                        .WithParam("_pSerialized", p)
                        .Create($"(s)-[:_Q]->(n)")
                        .ExecuteWithoutResults();                                                                                                                                             
                        */
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
            _P pToExecute = new _P();
            pToExecute.ManagerType = "Nasdan.Core.Senses.SensesManager";
            pToExecute.FunctionName = "Process";
            pToExecute.ArgumentJson = this.Serialize(msg);
            pToExecute.ArgumentType = msg.GetType().ToString();
            pToExecute.ResultType = "Nasdan.Core.Representation._N`1";
            var query = this.Neo4j.Client.Cypher
                 //Nodes: _I, _S, ImageMessage, _P
                 .Create($"(i:_I {{iFrame}})")
                 .Create($"(m:ImageMessage {{msg}})")
                 .Create($"(s:_S)")
                 .Create($"(p:_P {{pToExecute}})")
                 .WithParams(new { iFrame, msg, pToExecute })
                 //Edges
                 .Create("(i)-[:_L]->(m)")
                 .Create("(s)<-[:_Q]-(m)")
                 .Create("(s)-[:_L]->(p)")
                 .Return(p => p.As<Neo4jClient.Node<_P>>());
            var _pNode = query.Results.Single();
            var _n = ((Neo4jClient.Node<_P>)_pNode).to_N<_P>();
            SelfActor.Tell(_n);
        }


    }
}