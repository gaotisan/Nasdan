using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Newtonsoft.Json;
using Nasdan.API.Neo4j;
using Nasdan.Representation;
namespace Nasdan.Core.Actors
{

    internal class SensesActor : IActor //Receive (SENSES) and Sent objects (Sólo manega experiencias)
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
                    string input = this.Cypher.Serialize(img);                    
                    //Almacenamos ese grafo con un _E (Frame de experiencias) apuntando (Marco o contexto)
                    _I iFrame = new _I();
                    string iSerialized = this.Cypher.Serialize(iFrame);
                    this.Cypher.CreateGraph("(n:SELF)<-[:RECEIVE]-(img:ImageMessage " + input + ")<-(f:_I " + iSerialized + ")");                                                            
                    //Los sentidos lo procesan automaticamente y devuelven lo que ven en colaboración con los conocimientos adquiridos (Contextualizaods)
                    //Esto devolvera un Frame (Marco) que apunta al grafo completo generado
                    var view = new ViewSense(this.Cypher);
                    _K kframe = view.Process(img); //Devuelve un Frame almacenado
                    //Enlazamos este nuevo frame de conocimiento con el frame de experiencia  (_K -> _I)
                    //Además SELF->_K   
                    //Hay que buscar ese nodo SELF y crear la relación. Para ello usamos la Guid del mensaje recibido y la relación [RECEIVE]
                    
                    //Notificar del cambio a Will con el Frame insertado
                    break;
            }
            return Actor.Done;
        }

    }

}