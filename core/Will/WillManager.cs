using System;
using Nasdan.Core.Neo4j;
namespace Nasdan.Core.Will
{
    internal partial class WillManager
    {

        protected Neo4jManager _neo4j { get; }
        

        public WillManager()
        {
            this._neo4j = new Neo4jManager(representation: Enviroment.Representation.unified);
           
        }




    }
}