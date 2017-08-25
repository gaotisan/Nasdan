using System;
namespace Nasdan.Representation
{
    internal class _I: IFrameContext //Input => Context (Frames que provienen del exterior)
    {

        public Guid Guid { get; set; }

        public _I()
        {
            this.Guid = Guid.NewGuid();
        }

        public _I(Guid id)
        {
            this.Guid = id;
        }

        


    }
}