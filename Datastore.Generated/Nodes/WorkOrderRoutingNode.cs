
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static WorkOrderRoutingNode WorkOrderRouting { get { return new WorkOrderRoutingNode(); } }
	}

	public partial class WorkOrderRoutingNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "WorkOrderRouting";
            }
        }

		internal WorkOrderRoutingNode() { }
		internal WorkOrderRoutingNode(WorkOrderRoutingAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal WorkOrderRoutingNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public WorkOrderRoutingNode Alias(out WorkOrderRoutingAlias alias)
		{
			alias = new WorkOrderRoutingAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public WorkOrderRoutingIn  In  { get { return new WorkOrderRoutingIn(this); } }
		public class WorkOrderRoutingIn
		{
			private WorkOrderRoutingNode Parent;
			internal WorkOrderRoutingIn(WorkOrderRoutingNode parent)
			{
                Parent = parent;
			}
			public IFromIn_WORKORDERROUTING_HAS_LOCATION_REL WORKORDERROUTING_HAS_LOCATION { get { return new WORKORDERROUTING_HAS_LOCATION_REL(Parent, DirectionEnum.In); } }
			public IFromIn_WORKORDERROUTING_HAS_PRODUCT_REL WORKORDERROUTING_HAS_PRODUCT { get { return new WORKORDERROUTING_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_WORKORDERROUTING_HAS_WORKORDER_REL WORKORDERROUTING_HAS_WORKORDER { get { return new WORKORDERROUTING_HAS_WORKORDER_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class WorkOrderRoutingAlias : AliasResult
    {
        internal WorkOrderRoutingAlias(WorkOrderRoutingNode parent)
        {
			Node = parent;
            OperationSequence = new StringResult(this, "OperationSequence", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["OperationSequence"]);
            ScheduledStartDate = new DateTimeResult(this, "ScheduledStartDate", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ScheduledStartDate"]);
            ScheduledEndDate = new DateTimeResult(this, "ScheduledEndDate", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ScheduledEndDate"]);
            ActualStartDate = new DateTimeResult(this, "ActualStartDate", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualStartDate"]);
            ActualEndDate = new DateTimeResult(this, "ActualEndDate", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualEndDate"]);
            ActualResourceHrs = new StringResult(this, "ActualResourceHrs", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualResourceHrs"]);
            PlannedCost = new StringResult(this, "PlannedCost", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["PlannedCost"]);
            ActualCost = new StringResult(this, "ActualCost", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualCost"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public WorkOrderRoutingNode.WorkOrderRoutingIn In { get { return new WorkOrderRoutingNode.WorkOrderRoutingIn(new WorkOrderRoutingNode(this, true)); } }

        public StringResult OperationSequence { get; private set; } 
        public DateTimeResult ScheduledStartDate { get; private set; } 
        public DateTimeResult ScheduledEndDate { get; private set; } 
        public DateTimeResult ActualStartDate { get; private set; } 
        public DateTimeResult ActualEndDate { get; private set; } 
        public StringResult ActualResourceHrs { get; private set; } 
        public StringResult PlannedCost { get; private set; } 
        public StringResult ActualCost { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
