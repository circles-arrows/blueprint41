using System;
using Blueprint41.Persistence;
using DataStore;

namespace Laboratory_version_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MockModel model = MockModel.Connect(@"bolt://localhost:7876", "neo4j", "neoneoneo", "unittest");
            model.Execute(true);

            // Generate documentation
        }
    }
}
