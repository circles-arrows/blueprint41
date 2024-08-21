using System;

using Blueprint41.Core;
using DataStore;

namespace Laboratory_version_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PersistenceProvider connection = PersistenceProvider.VoidProvider; // new PersistenceProvider("localhost", 5687, "neo4j", "neoneoneo", "unittest");

            MockModel model = new MockModel(connection);
            model.Execute(false);

            // Generate documentation
        }
    }
}
