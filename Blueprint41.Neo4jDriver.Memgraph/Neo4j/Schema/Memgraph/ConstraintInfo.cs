using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;

namespace Blueprint41.Neo4j.Schema.Memgraph
{
    public class ConstraintInfo_Memgraph : ConstraintInfo
    {
        private bool isKey = false;
        public override bool IsKey => isKey;

        internal ConstraintInfo_Memgraph(RawRecord record) : base(record) { }

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