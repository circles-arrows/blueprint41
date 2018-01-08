
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesOrderDetailNode SalesOrderDetail { get { return new SalesOrderDetailNode(); } }
	}

	public partial class SalesOrderDetailNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesOrderDetail";
            }
        }

		internal SalesOrderDetailNode() { }
		internal SalesOrderDetailNode(SalesOrderDetailAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesOrderDetailNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesOrderDetailNode Alias(out SalesOrderDetailAlias alias)
		{
			alias = new SalesOrderDetailAlias(this);
            NodeAlias = alias;
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

    public class SalesOrderDetailAlias : AliasResult
    {
        internal SalesOrderDetailAlias(SalesOrderDetailNode parent)
        {
			Node = parent;
            CarrierTrackingNumber = new StringResult(this, "CarrierTrackingNumber", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["CarrierTrackingNumber"]);
            OrderQty = new NumericResult(this, "OrderQty", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["OrderQty"]);
            UnitPrice = new FloatResult(this, "UnitPrice", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["UnitPrice"]);
            UnitPriceDiscount = new StringResult(this, "UnitPriceDiscount", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["UnitPriceDiscount"]);
            LineTotal = new StringResult(this, "LineTotal", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["LineTotal"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public SalesOrderDetailNode.SalesOrderDetailIn In { get { return new SalesOrderDetailNode.SalesOrderDetailIn(new SalesOrderDetailNode(this, true)); } }

        public StringResult CarrierTrackingNumber { get; private set; } 
        public NumericResult OrderQty { get; private set; } 
        public FloatResult UnitPrice { get; private set; } 
        public StringResult UnitPriceDiscount { get; private set; } 
        public StringResult LineTotal { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
