using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesTerritoryHistoryNode SalesTerritoryHistory { get { return new SalesTerritoryHistoryNode(); } }
	}

	public partial class SalesTerritoryHistoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesTerritoryHistory";
            }
        }

		internal SalesTerritoryHistoryNode() { }
		internal SalesTerritoryHistoryNode(SalesTerritoryHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesTerritoryHistoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesTerritoryHistoryNode Alias(out SalesTerritoryHistoryAlias alias)
		{
			alias = new SalesTerritoryHistoryAlias(this);
            NodeAlias = alias;
			return this;
		}


		public SalesTerritoryHistoryOut Out { get { return new SalesTerritoryHistoryOut(this); } }
		public class SalesTerritoryHistoryOut
		{
			private SalesTerritoryHistoryNode Parent;
			internal SalesTerritoryHistoryOut(SalesTerritoryHistoryNode parent)
			{
                Parent = parent;
			}
			public IFromOut_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL SALESTERRITORY_HAS_SALESTERRITORYHISTORY { get { return new SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class SalesTerritoryHistoryAlias : AliasResult
    {
        internal SalesTerritoryHistoryAlias(SalesTerritoryHistoryNode parent)
        {
			Node = parent;
            StartDate = new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"], Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"].Properties["StartDate"]);
            EndDate = new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"], Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"].Properties["EndDate"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"], Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public SalesTerritoryHistoryNode.SalesTerritoryHistoryOut Out { get { return new SalesTerritoryHistoryNode.SalesTerritoryHistoryOut(new SalesTerritoryHistoryNode(this, true)); } }

        public DateTimeResult StartDate { get; private set; } 
        public DateTimeResult EndDate { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
