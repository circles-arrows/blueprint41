using System;
using System.Collections.Generic;
using System.Linq;
using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41.Refactoring.Schema
{
    public class IndexInfo_MemgraphV1 : IndexInfo
    {
        internal IndexInfo_MemgraphV1(IReadOnlyDictionary<string, object> record, PersistenceProvider neo4JPersistenceProvider) : base(record, neo4JPersistenceProvider) { }

        protected override void Initialize(IReadOnlyDictionary<string, object> record)
        {
            //Name = record.Values["name"].As<string>();
            //State = record.Values["state"].As<string>();
            string indexType = record["index type"].As<string>();
            Type = indexType.Contains("label") ? "RANGE" : "LOOKUP";
            //OwningConstraint = record.Values["type"].As<string>();

            IsIndexed = true;
            //isUnique = record.Values["owningConstraint"].As<string>() is not null;
            string? label = record["label"]?.ToString();
            Entity = !string.IsNullOrEmpty(label) ? new List<string>() { label! } : default!;

            List<object>? field = record["property"] as List<object>;
            if (field is null)
                field = new List<object> { record["property"].As<string>() };
            if (field[0] is null)
                field = null;

            Field = field is null ? default! : field.Cast<string>().ToList();
        }
    }
}
