using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class CUSTOMER_HAS_STORE_REL : RELATIONSHIP, IFromIn_CUSTOMER_HAS_STORE_REL, IFromOut_CUSTOMER_HAS_STORE_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_STORE";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal CUSTOMER_HAS_STORE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public CUSTOMER_HAS_STORE_REL Alias(out CUSTOMER_HAS_STORE_ALIAS alias)
		{
			alias = new CUSTOMER_HAS_STORE_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public CUSTOMER_HAS_STORE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public CUSTOMER_HAS_STORE_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_CUSTOMER_HAS_STORE_REL IFromIn_CUSTOMER_HAS_STORE_REL.Alias(out CUSTOMER_HAS_STORE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_CUSTOMER_HAS_STORE_REL IFromOut_CUSTOMER_HAS_STORE_REL.Alias(out CUSTOMER_HAS_STORE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_CUSTOMER_HAS_STORE_REL IFromIn_CUSTOMER_HAS_STORE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_CUSTOMER_HAS_STORE_REL IFromIn_CUSTOMER_HAS_STORE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_CUSTOMER_HAS_STORE_REL IFromOut_CUSTOMER_HAS_STORE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_CUSTOMER_HAS_STORE_REL IFromOut_CUSTOMER_HAS_STORE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public CUSTOMER_HAS_STORE_IN In { get { return new CUSTOMER_HAS_STORE_IN(this); } }
        public class CUSTOMER_HAS_STORE_IN
        {
            private CUSTOMER_HAS_STORE_REL Parent;
            internal CUSTOMER_HAS_STORE_IN(CUSTOMER_HAS_STORE_REL parent)
            {
                Parent = parent;
            }

			public CustomerNode Customer { get { return new CustomerNode(Parent, DirectionEnum.In); } }
        }

        public CUSTOMER_HAS_STORE_OUT Out { get { return new CUSTOMER_HAS_STORE_OUT(this); } }
        public class CUSTOMER_HAS_STORE_OUT
        {
            private CUSTOMER_HAS_STORE_REL Parent;
            internal CUSTOMER_HAS_STORE_OUT(CUSTOMER_HAS_STORE_REL parent)
            {
                Parent = parent;
            }

			public StoreNode Store { get { return new StoreNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_CUSTOMER_HAS_STORE_REL
    {
		IFromIn_CUSTOMER_HAS_STORE_REL Alias(out CUSTOMER_HAS_STORE_ALIAS alias);
		IFromIn_CUSTOMER_HAS_STORE_REL Repeat(int maxHops);
		IFromIn_CUSTOMER_HAS_STORE_REL Repeat(int minHops, int maxHops);

        CUSTOMER_HAS_STORE_REL.CUSTOMER_HAS_STORE_OUT Out { get; }
    }
    public interface IFromOut_CUSTOMER_HAS_STORE_REL
    {
		IFromOut_CUSTOMER_HAS_STORE_REL Alias(out CUSTOMER_HAS_STORE_ALIAS alias);
		IFromOut_CUSTOMER_HAS_STORE_REL Repeat(int maxHops);
		IFromOut_CUSTOMER_HAS_STORE_REL Repeat(int minHops, int maxHops);

        CUSTOMER_HAS_STORE_REL.CUSTOMER_HAS_STORE_IN In { get; }
    }

    public class CUSTOMER_HAS_STORE_ALIAS : AliasResult
    {
		private CUSTOMER_HAS_STORE_REL Parent;

        internal CUSTOMER_HAS_STORE_ALIAS(CUSTOMER_HAS_STORE_REL parent)
        {
			Parent = parent;
        }
    }
}
