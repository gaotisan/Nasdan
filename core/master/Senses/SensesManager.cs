using System.Threading.Tasks;
using Nasdan.Core.Representation;
using Nasdan.Core.Neo4j;
using System.Linq;
namespace Nasdan.Core.Senses
{
    internal class SensesManager : Manager
    {

        public SensesManager(Neo4jManager neo4j) : base(neo4j)
        {

        }

        public object Process(ImageMessage img)
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
                        this.Neo4j.CreateGraph("(n:SELF)-[:PROCESS {Order:2}]->(img " + ":?)-[:IS]->(img :?)");
                        break;
                }
            }
            _C concept1 = new _C();
            concept1.Name = img.Url;
            _C concept2 = new _C();
            concept2.Name = null;
            var query = this.Neo4j.Client.Cypher
                 //Nodes
                 .Create($"(c1:_Q:_C {{concept1}})")
                 .Create($"(c2:_Q:_C {{concept2}})")
                 .WithParams(new { concept1, concept2 })
                 //Edges
                 .Create("(c1)-[:_Q]->(c2)")
                 .Return(c1 => c1.As<Neo4jClient.Node<_C>>());
            var _pNode = query.Results.Single();
            var _n = ((Neo4jClient.Node<_C>)_pNode).to_N<_C>();
            return _n;
        }


    }

}