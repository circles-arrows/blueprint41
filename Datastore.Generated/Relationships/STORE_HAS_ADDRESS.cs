using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class STORE_HAS_ADDRESS_REL : RELATIONSHIP, IFromIn_STORE_HAS_ADDRESS_REL, IFromOut_STORE_HAS_ADDRESS_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_ADDRESS";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal STORE_HAS_ADDRESS_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public STORE_HAS_ADDRESS_REL Alias(out STORE_HAS_ADDRESS_ALIAS alias)
		{
			alias = new STORE_HAS_ADDRESS_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public STORE_HAS_ADDRESS_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public STORE_HAS_ADDRESS_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_STORE_HAS_ADDRESS_REL IFromIn_STORE_HAS_ADDRESS_REL.Alias(out STORE_HAS_ADDRESS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_STORE_HAS_ADDRESS_REL IFromOut_STORE_HAS_ADDRESS_REL.Alias(out STORE_HAS_ADDRESS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_STORE_HAS_ADDRESS_REL IFromIn_STORE_HAS_ADDRESS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_STORE_HAS_ADDRESS_REL IFromIn_STORE_HAS_ADDRESS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_STORE_HAS_ADDRESS_REL IFromOut_STORE_HAS_ADDRESS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_STORE_HAS_ADDRESS_REL IFromOut_STORE_HAS_ADDRESS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public STORE_HAS_ADDRESS_IN In { get { return new STORE_HAS_ADDRESS_IN(this); } }
        public class STORE_HAS_ADDRESS_IN
        {
            private STORE_HAS_ADDRESS_REL Parent;
            internal STORE_HAS_ADDRESS_IN(STORE_HAS_ADDRESS_REL parent)
            {
                Parent = parent;
            }

			public StoreNode Store { get { return new StoreNode(Parent, DirectionEnum.In); } }
        }

        public STORE_HAS_ADDRESS_OUT Out { get { return new STORE_HAS_ADDRESS_OUT(this); } }
        public class STORE_HAS_ADDRESS_OUT
        {
            private STORE_HAS_ADDRESS_REL Parent;
            internal STORE_HAS_ADDRESS_OUT(STORE_HAS_ADDRESS_REL parent)
            {
                Parent = parent;
            }

			public AddressNode Address { get { return new AddressNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_STORE_HAS_ADDRESS_REL
    {
		IFromIn_STORE_HAS_ADDRESS_REL Alias(out STORE_HAS_ADDRESS_ALIAS alias);
		IFromIn_STORE_HAS_ADDRESS_REL Repeat(int maxHops);
		IFromIn_STORE_HAS_ADDRESS_REL Repeat(int minHops, int maxHops);

        STORE_HAS_ADDRESS_REL.STORE_HAS_ADDRESS_OUT Out { get; }
    }
    public interface IFromOut_STORE_HAS_ADDRESS_REL
    {
		IFromOut_STORE_HAS_ADDRESS_REL Alias(out STORE_HAS_ADDRESS_ALIAS alias);
		IFromOut_STORE_HAS_ADDRESS_REL Repeat(int maxHops);
		IFromOut_STORE_HAS_ADDRESS_REL Repeat(int minHops, int maxHops);

        STORE_HAS_ADDRESS_REL.STORE_HAS_ADDRESS_IN In { get; }
    }

    public class STORE_HAS_ADDRESS_ALIAS : AliasResult
    {
		private STORE_HAS_ADDRESS_REL Parent;

        internal STORE_HAS_ADDRESS_ALIAS(STORE_HAS_ADDRESS_REL parent)
        {
			Parent = parent;
        }
    }
}
