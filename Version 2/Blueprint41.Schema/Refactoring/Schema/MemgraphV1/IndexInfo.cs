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

            Entity = ToStringList(record["label"]);

            Field = ToStringList(record["property"]);

            static List<string> ToStringList(object? value)
            {
                if (value is null)
                    return null!;

                if (value is List<object> list)
                    return list.Select(item => item?.ToString()!).ToList();

                if (value is string)
                    return new List<string>() { (string)value };

                return new List<string>() { value.ToString()! };
            }
        }
    }
}
