using System;
using System.Collections.Generic;
using System.Text;

using Neo4j.Driver;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System.Linq;

namespace Blueprint41.Neo4j.Persistence.Driver.v5
{
    internal class Neo4jRawNode : RawNode
    {
        internal Neo4jRawNode(INode node)
        {
            Node = node;
        }
        private INode Node;

        public override long Id => Node.Id;
        public override IReadOnlyList<string> Labels => Node.Labels;
        public override IReadOnlyDictionary<string, object?> Properties => Node.Properties;
        public override object? this[string key] => Node[key];
        public override bool Equals(RawNode other) => other is Neo4jRawNode rawOther ? Node.Equals(rawOther.Node) : false;
    }
}
