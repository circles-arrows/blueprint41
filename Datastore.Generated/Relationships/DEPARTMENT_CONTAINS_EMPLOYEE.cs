
using Blueprint41;
using Blueprint41.Query;


namespace Domain.Data.Query
{
public partial class DEPARTMENT_CONTAINS_EMPLOYEE_REL : RELATIONSHIP, IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL, IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "CONTAINS_EMPLOYEE";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal DEPARTMENT_CONTAINS_EMPLOYEE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public DEPARTMENT_CONTAINS_EMPLOYEE_REL Alias(out DEPARTMENT_CONTAINS_EMPLOYEE_ALIAS alias)
		{
			alias = new DEPARTMENT_CONTAINS_EMPLOYEE_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public DEPARTMENT_CONTAINS_EMPLOYEE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public DEPARTMENT_CONTAINS_EMPLOYEE_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL.Alias(out DEPARTMENT_CONTAINS_EMPLOYEE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL.Alias(out DEPARTMENT_CONTAINS_EMPLOYEE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public DEPARTMENT_CONTAINS_EMPLOYEE_IN In { get { return new DEPARTMENT_CONTAINS_EMPLOYEE_IN(this); } }
        public class DEPARTMENT_CONTAINS_EMPLOYEE_IN
        {
            private DEPARTMENT_CONTAINS_EMPLOYEE_REL Parent;
            internal DEPARTMENT_CONTAINS_EMPLOYEE_IN(DEPARTMENT_CONTAINS_EMPLOYEE_REL parent)
            {
                Parent = parent;
            }

			public DepartmentNode Department { get { return new DepartmentNode(Parent, DirectionEnum.In); } }
        }

        public DEPARTMENT_CONTAINS_EMPLOYEE_OUT Out { get { return new DEPARTMENT_CONTAINS_EMPLOYEE_OUT(this); } }
        public class DEPARTMENT_CONTAINS_EMPLOYEE_OUT
        {
            private DEPARTMENT_CONTAINS_EMPLOYEE_REL Parent;
            internal DEPARTMENT_CONTAINS_EMPLOYEE_OUT(DEPARTMENT_CONTAINS_EMPLOYEE_REL parent)
            {
                Parent = parent;
            }

			public EmployeeNode Employee { get { return new EmployeeNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL
    {
		IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL Alias(out DEPARTMENT_CONTAINS_EMPLOYEE_ALIAS alias);
		IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL Repeat(int maxHops);
		IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL Repeat(int minHops, int maxHops);

        DEPARTMENT_CONTAINS_EMPLOYEE_REL.DEPARTMENT_CONTAINS_EMPLOYEE_OUT Out { get; }
    }
    public interface IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL
    {
		IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL Alias(out DEPARTMENT_CONTAINS_EMPLOYEE_ALIAS alias);
		IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL Repeat(int maxHops);
		IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL Repeat(int minHops, int maxHops);

        DEPARTMENT_CONTAINS_EMPLOYEE_REL.DEPARTMENT_CONTAINS_EMPLOYEE_IN In { get; }
    }

    public class DEPARTMENT_CONTAINS_EMPLOYEE_ALIAS : AliasResult
    {
		private DEPARTMENT_CONTAINS_EMPLOYEE_REL Parent;

        internal DEPARTMENT_CONTAINS_EMPLOYEE_ALIAS(DEPARTMENT_CONTAINS_EMPLOYEE_REL parent)
        {
			Parent = parent;
        }
    }
}
