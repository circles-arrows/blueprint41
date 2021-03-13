using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blueprint41.Core;

namespace Blueprint41.Neo4j.Schema.v4
{
    public class IndexInfo_v4: IndexInfo
    {
        internal IndexInfo_v4(RawRecord record) : base(record) { }

        protected override void Initialize(RawRecord record)
        {
            Description = record.Values["name"].As<string>();
            State = record.Values["state"].As<string>();
            Type = record.Values["type"].As<string>();
            IsIndexed = true;
            isUnique = record.Values["uniqueness"].As<string>().ToLowerInvariant() == "unique";
            Entity = record.Values["labelsOrTypes"].As<List<string>>().FirstOrDefault();
            Field = record.Values["properties"].As<List<string>>().FirstOrDefault();
        }
    }
}
