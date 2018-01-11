using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PRODUCTINVENTORY_HAS_PRODUCT_REL : RELATIONSHIP, IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL, IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_PRODUCT";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PRODUCTINVENTORY_HAS_PRODUCT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCTINVENTORY_HAS_PRODUCT_REL Alias(out PRODUCTINVENTORY_HAS_PRODUCT_ALIAS alias)
		{
			alias = new PRODUCTINVENTORY_HAS_PRODUCT_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PRODUCTINVENTORY_HAS_PRODUCT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PRODUCTINVENTORY_HAS_PRODUCT_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL.Alias(out PRODUCTINVENTORY_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL.Alias(out PRODUCTINVENTORY_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCTINVENTORY_HAS_PRODUCT_IN In { get { return new PRODUCTINVENTORY_HAS_PRODUCT_IN(this); } }
        public class PRODUCTINVENTORY_HAS_PRODUCT_IN
        {
            private PRODUCTINVENTORY_HAS_PRODUCT_REL Parent;
            internal PRODUCTINVENTORY_HAS_PRODUCT_IN(PRODUCTINVENTORY_HAS_PRODUCT_REL parent)
            {
                Parent = parent;
            }

			public ProductInventoryNode ProductInventory { get { return new ProductInventoryNode(Parent, DirectionEnum.In); } }
        }

        public PRODUCTINVENTORY_HAS_PRODUCT_OUT Out { get { return new PRODUCTINVENTORY_HAS_PRODUCT_OUT(this); } }
        public class PRODUCTINVENTORY_HAS_PRODUCT_OUT
        {
            private PRODUCTINVENTORY_HAS_PRODUCT_REL Parent;
            internal PRODUCTINVENTORY_HAS_PRODUCT_OUT(PRODUCTINVENTORY_HAS_PRODUCT_REL parent)
            {
                Parent = parent;
            }

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL
    {
		IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL Alias(out PRODUCTINVENTORY_HAS_PRODUCT_ALIAS alias);
		IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

        PRODUCTINVENTORY_HAS_PRODUCT_REL.PRODUCTINVENTORY_HAS_PRODUCT_OUT Out { get; }
    }
    public interface IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL
    {
		IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL Alias(out PRODUCTINVENTORY_HAS_PRODUCT_ALIAS alias);
		IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromOut_PRODUCTINVENTORY_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

        PRODUCTINVENTORY_HAS_PRODUCT_REL.PRODUCTINVENTORY_HAS_PRODUCT_IN In { get; }
    }

    public class PRODUCTINVENTORY_HAS_PRODUCT_ALIAS : AliasResult
    {
		private PRODUCTINVENTORY_HAS_PRODUCT_REL Parent;

        internal PRODUCTINVENTORY_HAS_PRODUCT_ALIAS(PRODUCTINVENTORY_HAS_PRODUCT_REL parent)
        {
			Parent = parent;
        }
    }
}
