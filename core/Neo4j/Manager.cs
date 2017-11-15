namespace Nasdan.Core.Neo4j
{
    internal abstract class Manager
    {
        public Neo4jManager Neo4j { get; }
        public Manager(Neo4jManager parameters)
        {
            this.Neo4j = parameters;
        }
    }
}