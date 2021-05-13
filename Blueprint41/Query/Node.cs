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
            InlineConditions = null;
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
        public QueryCondition[]? InlineConditions { get; protected set; }
        public Assignment[]? InlineAssignments { get; protected set; }
        public bool IsReference { get; protected set; }
        public Entity Entity { get; private set; }

        public PathNode Path(out PathResult alias)
        {
            AliasResult aliasResult = new AliasResult()
            {
                Node = this,
            };
            alias = new PathResult(aliasResult, null);
            return new PathNode(this, aliasResult);
        }
        public PathNode Path(out PathResult alias, string name)
        {
            AliasResult aliasResult = new AliasResult()
            {
                AliasName = name,
                Node = this,
            };
            alias = new PathResult(aliasResult, null);
            return new PathNode(this, aliasResult); ;
        }

        internal virtual void Compile(CompileState state)
        {
            Compile(state, false);
        }
        internal void Compile(CompileState state, bool suppressAliases)
        {
            state.Translator.Compile(this, state, suppressAliases);
        }
    }
}
