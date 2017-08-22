using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Newtonsoft.Json;
using Nasdan.API.Neo4j;

namespace Nasdan.Core.Actors
{

    internal class SelfActor : IActor //Receive (SENSES) and Sent objects (Sólo manega experiencias)
    {
        protected Cypher _cypherExperiences;
        public Cypher CypherExperiences
        {
            get
            {
                if (_cypherExperiences == null)
                {
                    this._cypherExperiences = new Cypher(Enviroment.Representation.experiences);
                }
                return this._cypherExperiences;
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
                    string output = this.CypherExperiences.Serialize(img);
                    //output = "(andres { name:'Andres' })-[:WORKS_AT]->(neo)<-[:WORKS_AT]-(michael { name: 'Michael' })";
                    this.CypherExperiences.CreateGraph("(n:SELF)<-[:RECEIVE {Order:1}]-(img:Nasdan.Core.SensesImageMessage " + output + ")");
                    //this.Cypher.CreateGraph(output);
                    //Self recibe un ImageMesage
                    //Guardamos registro en Solr
                    //Procesamos la imagen (El procesar devuelve una representación) 
                    var view = new ViewSense(this.CypherExperiences);
                    var graph = view.Process(img);
                    //guardar grpah en neo4j
                    //Notificar del cambio a Will con el Frame insertado
                    break;
            }
            return Actor.Done;
        }

    }

}