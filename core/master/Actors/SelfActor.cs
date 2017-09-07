using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Newtonsoft.Json;
using Nasdan.Core.Neo4j;
using Nasdan.Core.Representation;
namespace Nasdan.Core.Actors
{

    internal class SelfActor : IActor //Las acciones que puede hacer nasdan. De programación o de lo que sea
    {
        protected Neo4jManager Neo4j { get; }
        public SelfActor()
        {
            this.Neo4j = new Neo4jManager(representation: Enviroment.Representation.unified);
        }

        public static void Tell(object message)
        {
            var pid = Actor.Spawn(SelfActor.Props);
            pid.Tell(message);
        }

        private static Props _props;
        private static Props Props
        {
            get
            {
                if (SelfActor._props == null)
                {
                    SelfActor._props = Actor.FromProducer(() => new SelfActor());
                }
                return SelfActor._props;
            }
        }

        public enum MessageActions
        {
            label,
        }

        public Task ReceiveAsync(IContext context)
        {
            ActorManager manager = new ActorManager(this.Neo4j);
            switch (context.Message)
            {
                //Ipunts
                case ImageMessage img:
                    manager.Receive(img);
                    break;
                //Do Task (Process)
                case _N<_P> p:
                    var pToExecute = (_N<_P>)context.Message;
                    manager.Execute(pToExecute);
                    break;
                case MessageActions msg:
                    switch (msg)
                    {
                        case MessageActions.label:
                            break;
                    }
                    break;
                    //Outputs
                    //Thinks
            }
            return Actor.Done;
        }

    }

}