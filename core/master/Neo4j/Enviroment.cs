

using System.Runtime.InteropServices;

namespace Nasdan.API.Neo4j
{
    internal class Enviroment
    {
        public enum Representation
        {
            knowloges,
            experiences,
        }

        private const string _neo4jVersion = "neo4j-3.2.3";
        private const string _rootGraphLinuxMacFolder = "C:/Users/Santi/desktop/Nasdan/core/master/";
        private const string _rootGraphWindowsFolder = "C:/Users/santiago.ochoa/Desktop/Nasdan/core/master/";

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
        public static string GetFolderServer(Representation representation, OSPlatform platform)
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
            return result;
        }

        public static string GetUriServer(Representation representation)
        {
            string result = "http://localhost.:7474/db/data";
            if (representation == Representation.experiences)
            {
                result = "http://localhost.:7575/db/data";
            }
            return result;
        }
    }
}