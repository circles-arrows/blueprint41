
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static IllustrationNode Illustration { get { return new IllustrationNode(); } }
	}

	public partial class IllustrationNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Illustration";
            }
        }

		internal IllustrationNode() { }
		internal IllustrationNode(IllustrationAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal IllustrationNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public IllustrationNode Alias(out IllustrationAlias alias)
		{
			alias = new IllustrationAlias(this);
            NodeAlias = alias;
			return this;
		}


		public IllustrationOut Out { get { return new IllustrationOut(this); } }
		public class IllustrationOut
		{
			private IllustrationNode Parent;
			internal IllustrationOut(IllustrationNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCTMODEL_HAS_ILLUSTRATION_REL PRODUCTMODEL_HAS_ILLUSTRATION { get { return new PRODUCTMODEL_HAS_ILLUSTRATION_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class IllustrationAlias : AliasResult
    {
        internal IllustrationAlias(IllustrationNode parent)
        {
			Node = parent;
            Diagram = new StringResult(this, "Diagram", Datastore.AdventureWorks.Model.Entities["Illustration"], Datastore.AdventureWorks.Model.Entities["Illustration"].Properties["Diagram"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Illustration"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Illustration"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public IllustrationNode.IllustrationOut Out { get { return new IllustrationNode.IllustrationOut(new IllustrationNode(this, true)); } }

        public StringResult Diagram { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
