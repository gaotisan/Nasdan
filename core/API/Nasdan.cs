using Proto;
using Nasdan.Core.Actors;
using Nasdan.Core.Senses;
using Nasdan.Core.Neo4j;

namespace Nasdan.Core.API
{
    public class Nasdan
    {


        public static void Tell(ImageMessage msg) => SelfActor.Tell(msg);
        
        public static void Tell(StringMessage msg) => SelfActor.Tell(msg);
                           
        public static void StartServers()
        {
            Nasdan._startProcess(Enviroment.GetFileName(Enviroment.Representation.experiences), Enviroment.GetStartArgument());
            Nasdan._startProcess(Enviroment.GetFileName(Enviroment.Representation.knowloges), Enviroment.GetStartArgument());
        }

        protected static void _startProcess(string filename, string arguments)
        {
            var server = new System.Diagnostics.Process();
            server.StartInfo.FileName = filename;
            server.StartInfo.Arguments = arguments;
            server.Start();
        }

    }






}
