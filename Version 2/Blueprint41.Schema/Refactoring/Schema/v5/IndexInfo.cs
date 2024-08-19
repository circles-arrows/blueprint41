using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blueprint41.Core;

namespace Blueprint41.Refactoring.Schema.v5
{
    public class IndexInfo_v5: IndexInfo
    {
        internal IndexInfo_v5(RawRecord record, PersistenceProvider persistenceProvider) : base(record, persistenceProvider) 
        {
        }

        protected override void Initialize(RawRecord record)
        {
            Name = record.Values["name"].As<string>();
            EntityType = record.Values["entityType"].As<string>();
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
