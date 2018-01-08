
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static WorkOrderNode WorkOrder { get { return new WorkOrderNode(); } }
	}

	public partial class WorkOrderNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "WorkOrder";
            }
        }

		internal WorkOrderNode() { }
		internal WorkOrderNode(WorkOrderAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal WorkOrderNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public WorkOrderNode Alias(out WorkOrderAlias alias)
		{
			alias = new WorkOrderAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public WorkOrderIn  In  { get { return new WorkOrderIn(this); } }
		public class WorkOrderIn
		{
			private WorkOrderNode Parent;
			internal WorkOrderIn(WorkOrderNode parent)
			{
                Parent = parent;
			}
			public IFromIn_WORKORDER_HAS_PRODUCT_REL WORKORDER_HAS_PRODUCT { get { return new WORKORDER_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_WORKORDER_HAS_SCRAPREASON_REL WORKORDER_HAS_SCRAPREASON { get { return new WORKORDER_HAS_SCRAPREASON_REL(Parent, DirectionEnum.In); } }

		}

		public WorkOrderOut Out { get { return new WorkOrderOut(this); } }
		public class WorkOrderOut
		{
			private WorkOrderNode Parent;
			internal WorkOrderOut(WorkOrderNode parent)
			{
                Parent = parent;
			}
			public IFromOut_WORKORDERROUTING_HAS_WORKORDER_REL WORKORDERROUTING_HAS_WORKORDER { get { return new WORKORDERROUTING_HAS_WORKORDER_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class WorkOrderAlias : AliasResult
    {
        internal WorkOrderAlias(WorkOrderNode parent)
        {
			Node = parent;
            OrderQty = new NumericResult(this, "OrderQty", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["OrderQty"]);
            StockedQty = new NumericResult(this, "StockedQty", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["StockedQty"]);
            ScrappedQty = new NumericResult(this, "ScrappedQty", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["ScrappedQty"]);
            StartDate = new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["StartDate"]);
            EndDate = new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["EndDate"]);
            DueDate = new DateTimeResult(this, "DueDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["DueDate"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public WorkOrderNode.WorkOrderIn In { get { return new WorkOrderNode.WorkOrderIn(new WorkOrderNode(this, true)); } }
        public WorkOrderNode.WorkOrderOut Out { get { return new WorkOrderNode.WorkOrderOut(new WorkOrderNode(this, true)); } }

        public NumericResult OrderQty { get; private set; } 
        public NumericResult StockedQty { get; private set; } 
        public NumericResult ScrappedQty { get; private set; } 
        public DateTimeResult StartDate { get; private set; } 
        public DateTimeResult EndDate { get; private set; } 
        public DateTimeResult DueDate { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
