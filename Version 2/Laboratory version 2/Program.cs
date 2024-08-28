using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Kerberos.NET.Win32;

using Blueprint41;
using Blueprint41.Persistence;

using DataStore;
using System.Reflection;
using System.IO;


namespace Laboratory
{
    internal class Program
    {
        private const int MAX_CONNECTION_POOL_SIZE = 400;
        private const int DEFAULT_READWRITESIZE = 65536;
        private const int DEFAULT_READWRITESIZE_MAX = 655360;

        static async Task Main(string[] args)
        {
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string[] queries = File.ReadAllText(Path.Combine(path, "movies.cypher")).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            Driver.Configure<Neo4j.Driver.IDriver>();

            #region Test Auth Tokens

            AuthToken voidToken = AuthToken.None;
            AuthToken basicToken1 = AuthToken.Basic("neo4j", "neoneoneo");
            AuthToken basicToken2 = AuthToken.Basic("neo4j", "neoneoneo", "realm");
            AuthToken kerberosToken = AuthToken.Kerberos(GetBase64KerberosTicket());
            AuthToken customToken1 = AuthToken.Custom("principal", "credentials", "realm", "scheme");
            AuthToken customToken2 = AuthToken.Custom("principal", "credentials", "realm", "scheme", new Dictionary<string, object>());
            AuthToken bearerToken = AuthToken.Bearer("SlAV32hkKG");

            #endregion

            #region Test Connectivity

            Driver driver = Driver.Get(new Uri(@"bolt://localhost:7687"), AuthToken.Basic("neo4j", "neoneoneo"), delegate (ConfigBuilder o)
            {
                o.WithFetchSize(ConfigBuilder.Infinite);
                o.WithMaxConnectionPoolSize(MAX_CONNECTION_POOL_SIZE);
                o.WithDefaultReadBufferSize(DEFAULT_READWRITESIZE);
                o.WithDefaultWriteBufferSize(DEFAULT_READWRITESIZE);
                o.WithMaxReadBufferSize(DEFAULT_READWRITESIZE_MAX);
                o.WithMaxWriteBufferSize(DEFAULT_READWRITESIZE_MAX);
                o.WithMaxTransactionRetryTime(TimeSpan.Zero);
            });

            #endregion

            #region Test Running a Query on the Session

            Bookmark bookmark;

            await CleanDB().ConfigureAwait(false);

            await using (DriverSession session = driver.AsyncSession(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(ReadWriteMode.ReadWrite); }))
            {
                foreach (string query in queries)
                {
                    Console.WriteLine(query);

                    DriverRecordSet result = await session.RunAsync(query).ConfigureAwait(false);
                    await result.ConsumeAsync().ConfigureAwait(false);
                }
                bookmark = session.LastBookmarks;
            }

            Console.Clear();

            await using (DriverSession session = driver.AsyncSession(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(ReadWriteMode.ReadOnly); config.WithBookmarks(bookmark); }))
            {
                Dictionary<string, object?> parameters = new Dictionary<string, object?>()
                {
                    { "actor", "Keanu Reeves" },
                };
                DriverRecordSet result = await session.RunAsync("""
                    MATCH (m:Movie)<-[:ACTED_IN]-(p:Person)
                    WHERE p.name = $actor
                    RETURN m.title AS Movie, p.name AS Actor
                    """,
                    parameters).ConfigureAwait(false);

                bool fetch = await result.FetchAsync().ConfigureAwait(false);
                while (fetch)
                {
                    IReadOnlyDictionary<string, object?> values = DriverRecord.Values(result.Current);

                    Console.WriteLine($"Movie: '{values["Movie"]}', Actor: '{values["Actor"]}'.");

                    fetch = await result.FetchAsync().ConfigureAwait(false);
                }
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey(true);

            #endregion

            #region Test Running a Query on the Transaction
            
            await CleanDB().ConfigureAwait(false);

            await using (DriverSession session = driver.AsyncSession(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(ReadWriteMode.ReadWrite); }))
            {
                foreach (string query in queries)
                {
                    Console.WriteLine(query);
                    await using (DriverTransaction transaction = await session.BeginTransactionAsync(config => { config.WithTimeout(TimeSpan.FromSeconds(30)); }).ConfigureAwait(false))
                    {

                        DriverRecordSet result = await transaction.RunAsync(query).ConfigureAwait(false);
                        DriverResultSummary resultSummary = await result.ConsumeAsync().ConfigureAwait(false);
                        System.Diagnostics.Debug.WriteLine(resultSummary.Query.Text);

                        await transaction.CommitAsync();
                    }
                }
                bookmark = session.LastBookmarks;

                Console.Clear();

                await using (DriverTransaction transaction = await session.BeginTransactionAsync(config => { config.WithTimeout(TimeSpan.FromSeconds(30)); config.WithMetadata(new Dictionary<string, object>() { { "Hello", "World" } }); }).ConfigureAwait(false))
                {
                    Dictionary<string, object?> parameters = new Dictionary<string, object?>()
                    {
                        { "actor", "Keanu Reeves" },
                    };
                    DriverRecordSet result = await transaction.RunAsync("""
                    MATCH (m:Movie)<-[:ACTED_IN]-(p:Person)
                    WHERE p.name = $actor
                    RETURN m.title AS Movie, p.name AS Actor
                    """,
                        parameters).ConfigureAwait(false);

                    bool fetch = await result.FetchAsync().ConfigureAwait(false);
                    while (fetch)
                    {
                        IReadOnlyDictionary<string, object?> values = DriverRecord.Values(result.Current);

                        Console.WriteLine($"Movie: '{values["Movie"]}', Actor: '{values["Actor"]}'.");

                        fetch = await result.FetchAsync().ConfigureAwait(false);
                    }

                    await transaction.RollbackAsync();
                }
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey(true);

            #endregion

            /* 
            Missing:

            ResultSet -> 
            ResultSet -> Summary
            ResultSet -> Notifications

            ResultSet -> IsOpen
            ResultSet -> KeysAsync()
            ResultSet -> ConsumeAsync()     -> ResultSummary
            ResultSet -> PeekAsync()
            ResultSet -> FetchAsync()

            Record -> Item
            Record -> Keys

            ResultSummary -> BINDINGS MISSING
            INode         -> BINDINGS MISSING
            IRelationship -> BINDINGS MISSING

            INode -> ElementId;
            INode -> Labels
            INode -> Properties
            INode -> Node[key]
            INode -> Equals(INode other)
            IsINode(object instance)

            IRelationship -> Id              ?ElementId?
            IRelationship -> Type
            IRelationship -> StartNodeId     ?ElementId?
            IRelationship -> EndNodeId       ?ElementId?
            IRelationship -> Properties
            IRelationship -> Relationship[key]
            IRelationship -> Equals(IRelationship other)
            IsIRelationship(object instance)

             */


            //MockModel model = MockModel.Connect(uri, AuthToken.Basic(username, password), database, config);
            //model.Execute(true);

            // Generate documentation

            string GetBase64KerberosTicket()
            {
                using (var context = new SspiContext($"host/localhost", "Negotiate"))
                {
                    var tokenBytes = context.RequestToken();

                    return Convert.ToBase64String(tokenBytes);
                }
            }

            async Task CleanDB()
            {
                DriverRecordSet result;

                await using (DriverSession session = driver.AsyncSession(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(ReadWriteMode.ReadWrite); }))
                {
                    result = await session.RunAsync("MATCH (n) DETACH DELETE n;").ConfigureAwait(false);
                    await result.ConsumeAsync().ConfigureAwait(false);

                    result = await session.RunAsync("CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *;").ConfigureAwait(false);
                    await result.ConsumeAsync().ConfigureAwait(false);
                }
            }
        }
    }
}
