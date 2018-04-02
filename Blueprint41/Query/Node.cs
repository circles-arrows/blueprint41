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
        }
        protected internal Node(RELATIONSHIP fromRelationship, DirectionEnum direction, string label)
            : this()
        {
            FromRelationship = fromRelationship;
            Direction = direction;
            FromRelationship.ToNode = this;
            Neo4jLabel = label ?? GetNeo4jLabel();
        }

        public RELATIONSHIP FromRelationship { get; set; }
        public RELATIONSHIP ToRelationship { get; set; }
        public DirectionEnum Direction { get; set; }

        protected abstract string GetNeo4jLabel();
        public string Neo4jLabel { get; private set; }
        public AliasResult NodeAlias { get; protected set; }
        public bool IsReference { get; protected set; }

        internal void Compile(CompileState state)
        {
            //find the root
            Node root = this;
            while (root.FromRelationship != null)
                root = root.FromRelationship.FromNode;

            Node current = root;
            do
            {
                GetDirection(current, state.Text);
                if ((object)current.NodeAlias != null)
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
                    GetDirection(current.ToRelationship, state.Text);
                    if ((object)current.ToRelationship.RelationshipAlias != null)
                    {
                        current.ToRelationship.RelationshipAlias.AliasName = string.Format("r{0}", state.patternSeq++);
                        state.Text.AppendFormat("[{0}:{1}]", current.ToRelationship.RelationshipAlias.AliasName, current.ToRelationship.NEO4J_TYPE);
                    }
                    else
                        state.Text.AppendFormat("[:{0}]", current.ToRelationship.NEO4J_TYPE);

                    current = current.ToRelationship.ToNode;
                }
                else
                    break;
                
            } while (true);
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
