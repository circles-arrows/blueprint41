using System;
using System.Collections.Generic;
using System.Linq;
using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.Refactoring.Schema
{
    public class IndexInfo_Neo4jV5 : IndexInfo
    {
        internal IndexInfo_Neo4jV5(IDictionary<string, object> record, PersistenceProvider persistenceProvider) : base(record, persistenceProvider) 
        {
        }

        protected override void Initialize(IDictionary<string, object> record)
        {
            Name = record["name"].As<string>();
            EntityType = record["entityType"].As<string>();
            State = record["state"].As<string>();
            Type = record["type"].As<string>();
            OwningConstraint = record["type"].As<string>();

            IsIndexed = true;
            isUnique = record["owningConstraint"].As<string>() is not null;
            Entity = record["labelsOrTypes"]?.As<List<object>>()?.Cast<string>()?.ToArray()!;
            Field = record["properties"]?.As<List<object>>()?.Cast<string>()?.ToArray()!;
        }
    }
}
