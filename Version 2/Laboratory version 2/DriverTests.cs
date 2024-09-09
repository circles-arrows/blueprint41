using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;

using Kerberos.NET.Win32;

using Blueprint41;
using d = Blueprint41.Driver;

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
            d.Driver.Configure<Neo4j.Driver.IDriver>();

            string[] queries = GetQueries();

            #region Test Connectivity

            d.Driver driver = d.Driver.Get(new Uri(@"bolt://localhost:7687"), AuthToken.Basic("neo4j", "neoneoneo"), delegate (d.ConfigBuilder o)
            {
                o.WithFetchSize(d.ConfigBuilder.Infinite);
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

            await using (d.DriverSession session = driver.Session(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Write); }))
            {
                foreach (string query in queries)
                {
                    Console.WriteLine(query);

                    d.ResultCursor result = await session.RunAsync(query).ConfigureAwait(false);
                    await result.ConsumeAsync().ConfigureAwait(false);
                }
                bookmark = session.LastBookmarks;
            }

            Console.Clear();

            await using (d.DriverSession session = driver.Session(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Read); config.WithBookmarks(bookmark); }))
            {
                Dictionary<string, object?> parameters = new Dictionary<string, object?>()
                {
                    { "actor", "Keanu Reeves" },
                };
                d.ResultCursor result = await session.RunAsync("""
                    MATCH (m:Movie)<-[:ACTED_IN]-(p:Person)
                    WHERE p.name = $actor
                    RETURN m.title AS Movie, p.name AS Actor
                    """,
                    parameters).ConfigureAwait(false);

                foreach (d.Record record in await result.ToListAsync())
                {
                    string movie = record["Movie"].As<string>();
                    string actor = record["Actor"].As<string>();

                    Console.WriteLine($"Movie: '{movie}', Actor: '{actor}'.");
                }
            }

            #endregion

            #region Test Running a Query on the Transaction

            await CleanDB().ConfigureAwait(false);

            await using (d.DriverSession session = driver.Session(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Write); }))
            {
                foreach (string query in queries)
                {
                    Console.WriteLine(query);
                    await using (d.DriverTransaction transaction = await session.BeginTransactionAsync(config => { config.WithTimeout(TimeSpan.FromSeconds(30)); }).ConfigureAwait(false))
                    {

                        d.ResultCursor result = await transaction.RunAsync(query).ConfigureAwait(false);
                        d.ResultSummary resultSummary = await result.ConsumeAsync().ConfigureAwait(false);
                        System.Diagnostics.Debug.WriteLine(resultSummary.Query.Text);

                        await transaction.CommitAsync();
                    }
                }
                bookmark = session.LastBookmarks;

                Console.Clear();

                await using (d.DriverTransaction transaction = await session.BeginTransactionAsync(config => { config.WithTimeout(TimeSpan.FromSeconds(30)); config.WithMetadata(new Dictionary<string, object>() { { "Hello", "World" } }); }).ConfigureAwait(false))
                {
                    Dictionary<string, object?> parameters = new Dictionary<string, object?>()
                    {
                        { "actor", "Keanu Reeves" },
                    };
                    d.ResultCursor result = await transaction.RunAsync("""
                    MATCH (m:Movie)<-[:ACTED_IN]-(p:Person)
                    WHERE p.name = $actor
                    RETURN m.title AS Movie, p.name AS Actor
                    """,
                        parameters).ConfigureAwait(false);

                    foreach (object record in await result.ToListAsyncInternal())
                    {
                        string movie = d.Record.ItemInternal(record, "Movie").As("NULL");
                        string actor = d.Record.ItemInternal(record, "Actor").As("NULL");

                        Console.WriteLine($"Movie: '{movie}', Actor: '{actor}'.");
                    }

                    await transaction.RollbackAsync();
                }
            }

            #endregion





            async Task CleanDB()
            {
                d.ResultCursor result;

                await using (d.DriverSession session = driver.Session(config => { config.WithDatabase("unittest"); config.WithDefaultAccessMode(AccessMode.Write); }))
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
            d.Driver.Configure<Neo4j.Driver.IDriver>();

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
            d.Driver.Configure<Neo4j.Driver.IDriver>();

            var bp41Logger = new InMemoryLogger(true, true, 100);
            var neo4jLogger = AsILogger(bp41Logger);

            neo4jLogger.Info("Hello World");

            var log = bp41Logger.GetLog();
            Debug.Assert(log.Count == 1);
            Debug.Assert(log[0].Message == "Hello World");

            Neo4j.Driver.ILogger AsILogger(d.ILogger resolver)
            {
                var neo4jILogger = (Neo4j.Driver.ILogger)d.Driver.I_LOGGER.ConvertToNeo4jILogger(resolver);

                Debug.Assert(neo4jILogger is not null, "Bp41 ILogger could not be converted into a Neo4j ILogger.");

                return neo4jILogger!;
            }
        }
        public static void TestServerAddressResolver()
        {
            d.Driver.Configure<Neo4j.Driver.IDriver>();

            d.ServerAddress localhost = d.ServerAddress.From("localhost", 7687);
            d.ServerAddress vcluster1 = d.ServerAddress.From("cluster1", 7687);
            d.ServerAddress vcluster2 = d.ServerAddress.From("cluster2", 7687);
            HashSet<d.ServerAddress> cluster1 = new HashSet<d.ServerAddress>()
            {
                d.ServerAddress.From("127.0.0.1", 7687),
                d.ServerAddress.From("127.0.0.2", 7687),
                d.ServerAddress.From("127.0.0.3", 7687),
            };
            HashSet<d.ServerAddress> cluster2 = new HashSet<d.ServerAddress>() 
            {
                d.ServerAddress.From("127.10.0.1", 7687),
                d.ServerAddress.From("127.10.0.2", 7687),
                d.ServerAddress.From("127.10.0.3", 7687),
            };
            var dict = new Dictionary<d.ServerAddress, ISet<d.ServerAddress>>()
            {
                { vcluster1, cluster1  },
                { vcluster2, cluster2  },
            };

            var bp41Resolver = new d.ServerAddressResolver(dict, new HashSet<d.ServerAddress>() { localhost });
            var neo4jResolver = AsIServerAddressResolver(bp41Resolver);

            ISet<d.ServerAddress>? bp41Cluster = bp41Resolver.Resolve(d.ServerAddress.From("cluster1", 7687));
            Debug.Assert(bp41Cluster?.Count == 3);

            ISet<d.ServerAddress>? bp41Unknown= bp41Resolver.Resolve(d.ServerAddress.From("unknown", 7687));
            Debug.Assert(bp41Unknown?.Count == 1);

            ISet<Neo4j.Driver.ServerAddress> neo4jCluster = neo4jResolver.Resolve(Neo4j.Driver.ServerAddress.From("cluster2", 7687));
            Debug.Assert(neo4jCluster.Count == 3);

            Neo4j.Driver.IServerAddressResolver AsIServerAddressResolver(d.ServerAddressResolver resolver)
            {
                var neo4jResolver = (Neo4j.Driver.IServerAddressResolver)d.Driver.I_SERVER_ADDRESS_RESOLVER.ConvertToNeo4jIServerAddressResolver(resolver);

                Debug.Assert(neo4jResolver is not null, "Bp41 ServerAddressResolver could not be converted into a Neo4j IServerAddressResolver.");

                return neo4jResolver!;
            }
        }
    }
}
