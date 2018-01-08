using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public abstract class RELATIONSHIP
    {
        public Node FromNode { get; set; }
        public Node ToNode { get; set; }
        public DirectionEnum Direction { get; set; }

        public abstract AliasResult RelationshipAlias { get; protected set; }

        protected internal RELATIONSHIP(Node fromNode, DirectionEnum direction)
        {
            FromNode = fromNode;
            Direction = direction;
            FromNode.ToRelationship = this;
        }

        public abstract string NEO4J_TYPE { get; }
    }
}
