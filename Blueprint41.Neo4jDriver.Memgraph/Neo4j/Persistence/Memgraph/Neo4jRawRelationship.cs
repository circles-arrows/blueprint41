using System;
using System.Collections.Generic;
using System.Text;

using Neo4j.Driver;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System.Linq;

namespace Blueprint41.Neo4j.Persistence.Driver.Memgraph
{
    internal class Neo4jRawRelationship : RawRelationship
    {
        internal Neo4jRawRelationship(IRelationship relationship)
        {
            Relationship = relationship;
        }
        private readonly IRelationship Relationship;

#pragma warning disable CS0618 // Type or member is obsolete
        public override long Id => Relationship.Id;
        public override string Type => Relationship.Type;
        public override long StartNodeId => Relationship.StartNodeId;
        public override long EndNodeId => Relationship.EndNodeId;
#pragma warning restore CS0618 // Type or member is obsolete
        public override IReadOnlyDictionary<string, object> Properties => Relationship.Properties;
        public override object this[string key] => Relationship[key];
        public override bool Equals(RawRelationship other) => other is Neo4jRawRelationship rawOther && Relationship.Equals(rawOther.Relationship);
    }
}
