using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ShoppingCartItemNode ShoppingCartItem { get { return new ShoppingCartItemNode(); } }
	}

	public partial class ShoppingCartItemNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ShoppingCartItem";
            }
        }

		internal ShoppingCartItemNode() { }
		internal ShoppingCartItemNode(ShoppingCartItemAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ShoppingCartItemNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ShoppingCartItemNode Alias(out ShoppingCartItemAlias alias)
		{
			alias = new ShoppingCartItemAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ShoppingCartItemIn  In  { get { return new ShoppingCartItemIn(this); } }
		public class ShoppingCartItemIn
		{
			private ShoppingCartItemNode Parent;
			internal ShoppingCartItemIn(ShoppingCartItemNode parent)
			{
                Parent = parent;
			}
			public IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL SHOPPINGCARTITEM_HAS_PRODUCT { get { return new SHOPPINGCARTITEM_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class ShoppingCartItemAlias : AliasResult
    {
        internal ShoppingCartItemAlias(ShoppingCartItemNode parent)
        {
			Node = parent;
            Quantity = new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"].Properties["Quantity"]);
            DateCreated = new DateTimeResult(this, "DateCreated", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"].Properties["DateCreated"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ShoppingCartItemNode.ShoppingCartItemIn In { get { return new ShoppingCartItemNode.ShoppingCartItemIn(new ShoppingCartItemNode(this, true)); } }

        public NumericResult Quantity { get; private set; } 
        public DateTimeResult DateCreated { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
