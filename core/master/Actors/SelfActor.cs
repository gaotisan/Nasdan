using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Nasdan.Core.Representation;
using Newtonsoft.Json;
using Nasdan.API.Neo4j;

namespace Nasdan.Core.Actors
{

    internal class SelfActor : IActor
    {
        protected Cypher _cypher;
        public Cypher Cypher
        {
            get
            {
                if (_cypher == null)
                {
                    this._cypher = new Cypher();
                }
                return this._cypher;
            }
        }

        public Task ReceiveAsync(IContext context)
        {
            //SELF RECIVE context.Message
            //Buscamos esa imagen en nuestros conocimientos.
            //Si no ésta lo que hacemos es  procesar la imagen y devolver lo que ve el sistema
            switch (context.Message)
            {
                case ImageMessage img:
                    string output = this.Cypher.Serialize(img);
                    //output = "(andres { name:'Andres' })-[:WORKS_AT]->(neo)<-[:WORKS_AT]-(michael { name: 'Michael' })";
                    this.Cypher.CreateGraph("(n:SELF:ACTOR)<-[:RECEIVE]-(img " + output + ")");
                    //this.Cypher.CreateGraph(output);
                    //Self recibe un ImageMesage
                    //Guardamos registro en Solr
                    //Procesamos la imagen (El procesar devuelve una representación) 
                    var view = new ViewSense();
                    var graph = view.Process(img);
                    //guardar grpah en neo4j
                    break;
            }
            return Actor.Done;
        }

    }

}