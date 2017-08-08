using System;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using Newtonsoft.Json;

namespace Nasdan.API.Neo4j
{
    internal class Cypher
    {
        public GraphClient Client { get; protected set; }
        public Cypher()
        {
            this.Client = new GraphClient(new Uri("http://localhost.:7474/db/data"), "neo4j", "123");
            this.Client.Connect();
        }
        public void CreateGraph(string graph)
        {
             this.Client.Cypher.Create("(n:SELF)")
                .ExecuteWithoutResults();
        }
        /*
        public Person GetTomHanks(){
            var client = new GraphClient(new Uri("http://127.0.0.1:7474/browser/"),"neo4j", "123");
            client.Connect();
            Person tom = client.Cypher.Match("(person:Person)")
                .Where((Person person) => person.name == "Tom Hanks")
                .Return(person => person.As<Person>())
                .Results
                .Single();
                
            return tom;
        }
         */

    }

}