using Blueprint41.DatastoreTemplates;
using Blueprint41.UnitTest.DataStore;
using Multiple = Blueprint41.UnitTest.Multiple.DataStore;

namespace UnitTest.Generation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SingleDatabase();
            MultipleDatabase();
        }

        private static void SingleDatabase()
        {
            string folder = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            while (folder != null && !File.Exists(Path.Combine(folder, "Blueprint41.sln")))
                folder = Path.GetDirectoryName(folder);

            folder = Path.Combine(folder, "Blueprint41.UnitTest");

            Generator.Execute<MockModel>(
                new GeneratorSettings(
                    folder,
                    "Datastore"
                )
            );
        }

        private static void MultipleDatabase()
        {
            string folder = Path.GetDirectoryName(typeof(Program).Assembly.Location)!;
            while (folder != null && !File.Exists(Path.Combine(folder, "Blueprint41.sln")))
                folder = Path.GetDirectoryName(folder)!;

            folder = Path.Combine(folder!, "Blueprint41.UnitTest.Multiple");

            string[] graphs = ["Neo4j", "Memgraph"];

            foreach (string graph in graphs)
            {
                string graphDir = Path.Combine(folder, graph);
                
                Directory.CreateDirectory(graphDir);

                Generator.Execute<Multiple.MockModel>(
                                new GeneratorSettings(
                                    graphDir,
                                    $"{graph}.Datastore"
                                )
                            );
            }
            
        }
    }
}
