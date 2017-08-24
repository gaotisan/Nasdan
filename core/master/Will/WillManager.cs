using System;
namespace Nasdan.API.Neo4j.Will
{
    internal partial class WillManager
    {

        protected Cypher Cypher { get; }
        

        public WillManager()
        {
            this.Cypher = new Cypher(representation: Enviroment.Representation.unified);
           
        }




    }
}