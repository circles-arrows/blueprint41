using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;

using Kerberos.NET.Win32;

using Blueprint41.Driver;

namespace Laboratory
{
    internal class DriverTests
    {
        private const int MAX_CONNECTION_POOL_SIZE = 400;
        private const int DEFAULT_READWRITESIZE = 65536;
        private const int DEFAULT_READWRITESIZE_MAX = 655360;


        public static async Task TestAll()
        {
            TestAuthTokens();

            TestILogger();
            TestServerAddressResolver();

            await TestConnectivity().ConfigureAwait(false);
        }

        public static async Task TestConnectivity()
        {
            Driver.Configure<Neo4j.Driver.IDriver>();

            string[] queries = GetQueries();

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

            await using (Session session = driver.Session(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Write); }))
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

            await using (Session session = driver.Session(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Read); config.WithBookmarks(bookmark); }))
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

                foreach (Record record in await result.ToListAsync())
                {
                    string movie = record["Movie"].As<string>();
                    string actor = record["Actor"].As<string>();

                    Console.WriteLine($"Movie: '{movie}', Actor: '{actor}'.");
                }
            }

            #endregion

            #region Test Running a Query on the Transaction

            await CleanDB().ConfigureAwait(false);

            await using (Session session = driver.Session(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Write); }))
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

                    foreach (object record in await result.ToListAsyncInternal())
                    {
                        string movie = Record.ItemInternal(record, "Movie").As("NULL");
                        string actor = Record.ItemInternal(record, "Actor").As("NULL");

                        Console.WriteLine($"Movie: '{movie}', Actor: '{actor}'.");
                    }

                    await transaction.RollbackAsync();
                }
            }

            #endregion





            async Task CleanDB()
            {
                ResultCursor result;

                await using (Session session = driver.Session(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Write); }))
                {
                    result = await session.RunAsync("MATCH (n) DETACH DELETE n;").ConfigureAwait(false);
                    await result.ConsumeAsync().ConfigureAwait(false);

                    result = await session.RunAsync("CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *;").ConfigureAwait(false);
                    await result.ConsumeAsync().ConfigureAwait(false);
                }
            }
            string[] GetQueries()
            {
                string path = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!;
#pragma warning disable S6966
                return File.ReadAllText(Path.Combine(path, "movies.cypher")).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
#pragma warning restore S6966
            }
        }

        public static void TestAuthTokens()
        {
            Driver.Configure<Neo4j.Driver.IDriver>();

            TestToken(AuthToken.None);
            TestToken(AuthToken.Basic("neo4j", "neoneoneo"));
            TestToken(AuthToken.Basic("neo4j", "neoneoneo", "realm"));
            TestToken(AuthToken.Kerberos(GetBase64KerberosTicket()));
            TestToken(AuthToken.Custom("principal", "credentials", "realm", "scheme"));
            TestToken(AuthToken.Custom("principal", "credentials", "realm", "scheme", new Dictionary<string, object>()));
            TestToken(AuthToken.Bearer("SlAV32hkKG"));

            void TestToken(AuthToken token) => Debug.Assert((token._instance as Neo4j.Driver.IAuthToken) is not null, "Token has no Neo4j backing token.");
            string GetBase64KerberosTicket()
            {
                using (var context = new SspiContext($"host/localhost", "Negotiate"))
                {
                    var tokenBytes = context.RequestToken();

                    return Convert.ToBase64String(tokenBytes);
                }
            }
        }

        public static void TestILogger()
        {
            Driver.Configure<Neo4j.Driver.IDriver>();

            var bp41Logger = new InMemoryLogger(true, true, 100);
            var neo4jLogger = AsILogger(bp41Logger);

            neo4jLogger.Info("Hello World");

            var log = bp41Logger.GetLog();
            Debug.Assert(log.Count == 1);
            Debug.Assert(log[0].Message == "Hello World");

            Neo4j.Driver.ILogger AsILogger(ILogger resolver)
            {
                var neo4jILogger = (Neo4j.Driver.ILogger)Driver.I_LOGGER.ConvertToNeo4jILogger(resolver);

                Debug.Assert(neo4jILogger is not null, "Bp41 ILogger could not be converted into a Neo4j ILogger.");

                return neo4jILogger!;
            }
        }
        public static void TestServerAddressResolver()
        {
            Driver.Configure<Neo4j.Driver.IDriver>();

            ServerAddress localhost = ServerAddress.From("localhost", 7687);
            ServerAddress vcluster1 = ServerAddress.From("cluster1", 7687);
            ServerAddress vcluster2 = ServerAddress.From("cluster2", 7687);
            HashSet<ServerAddress> cluster1 = new HashSet<ServerAddress>()
            {
                ServerAddress.From("127.0.0.1", 7687),
                ServerAddress.From("127.0.0.2", 7687),
                ServerAddress.From("127.0.0.3", 7687),
            };
            HashSet<ServerAddress> cluster2 = new HashSet<ServerAddress>() 
            {
                ServerAddress.From("127.10.0.1", 7687),
                ServerAddress.From("127.10.0.2", 7687),
                ServerAddress.From("127.10.0.3", 7687),
            };
            var dict = new Dictionary<ServerAddress, ISet<ServerAddress>>()
            {
                { vcluster1, cluster1  },
                { vcluster2, cluster2  },
            };

            var bp41Resolver = new ServerAddressResolver(dict, new HashSet<ServerAddress>() { localhost });
            var neo4jResolver = AsIServerAddressResolver(bp41Resolver);

            ISet<ServerAddress> bp41Cluster = bp41Resolver.Resolve(ServerAddress.From("cluster1", 7687));
            Debug.Assert(bp41Cluster.Count == 3);

            ISet<ServerAddress> bp41Unknown= bp41Resolver.Resolve(ServerAddress.From("unknown", 7687));
            Debug.Assert(bp41Unknown.Count == 1);

            ISet<Neo4j.Driver.ServerAddress> neo4jCluster = neo4jResolver.Resolve(Neo4j.Driver.ServerAddress.From("cluster2", 7687));
            Debug.Assert(neo4jCluster.Count == 3);

            Neo4j.Driver.IServerAddressResolver AsIServerAddressResolver(ServerAddressResolver resolver)
            {
                var neo4jResolver = (Neo4j.Driver.IServerAddressResolver)Driver.I_SERVER_ADDRESS_RESOLVER.ConvertToNeo4jIServerAddressResolver(resolver);

                Debug.Assert(neo4jResolver is not null, "Bp41 ServerAddressResolver could not be converted into a Neo4j IServerAddressResolver.");

                return neo4jResolver!;
            }
        }
    }
}
