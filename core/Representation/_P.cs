using System;
namespace Nasdan.Core.Representation
{
    internal class _P //Process to Excute by SELF
    {
        //Function
        public string ManagerAssemblyPath { get; set; }
        public string ManagerType { get; set; }
        public string FunctionName { get; set; }

        //(Argumen1)-[Function]->(Argument2)
        //Argument 1 (Node init)
        public string Argument1AssemblyPath { get; set; }
        public string Argument1Json { get; set; }
        public string Argument1Type { get; set; }

        //Argument 2 (Node finish)
        public string Argument2AssemblyPath { get; set; }
        public string Argument2Json { get; set; }
        public string Argument2Type { get; set; }

        //Result
        public string ResultAssemblyPath { get; set; }
        public string ResultType { get; set; }



    }
}