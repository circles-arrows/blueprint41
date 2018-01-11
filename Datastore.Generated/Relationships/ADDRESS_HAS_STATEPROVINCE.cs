using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class ADDRESS_HAS_STATEPROVINCE_REL : RELATIONSHIP, IFromIn_ADDRESS_HAS_STATEPROVINCE_REL, IFromOut_ADDRESS_HAS_STATEPROVINCE_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_STATEPROVINCE";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal ADDRESS_HAS_STATEPROVINCE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public ADDRESS_HAS_STATEPROVINCE_REL Alias(out ADDRESS_HAS_STATEPROVINCE_ALIAS alias)
		{
			alias = new ADDRESS_HAS_STATEPROVINCE_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public ADDRESS_HAS_STATEPROVINCE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public ADDRESS_HAS_STATEPROVINCE_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_ADDRESS_HAS_STATEPROVINCE_REL IFromIn_ADDRESS_HAS_STATEPROVINCE_REL.Alias(out ADDRESS_HAS_STATEPROVINCE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_ADDRESS_HAS_STATEPROVINCE_REL IFromOut_ADDRESS_HAS_STATEPROVINCE_REL.Alias(out ADDRESS_HAS_STATEPROVINCE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_ADDRESS_HAS_STATEPROVINCE_REL IFromIn_ADDRESS_HAS_STATEPROVINCE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_ADDRESS_HAS_STATEPROVINCE_REL IFromIn_ADDRESS_HAS_STATEPROVINCE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_ADDRESS_HAS_STATEPROVINCE_REL IFromOut_ADDRESS_HAS_STATEPROVINCE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_ADDRESS_HAS_STATEPROVINCE_REL IFromOut_ADDRESS_HAS_STATEPROVINCE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public ADDRESS_HAS_STATEPROVINCE_IN In { get { return new ADDRESS_HAS_STATEPROVINCE_IN(this); } }
        public class ADDRESS_HAS_STATEPROVINCE_IN
        {
            private ADDRESS_HAS_STATEPROVINCE_REL Parent;
            internal ADDRESS_HAS_STATEPROVINCE_IN(ADDRESS_HAS_STATEPROVINCE_REL parent)
            {
                Parent = parent;
            }

			public AddressNode Address { get { return new AddressNode(Parent, DirectionEnum.In); } }
        }

        public ADDRESS_HAS_STATEPROVINCE_OUT Out { get { return new ADDRESS_HAS_STATEPROVINCE_OUT(this); } }
        public class ADDRESS_HAS_STATEPROVINCE_OUT
        {
            private ADDRESS_HAS_STATEPROVINCE_REL Parent;
            internal ADDRESS_HAS_STATEPROVINCE_OUT(ADDRESS_HAS_STATEPROVINCE_REL parent)
            {
                Parent = parent;
            }

			public StateProvinceNode StateProvince { get { return new StateProvinceNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_ADDRESS_HAS_STATEPROVINCE_REL
    {
		IFromIn_ADDRESS_HAS_STATEPROVINCE_REL Alias(out ADDRESS_HAS_STATEPROVINCE_ALIAS alias);
		IFromIn_ADDRESS_HAS_STATEPROVINCE_REL Repeat(int maxHops);
		IFromIn_ADDRESS_HAS_STATEPROVINCE_REL Repeat(int minHops, int maxHops);

        ADDRESS_HAS_STATEPROVINCE_REL.ADDRESS_HAS_STATEPROVINCE_OUT Out { get; }
    }
    public interface IFromOut_ADDRESS_HAS_STATEPROVINCE_REL
    {
		IFromOut_ADDRESS_HAS_STATEPROVINCE_REL Alias(out ADDRESS_HAS_STATEPROVINCE_ALIAS alias);
		IFromOut_ADDRESS_HAS_STATEPROVINCE_REL Repeat(int maxHops);
		IFromOut_ADDRESS_HAS_STATEPROVINCE_REL Repeat(int minHops, int maxHops);

        ADDRESS_HAS_STATEPROVINCE_REL.ADDRESS_HAS_STATEPROVINCE_IN In { get; }
    }

    public class ADDRESS_HAS_STATEPROVINCE_ALIAS : AliasResult
    {
		private ADDRESS_HAS_STATEPROVINCE_REL Parent;

        internal ADDRESS_HAS_STATEPROVINCE_ALIAS(ADDRESS_HAS_STATEPROVINCE_REL parent)
        {
			Parent = parent;
        }
    }
}
