using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PERSON_VALID_FOR_CREDITCARD_REL : RELATIONSHIP, IFromIn_PERSON_VALID_FOR_CREDITCARD_REL, IFromOut_PERSON_VALID_FOR_CREDITCARD_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "VALID_FOR_CREDITCARD";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PERSON_VALID_FOR_CREDITCARD_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PERSON_VALID_FOR_CREDITCARD_REL Alias(out PERSON_VALID_FOR_CREDITCARD_ALIAS alias)
		{
			alias = new PERSON_VALID_FOR_CREDITCARD_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PERSON_VALID_FOR_CREDITCARD_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PERSON_VALID_FOR_CREDITCARD_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PERSON_VALID_FOR_CREDITCARD_REL IFromIn_PERSON_VALID_FOR_CREDITCARD_REL.Alias(out PERSON_VALID_FOR_CREDITCARD_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PERSON_VALID_FOR_CREDITCARD_REL IFromOut_PERSON_VALID_FOR_CREDITCARD_REL.Alias(out PERSON_VALID_FOR_CREDITCARD_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PERSON_VALID_FOR_CREDITCARD_REL IFromIn_PERSON_VALID_FOR_CREDITCARD_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PERSON_VALID_FOR_CREDITCARD_REL IFromIn_PERSON_VALID_FOR_CREDITCARD_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PERSON_VALID_FOR_CREDITCARD_REL IFromOut_PERSON_VALID_FOR_CREDITCARD_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PERSON_VALID_FOR_CREDITCARD_REL IFromOut_PERSON_VALID_FOR_CREDITCARD_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PERSON_VALID_FOR_CREDITCARD_IN In { get { return new PERSON_VALID_FOR_CREDITCARD_IN(this); } }
        public class PERSON_VALID_FOR_CREDITCARD_IN
        {
            private PERSON_VALID_FOR_CREDITCARD_REL Parent;
            internal PERSON_VALID_FOR_CREDITCARD_IN(PERSON_VALID_FOR_CREDITCARD_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public PERSON_VALID_FOR_CREDITCARD_OUT Out { get { return new PERSON_VALID_FOR_CREDITCARD_OUT(this); } }
        public class PERSON_VALID_FOR_CREDITCARD_OUT
        {
            private PERSON_VALID_FOR_CREDITCARD_REL Parent;
            internal PERSON_VALID_FOR_CREDITCARD_OUT(PERSON_VALID_FOR_CREDITCARD_REL parent)
            {
                Parent = parent;
            }

			public CreditCardNode CreditCard { get { return new CreditCardNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PERSON_VALID_FOR_CREDITCARD_REL
    {
		IFromIn_PERSON_VALID_FOR_CREDITCARD_REL Alias(out PERSON_VALID_FOR_CREDITCARD_ALIAS alias);
		IFromIn_PERSON_VALID_FOR_CREDITCARD_REL Repeat(int maxHops);
		IFromIn_PERSON_VALID_FOR_CREDITCARD_REL Repeat(int minHops, int maxHops);

        PERSON_VALID_FOR_CREDITCARD_REL.PERSON_VALID_FOR_CREDITCARD_OUT Out { get; }
    }
    public interface IFromOut_PERSON_VALID_FOR_CREDITCARD_REL
    {
		IFromOut_PERSON_VALID_FOR_CREDITCARD_REL Alias(out PERSON_VALID_FOR_CREDITCARD_ALIAS alias);
		IFromOut_PERSON_VALID_FOR_CREDITCARD_REL Repeat(int maxHops);
		IFromOut_PERSON_VALID_FOR_CREDITCARD_REL Repeat(int minHops, int maxHops);

        PERSON_VALID_FOR_CREDITCARD_REL.PERSON_VALID_FOR_CREDITCARD_IN In { get; }
    }

    public class PERSON_VALID_FOR_CREDITCARD_ALIAS : AliasResult
    {
		private PERSON_VALID_FOR_CREDITCARD_REL Parent;

        internal PERSON_VALID_FOR_CREDITCARD_ALIAS(PERSON_VALID_FOR_CREDITCARD_REL parent)
        {
			Parent = parent;
        }
    }
}
