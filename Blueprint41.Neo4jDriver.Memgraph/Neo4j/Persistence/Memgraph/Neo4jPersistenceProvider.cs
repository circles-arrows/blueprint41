using System;
using System.Collections.Generic;
using System.Linq;

using Neo4j.Driver;

using Blueprint41.Core;
using Blueprint41.Neo4j.Model;

namespace Blueprint41.Neo4j.Persistence.Driver.Memgraph
{
    public partial class Neo4jPersistenceProvider : Void.Neo4jPersistenceProvider
    {
        private const int MAX_CONNECTION_POOL_SIZE = 400;
        private const int DEFAULT_READWRITESIZE = 65536;
        private const int DEFAULT_READWRITESIZE_MAX = 655360;
        private IDriver? driver = null;
        public IDriver Driver
        {
            get
            {
                if (driver is null)
                {
                    lock (_lockObject)
                    {
                        driver ??= GraphDatabase.Driver(Uri, AuthTokens.Basic(Username, Password),
                                o =>
                                {
                                    o.WithFetchSize(Config.Infinite);
                                    o.WithMaxConnectionPoolSize(MAX_CONNECTION_POOL_SIZE);
                                    o.WithDefaultReadBufferSize(DEFAULT_READWRITESIZE);
                                    o.WithDefaultWriteBufferSize(DEFAULT_READWRITESIZE);
                                    o.WithMaxReadBufferSize(DEFAULT_READWRITESIZE_MAX);
                                    o.WithMaxWriteBufferSize(DEFAULT_READWRITESIZE_MAX);
                                    o.WithMaxTransactionRetryTime(TimeSpan.Zero);

                                    if (AdvancedConfig?.DNSResolverHook is not null)
                                        o.WithResolver(new HostResolver(AdvancedConfig));
                                }
                            );
                    }
                }
                return driver;
            }
        }

        private class HostResolver : IServerAddressResolver
        {
            public HostResolver(AdvancedConfig config)
            {
                Config = config;
            }
            private readonly AdvancedConfig Config;

            public ISet<ServerAddress> Resolve(ServerAddress address)
            {
                return new HashSet<ServerAddress>(Config.DNSResolverHook!(new Neo4jHost() { Host = address.Host, Port = address.Port }).Select(host => ServerAddress.From(host.Host, host.Port)));
            }
        }

        private Neo4jPersistenceProvider() : this(null, null, null) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, AdvancedConfig? advancedConfig = null) : base(uri, username, password, advancedConfig)
        {
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawNode), from => from is INode item ? new Neo4jRawNode(item) : null);
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawRelationship), from => from is IRelationship item ? new Neo4jRawRelationship(item) : null);
        }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, string database, AdvancedConfig? advancedConfig = null) : base(uri, username, password, database, advancedConfig)
        {
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawNode), from => from is INode item ? new Neo4jRawNode(item) : null);
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawRelationship), from => from is IRelationship item ? new Neo4jRawRelationship(item) : null);
        }
        public override RawResult RunImplicit(string cypher)
        {
            IAsyncSession GetSession()
            {
                return Driver.AsyncSession((SessionConfigBuilder builder) => builder.WithFetchSize(Config.Infinite).WithDefaultAccessMode(AccessMode.Write));
            }

            IResultCursor results = TaskScheduler.RunBlocking(() => GetSession()!.RunAsync(cypher), cypher);

            //DebugQueryString(cypher, parameters);
            return new Neo4jRawResult(TaskScheduler, results);
        }

        internal override QueryTranslator Translator
        {
            get
            {
                if (translator is null)
                {
                    lock (_lockObject3)
                    {
                        if (translator is null)
                        {
                            if (this.GetType() == typeof(Void.Neo4jPersistenceProvider))
                                return GetVoidTranslator();

                            if (Uri is null && Username is null && Password is null)
                                return GetVoidTranslator();

                            using (Transaction.Begin())
                            {
                                RawResult? components = Transaction.RunningTransaction.Run("call dbms.components() yield name, versions, edition unwind versions as version return name, version, edition");
                                var record = components.First();

                                Version = record["version"].ValueAs<string>();
                                IsEnterpriseEdition = (string.Equals(record["edition"].ValueAs<string>(), "enterprise", StringComparison.InvariantCultureIgnoreCase));

                                string[] parts = Version.Split(new[] { '.' });
                                Major = int.Parse(parts[0]);

                                if (parts[1].IndexOf("-aura", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    parts[1] = parts[1].Replace("-aura", "");
                                    IsAura = true;
                                }

                                Minor = int.Parse(parts[1]);

                                if (parts.Length > 2)
                                    Revision = int.Parse(parts[2]);

                                functions = new HashSet<string>(Transaction.RunningTransaction.Run(GetFunctions(Major)).Select(item => item.Values["name"].ValueAs<string>()));
                                procedures = new HashSet<string>(Transaction.RunningTransaction.Run(GetProcedures(Major)).Select(item => item.Values["name"].ValueAs<string>()));
                            }

                            translator = new Persistence.Memgraph.Neo4jQueryTranslator(this);
                        }
                    }
                }
                return translator;

                static string GetFunctions(int version) => version switch
                {
                    _ => "call mg.functions() yield name return name",
                };
                static string GetProcedures(int version) => version switch
                {
                    _ => "call mg.procedures() yield name return name",
                };
            }
        }

        private QueryTranslator? translator = null;
        private QueryTranslator GetVoidTranslator()
        {
            if (voidTranslator is null)
            {
                lock (_lockObject2)
                {
                    voidTranslator ??= new Void.Neo4jQueryTranslator(this);
                }
            }

            return voidTranslator;
        }
        private static QueryTranslator? voidTranslator = null;

        public override Transaction NewTransaction(bool readWriteMode) => new Neo4jTransaction(this, readWriteMode, TransactionLogger);
        public override Bookmark FromToken(string consistencyToken) => Neo4jBookmark.FromTokenInternal(consistencyToken);
        public override string ToToken(Bookmark consistency) => Neo4jBookmark.ToTokenInternal(consistency);

        public CustomTaskScheduler TaskScheduler
        {
            get
            {
                if (taskScheduler is null)
                {
                    lock (sync)
                    {
                        if (taskScheduler is null)
                        {
                            CustomTaskQueueOptions main = new(10, 50);
                            CustomTaskQueueOptions sub = new(4, 20);
                            taskScheduler = new CustomThreadSafeTaskScheduler(main, sub);
                        }
                    }
                }

                return taskScheduler;
            }
        }
        private CustomTaskScheduler? taskScheduler = null;
        private static readonly object sync = new();
        private readonly object _lockObject = new();
        private readonly object _lockObject2 = new();
        private readonly object _lockObject3 = new();

        public Neo4jPersistenceProvider ConfigureTaskScheduler(CustomTaskQueueOptions mainQueue) => ConfigureTaskScheduler(mainQueue, CustomTaskQueueOptions.Disabled);
        public Neo4jPersistenceProvider ConfigureTaskScheduler(CustomTaskQueueOptions mainQueue, CustomTaskQueueOptions subQueue)
        {
            lock (sync)
            {
                if (taskScheduler is not null)
                    throw new InvalidOperationException("You can only configure the TaskScheduler during initialization of Blueprint41.");

                taskScheduler = new CustomThreadSafeTaskScheduler(mainQueue, subQueue);
            }

            return this;
        }
    }
}
