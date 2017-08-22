using Proto;
using Nasdan.Core.Actors;
using Nasdan.Core.Senses;
using Nasdan.API.Neo4j;

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
            Nasdan._startProcess(Enviroment.GetFileName(Enviroment.Representation.experiences), Enviroment.GetStartArgument());
            Nasdan._startProcess(Enviroment.GetFileName(Enviroment.Representation.knowloges), Enviroment.GetStartArgument());
        }

        protected static void _startProcess(string filename, string arguments){
            var server = new System.Diagnostics.Process();
            server.StartInfo.FileName = filename;
            server.StartInfo.Arguments = arguments;
            server.Start();
        }

    }






}
