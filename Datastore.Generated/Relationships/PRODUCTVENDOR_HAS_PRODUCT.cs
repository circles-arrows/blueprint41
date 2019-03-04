using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PRODUCTVENDOR_HAS_PRODUCT_REL : RELATIONSHIP, IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL, IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_PRODUCT";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PRODUCTVENDOR_HAS_PRODUCT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCTVENDOR_HAS_PRODUCT_REL Alias(out PRODUCTVENDOR_HAS_PRODUCT_ALIAS alias)
		{
			alias = new PRODUCTVENDOR_HAS_PRODUCT_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PRODUCTVENDOR_HAS_PRODUCT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PRODUCTVENDOR_HAS_PRODUCT_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL.Alias(out PRODUCTVENDOR_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL.Alias(out PRODUCTVENDOR_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCTVENDOR_HAS_PRODUCT_IN In { get { return new PRODUCTVENDOR_HAS_PRODUCT_IN(this); } }
        public class PRODUCTVENDOR_HAS_PRODUCT_IN
        {
            private PRODUCTVENDOR_HAS_PRODUCT_REL Parent;
            internal PRODUCTVENDOR_HAS_PRODUCT_IN(PRODUCTVENDOR_HAS_PRODUCT_REL parent)
            {
                Parent = parent;
            }

			public ProductVendorNode ProductVendor { get { return new ProductVendorNode(Parent, DirectionEnum.In); } }
        }

        public PRODUCTVENDOR_HAS_PRODUCT_OUT Out { get { return new PRODUCTVENDOR_HAS_PRODUCT_OUT(this); } }
        public class PRODUCTVENDOR_HAS_PRODUCT_OUT
        {
            private PRODUCTVENDOR_HAS_PRODUCT_REL Parent;
            internal PRODUCTVENDOR_HAS_PRODUCT_OUT(PRODUCTVENDOR_HAS_PRODUCT_REL parent)
            {
                Parent = parent;
            }

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL
    {
		IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL Alias(out PRODUCTVENDOR_HAS_PRODUCT_ALIAS alias);
		IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

        PRODUCTVENDOR_HAS_PRODUCT_REL.PRODUCTVENDOR_HAS_PRODUCT_OUT Out { get; }
    }
    public interface IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL
    {
		IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL Alias(out PRODUCTVENDOR_HAS_PRODUCT_ALIAS alias);
		IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromOut_PRODUCTVENDOR_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

        PRODUCTVENDOR_HAS_PRODUCT_REL.PRODUCTVENDOR_HAS_PRODUCT_IN In { get; }
    }

    public class PRODUCTVENDOR_HAS_PRODUCT_ALIAS : AliasResult
    {
		private PRODUCTVENDOR_HAS_PRODUCT_REL Parent;

        internal PRODUCTVENDOR_HAS_PRODUCT_ALIAS(PRODUCTVENDOR_HAS_PRODUCT_REL parent)
        {
			Parent = parent;
        }
    }
}
