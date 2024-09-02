using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

using Kerberos.NET.Win32;

using Blueprint41.Driver;

using DataStore;
using System.Linq;

namespace Laboratory
{
    internal class Program
    {
        private const int MAX_CONNECTION_POOL_SIZE = 400;
        private const int DEFAULT_READWRITESIZE = 65536;
        private const int DEFAULT_READWRITESIZE_MAX = 655360;

        static async Task Main(string[] args)
        {
            Driver.Configure<Neo4j.Driver.IDriver>();

            string version = Driver.NugetVersion;

            // Prove new 
            ServerAddressResolver resolver = new ServerAddressResolver(null, null);
            Neo4j.Driver.IServerAddressResolver neo4jResolver = (Neo4j.Driver.IServerAddressResolver)Driver.I_SERVER_ADDRESS_RESOLVER.ConvertToNeo4jIServerAddressResolver(resolver);

            ILogger logger = new InMemoryLogger(true, true, 100);
            Neo4j.Driver.ILogger neo4jLogger = (Neo4j.Driver.ILogger)Driver.I_LOGGER.ConvertToNeo4jILogger(logger);

            //await TestDriver().ConfigureAwait(false);
        }

        private static async Task TestDriver()
        {
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!;
#pragma warning disable S6966
            string[] queries = File.ReadAllText(Path.Combine(path, "movies.cypher")).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
#pragma warning restore S6966

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

                foreach (Record record in await result.ToListAsync())
                {
                    string movie = record["Movie"].As<string>();
                    string actor = record["Actor"].As<string>();

                    Console.WriteLine($"Movie: '{movie}', Actor: '{actor}'.");
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

                    foreach (object record in await result.ToListAsyncInternal())
                    {
                        string movie = Record.ItemInternal(record, "Movie").As("NULL");
                        string actor = Record.ItemInternal(record, "Actor").As("NULL");

                        Console.WriteLine($"Movie: '{movie}', Actor: '{actor}'.");
                    }

                    await transaction.RollbackAsync();
                }
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey(true);

            #endregion



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

    public class InMemoryLogger : ILogger
    {
        public InMemoryLogger(bool debug, bool trace, int maxEntries = 1000)
        {
            _debug = debug;
            _trace = trace;
            _maxEntries = maxEntries;
        }

        public void Debug(string message, params object[] args)
        {
            if (_debug)
                Add(new LogEntry(LogEntryType.Debug, message, args));
        }
        public void Error(Exception cause, string message, params object[] args)
        {
            Add(new LogEntry(LogEntryType.Error, message, args, cause));
        }
        public void Info(string message, params object[] args)
        {
            Add(new LogEntry(LogEntryType.Info, message, args));
        }
        public void Trace(string message, params object[] args)
        {
            if (_trace)
                Add(new LogEntry(LogEntryType.Trace, message, args));
        }
        public void Warn(Exception cause, string message, params object[] args)
        {
            Add(new LogEntry(LogEntryType.Warn, message, args, cause));
        }

        public bool IsDebugEnabled() => _debug;
        private readonly bool _debug;

        public bool IsTraceEnabled() => _trace;
        private readonly bool _trace;

        public IReadOnlyList<LogEntry> GetLog()
        {
            lock (_log)
            {
                return _log.ToList();
            }
        }
        private void Add(LogEntry entry)
        {
            lock (_log)
            {
                _log.AddLast(entry);
                while (_log.Count > _maxEntries)
                    _log.RemoveFirst();
            }
        }
        private readonly LinkedList<LogEntry> _log = new LinkedList<LogEntry>();
        private readonly int _maxEntries;
    }
    public class LogEntry
    {
        internal LogEntry(LogEntryType type, string message, object[] arguments, Exception? cause = null)
        {
            Type = type;
            Message = message;
            Arguments = arguments;
            Cause = cause;
        }

        public LogEntryType Type { get; private set; }
        public string Message { get; private set; }
        public object[] Arguments { get; private set; }
        public Exception? Cause { get; private set; }
    }
    public enum LogEntryType
    {
        Debug,
        Error,
        Info,
        Trace,
        Warn,
    }
}