using System;
namespace Nasdan.Representation
{
    internal class _K : IFrameContext //Frame => Context (Knowloges) (Provienen de razonamientos internos)
    {

        public Guid Guid { get; set; }

        public _K()
        {
            this.Guid = Guid.NewGuid();
        }

        public _K(Guid id)
        {
            this.Guid = id;
        }

        


    }
}