using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class DIRECTED_REL : RELATIONSHIP, IFromIn_DIRECTED_REL, IFromOut_DIRECTED_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "DIRECTED";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal DIRECTED_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public DIRECTED_REL Alias(out DIRECTED_ALIAS alias)
		{
			alias = new DIRECTED_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public DIRECTED_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public DIRECTED_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_DIRECTED_REL IFromIn_DIRECTED_REL.Alias(out DIRECTED_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_DIRECTED_REL IFromOut_DIRECTED_REL.Alias(out DIRECTED_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_DIRECTED_REL IFromIn_DIRECTED_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_DIRECTED_REL IFromIn_DIRECTED_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_DIRECTED_REL IFromOut_DIRECTED_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_DIRECTED_REL IFromOut_DIRECTED_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public DIRECTED_IN In { get { return new DIRECTED_IN(this); } }
        public class DIRECTED_IN
        {
            private DIRECTED_REL Parent;
            internal DIRECTED_IN(DIRECTED_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public DIRECTED_OUT Out { get { return new DIRECTED_OUT(this); } }
        public class DIRECTED_OUT
        {
            private DIRECTED_REL Parent;
            internal DIRECTED_OUT(DIRECTED_REL parent)
            {
                Parent = parent;
            }

			public MovieNode Movie { get { return new MovieNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_DIRECTED_REL
    {
		IFromIn_DIRECTED_REL Alias(out DIRECTED_ALIAS alias);
		IFromIn_DIRECTED_REL Repeat(int maxHops);
		IFromIn_DIRECTED_REL Repeat(int minHops, int maxHops);

        DIRECTED_REL.DIRECTED_OUT Out { get; }
    }
    public interface IFromOut_DIRECTED_REL
    {
		IFromOut_DIRECTED_REL Alias(out DIRECTED_ALIAS alias);
		IFromOut_DIRECTED_REL Repeat(int maxHops);
		IFromOut_DIRECTED_REL Repeat(int minHops, int maxHops);

        DIRECTED_REL.DIRECTED_IN In { get; }
    }

    public class DIRECTED_ALIAS : AliasResult
    {
		private DIRECTED_REL Parent;

        internal DIRECTED_ALIAS(DIRECTED_REL parent)
        {
			Parent = parent;
        }
    }
}
