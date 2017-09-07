using System;
using Neo4jClient;

namespace Nasdan.Core.Representation
{
    internal class _N<TNode> : Neo4jClient.Node<TNode> //Node
    {
        public _N(TNode data, NodeReference<TNode> reference) : base(data, reference)
        {

        }
        public _N(TNode data, long id, IGraphClient client) : base(data, id, client)
        {

        }


    }
}