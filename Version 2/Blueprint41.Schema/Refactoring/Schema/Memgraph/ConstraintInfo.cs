using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;

namespace Blueprint41.Refactoring.Schema.Memgraph
{
    public class ConstraintInfo_Memgraph : ConstraintInfo
    {
        public override bool IsKey { get; }

        internal ConstraintInfo_Memgraph(RawRecord record, PersistenceProvider persistenceProvider) : base(record, persistenceProvider) { }

        protected override void Initialize(RawRecord record)
        {
            //Name = record.Values["name"].As<string>();
            IsUnique = false;
            IsMandatory = false;
            string constraintType = record.Values["constraint type"]?.As<string>()?.ToLowerInvariant() ?? "";
            if (constraintType.Contains("unique") || constraintType.Contains("key"))
                IsUnique = true;

            if (constraintType.Contains("exists") || constraintType.Contains("key"))
                IsMandatory = true;

            Entity = new List<string>() { record.Values["label"].ToString()! };
            Field = IsMandatory ? new List<string>() { record.Values["properties"].ToString()! } : record.Values["properties"]?.As<List<object>>()?.Cast<string>()?.ToList()!;
        }
    }
}