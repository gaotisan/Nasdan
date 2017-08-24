using System;
namespace Nasdan.API.Neo4j.Will
{
    internal partial class WillManager
    {

        protected Cypher CypherExperiences { get; }
        protected Cypher CypherKnowloges { get; }

        public WillManager()
        {
            this.CypherExperiences = new Cypher(Enviroment.Representation.experiences);
            this.CypherKnowloges = new Cypher(Enviroment.Representation.knowloges);
        }




    }
}