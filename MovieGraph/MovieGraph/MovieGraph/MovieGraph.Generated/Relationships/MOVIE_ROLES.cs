using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class MOVIE_ROLES_REL : RELATIONSHIP, IFromIn_MOVIE_ROLES_REL, IFromOut_MOVIE_ROLES_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "MOVIE_ROLES";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal MOVIE_ROLES_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public MOVIE_ROLES_REL Alias(out MOVIE_ROLES_ALIAS alias)
		{
			alias = new MOVIE_ROLES_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public MOVIE_ROLES_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public MOVIE_ROLES_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_MOVIE_ROLES_REL IFromIn_MOVIE_ROLES_REL.Alias(out MOVIE_ROLES_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_MOVIE_ROLES_REL IFromOut_MOVIE_ROLES_REL.Alias(out MOVIE_ROLES_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_MOVIE_ROLES_REL IFromIn_MOVIE_ROLES_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_MOVIE_ROLES_REL IFromIn_MOVIE_ROLES_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_MOVIE_ROLES_REL IFromOut_MOVIE_ROLES_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_MOVIE_ROLES_REL IFromOut_MOVIE_ROLES_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public MOVIE_ROLES_IN In { get { return new MOVIE_ROLES_IN(this); } }
        public class MOVIE_ROLES_IN
        {
            private MOVIE_ROLES_REL Parent;
            internal MOVIE_ROLES_IN(MOVIE_ROLES_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public MOVIE_ROLES_OUT Out { get { return new MOVIE_ROLES_OUT(this); } }
        public class MOVIE_ROLES_OUT
        {
            private MOVIE_ROLES_REL Parent;
            internal MOVIE_ROLES_OUT(MOVIE_ROLES_REL parent)
            {
                Parent = parent;
            }

			public MovieRoleNode MovieRole { get { return new MovieRoleNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_MOVIE_ROLES_REL
    {
		IFromIn_MOVIE_ROLES_REL Alias(out MOVIE_ROLES_ALIAS alias);
		IFromIn_MOVIE_ROLES_REL Repeat(int maxHops);
		IFromIn_MOVIE_ROLES_REL Repeat(int minHops, int maxHops);

        MOVIE_ROLES_REL.MOVIE_ROLES_OUT Out { get; }
    }
    public interface IFromOut_MOVIE_ROLES_REL
    {
		IFromOut_MOVIE_ROLES_REL Alias(out MOVIE_ROLES_ALIAS alias);
		IFromOut_MOVIE_ROLES_REL Repeat(int maxHops);
		IFromOut_MOVIE_ROLES_REL Repeat(int minHops, int maxHops);

        MOVIE_ROLES_REL.MOVIE_ROLES_IN In { get; }
    }

    public class MOVIE_ROLES_ALIAS : AliasResult
    {
		private MOVIE_ROLES_REL Parent;

        internal MOVIE_ROLES_ALIAS(MOVIE_ROLES_REL parent)
        {
			Parent = parent;
        }
    }
}
