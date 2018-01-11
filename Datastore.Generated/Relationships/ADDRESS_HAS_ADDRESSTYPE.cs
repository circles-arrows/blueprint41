using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class ADDRESS_HAS_ADDRESSTYPE_REL : RELATIONSHIP, IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL, IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_ADDRESSTYPE";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal ADDRESS_HAS_ADDRESSTYPE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public ADDRESS_HAS_ADDRESSTYPE_REL Alias(out ADDRESS_HAS_ADDRESSTYPE_ALIAS alias)
		{
			alias = new ADDRESS_HAS_ADDRESSTYPE_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public ADDRESS_HAS_ADDRESSTYPE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public ADDRESS_HAS_ADDRESSTYPE_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL.Alias(out ADDRESS_HAS_ADDRESSTYPE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL.Alias(out ADDRESS_HAS_ADDRESSTYPE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public ADDRESS_HAS_ADDRESSTYPE_IN In { get { return new ADDRESS_HAS_ADDRESSTYPE_IN(this); } }
        public class ADDRESS_HAS_ADDRESSTYPE_IN
        {
            private ADDRESS_HAS_ADDRESSTYPE_REL Parent;
            internal ADDRESS_HAS_ADDRESSTYPE_IN(ADDRESS_HAS_ADDRESSTYPE_REL parent)
            {
                Parent = parent;
            }

			public AddressNode Address { get { return new AddressNode(Parent, DirectionEnum.In); } }
        }

        public ADDRESS_HAS_ADDRESSTYPE_OUT Out { get { return new ADDRESS_HAS_ADDRESSTYPE_OUT(this); } }
        public class ADDRESS_HAS_ADDRESSTYPE_OUT
        {
            private ADDRESS_HAS_ADDRESSTYPE_REL Parent;
            internal ADDRESS_HAS_ADDRESSTYPE_OUT(ADDRESS_HAS_ADDRESSTYPE_REL parent)
            {
                Parent = parent;
            }

			public AddressTypeNode AddressType { get { return new AddressTypeNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL
    {
		IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL Alias(out ADDRESS_HAS_ADDRESSTYPE_ALIAS alias);
		IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL Repeat(int maxHops);
		IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL Repeat(int minHops, int maxHops);

        ADDRESS_HAS_ADDRESSTYPE_REL.ADDRESS_HAS_ADDRESSTYPE_OUT Out { get; }
    }
    public interface IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL
    {
		IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL Alias(out ADDRESS_HAS_ADDRESSTYPE_ALIAS alias);
		IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL Repeat(int maxHops);
		IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL Repeat(int minHops, int maxHops);

        ADDRESS_HAS_ADDRESSTYPE_REL.ADDRESS_HAS_ADDRESSTYPE_IN In { get; }
    }

    public class ADDRESS_HAS_ADDRESSTYPE_ALIAS : AliasResult
    {
		private ADDRESS_HAS_ADDRESSTYPE_REL Parent;

        internal ADDRESS_HAS_ADDRESSTYPE_ALIAS(ADDRESS_HAS_ADDRESSTYPE_REL parent)
        {
			Parent = parent;
        }
    }
}
