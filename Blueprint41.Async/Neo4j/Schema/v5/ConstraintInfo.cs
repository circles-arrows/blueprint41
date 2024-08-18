using System.Collections.Generic;
using System.Linq;

using Blueprint41.Async.Core;
using Blueprint41.Async.Neo4j.Persistence.Void;

namespace Blueprint41.Async.Neo4j.Schema.v5
{
    public class ConstraintInfo_v5 : ConstraintInfo
    {
        private bool isKey = false;
        public override bool IsKey => isKey;

        internal ConstraintInfo_v5(RawRecord record, Neo4jPersistenceProvider persistenceProvider) : base(record, persistenceProvider) { }

        protected override void Initialize(RawRecord record)
        {
            Name = record.Values["name"].As<string>();
            EntityType = record.Values["entityType"].As<string>();
            IsUnique = false;
            IsMandatory = false;

            string type = record.Values["type"]?.As<string>()?.ToLowerInvariant() ?? "";

            if (type.Contains("node_key"))
                isKey = true;

            if (type.Contains("uniqueness"))
                IsUnique = true;

            if (type.Contains("existence"))
                IsMandatory = true;

            Entity = record.Values["labelsOrTypes"]?.As<List<object>>()?.Cast<string>()?.ToArray()!;
            Field = record.Values["properties"]?.As<List<object>>()?.Cast<string>()?.ToArray()!;
        }
    }
}
