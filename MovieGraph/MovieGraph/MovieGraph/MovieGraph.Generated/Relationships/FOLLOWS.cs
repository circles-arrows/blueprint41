using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class FOLLOWS_REL : RELATIONSHIP, IFromIn_FOLLOWS_REL, IFromOut_FOLLOWS_REL, IFromAny_FOLLOWS_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "FOLLOWS";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal FOLLOWS_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public FOLLOWS_REL Alias(out FOLLOWS_ALIAS alias)
		{
			alias = new FOLLOWS_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public FOLLOWS_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public FOLLOWS_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_FOLLOWS_REL IFromIn_FOLLOWS_REL.Alias(out FOLLOWS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_FOLLOWS_REL IFromOut_FOLLOWS_REL.Alias(out FOLLOWS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_FOLLOWS_REL IFromIn_FOLLOWS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_FOLLOWS_REL IFromIn_FOLLOWS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_FOLLOWS_REL IFromOut_FOLLOWS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_FOLLOWS_REL IFromOut_FOLLOWS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}

		IFromAny_FOLLOWS_REL IFromAny_FOLLOWS_REL.Alias(out FOLLOWS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromAny_FOLLOWS_REL IFromAny_FOLLOWS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromAny_FOLLOWS_REL IFromAny_FOLLOWS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}

		public FOLLOWS_IN In { get { return new FOLLOWS_IN(this); } }
        public class FOLLOWS_IN
        {
            private FOLLOWS_REL Parent;
            internal FOLLOWS_IN(FOLLOWS_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
        }

        public FOLLOWS_OUT Out { get { return new FOLLOWS_OUT(this); } }
        public class FOLLOWS_OUT
        {
            private FOLLOWS_REL Parent;
            internal FOLLOWS_OUT(FOLLOWS_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.Out); } }
        }

        public FOLLOWS_ANY Any { get { return new FOLLOWS_ANY(this); } }
        public class FOLLOWS_ANY
        {
            private FOLLOWS_REL Parent;
            internal FOLLOWS_ANY(FOLLOWS_REL parent)
            {
                Parent = parent;
            }

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.None); } }
        }
	}

    public interface IFromIn_FOLLOWS_REL
    {
		IFromIn_FOLLOWS_REL Alias(out FOLLOWS_ALIAS alias);
		IFromIn_FOLLOWS_REL Repeat(int maxHops);
		IFromIn_FOLLOWS_REL Repeat(int minHops, int maxHops);

        FOLLOWS_REL.FOLLOWS_OUT Out { get; }
    }
    public interface IFromOut_FOLLOWS_REL
    {
		IFromOut_FOLLOWS_REL Alias(out FOLLOWS_ALIAS alias);
		IFromOut_FOLLOWS_REL Repeat(int maxHops);
		IFromOut_FOLLOWS_REL Repeat(int minHops, int maxHops);

        FOLLOWS_REL.FOLLOWS_IN In { get; }
    }
    public interface IFromAny_FOLLOWS_REL
    {
		IFromAny_FOLLOWS_REL Alias(out FOLLOWS_ALIAS alias);
		IFromAny_FOLLOWS_REL Repeat(int maxHops);
		IFromAny_FOLLOWS_REL Repeat(int minHops, int maxHops);

        FOLLOWS_REL.FOLLOWS_ANY Any { get; }
    }

    public class FOLLOWS_ALIAS : AliasResult
    {
		private FOLLOWS_REL Parent;

        internal FOLLOWS_ALIAS(FOLLOWS_REL parent)
        {
			Parent = parent;
        }
    }
}
