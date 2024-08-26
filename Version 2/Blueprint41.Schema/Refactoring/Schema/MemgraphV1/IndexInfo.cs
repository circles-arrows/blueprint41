using System;
using System.Collections.Generic;

using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41.Refactoring.Schema
{
    public class IndexInfo_MemgraphV1 : IndexInfo
    {
        internal IndexInfo_MemgraphV1(IDictionary<string, object> record, PersistenceProvider neo4JPersistenceProvider) : base(record, neo4JPersistenceProvider) { }

        protected override void Initialize(IDictionary<string, object> record)
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

            string? field = record["property"]?.ToString();
            Field = !string.IsNullOrEmpty(field) ? new List<string>() { field! } : default!;
        }
    }
}
