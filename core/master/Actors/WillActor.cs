using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Newtonsoft.Json;
using Nasdan.Core.Will;

namespace Nasdan.Core.Actors
{

    internal class WillActor : IActor //IA request
    {


        protected static Props _props;
        public static Props Props
        {
            get
            {
                if (WillActor._props == null)
                {
                    WillActor._props = Actor.FromProducer(() => new WillActor());
                }
                return WillActor._props;
            }
        }

        public enum Notification
        {
            _PAdded,
            _News,
        }

        public Task ReceiveAsync(IContext context)
        {
            if (context.Message is Notification)
            {
                Notification n = (Notification)context.Message;
                switch (n)
                {
                    case Notification._PAdded:
                        //Buscamos todos los procesos y los ejecutamos

                        //Lanzamos otro mensaje a Will que es la de buscar respuesta a cosas que no sabemos (Curiosidad), buscar preguntas sobre lo recibido                
                        break;
                    case Notification._News:
                        //Buscamos respuestas
                        break;

                }
            }

            return null;
        }

    }

}