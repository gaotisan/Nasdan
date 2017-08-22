using System;
namespace Nasdan.API.Neo4j.Representation
{
    internal class Frame
    {

        public Guid Guid { get; set; }

        public Frame()
        {
            this.Guid = Guid.NewGuid();
        }

        public Frame(Guid id)
        {
            this.Guid = id;
        }

        


    }
}