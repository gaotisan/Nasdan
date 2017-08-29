using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Newtonsoft.Json;
using Nasdan.Core.Neo4j;
using Nasdan.Core.Representation;
namespace Nasdan.Core.Actors
{

    internal class SelfActor : IActor //Las acciones que puede hacer nasdan. De programación o de lo que sea
    {
         protected static Props _props;
        public static Props Props
        {
            get
            {
                if (SelfActor._props == null)
                {
                    SelfActor._props = Actor.FromProducer(() => new SelfActor());
                }
                return SelfActor._props;
            }
        }
        public Task ReceiveAsync(IContext context)
        {
            if (context.Message is _P){
                _P pToExecute = (_P)context.Message;
                //Lo ejecutamos Sincronamente

                //Lanzamos otro mensaje a Will que es la de buscar respuesta a cosas que no sabemos (Curiosidad), buscar preguntas sobre lo recibido                
            }            
            return Actor.Done;
        }

    }

}