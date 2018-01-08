
using Blueprint41;
using Blueprint41.Query;


namespace Domain.Data.Query
{
public partial class PERSON_VALID_FOR_DOCUMENT_REL : RELATIONSHIP, IFromIn_PERSON_VALID_FOR_DOCUMENT_REL, IFromOut_PERSON_VALID_FOR_DOCUMENT_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "VALID_FOR_DOCUMENT";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PERSON_VALID_FOR_DOCUMENT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PERSON_VALID_FOR_DOCUMENT_REL Alias(out PERSON_VALID_FOR_DOCUMENT_ALIAS alias)
		{
			alias = new PERSON_VALID_FOR_DOCUMENT_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PERSON_VALID_FOR_DOCUMENT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PERSON_VALID_FOR_DOCUMENT_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PERSON_VALID_FOR_DOCUMENT_REL IFromIn_PERSON_VALID_FOR_DOCUMENT_REL.Alias(out PERSON_VALID_FOR_DOCUMENT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PERSON_VALID_FOR_DOCUMENT_REL IFromOut_PERSON_VALID_FOR_DOCUMENT_REL.Alias(out PERSON_VALID_FOR_DOCUMENT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PERSON_VALID_FOR_DOCUMENT_REL IFromIn_PERSON_VALID_FOR_DOCUMENT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PERSON_VALID_FOR_DOCUMENT_REL IFromIn_PERSON_VALID_FOR_DOCUMENT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PERSON_VALID_FOR_DOCUMENT_REL IFromOut_PERSON_VALID_FOR_DOCUMENT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PERSON_VALID_FOR_DOCUMENT_REL IFromOut_PERSON_VALID_FOR_DOCUMENT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PERSON_VALID_FOR_DOCUMENT_IN In { get { return new PERSON_VALID_FOR_DOCUMENT_IN(this); } }
        public class PERSON_VALID_FOR_DOCUMENT_IN
        {
            private PERSON_VALID_FOR_DOCUMENT_REL Parent;
            internal PERSON_VALID_FOR_DOCUMENT_IN(PERSON_VALID_FOR_DOCUMENT_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public PERSON_VALID_FOR_DOCUMENT_OUT Out { get { return new PERSON_VALID_FOR_DOCUMENT_OUT(this); } }
        public class PERSON_VALID_FOR_DOCUMENT_OUT
        {
            private PERSON_VALID_FOR_DOCUMENT_REL Parent;
            internal PERSON_VALID_FOR_DOCUMENT_OUT(PERSON_VALID_FOR_DOCUMENT_REL parent)
            {
                Parent = parent;
            }

			public DocumentNode Document { get { return new DocumentNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PERSON_VALID_FOR_DOCUMENT_REL
    {
		IFromIn_PERSON_VALID_FOR_DOCUMENT_REL Alias(out PERSON_VALID_FOR_DOCUMENT_ALIAS alias);
		IFromIn_PERSON_VALID_FOR_DOCUMENT_REL Repeat(int maxHops);
		IFromIn_PERSON_VALID_FOR_DOCUMENT_REL Repeat(int minHops, int maxHops);

        PERSON_VALID_FOR_DOCUMENT_REL.PERSON_VALID_FOR_DOCUMENT_OUT Out { get; }
    }
    public interface IFromOut_PERSON_VALID_FOR_DOCUMENT_REL
    {
		IFromOut_PERSON_VALID_FOR_DOCUMENT_REL Alias(out PERSON_VALID_FOR_DOCUMENT_ALIAS alias);
		IFromOut_PERSON_VALID_FOR_DOCUMENT_REL Repeat(int maxHops);
		IFromOut_PERSON_VALID_FOR_DOCUMENT_REL Repeat(int minHops, int maxHops);

        PERSON_VALID_FOR_DOCUMENT_REL.PERSON_VALID_FOR_DOCUMENT_IN In { get; }
    }

    public class PERSON_VALID_FOR_DOCUMENT_ALIAS : AliasResult
    {
		private PERSON_VALID_FOR_DOCUMENT_REL Parent;

        internal PERSON_VALID_FOR_DOCUMENT_ALIAS(PERSON_VALID_FOR_DOCUMENT_REL parent)
        {
			Parent = parent;
        }
    }
}
