using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Query;

namespace Blueprint41.Query
{
    public abstract class Node
    {
        protected Node()
        {
            NodeAlias = null;
            IsReference = false;
            Neo4jLabel = GetNeo4jLabel();
            Entity = GetEntity();
        }
        protected internal Node(RELATIONSHIP fromRelationship, DirectionEnum direction, string label, Entity entity)
            : this()
        {
            FromRelationship = fromRelationship;
            Direction = direction;
            FromRelationship.ToNode = this;
            Neo4jLabel = label ?? GetNeo4jLabel();
            Entity = entity ?? GetEntity();
        }

        public RELATIONSHIP? FromRelationship { get; set; }
        public RELATIONSHIP? ToRelationship { get; set; }
        public DirectionEnum Direction { get; set; }

        protected abstract string GetNeo4jLabel();
        protected abstract Entity GetEntity();
        public string Neo4jLabel { get; private set; }
        public AliasResult? NodeAlias { get; protected set; }
        public bool IsReference { get; protected set; }
        public Entity Entity { get; private set; }

        internal void Compile(CompileState state)
        {
            //find the root
            Node root = this;
            while (root.FromRelationship != null)
                root = root.FromRelationship.FromNode;

            Node? current = root;
            do
            {
                GetDirection(current, state.Text);
                if (!(current.NodeAlias is null))
                {
                    if (current.NodeAlias.AliasName == null)
                        current.NodeAlias.AliasName = string.Format("n{0}", state.patternSeq++);
                    if (current.IsReference || current.Neo4jLabel == null)
                        state.Text.AppendFormat("({0})", current.NodeAlias.AliasName);
                    else
                        state.Text.AppendFormat("({0}:{1})", current.NodeAlias.AliasName, current.Neo4jLabel);
                }
                else
                {
                    if (current.Neo4jLabel == null)
                        state.Text.AppendFormat("()");
                    else
                        state.Text.AppendFormat("(:{0})", current.Neo4jLabel);
                }

                if (current.ToRelationship != null)
                {
                    current.ToRelationship.Compile(state);
                    current = current.ToRelationship.ToNode;
                    if (current is null)
                        break;
                }
                else
                    break;
                
            } while (true);
        }

        private void GetDirection(Node node, StringBuilder sb)
        {
            if (node.FromRelationship == null)
                return;

            switch (node.Direction)
            {
                case DirectionEnum.In:
                    sb.Append("-");
                    break;
                case DirectionEnum.Out:
                    sb.Append("->");
                    break;
                case DirectionEnum.None:
                    sb.Append("-");
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
