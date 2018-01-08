
using Blueprint41;
using Blueprint41.Query;


namespace Domain.Data.Query
{
public partial class PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL : RELATIONSHIP, IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL, IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_PRODUCTPRODUCTPHOTO";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Alias(out PRODUCT_HAS_PRODUCTPRODUCTPHOTO_ALIAS alias)
		{
			alias = new PRODUCT_HAS_PRODUCTPRODUCTPHOTO_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL.Alias(out PRODUCT_HAS_PRODUCTPRODUCTPHOTO_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL.Alias(out PRODUCT_HAS_PRODUCTPRODUCTPHOTO_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCT_HAS_PRODUCTPRODUCTPHOTO_IN In { get { return new PRODUCT_HAS_PRODUCTPRODUCTPHOTO_IN(this); } }
        public class PRODUCT_HAS_PRODUCTPRODUCTPHOTO_IN
        {
            private PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Parent;
            internal PRODUCT_HAS_PRODUCTPRODUCTPHOTO_IN(PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL parent)
            {
                Parent = parent;
            }

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.In); } }
        }

        public PRODUCT_HAS_PRODUCTPRODUCTPHOTO_OUT Out { get { return new PRODUCT_HAS_PRODUCTPRODUCTPHOTO_OUT(this); } }
        public class PRODUCT_HAS_PRODUCTPRODUCTPHOTO_OUT
        {
            private PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Parent;
            internal PRODUCT_HAS_PRODUCTPRODUCTPHOTO_OUT(PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL parent)
            {
                Parent = parent;
            }

			public ProductProductPhotoNode ProductProductPhoto { get { return new ProductProductPhotoNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL
    {
		IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Alias(out PRODUCT_HAS_PRODUCTPRODUCTPHOTO_ALIAS alias);
		IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Repeat(int maxHops);
		IFromIn_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Repeat(int minHops, int maxHops);

        PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL.PRODUCT_HAS_PRODUCTPRODUCTPHOTO_OUT Out { get; }
    }
    public interface IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL
    {
		IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Alias(out PRODUCT_HAS_PRODUCTPRODUCTPHOTO_ALIAS alias);
		IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Repeat(int maxHops);
		IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Repeat(int minHops, int maxHops);

        PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL.PRODUCT_HAS_PRODUCTPRODUCTPHOTO_IN In { get; }
    }

    public class PRODUCT_HAS_PRODUCTPRODUCTPHOTO_ALIAS : AliasResult
    {
		private PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL Parent;

        internal PRODUCT_HAS_PRODUCTPRODUCTPHOTO_ALIAS(PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL parent)
        {
			Parent = parent;
        }
    }
}
