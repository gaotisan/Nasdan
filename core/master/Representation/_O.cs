using System;
namespace Nasdan.Representation
{
    internal class _O: IFrameContext //Output => Context (Frames que salen al exterior)
    {

        public Guid Guid { get; set; }

        public _O()
        {
            this.Guid = Guid.NewGuid();
        }

        public _O(Guid id)
        {
            this.Guid = id;
        }

        


    }
}