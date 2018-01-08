
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesReasonNode SalesReason { get { return new SalesReasonNode(); } }
	}

	public partial class SalesReasonNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesReason";
            }
        }

		internal SalesReasonNode() { }
		internal SalesReasonNode(SalesReasonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesReasonNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesReasonNode Alias(out SalesReasonAlias alias)
		{
			alias = new SalesReasonAlias(this);
            NodeAlias = alias;
			return this;
		}


		public SalesReasonOut Out { get { return new SalesReasonOut(this); } }
		public class SalesReasonOut
		{
			private SalesReasonNode Parent;
			internal SalesReasonOut(SalesReasonNode parent)
			{
                Parent = parent;
			}
			public IFromOut_SALESORDERHEADER_HAS_SALESREASON_REL SALESORDERHEADER_HAS_SALESREASON { get { return new SALESORDERHEADER_HAS_SALESREASON_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class SalesReasonAlias : AliasResult
    {
        internal SalesReasonAlias(SalesReasonNode parent)
        {
			Node = parent;
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["SalesReason"].Properties["Name"]);
            ReasonType = new StringResult(this, "ReasonType", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["SalesReason"].Properties["ReasonType"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public SalesReasonNode.SalesReasonOut Out { get { return new SalesReasonNode.SalesReasonOut(new SalesReasonNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public StringResult ReasonType { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
