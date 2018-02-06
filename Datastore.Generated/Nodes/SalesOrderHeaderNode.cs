using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesOrderHeaderNode SalesOrderHeader { get { return new SalesOrderHeaderNode(); } }
	}

	public partial class SalesOrderHeaderNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesOrderHeader";
            }
        }

		internal SalesOrderHeaderNode() { }
		internal SalesOrderHeaderNode(SalesOrderHeaderAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesOrderHeaderNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesOrderHeaderNode Alias(out SalesOrderHeaderAlias alias)
		{
			alias = new SalesOrderHeaderAlias(this);
            NodeAlias = alias;
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

    public class SalesOrderHeaderAlias : AliasResult
    {
        internal SalesOrderHeaderAlias(SalesOrderHeaderNode parent)
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
				if ((object)m_RevisionNumber == null)
					m_RevisionNumber = (StringResult)AliasFields["RevisionNumber"];

				return m_RevisionNumber;
			}
		} 
        private StringResult m_RevisionNumber = null;
        public DateTimeResult OrderDate
		{
			get
			{
				if ((object)m_OrderDate == null)
					m_OrderDate = (DateTimeResult)AliasFields["OrderDate"];

				return m_OrderDate;
			}
		} 
        private DateTimeResult m_OrderDate = null;
        public DateTimeResult DueDate
		{
			get
			{
				if ((object)m_DueDate == null)
					m_DueDate = (DateTimeResult)AliasFields["DueDate"];

				return m_DueDate;
			}
		} 
        private DateTimeResult m_DueDate = null;
        public DateTimeResult ShipDate
		{
			get
			{
				if ((object)m_ShipDate == null)
					m_ShipDate = (DateTimeResult)AliasFields["ShipDate"];

				return m_ShipDate;
			}
		} 
        private DateTimeResult m_ShipDate = null;
        public StringResult Status
		{
			get
			{
				if ((object)m_Status == null)
					m_Status = (StringResult)AliasFields["Status"];

				return m_Status;
			}
		} 
        private StringResult m_Status = null;
        public StringResult OnlineOrderFlag
		{
			get
			{
				if ((object)m_OnlineOrderFlag == null)
					m_OnlineOrderFlag = (StringResult)AliasFields["OnlineOrderFlag"];

				return m_OnlineOrderFlag;
			}
		} 
        private StringResult m_OnlineOrderFlag = null;
        public StringResult SalesOrderNumber
		{
			get
			{
				if ((object)m_SalesOrderNumber == null)
					m_SalesOrderNumber = (StringResult)AliasFields["SalesOrderNumber"];

				return m_SalesOrderNumber;
			}
		} 
        private StringResult m_SalesOrderNumber = null;
        public StringResult PurchaseOrderNumber
		{
			get
			{
				if ((object)m_PurchaseOrderNumber == null)
					m_PurchaseOrderNumber = (StringResult)AliasFields["PurchaseOrderNumber"];

				return m_PurchaseOrderNumber;
			}
		} 
        private StringResult m_PurchaseOrderNumber = null;
        public StringResult AccountNumber
		{
			get
			{
				if ((object)m_AccountNumber == null)
					m_AccountNumber = (StringResult)AliasFields["AccountNumber"];

				return m_AccountNumber;
			}
		} 
        private StringResult m_AccountNumber = null;
        public NumericResult CreditCardID
		{
			get
			{
				if ((object)m_CreditCardID == null)
					m_CreditCardID = (NumericResult)AliasFields["CreditCardID"];

				return m_CreditCardID;
			}
		} 
        private NumericResult m_CreditCardID = null;
        public StringResult CreditCardApprovalCode
		{
			get
			{
				if ((object)m_CreditCardApprovalCode == null)
					m_CreditCardApprovalCode = (StringResult)AliasFields["CreditCardApprovalCode"];

				return m_CreditCardApprovalCode;
			}
		} 
        private StringResult m_CreditCardApprovalCode = null;
        public NumericResult CurrencyRateID
		{
			get
			{
				if ((object)m_CurrencyRateID == null)
					m_CurrencyRateID = (NumericResult)AliasFields["CurrencyRateID"];

				return m_CurrencyRateID;
			}
		} 
        private NumericResult m_CurrencyRateID = null;
        public StringResult SubTotal
		{
			get
			{
				if ((object)m_SubTotal == null)
					m_SubTotal = (StringResult)AliasFields["SubTotal"];

				return m_SubTotal;
			}
		} 
        private StringResult m_SubTotal = null;
        public StringResult TaxAmt
		{
			get
			{
				if ((object)m_TaxAmt == null)
					m_TaxAmt = (StringResult)AliasFields["TaxAmt"];

				return m_TaxAmt;
			}
		} 
        private StringResult m_TaxAmt = null;
        public StringResult Freight
		{
			get
			{
				if ((object)m_Freight == null)
					m_Freight = (StringResult)AliasFields["Freight"];

				return m_Freight;
			}
		} 
        private StringResult m_Freight = null;
        public StringResult TotalDue
		{
			get
			{
				if ((object)m_TotalDue == null)
					m_TotalDue = (StringResult)AliasFields["TotalDue"];

				return m_TotalDue;
			}
		} 
        private StringResult m_TotalDue = null;
        public StringResult Comment
		{
			get
			{
				if ((object)m_Comment == null)
					m_Comment = (StringResult)AliasFields["Comment"];

				return m_Comment;
			}
		} 
        private StringResult m_Comment = null;
        public StringResult rowguid
		{
			get
			{
				if ((object)m_rowguid == null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		} 
        private StringResult m_rowguid = null;
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
