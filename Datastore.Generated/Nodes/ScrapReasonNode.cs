using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ScrapReasonNode ScrapReason { get { return new ScrapReasonNode(); } }
	}

	public partial class ScrapReasonNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ScrapReason";
            }
        }

		internal ScrapReasonNode() { }
		internal ScrapReasonNode(ScrapReasonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ScrapReasonNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ScrapReasonNode Alias(out ScrapReasonAlias alias)
		{
			alias = new ScrapReasonAlias(this);
            NodeAlias = alias;
			return this;
		}


		public ScrapReasonOut Out { get { return new ScrapReasonOut(this); } }
		public class ScrapReasonOut
		{
			private ScrapReasonNode Parent;
			internal ScrapReasonOut(ScrapReasonNode parent)
			{
                Parent = parent;
			}
			public IFromOut_WORKORDER_HAS_SCRAPREASON_REL WORKORDER_HAS_SCRAPREASON { get { return new WORKORDER_HAS_SCRAPREASON_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ScrapReasonAlias : AliasResult
    {
        internal ScrapReasonAlias(ScrapReasonNode parent)
        {
			Node = parent;
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ScrapReason"], Datastore.AdventureWorks.Model.Entities["ScrapReason"].Properties["Name"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ScrapReason"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ScrapReason"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ScrapReasonNode.ScrapReasonOut Out { get { return new ScrapReasonNode.ScrapReasonOut(new ScrapReasonNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
