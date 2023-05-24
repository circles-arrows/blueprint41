using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;

namespace Blueprint41.Neo4j.Schema.v5
{
    public class ConstraintInfo_v5 : ConstraintInfo
    {
        internal ConstraintInfo_v5(RawRecord record) : base(record) { }
        protected override void Initialize(RawRecord record)
        {
            Name = record.Values["name"].As<string>();
            IsUnique = false;
            IsMandatory = false;

            string type = record.Values["type"]?.As<string>()?.ToLowerInvariant() ?? "";
            if (type.Contains("uniqueness"))
                IsUnique = true;

            if (type.Contains("existence"))
                IsMandatory = true;

            Entity = record.Values["labelsOrTypes"]?.As<List<object>>()?.Cast<string>()?.ToArray()!;
            Field = record.Values["properties"]?.As<List<object>>()?.Cast<string>()?.ToArray()!;
        }
    }
}