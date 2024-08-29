using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

using Kerberos.NET.Win32;

using Blueprint41.Driver;

using DataStore;

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

            Bookmarks bookmark;

            await CleanDB().ConfigureAwait(false);

            await using (Session session = driver.AsyncSession(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Write); }))
            {
                foreach (string query in queries)
                {
                    Console.WriteLine(query);

                    ResultCursor result = await session.RunAsync(query).ConfigureAwait(false);
                    await result.ConsumeAsync().ConfigureAwait(false);
                }
                bookmark = session.LastBookmarks;
            }

            Console.Clear();

            await using (Session session = driver.AsyncSession(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Read); config.WithBookmarks(bookmark); }))
            {
                Dictionary<string, object?> parameters = new Dictionary<string, object?>()
                {
                    { "actor", "Keanu Reeves" },
                };
                ResultCursor result = await session.RunAsync("""
                    MATCH (m:Movie)<-[:ACTED_IN]-(p:Person)
                    WHERE p.name = $actor
                    RETURN m.title AS Movie, p.name AS Actor
                    """,
                    parameters).ConfigureAwait(false);

                bool fetch = await result.FetchAsync().ConfigureAwait(false);
                while (fetch)
                {
                    IReadOnlyDictionary<string, object?> values = Record.ValuesInternal(result.Current);

                    Console.WriteLine($"Movie: '{values["Movie"]}', Actor: '{values["Actor"]}'.");

                    fetch = await result.FetchAsync().ConfigureAwait(false);
                }
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey(true);

            #endregion

            #region Test Running a Query on the Transaction
            
            await CleanDB().ConfigureAwait(false);

            await using (Session session = driver.AsyncSession(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Write); }))
            {
                foreach (string query in queries)
                {
                    Console.WriteLine(query);
                    await using (Transaction transaction = await session.BeginTransactionAsync(config => { config.WithTimeout(TimeSpan.FromSeconds(30)); }).ConfigureAwait(false))
                    {

                        ResultCursor result = await transaction.RunAsync(query).ConfigureAwait(false);
                        ResultSummary resultSummary = await result.ConsumeAsync().ConfigureAwait(false);
                        System.Diagnostics.Debug.WriteLine(resultSummary.Query.Text);

                        await transaction.CommitAsync();
                    }
                }
                bookmark = session.LastBookmarks;

                Console.Clear();

                await using (Transaction transaction = await session.BeginTransactionAsync(config => { config.WithTimeout(TimeSpan.FromSeconds(30)); config.WithMetadata(new Dictionary<string, object>() { { "Hello", "World" } }); }).ConfigureAwait(false))
                {
                    Dictionary<string, object?> parameters = new Dictionary<string, object?>()
                    {
                        { "actor", "Keanu Reeves" },
                    };
                    ResultCursor result = await transaction.RunAsync("""
                    MATCH (m:Movie)<-[:ACTED_IN]-(p:Person)
                    WHERE p.name = $actor
                    RETURN m.title AS Movie, p.name AS Actor
                    """,
                        parameters).ConfigureAwait(false);

                    bool fetch = await result.FetchAsync().ConfigureAwait(false);
                    while (fetch)
                    {
                        IReadOnlyDictionary<string, object?> values = Record.ValuesInternal(result.Current);

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

            ResultCursor -> 
            ResultCursor -> Summary
            ResultCursor -> IsOpen
            ResultCursor -> KeysAsync()
            ResultCursor -> ConsumeAsync()
            ResultCursor -> PeekAsync()
            ResultCursor -> FetchAsync()

            Record -> Item
            Record -> Keys

            ResultSummary -> Query
            ResultSummary -> Counters
            ResultSummary -> Notifications

            Query -> Text
            Query -> Parameters

            Counters -> ContainsUpdates
            Counters -> NodesCreated
            Counters -> NodesDeleted
            Counters -> RelationshipsCreated
            Counters -> RelationshipsDeleted
            Counters -> PropertiesSet
            Counters -> LabelsAdded
            Counters -> LabelsRemoved
            Counters -> IndexesAdded
            Counters -> IndexesRemoved
            Counters -> ConstraintsAdded
            Counters -> ConstraintsRemoved
            Counters -> SystemUpdates
            Counters -> ContainsSystemUpdates

            Notification -> Code
            Notification -> Title
            Notification -> Description
            Notification -> Offset
            Notification -> Line
            Notification -> Column



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
                ResultCursor result;

                await using (Session session = driver.AsyncSession(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Write); }))
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
