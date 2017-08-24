using System;
namespace Nasdan.API.Representation
{
    internal class _F //Frame => Context
    {

        public Guid Guid { get; set; }

        public _F()
        {
            this.Guid = Guid.NewGuid();
        }

        public _F(Guid id)
        {
            this.Guid = id;
        }

        


    }
}