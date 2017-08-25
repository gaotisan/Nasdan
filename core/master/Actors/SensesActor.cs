using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Newtonsoft.Json;
using Nasdan.Core.Neo4j;
namespace Nasdan.Core.Actors
{

    internal class SensesActor : IActor //Receive (SENSES) and Sent objects (Sólo manega experiencias)
    {
        
        public Task ReceiveAsync(IContext context)
        {
            //SELF RECIVE context.Message
            //Buscamos esa imagen en nuestros conocimientos.
            //Si no ésta lo que hacemos es  procesar la imagen y devolver lo que ve el sistema
            switch (context.Message)
            {
                case ImageMessage img:
                    SensesManager manager = new SensesManager();                    
                    manager.Receive(img);                    
                    break;
            }
            return Actor.Done;
        }

    }

}