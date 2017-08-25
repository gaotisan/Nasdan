using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Newtonsoft.Json;
using Nasdan.Core.Neo4j;
namespace Nasdan.Core.Actors
{

    internal class SelfActor : IActor //Las acciones que puede hacer nasdan. De programación o de lo que sea
    {
        
        public Task ReceiveAsync(IContext context)
        {
            //SELF RECIVE context.Message
            //Buscamos esa imagen en nuestros conocimientos.
            //Si no ésta lo que hacemos es  procesar la imagen y devolver lo que ve el sistema
            switch (context.Message)
            {
                case object obj:
                    break;
            }
            return Actor.Done;
        }

    }

}