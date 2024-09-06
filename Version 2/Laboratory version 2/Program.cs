using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

using Blueprint41;
using Blueprint41.Persistence;
using AuthToken = Blueprint41.Driver.AuthToken;
using Driver = Blueprint41.Driver.Driver;
using ResultCursor = Blueprint41.Driver.ResultCursor;

using DataStore;

namespace Laboratory
{
    internal static class Program
    {
#pragma warning disable CS1998
        static async Task Main(string[] args)
#pragma warning restore CS1998
        {
            //DryRunModel();
            await ApplyModelToDB();

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

        private static async Task ApplyModelToDB()
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
            await CleanDB();

            model.Execute(true);

            Debug.Assert(model.HasExecuted);
            Debug.Assert(model.IsUpgraded);
            // Model ready to be used for both  model inspection and database access (e.g. executing transactions on the DB)

        }

        private static async Task CleanDB()
        {
            await using (var trans = await MockModel.BeginTransactionAsync())
            {
                await trans.RunAsync("MATCH (n) DETACH DELETE n;");
                await trans.RunAsync("CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *;");
            }
        }

        private static void CopyDB()
        {
            using (IStatementRunner a = MockModel.BeginSession())
            using (IStatementRunner b = MockModel.BeginSession())
            {
                a.Run("MATCH ____");
                b.Run("MERGE ____");
            }
        }

    }
}