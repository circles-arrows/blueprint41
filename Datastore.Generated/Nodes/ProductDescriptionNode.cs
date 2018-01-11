using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductDescriptionNode ProductDescription { get { return new ProductDescriptionNode(); } }
	}

	public partial class ProductDescriptionNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductDescription";
            }
        }

		internal ProductDescriptionNode() { }
		internal ProductDescriptionNode(ProductDescriptionAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductDescriptionNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductDescriptionNode Alias(out ProductDescriptionAlias alias)
		{
			alias = new ProductDescriptionAlias(this);
            NodeAlias = alias;
			return this;
		}


		public ProductDescriptionOut Out { get { return new ProductDescriptionOut(this); } }
		public class ProductDescriptionOut
		{
			private ProductDescriptionNode Parent;
			internal ProductDescriptionOut(ProductDescriptionNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCTMODEL_HAS_PRODUCTDESCRIPTION_REL PRODUCTMODEL_HAS_PRODUCTDESCRIPTION { get { return new PRODUCTMODEL_HAS_PRODUCTDESCRIPTION_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductDescriptionAlias : AliasResult
    {
        internal ProductDescriptionAlias(ProductDescriptionNode parent)
        {
			Node = parent;
            Description = new StringResult(this, "Description", Datastore.AdventureWorks.Model.Entities["ProductDescription"], Datastore.AdventureWorks.Model.Entities["ProductDescription"].Properties["Description"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ProductDescription"], Datastore.AdventureWorks.Model.Entities["ProductDescription"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductDescription"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductDescription"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ProductDescriptionNode.ProductDescriptionOut Out { get { return new ProductDescriptionNode.ProductDescriptionOut(new ProductDescriptionNode(this, true)); } }

        public StringResult Description { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
