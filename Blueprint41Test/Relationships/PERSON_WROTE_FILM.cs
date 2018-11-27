using System;

using Blueprint41;
using Blueprint41.Query;

namespace Datastore.Query
{
public partial class PERSON_WROTE_FILM_REL : RELATIONSHIP, IFromIn_PERSON_WROTE_FILM_REL, IFromOut_PERSON_WROTE_FILM_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "WROTE";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PERSON_WROTE_FILM_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PERSON_WROTE_FILM_REL Alias(out PERSON_WROTE_FILM_ALIAS alias)
		{
			alias = new PERSON_WROTE_FILM_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PERSON_WROTE_FILM_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PERSON_WROTE_FILM_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PERSON_WROTE_FILM_REL IFromIn_PERSON_WROTE_FILM_REL.Alias(out PERSON_WROTE_FILM_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PERSON_WROTE_FILM_REL IFromOut_PERSON_WROTE_FILM_REL.Alias(out PERSON_WROTE_FILM_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PERSON_WROTE_FILM_REL IFromIn_PERSON_WROTE_FILM_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PERSON_WROTE_FILM_REL IFromIn_PERSON_WROTE_FILM_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PERSON_WROTE_FILM_REL IFromOut_PERSON_WROTE_FILM_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PERSON_WROTE_FILM_REL IFromOut_PERSON_WROTE_FILM_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PERSON_WROTE_FILM_IN In { get { return new PERSON_WROTE_FILM_IN(this); } }
        public class PERSON_WROTE_FILM_IN
        {
            private PERSON_WROTE_FILM_REL Parent;
            internal PERSON_WROTE_FILM_IN(PERSON_WROTE_FILM_REL parent)
            {
                Parent = parent;
            }

			public ActorNode Actor { get { return new ActorNode(Parent, DirectionEnum.In); } }
        }

        public PERSON_WROTE_FILM_OUT Out { get { return new PERSON_WROTE_FILM_OUT(this); } }
        public class PERSON_WROTE_FILM_OUT
        {
            private PERSON_WROTE_FILM_REL Parent;
            internal PERSON_WROTE_FILM_OUT(PERSON_WROTE_FILM_REL parent)
            {
                Parent = parent;
            }

			public FilmNode Film { get { return new FilmNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PERSON_WROTE_FILM_REL
    {
		IFromIn_PERSON_WROTE_FILM_REL Alias(out PERSON_WROTE_FILM_ALIAS alias);
		IFromIn_PERSON_WROTE_FILM_REL Repeat(int maxHops);
		IFromIn_PERSON_WROTE_FILM_REL Repeat(int minHops, int maxHops);

        PERSON_WROTE_FILM_REL.PERSON_WROTE_FILM_OUT Out { get; }
    }
    public interface IFromOut_PERSON_WROTE_FILM_REL
    {
		IFromOut_PERSON_WROTE_FILM_REL Alias(out PERSON_WROTE_FILM_ALIAS alias);
		IFromOut_PERSON_WROTE_FILM_REL Repeat(int maxHops);
		IFromOut_PERSON_WROTE_FILM_REL Repeat(int minHops, int maxHops);

        PERSON_WROTE_FILM_REL.PERSON_WROTE_FILM_IN In { get; }
    }

    public class PERSON_WROTE_FILM_ALIAS : AliasResult
    {
		private PERSON_WROTE_FILM_REL Parent;

        internal PERSON_WROTE_FILM_ALIAS(PERSON_WROTE_FILM_REL parent)
        {
			Parent = parent;
        }
    }
}
