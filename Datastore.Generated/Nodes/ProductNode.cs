using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductNode Product { get { return new ProductNode(); } }
	}

	public partial class ProductNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Product";
            }
        }

		internal ProductNode() { }
		internal ProductNode(ProductAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductNode Alias(out ProductAlias alias)
		{
			alias = new ProductAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductIn  In  { get { return new ProductIn(this); } }
		public class ProductIn
		{
			private ProductNode Parent;
			internal ProductIn(ProductNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCT_HAS_DOCUMENT_REL PRODUCT_HAS_DOCUMENT { get { return new PRODUCT_HAS_DOCUMENT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCT_HAS_PRODUCTMODEL_REL PRODUCT_HAS_PRODUCTMODEL { get { return new PRODUCT_HAS_PRODUCTMODEL_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL PRODUCT_HAS_PRODUCTPRODUCTPHOTO { get { return new PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL PRODUCT_HAS_TRANSACTIONHISTORY { get { return new PRODUCT_HAS_TRANSACTIONHISTORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL PRODUCT_VALID_FOR_PRODUCTREVIEW { get { return new PRODUCT_VALID_FOR_PRODUCTREVIEW_REL(Parent, DirectionEnum.In); } }

		}

		public ProductOut Out { get { return new ProductOut(this); } }
		public class ProductOut
		{
			private ProductNode Parent;
			internal ProductOut(ProductNode parent)
			{
                Parent = parent;
			}
			public IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL BILLOFMATERIALS_HAS_PRODUCT { get { return new BILLOFMATERIALS_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL PRODUCTCOSTHISTORY_HAS_PRODUCT { get { return new PRODUCTCOSTHISTORY_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL PRODUCTINVENTORY_HAS_PRODUCT { get { return new PRODUCTINVENTORY_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT { get { return new PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL PRODUCTVENDOR_HAS_PRODUCT { get { return new PRODUCTVENDOR_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PURCHASEORDERDETAIL_HAS_PRODUCT_REL PURCHASEORDERDETAIL_HAS_PRODUCT { get { return new PURCHASEORDERDETAIL_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL SALESORDERDETAIL_HAS_PRODUCT { get { return new SALESORDERDETAIL_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL SHOPPINGCARTITEM_HAS_PRODUCT { get { return new SHOPPINGCARTITEM_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT { get { return new TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_WORKORDER_HAS_PRODUCT_REL WORKORDER_HAS_PRODUCT { get { return new WORKORDER_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_WORKORDERROUTING_HAS_PRODUCT_REL WORKORDERROUTING_HAS_PRODUCT { get { return new WORKORDERROUTING_HAS_PRODUCT_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductAlias : AliasResult
    {
        internal ProductAlias(ProductNode parent)
        {
			Node = parent;
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Name"]);
            ProductNumber = new StringResult(this, "ProductNumber", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["ProductNumber"]);
            MakeFlag = new BooleanResult(this, "MakeFlag", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["MakeFlag"]);
            FinishedGoodsFlag = new BooleanResult(this, "FinishedGoodsFlag", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["FinishedGoodsFlag"]);
            Color = new StringResult(this, "Color", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Color"]);
            SafetyStockLevel = new NumericResult(this, "SafetyStockLevel", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["SafetyStockLevel"]);
            ReorderPoint = new NumericResult(this, "ReorderPoint", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["ReorderPoint"]);
            StandardCost = new FloatResult(this, "StandardCost", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["StandardCost"]);
            ListPrice = new FloatResult(this, "ListPrice", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["ListPrice"]);
            Size = new StringResult(this, "Size", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Size"]);
            SizeUnitMeasureCode = new StringResult(this, "SizeUnitMeasureCode", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["SizeUnitMeasureCode"]);
            WeightUnitMeasureCode = new StringResult(this, "WeightUnitMeasureCode", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["WeightUnitMeasureCode"]);
            Weight = new MiscResult(this, "Weight", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Weight"]);
            DaysToManufacture = new NumericResult(this, "DaysToManufacture", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["DaysToManufacture"]);
            ProductLine = new StringResult(this, "ProductLine", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["ProductLine"]);
            Class = new StringResult(this, "Class", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Class"]);
            Style = new StringResult(this, "Style", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["Style"]);
            SellStartDate = new DateTimeResult(this, "SellStartDate", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["SellStartDate"]);
            SellEndDate = new DateTimeResult(this, "SellEndDate", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["SellEndDate"]);
            DiscontinuedDate = new DateTimeResult(this, "DiscontinuedDate", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["DiscontinuedDate"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Product"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Product"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ProductNode.ProductIn In { get { return new ProductNode.ProductIn(new ProductNode(this, true)); } }
        public ProductNode.ProductOut Out { get { return new ProductNode.ProductOut(new ProductNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public StringResult ProductNumber { get; private set; } 
        public BooleanResult MakeFlag { get; private set; } 
        public BooleanResult FinishedGoodsFlag { get; private set; } 
        public StringResult Color { get; private set; } 
        public NumericResult SafetyStockLevel { get; private set; } 
        public NumericResult ReorderPoint { get; private set; } 
        public FloatResult StandardCost { get; private set; } 
        public FloatResult ListPrice { get; private set; } 
        public StringResult Size { get; private set; } 
        public StringResult SizeUnitMeasureCode { get; private set; } 
        public StringResult WeightUnitMeasureCode { get; private set; } 
        public MiscResult Weight { get; private set; } 
        public NumericResult DaysToManufacture { get; private set; } 
        public StringResult ProductLine { get; private set; } 
        public StringResult Class { get; private set; } 
        public StringResult Style { get; private set; } 
        public DateTimeResult SellStartDate { get; private set; } 
        public DateTimeResult SellEndDate { get; private set; } 
        public DateTimeResult DiscontinuedDate { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
