using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductInventoryNode ProductInventory { get { return new ProductInventoryNode(); } }
	}

	public partial class ProductInventoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductInventory";
            }
        }

		internal ProductInventoryNode() { }
		internal ProductInventoryNode(ProductInventoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductInventoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductInventoryNode Alias(out ProductInventoryAlias alias)
		{
			alias = new ProductInventoryAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductInventoryIn  In  { get { return new ProductInventoryIn(this); } }
		public class ProductInventoryIn
		{
			private ProductInventoryNode Parent;
			internal ProductInventoryIn(ProductInventoryNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL PRODUCTINVENTORY_HAS_LOCATION { get { return new PRODUCTINVENTORY_HAS_LOCATION_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL PRODUCTINVENTORY_HAS_PRODUCT { get { return new PRODUCTINVENTORY_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class ProductInventoryAlias : AliasResult
    {
        internal ProductInventoryAlias(ProductInventoryNode parent)
        {
			Node = parent;
            Shelf = new StringResult(this, "Shelf", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Shelf"]);
            Bin = new StringResult(this, "Bin", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Bin"]);
            Quantity = new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Quantity"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ProductInventoryNode.ProductInventoryIn In { get { return new ProductInventoryNode.ProductInventoryIn(new ProductInventoryNode(this, true)); } }

        public StringResult Shelf { get; private set; } 
        public StringResult Bin { get; private set; } 
        public NumericResult Quantity { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
