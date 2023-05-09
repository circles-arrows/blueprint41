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
		public static PurchaseOrderDetailNode PurchaseOrderDetail { get { return new PurchaseOrderDetailNode(); } }
	}

	public partial class PurchaseOrderDetailNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(PurchaseOrderDetailNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(PurchaseOrderDetailNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "PurchaseOrderDetail";
		}

		protected override Entity GetEntity()
        {
			return m.PurchaseOrderDetail.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.PurchaseOrderDetail.Entity.FunctionalId;
            }
        }

		internal PurchaseOrderDetailNode() { }
		internal PurchaseOrderDetailNode(PurchaseOrderDetailAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PurchaseOrderDetailNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal PurchaseOrderDetailNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public PurchaseOrderDetailNode Where(JsNotation<System.DateTime> DueDate = default, JsNotation<string> LineTotal = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OrderQty = default, JsNotation<int> ReceivedQty = default, JsNotation<int> RejectedQty = default, JsNotation<int> StockedQty = default, JsNotation<string> Uid = default, JsNotation<double> UnitPrice = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PurchaseOrderDetailAlias> alias = new Lazy<PurchaseOrderDetailAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (DueDate.HasValue) conditions.Add(new QueryCondition(alias.Value.DueDate, Operator.Equals, ((IValue)DueDate).GetValue()));
            if (LineTotal.HasValue) conditions.Add(new QueryCondition(alias.Value.LineTotal, Operator.Equals, ((IValue)LineTotal).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (OrderQty.HasValue) conditions.Add(new QueryCondition(alias.Value.OrderQty, Operator.Equals, ((IValue)OrderQty).GetValue()));
            if (ReceivedQty.HasValue) conditions.Add(new QueryCondition(alias.Value.ReceivedQty, Operator.Equals, ((IValue)ReceivedQty).GetValue()));
            if (RejectedQty.HasValue) conditions.Add(new QueryCondition(alias.Value.RejectedQty, Operator.Equals, ((IValue)RejectedQty).GetValue()));
            if (StockedQty.HasValue) conditions.Add(new QueryCondition(alias.Value.StockedQty, Operator.Equals, ((IValue)StockedQty).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));
            if (UnitPrice.HasValue) conditions.Add(new QueryCondition(alias.Value.UnitPrice, Operator.Equals, ((IValue)UnitPrice).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public PurchaseOrderDetailNode Assign(JsNotation<System.DateTime> DueDate = default, JsNotation<string> LineTotal = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OrderQty = default, JsNotation<int> ReceivedQty = default, JsNotation<int> RejectedQty = default, JsNotation<int> StockedQty = default, JsNotation<string> Uid = default, JsNotation<double> UnitPrice = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PurchaseOrderDetailAlias> alias = new Lazy<PurchaseOrderDetailAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (DueDate.HasValue) assignments.Add(new Assignment(alias.Value.DueDate, DueDate));
            if (LineTotal.HasValue) assignments.Add(new Assignment(alias.Value.LineTotal, LineTotal));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (OrderQty.HasValue) assignments.Add(new Assignment(alias.Value.OrderQty, OrderQty));
            if (ReceivedQty.HasValue) assignments.Add(new Assignment(alias.Value.ReceivedQty, ReceivedQty));
            if (RejectedQty.HasValue) assignments.Add(new Assignment(alias.Value.RejectedQty, RejectedQty));
            if (StockedQty.HasValue) assignments.Add(new Assignment(alias.Value.StockedQty, StockedQty));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));
            if (UnitPrice.HasValue) assignments.Add(new Assignment(alias.Value.UnitPrice, UnitPrice));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public PurchaseOrderDetailNode Alias(out PurchaseOrderDetailAlias alias)
        {
            if (NodeAlias is PurchaseOrderDetailAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new PurchaseOrderDetailAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public PurchaseOrderDetailNode Alias(out PurchaseOrderDetailAlias alias, string name)
        {
            if (NodeAlias is PurchaseOrderDetailAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new PurchaseOrderDetailAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public PurchaseOrderDetailNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public PurchaseOrderDetailIn  In  { get { return new PurchaseOrderDetailIn(this); } }
		public class PurchaseOrderDetailIn
		{
			private PurchaseOrderDetailNode Parent;
			internal PurchaseOrderDetailIn(PurchaseOrderDetailNode parent)
			{
				Parent = parent;
			}
			public IFromIn_PURCHASEORDERDETAIL_HAS_PRODUCT_REL PURCHASEORDERDETAIL_HAS_PRODUCT { get { return new PURCHASEORDERDETAIL_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER_REL PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER { get { return new PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class PurchaseOrderDetailAlias : AliasResult<PurchaseOrderDetailAlias, PurchaseOrderDetailListAlias>
	{
		internal PurchaseOrderDetailAlias(PurchaseOrderDetailNode parent)
		{
			Node = parent;
		}
		internal PurchaseOrderDetailAlias(PurchaseOrderDetailNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  PurchaseOrderDetailAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  PurchaseOrderDetailAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  PurchaseOrderDetailAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> DueDate = default, JsNotation<string> LineTotal = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OrderQty = default, JsNotation<int> ReceivedQty = default, JsNotation<int> RejectedQty = default, JsNotation<int> StockedQty = default, JsNotation<string> Uid = default, JsNotation<double> UnitPrice = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (DueDate.HasValue) assignments.Add(new Assignment(this.DueDate, DueDate));
			if (LineTotal.HasValue) assignments.Add(new Assignment(this.LineTotal, LineTotal));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (OrderQty.HasValue) assignments.Add(new Assignment(this.OrderQty, OrderQty));
			if (ReceivedQty.HasValue) assignments.Add(new Assignment(this.ReceivedQty, ReceivedQty));
			if (RejectedQty.HasValue) assignments.Add(new Assignment(this.RejectedQty, RejectedQty));
			if (StockedQty.HasValue) assignments.Add(new Assignment(this.StockedQty, StockedQty));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
			if (UnitPrice.HasValue) assignments.Add(new Assignment(this.UnitPrice, UnitPrice));
            
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
						{ "DueDate", new DateTimeResult(this, "DueDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["DueDate"]) },
						{ "OrderQty", new NumericResult(this, "OrderQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["OrderQty"]) },
						{ "UnitPrice", new FloatResult(this, "UnitPrice", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["UnitPrice"]) },
						{ "LineTotal", new StringResult(this, "LineTotal", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["LineTotal"]) },
						{ "ReceivedQty", new NumericResult(this, "ReceivedQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["ReceivedQty"]) },
						{ "RejectedQty", new NumericResult(this, "RejectedQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["RejectedQty"]) },
						{ "StockedQty", new NumericResult(this, "StockedQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["StockedQty"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public PurchaseOrderDetailNode.PurchaseOrderDetailIn In { get { return new PurchaseOrderDetailNode.PurchaseOrderDetailIn(new PurchaseOrderDetailNode(this, true)); } }

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
		public FloatResult UnitPrice
		{
			get
			{
				if (m_UnitPrice is null)
					m_UnitPrice = (FloatResult)AliasFields["UnitPrice"];

				return m_UnitPrice;
			}
		}
		private FloatResult m_UnitPrice = null;
		public StringResult LineTotal
		{
			get
			{
				if (m_LineTotal is null)
					m_LineTotal = (StringResult)AliasFields["LineTotal"];

				return m_LineTotal;
			}
		}
		private StringResult m_LineTotal = null;
		public NumericResult ReceivedQty
		{
			get
			{
				if (m_ReceivedQty is null)
					m_ReceivedQty = (NumericResult)AliasFields["ReceivedQty"];

				return m_ReceivedQty;
			}
		}
		private NumericResult m_ReceivedQty = null;
		public NumericResult RejectedQty
		{
			get
			{
				if (m_RejectedQty is null)
					m_RejectedQty = (NumericResult)AliasFields["RejectedQty"];

				return m_RejectedQty;
			}
		}
		private NumericResult m_RejectedQty = null;
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
		public AsResult As(string aliasName, out PurchaseOrderDetailAlias alias)
		{
			alias = new PurchaseOrderDetailAlias((PurchaseOrderDetailNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class PurchaseOrderDetailListAlias : ListResult<PurchaseOrderDetailListAlias, PurchaseOrderDetailAlias>, IAliasListResult
	{
		private PurchaseOrderDetailListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PurchaseOrderDetailListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PurchaseOrderDetailListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class PurchaseOrderDetailJaggedListAlias : ListResult<PurchaseOrderDetailJaggedListAlias, PurchaseOrderDetailListAlias>, IAliasJaggedListResult
	{
		private PurchaseOrderDetailJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PurchaseOrderDetailJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PurchaseOrderDetailJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
