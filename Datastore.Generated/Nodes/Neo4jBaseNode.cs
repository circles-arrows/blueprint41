
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		[Obsolete("This entity is virtual, consider making entity Neo4jBase concrete or use another entity as your starting point.", true)]
		public static Neo4jBaseNode Neo4jBase { get { return new Neo4jBaseNode(); } }
	}

	public partial class Neo4jBaseNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
                return null;
            }
        }

		internal Neo4jBaseNode() { }
		internal Neo4jBaseNode(Neo4jBaseAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal Neo4jBaseNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public Neo4jBaseNode Alias(out Neo4jBaseAlias alias)
		{
			alias = new Neo4jBaseAlias(this);
            NodeAlias = alias;
			return this;
		}

	}

    public class Neo4jBaseAlias : AliasResult
    {
        internal Neo4jBaseAlias(Neo4jBaseNode parent)
        {
			Node = parent;
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Neo4jBase"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }


        public StringResult Uid { get; private set; } 
    }
}
