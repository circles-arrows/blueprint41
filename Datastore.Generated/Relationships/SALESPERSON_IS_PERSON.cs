
using Blueprint41;
using Blueprint41.Query;


namespace Domain.Data.Query
{
public partial class SALESPERSON_IS_PERSON_REL : RELATIONSHIP, IFromIn_SALESPERSON_IS_PERSON_REL, IFromOut_SALESPERSON_IS_PERSON_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "IS_PERSON";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal SALESPERSON_IS_PERSON_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public SALESPERSON_IS_PERSON_REL Alias(out SALESPERSON_IS_PERSON_ALIAS alias)
		{
			alias = new SALESPERSON_IS_PERSON_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public SALESPERSON_IS_PERSON_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public SALESPERSON_IS_PERSON_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_SALESPERSON_IS_PERSON_REL IFromIn_SALESPERSON_IS_PERSON_REL.Alias(out SALESPERSON_IS_PERSON_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_SALESPERSON_IS_PERSON_REL IFromOut_SALESPERSON_IS_PERSON_REL.Alias(out SALESPERSON_IS_PERSON_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_SALESPERSON_IS_PERSON_REL IFromIn_SALESPERSON_IS_PERSON_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_SALESPERSON_IS_PERSON_REL IFromIn_SALESPERSON_IS_PERSON_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_SALESPERSON_IS_PERSON_REL IFromOut_SALESPERSON_IS_PERSON_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_SALESPERSON_IS_PERSON_REL IFromOut_SALESPERSON_IS_PERSON_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public SALESPERSON_IS_PERSON_IN In { get { return new SALESPERSON_IS_PERSON_IN(this); } }
        public class SALESPERSON_IS_PERSON_IN
        {
            private SALESPERSON_IS_PERSON_REL Parent;
            internal SALESPERSON_IS_PERSON_IN(SALESPERSON_IS_PERSON_REL parent)
            {
                Parent = parent;
            }

			public SalesPersonNode SalesPerson { get { return new SalesPersonNode(Parent, DirectionEnum.In); } }
        }

        public SALESPERSON_IS_PERSON_OUT Out { get { return new SALESPERSON_IS_PERSON_OUT(this); } }
        public class SALESPERSON_IS_PERSON_OUT
        {
            private SALESPERSON_IS_PERSON_REL Parent;
            internal SALESPERSON_IS_PERSON_OUT(SALESPERSON_IS_PERSON_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_SALESPERSON_IS_PERSON_REL
    {
		IFromIn_SALESPERSON_IS_PERSON_REL Alias(out SALESPERSON_IS_PERSON_ALIAS alias);
		IFromIn_SALESPERSON_IS_PERSON_REL Repeat(int maxHops);
		IFromIn_SALESPERSON_IS_PERSON_REL Repeat(int minHops, int maxHops);

        SALESPERSON_IS_PERSON_REL.SALESPERSON_IS_PERSON_OUT Out { get; }
    }
    public interface IFromOut_SALESPERSON_IS_PERSON_REL
    {
		IFromOut_SALESPERSON_IS_PERSON_REL Alias(out SALESPERSON_IS_PERSON_ALIAS alias);
		IFromOut_SALESPERSON_IS_PERSON_REL Repeat(int maxHops);
		IFromOut_SALESPERSON_IS_PERSON_REL Repeat(int minHops, int maxHops);

        SALESPERSON_IS_PERSON_REL.SALESPERSON_IS_PERSON_IN In { get; }
    }

    public class SALESPERSON_IS_PERSON_ALIAS : AliasResult
    {
		private SALESPERSON_IS_PERSON_REL Parent;

        internal SALESPERSON_IS_PERSON_ALIAS(SALESPERSON_IS_PERSON_REL parent)
        {
			Parent = parent;
        }
    }
}
