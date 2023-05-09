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
		public static SalesOrderDetailNode SalesOrderDetail { get { return new SalesOrderDetailNode(); } }
	}

	public partial class SalesOrderDetailNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(SalesOrderDetailNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(SalesOrderDetailNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "SalesOrderDetail";
		}

		protected override Entity GetEntity()
        {
			return m.SalesOrderDetail.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.SalesOrderDetail.Entity.FunctionalId;
            }
        }

		internal SalesOrderDetailNode() { }
		internal SalesOrderDetailNode(SalesOrderDetailAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesOrderDetailNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal SalesOrderDetailNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public SalesOrderDetailNode Where(JsNotation<string> CarrierTrackingNumber = default, JsNotation<string> LineTotal = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OrderQty = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default, JsNotation<double> UnitPrice = default, JsNotation<string> UnitPriceDiscount = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesOrderDetailAlias> alias = new Lazy<SalesOrderDetailAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (CarrierTrackingNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.CarrierTrackingNumber, Operator.Equals, ((IValue)CarrierTrackingNumber).GetValue()));
            if (LineTotal.HasValue) conditions.Add(new QueryCondition(alias.Value.LineTotal, Operator.Equals, ((IValue)LineTotal).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (OrderQty.HasValue) conditions.Add(new QueryCondition(alias.Value.OrderQty, Operator.Equals, ((IValue)OrderQty).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));
            if (UnitPrice.HasValue) conditions.Add(new QueryCondition(alias.Value.UnitPrice, Operator.Equals, ((IValue)UnitPrice).GetValue()));
            if (UnitPriceDiscount.HasValue) conditions.Add(new QueryCondition(alias.Value.UnitPriceDiscount, Operator.Equals, ((IValue)UnitPriceDiscount).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public SalesOrderDetailNode Assign(JsNotation<string> CarrierTrackingNumber = default, JsNotation<string> LineTotal = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OrderQty = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default, JsNotation<double> UnitPrice = default, JsNotation<string> UnitPriceDiscount = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesOrderDetailAlias> alias = new Lazy<SalesOrderDetailAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (CarrierTrackingNumber.HasValue) assignments.Add(new Assignment(alias.Value.CarrierTrackingNumber, CarrierTrackingNumber));
            if (LineTotal.HasValue) assignments.Add(new Assignment(alias.Value.LineTotal, LineTotal));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (OrderQty.HasValue) assignments.Add(new Assignment(alias.Value.OrderQty, OrderQty));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));
            if (UnitPrice.HasValue) assignments.Add(new Assignment(alias.Value.UnitPrice, UnitPrice));
            if (UnitPriceDiscount.HasValue) assignments.Add(new Assignment(alias.Value.UnitPriceDiscount, UnitPriceDiscount));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public SalesOrderDetailNode Alias(out SalesOrderDetailAlias alias)
        {
            if (NodeAlias is SalesOrderDetailAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new SalesOrderDetailAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public SalesOrderDetailNode Alias(out SalesOrderDetailAlias alias, string name)
        {
            if (NodeAlias is SalesOrderDetailAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new SalesOrderDetailAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public SalesOrderDetailNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public SalesOrderDetailIn  In  { get { return new SalesOrderDetailIn(this); } }
		public class SalesOrderDetailIn
		{
			private SalesOrderDetailNode Parent;
			internal SalesOrderDetailIn(SalesOrderDetailNode parent)
			{
				Parent = parent;
			}
			public IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL SALESORDERDETAIL_HAS_PRODUCT { get { return new SALESORDERDETAIL_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESORDERDETAIL_HAS_SALESORDERHEADER_REL SALESORDERDETAIL_HAS_SALESORDERHEADER { get { return new SALESORDERDETAIL_HAS_SALESORDERHEADER_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESORDERDETAIL_HAS_SPECIALOFFER_REL SALESORDERDETAIL_HAS_SPECIALOFFER { get { return new SALESORDERDETAIL_HAS_SPECIALOFFER_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class SalesOrderDetailAlias : AliasResult<SalesOrderDetailAlias, SalesOrderDetailListAlias>
	{
		internal SalesOrderDetailAlias(SalesOrderDetailNode parent)
		{
			Node = parent;
		}
		internal SalesOrderDetailAlias(SalesOrderDetailNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  SalesOrderDetailAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  SalesOrderDetailAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  SalesOrderDetailAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> CarrierTrackingNumber = default, JsNotation<string> LineTotal = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OrderQty = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default, JsNotation<double> UnitPrice = default, JsNotation<string> UnitPriceDiscount = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (CarrierTrackingNumber.HasValue) assignments.Add(new Assignment(this.CarrierTrackingNumber, CarrierTrackingNumber));
			if (LineTotal.HasValue) assignments.Add(new Assignment(this.LineTotal, LineTotal));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (OrderQty.HasValue) assignments.Add(new Assignment(this.OrderQty, OrderQty));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
			if (UnitPrice.HasValue) assignments.Add(new Assignment(this.UnitPrice, UnitPrice));
			if (UnitPriceDiscount.HasValue) assignments.Add(new Assignment(this.UnitPriceDiscount, UnitPriceDiscount));
            
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
						{ "CarrierTrackingNumber", new StringResult(this, "CarrierTrackingNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["CarrierTrackingNumber"]) },
						{ "OrderQty", new NumericResult(this, "OrderQty", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["OrderQty"]) },
						{ "UnitPrice", new FloatResult(this, "UnitPrice", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["UnitPrice"]) },
						{ "UnitPriceDiscount", new StringResult(this, "UnitPriceDiscount", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["UnitPriceDiscount"]) },
						{ "LineTotal", new StringResult(this, "LineTotal", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["LineTotal"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public SalesOrderDetailNode.SalesOrderDetailIn In { get { return new SalesOrderDetailNode.SalesOrderDetailIn(new SalesOrderDetailNode(this, true)); } }

		public StringResult CarrierTrackingNumber
		{
			get
			{
				if (m_CarrierTrackingNumber is null)
					m_CarrierTrackingNumber = (StringResult)AliasFields["CarrierTrackingNumber"];

				return m_CarrierTrackingNumber;
			}
		}
		private StringResult m_CarrierTrackingNumber = null;
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
		public StringResult UnitPriceDiscount
		{
			get
			{
				if (m_UnitPriceDiscount is null)
					m_UnitPriceDiscount = (StringResult)AliasFields["UnitPriceDiscount"];

				return m_UnitPriceDiscount;
			}
		}
		private StringResult m_UnitPriceDiscount = null;
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
		public StringResult rowguid
		{
			get
			{
				if (m_rowguid is null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		}
		private StringResult m_rowguid = null;
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
		public AsResult As(string aliasName, out SalesOrderDetailAlias alias)
		{
			alias = new SalesOrderDetailAlias((SalesOrderDetailNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class SalesOrderDetailListAlias : ListResult<SalesOrderDetailListAlias, SalesOrderDetailAlias>, IAliasListResult
	{
		private SalesOrderDetailListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesOrderDetailListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesOrderDetailListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class SalesOrderDetailJaggedListAlias : ListResult<SalesOrderDetailJaggedListAlias, SalesOrderDetailListAlias>, IAliasJaggedListResult
	{
		private SalesOrderDetailJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesOrderDetailJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesOrderDetailJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
