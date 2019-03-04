using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PURCHASEORDERHEADER_HAS_VENDOR_REL : RELATIONSHIP, IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL, IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_VENDOR";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PURCHASEORDERHEADER_HAS_VENDOR_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PURCHASEORDERHEADER_HAS_VENDOR_REL Alias(out PURCHASEORDERHEADER_HAS_VENDOR_ALIAS alias)
		{
			alias = new PURCHASEORDERHEADER_HAS_VENDOR_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PURCHASEORDERHEADER_HAS_VENDOR_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PURCHASEORDERHEADER_HAS_VENDOR_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL.Alias(out PURCHASEORDERHEADER_HAS_VENDOR_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL.Alias(out PURCHASEORDERHEADER_HAS_VENDOR_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PURCHASEORDERHEADER_HAS_VENDOR_IN In { get { return new PURCHASEORDERHEADER_HAS_VENDOR_IN(this); } }
        public class PURCHASEORDERHEADER_HAS_VENDOR_IN
        {
            private PURCHASEORDERHEADER_HAS_VENDOR_REL Parent;
            internal PURCHASEORDERHEADER_HAS_VENDOR_IN(PURCHASEORDERHEADER_HAS_VENDOR_REL parent)
            {
                Parent = parent;
            }

			public PurchaseOrderHeaderNode PurchaseOrderHeader { get { return new PurchaseOrderHeaderNode(Parent, DirectionEnum.In); } }
        }

        public PURCHASEORDERHEADER_HAS_VENDOR_OUT Out { get { return new PURCHASEORDERHEADER_HAS_VENDOR_OUT(this); } }
        public class PURCHASEORDERHEADER_HAS_VENDOR_OUT
        {
            private PURCHASEORDERHEADER_HAS_VENDOR_REL Parent;
            internal PURCHASEORDERHEADER_HAS_VENDOR_OUT(PURCHASEORDERHEADER_HAS_VENDOR_REL parent)
            {
                Parent = parent;
            }

			public VendorNode Vendor { get { return new VendorNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL
    {
		IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL Alias(out PURCHASEORDERHEADER_HAS_VENDOR_ALIAS alias);
		IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL Repeat(int maxHops);
		IFromIn_PURCHASEORDERHEADER_HAS_VENDOR_REL Repeat(int minHops, int maxHops);

        PURCHASEORDERHEADER_HAS_VENDOR_REL.PURCHASEORDERHEADER_HAS_VENDOR_OUT Out { get; }
    }
    public interface IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL
    {
		IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL Alias(out PURCHASEORDERHEADER_HAS_VENDOR_ALIAS alias);
		IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL Repeat(int maxHops);
		IFromOut_PURCHASEORDERHEADER_HAS_VENDOR_REL Repeat(int minHops, int maxHops);

        PURCHASEORDERHEADER_HAS_VENDOR_REL.PURCHASEORDERHEADER_HAS_VENDOR_IN In { get; }
    }

    public class PURCHASEORDERHEADER_HAS_VENDOR_ALIAS : AliasResult
    {
		private PURCHASEORDERHEADER_HAS_VENDOR_REL Parent;

        internal PURCHASEORDERHEADER_HAS_VENDOR_ALIAS(PURCHASEORDERHEADER_HAS_VENDOR_REL parent)
        {
			Parent = parent;
        }
    }
}
