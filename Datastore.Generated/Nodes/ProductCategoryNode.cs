
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductCategoryNode ProductCategory { get { return new ProductCategoryNode(); } }
	}

	public partial class ProductCategoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductCategory";
            }
        }

		internal ProductCategoryNode() { }
		internal ProductCategoryNode(ProductCategoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductCategoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductCategoryNode Alias(out ProductCategoryAlias alias)
		{
			alias = new ProductCategoryAlias(this);
            NodeAlias = alias;
			return this;
		}

	}

    public class ProductCategoryAlias : AliasResult
    {
        internal ProductCategoryAlias(ProductCategoryNode parent)
        {
			Node = parent;
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ProductCategory"], Datastore.AdventureWorks.Model.Entities["ProductCategory"].Properties["Name"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ProductCategory"], Datastore.AdventureWorks.Model.Entities["ProductCategory"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductCategory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductCategory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }


        public StringResult Name { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
