using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Void;

namespace Blueprint41.Neo4j.Schema.v5
{
    public class IndexInfo_v5: IndexInfo
    {
        internal IndexInfo_v5(RawRecord record, Neo4jPersistenceProvider persistenceProvider) : base(record, persistenceProvider) 
        {
        }

        protected override void Initialize(RawRecord record)
        {
            // TODO: Fix case where constraint is for a property on a relationship
            //       https://neo4j.com/docs/cypher-manual/current/constraints/
            Name = record.Values["name"].As<string>();
            State = record.Values["state"].As<string>();
            Type = record.Values["type"].As<string>();
            OwningConstraint = record.Values["type"].As<string>();

            IsIndexed = true;
            isUnique = record.Values["owningConstraint"].As<string>() is not null;
            Entity = record.Values["labelsOrTypes"]?.As<List<object>>()?.Cast<string>()?.ToArray()!;
            Field = record.Values["properties"]?.As<List<object>>()?.Cast<string>()?.ToArray()!;
        }
    }
}
