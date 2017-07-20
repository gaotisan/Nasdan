using System;
using Neo4jClient;
using System.Linq;

namespace library
{
    public class Test
    {
        public Person GetTomHanks(){
            var client = new GraphClient(new Uri("http://127.0.0.1:7474/db/data"),"neo4j", "123");
            client.Connect();
            Person tom = client.Cypher.Match("(person:Person)")
                .Where((Person person) => person.name == "Tom Hanks")
                .Return(person => person.As<Person>())
                .Results
                .Single();
                
            return tom;
        }
    }
}
