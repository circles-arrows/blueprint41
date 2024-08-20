﻿using System;
using System.Collections.Generic;

using Blueprint41.Core;

namespace Blueprint41.Refactoring.Schema.Memgraph
{
    public class IndexInfo_Memgraph : IndexInfo
    {
        internal IndexInfo_Memgraph(RawRecord record, PersistenceProvider neo4JPersistenceProvider) : base(record, neo4JPersistenceProvider) { }

        protected override void Initialize(RawRecord record)
        {
            //Name = record.Values["name"].As<string>();
            //State = record.Values["state"].As<string>();
            string indexType = record.Values["index type"].As<string>();
            Type = indexType.Contains("label") ? "RANGE" : "LOOKUP";
            //OwningConstraint = record.Values["type"].As<string>();

            IsIndexed = true;
            //isUnique = record.Values["owningConstraint"].As<string>() is not null;
            string? label = record.Values["label"]?.ToString();
            Entity = !string.IsNullOrEmpty(label) ? new List<string>() { label! } : default!;

            string? field = record.Values["property"]?.ToString();
            Field = !string.IsNullOrEmpty(field) ? new List<string>() { field! } : default!;
        }
    }
}
