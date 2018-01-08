
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductVendorNode ProductVendor { get { return new ProductVendorNode(); } }
	}

	public partial class ProductVendorNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductVendor";
            }
        }

		internal ProductVendorNode() { }
		internal ProductVendorNode(ProductVendorAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductVendorNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductVendorNode Alias(out ProductVendorAlias alias)
		{
			alias = new ProductVendorAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductVendorIn  In  { get { return new ProductVendorIn(this); } }
		public class ProductVendorIn
		{
			private ProductVendorNode Parent;
			internal ProductVendorIn(ProductVendorNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL PRODUCTVENDOR_HAS_PRODUCT { get { return new PRODUCTVENDOR_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL PRODUCTVENDOR_HAS_UNITMEASURE { get { return new PRODUCTVENDOR_HAS_UNITMEASURE_REL(Parent, DirectionEnum.In); } }

		}

		public ProductVendorOut Out { get { return new ProductVendorOut(this); } }
		public class ProductVendorOut
		{
			private ProductVendorNode Parent;
			internal ProductVendorOut(ProductVendorNode parent)
			{
                Parent = parent;
			}
			public IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL VENDOR_BECOMES_PRODUCTVENDOR { get { return new VENDOR_BECOMES_PRODUCTVENDOR_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductVendorAlias : AliasResult
    {
        internal ProductVendorAlias(ProductVendorNode parent)
        {
			Node = parent;
            AverageLeadTime = new StringResult(this, "AverageLeadTime", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["AverageLeadTime"]);
            StandardPrice = new StringResult(this, "StandardPrice", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["StandardPrice"]);
            LastReceiptCost = new StringResult(this, "LastReceiptCost", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["LastReceiptCost"]);
            LastReceiptDate = new DateTimeResult(this, "LastReceiptDate", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["LastReceiptDate"]);
            MinOrderQty = new NumericResult(this, "MinOrderQty", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["MinOrderQty"]);
            MaxOrderQty = new NumericResult(this, "MaxOrderQty", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["MaxOrderQty"]);
            OnOrderQty = new NumericResult(this, "OnOrderQty", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["OnOrderQty"]);
            UnitMeasureCode = new StringResult(this, "UnitMeasureCode", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["UnitMeasureCode"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ProductVendorNode.ProductVendorIn In { get { return new ProductVendorNode.ProductVendorIn(new ProductVendorNode(this, true)); } }
        public ProductVendorNode.ProductVendorOut Out { get { return new ProductVendorNode.ProductVendorOut(new ProductVendorNode(this, true)); } }

        public StringResult AverageLeadTime { get; private set; } 
        public StringResult StandardPrice { get; private set; } 
        public StringResult LastReceiptCost { get; private set; } 
        public DateTimeResult LastReceiptDate { get; private set; } 
        public NumericResult MinOrderQty { get; private set; } 
        public NumericResult MaxOrderQty { get; private set; } 
        public NumericResult OnOrderQty { get; private set; } 
        public StringResult UnitMeasureCode { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
