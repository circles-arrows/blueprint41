using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PERSON_HAS_EMAILADDRESS_REL : RELATIONSHIP, IFromIn_PERSON_HAS_EMAILADDRESS_REL, IFromOut_PERSON_HAS_EMAILADDRESS_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_EMAILADDRESS";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PERSON_HAS_EMAILADDRESS_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PERSON_HAS_EMAILADDRESS_REL Alias(out PERSON_HAS_EMAILADDRESS_ALIAS alias)
		{
			alias = new PERSON_HAS_EMAILADDRESS_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PERSON_HAS_EMAILADDRESS_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PERSON_HAS_EMAILADDRESS_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PERSON_HAS_EMAILADDRESS_REL IFromIn_PERSON_HAS_EMAILADDRESS_REL.Alias(out PERSON_HAS_EMAILADDRESS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PERSON_HAS_EMAILADDRESS_REL IFromOut_PERSON_HAS_EMAILADDRESS_REL.Alias(out PERSON_HAS_EMAILADDRESS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PERSON_HAS_EMAILADDRESS_REL IFromIn_PERSON_HAS_EMAILADDRESS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PERSON_HAS_EMAILADDRESS_REL IFromIn_PERSON_HAS_EMAILADDRESS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PERSON_HAS_EMAILADDRESS_REL IFromOut_PERSON_HAS_EMAILADDRESS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PERSON_HAS_EMAILADDRESS_REL IFromOut_PERSON_HAS_EMAILADDRESS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PERSON_HAS_EMAILADDRESS_IN In { get { return new PERSON_HAS_EMAILADDRESS_IN(this); } }
        public class PERSON_HAS_EMAILADDRESS_IN
        {
            private PERSON_HAS_EMAILADDRESS_REL Parent;
            internal PERSON_HAS_EMAILADDRESS_IN(PERSON_HAS_EMAILADDRESS_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public PERSON_HAS_EMAILADDRESS_OUT Out { get { return new PERSON_HAS_EMAILADDRESS_OUT(this); } }
        public class PERSON_HAS_EMAILADDRESS_OUT
        {
            private PERSON_HAS_EMAILADDRESS_REL Parent;
            internal PERSON_HAS_EMAILADDRESS_OUT(PERSON_HAS_EMAILADDRESS_REL parent)
            {
                Parent = parent;
            }

			public EmailAddressNode EmailAddress { get { return new EmailAddressNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PERSON_HAS_EMAILADDRESS_REL
    {
		IFromIn_PERSON_HAS_EMAILADDRESS_REL Alias(out PERSON_HAS_EMAILADDRESS_ALIAS alias);
		IFromIn_PERSON_HAS_EMAILADDRESS_REL Repeat(int maxHops);
		IFromIn_PERSON_HAS_EMAILADDRESS_REL Repeat(int minHops, int maxHops);

        PERSON_HAS_EMAILADDRESS_REL.PERSON_HAS_EMAILADDRESS_OUT Out { get; }
    }
    public interface IFromOut_PERSON_HAS_EMAILADDRESS_REL
    {
		IFromOut_PERSON_HAS_EMAILADDRESS_REL Alias(out PERSON_HAS_EMAILADDRESS_ALIAS alias);
		IFromOut_PERSON_HAS_EMAILADDRESS_REL Repeat(int maxHops);
		IFromOut_PERSON_HAS_EMAILADDRESS_REL Repeat(int minHops, int maxHops);

        PERSON_HAS_EMAILADDRESS_REL.PERSON_HAS_EMAILADDRESS_IN In { get; }
    }

    public class PERSON_HAS_EMAILADDRESS_ALIAS : AliasResult
    {
		private PERSON_HAS_EMAILADDRESS_REL Parent;

        internal PERSON_HAS_EMAILADDRESS_ALIAS(PERSON_HAS_EMAILADDRESS_REL parent)
        {
			Parent = parent;
        }
    }
}
