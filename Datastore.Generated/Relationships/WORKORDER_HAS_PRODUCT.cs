using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class WORKORDER_HAS_PRODUCT_REL : RELATIONSHIP, IFromIn_WORKORDER_HAS_PRODUCT_REL, IFromOut_WORKORDER_HAS_PRODUCT_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_PRODUCT";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal WORKORDER_HAS_PRODUCT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public WORKORDER_HAS_PRODUCT_REL Alias(out WORKORDER_HAS_PRODUCT_ALIAS alias)
		{
			alias = new WORKORDER_HAS_PRODUCT_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public WORKORDER_HAS_PRODUCT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public WORKORDER_HAS_PRODUCT_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_WORKORDER_HAS_PRODUCT_REL IFromIn_WORKORDER_HAS_PRODUCT_REL.Alias(out WORKORDER_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_WORKORDER_HAS_PRODUCT_REL IFromOut_WORKORDER_HAS_PRODUCT_REL.Alias(out WORKORDER_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_WORKORDER_HAS_PRODUCT_REL IFromIn_WORKORDER_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_WORKORDER_HAS_PRODUCT_REL IFromIn_WORKORDER_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_WORKORDER_HAS_PRODUCT_REL IFromOut_WORKORDER_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_WORKORDER_HAS_PRODUCT_REL IFromOut_WORKORDER_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public WORKORDER_HAS_PRODUCT_IN In { get { return new WORKORDER_HAS_PRODUCT_IN(this); } }
        public class WORKORDER_HAS_PRODUCT_IN
        {
            private WORKORDER_HAS_PRODUCT_REL Parent;
            internal WORKORDER_HAS_PRODUCT_IN(WORKORDER_HAS_PRODUCT_REL parent)
            {
                Parent = parent;
            }

			public WorkOrderNode WorkOrder { get { return new WorkOrderNode(Parent, DirectionEnum.In); } }
        }

        public WORKORDER_HAS_PRODUCT_OUT Out { get { return new WORKORDER_HAS_PRODUCT_OUT(this); } }
        public class WORKORDER_HAS_PRODUCT_OUT
        {
            private WORKORDER_HAS_PRODUCT_REL Parent;
            internal WORKORDER_HAS_PRODUCT_OUT(WORKORDER_HAS_PRODUCT_REL parent)
            {
                Parent = parent;
            }

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_WORKORDER_HAS_PRODUCT_REL
    {
		IFromIn_WORKORDER_HAS_PRODUCT_REL Alias(out WORKORDER_HAS_PRODUCT_ALIAS alias);
		IFromIn_WORKORDER_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromIn_WORKORDER_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

        WORKORDER_HAS_PRODUCT_REL.WORKORDER_HAS_PRODUCT_OUT Out { get; }
    }
    public interface IFromOut_WORKORDER_HAS_PRODUCT_REL
    {
		IFromOut_WORKORDER_HAS_PRODUCT_REL Alias(out WORKORDER_HAS_PRODUCT_ALIAS alias);
		IFromOut_WORKORDER_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromOut_WORKORDER_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

        WORKORDER_HAS_PRODUCT_REL.WORKORDER_HAS_PRODUCT_IN In { get; }
    }

    public class WORKORDER_HAS_PRODUCT_ALIAS : AliasResult
    {
		private WORKORDER_HAS_PRODUCT_REL Parent;

        internal WORKORDER_HAS_PRODUCT_ALIAS(WORKORDER_HAS_PRODUCT_REL parent)
        {
			Parent = parent;
        }
    }
}
