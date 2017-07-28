using System.Threading.Tasks;
using Proto;

namespace Nasdan.Core.Actors
{

    public class ImageMessage
    {

        public string Url { get; set; }

    }

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