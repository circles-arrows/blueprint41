
using Blueprint41;
using Blueprint41.Query;


namespace Domain.Data.Query
{
public partial class EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL : RELATIONSHIP, IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL, IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_SHIFT";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Alias(out EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_ALIAS alias)
		{
			alias = new EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL.Alias(out EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL.Alias(out EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_IN In { get { return new EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_IN(this); } }
        public class EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_IN
        {
            private EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Parent;
            internal EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_IN(EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL parent)
            {
                Parent = parent;
            }

			public EmployeeDepartmentHistoryNode EmployeeDepartmentHistory { get { return new EmployeeDepartmentHistoryNode(Parent, DirectionEnum.In); } }
        }

        public EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_OUT Out { get { return new EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_OUT(this); } }
        public class EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_OUT
        {
            private EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Parent;
            internal EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_OUT(EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL parent)
            {
                Parent = parent;
            }

			public ShiftNode Shift { get { return new ShiftNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL
    {
		IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Alias(out EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_ALIAS alias);
		IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Repeat(int maxHops);
		IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Repeat(int minHops, int maxHops);

        EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL.EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_OUT Out { get; }
    }
    public interface IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL
    {
		IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Alias(out EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_ALIAS alias);
		IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Repeat(int maxHops);
		IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Repeat(int minHops, int maxHops);

        EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL.EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_IN In { get; }
    }

    public class EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_ALIAS : AliasResult
    {
		private EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL Parent;

        internal EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_ALIAS(EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL parent)
        {
			Parent = parent;
        }
    }
}
