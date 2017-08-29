using System.Threading.Tasks;
using Nasdan.Core.Representation;
using Nasdan.Core.Neo4j;
namespace Nasdan.Core.Senses
{
    internal class ViewSense
    {
        protected Neo4jManager _neo4j;
        public ViewSense(Neo4jManager cypher)
        {
            this._neo4j = cypher;
        }
        public ViewSense(){
            this._neo4j = new Neo4jManager(representation: Enviroment.Representation.unified);
        }

        public _K Process(ImageMessage img)
        {
            //Guardamos lo recibido en Solr. 

            //-Usuario que lo envia (sesión), lo suyo es que se guarde por sesión las conversaciones y las imagenes recibidas en esa sesión
            //-Buscamos en nuestros conocimientos esa imagen tal cual a ver si ya ha sido analizada antes. Si
            //no existe creamos un nuevo grafo y lo enlazamos a lo metido en Solr. Ese grafo tiene todas las
            //propiedades percibidas pero sin categorizar.
            if (img != null && !string.IsNullOrEmpty(img.Url))
            {
                switch (img.Url.ToLower())
                {
                    case "~/images/inputs/colors-black.gif":
                    case "~/images/inputs/colors-blue.gif":
                    case "~/images/inputs/colors-red.gif":
                    case "~/images/inputs/colors-orange.gif":
                    case "~/images/inputs/colors-white.gif":
                    case "~/images/inputs/colors-green.gif":
                        this._neo4j.CreateGraph("(n:SELF)-[:PROCESS {Order:2}]->(img " + ":?)-[:IS]->(img :?)");
                        break;
                }
            }
            return null;
        }


    }

}