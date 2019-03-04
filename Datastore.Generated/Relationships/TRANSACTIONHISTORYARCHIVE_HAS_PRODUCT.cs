using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL : RELATIONSHIP, IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL, IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_PRODUCT";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Alias(out TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_ALIAS alias)
		{
			alias = new TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL.Alias(out TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL.Alias(out TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_IN In { get { return new TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_IN(this); } }
        public class TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_IN
        {
            private TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Parent;
            internal TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_IN(TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL parent)
            {
                Parent = parent;
            }

			public TransactionHistoryArchiveNode TransactionHistoryArchive { get { return new TransactionHistoryArchiveNode(Parent, DirectionEnum.In); } }
        }

        public TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_OUT Out { get { return new TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_OUT(this); } }
        public class TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_OUT
        {
            private TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Parent;
            internal TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_OUT(TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL parent)
            {
                Parent = parent;
            }

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL
    {
		IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Alias(out TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_ALIAS alias);
		IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

        TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL.TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_OUT Out { get; }
    }
    public interface IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL
    {
		IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Alias(out TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_ALIAS alias);
		IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromOut_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

        TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL.TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_IN In { get; }
    }

    public class TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_ALIAS : AliasResult
    {
		private TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL Parent;

        internal TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_ALIAS(TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL parent)
        {
			Parent = parent;
        }
    }
}
