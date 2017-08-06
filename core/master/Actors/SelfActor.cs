using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Nasdan.Core.Representation;

namespace Nasdan.Core.Actors
{

    public class SelfActor : IActor
    {

        public Task ReceiveAsync(IContext context)
        {
            //Buscamos esa imagen en nuestros conocimientos.
            //Si no ésta lo que hacemos es  procesar la imagen y devolver lo que ve el sistema
            switch (context.Message)
            {
                case ImageMessage img:
                    //Almacenaso la acción recibida
                    var r = new Nasdan.API.Neo4j.Cypher();
                    r.Create("");
                    break;
            }
            return Actor.Done;
        }

    }

}