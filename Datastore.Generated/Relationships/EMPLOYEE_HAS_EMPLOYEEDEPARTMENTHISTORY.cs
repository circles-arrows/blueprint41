using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL : RELATIONSHIP, IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL, IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "HAS_EMPLOYEEDEPARTMENTHISTORY";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Alias(out EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_ALIAS alias)
		{
			alias = new EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL.Alias(out EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL.Alias(out EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_IN In { get { return new EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_IN(this); } }
        public class EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_IN
        {
            private EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Parent;
            internal EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_IN(EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL parent)
            {
                Parent = parent;
            }

			public EmployeeNode Employee { get { return new EmployeeNode(Parent, DirectionEnum.In); } }
        }

        public EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_OUT Out { get { return new EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_OUT(this); } }
        public class EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_OUT
        {
            private EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Parent;
            internal EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_OUT(EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL parent)
            {
                Parent = parent;
            }

			public EmployeeDepartmentHistoryNode EmployeeDepartmentHistory { get { return new EmployeeDepartmentHistoryNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL
    {
		IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Alias(out EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_ALIAS alias);
		IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Repeat(int maxHops);
		IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Repeat(int minHops, int maxHops);

        EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL.EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_OUT Out { get; }
    }
    public interface IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL
    {
		IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Alias(out EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_ALIAS alias);
		IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Repeat(int maxHops);
		IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Repeat(int minHops, int maxHops);

        EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL.EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_IN In { get; }
    }

    public class EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_ALIAS : AliasResult
    {
		private EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL Parent;

        internal EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_ALIAS(EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL parent)
        {
			Parent = parent;
        }
    }
}
