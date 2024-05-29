using Blueprint41.DatastoreTemplates;
using Blueprint41.UnitTest.DataStore;

namespace UnitTest.Generation
{
    internal class Program
    {
        static void Main(string[] args)
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
    }
}
