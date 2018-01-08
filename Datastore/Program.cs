using Blueprint41.DatastoreTemplates;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace Datastore
{
    public class Program
    {
        static void Main(string[] args)
        {
            GenerateFiles();

            Console.WriteLine("----- Press any key to continue... -----");
            Console.ReadKey(true);
        }

        public static void GenerateFiles()
        {
            string path = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Datastore.Generated"));

            Generator.Execute<AdventureWorks>(new GeneratorSettings(path, "Domain.Data"));
        }
    }
}
