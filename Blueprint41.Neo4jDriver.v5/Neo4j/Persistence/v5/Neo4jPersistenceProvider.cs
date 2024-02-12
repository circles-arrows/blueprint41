using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Neo4j.Driver;

using Blueprint41.Core;
using Blueprint41.Log;
using Blueprint41.Neo4j.Model;
using Blueprint41.Neo4j.Schema;
using System.Runtime.CompilerServices;

namespace Blueprint41.Neo4j.Persistence.Driver.v5
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
                    lock (typeof(Neo4jPersistenceProvider))
                    {
                        if (driver is null)
                        {
                            driver = GraphDatabase.Driver(Uri, AuthTokens.Basic(Username, Password), 
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
        public override RawResult Run(string cypher, bool useTransactionIfAvailable = true, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Transaction? trans = Transaction.Current;

            if (useTransactionIfAvailable && trans is not null)
            {
                return trans.Run(cypher, memberName, sourceFilePath, sourceLineNumber);
            }
            else
            {
                IResultCursor results = TaskScheduler.RunBlocking(() => GetSession()!.RunAsync(cypher), cypher);

                //DebugQueryString(cypher, parameters);
                return new Neo4jRawResult(TaskScheduler, results);

                IAsyncSession GetSession()
                {
                    return Driver.AsyncSession((SessionConfigBuilder builder) => builder.WithFetchSize(Config.Infinite).WithDefaultAccessMode(AccessMode.Write));
                }
            }
        }
        public override RawResult Run(string cypher, Dictionary<string, object?>? parameters, bool useTransactionIfAvailable = true, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Transaction? trans = Transaction.Current;

            if (useTransactionIfAvailable && trans is not null)
            {
                return trans.Run(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);
            }
            else
            {
                IResultCursor results = TaskScheduler.RunBlocking(() => GetSession()!.RunAsync(cypher, parameters), cypher);

                //DebugQueryString(cypher, parameters);
                return new Neo4jRawResult(TaskScheduler, results);

                IAsyncSession GetSession()
                {
                    return Driver.AsyncSession((SessionConfigBuilder builder) => builder.WithFetchSize(Config.Infinite).WithDefaultAccessMode(AccessMode.Write));
                }
            }
        }

        public override Transaction NewTransaction(bool readWriteMode) => new Neo4jTransaction(this, readWriteMode, TransactionLogger);
        public override Bookmark FromToken(string consistencyToken)    => Neo4jBookmark.FromTokenInternal(consistencyToken);
        public override string ToToken(Bookmark consistency)           => Neo4jBookmark.ToTokenInternal(consistency);

        internal CustomTaskScheduler TaskScheduler
        {
            get
            {
                if (taskScheduler is null)
                {
                    lock (sync)
                    {
                        if (taskScheduler is null)
                        {
                            CustomTaskQueueOptions main = new CustomTaskQueueOptions(10, 50);
                            CustomTaskQueueOptions sub = new CustomTaskQueueOptions(4, 20);
                            taskScheduler = new CustomThreadSafeTaskScheduler(main, sub);
                        }
                    }
                }

                return taskScheduler;
            }
        }
        private CustomTaskScheduler? taskScheduler = null;
        private static object sync = new object();

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
