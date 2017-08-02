using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;

namespace Nasdan.Core.Actors
{

    

    public class NasdanActor : IActor
    {

        public Task ReceiveAsync(IContext context)
        {
            //Buscamos esa imagen en nuestros conocimientos.
            //Si no ésta lo que hacemos es  procesar la imagen y devolver lo que ve el sistema
            switch (context.Message)
            {
                case ImageMessage img:
                    break;
            }
            return Actor.Done;
        }

    }

}