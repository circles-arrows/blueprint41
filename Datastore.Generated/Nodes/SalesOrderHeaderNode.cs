using System;
using Blueprint41;
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
            RevisionNumber = new StringResult(this, "RevisionNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["RevisionNumber"]);
            OrderDate = new DateTimeResult(this, "OrderDate", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["OrderDate"]);
            DueDate = new DateTimeResult(this, "DueDate", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["DueDate"]);
            ShipDate = new DateTimeResult(this, "ShipDate", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["ShipDate"]);
            Status = new StringResult(this, "Status", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["Status"]);
            OnlineOrderFlag = new StringResult(this, "OnlineOrderFlag", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["OnlineOrderFlag"]);
            SalesOrderNumber = new StringResult(this, "SalesOrderNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["SalesOrderNumber"]);
            PurchaseOrderNumber = new StringResult(this, "PurchaseOrderNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["PurchaseOrderNumber"]);
            AccountNumber = new StringResult(this, "AccountNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["AccountNumber"]);
            CreditCardID = new NumericResult(this, "CreditCardID", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CreditCardID"]);
            CreditCardApprovalCode = new StringResult(this, "CreditCardApprovalCode", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CreditCardApprovalCode"]);
            CurrencyRateID = new NumericResult(this, "CurrencyRateID", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["CurrencyRateID"]);
            SubTotal = new StringResult(this, "SubTotal", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["SubTotal"]);
            TaxAmt = new StringResult(this, "TaxAmt", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["TaxAmt"]);
            Freight = new StringResult(this, "Freight", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["Freight"]);
            TotalDue = new StringResult(this, "TotalDue", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["TotalDue"]);
            Comment = new StringResult(this, "Comment", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["Comment"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public SalesOrderHeaderNode.SalesOrderHeaderIn In { get { return new SalesOrderHeaderNode.SalesOrderHeaderIn(new SalesOrderHeaderNode(this, true)); } }
        public SalesOrderHeaderNode.SalesOrderHeaderOut Out { get { return new SalesOrderHeaderNode.SalesOrderHeaderOut(new SalesOrderHeaderNode(this, true)); } }

        public StringResult RevisionNumber { get; private set; } 
        public DateTimeResult OrderDate { get; private set; } 
        public DateTimeResult DueDate { get; private set; } 
        public DateTimeResult ShipDate { get; private set; } 
        public StringResult Status { get; private set; } 
        public StringResult OnlineOrderFlag { get; private set; } 
        public StringResult SalesOrderNumber { get; private set; } 
        public StringResult PurchaseOrderNumber { get; private set; } 
        public StringResult AccountNumber { get; private set; } 
        public NumericResult CreditCardID { get; private set; } 
        public StringResult CreditCardApprovalCode { get; private set; } 
        public NumericResult CurrencyRateID { get; private set; } 
        public StringResult SubTotal { get; private set; } 
        public StringResult TaxAmt { get; private set; } 
        public StringResult Freight { get; private set; } 
        public StringResult TotalDue { get; private set; } 
        public StringResult Comment { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
