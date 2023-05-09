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
		public static PurchaseOrderHeaderNode PurchaseOrderHeader { get { return new PurchaseOrderHeaderNode(); } }
	}

	public partial class PurchaseOrderHeaderNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(PurchaseOrderHeaderNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(PurchaseOrderHeaderNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "PurchaseOrderHeader";
		}

		protected override Entity GetEntity()
        {
			return m.PurchaseOrderHeader.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.PurchaseOrderHeader.Entity.FunctionalId;
            }
        }

		internal PurchaseOrderHeaderNode() { }
		internal PurchaseOrderHeaderNode(PurchaseOrderHeaderAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PurchaseOrderHeaderNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal PurchaseOrderHeaderNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public PurchaseOrderHeaderNode Where(JsNotation<string> Freight = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> OrderDate = default, JsNotation<string> RevisionNumber = default, JsNotation<System.DateTime> ShipDate = default, JsNotation<string> Status = default, JsNotation<double> SubTotal = default, JsNotation<double> TaxAmt = default, JsNotation<double> TotalDue = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PurchaseOrderHeaderAlias> alias = new Lazy<PurchaseOrderHeaderAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Freight.HasValue) conditions.Add(new QueryCondition(alias.Value.Freight, Operator.Equals, ((IValue)Freight).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (OrderDate.HasValue) conditions.Add(new QueryCondition(alias.Value.OrderDate, Operator.Equals, ((IValue)OrderDate).GetValue()));
            if (RevisionNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.RevisionNumber, Operator.Equals, ((IValue)RevisionNumber).GetValue()));
            if (ShipDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ShipDate, Operator.Equals, ((IValue)ShipDate).GetValue()));
            if (Status.HasValue) conditions.Add(new QueryCondition(alias.Value.Status, Operator.Equals, ((IValue)Status).GetValue()));
            if (SubTotal.HasValue) conditions.Add(new QueryCondition(alias.Value.SubTotal, Operator.Equals, ((IValue)SubTotal).GetValue()));
            if (TaxAmt.HasValue) conditions.Add(new QueryCondition(alias.Value.TaxAmt, Operator.Equals, ((IValue)TaxAmt).GetValue()));
            if (TotalDue.HasValue) conditions.Add(new QueryCondition(alias.Value.TotalDue, Operator.Equals, ((IValue)TotalDue).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public PurchaseOrderHeaderNode Assign(JsNotation<string> Freight = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> OrderDate = default, JsNotation<string> RevisionNumber = default, JsNotation<System.DateTime> ShipDate = default, JsNotation<string> Status = default, JsNotation<double> SubTotal = default, JsNotation<double> TaxAmt = default, JsNotation<double> TotalDue = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PurchaseOrderHeaderAlias> alias = new Lazy<PurchaseOrderHeaderAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Freight.HasValue) assignments.Add(new Assignment(alias.Value.Freight, Freight));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (OrderDate.HasValue) assignments.Add(new Assignment(alias.Value.OrderDate, OrderDate));
            if (RevisionNumber.HasValue) assignments.Add(new Assignment(alias.Value.RevisionNumber, RevisionNumber));
            if (ShipDate.HasValue) assignments.Add(new Assignment(alias.Value.ShipDate, ShipDate));
            if (Status.HasValue) assignments.Add(new Assignment(alias.Value.Status, Status));
            if (SubTotal.HasValue) assignments.Add(new Assignment(alias.Value.SubTotal, SubTotal));
            if (TaxAmt.HasValue) assignments.Add(new Assignment(alias.Value.TaxAmt, TaxAmt));
            if (TotalDue.HasValue) assignments.Add(new Assignment(alias.Value.TotalDue, TotalDue));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public PurchaseOrderHeaderNode Alias(out PurchaseOrderHeaderAlias alias)
        {
            if (NodeAlias is PurchaseOrderHeaderAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new PurchaseOrderHeaderAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public PurchaseOrderHeaderNode Alias(out PurchaseOrderHeaderAlias alias, string name)
        {
            if (NodeAlias is PurchaseOrderHeaderAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new PurchaseOrderHeaderAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public PurchaseOrderHeaderNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public PurchaseOrderHeaderIn  In  { get { return new PurchaseOrderHeaderIn(this); } }
		public class PurchaseOrderHeaderIn
		{
			private PurchaseOrderHeaderNode Parent;
			internal PurchaseOrderHeaderIn(PurchaseOrderHeaderNode parent)
			{
				Parent = parent;
			}
			public IFromIn_PURCHASEORDERHEADER_HAS_SHIPMETHOD_REL PURCHASEORDERHEADER_HAS_SHIPMETHOD { get { return new PURCHASEORDERHEADER_HAS_SHIPMETHOD_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL PURCHASEORDERHEADER_HAS_VENDOR { get { return new PURCHASEORDERHEADER_HAS_VENDOR_REL(Parent, DirectionEnum.In); } }

		}

		public PurchaseOrderHeaderOut Out { get { return new PurchaseOrderHeaderOut(this); } }
		public class PurchaseOrderHeaderOut
		{
			private PurchaseOrderHeaderNode Parent;
			internal PurchaseOrderHeaderOut(PurchaseOrderHeaderNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER_REL PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER { get { return new PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class PurchaseOrderHeaderAlias : AliasResult<PurchaseOrderHeaderAlias, PurchaseOrderHeaderListAlias>
	{
		internal PurchaseOrderHeaderAlias(PurchaseOrderHeaderNode parent)
		{
			Node = parent;
		}
		internal PurchaseOrderHeaderAlias(PurchaseOrderHeaderNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  PurchaseOrderHeaderAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  PurchaseOrderHeaderAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  PurchaseOrderHeaderAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Freight = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> OrderDate = default, JsNotation<string> RevisionNumber = default, JsNotation<System.DateTime> ShipDate = default, JsNotation<string> Status = default, JsNotation<double> SubTotal = default, JsNotation<double> TaxAmt = default, JsNotation<double> TotalDue = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Freight.HasValue) assignments.Add(new Assignment(this.Freight, Freight));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (OrderDate.HasValue) assignments.Add(new Assignment(this.OrderDate, OrderDate));
			if (RevisionNumber.HasValue) assignments.Add(new Assignment(this.RevisionNumber, RevisionNumber));
			if (ShipDate.HasValue) assignments.Add(new Assignment(this.ShipDate, ShipDate));
			if (Status.HasValue) assignments.Add(new Assignment(this.Status, Status));
			if (SubTotal.HasValue) assignments.Add(new Assignment(this.SubTotal, SubTotal));
			if (TaxAmt.HasValue) assignments.Add(new Assignment(this.TaxAmt, TaxAmt));
			if (TotalDue.HasValue) assignments.Add(new Assignment(this.TotalDue, TotalDue));
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
						{ "RevisionNumber", new StringResult(this, "RevisionNumber", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["RevisionNumber"]) },
						{ "Status", new StringResult(this, "Status", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["Status"]) },
						{ "OrderDate", new DateTimeResult(this, "OrderDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["OrderDate"]) },
						{ "ShipDate", new DateTimeResult(this, "ShipDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["ShipDate"]) },
						{ "SubTotal", new FloatResult(this, "SubTotal", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["SubTotal"]) },
						{ "TaxAmt", new FloatResult(this, "TaxAmt", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["TaxAmt"]) },
						{ "Freight", new StringResult(this, "Freight", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["Freight"]) },
						{ "TotalDue", new FloatResult(this, "TotalDue", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["TotalDue"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public PurchaseOrderHeaderNode.PurchaseOrderHeaderIn In { get { return new PurchaseOrderHeaderNode.PurchaseOrderHeaderIn(new PurchaseOrderHeaderNode(this, true)); } }
		public PurchaseOrderHeaderNode.PurchaseOrderHeaderOut Out { get { return new PurchaseOrderHeaderNode.PurchaseOrderHeaderOut(new PurchaseOrderHeaderNode(this, true)); } }

		public StringResult RevisionNumber
		{
			get
			{
				if (m_RevisionNumber is null)
					m_RevisionNumber = (StringResult)AliasFields["RevisionNumber"];

				return m_RevisionNumber;
			}
		}
		private StringResult m_RevisionNumber = null;
		public StringResult Status
		{
			get
			{
				if (m_Status is null)
					m_Status = (StringResult)AliasFields["Status"];

				return m_Status;
			}
		}
		private StringResult m_Status = null;
		public DateTimeResult OrderDate
		{
			get
			{
				if (m_OrderDate is null)
					m_OrderDate = (DateTimeResult)AliasFields["OrderDate"];

				return m_OrderDate;
			}
		}
		private DateTimeResult m_OrderDate = null;
		public DateTimeResult ShipDate
		{
			get
			{
				if (m_ShipDate is null)
					m_ShipDate = (DateTimeResult)AliasFields["ShipDate"];

				return m_ShipDate;
			}
		}
		private DateTimeResult m_ShipDate = null;
		public FloatResult SubTotal
		{
			get
			{
				if (m_SubTotal is null)
					m_SubTotal = (FloatResult)AliasFields["SubTotal"];

				return m_SubTotal;
			}
		}
		private FloatResult m_SubTotal = null;
		public FloatResult TaxAmt
		{
			get
			{
				if (m_TaxAmt is null)
					m_TaxAmt = (FloatResult)AliasFields["TaxAmt"];

				return m_TaxAmt;
			}
		}
		private FloatResult m_TaxAmt = null;
		public StringResult Freight
		{
			get
			{
				if (m_Freight is null)
					m_Freight = (StringResult)AliasFields["Freight"];

				return m_Freight;
			}
		}
		private StringResult m_Freight = null;
		public FloatResult TotalDue
		{
			get
			{
				if (m_TotalDue is null)
					m_TotalDue = (FloatResult)AliasFields["TotalDue"];

				return m_TotalDue;
			}
		}
		private FloatResult m_TotalDue = null;
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
		public AsResult As(string aliasName, out PurchaseOrderHeaderAlias alias)
		{
			alias = new PurchaseOrderHeaderAlias((PurchaseOrderHeaderNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class PurchaseOrderHeaderListAlias : ListResult<PurchaseOrderHeaderListAlias, PurchaseOrderHeaderAlias>, IAliasListResult
	{
		private PurchaseOrderHeaderListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PurchaseOrderHeaderListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PurchaseOrderHeaderListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class PurchaseOrderHeaderJaggedListAlias : ListResult<PurchaseOrderHeaderJaggedListAlias, PurchaseOrderHeaderListAlias>, IAliasJaggedListResult
	{
		private PurchaseOrderHeaderJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PurchaseOrderHeaderJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PurchaseOrderHeaderJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
