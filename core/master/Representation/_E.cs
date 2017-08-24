using System;
namespace Nasdan.API.Representation
{
    internal class _E: IContext //Experience => Context (Frames que provienen del exterior)
    {

        public Guid Guid { get; set; }

        public _E()
        {
            this.Guid = Guid.NewGuid();
        }

        public _E(Guid id)
        {
            this.Guid = id;
        }

        


    }
}