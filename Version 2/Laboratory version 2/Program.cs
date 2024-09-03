using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

using Blueprint41.Driver;
using Blueprint41.Persistence;

using DataStore;

namespace Laboratory
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //DryRunModel();
            ApplyModelToDB();

            //await DriverTests.TestAll().ConfigureAwait(false);
            //await DriverTests.TestConnectivity().ConfigureAwait(false);

            Console.WriteLine("Press any key...");
            Console.ReadKey(true);
        }

        private static void DryRunModel()
        {
            MockModel model = MockModel.Model;
            Debug.Assert(model.HasExecuted);
            Debug.Assert(!model.IsUpgraded);
            // Model ready to be used for model inspection (e.g. generation of code or documentation)
        }

        private static void ApplyModelToDB()
        {
            Driver.Configure<Neo4j.Driver.IDriver>();

            AdvancedConfig config = new AdvancedConfig()
            {
                DNSResolverHook = delegate (Neo4jHost host)
                {
                    return new[] { host };
                },
                CustomCypherLogging = delegate (string cypher, Dictionary<string, object?>? parameters, long elapsedMilliseconds, string? memberName, string? sourceFilePath, int sourceLineNumber)
                {
                },
                CustomLogging = delegate (string entry)
                {
                },
                ThresholdInSeconds = 0,
            };

            MockModel model = MockModel.Connect(new Uri(@"bolt://localhost:7687"), AuthToken.Basic("neo4j", "neoneoneo"), "unittest", config);
            model.Execute(true);

            Debug.Assert(model.HasExecuted);
            Debug.Assert(model.IsUpgraded);
            // Model ready to be used for both  model inspection and database access (e.g. executing transactions on the DB)
        }
    }
}