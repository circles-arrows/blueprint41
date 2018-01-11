using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductProductPhotoNode ProductProductPhoto { get { return new ProductProductPhotoNode(); } }
	}

	public partial class ProductProductPhotoNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductProductPhoto";
            }
        }

		internal ProductProductPhotoNode() { }
		internal ProductProductPhotoNode(ProductProductPhotoAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductProductPhotoNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductProductPhotoNode Alias(out ProductProductPhotoAlias alias)
		{
			alias = new ProductProductPhotoAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductProductPhotoIn  In  { get { return new ProductProductPhotoIn(this); } }
		public class ProductProductPhotoIn
		{
			private ProductProductPhotoNode Parent;
			internal ProductProductPhotoIn(ProductProductPhotoNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO { get { return new PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL(Parent, DirectionEnum.In); } }

		}

		public ProductProductPhotoOut Out { get { return new ProductProductPhotoOut(this); } }
		public class ProductProductPhotoOut
		{
			private ProductProductPhotoNode Parent;
			internal ProductProductPhotoOut(ProductProductPhotoNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL PRODUCT_HAS_PRODUCTPRODUCTPHOTO { get { return new PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductProductPhotoAlias : AliasResult
    {
        internal ProductProductPhotoAlias(ProductProductPhotoNode parent)
        {
			Node = parent;
            Primary = new StringResult(this, "Primary", Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"].Properties["Primary"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ProductProductPhotoNode.ProductProductPhotoIn In { get { return new ProductProductPhotoNode.ProductProductPhotoIn(new ProductProductPhotoNode(this, true)); } }
        public ProductProductPhotoNode.ProductProductPhotoOut Out { get { return new ProductProductPhotoNode.ProductProductPhotoOut(new ProductProductPhotoNode(this, true)); } }

        public StringResult Primary { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
