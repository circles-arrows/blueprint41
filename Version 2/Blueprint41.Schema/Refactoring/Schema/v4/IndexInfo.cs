using System;
using System.Collections.Generic;

using Blueprint41.Core;

namespace Blueprint41.Refactoring.Schema.v4
{
    public class IndexInfo_v4: IndexInfo
    {
        internal IndexInfo_v4(RawRecord record, PersistenceProvider persistenceProvider) : base(record, persistenceProvider) 
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
