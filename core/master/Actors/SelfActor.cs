using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Newtonsoft.Json;
using Nasdan.API.Neo4j;
using Nasdan.Representation;
namespace Nasdan.Core.Actors
{

    internal class SelfActor : IActor //Receive (SENSES) and Sent objects (Sólo manega experiencias)
    {
        protected Cypher _cypher;
        public Cypher Cypher
        {
            get
            {
                if (_cypher == null)
                {
                    this._cypher = new Cypher(Enviroment.Representation.unified);
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
                    //Self recibe un ImageMesage
                    string output = this.Cypher.Serialize(img);                    
                    //Almacenamos ese grafo con un _E (Frame de experiencias) apuntando (Marco o contexto)
                    _E eframe = new _E();
                    this.Cypher.CreateGraph("(n:SELF)<-[:RECEIVE]-(img:ImageMessage " + output + ")");                                                            
                    //Los sentidos lo procesan automaticamente y devuelven lo que ven en colaboración con los conocimientos adquiridos (Contextualizaods)
                    //Esto devolvera un Frame (Marco) que apunta al grafo completo generado
                    var view = new ViewSense(this.Cypher);
                    _K kframe = view.Process(img); //Devuelve un Frame almacenado
                    //Enlazamos este nuevo frame de conocimiento con el frame de experiencia (Estudiar como : relación crear/generar) 
                    //o bien crear una propiedad especifica para relacionar frames sin ser relacion. (Creo que mejor relacion)                    
                    //Notificar del cambio a Will con el Frame insertado
                    break;
            }
            return Actor.Done;
        }

    }

}