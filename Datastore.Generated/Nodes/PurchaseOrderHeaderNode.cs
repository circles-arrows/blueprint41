
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static PurchaseOrderHeaderNode PurchaseOrderHeader { get { return new PurchaseOrderHeaderNode(); } }
	}

	public partial class PurchaseOrderHeaderNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "PurchaseOrderHeader";
            }
        }

		internal PurchaseOrderHeaderNode() { }
		internal PurchaseOrderHeaderNode(PurchaseOrderHeaderAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PurchaseOrderHeaderNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public PurchaseOrderHeaderNode Alias(out PurchaseOrderHeaderAlias alias)
		{
			alias = new PurchaseOrderHeaderAlias(this);
            NodeAlias = alias;
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

    public class PurchaseOrderHeaderAlias : AliasResult
    {
        internal PurchaseOrderHeaderAlias(PurchaseOrderHeaderNode parent)
        {
			Node = parent;
            RevisionNumber = new StringResult(this, "RevisionNumber", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["RevisionNumber"]);
            Status = new StringResult(this, "Status", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["Status"]);
            OrderDate = new DateTimeResult(this, "OrderDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["OrderDate"]);
            ShipDate = new DateTimeResult(this, "ShipDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["ShipDate"]);
            SubTotal = new FloatResult(this, "SubTotal", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["SubTotal"]);
            TaxAmt = new FloatResult(this, "TaxAmt", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["TaxAmt"]);
            Freight = new StringResult(this, "Freight", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["Freight"]);
            TotalDue = new FloatResult(this, "TotalDue", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"].Properties["TotalDue"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public PurchaseOrderHeaderNode.PurchaseOrderHeaderIn In { get { return new PurchaseOrderHeaderNode.PurchaseOrderHeaderIn(new PurchaseOrderHeaderNode(this, true)); } }
        public PurchaseOrderHeaderNode.PurchaseOrderHeaderOut Out { get { return new PurchaseOrderHeaderNode.PurchaseOrderHeaderOut(new PurchaseOrderHeaderNode(this, true)); } }

        public StringResult RevisionNumber { get; private set; } 
        public StringResult Status { get; private set; } 
        public DateTimeResult OrderDate { get; private set; } 
        public DateTimeResult ShipDate { get; private set; } 
        public FloatResult SubTotal { get; private set; } 
        public FloatResult TaxAmt { get; private set; } 
        public StringResult Freight { get; private set; } 
        public FloatResult TotalDue { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
