using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

namespace Blueprint41.ApocDocumentationParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApocDocumentation doc = ApocDocumentation.Parse("4.4");
            doc.SyncTo(Path.GetFullPath(Path.Combine(doc.ProjectFolder, "..", "Blueprint41", "Query", "Functions.Apoc.xml")));

            Console.WriteLine("Finished");
            Console.ReadKey(true);
        }
    }
}