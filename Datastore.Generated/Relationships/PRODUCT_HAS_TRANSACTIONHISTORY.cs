
using Blueprint41;
using Blueprint41.Query;


namespace Domain.Data.Query
{
public partial class PRODUCT_HAS_TRANSACTIONHISTORY_REL : RELATIONSHIP, IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL, IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_TRANSACTIONHISTORY";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal PRODUCT_HAS_TRANSACTIONHISTORY_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCT_HAS_TRANSACTIONHISTORY_REL Alias(out PRODUCT_HAS_TRANSACTIONHISTORY_ALIAS alias)
		{
			alias = new PRODUCT_HAS_TRANSACTIONHISTORY_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public PRODUCT_HAS_TRANSACTIONHISTORY_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public PRODUCT_HAS_TRANSACTIONHISTORY_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL.Alias(out PRODUCT_HAS_TRANSACTIONHISTORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL.Alias(out PRODUCT_HAS_TRANSACTIONHISTORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCT_HAS_TRANSACTIONHISTORY_IN In { get { return new PRODUCT_HAS_TRANSACTIONHISTORY_IN(this); } }
        public class PRODUCT_HAS_TRANSACTIONHISTORY_IN
        {
            private PRODUCT_HAS_TRANSACTIONHISTORY_REL Parent;
            internal PRODUCT_HAS_TRANSACTIONHISTORY_IN(PRODUCT_HAS_TRANSACTIONHISTORY_REL parent)
            {
                Parent = parent;
            }

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.In); } }
        }

        public PRODUCT_HAS_TRANSACTIONHISTORY_OUT Out { get { return new PRODUCT_HAS_TRANSACTIONHISTORY_OUT(this); } }
        public class PRODUCT_HAS_TRANSACTIONHISTORY_OUT
        {
            private PRODUCT_HAS_TRANSACTIONHISTORY_REL Parent;
            internal PRODUCT_HAS_TRANSACTIONHISTORY_OUT(PRODUCT_HAS_TRANSACTIONHISTORY_REL parent)
            {
                Parent = parent;
            }

			public TransactionHistoryNode TransactionHistory { get { return new TransactionHistoryNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL
    {
		IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL Alias(out PRODUCT_HAS_TRANSACTIONHISTORY_ALIAS alias);
		IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL Repeat(int maxHops);
		IFromIn_PRODUCT_HAS_TRANSACTIONHISTORY_REL Repeat(int minHops, int maxHops);

        PRODUCT_HAS_TRANSACTIONHISTORY_REL.PRODUCT_HAS_TRANSACTIONHISTORY_OUT Out { get; }
    }
    public interface IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL
    {
		IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL Alias(out PRODUCT_HAS_TRANSACTIONHISTORY_ALIAS alias);
		IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL Repeat(int maxHops);
		IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL Repeat(int minHops, int maxHops);

        PRODUCT_HAS_TRANSACTIONHISTORY_REL.PRODUCT_HAS_TRANSACTIONHISTORY_IN In { get; }
    }

    public class PRODUCT_HAS_TRANSACTIONHISTORY_ALIAS : AliasResult
    {
		private PRODUCT_HAS_TRANSACTIONHISTORY_REL Parent;

        internal PRODUCT_HAS_TRANSACTIONHISTORY_ALIAS(PRODUCT_HAS_TRANSACTIONHISTORY_REL parent)
        {
			Parent = parent;
        }
    }
}
