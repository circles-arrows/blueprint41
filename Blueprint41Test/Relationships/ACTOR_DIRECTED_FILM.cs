using System;

using Blueprint41;
using Blueprint41.Query;

namespace Datastore.Query
{
public partial class ACTOR_DIRECTED_FILM_REL : RELATIONSHIP, IFromIn_ACTOR_DIRECTED_FILM_REL, IFromOut_ACTOR_DIRECTED_FILM_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "ACTOR_DIRECTED_FILM";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal ACTOR_DIRECTED_FILM_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public ACTOR_DIRECTED_FILM_REL Alias(out ACTOR_DIRECTED_FILM_ALIAS alias)
		{
			alias = new ACTOR_DIRECTED_FILM_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public ACTOR_DIRECTED_FILM_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public ACTOR_DIRECTED_FILM_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_ACTOR_DIRECTED_FILM_REL IFromIn_ACTOR_DIRECTED_FILM_REL.Alias(out ACTOR_DIRECTED_FILM_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_ACTOR_DIRECTED_FILM_REL IFromOut_ACTOR_DIRECTED_FILM_REL.Alias(out ACTOR_DIRECTED_FILM_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_ACTOR_DIRECTED_FILM_REL IFromIn_ACTOR_DIRECTED_FILM_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_ACTOR_DIRECTED_FILM_REL IFromIn_ACTOR_DIRECTED_FILM_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_ACTOR_DIRECTED_FILM_REL IFromOut_ACTOR_DIRECTED_FILM_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_ACTOR_DIRECTED_FILM_REL IFromOut_ACTOR_DIRECTED_FILM_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public ACTOR_DIRECTED_FILM_IN In { get { return new ACTOR_DIRECTED_FILM_IN(this); } }
        public class ACTOR_DIRECTED_FILM_IN
        {
            private ACTOR_DIRECTED_FILM_REL Parent;
            internal ACTOR_DIRECTED_FILM_IN(ACTOR_DIRECTED_FILM_REL parent)
            {
                Parent = parent;
            }

			public ActorNode Actor { get { return new ActorNode(Parent, DirectionEnum.In); } }
        }

        public ACTOR_DIRECTED_FILM_OUT Out { get { return new ACTOR_DIRECTED_FILM_OUT(this); } }
        public class ACTOR_DIRECTED_FILM_OUT
        {
            private ACTOR_DIRECTED_FILM_REL Parent;
            internal ACTOR_DIRECTED_FILM_OUT(ACTOR_DIRECTED_FILM_REL parent)
            {
                Parent = parent;
            }

			public FilmNode Film { get { return new FilmNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_ACTOR_DIRECTED_FILM_REL
    {
		IFromIn_ACTOR_DIRECTED_FILM_REL Alias(out ACTOR_DIRECTED_FILM_ALIAS alias);
		IFromIn_ACTOR_DIRECTED_FILM_REL Repeat(int maxHops);
		IFromIn_ACTOR_DIRECTED_FILM_REL Repeat(int minHops, int maxHops);

        ACTOR_DIRECTED_FILM_REL.ACTOR_DIRECTED_FILM_OUT Out { get; }
    }
    public interface IFromOut_ACTOR_DIRECTED_FILM_REL
    {
		IFromOut_ACTOR_DIRECTED_FILM_REL Alias(out ACTOR_DIRECTED_FILM_ALIAS alias);
		IFromOut_ACTOR_DIRECTED_FILM_REL Repeat(int maxHops);
		IFromOut_ACTOR_DIRECTED_FILM_REL Repeat(int minHops, int maxHops);

        ACTOR_DIRECTED_FILM_REL.ACTOR_DIRECTED_FILM_IN In { get; }
    }

    public class ACTOR_DIRECTED_FILM_ALIAS : AliasResult
    {
		private ACTOR_DIRECTED_FILM_REL Parent;

        internal ACTOR_DIRECTED_FILM_ALIAS(ACTOR_DIRECTED_FILM_REL parent)
        {
			Parent = parent;
        }
    }
}
