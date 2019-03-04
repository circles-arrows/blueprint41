using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PRODUCTINVENTORY_HAS_LOCATION_REL : RELATIONSHIP, IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL, IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_LOCATION";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PRODUCTINVENTORY_HAS_LOCATION_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCTINVENTORY_HAS_LOCATION_REL Alias(out PRODUCTINVENTORY_HAS_LOCATION_ALIAS alias)
		{
			alias = new PRODUCTINVENTORY_HAS_LOCATION_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PRODUCTINVENTORY_HAS_LOCATION_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PRODUCTINVENTORY_HAS_LOCATION_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL.Alias(out PRODUCTINVENTORY_HAS_LOCATION_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL.Alias(out PRODUCTINVENTORY_HAS_LOCATION_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCTINVENTORY_HAS_LOCATION_IN In { get { return new PRODUCTINVENTORY_HAS_LOCATION_IN(this); } }
        public class PRODUCTINVENTORY_HAS_LOCATION_IN
        {
            private PRODUCTINVENTORY_HAS_LOCATION_REL Parent;
            internal PRODUCTINVENTORY_HAS_LOCATION_IN(PRODUCTINVENTORY_HAS_LOCATION_REL parent)
            {
                Parent = parent;
            }

			public ProductInventoryNode ProductInventory { get { return new ProductInventoryNode(Parent, DirectionEnum.In); } }
        }

        public PRODUCTINVENTORY_HAS_LOCATION_OUT Out { get { return new PRODUCTINVENTORY_HAS_LOCATION_OUT(this); } }
        public class PRODUCTINVENTORY_HAS_LOCATION_OUT
        {
            private PRODUCTINVENTORY_HAS_LOCATION_REL Parent;
            internal PRODUCTINVENTORY_HAS_LOCATION_OUT(PRODUCTINVENTORY_HAS_LOCATION_REL parent)
            {
                Parent = parent;
            }

			public LocationNode Location { get { return new LocationNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL
    {
		IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL Alias(out PRODUCTINVENTORY_HAS_LOCATION_ALIAS alias);
		IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL Repeat(int maxHops);
		IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL Repeat(int minHops, int maxHops);

        PRODUCTINVENTORY_HAS_LOCATION_REL.PRODUCTINVENTORY_HAS_LOCATION_OUT Out { get; }
    }
    public interface IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL
    {
		IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL Alias(out PRODUCTINVENTORY_HAS_LOCATION_ALIAS alias);
		IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL Repeat(int maxHops);
		IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL Repeat(int minHops, int maxHops);

        PRODUCTINVENTORY_HAS_LOCATION_REL.PRODUCTINVENTORY_HAS_LOCATION_IN In { get; }
    }

    public class PRODUCTINVENTORY_HAS_LOCATION_ALIAS : AliasResult
    {
		private PRODUCTINVENTORY_HAS_LOCATION_REL Parent;

        internal PRODUCTINVENTORY_HAS_LOCATION_ALIAS(PRODUCTINVENTORY_HAS_LOCATION_REL parent)
        {
			Parent = parent;
        }
    }
}
