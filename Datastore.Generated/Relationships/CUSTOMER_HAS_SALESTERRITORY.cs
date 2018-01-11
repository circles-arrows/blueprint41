using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class CUSTOMER_HAS_SALESTERRITORY_REL : RELATIONSHIP, IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL, IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_SALESTERRITORY";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal CUSTOMER_HAS_SALESTERRITORY_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public CUSTOMER_HAS_SALESTERRITORY_REL Alias(out CUSTOMER_HAS_SALESTERRITORY_ALIAS alias)
		{
			alias = new CUSTOMER_HAS_SALESTERRITORY_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public CUSTOMER_HAS_SALESTERRITORY_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public CUSTOMER_HAS_SALESTERRITORY_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL.Alias(out CUSTOMER_HAS_SALESTERRITORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL.Alias(out CUSTOMER_HAS_SALESTERRITORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public CUSTOMER_HAS_SALESTERRITORY_IN In { get { return new CUSTOMER_HAS_SALESTERRITORY_IN(this); } }
        public class CUSTOMER_HAS_SALESTERRITORY_IN
        {
            private CUSTOMER_HAS_SALESTERRITORY_REL Parent;
            internal CUSTOMER_HAS_SALESTERRITORY_IN(CUSTOMER_HAS_SALESTERRITORY_REL parent)
            {
                Parent = parent;
            }

			public CustomerNode Customer { get { return new CustomerNode(Parent, DirectionEnum.In); } }
        }

        public CUSTOMER_HAS_SALESTERRITORY_OUT Out { get { return new CUSTOMER_HAS_SALESTERRITORY_OUT(this); } }
        public class CUSTOMER_HAS_SALESTERRITORY_OUT
        {
            private CUSTOMER_HAS_SALESTERRITORY_REL Parent;
            internal CUSTOMER_HAS_SALESTERRITORY_OUT(CUSTOMER_HAS_SALESTERRITORY_REL parent)
            {
                Parent = parent;
            }

			public SalesTerritoryNode SalesTerritory { get { return new SalesTerritoryNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL
    {
		IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL Alias(out CUSTOMER_HAS_SALESTERRITORY_ALIAS alias);
		IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL Repeat(int maxHops);
		IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL Repeat(int minHops, int maxHops);

        CUSTOMER_HAS_SALESTERRITORY_REL.CUSTOMER_HAS_SALESTERRITORY_OUT Out { get; }
    }
    public interface IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL
    {
		IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL Alias(out CUSTOMER_HAS_SALESTERRITORY_ALIAS alias);
		IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL Repeat(int maxHops);
		IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL Repeat(int minHops, int maxHops);

        CUSTOMER_HAS_SALESTERRITORY_REL.CUSTOMER_HAS_SALESTERRITORY_IN In { get; }
    }

    public class CUSTOMER_HAS_SALESTERRITORY_ALIAS : AliasResult
    {
		private CUSTOMER_HAS_SALESTERRITORY_REL Parent;

        internal CUSTOMER_HAS_SALESTERRITORY_ALIAS(CUSTOMER_HAS_SALESTERRITORY_REL parent)
        {
			Parent = parent;
        }
    }
}
