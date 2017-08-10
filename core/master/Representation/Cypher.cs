using System;
using System.IO;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Nasdan.API.Neo4j
{
    internal class Cypher
    {
        public GraphClient Client { get; protected set; }

        public enum Representation
        {
            knowloges,
            experiences,
        }

        private string _user = "neo4j";
        private string _password = "123";

        private string _neo4jVersion = "neo4j-3.2.3";


        public Cypher(Representation representation)
        {
            string uri = this.GetUriServer(representation);



        }

        public string GetFolderServer(Representation representation, OSPlatform platform)
        {
            string result = representation.ToString() + "-" + this._neo4jVersion;
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                result += "-windows";
            }
            else
            {
                result += "-linux-mac";
            }
            return result;
        }

        public string GetUriServer(Representation representation)
        {
            string result = "http://localhost.:7474/db/data";
            if (representation == Representation.experiences)
            {
                result = "http://localhost.:7474/db/data";
            }
            return result;
        }

        public Cypher(string uri, string user, string password)
        {
            this.Client = new GraphClient(new Uri(uri), this._user, this._password);
            this.Client.Connect();
        }

        public string Serialize(object obj)
        {
            var serializer = new JsonSerializer();
            var stringWriter = new StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                writer.QuoteName = false;
                serializer.Serialize(writer, obj);
            }
            return stringWriter.ToString();
        }
        public void CreateGraph(string graph)
        {
            this.Client.Cypher.Create(graph)
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