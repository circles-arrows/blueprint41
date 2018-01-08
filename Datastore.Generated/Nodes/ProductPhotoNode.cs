
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductPhotoNode ProductPhoto { get { return new ProductPhotoNode(); } }
	}

	public partial class ProductPhotoNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductPhoto";
            }
        }

		internal ProductPhotoNode() { }
		internal ProductPhotoNode(ProductPhotoAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductPhotoNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductPhotoNode Alias(out ProductPhotoAlias alias)
		{
			alias = new ProductPhotoAlias(this);
            NodeAlias = alias;
			return this;
		}


		public ProductPhotoOut Out { get { return new ProductPhotoOut(this); } }
		public class ProductPhotoOut
		{
			private ProductPhotoNode Parent;
			internal ProductPhotoOut(ProductPhotoNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO { get { return new PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductPhotoAlias : AliasResult
    {
        internal ProductPhotoAlias(ProductPhotoNode parent)
        {
			Node = parent;
            ThumbNailPhoto = new StringResult(this, "ThumbNailPhoto", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["ThumbNailPhoto"]);
            ThumbNailPhotoFileName = new StringResult(this, "ThumbNailPhotoFileName", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["ThumbNailPhotoFileName"]);
            LargePhoto = new StringResult(this, "LargePhoto", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["LargePhoto"]);
            LargePhotoFileName = new StringResult(this, "LargePhotoFileName", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["LargePhotoFileName"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ProductPhotoNode.ProductPhotoOut Out { get { return new ProductPhotoNode.ProductPhotoOut(new ProductPhotoNode(this, true)); } }

        public StringResult ThumbNailPhoto { get; private set; } 
        public StringResult ThumbNailPhotoFileName { get; private set; } 
        public StringResult LargePhoto { get; private set; } 
        public StringResult LargePhotoFileName { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
