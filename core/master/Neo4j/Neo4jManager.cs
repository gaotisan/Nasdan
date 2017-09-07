using System;
using System.IO;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Reflection;

namespace Nasdan.Core.Neo4j
{
    internal partial class Neo4jManager
    {
        public GraphClient Client { get; protected set; }

        private const string _user = "neo4j";
        private const string _password = "123";
        public Neo4jManager(Enviroment.Representation representation)
        {
            string uri = Enviroment.GetUriServer(representation);
            this.Client = new GraphClient(new Uri(uri), Neo4jManager._user, Neo4jManager._password);
            this.Client.Connect();


        }
        public Neo4jManager()
        {
            string uri = Enviroment.GetUriServer(Enviroment.Representation.unified);
            this.Client = new GraphClient(new Uri(uri), Neo4jManager._user, Neo4jManager._password);
            this.Client.Connect();
        }

        public void Populate(string value, object target){
            JsonConvert.PopulateObject(value, target);
        }

        
        public Type GetType(string json){
            Type result = typeof(Dictionary<string, object>);
            if (json.StartsWith("{Type:")){
                var index = json.IndexOf(",");
                var t = json.Substring(5,5-index);                
            }
            return result;
        }

        public object GetParameter(string json){
            object result = null;
            if (json != string.Empty){
                
            }
            return result;
        }

        public object GetInstance(string json){
            return null;
        }

        public object GetInstancePopulate(string json){
            return null;
        }

        public object Deserialize(string json)
        {
            object result = null;
            /* 
            if (json != string.Empty)
            {
                if (json.StartsWith("{Type:")){

                }
                JsonConvert.PopulateObject()
                var test = JsonConvert.DeserializeObject<Neo4jManager>(json);
                //var serializer = new JsonSerializer();
                //var stringReader = new StringReader(json);
                //using (var reader = new JsonTextReader(stringReader))
                //{
                //    result = serializer.Deserialize(reader);
                //}
            }
            */
            return result;
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

        public string GetLabel(object obj)
        {
            string result = string.Empty;
            if (obj != null)
            {
                result = obj.GetType().Name;
            }
            return result;
        }

        public void CreateGraph(string graph)
        {
            this.Client.Cypher.Create(graph).ExecuteWithoutResults();
        }

        #region Cypher Expresions
        public string GetCypherExpresion_Node(string label, object obj = null, string reference = "")
        {
            string objSerialized = string.Empty;
            if (obj != null)
            {
                objSerialized = " " + this.Serialize(obj);
            }
            return "(" + reference + ":" + label + objSerialized + ")";
        }

        public string GetCypherExpresion_Node(IEnumerable<string> labels, object obj = null, string reference = "")
        {
            return null;
        }



        public enum EdgeDirection
        {
            inDirection,//<-[]-
            outDirection, //-[]->
            //BiDirection, //<-[]-> //No existen en Neo4j
            //Neutral, //-[] -
        }


        public string GetCypherExpresion_Edge(EdgeDirection direction, string type = "_R", object obj = null, string reference = "")
        {
            string objSerialized = string.Empty;
            if (obj != null)
            {
                objSerialized = " " + this.Serialize(obj);
            }
            string result = "[" + reference + ":" + type + objSerialized + "]";
            switch (direction)
            {
                case Neo4jManager.EdgeDirection.inDirection:
                    result = "<-" + result + "-";
                    break;
                case Neo4jManager.EdgeDirection.outDirection:
                    result = "-" + result + "->";
                    break;
            }
            return result;
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