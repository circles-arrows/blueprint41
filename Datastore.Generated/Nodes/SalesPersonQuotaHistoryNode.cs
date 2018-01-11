using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesPersonQuotaHistoryNode SalesPersonQuotaHistory { get { return new SalesPersonQuotaHistoryNode(); } }
	}

	public partial class SalesPersonQuotaHistoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesPersonQuotaHistory";
            }
        }

		internal SalesPersonQuotaHistoryNode() { }
		internal SalesPersonQuotaHistoryNode(SalesPersonQuotaHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesPersonQuotaHistoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesPersonQuotaHistoryNode Alias(out SalesPersonQuotaHistoryAlias alias)
		{
			alias = new SalesPersonQuotaHistoryAlias(this);
            NodeAlias = alias;
			return this;
		}


		public SalesPersonQuotaHistoryOut Out { get { return new SalesPersonQuotaHistoryOut(this); } }
		public class SalesPersonQuotaHistoryOut
		{
			private SalesPersonQuotaHistoryNode Parent;
			internal SalesPersonQuotaHistoryOut(SalesPersonQuotaHistoryNode parent)
			{
                Parent = parent;
			}
			public IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL SALESPERSON_HAS_SALESPERSONQUOTAHISTORY { get { return new SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class SalesPersonQuotaHistoryAlias : AliasResult
    {
        internal SalesPersonQuotaHistoryAlias(SalesPersonQuotaHistoryNode parent)
        {
			Node = parent;
            QuotaDate = new DateTimeResult(this, "QuotaDate", Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"], Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"].Properties["QuotaDate"]);
            SalesQuota = new StringResult(this, "SalesQuota", Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"], Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"].Properties["SalesQuota"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"], Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public SalesPersonQuotaHistoryNode.SalesPersonQuotaHistoryOut Out { get { return new SalesPersonQuotaHistoryNode.SalesPersonQuotaHistoryOut(new SalesPersonQuotaHistoryNode(this, true)); } }

        public DateTimeResult QuotaDate { get; private set; } 
        public StringResult SalesQuota { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
