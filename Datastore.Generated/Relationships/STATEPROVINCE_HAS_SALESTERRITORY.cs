using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class STATEPROVINCE_HAS_SALESTERRITORY_REL : RELATIONSHIP, IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL, IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_SALESTERRITORY";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal STATEPROVINCE_HAS_SALESTERRITORY_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public STATEPROVINCE_HAS_SALESTERRITORY_REL Alias(out STATEPROVINCE_HAS_SALESTERRITORY_ALIAS alias)
		{
			alias = new STATEPROVINCE_HAS_SALESTERRITORY_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public STATEPROVINCE_HAS_SALESTERRITORY_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public STATEPROVINCE_HAS_SALESTERRITORY_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL.Alias(out STATEPROVINCE_HAS_SALESTERRITORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL.Alias(out STATEPROVINCE_HAS_SALESTERRITORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public STATEPROVINCE_HAS_SALESTERRITORY_IN In { get { return new STATEPROVINCE_HAS_SALESTERRITORY_IN(this); } }
        public class STATEPROVINCE_HAS_SALESTERRITORY_IN
        {
            private STATEPROVINCE_HAS_SALESTERRITORY_REL Parent;
            internal STATEPROVINCE_HAS_SALESTERRITORY_IN(STATEPROVINCE_HAS_SALESTERRITORY_REL parent)
            {
                Parent = parent;
            }

			public StateProvinceNode StateProvince { get { return new StateProvinceNode(Parent, DirectionEnum.In); } }
        }

        public STATEPROVINCE_HAS_SALESTERRITORY_OUT Out { get { return new STATEPROVINCE_HAS_SALESTERRITORY_OUT(this); } }
        public class STATEPROVINCE_HAS_SALESTERRITORY_OUT
        {
            private STATEPROVINCE_HAS_SALESTERRITORY_REL Parent;
            internal STATEPROVINCE_HAS_SALESTERRITORY_OUT(STATEPROVINCE_HAS_SALESTERRITORY_REL parent)
            {
                Parent = parent;
            }

			public SalesTerritoryNode SalesTerritory { get { return new SalesTerritoryNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL
    {
		IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL Alias(out STATEPROVINCE_HAS_SALESTERRITORY_ALIAS alias);
		IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL Repeat(int maxHops);
		IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL Repeat(int minHops, int maxHops);

        STATEPROVINCE_HAS_SALESTERRITORY_REL.STATEPROVINCE_HAS_SALESTERRITORY_OUT Out { get; }
    }
    public interface IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL
    {
		IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL Alias(out STATEPROVINCE_HAS_SALESTERRITORY_ALIAS alias);
		IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL Repeat(int maxHops);
		IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL Repeat(int minHops, int maxHops);

        STATEPROVINCE_HAS_SALESTERRITORY_REL.STATEPROVINCE_HAS_SALESTERRITORY_IN In { get; }
    }

    public class STATEPROVINCE_HAS_SALESTERRITORY_ALIAS : AliasResult
    {
		private STATEPROVINCE_HAS_SALESTERRITORY_REL Parent;

        internal STATEPROVINCE_HAS_SALESTERRITORY_ALIAS(STATEPROVINCE_HAS_SALESTERRITORY_REL parent)
        {
			Parent = parent;
        }
    }
}
