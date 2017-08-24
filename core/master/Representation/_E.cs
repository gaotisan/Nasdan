using System;
namespace Nasdan.Representation
{
    internal class _E: IFrameContext //Experience => Context (Frames que provienen del exterior)
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