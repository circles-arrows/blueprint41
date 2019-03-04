using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class SALESORDERHEADER_HAS_ADDRESS_REL : RELATIONSHIP, IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL, IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_ADDRESS";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal SALESORDERHEADER_HAS_ADDRESS_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public SALESORDERHEADER_HAS_ADDRESS_REL Alias(out SALESORDERHEADER_HAS_ADDRESS_ALIAS alias)
		{
			alias = new SALESORDERHEADER_HAS_ADDRESS_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public SALESORDERHEADER_HAS_ADDRESS_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public SALESORDERHEADER_HAS_ADDRESS_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL.Alias(out SALESORDERHEADER_HAS_ADDRESS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL.Alias(out SALESORDERHEADER_HAS_ADDRESS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public SALESORDERHEADER_HAS_ADDRESS_IN In { get { return new SALESORDERHEADER_HAS_ADDRESS_IN(this); } }
        public class SALESORDERHEADER_HAS_ADDRESS_IN
        {
            private SALESORDERHEADER_HAS_ADDRESS_REL Parent;
            internal SALESORDERHEADER_HAS_ADDRESS_IN(SALESORDERHEADER_HAS_ADDRESS_REL parent)
            {
                Parent = parent;
            }

			public SalesOrderHeaderNode SalesOrderHeader { get { return new SalesOrderHeaderNode(Parent, DirectionEnum.In); } }
        }

        public SALESORDERHEADER_HAS_ADDRESS_OUT Out { get { return new SALESORDERHEADER_HAS_ADDRESS_OUT(this); } }
        public class SALESORDERHEADER_HAS_ADDRESS_OUT
        {
            private SALESORDERHEADER_HAS_ADDRESS_REL Parent;
            internal SALESORDERHEADER_HAS_ADDRESS_OUT(SALESORDERHEADER_HAS_ADDRESS_REL parent)
            {
                Parent = parent;
            }

			public AddressNode Address { get { return new AddressNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL
    {
		IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL Alias(out SALESORDERHEADER_HAS_ADDRESS_ALIAS alias);
		IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL Repeat(int maxHops);
		IFromIn_SALESORDERHEADER_HAS_ADDRESS_REL Repeat(int minHops, int maxHops);

        SALESORDERHEADER_HAS_ADDRESS_REL.SALESORDERHEADER_HAS_ADDRESS_OUT Out { get; }
    }
    public interface IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL
    {
		IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL Alias(out SALESORDERHEADER_HAS_ADDRESS_ALIAS alias);
		IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL Repeat(int maxHops);
		IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL Repeat(int minHops, int maxHops);

        SALESORDERHEADER_HAS_ADDRESS_REL.SALESORDERHEADER_HAS_ADDRESS_IN In { get; }
    }

    public class SALESORDERHEADER_HAS_ADDRESS_ALIAS : AliasResult
    {
		private SALESORDERHEADER_HAS_ADDRESS_REL Parent;

        internal SALESORDERHEADER_HAS_ADDRESS_ALIAS(SALESORDERHEADER_HAS_ADDRESS_REL parent)
        {
			Parent = parent;
        }
    }
}
