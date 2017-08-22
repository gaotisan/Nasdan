using System;
namespace Nasdan.API.Neo4j.Representation
{
    internal class Concept
    {
        public Guid Guid { get; set; }
        public string Name { get; set; } //Property
        public string Class {get;set;} //Label

        public Concept()
        {
            
        }

        public Concept(Guid id) => this.Guid = id;




    }
}