using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductListPriceHistoryNode ProductListPriceHistory { get { return new ProductListPriceHistoryNode(); } }
	}

	public partial class ProductListPriceHistoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductListPriceHistory";
            }
        }

		internal ProductListPriceHistoryNode() { }
		internal ProductListPriceHistoryNode(ProductListPriceHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductListPriceHistoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductListPriceHistoryNode Alias(out ProductListPriceHistoryAlias alias)
		{
			alias = new ProductListPriceHistoryAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductListPriceHistoryIn  In  { get { return new ProductListPriceHistoryIn(this); } }
		public class ProductListPriceHistoryIn
		{
			private ProductListPriceHistoryNode Parent;
			internal ProductListPriceHistoryIn(ProductListPriceHistoryNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT { get { return new PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class ProductListPriceHistoryAlias : AliasResult
    {
        internal ProductListPriceHistoryAlias(ProductListPriceHistoryNode parent)
        {
			Node = parent;
            StartDate = new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"].Properties["StartDate"]);
            EndDate = new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"].Properties["EndDate"]);
            ListPrice = new StringResult(this, "ListPrice", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"].Properties["ListPrice"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ProductListPriceHistoryNode.ProductListPriceHistoryIn In { get { return new ProductListPriceHistoryNode.ProductListPriceHistoryIn(new ProductListPriceHistoryNode(this, true)); } }

        public DateTimeResult StartDate { get; private set; } 
        public DateTimeResult EndDate { get; private set; } 
        public StringResult ListPrice { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
