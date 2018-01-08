
using Blueprint41;
using Blueprint41.Query;


namespace Domain.Data.Query
{
public partial class WORKORDERROUTING_HAS_LOCATION_REL : RELATIONSHIP, IFromIn_WORKORDERROUTING_HAS_LOCATION_REL, IFromOut_WORKORDERROUTING_HAS_LOCATION_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_LOCATION";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal WORKORDERROUTING_HAS_LOCATION_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public WORKORDERROUTING_HAS_LOCATION_REL Alias(out WORKORDERROUTING_HAS_LOCATION_ALIAS alias)
		{
			alias = new WORKORDERROUTING_HAS_LOCATION_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public WORKORDERROUTING_HAS_LOCATION_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public WORKORDERROUTING_HAS_LOCATION_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_WORKORDERROUTING_HAS_LOCATION_REL IFromIn_WORKORDERROUTING_HAS_LOCATION_REL.Alias(out WORKORDERROUTING_HAS_LOCATION_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_WORKORDERROUTING_HAS_LOCATION_REL IFromOut_WORKORDERROUTING_HAS_LOCATION_REL.Alias(out WORKORDERROUTING_HAS_LOCATION_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_WORKORDERROUTING_HAS_LOCATION_REL IFromIn_WORKORDERROUTING_HAS_LOCATION_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_WORKORDERROUTING_HAS_LOCATION_REL IFromIn_WORKORDERROUTING_HAS_LOCATION_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_WORKORDERROUTING_HAS_LOCATION_REL IFromOut_WORKORDERROUTING_HAS_LOCATION_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_WORKORDERROUTING_HAS_LOCATION_REL IFromOut_WORKORDERROUTING_HAS_LOCATION_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public WORKORDERROUTING_HAS_LOCATION_IN In { get { return new WORKORDERROUTING_HAS_LOCATION_IN(this); } }
        public class WORKORDERROUTING_HAS_LOCATION_IN
        {
            private WORKORDERROUTING_HAS_LOCATION_REL Parent;
            internal WORKORDERROUTING_HAS_LOCATION_IN(WORKORDERROUTING_HAS_LOCATION_REL parent)
            {
                Parent = parent;
            }

			public WorkOrderRoutingNode WorkOrderRouting { get { return new WorkOrderRoutingNode(Parent, DirectionEnum.In); } }
        }

        public WORKORDERROUTING_HAS_LOCATION_OUT Out { get { return new WORKORDERROUTING_HAS_LOCATION_OUT(this); } }
        public class WORKORDERROUTING_HAS_LOCATION_OUT
        {
            private WORKORDERROUTING_HAS_LOCATION_REL Parent;
            internal WORKORDERROUTING_HAS_LOCATION_OUT(WORKORDERROUTING_HAS_LOCATION_REL parent)
            {
                Parent = parent;
            }

			public LocationNode Location { get { return new LocationNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_WORKORDERROUTING_HAS_LOCATION_REL
    {
		IFromIn_WORKORDERROUTING_HAS_LOCATION_REL Alias(out WORKORDERROUTING_HAS_LOCATION_ALIAS alias);
		IFromIn_WORKORDERROUTING_HAS_LOCATION_REL Repeat(int maxHops);
		IFromIn_WORKORDERROUTING_HAS_LOCATION_REL Repeat(int minHops, int maxHops);

        WORKORDERROUTING_HAS_LOCATION_REL.WORKORDERROUTING_HAS_LOCATION_OUT Out { get; }
    }
    public interface IFromOut_WORKORDERROUTING_HAS_LOCATION_REL
    {
		IFromOut_WORKORDERROUTING_HAS_LOCATION_REL Alias(out WORKORDERROUTING_HAS_LOCATION_ALIAS alias);
		IFromOut_WORKORDERROUTING_HAS_LOCATION_REL Repeat(int maxHops);
		IFromOut_WORKORDERROUTING_HAS_LOCATION_REL Repeat(int minHops, int maxHops);

        WORKORDERROUTING_HAS_LOCATION_REL.WORKORDERROUTING_HAS_LOCATION_IN In { get; }
    }

    public class WORKORDERROUTING_HAS_LOCATION_ALIAS : AliasResult
    {
		private WORKORDERROUTING_HAS_LOCATION_REL Parent;

        internal WORKORDERROUTING_HAS_LOCATION_ALIAS(WORKORDERROUTING_HAS_LOCATION_REL parent)
        {
			Parent = parent;
        }
    }
}
