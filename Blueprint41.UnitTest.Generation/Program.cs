using Blueprint41.DatastoreTemplates;
using Blueprint41.UnitTest.DataStore;
using Multiple = Blueprint41.UnitTest.Multiple;

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


            #region Neo4j Code Generation

            string graphDir = Path.Combine(folder, "Neo4j", "Generated");

            Directory.CreateDirectory(graphDir);

            Generator.Execute<Multiple.Neo4j.DataStore.Neo4jModel>(
                            new GeneratorSettings(
                                graphDir,
                                $"Neo4j.Datastore"
                            )
                        );

            #endregion

            #region Memgraph Code Generation

            graphDir = Path.Combine(folder, "Memgraph", "Generated");

            Directory.CreateDirectory(graphDir);

            Generator.Execute<Multiple.Memgraph.DataStore.MemgraphModel>(
                          new GeneratorSettings(
                              graphDir,
                              $"Memgraph.Datastore"
                          )
                      );

            #endregion
        }
    }
}
