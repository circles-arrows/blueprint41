using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Domain.Data.Manipulation;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static WorkOrderRoutingNode WorkOrderRouting { get { return new WorkOrderRoutingNode(); } }
	}

	public partial class WorkOrderRoutingNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(WorkOrderRoutingNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(WorkOrderRoutingNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "WorkOrderRouting";
		}

		protected override Entity GetEntity()
        {
			return m.WorkOrderRouting.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.WorkOrderRouting.Entity.FunctionalId;
            }
        }

		internal WorkOrderRoutingNode() { }
		internal WorkOrderRoutingNode(WorkOrderRoutingAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal WorkOrderRoutingNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal WorkOrderRoutingNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public WorkOrderRoutingNode Where(JsNotation<string> ActualCost = default, JsNotation<System.DateTime> ActualEndDate = default, JsNotation<string> ActualResourceHrs = default, JsNotation<System.DateTime> ActualStartDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> OperationSequence = default, JsNotation<string> PlannedCost = default, JsNotation<System.DateTime> ScheduledEndDate = default, JsNotation<System.DateTime> ScheduledStartDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<WorkOrderRoutingAlias> alias = new Lazy<WorkOrderRoutingAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ActualCost.HasValue) conditions.Add(new QueryCondition(alias.Value.ActualCost, Operator.Equals, ((IValue)ActualCost).GetValue()));
            if (ActualEndDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ActualEndDate, Operator.Equals, ((IValue)ActualEndDate).GetValue()));
            if (ActualResourceHrs.HasValue) conditions.Add(new QueryCondition(alias.Value.ActualResourceHrs, Operator.Equals, ((IValue)ActualResourceHrs).GetValue()));
            if (ActualStartDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ActualStartDate, Operator.Equals, ((IValue)ActualStartDate).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (OperationSequence.HasValue) conditions.Add(new QueryCondition(alias.Value.OperationSequence, Operator.Equals, ((IValue)OperationSequence).GetValue()));
            if (PlannedCost.HasValue) conditions.Add(new QueryCondition(alias.Value.PlannedCost, Operator.Equals, ((IValue)PlannedCost).GetValue()));
            if (ScheduledEndDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ScheduledEndDate, Operator.Equals, ((IValue)ScheduledEndDate).GetValue()));
            if (ScheduledStartDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ScheduledStartDate, Operator.Equals, ((IValue)ScheduledStartDate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public WorkOrderRoutingNode Assign(JsNotation<string> ActualCost = default, JsNotation<System.DateTime> ActualEndDate = default, JsNotation<string> ActualResourceHrs = default, JsNotation<System.DateTime> ActualStartDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> OperationSequence = default, JsNotation<string> PlannedCost = default, JsNotation<System.DateTime> ScheduledEndDate = default, JsNotation<System.DateTime> ScheduledStartDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<WorkOrderRoutingAlias> alias = new Lazy<WorkOrderRoutingAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ActualCost.HasValue) assignments.Add(new Assignment(alias.Value.ActualCost, ActualCost));
            if (ActualEndDate.HasValue) assignments.Add(new Assignment(alias.Value.ActualEndDate, ActualEndDate));
            if (ActualResourceHrs.HasValue) assignments.Add(new Assignment(alias.Value.ActualResourceHrs, ActualResourceHrs));
            if (ActualStartDate.HasValue) assignments.Add(new Assignment(alias.Value.ActualStartDate, ActualStartDate));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (OperationSequence.HasValue) assignments.Add(new Assignment(alias.Value.OperationSequence, OperationSequence));
            if (PlannedCost.HasValue) assignments.Add(new Assignment(alias.Value.PlannedCost, PlannedCost));
            if (ScheduledEndDate.HasValue) assignments.Add(new Assignment(alias.Value.ScheduledEndDate, ScheduledEndDate));
            if (ScheduledStartDate.HasValue) assignments.Add(new Assignment(alias.Value.ScheduledStartDate, ScheduledStartDate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public WorkOrderRoutingNode Alias(out WorkOrderRoutingAlias alias)
        {
            if (NodeAlias is WorkOrderRoutingAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new WorkOrderRoutingAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public WorkOrderRoutingNode Alias(out WorkOrderRoutingAlias alias, string name)
        {
            if (NodeAlias is WorkOrderRoutingAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new WorkOrderRoutingAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public WorkOrderRoutingNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
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

	public class WorkOrderRoutingAlias : AliasResult<WorkOrderRoutingAlias, WorkOrderRoutingListAlias>
	{
		internal WorkOrderRoutingAlias(WorkOrderRoutingNode parent)
		{
			Node = parent;
		}
		internal WorkOrderRoutingAlias(WorkOrderRoutingNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  WorkOrderRoutingAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  WorkOrderRoutingAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  WorkOrderRoutingAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> ActualCost = default, JsNotation<System.DateTime> ActualEndDate = default, JsNotation<string> ActualResourceHrs = default, JsNotation<System.DateTime> ActualStartDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> OperationSequence = default, JsNotation<string> PlannedCost = default, JsNotation<System.DateTime> ScheduledEndDate = default, JsNotation<System.DateTime> ScheduledStartDate = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (ActualCost.HasValue) assignments.Add(new Assignment(this.ActualCost, ActualCost));
			if (ActualEndDate.HasValue) assignments.Add(new Assignment(this.ActualEndDate, ActualEndDate));
			if (ActualResourceHrs.HasValue) assignments.Add(new Assignment(this.ActualResourceHrs, ActualResourceHrs));
			if (ActualStartDate.HasValue) assignments.Add(new Assignment(this.ActualStartDate, ActualStartDate));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (OperationSequence.HasValue) assignments.Add(new Assignment(this.OperationSequence, OperationSequence));
			if (PlannedCost.HasValue) assignments.Add(new Assignment(this.PlannedCost, PlannedCost));
			if (ScheduledEndDate.HasValue) assignments.Add(new Assignment(this.ScheduledEndDate, ScheduledEndDate));
			if (ScheduledStartDate.HasValue) assignments.Add(new Assignment(this.ScheduledStartDate, ScheduledStartDate));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
            
            return assignments.ToArray();
        }


		public override IReadOnlyDictionary<string, FieldResult> AliasFields
		{
			get
			{
				if (m_AliasFields is null)
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
				if (m_OperationSequence is null)
					m_OperationSequence = (StringResult)AliasFields["OperationSequence"];

				return m_OperationSequence;
			}
		}
		private StringResult m_OperationSequence = null;
		public DateTimeResult ScheduledStartDate
		{
			get
			{
				if (m_ScheduledStartDate is null)
					m_ScheduledStartDate = (DateTimeResult)AliasFields["ScheduledStartDate"];

				return m_ScheduledStartDate;
			}
		}
		private DateTimeResult m_ScheduledStartDate = null;
		public DateTimeResult ScheduledEndDate
		{
			get
			{
				if (m_ScheduledEndDate is null)
					m_ScheduledEndDate = (DateTimeResult)AliasFields["ScheduledEndDate"];

				return m_ScheduledEndDate;
			}
		}
		private DateTimeResult m_ScheduledEndDate = null;
		public DateTimeResult ActualStartDate
		{
			get
			{
				if (m_ActualStartDate is null)
					m_ActualStartDate = (DateTimeResult)AliasFields["ActualStartDate"];

				return m_ActualStartDate;
			}
		}
		private DateTimeResult m_ActualStartDate = null;
		public DateTimeResult ActualEndDate
		{
			get
			{
				if (m_ActualEndDate is null)
					m_ActualEndDate = (DateTimeResult)AliasFields["ActualEndDate"];

				return m_ActualEndDate;
			}
		}
		private DateTimeResult m_ActualEndDate = null;
		public StringResult ActualResourceHrs
		{
			get
			{
				if (m_ActualResourceHrs is null)
					m_ActualResourceHrs = (StringResult)AliasFields["ActualResourceHrs"];

				return m_ActualResourceHrs;
			}
		}
		private StringResult m_ActualResourceHrs = null;
		public StringResult PlannedCost
		{
			get
			{
				if (m_PlannedCost is null)
					m_PlannedCost = (StringResult)AliasFields["PlannedCost"];

				return m_PlannedCost;
			}
		}
		private StringResult m_PlannedCost = null;
		public StringResult ActualCost
		{
			get
			{
				if (m_ActualCost is null)
					m_ActualCost = (StringResult)AliasFields["ActualCost"];

				return m_ActualCost;
			}
		}
		private StringResult m_ActualCost = null;
		public DateTimeResult ModifiedDate
		{
			get
			{
				if (m_ModifiedDate is null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		}
		private DateTimeResult m_ModifiedDate = null;
		public StringResult Uid
		{
			get
			{
				if (m_Uid is null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		}
		private StringResult m_Uid = null;
		public AsResult As(string aliasName, out WorkOrderRoutingAlias alias)
		{
			alias = new WorkOrderRoutingAlias((WorkOrderRoutingNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class WorkOrderRoutingListAlias : ListResult<WorkOrderRoutingListAlias, WorkOrderRoutingAlias>, IAliasListResult
	{
		private WorkOrderRoutingListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private WorkOrderRoutingListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private WorkOrderRoutingListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class WorkOrderRoutingJaggedListAlias : ListResult<WorkOrderRoutingJaggedListAlias, WorkOrderRoutingListAlias>, IAliasJaggedListResult
	{
		private WorkOrderRoutingJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private WorkOrderRoutingJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private WorkOrderRoutingJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
