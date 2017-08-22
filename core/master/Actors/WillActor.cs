using System.Threading.Tasks;
using Proto;
using Nasdan.Core.Senses;
using Newtonsoft.Json;
using Nasdan.API.Neo4j;
using Nasdan.API.Neo4j.Will;

namespace Nasdan.Core.Actors
{

    internal class WillActor : IActor //IA request
    {

        protected WillManager _manager;
        public WillManager Will
        {
            get
            {
                if (this._manager == null)
                {
                    this._manager = new WillManager();
                }
                return this._manager;
            }
        }

        public Task ReceiveAsync(IContext context)
        {

            return null;
        }

    }

}