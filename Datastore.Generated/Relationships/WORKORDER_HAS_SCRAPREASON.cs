using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class WORKORDER_HAS_SCRAPREASON_REL : RELATIONSHIP, IFromIn_WORKORDER_HAS_SCRAPREASON_REL, IFromOut_WORKORDER_HAS_SCRAPREASON_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_SCRAPREASON";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal WORKORDER_HAS_SCRAPREASON_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public WORKORDER_HAS_SCRAPREASON_REL Alias(out WORKORDER_HAS_SCRAPREASON_ALIAS alias)
		{
			alias = new WORKORDER_HAS_SCRAPREASON_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public WORKORDER_HAS_SCRAPREASON_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public WORKORDER_HAS_SCRAPREASON_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_WORKORDER_HAS_SCRAPREASON_REL IFromIn_WORKORDER_HAS_SCRAPREASON_REL.Alias(out WORKORDER_HAS_SCRAPREASON_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_WORKORDER_HAS_SCRAPREASON_REL IFromOut_WORKORDER_HAS_SCRAPREASON_REL.Alias(out WORKORDER_HAS_SCRAPREASON_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_WORKORDER_HAS_SCRAPREASON_REL IFromIn_WORKORDER_HAS_SCRAPREASON_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_WORKORDER_HAS_SCRAPREASON_REL IFromIn_WORKORDER_HAS_SCRAPREASON_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_WORKORDER_HAS_SCRAPREASON_REL IFromOut_WORKORDER_HAS_SCRAPREASON_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_WORKORDER_HAS_SCRAPREASON_REL IFromOut_WORKORDER_HAS_SCRAPREASON_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public WORKORDER_HAS_SCRAPREASON_IN In { get { return new WORKORDER_HAS_SCRAPREASON_IN(this); } }
        public class WORKORDER_HAS_SCRAPREASON_IN
        {
            private WORKORDER_HAS_SCRAPREASON_REL Parent;
            internal WORKORDER_HAS_SCRAPREASON_IN(WORKORDER_HAS_SCRAPREASON_REL parent)
            {
                Parent = parent;
            }

			public WorkOrderNode WorkOrder { get { return new WorkOrderNode(Parent, DirectionEnum.In); } }
        }

        public WORKORDER_HAS_SCRAPREASON_OUT Out { get { return new WORKORDER_HAS_SCRAPREASON_OUT(this); } }
        public class WORKORDER_HAS_SCRAPREASON_OUT
        {
            private WORKORDER_HAS_SCRAPREASON_REL Parent;
            internal WORKORDER_HAS_SCRAPREASON_OUT(WORKORDER_HAS_SCRAPREASON_REL parent)
            {
                Parent = parent;
            }

			public ScrapReasonNode ScrapReason { get { return new ScrapReasonNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_WORKORDER_HAS_SCRAPREASON_REL
    {
		IFromIn_WORKORDER_HAS_SCRAPREASON_REL Alias(out WORKORDER_HAS_SCRAPREASON_ALIAS alias);
		IFromIn_WORKORDER_HAS_SCRAPREASON_REL Repeat(int maxHops);
		IFromIn_WORKORDER_HAS_SCRAPREASON_REL Repeat(int minHops, int maxHops);

        WORKORDER_HAS_SCRAPREASON_REL.WORKORDER_HAS_SCRAPREASON_OUT Out { get; }
    }
    public interface IFromOut_WORKORDER_HAS_SCRAPREASON_REL
    {
		IFromOut_WORKORDER_HAS_SCRAPREASON_REL Alias(out WORKORDER_HAS_SCRAPREASON_ALIAS alias);
		IFromOut_WORKORDER_HAS_SCRAPREASON_REL Repeat(int maxHops);
		IFromOut_WORKORDER_HAS_SCRAPREASON_REL Repeat(int minHops, int maxHops);

        WORKORDER_HAS_SCRAPREASON_REL.WORKORDER_HAS_SCRAPREASON_IN In { get; }
    }

    public class WORKORDER_HAS_SCRAPREASON_ALIAS : AliasResult
    {
		private WORKORDER_HAS_SCRAPREASON_REL Parent;

        internal WORKORDER_HAS_SCRAPREASON_ALIAS(WORKORDER_HAS_SCRAPREASON_REL parent)
        {
			Parent = parent;
        }
    }
}
