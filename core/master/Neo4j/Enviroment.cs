

using System.Runtime.InteropServices;

namespace Nasdan.API.Neo4j
{
    internal class Enviroment
    {
        public enum Representation
        {
            knowloges,
            experiences,
            unified,
        }

        private const string _neo4jVersion = "neo4j-3.2.3";
        private const string _rootServerLocation = "C:/github/Nasdan/core/master/";
       
        public static OSPlatform GetOSPlatform()
        {
            OSPlatform result = OSPlatform.OSX;
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                result = OSPlatform.Windows;
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                result = OSPlatform.Linux;
            }
            return result;
        }
        public static string GetBinFolderServer(Representation representation)
        {
            string result = representation.ToString() + "-" + Enviroment._neo4jVersion;
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                result += "-windows";
            }
            else
            {
                result += "-linux-mac";
            }
            result = Enviroment._rootServerLocation + result + "/bin/";
            return result;
        }
        public static string GetFileName(Representation representation, bool pathCompleted = true)
        {
            string result = "neo4j";
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                result = "neo4j.bat";
            }
            if (pathCompleted)
            {
                result = Enviroment.GetBinFolderServer(representation) + result;
            }
            return result;
        }

        public static string GetStartArgument()
        {
            return "console";
        }

        public static string GetUriServer(Representation representation)
        {
            //string result = "http://localhost.:7373/db/data";
            //if (representation == Representation.experiences)
            //{
            //    result = "http://localhost.:7474/db/data";
            //}
            //return result;
            return "http://localhost.:7474/db/data";
        }


    }
}