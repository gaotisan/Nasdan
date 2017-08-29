using System;
using Nasdan.Core.Neo4j;
using Nasdan.Core.Representation;
using System.Linq;
using Nasdan.Core.Senses;
using Proto;

namespace Nasdan.Core.Actors
{
    internal partial class ActorManager
    {

        protected Neo4jManager Neo4j { get; }


        public ActorManager()
        {
            this.Neo4j = new Neo4jManager(representation: Enviroment.Representation.unified);
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
                 .Create("(s)-[:_R]->(p)");          
            Actor.FromProducer(() => new WillActor());
            var pid = Actor.Spawn(SensesActor.Props);
            pid.Tell(WillActor.Notification._PAdded);                          
        }


    }
}