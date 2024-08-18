using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Async.Query
{
    public abstract class RELATIONSHIP
    {
        private Repeat? repeat = null;
        public Node FromNode { get; set; }
        public Node? ToNode { get; set; }
        

        public DirectionEnum Direction { get; set; }

        protected void Repeat(int minHops, int maxHops)
        {
            if (repeat is null)
                repeat = new Repeat();

            repeat.MinHops = minHops;
            repeat.MaxHops = maxHops;
        }

        public abstract AliasResult? RelationshipAlias { get; protected set; }

        protected internal RELATIONSHIP(Node fromNode, DirectionEnum direction)
        {
            FromNode = fromNode;
            Direction = direction;
            FromNode.ToRelationship = this;
        }

        public abstract string NEO4J_TYPE { get; }

        internal void Compile(CompileState state)
        {
            string repeatPattern = (repeat is null) ? string.Empty : $"*{repeat.MinHops}..{repeat.MaxHops}";

            GetDirection(this, state.Text);
            if (RelationshipAlias is not null)
            {
                RelationshipAlias.AliasName = $"r{state.patternSeq++}";
                state.Text.Append($"[{RelationshipAlias.AliasName}:{NEO4J_TYPE}{repeatPattern}]");
            }
            else
            {
                state.Text.Append($"[:{NEO4J_TYPE}{repeatPattern}]");
            }
        }

        private void GetDirection(RELATIONSHIP relationship, StringBuilder sb)
        {
            switch (relationship.Direction)
            {
                case DirectionEnum.In:
                    sb.Append("-");
                    break;
                case DirectionEnum.Out:
                    sb.Append("<-");
                    break;
                case DirectionEnum.None:
                    sb.Append("-");
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }

    internal class Repeat
    {
        public int MinHops { get; set; }
        public int MaxHops { get; set; }
    }
}
