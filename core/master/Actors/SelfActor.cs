using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Nasdan.Core.Representation;

namespace Nasdan.Core.Actors
{

    internal class SelfActor : IActor
    {

        public Task ReceiveAsync(IContext context)
        {
            //Buscamos esa imagen en nuestros conocimientos.
            //Si no ésta lo que hacemos es  procesar la imagen y devolver lo que ve el sistema
            switch (context.Message)
            {
                case ImageMessage img:
                    //Self recibe un ImageMesage
                    //Guardamos registro en Solr
                    //Procesamos la imagen (El procesar devuelve una representación) 
                    var r = new Nasdan.API.Neo4j.Cypher();
                    r.CreateGraph("(self)");
                    var view = new ViewSense();
                    var graph = view.Process(img);
                    //guardar grpah en neo4j
                    break;
            }
            return Actor.Done;
        }

    }

}