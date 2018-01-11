using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class SHOPPINGCARTITEM_HAS_PRODUCT_REL : RELATIONSHIP, IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL, IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_PRODUCT";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal SHOPPINGCARTITEM_HAS_PRODUCT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public SHOPPINGCARTITEM_HAS_PRODUCT_REL Alias(out SHOPPINGCARTITEM_HAS_PRODUCT_ALIAS alias)
		{
			alias = new SHOPPINGCARTITEM_HAS_PRODUCT_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public SHOPPINGCARTITEM_HAS_PRODUCT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public SHOPPINGCARTITEM_HAS_PRODUCT_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL.Alias(out SHOPPINGCARTITEM_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL.Alias(out SHOPPINGCARTITEM_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public SHOPPINGCARTITEM_HAS_PRODUCT_IN In { get { return new SHOPPINGCARTITEM_HAS_PRODUCT_IN(this); } }
        public class SHOPPINGCARTITEM_HAS_PRODUCT_IN
        {
            private SHOPPINGCARTITEM_HAS_PRODUCT_REL Parent;
            internal SHOPPINGCARTITEM_HAS_PRODUCT_IN(SHOPPINGCARTITEM_HAS_PRODUCT_REL parent)
            {
                Parent = parent;
            }

			public ShoppingCartItemNode ShoppingCartItem { get { return new ShoppingCartItemNode(Parent, DirectionEnum.In); } }
        }

        public SHOPPINGCARTITEM_HAS_PRODUCT_OUT Out { get { return new SHOPPINGCARTITEM_HAS_PRODUCT_OUT(this); } }
        public class SHOPPINGCARTITEM_HAS_PRODUCT_OUT
        {
            private SHOPPINGCARTITEM_HAS_PRODUCT_REL Parent;
            internal SHOPPINGCARTITEM_HAS_PRODUCT_OUT(SHOPPINGCARTITEM_HAS_PRODUCT_REL parent)
            {
                Parent = parent;
            }

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL
    {
		IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL Alias(out SHOPPINGCARTITEM_HAS_PRODUCT_ALIAS alias);
		IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

        SHOPPINGCARTITEM_HAS_PRODUCT_REL.SHOPPINGCARTITEM_HAS_PRODUCT_OUT Out { get; }
    }
    public interface IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL
    {
		IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL Alias(out SHOPPINGCARTITEM_HAS_PRODUCT_ALIAS alias);
		IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromOut_SHOPPINGCARTITEM_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

        SHOPPINGCARTITEM_HAS_PRODUCT_REL.SHOPPINGCARTITEM_HAS_PRODUCT_IN In { get; }
    }

    public class SHOPPINGCARTITEM_HAS_PRODUCT_ALIAS : AliasResult
    {
		private SHOPPINGCARTITEM_HAS_PRODUCT_REL Parent;

        internal SHOPPINGCARTITEM_HAS_PRODUCT_ALIAS(SHOPPINGCARTITEM_HAS_PRODUCT_REL parent)
        {
			Parent = parent;
        }
    }
}
