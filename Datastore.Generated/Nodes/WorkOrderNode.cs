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
		public static WorkOrderNode WorkOrder { get { return new WorkOrderNode(); } }
	}

	public partial class WorkOrderNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(WorkOrderNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(WorkOrderNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "WorkOrder";
		}

		protected override Entity GetEntity()
        {
			return m.WorkOrder.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.WorkOrder.Entity.FunctionalId;
            }
        }

		internal WorkOrderNode() { }
		internal WorkOrderNode(WorkOrderAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal WorkOrderNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal WorkOrderNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public WorkOrderNode Where(JsNotation<System.DateTime> DueDate = default, JsNotation<System.DateTime?> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OrderQty = default, JsNotation<int> ScrappedQty = default, JsNotation<System.DateTime> StartDate = default, JsNotation<int> StockedQty = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<WorkOrderAlias> alias = new Lazy<WorkOrderAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (DueDate.HasValue) conditions.Add(new QueryCondition(alias.Value.DueDate, Operator.Equals, ((IValue)DueDate).GetValue()));
            if (EndDate.HasValue) conditions.Add(new QueryCondition(alias.Value.EndDate, Operator.Equals, ((IValue)EndDate).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (OrderQty.HasValue) conditions.Add(new QueryCondition(alias.Value.OrderQty, Operator.Equals, ((IValue)OrderQty).GetValue()));
            if (ScrappedQty.HasValue) conditions.Add(new QueryCondition(alias.Value.ScrappedQty, Operator.Equals, ((IValue)ScrappedQty).GetValue()));
            if (StartDate.HasValue) conditions.Add(new QueryCondition(alias.Value.StartDate, Operator.Equals, ((IValue)StartDate).GetValue()));
            if (StockedQty.HasValue) conditions.Add(new QueryCondition(alias.Value.StockedQty, Operator.Equals, ((IValue)StockedQty).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public WorkOrderNode Assign(JsNotation<System.DateTime> DueDate = default, JsNotation<System.DateTime?> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OrderQty = default, JsNotation<int> ScrappedQty = default, JsNotation<System.DateTime> StartDate = default, JsNotation<int> StockedQty = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<WorkOrderAlias> alias = new Lazy<WorkOrderAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (DueDate.HasValue) assignments.Add(new Assignment(alias.Value.DueDate, DueDate));
            if (EndDate.HasValue) assignments.Add(new Assignment(alias.Value.EndDate, EndDate));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (OrderQty.HasValue) assignments.Add(new Assignment(alias.Value.OrderQty, OrderQty));
            if (ScrappedQty.HasValue) assignments.Add(new Assignment(alias.Value.ScrappedQty, ScrappedQty));
            if (StartDate.HasValue) assignments.Add(new Assignment(alias.Value.StartDate, StartDate));
            if (StockedQty.HasValue) assignments.Add(new Assignment(alias.Value.StockedQty, StockedQty));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public WorkOrderNode Alias(out WorkOrderAlias alias)
        {
            if (NodeAlias is WorkOrderAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new WorkOrderAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public WorkOrderNode Alias(out WorkOrderAlias alias, string name)
        {
            if (NodeAlias is WorkOrderAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new WorkOrderAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public WorkOrderNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
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

	public class WorkOrderAlias : AliasResult<WorkOrderAlias, WorkOrderListAlias>
	{
		internal WorkOrderAlias(WorkOrderNode parent)
		{
			Node = parent;
		}
		internal WorkOrderAlias(WorkOrderNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  WorkOrderAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  WorkOrderAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  WorkOrderAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> DueDate = default, JsNotation<System.DateTime?> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OrderQty = default, JsNotation<int> ScrappedQty = default, JsNotation<System.DateTime> StartDate = default, JsNotation<int> StockedQty = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (DueDate.HasValue) assignments.Add(new Assignment(this.DueDate, DueDate));
			if (EndDate.HasValue) assignments.Add(new Assignment(this.EndDate, EndDate));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (OrderQty.HasValue) assignments.Add(new Assignment(this.OrderQty, OrderQty));
			if (ScrappedQty.HasValue) assignments.Add(new Assignment(this.ScrappedQty, ScrappedQty));
			if (StartDate.HasValue) assignments.Add(new Assignment(this.StartDate, StartDate));
			if (StockedQty.HasValue) assignments.Add(new Assignment(this.StockedQty, StockedQty));
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
						{ "OrderQty", new NumericResult(this, "OrderQty", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["OrderQty"]) },
						{ "StockedQty", new NumericResult(this, "StockedQty", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["StockedQty"]) },
						{ "ScrappedQty", new NumericResult(this, "ScrappedQty", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["ScrappedQty"]) },
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["EndDate"]) },
						{ "DueDate", new DateTimeResult(this, "DueDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["DueDate"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public WorkOrderNode.WorkOrderIn In { get { return new WorkOrderNode.WorkOrderIn(new WorkOrderNode(this, true)); } }
		public WorkOrderNode.WorkOrderOut Out { get { return new WorkOrderNode.WorkOrderOut(new WorkOrderNode(this, true)); } }

		public NumericResult OrderQty
		{
			get
			{
				if (m_OrderQty is null)
					m_OrderQty = (NumericResult)AliasFields["OrderQty"];

				return m_OrderQty;
			}
		}
		private NumericResult m_OrderQty = null;
		public NumericResult StockedQty
		{
			get
			{
				if (m_StockedQty is null)
					m_StockedQty = (NumericResult)AliasFields["StockedQty"];

				return m_StockedQty;
			}
		}
		private NumericResult m_StockedQty = null;
		public NumericResult ScrappedQty
		{
			get
			{
				if (m_ScrappedQty is null)
					m_ScrappedQty = (NumericResult)AliasFields["ScrappedQty"];

				return m_ScrappedQty;
			}
		}
		private NumericResult m_ScrappedQty = null;
		public DateTimeResult StartDate
		{
			get
			{
				if (m_StartDate is null)
					m_StartDate = (DateTimeResult)AliasFields["StartDate"];

				return m_StartDate;
			}
		}
		private DateTimeResult m_StartDate = null;
		public DateTimeResult EndDate
		{
			get
			{
				if (m_EndDate is null)
					m_EndDate = (DateTimeResult)AliasFields["EndDate"];

				return m_EndDate;
			}
		}
		private DateTimeResult m_EndDate = null;
		public DateTimeResult DueDate
		{
			get
			{
				if (m_DueDate is null)
					m_DueDate = (DateTimeResult)AliasFields["DueDate"];

				return m_DueDate;
			}
		}
		private DateTimeResult m_DueDate = null;
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
		public AsResult As(string aliasName, out WorkOrderAlias alias)
		{
			alias = new WorkOrderAlias((WorkOrderNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class WorkOrderListAlias : ListResult<WorkOrderListAlias, WorkOrderAlias>, IAliasListResult
	{
		private WorkOrderListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private WorkOrderListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private WorkOrderListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class WorkOrderJaggedListAlias : ListResult<WorkOrderJaggedListAlias, WorkOrderListAlias>, IAliasJaggedListResult
	{
		private WorkOrderJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private WorkOrderJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private WorkOrderJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
