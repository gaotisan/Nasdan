using Nasdan.Core.Representation;
namespace Nasdan.Core.Neo4j
{
    public static class Extension
    {
        internal static _N<TNode> to_N<TNode>(this Neo4jClient.Node<TNode> node)
        {
            return new _N<TNode>(node.Data, node.Reference);
        }
    }
}