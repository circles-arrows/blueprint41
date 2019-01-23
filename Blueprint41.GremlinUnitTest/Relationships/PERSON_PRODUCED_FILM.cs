using System;

using Blueprint41;
using Blueprint41.Query;

namespace Datastore.Query
{
public partial class PERSON_PRODUCED_FILM_REL : RELATIONSHIP, IFromIn_PERSON_PRODUCED_FILM_REL, IFromOut_PERSON_PRODUCED_FILM_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "PRODUCED";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PERSON_PRODUCED_FILM_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PERSON_PRODUCED_FILM_REL Alias(out PERSON_PRODUCED_FILM_ALIAS alias)
		{
			alias = new PERSON_PRODUCED_FILM_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PERSON_PRODUCED_FILM_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PERSON_PRODUCED_FILM_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PERSON_PRODUCED_FILM_REL IFromIn_PERSON_PRODUCED_FILM_REL.Alias(out PERSON_PRODUCED_FILM_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PERSON_PRODUCED_FILM_REL IFromOut_PERSON_PRODUCED_FILM_REL.Alias(out PERSON_PRODUCED_FILM_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PERSON_PRODUCED_FILM_REL IFromIn_PERSON_PRODUCED_FILM_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PERSON_PRODUCED_FILM_REL IFromIn_PERSON_PRODUCED_FILM_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PERSON_PRODUCED_FILM_REL IFromOut_PERSON_PRODUCED_FILM_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PERSON_PRODUCED_FILM_REL IFromOut_PERSON_PRODUCED_FILM_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PERSON_PRODUCED_FILM_IN In { get { return new PERSON_PRODUCED_FILM_IN(this); } }
        public class PERSON_PRODUCED_FILM_IN
        {
            private PERSON_PRODUCED_FILM_REL Parent;
            internal PERSON_PRODUCED_FILM_IN(PERSON_PRODUCED_FILM_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public PERSON_PRODUCED_FILM_OUT Out { get { return new PERSON_PRODUCED_FILM_OUT(this); } }
        public class PERSON_PRODUCED_FILM_OUT
        {
            private PERSON_PRODUCED_FILM_REL Parent;
            internal PERSON_PRODUCED_FILM_OUT(PERSON_PRODUCED_FILM_REL parent)
            {
                Parent = parent;
            }

			public FilmNode Film { get { return new FilmNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PERSON_PRODUCED_FILM_REL
    {
		IFromIn_PERSON_PRODUCED_FILM_REL Alias(out PERSON_PRODUCED_FILM_ALIAS alias);
		IFromIn_PERSON_PRODUCED_FILM_REL Repeat(int maxHops);
		IFromIn_PERSON_PRODUCED_FILM_REL Repeat(int minHops, int maxHops);

        PERSON_PRODUCED_FILM_REL.PERSON_PRODUCED_FILM_OUT Out { get; }
    }
    public interface IFromOut_PERSON_PRODUCED_FILM_REL
    {
		IFromOut_PERSON_PRODUCED_FILM_REL Alias(out PERSON_PRODUCED_FILM_ALIAS alias);
		IFromOut_PERSON_PRODUCED_FILM_REL Repeat(int maxHops);
		IFromOut_PERSON_PRODUCED_FILM_REL Repeat(int minHops, int maxHops);

        PERSON_PRODUCED_FILM_REL.PERSON_PRODUCED_FILM_IN In { get; }
    }

    public class PERSON_PRODUCED_FILM_ALIAS : AliasResult
    {
		private PERSON_PRODUCED_FILM_REL Parent;

        internal PERSON_PRODUCED_FILM_ALIAS(PERSON_PRODUCED_FILM_REL parent)
        {
			Parent = parent;
        }
    }
}