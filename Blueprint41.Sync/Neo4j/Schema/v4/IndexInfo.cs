using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blueprint41.Sync.Core;
using Blueprint41.Sync.Neo4j.Persistence.Void;

namespace Blueprint41.Sync.Neo4j.Schema.v4
{
    public class IndexInfo_v4: IndexInfo
    {
        internal IndexInfo_v4(RawRecord record, Neo4jPersistenceProvider persistenceProvider) : base(record, persistenceProvider) 
        {
        }

        protected override void Initialize(RawRecord record)
        {
            EntityType = "NODE";
            Name = record.Values["name"].As<string>();
            State = record.Values["state"].As<string>();
            Type = record.Values["type"].As<string>();
            IsIndexed = true;
            isUnique = record.Values["uniqueness"].As<string>().ToLowerInvariant() == "unique";
            Entity = record.Values["labelsOrTypes"].As<List<string>>().ToArray();
            Field = record.Values["properties"].As<List<string>>().ToArray();
        }
    }
}
