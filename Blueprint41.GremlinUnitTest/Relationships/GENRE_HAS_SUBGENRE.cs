using System;

using Blueprint41;
using Blueprint41.Query;

namespace Datastore.Query
{
public partial class GENRE_HAS_SUBGENRE_REL : RELATIONSHIP, IFromIn_GENRE_HAS_SUBGENRE_REL, IFromOut_GENRE_HAS_SUBGENRE_REL, IFromAny_GENRE_HAS_SUBGENRE_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "SUBGENRE";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal GENRE_HAS_SUBGENRE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public GENRE_HAS_SUBGENRE_REL Alias(out GENRE_HAS_SUBGENRE_ALIAS alias)
		{
			alias = new GENRE_HAS_SUBGENRE_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public GENRE_HAS_SUBGENRE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public GENRE_HAS_SUBGENRE_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_GENRE_HAS_SUBGENRE_REL IFromIn_GENRE_HAS_SUBGENRE_REL.Alias(out GENRE_HAS_SUBGENRE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_GENRE_HAS_SUBGENRE_REL IFromOut_GENRE_HAS_SUBGENRE_REL.Alias(out GENRE_HAS_SUBGENRE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_GENRE_HAS_SUBGENRE_REL IFromIn_GENRE_HAS_SUBGENRE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_GENRE_HAS_SUBGENRE_REL IFromIn_GENRE_HAS_SUBGENRE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_GENRE_HAS_SUBGENRE_REL IFromOut_GENRE_HAS_SUBGENRE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_GENRE_HAS_SUBGENRE_REL IFromOut_GENRE_HAS_SUBGENRE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}

		IFromAny_GENRE_HAS_SUBGENRE_REL IFromAny_GENRE_HAS_SUBGENRE_REL.Alias(out GENRE_HAS_SUBGENRE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromAny_GENRE_HAS_SUBGENRE_REL IFromAny_GENRE_HAS_SUBGENRE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromAny_GENRE_HAS_SUBGENRE_REL IFromAny_GENRE_HAS_SUBGENRE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}

		public GENRE_HAS_SUBGENRE_IN In { get { return new GENRE_HAS_SUBGENRE_IN(this); } }
        public class GENRE_HAS_SUBGENRE_IN
        {
            private GENRE_HAS_SUBGENRE_REL Parent;
            internal GENRE_HAS_SUBGENRE_IN(GENRE_HAS_SUBGENRE_REL parent)
            {
                Parent = parent;
            }

			public GenreNode Genre { get { return new GenreNode(Parent, DirectionEnum.In); } }
        }

        public GENRE_HAS_SUBGENRE_OUT Out { get { return new GENRE_HAS_SUBGENRE_OUT(this); } }
        public class GENRE_HAS_SUBGENRE_OUT
        {
            private GENRE_HAS_SUBGENRE_REL Parent;
            internal GENRE_HAS_SUBGENRE_OUT(GENRE_HAS_SUBGENRE_REL parent)
            {
                Parent = parent;
            }

			public GenreNode Genre { get { return new GenreNode(Parent, DirectionEnum.Out); } }
        }

        public GENRE_HAS_SUBGENRE_ANY Any { get { return new GENRE_HAS_SUBGENRE_ANY(this); } }
        public class GENRE_HAS_SUBGENRE_ANY
        {
            private GENRE_HAS_SUBGENRE_REL Parent;
            internal GENRE_HAS_SUBGENRE_ANY(GENRE_HAS_SUBGENRE_REL parent)
            {
                Parent = parent;
            }

			public GenreNode Genre { get { return new GenreNode(Parent, DirectionEnum.None); } }
        }
	}

    public interface IFromIn_GENRE_HAS_SUBGENRE_REL
    {
		IFromIn_GENRE_HAS_SUBGENRE_REL Alias(out GENRE_HAS_SUBGENRE_ALIAS alias);
		IFromIn_GENRE_HAS_SUBGENRE_REL Repeat(int maxHops);
		IFromIn_GENRE_HAS_SUBGENRE_REL Repeat(int minHops, int maxHops);

        GENRE_HAS_SUBGENRE_REL.GENRE_HAS_SUBGENRE_OUT Out { get; }
    }
    public interface IFromOut_GENRE_HAS_SUBGENRE_REL
    {
		IFromOut_GENRE_HAS_SUBGENRE_REL Alias(out GENRE_HAS_SUBGENRE_ALIAS alias);
		IFromOut_GENRE_HAS_SUBGENRE_REL Repeat(int maxHops);
		IFromOut_GENRE_HAS_SUBGENRE_REL Repeat(int minHops, int maxHops);

        GENRE_HAS_SUBGENRE_REL.GENRE_HAS_SUBGENRE_IN In { get; }
    }
    public interface IFromAny_GENRE_HAS_SUBGENRE_REL
    {
		IFromAny_GENRE_HAS_SUBGENRE_REL Alias(out GENRE_HAS_SUBGENRE_ALIAS alias);
		IFromAny_GENRE_HAS_SUBGENRE_REL Repeat(int maxHops);
		IFromAny_GENRE_HAS_SUBGENRE_REL Repeat(int minHops, int maxHops);

        GENRE_HAS_SUBGENRE_REL.GENRE_HAS_SUBGENRE_ANY Any { get; }
    }

    public class GENRE_HAS_SUBGENRE_ALIAS : AliasResult
    {
		private GENRE_HAS_SUBGENRE_REL Parent;

        internal GENRE_HAS_SUBGENRE_ALIAS(GENRE_HAS_SUBGENRE_REL parent)
        {
			Parent = parent;
        }
    }
}
