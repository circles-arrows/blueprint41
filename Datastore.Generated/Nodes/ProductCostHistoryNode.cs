using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductCostHistoryNode ProductCostHistory { get { return new ProductCostHistoryNode(); } }
	}

	public partial class ProductCostHistoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductCostHistory";
            }
        }

		internal ProductCostHistoryNode() { }
		internal ProductCostHistoryNode(ProductCostHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductCostHistoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductCostHistoryNode Alias(out ProductCostHistoryAlias alias)
		{
			alias = new ProductCostHistoryAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductCostHistoryIn  In  { get { return new ProductCostHistoryIn(this); } }
		public class ProductCostHistoryIn
		{
			private ProductCostHistoryNode Parent;
			internal ProductCostHistoryIn(ProductCostHistoryNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL PRODUCTCOSTHISTORY_HAS_PRODUCT { get { return new PRODUCTCOSTHISTORY_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class ProductCostHistoryAlias : AliasResult
    {
        internal ProductCostHistoryAlias(ProductCostHistoryNode parent)
        {
			Node = parent;
            StartDate = new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["StartDate"]);
            EndDate = new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["EndDate"]);
            StandardCost = new StringResult(this, "StandardCost", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["StandardCost"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ProductCostHistoryNode.ProductCostHistoryIn In { get { return new ProductCostHistoryNode.ProductCostHistoryIn(new ProductCostHistoryNode(this, true)); } }

        public DateTimeResult StartDate { get; private set; } 
        public DateTimeResult EndDate { get; private set; } 
        public StringResult StandardCost { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
