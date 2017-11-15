using System;

namespace Nasdan.Core.Representation
{
    internal abstract class Frame : IFrameContext
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