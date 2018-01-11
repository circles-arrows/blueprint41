using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class SALESPERSON_HAS_SALESTERRITORY_REL : RELATIONSHIP, IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL, IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_SALESTERRITORY";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal SALESPERSON_HAS_SALESTERRITORY_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public SALESPERSON_HAS_SALESTERRITORY_REL Alias(out SALESPERSON_HAS_SALESTERRITORY_ALIAS alias)
		{
			alias = new SALESPERSON_HAS_SALESTERRITORY_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public SALESPERSON_HAS_SALESTERRITORY_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public SALESPERSON_HAS_SALESTERRITORY_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL.Alias(out SALESPERSON_HAS_SALESTERRITORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL.Alias(out SALESPERSON_HAS_SALESTERRITORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public SALESPERSON_HAS_SALESTERRITORY_IN In { get { return new SALESPERSON_HAS_SALESTERRITORY_IN(this); } }
        public class SALESPERSON_HAS_SALESTERRITORY_IN
        {
            private SALESPERSON_HAS_SALESTERRITORY_REL Parent;
            internal SALESPERSON_HAS_SALESTERRITORY_IN(SALESPERSON_HAS_SALESTERRITORY_REL parent)
            {
                Parent = parent;
            }

			public SalesPersonNode SalesPerson { get { return new SalesPersonNode(Parent, DirectionEnum.In); } }
        }

        public SALESPERSON_HAS_SALESTERRITORY_OUT Out { get { return new SALESPERSON_HAS_SALESTERRITORY_OUT(this); } }
        public class SALESPERSON_HAS_SALESTERRITORY_OUT
        {
            private SALESPERSON_HAS_SALESTERRITORY_REL Parent;
            internal SALESPERSON_HAS_SALESTERRITORY_OUT(SALESPERSON_HAS_SALESTERRITORY_REL parent)
            {
                Parent = parent;
            }

			public SalesTerritoryNode SalesTerritory { get { return new SalesTerritoryNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL
    {
		IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL Alias(out SALESPERSON_HAS_SALESTERRITORY_ALIAS alias);
		IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL Repeat(int maxHops);
		IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL Repeat(int minHops, int maxHops);

        SALESPERSON_HAS_SALESTERRITORY_REL.SALESPERSON_HAS_SALESTERRITORY_OUT Out { get; }
    }
    public interface IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL
    {
		IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL Alias(out SALESPERSON_HAS_SALESTERRITORY_ALIAS alias);
		IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL Repeat(int maxHops);
		IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL Repeat(int minHops, int maxHops);

        SALESPERSON_HAS_SALESTERRITORY_REL.SALESPERSON_HAS_SALESTERRITORY_IN In { get; }
    }

    public class SALESPERSON_HAS_SALESTERRITORY_ALIAS : AliasResult
    {
		private SALESPERSON_HAS_SALESTERRITORY_REL Parent;

        internal SALESPERSON_HAS_SALESTERRITORY_ALIAS(SALESPERSON_HAS_SALESTERRITORY_REL parent)
        {
			Parent = parent;
        }
    }
}
