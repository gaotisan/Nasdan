using System;
using Nasdan.Core.Neo4j;
using Nasdan.Core.Representation;
using System.Linq;
using Nasdan.Core.Senses;
using Proto;
using System.Collections.Generic;
namespace Nasdan.Core.Actors
{
    internal partial class ActorManager
    {

        protected Neo4jManager Neo4j { get; }

        public ActorManager(Neo4jManager neo4j)
        {
            this.Neo4j = neo4j;
        }
       

        public void ExecuteAllProcess()
        {
            /* 
            var query = this.Neo4j.Client.Cypher
                .Match("(p:_P)")
                .Return(p => p.As<Neo4jClient.Node<_P>>());
            IEnumerable<Neo4jClient.Node<_P>> _pNodes = query.Results;
            foreach (var node in _pNodes)
            {
                Actor.FromProducer(() => new WillActor());
                var pid = Actor.Spawn(SensesActor.Props);
                pid.Tell(node);
            }
            */
        }

        public void Execute(Neo4jClient.Node<_P> p)
        {

        }

        public void Receive(ImageMessage msg)
        {
            //Grafo 1: (_Input)-[:_R]->(ImageMessage)  
            //Grafo 2: (ImageMessage)-[:_R]->(_SELF)
            //Grafo 3: (_SELF)-[:_R]->(_P)
            _I iFrame = new _I();
            _S self = new _S();
            _P pToExecute = new _P();
            pToExecute.ClassName = "Nasdan.Core.Senses.ViewSense";
            pToExecute.FunctionName = "Process";
            pToExecute.Arguments = this.Neo4j.Serialize(msg, true);
            var query = this.Neo4j.Client.Cypher
                 //Nodes: _I, _S, ImageMessage, _P
                 .Create($"(i:_I {{iFrame}})")
                 .Create($"(m:ImageMessage {{msg}})")
                 .Create($"(s:_S {{self}})")
                 .Create($"(p:_P {{pToExecute}})")
                 .WithParams(new { iFrame, msg, self, pToExecute })
                 //Edges
                 .Create("(i)-[:_R]->(m)")
                 .Create("(s)<-[:_R]-(m)")
                 .Create("(s)-[:_R]->(p)")
                 .Return(p => p.As<Neo4jClient.Node<_P>>());
            var _pNode = query.Results.Single();            
            SelfActor.Tell(_pNode);
        }


    }
}