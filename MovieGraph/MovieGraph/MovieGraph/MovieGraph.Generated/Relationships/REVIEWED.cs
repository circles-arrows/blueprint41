using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class REVIEWED_REL : RELATIONSHIP, IFromIn_REVIEWED_REL, IFromOut_REVIEWED_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "REVIEWED";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal REVIEWED_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public REVIEWED_REL Alias(out REVIEWED_ALIAS alias)
		{
			alias = new REVIEWED_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public REVIEWED_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public REVIEWED_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_REVIEWED_REL IFromIn_REVIEWED_REL.Alias(out REVIEWED_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_REVIEWED_REL IFromOut_REVIEWED_REL.Alias(out REVIEWED_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_REVIEWED_REL IFromIn_REVIEWED_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_REVIEWED_REL IFromIn_REVIEWED_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_REVIEWED_REL IFromOut_REVIEWED_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_REVIEWED_REL IFromOut_REVIEWED_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public REVIEWED_IN In { get { return new REVIEWED_IN(this); } }
        public class REVIEWED_IN
        {
            private REVIEWED_REL Parent;
            internal REVIEWED_IN(REVIEWED_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public REVIEWED_OUT Out { get { return new REVIEWED_OUT(this); } }
        public class REVIEWED_OUT
        {
            private REVIEWED_REL Parent;
            internal REVIEWED_OUT(REVIEWED_REL parent)
            {
                Parent = parent;
            }

			public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_REVIEWED_REL
    {
		IFromIn_REVIEWED_REL Alias(out REVIEWED_ALIAS alias);
		IFromIn_REVIEWED_REL Repeat(int maxHops);
		IFromIn_REVIEWED_REL Repeat(int minHops, int maxHops);

        REVIEWED_REL.REVIEWED_OUT Out { get; }
    }
    public interface IFromOut_REVIEWED_REL
    {
		IFromOut_REVIEWED_REL Alias(out REVIEWED_ALIAS alias);
		IFromOut_REVIEWED_REL Repeat(int maxHops);
		IFromOut_REVIEWED_REL Repeat(int minHops, int maxHops);

        REVIEWED_REL.REVIEWED_IN In { get; }
    }

    public class REVIEWED_ALIAS : AliasResult
    {
		private REVIEWED_REL Parent;

        internal REVIEWED_ALIAS(REVIEWED_REL parent)
        {
			Parent = parent;
        }
    }
}
