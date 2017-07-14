using System;
using OrientDB.Net.Core;
using Neo4jClient;
using System.Linq;

namespace library
{
    public class Hello
    {
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
    }
}
