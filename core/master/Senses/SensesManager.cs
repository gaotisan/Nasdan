using System;
using Nasdan.Core.Neo4j;
using Nasdan.Core.Representation;
using System.Linq;

namespace Nasdan.Core.Senses
{
    internal partial class SensesManager
    {

        protected Neo4jManager Neo4j { get; }


        public SensesManager()
        {
            this.Neo4j = new Neo4jManager(representation: Enviroment.Representation.unified);

        }



        public void Receive(ImageMessage msg)
        {
            //Almacenamos nuevo grafo: (:_I)-[:_R]->(:ImageMessage)  
            //'donde _I es un Frame de input, _R es una relación base (ya que no puede ser vacio) y ImageMessage es el mensage recibido
            _I iFrame = new _I();
            var query = this.Neo4j.Client.Cypher
                 .Create($"(i:_I {{iFrame}})")
                 .Create($"(m:ImageMessge {{msg}})")
                 .WithParams(new {iFrame, msg})
                 .Create("(i)-[:_Test]->(m)");
            query.ExecuteWithoutResults();     
            

            //string iNode = this.Neo4j.GetCypherExpresion_Node("_I", iFrame, "i");
            //string msgNode = this.Neo4j.GetCypherExpresion_Node("ImageMessage", msg);
            //string _rEdge = this.Neo4j.GetCypherExpresion_Edge(Neo4jManager.EdgeDirection.outDirection);
            //this.Neo4j.CreateGraph(iNode + _rEdge + msgNode);



            /* 
                         var query = client.Cypher
                    .Create($"(o:{GetLabel<Owner>()} {{owner}})")
                    .Create($"(i:{GetLabel<Identity>()} {{identity}})")
                    .WithParams(new {owner, identity})
                    .Create("(o)-[:HAS]->(i)")
                    .Return(o => o.As<Node<Owner>>());

                return query.Results.Single(); */


            //Nuevo: (:_S)<-[:RECEIVE]-(:ImageMessage)
            //SELF recibe un ImageMessage
            string selfNode = this.Neo4j.GetCypherExpresion_Node("_S");
            string receiveEdge = this.Neo4j.GetCypherExpresion_Edge(Neo4jManager.EdgeDirection.outDirection, "RECEIVE");
            //Cogemos el Nodo ImageMessage con ese Guid y le metemos la relación

            //this.Cypher.Client.

            //'MATCH (u:User {username:'admin'}), (r:Role {name:'ROLE_WEB_USER'})
            //'CREATE (u)-[:HAS_ROLE]->(r)
            //string iSerialized = this.Cypher.Serialize(iFrame);
            //this.Cypher.CreateGraph("(n:SELF)<-[:RECEIVE]-(img:ImageMessage " + input + ")<-[]-(f:_I " + iSerialized + ")");
            //Los sentidos lo procesan automaticamente y devuelven lo que ven en colaboración con los conocimientos adquiridos (Contextualizaods)
            //Esto devolvera un Frame (Marco) que apunta al grafo completo generado
            // var view = new ViewSense(this.Cypher);
            // _K kframe = view.Process(img); //Devuelve un Frame almacenado
            //Enlazamos este nuevo frame de conocimiento con el frame de experiencia  (_K -> _I)
            //Además SELF->_K   
            //Hay que buscar ese nodo SELF y crear la relación. Para ello usamos la Guid del mensaje recibido y la relación [RECEIVE]

            //Notificar del cambio a Will con el Frame insertado
        }


    }
}