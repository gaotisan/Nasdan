using System;
namespace Nasdan.Representation
{
    internal class _F : IFrameContext //Frame de Frames: Enlaca con frames Ej: _K,_I,_O
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