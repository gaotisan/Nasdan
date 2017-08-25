using System;
using System.IO;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Nasdan.API.Neo4j
{
    internal partial class Cypher
    {
        public GraphClient Client { get; protected set; }

        private const string _user = "neo4j";
        private const string _password = "123";
        public Cypher(Enviroment.Representation representation)
        {
            string uri = Enviroment.GetUriServer(representation);
            this.Client = new GraphClient(new Uri(uri), Cypher._user, Cypher._password);
            this.Client.Connect();


        }

        public string Serialize(object obj, bool addType = false)
        {
            string result = "null";
            if (obj != null)
            {
                var serializer = new JsonSerializer();
                var stringWriter = new StringWriter();
                using (var writer = new JsonTextWriter(stringWriter))
                {
                    writer.QuoteName = false;
                    serializer.Serialize(writer, obj);
                }
                result = stringWriter.ToString();
                if (addType)
                {
                    string t = "{Type:\"" + obj.GetType() + "\",";
                    result = t + result.Substring(1);
                }
            }
            return result;
        }

        public void CreateGraph(string graph)
        {
            this.Client.Cypher.Create(graph).ExecuteWithoutResults();
        }

        #region Cypher Expresions
        public string GetCypherExpresion_Node(string label)
        {
            return null;
        }
        public string GetCypherExpresion_Node(IEnumerable<string> labels)
        {
            return null;
        }

        public string GetCypherExpresion_Node(object obj)
        {
            return null;
        }


        public enum EdgeDirection
        {
            inDirection,//<-
            outDirection, //->
            //BiDirection, //<->
        }


        public string GetCypherExpresion_Edge(EdgeDirection direction, string name)
        {
            return null;
        }

        public string GetCypherExpresion_Edge(EdgeDirection direction, string name, object obj)
        {
            return null;
        }

        #endregion

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