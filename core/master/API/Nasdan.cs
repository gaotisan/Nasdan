using Proto;
using Nasdan.Core.Actors;
using Nasdan.Core.Senses;

namespace Nasdan.Core.API
{
    public class Nasdan
    {
        protected static Props _props;
        protected static Props Props
        {
            get
            {
                if (Nasdan._props == null)
                {
                    Nasdan._props = Actor.FromProducer(() => new SelfActor());
                }
                return Nasdan._props;
            }
        }
        protected static PID GetPID()
        {
            return Actor.Spawn(Nasdan.Props);
        }


        public static void Tell(ImageMessage msg)
        {
            var pid = Nasdan.GetPID();
            pid.Tell(msg);
        }

        public static void Tell(StringMessage msg)
        {
            var pid = Nasdan.GetPID();
            pid.Tell(msg);
        }

        public static void StartServers()
        {
            System.Diagnostics.Process.Start("https://www.duckduckgo.com/?q=cat pictures");
        }

    }
}
