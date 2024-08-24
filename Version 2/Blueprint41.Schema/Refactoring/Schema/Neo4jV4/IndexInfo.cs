using System;
using System.Collections.Generic;
using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.Refactoring.Schema
{
    public class IndexInfo_Neo4jV4 : IndexInfo
    {
        internal IndexInfo_Neo4jV4(IDictionary<string, object> record, PersistenceProvider persistenceProvider) : base(record, persistenceProvider) 
        {
        }

        protected override void Initialize(IDictionary<string, object> record)
        {
            EntityType = "NODE";
            Name = record["name"].As<string>();
            State = record["state"].As<string>();
            Type = record["type"].As<string>();
            IsIndexed = true;
            isUnique = record["uniqueness"].As<string>().ToLowerInvariant() == "unique";
            Entity = record["labelsOrTypes"].As<List<string>>().ToArray();
            Field = record["properties"].As<List<string>>().ToArray();
        }
    }
}
