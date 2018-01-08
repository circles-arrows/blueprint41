
using Blueprint41;
using Blueprint41.Query;


namespace Domain.Data.Query
{
public partial class VENDOR_BECOMES_PRODUCTVENDOR_REL : RELATIONSHIP, IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL, IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "BECOMES_PRODUCTVENDOR";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal VENDOR_BECOMES_PRODUCTVENDOR_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public VENDOR_BECOMES_PRODUCTVENDOR_REL Alias(out VENDOR_BECOMES_PRODUCTVENDOR_ALIAS alias)
		{
			alias = new VENDOR_BECOMES_PRODUCTVENDOR_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public VENDOR_BECOMES_PRODUCTVENDOR_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public VENDOR_BECOMES_PRODUCTVENDOR_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL.Alias(out VENDOR_BECOMES_PRODUCTVENDOR_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL.Alias(out VENDOR_BECOMES_PRODUCTVENDOR_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public VENDOR_BECOMES_PRODUCTVENDOR_IN In { get { return new VENDOR_BECOMES_PRODUCTVENDOR_IN(this); } }
        public class VENDOR_BECOMES_PRODUCTVENDOR_IN
        {
            private VENDOR_BECOMES_PRODUCTVENDOR_REL Parent;
            internal VENDOR_BECOMES_PRODUCTVENDOR_IN(VENDOR_BECOMES_PRODUCTVENDOR_REL parent)
            {
                Parent = parent;
            }

			public VendorNode Vendor { get { return new VendorNode(Parent, DirectionEnum.In); } }
        }

        public VENDOR_BECOMES_PRODUCTVENDOR_OUT Out { get { return new VENDOR_BECOMES_PRODUCTVENDOR_OUT(this); } }
        public class VENDOR_BECOMES_PRODUCTVENDOR_OUT
        {
            private VENDOR_BECOMES_PRODUCTVENDOR_REL Parent;
            internal VENDOR_BECOMES_PRODUCTVENDOR_OUT(VENDOR_BECOMES_PRODUCTVENDOR_REL parent)
            {
                Parent = parent;
            }

			public ProductVendorNode ProductVendor { get { return new ProductVendorNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL
    {
		IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL Alias(out VENDOR_BECOMES_PRODUCTVENDOR_ALIAS alias);
		IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL Repeat(int maxHops);
		IFromIn_VENDOR_BECOMES_PRODUCTVENDOR_REL Repeat(int minHops, int maxHops);

        VENDOR_BECOMES_PRODUCTVENDOR_REL.VENDOR_BECOMES_PRODUCTVENDOR_OUT Out { get; }
    }
    public interface IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL
    {
		IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL Alias(out VENDOR_BECOMES_PRODUCTVENDOR_ALIAS alias);
		IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL Repeat(int maxHops);
		IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL Repeat(int minHops, int maxHops);

        VENDOR_BECOMES_PRODUCTVENDOR_REL.VENDOR_BECOMES_PRODUCTVENDOR_IN In { get; }
    }

    public class VENDOR_BECOMES_PRODUCTVENDOR_ALIAS : AliasResult
    {
		private VENDOR_BECOMES_PRODUCTVENDOR_REL Parent;

        internal VENDOR_BECOMES_PRODUCTVENDOR_ALIAS(VENDOR_BECOMES_PRODUCTVENDOR_REL parent)
        {
			Parent = parent;
        }
    }
}
