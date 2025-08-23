using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Void;

namespace Blueprint41.Neo4j.Schema.Memgraph
{
    public class IndexInfo_Memgraph : IndexInfo
    {
        internal IndexInfo_Memgraph(RawRecord record, Neo4jPersistenceProvider neo4JPersistenceProvider) : base(record, neo4JPersistenceProvider) { }

        protected override void Initialize(RawRecord record)
        {
            //Name = record.Values["name"].As<string>();
            //State = record.Values["state"].As<string>();
            string indexType = record.Values["index type"].As<string>();
            Type = indexType.Contains("label") ? "RANGE" : "LOOKUP";
            //OwningConstraint = record.Values["type"].As<string>();

            IsIndexed = true;
            //isUnique = record.Values["owningConstraint"].As<string>() is not null;
            
            Entity = ToStringList(record.Values["label"]);

            Field = ToStringList(record.Values["property"]);

            static List<string> ToStringList(object? value)
            {
                if (value is null)
                    return null!;

                if (value is List<object> list)
                    return list.Select(item => item?.ToString()!).ToList();

                if (value is string)
                    return new List<string>() { (string)value };

                return new List<string>() { value.ToString() };
            }
        }
    }
}
