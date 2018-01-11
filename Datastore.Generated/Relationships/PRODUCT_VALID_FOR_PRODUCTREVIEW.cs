using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PRODUCT_VALID_FOR_PRODUCTREVIEW_REL : RELATIONSHIP, IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL, IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "VALID_FOR_PRODUCTREVIEW";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PRODUCT_VALID_FOR_PRODUCTREVIEW_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Alias(out PRODUCT_VALID_FOR_PRODUCTREVIEW_ALIAS alias)
		{
			alias = new PRODUCT_VALID_FOR_PRODUCTREVIEW_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL.Alias(out PRODUCT_VALID_FOR_PRODUCTREVIEW_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL.Alias(out PRODUCT_VALID_FOR_PRODUCTREVIEW_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCT_VALID_FOR_PRODUCTREVIEW_IN In { get { return new PRODUCT_VALID_FOR_PRODUCTREVIEW_IN(this); } }
        public class PRODUCT_VALID_FOR_PRODUCTREVIEW_IN
        {
            private PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Parent;
            internal PRODUCT_VALID_FOR_PRODUCTREVIEW_IN(PRODUCT_VALID_FOR_PRODUCTREVIEW_REL parent)
            {
                Parent = parent;
            }

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.In); } }
        }

        public PRODUCT_VALID_FOR_PRODUCTREVIEW_OUT Out { get { return new PRODUCT_VALID_FOR_PRODUCTREVIEW_OUT(this); } }
        public class PRODUCT_VALID_FOR_PRODUCTREVIEW_OUT
        {
            private PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Parent;
            internal PRODUCT_VALID_FOR_PRODUCTREVIEW_OUT(PRODUCT_VALID_FOR_PRODUCTREVIEW_REL parent)
            {
                Parent = parent;
            }

			public ProductReviewNode ProductReview { get { return new ProductReviewNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL
    {
		IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Alias(out PRODUCT_VALID_FOR_PRODUCTREVIEW_ALIAS alias);
		IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Repeat(int maxHops);
		IFromIn_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Repeat(int minHops, int maxHops);

        PRODUCT_VALID_FOR_PRODUCTREVIEW_REL.PRODUCT_VALID_FOR_PRODUCTREVIEW_OUT Out { get; }
    }
    public interface IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL
    {
		IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Alias(out PRODUCT_VALID_FOR_PRODUCTREVIEW_ALIAS alias);
		IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Repeat(int maxHops);
		IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Repeat(int minHops, int maxHops);

        PRODUCT_VALID_FOR_PRODUCTREVIEW_REL.PRODUCT_VALID_FOR_PRODUCTREVIEW_IN In { get; }
    }

    public class PRODUCT_VALID_FOR_PRODUCTREVIEW_ALIAS : AliasResult
    {
		private PRODUCT_VALID_FOR_PRODUCTREVIEW_REL Parent;

        internal PRODUCT_VALID_FOR_PRODUCTREVIEW_ALIAS(PRODUCT_VALID_FOR_PRODUCTREVIEW_REL parent)
        {
			Parent = parent;
        }
    }
}
