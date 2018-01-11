using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductModelNode ProductModel { get { return new ProductModelNode(); } }
	}

	public partial class ProductModelNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductModel";
            }
        }

		internal ProductModelNode() { }
		internal ProductModelNode(ProductModelAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductModelNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductModelNode Alias(out ProductModelAlias alias)
		{
			alias = new ProductModelAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductModelIn  In  { get { return new ProductModelIn(this); } }
		public class ProductModelIn
		{
			private ProductModelNode Parent;
			internal ProductModelIn(ProductModelNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTMODEL_HAS_CULTURE_REL PRODUCTMODEL_HAS_CULTURE { get { return new PRODUCTMODEL_HAS_CULTURE_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTMODEL_HAS_ILLUSTRATION_REL PRODUCTMODEL_HAS_ILLUSTRATION { get { return new PRODUCTMODEL_HAS_ILLUSTRATION_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTMODEL_HAS_PRODUCTDESCRIPTION_REL PRODUCTMODEL_HAS_PRODUCTDESCRIPTION { get { return new PRODUCTMODEL_HAS_PRODUCTDESCRIPTION_REL(Parent, DirectionEnum.In); } }

		}

		public ProductModelOut Out { get { return new ProductModelOut(this); } }
		public class ProductModelOut
		{
			private ProductModelNode Parent;
			internal ProductModelOut(ProductModelNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCT_HAS_PRODUCTMODEL_REL PRODUCT_HAS_PRODUCTMODEL { get { return new PRODUCT_HAS_PRODUCTMODEL_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductModelAlias : AliasResult
    {
        internal ProductModelAlias(ProductModelNode parent)
        {
			Node = parent;
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["Name"]);
            CatalogDescription = new StringResult(this, "CatalogDescription", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["CatalogDescription"]);
            Instructions = new StringResult(this, "Instructions", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["Instructions"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ProductModelNode.ProductModelIn In { get { return new ProductModelNode.ProductModelIn(new ProductModelNode(this, true)); } }
        public ProductModelNode.ProductModelOut Out { get { return new ProductModelNode.ProductModelOut(new ProductModelNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public StringResult CatalogDescription { get; private set; } 
        public StringResult Instructions { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
