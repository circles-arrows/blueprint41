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
		public static SalesOrderHeaderNode SalesOrderHeader { get { return new SalesOrderHeaderNode(); } }
	}

	public partial class SalesOrderHeaderNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(SalesOrderHeaderNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(SalesOrderHeaderNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "SalesOrderHeader";
		}

		protected override Entity GetEntity()
        {
			return m.SalesOrderHeader.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.SalesOrderHeader.Entity.FunctionalId;
            }
        }

		internal SalesOrderHeaderNode() { }
		internal SalesOrderHeaderNode(SalesOrderHeaderAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesOrderHeaderNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal SalesOrderHeaderNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public SalesOrderHeaderNode Where(JsNotation<string> AccountNumber = default, JsNotation<string> Comment = default, JsNotation<string> CreditCardApprovalCode = default, JsNotation<int?> CreditCardID = default, JsNotation<int?> CurrencyRateID = default, JsNotation<System.DateTime> DueDate = default, JsNotation<string> Freight = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> OnlineOrderFlag = default, JsNotation<System.DateTime> OrderDate = default, JsNotation<string> PurchaseOrderNumber = default, JsNotation<string> RevisionNumber = default, JsNotation<string> rowguid = default, JsNotation<string> SalesOrderNumber = default, JsNotation<System.DateTime?> ShipDate = default, JsNotation<string> Status = default, JsNotation<string> SubTotal = default, JsNotation<string> TaxAmt = default, JsNotation<string> TotalDue = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesOrderHeaderAlias> alias = new Lazy<SalesOrderHeaderAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (AccountNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.AccountNumber, Operator.Equals, ((IValue)AccountNumber).GetValue()));
            if (Comment.HasValue) conditions.Add(new QueryCondition(alias.Value.Comment, Operator.Equals, ((IValue)Comment).GetValue()));
            if (CreditCardApprovalCode.HasValue) conditions.Add(new QueryCondition(alias.Value.CreditCardApprovalCode, Operator.Equals, ((IValue)CreditCardApprovalCode).GetValue()));
            if (CreditCardID.HasValue) conditions.Add(new QueryCondition(alias.Value.CreditCardID, Operator.Equals, ((IValue)CreditCardID).GetValue()));
            if (CurrencyRateID.HasValue) conditions.Add(new QueryCondition(alias.Value.CurrencyRateID, Operator.Equals, ((IValue)CurrencyRateID).GetValue()));
            if (DueDate.HasValue) conditions.Add(new QueryCondition(alias.Value.DueDate, Operator.Equals, ((IValue)DueDate).GetValue()));
            if (Freight.HasValue) conditions.Add(new QueryCondition(alias.Value.Freight, Operator.Equals, ((IValue)Freight).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (OnlineOrderFlag.HasValue) conditions.Add(new QueryCondition(alias.Value.OnlineOrderFlag, Operator.Equals, ((IValue)OnlineOrderFlag).GetValue()));
            if (OrderDate.HasValue) conditions.Add(new QueryCondition(alias.Value.OrderDate, Operator.Equals, ((IValue)OrderDate).GetValue()));
            if (PurchaseOrderNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.PurchaseOrderNumber, Operator.Equals, ((IValue)PurchaseOrderNumber).GetValue()));
            if (RevisionNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.RevisionNumber, Operator.Equals, ((IValue)RevisionNumber).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (SalesOrderNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.SalesOrderNumber, Operator.Equals, ((IValue)SalesOrderNumber).GetValue()));
            if (ShipDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ShipDate, Operator.Equals, ((IValue)ShipDate).GetValue()));
            if (Status.HasValue) conditions.Add(new QueryCondition(alias.Value.Status, Operator.Equals, ((IValue)Status).GetValue()));
            if (SubTotal.HasValue) conditions.Add(new QueryCondition(alias.Value.SubTotal, Operator.Equals, ((IValue)SubTotal).GetValue()));
            if (TaxAmt.HasValue) conditions.Add(new QueryCondition(alias.Value.TaxAmt, Operator.Equals, ((IValue)TaxAmt).GetValue()));
            if (TotalDue.HasValue) conditions.Add(new QueryCondition(alias.Value.TotalDue, Operator.Equals, ((IValue)TotalDue).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public SalesOrderHeaderNode Assign(JsNotation<string> AccountNumber = default, JsNotation<string> Comment = default, JsNotation<string> CreditCardApprovalCode = default, JsNotation<int?> CreditCardID = default, JsNotation<int?> CurrencyRateID = default, JsNotation<System.DateTime> DueDate = default, JsNotation<string> Freight = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> OnlineOrderFlag = default, JsNotation<System.DateTime> OrderDate = default, JsNotation<string> PurchaseOrderNumber = default, JsNotation<string> RevisionNumber = default, JsNotation<string> rowguid = default, JsNotation<string> SalesOrderNumber = default, JsNotation<System.DateTime?> ShipDate = default, JsNotation<string> Status = default, JsNotation<string> SubTotal = default, JsNotation<string> TaxAmt = default, JsNotation<string> TotalDue = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesOrderHeaderAlias> alias = new Lazy<SalesOrderHeaderAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (AccountNumber.HasValue) assignments.Add(new Assignment(alias.Value.AccountNumber, AccountNumber));
            if (Comment.HasValue) assignments.Add(new Assignment(alias.Value.Comment, Comment));
            if (CreditCardApprovalCode.HasValue) assignments.Add(new Assignment(alias.Value.CreditCardApprovalCode, CreditCardApprovalCode));
            if (CreditCardID.HasValue) assignments.Add(new Assignment(alias.Value.CreditCardID, CreditCardID));
            if (CurrencyRateID.HasValue) assignments.Add(new Assignment(alias.Value.CurrencyRateID, CurrencyRateID));
            if (DueDate.HasValue) assignments.Add(new Assignment(alias.Value.DueDate, DueDate));
            if (Freight.HasValue) assignments.Add(new Assignment(alias.Value.Freight, Freight));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (OnlineOrderFlag.HasValue) assignments.Add(new Assignment(alias.Value.OnlineOrderFlag, OnlineOrderFlag));
            if (OrderDate.HasValue) assignments.Add(new Assignment(alias.Value.OrderDate, OrderDate));
            if (PurchaseOrderNumber.HasValue) assignments.Add(new Assignment(alias.Value.PurchaseOrderNumber, PurchaseOrderNumber));
            if (RevisionNumber.HasValue) assignments.Add(new Assignment(alias.Value.RevisionNumber, RevisionNumber));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (SalesOrderNumber.HasValue) assignments.Add(new Assignment(alias.Value.SalesOrderNumber, SalesOrderNumber));
            if (ShipDate.HasValue) assignments.Add(new Assignment(alias.Value.ShipDate, ShipDate));
            if (Status.HasValue) assignments.Add(new Assignment(alias.Value.Status, Status));
            if (SubTotal.HasValue) assignments.Add(new Assignment(alias.Value.SubTotal, SubTotal));
            if (TaxAmt.HasValue) assignments.Add(new Assignment(alias.Value.TaxAmt, TaxAmt));
            if (TotalDue.HasValue) assignments.Add(new Assignment(alias.Value.TotalDue, TotalDue));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public SalesOrderHeaderNode Alias(out SalesOrderHeaderAlias alias)
        {
            if (NodeAlias is SalesOrderHeaderAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new SalesOrderHeaderAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public SalesOrderHeaderNode Alias(out SalesOrderHeaderAlias alias, string name)
        {
            if (NodeAlias is SalesOrderHeaderAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new SalesOrderHeaderAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public SalesOrderHeaderNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public SalesOrderHeaderIn  In  { get { return new SalesOrderHeaderIn(this); } }
		public class SalesOrderHeaderIn
		{
			private SalesOrderHeaderNode Parent;
			internal SalesOrderHeaderIn(SalesOrderHeaderNode parent)
			{
				Parent = parent;
			}
			public IFromIn_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL SALESORDERHEADER_CONTAINS_SALESTERRITORY { get { return new SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL SALESORDERHEADER_HAS_ADDRESS { get { return new SALESORDERHEADER_HAS_ADDRESS_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESORDERHEADER_HAS_CREDITCARD_REL SALESORDERHEADER_HAS_CREDITCARD { get { return new SALESORDERHEADER_HAS_CREDITCARD_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESORDERHEADER_HAS_CURRENCYRATE_REL SALESORDERHEADER_HAS_CURRENCYRATE { get { return new SALESORDERHEADER_HAS_CURRENCYRATE_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESORDERHEADER_HAS_SALESREASON_REL SALESORDERHEADER_HAS_SALESREASON { get { return new SALESORDERHEADER_HAS_SALESREASON_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESORDERHEADER_HAS_SHIPMETHOD_REL SALESORDERHEADER_HAS_SHIPMETHOD { get { return new SALESORDERHEADER_HAS_SHIPMETHOD_REL(Parent, DirectionEnum.In); } }

		}

		public SalesOrderHeaderOut Out { get { return new SalesOrderHeaderOut(this); } }
		public class SalesOrderHeaderOut
		{
			private SalesOrderHeaderNode Parent;
			internal SalesOrderHeaderOut(SalesOrderHeaderNode parent)
			{
				Parent = parent;
			}
			public IFromOut_SALESORDERDETAIL_HAS_SALESORDERHEADER_REL SALESORDERDETAIL_HAS_SALESORDERHEADER { get { return new SALESORDERDETAIL_HAS_SALESORDERHEADER_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class SalesOrderHeaderAlias : AliasResult<SalesOrderHeaderAlias, SalesOrderHeaderListAlias>
	{
		internal SalesOrderHeaderAlias(SalesOrderHeaderNode parent)
		{
			Node = parent;
		}
		internal SalesOrderHeaderAlias(SalesOrderHeaderNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  SalesOrderHeaderAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  SalesOrderHeaderAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  SalesOrderHeaderAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> AccountNumber = default, JsNotation<string> Comment = default, JsNotation<string> CreditCardApprovalCode = default, JsNotation<int?> CreditCardID = default, JsNotation<int?> CurrencyRateID = default, JsNotation<System.DateTime> DueDate = default, JsNotation<string> Freight = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> OnlineOrderFlag = default, JsNotation<System.DateTime> OrderDate = default, JsNotation<string> PurchaseOrderNumber = default, JsNotation<string> RevisionNumber = default, JsNotation<string> rowguid = default, JsNotation<string> SalesOrderNumber = default, JsNotation<System.DateTime?> ShipDate = default, JsNotation<string> Status = default, JsNotation<string> SubTotal = default, JsNotation<string> TaxAmt = default, JsNotation<string> TotalDue = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (AccountNumber.HasValue) assignments.Add(new Assignment(this.AccountNumber, AccountNumber));
			if (Comment.HasValue) assignments.Add(new Assignment(this.Comment, Comment));
			if (CreditCardApprovalCode.HasValue) assignments.Add(new Assignment(this.CreditCardApprovalCode, CreditCardApprovalCode));
			if (CreditCardID.HasValue) assignments.Add(new Assignment(this.CreditCardID, CreditCardID));
			if (CurrencyRateID.HasValue) assignments.Add(new Assignment(this.CurrencyRateID, CurrencyRateID));
			if (DueDate.HasValue) assignments.Add(new Assignment(this.DueDate, DueDate));
			if (Freight.HasValue) assignments.Add(new Assignment(this.Freight, Freight));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (OnlineOrderFlag.HasValue) assignments.Add(new Assignment(this.OnlineOrderFlag, OnlineOrderFlag));
			if (OrderDate.HasValue) assignments.Add(new Assignment(this.OrderDate, OrderDate));
			if (PurchaseOrderNumber.HasValue) assignments.Add(new Assignment(this.PurchaseOrderNumber, PurchaseOrderNumber));
			if (RevisionNumber.HasValue) assignments.Add(new Assignment(this.RevisionNumber, RevisionNumber));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (SalesOrderNumber.HasValue) assignments.Add(new Assignment(this.SalesOrderNumber, SalesOrderNumber));
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
						{ "RevisionNumber", new StringResult(this, "RevisionNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["RevisionNumber"]) },
						{ "OrderDate", new DateTimeResult(this, "OrderDate", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["OrderDate"]) },
						{ "DueDate", new DateTimeResult(this, "DueDate", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["DueDate"]) },
						{ "ShipDate", new DateTimeResult(this, "ShipDate", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["ShipDate"]) },
						{ "Status", new StringResult(this, "Status", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["Status"]) },
						{ "OnlineOrderFlag", new StringResult(this, "OnlineOrderFlag", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["OnlineOrderFlag"]) },
						{ "SalesOrderNumber", new StringResult(this, "SalesOrderNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["SalesOrderNumber"]) },
						{ "PurchaseOrderNumber", new StringResult(this, "PurchaseOrderNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["PurchaseOrderNumber"]) },
						{ "AccountNumber", new StringResult(this, "AccountNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["AccountNumber"]) },
						{ "CreditCardID", new NumericResult(this, "CreditCardID", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CreditCardID"]) },
						{ "CreditCardApprovalCode", new StringResult(this, "CreditCardApprovalCode", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CreditCardApprovalCode"]) },
						{ "CurrencyRateID", new NumericResult(this, "CurrencyRateID", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CurrencyRateID"]) },
						{ "SubTotal", new StringResult(this, "SubTotal", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["SubTotal"]) },
						{ "TaxAmt", new StringResult(this, "TaxAmt", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["TaxAmt"]) },
						{ "Freight", new StringResult(this, "Freight", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["Freight"]) },
						{ "TotalDue", new StringResult(this, "TotalDue", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["TotalDue"]) },
						{ "Comment", new StringResult(this, "Comment", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["Comment"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public SalesOrderHeaderNode.SalesOrderHeaderIn In { get { return new SalesOrderHeaderNode.SalesOrderHeaderIn(new SalesOrderHeaderNode(this, true)); } }
		public SalesOrderHeaderNode.SalesOrderHeaderOut Out { get { return new SalesOrderHeaderNode.SalesOrderHeaderOut(new SalesOrderHeaderNode(this, true)); } }

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
		public StringResult OnlineOrderFlag
		{
			get
			{
				if (m_OnlineOrderFlag is null)
					m_OnlineOrderFlag = (StringResult)AliasFields["OnlineOrderFlag"];

				return m_OnlineOrderFlag;
			}
		}
		private StringResult m_OnlineOrderFlag = null;
		public StringResult SalesOrderNumber
		{
			get
			{
				if (m_SalesOrderNumber is null)
					m_SalesOrderNumber = (StringResult)AliasFields["SalesOrderNumber"];

				return m_SalesOrderNumber;
			}
		}
		private StringResult m_SalesOrderNumber = null;
		public StringResult PurchaseOrderNumber
		{
			get
			{
				if (m_PurchaseOrderNumber is null)
					m_PurchaseOrderNumber = (StringResult)AliasFields["PurchaseOrderNumber"];

				return m_PurchaseOrderNumber;
			}
		}
		private StringResult m_PurchaseOrderNumber = null;
		public StringResult AccountNumber
		{
			get
			{
				if (m_AccountNumber is null)
					m_AccountNumber = (StringResult)AliasFields["AccountNumber"];

				return m_AccountNumber;
			}
		}
		private StringResult m_AccountNumber = null;
		public NumericResult CreditCardID
		{
			get
			{
				if (m_CreditCardID is null)
					m_CreditCardID = (NumericResult)AliasFields["CreditCardID"];

				return m_CreditCardID;
			}
		}
		private NumericResult m_CreditCardID = null;
		public StringResult CreditCardApprovalCode
		{
			get
			{
				if (m_CreditCardApprovalCode is null)
					m_CreditCardApprovalCode = (StringResult)AliasFields["CreditCardApprovalCode"];

				return m_CreditCardApprovalCode;
			}
		}
		private StringResult m_CreditCardApprovalCode = null;
		public NumericResult CurrencyRateID
		{
			get
			{
				if (m_CurrencyRateID is null)
					m_CurrencyRateID = (NumericResult)AliasFields["CurrencyRateID"];

				return m_CurrencyRateID;
			}
		}
		private NumericResult m_CurrencyRateID = null;
		public StringResult SubTotal
		{
			get
			{
				if (m_SubTotal is null)
					m_SubTotal = (StringResult)AliasFields["SubTotal"];

				return m_SubTotal;
			}
		}
		private StringResult m_SubTotal = null;
		public StringResult TaxAmt
		{
			get
			{
				if (m_TaxAmt is null)
					m_TaxAmt = (StringResult)AliasFields["TaxAmt"];

				return m_TaxAmt;
			}
		}
		private StringResult m_TaxAmt = null;
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
		public StringResult TotalDue
		{
			get
			{
				if (m_TotalDue is null)
					m_TotalDue = (StringResult)AliasFields["TotalDue"];

				return m_TotalDue;
			}
		}
		private StringResult m_TotalDue = null;
		public StringResult Comment
		{
			get
			{
				if (m_Comment is null)
					m_Comment = (StringResult)AliasFields["Comment"];

				return m_Comment;
			}
		}
		private StringResult m_Comment = null;
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
		public AsResult As(string aliasName, out SalesOrderHeaderAlias alias)
		{
			alias = new SalesOrderHeaderAlias((SalesOrderHeaderNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class SalesOrderHeaderListAlias : ListResult<SalesOrderHeaderListAlias, SalesOrderHeaderAlias>, IAliasListResult
	{
		private SalesOrderHeaderListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesOrderHeaderListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesOrderHeaderListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class SalesOrderHeaderJaggedListAlias : ListResult<SalesOrderHeaderJaggedListAlias, SalesOrderHeaderListAlias>, IAliasJaggedListResult
	{
		private SalesOrderHeaderJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesOrderHeaderJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesOrderHeaderJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
