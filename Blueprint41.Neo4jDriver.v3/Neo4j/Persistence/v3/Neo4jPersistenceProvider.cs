using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Neo4j.Driver.V1;

using Blueprint41.Core;
using Blueprint41.Log;

namespace Blueprint41.Neo4j.Persistence.Driver.v3
{
    public partial class Neo4jPersistenceProvider : Void.Neo4jPersistenceProvider
    {
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
                            driver = GraphDatabase.Driver(Uri, AuthTokens.Basic(Username, Password));
                    }
                }
                return driver;
            }
        }

        private Neo4jPersistenceProvider() : this(null, null, null, false) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, bool withLogging = false) : base(uri, username, password, null, withLogging)
        {
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawNode), from => from is INode item ? new Neo4jRawNode(item) : null);
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawRelationship), from => from is IRelationship item ? new Neo4jRawRelationship(item) : null);
        }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, Action<string> logger) : base(uri, username, password, null, logger)
        {
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawNode), from => from is INode item ? new Neo4jRawNode(item) : null);
            Core.ExtensionMethods.RegisterAsConversion(typeof(Neo4jPersistenceProvider), typeof(RawRelationship), from => from is IRelationship item ? new Neo4jRawRelationship(item) : null);
        }

        public override Transaction NewTransaction(bool withTransaction)
        {
            return new Neo4jTransaction(Driver, withTransaction, TransactionLogger);
        }
    }
}
