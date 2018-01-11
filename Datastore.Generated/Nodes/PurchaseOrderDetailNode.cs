using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static PurchaseOrderDetailNode PurchaseOrderDetail { get { return new PurchaseOrderDetailNode(); } }
	}

	public partial class PurchaseOrderDetailNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "PurchaseOrderDetail";
            }
        }

		internal PurchaseOrderDetailNode() { }
		internal PurchaseOrderDetailNode(PurchaseOrderDetailAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PurchaseOrderDetailNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public PurchaseOrderDetailNode Alias(out PurchaseOrderDetailAlias alias)
		{
			alias = new PurchaseOrderDetailAlias(this);
            NodeAlias = alias;
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

    public class PurchaseOrderDetailAlias : AliasResult
    {
        internal PurchaseOrderDetailAlias(PurchaseOrderDetailNode parent)
        {
			Node = parent;
            DueDate = new DateTimeResult(this, "DueDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["DueDate"]);
            OrderQty = new NumericResult(this, "OrderQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["OrderQty"]);
            UnitPrice = new FloatResult(this, "UnitPrice", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["UnitPrice"]);
            LineTotal = new StringResult(this, "LineTotal", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["LineTotal"]);
            ReceivedQty = new NumericResult(this, "ReceivedQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["ReceivedQty"]);
            RejectedQty = new NumericResult(this, "RejectedQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["RejectedQty"]);
            StockedQty = new NumericResult(this, "StockedQty", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"].Properties["StockedQty"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public PurchaseOrderDetailNode.PurchaseOrderDetailIn In { get { return new PurchaseOrderDetailNode.PurchaseOrderDetailIn(new PurchaseOrderDetailNode(this, true)); } }

        public DateTimeResult DueDate { get; private set; } 
        public NumericResult OrderQty { get; private set; } 
        public FloatResult UnitPrice { get; private set; } 
        public StringResult LineTotal { get; private set; } 
        public NumericResult ReceivedQty { get; private set; } 
        public NumericResult RejectedQty { get; private set; } 
        public NumericResult StockedQty { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
