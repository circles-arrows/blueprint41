using System;
using System.Collections.Generic;
using System.Linq;
using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.Refactoring.Schema
{
    public class ConstraintInfo_Neo4jV5 : ConstraintInfo
    {
        private bool isKey = false;
        public override bool IsKey => isKey;

        internal ConstraintInfo_Neo4jV5(IDictionary<string, object> record, PersistenceProvider persistenceProvider) : base(record, persistenceProvider) { }

        protected override void Initialize(IDictionary<string, object> record)
        {
            Name = record["name"].As<string>();
            EntityType = record["entityType"].As<string>();
            IsUnique = false;
            IsMandatory = false;

            string type = record["type"]?.As<string>()?.ToLowerInvariant() ?? "";

            if (type.Contains("node_key"))
                isKey = true;

            if (type.Contains("uniqueness"))
                IsUnique = true;

            if (type.Contains("existence"))
                IsMandatory = true;

            Entity = record["labelsOrTypes"]?.As<List<object>>()?.Cast<string>()?.ToArray()!;
            Field = record["properties"]?.As<List<object>>()?.Cast<string>()?.ToArray()!;
        }
    }
}