using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static WorkOrderRoutingNode WorkOrderRouting { get { return new WorkOrderRoutingNode(); } }
	}

	public partial class WorkOrderRoutingNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "WorkOrderRouting";
        }

		internal WorkOrderRoutingNode() { }
		internal WorkOrderRoutingNode(WorkOrderRoutingAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal WorkOrderRoutingNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public WorkOrderRoutingNode Alias(out WorkOrderRoutingAlias alias)
		{
			alias = new WorkOrderRoutingAlias(this);
            NodeAlias = alias;
			return this;
		}

		public WorkOrderRoutingNode UseExistingAlias(AliasResult alias)
        {
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "OperationSequence", new StringResult(this, "OperationSequence", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["OperationSequence"]) },
						{ "ScheduledStartDate", new DateTimeResult(this, "ScheduledStartDate", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ScheduledStartDate"]) },
						{ "ScheduledEndDate", new DateTimeResult(this, "ScheduledEndDate", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ScheduledEndDate"]) },
						{ "ActualStartDate", new DateTimeResult(this, "ActualStartDate", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualStartDate"]) },
						{ "ActualEndDate", new DateTimeResult(this, "ActualEndDate", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualEndDate"]) },
						{ "ActualResourceHrs", new StringResult(this, "ActualResourceHrs", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualResourceHrs"]) },
						{ "PlannedCost", new StringResult(this, "PlannedCost", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["PlannedCost"]) },
						{ "ActualCost", new StringResult(this, "ActualCost", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"].Properties["ActualCost"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public WorkOrderRoutingNode.WorkOrderRoutingIn In { get { return new WorkOrderRoutingNode.WorkOrderRoutingIn(new WorkOrderRoutingNode(this, true)); } }

        public StringResult OperationSequence
		{
			get
			{
				if ((object)m_OperationSequence == null)
					m_OperationSequence = (StringResult)AliasFields["OperationSequence"];

				return m_OperationSequence;
			}
		} 
        private StringResult m_OperationSequence = null;
        public DateTimeResult ScheduledStartDate
		{
			get
			{
				if ((object)m_ScheduledStartDate == null)
					m_ScheduledStartDate = (DateTimeResult)AliasFields["ScheduledStartDate"];

				return m_ScheduledStartDate;
			}
		} 
        private DateTimeResult m_ScheduledStartDate = null;
        public DateTimeResult ScheduledEndDate
		{
			get
			{
				if ((object)m_ScheduledEndDate == null)
					m_ScheduledEndDate = (DateTimeResult)AliasFields["ScheduledEndDate"];

				return m_ScheduledEndDate;
			}
		} 
        private DateTimeResult m_ScheduledEndDate = null;
        public DateTimeResult ActualStartDate
		{
			get
			{
				if ((object)m_ActualStartDate == null)
					m_ActualStartDate = (DateTimeResult)AliasFields["ActualStartDate"];

				return m_ActualStartDate;
			}
		} 
        private DateTimeResult m_ActualStartDate = null;
        public DateTimeResult ActualEndDate
		{
			get
			{
				if ((object)m_ActualEndDate == null)
					m_ActualEndDate = (DateTimeResult)AliasFields["ActualEndDate"];

				return m_ActualEndDate;
			}
		} 
        private DateTimeResult m_ActualEndDate = null;
        public StringResult ActualResourceHrs
		{
			get
			{
				if ((object)m_ActualResourceHrs == null)
					m_ActualResourceHrs = (StringResult)AliasFields["ActualResourceHrs"];

				return m_ActualResourceHrs;
			}
		} 
        private StringResult m_ActualResourceHrs = null;
        public StringResult PlannedCost
		{
			get
			{
				if ((object)m_PlannedCost == null)
					m_PlannedCost = (StringResult)AliasFields["PlannedCost"];

				return m_PlannedCost;
			}
		} 
        private StringResult m_PlannedCost = null;
        public StringResult ActualCost
		{
			get
			{
				if ((object)m_ActualCost == null)
					m_ActualCost = (StringResult)AliasFields["ActualCost"];

				return m_ActualCost;
			}
		} 
        private StringResult m_ActualCost = null;
        public DateTimeResult ModifiedDate
		{
			get
			{
				if ((object)m_ModifiedDate == null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		} 
        private DateTimeResult m_ModifiedDate = null;
        public StringResult Uid
		{
			get
			{
				if ((object)m_Uid == null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		} 
        private StringResult m_Uid = null;
    }
}
