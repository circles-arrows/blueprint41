
using Blueprint41;
using Blueprint41.Query;


namespace Domain.Data.Query
{
public partial class PERSON_HAS_ADDRESS_REL : RELATIONSHIP, IFromIn_PERSON_HAS_ADDRESS_REL, IFromOut_PERSON_HAS_ADDRESS_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_ADDRESS";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PERSON_HAS_ADDRESS_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PERSON_HAS_ADDRESS_REL Alias(out PERSON_HAS_ADDRESS_ALIAS alias)
		{
			alias = new PERSON_HAS_ADDRESS_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PERSON_HAS_ADDRESS_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PERSON_HAS_ADDRESS_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PERSON_HAS_ADDRESS_REL IFromIn_PERSON_HAS_ADDRESS_REL.Alias(out PERSON_HAS_ADDRESS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PERSON_HAS_ADDRESS_REL IFromOut_PERSON_HAS_ADDRESS_REL.Alias(out PERSON_HAS_ADDRESS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PERSON_HAS_ADDRESS_REL IFromIn_PERSON_HAS_ADDRESS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PERSON_HAS_ADDRESS_REL IFromIn_PERSON_HAS_ADDRESS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PERSON_HAS_ADDRESS_REL IFromOut_PERSON_HAS_ADDRESS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PERSON_HAS_ADDRESS_REL IFromOut_PERSON_HAS_ADDRESS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PERSON_HAS_ADDRESS_IN In { get { return new PERSON_HAS_ADDRESS_IN(this); } }
        public class PERSON_HAS_ADDRESS_IN
        {
            private PERSON_HAS_ADDRESS_REL Parent;
            internal PERSON_HAS_ADDRESS_IN(PERSON_HAS_ADDRESS_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public PERSON_HAS_ADDRESS_OUT Out { get { return new PERSON_HAS_ADDRESS_OUT(this); } }
        public class PERSON_HAS_ADDRESS_OUT
        {
            private PERSON_HAS_ADDRESS_REL Parent;
            internal PERSON_HAS_ADDRESS_OUT(PERSON_HAS_ADDRESS_REL parent)
            {
                Parent = parent;
            }

			public AddressNode Address { get { return new AddressNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PERSON_HAS_ADDRESS_REL
    {
		IFromIn_PERSON_HAS_ADDRESS_REL Alias(out PERSON_HAS_ADDRESS_ALIAS alias);
		IFromIn_PERSON_HAS_ADDRESS_REL Repeat(int maxHops);
		IFromIn_PERSON_HAS_ADDRESS_REL Repeat(int minHops, int maxHops);

        PERSON_HAS_ADDRESS_REL.PERSON_HAS_ADDRESS_OUT Out { get; }
    }
    public interface IFromOut_PERSON_HAS_ADDRESS_REL
    {
		IFromOut_PERSON_HAS_ADDRESS_REL Alias(out PERSON_HAS_ADDRESS_ALIAS alias);
		IFromOut_PERSON_HAS_ADDRESS_REL Repeat(int maxHops);
		IFromOut_PERSON_HAS_ADDRESS_REL Repeat(int minHops, int maxHops);

        PERSON_HAS_ADDRESS_REL.PERSON_HAS_ADDRESS_IN In { get; }
    }

    public class PERSON_HAS_ADDRESS_ALIAS : AliasResult
    {
		private PERSON_HAS_ADDRESS_REL Parent;

        internal PERSON_HAS_ADDRESS_ALIAS(PERSON_HAS_ADDRESS_REL parent)
        {
			Parent = parent;
        }
    }
}
