using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class EMPLOYEE_BECOMES_SALESPERSON_REL : RELATIONSHIP, IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL, IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "BECOMES_SALESPERSON";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal EMPLOYEE_BECOMES_SALESPERSON_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public EMPLOYEE_BECOMES_SALESPERSON_REL Alias(out EMPLOYEE_BECOMES_SALESPERSON_ALIAS alias)
		{
			alias = new EMPLOYEE_BECOMES_SALESPERSON_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public EMPLOYEE_BECOMES_SALESPERSON_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public EMPLOYEE_BECOMES_SALESPERSON_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL.Alias(out EMPLOYEE_BECOMES_SALESPERSON_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL.Alias(out EMPLOYEE_BECOMES_SALESPERSON_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public EMPLOYEE_BECOMES_SALESPERSON_IN In { get { return new EMPLOYEE_BECOMES_SALESPERSON_IN(this); } }
        public class EMPLOYEE_BECOMES_SALESPERSON_IN
        {
            private EMPLOYEE_BECOMES_SALESPERSON_REL Parent;
            internal EMPLOYEE_BECOMES_SALESPERSON_IN(EMPLOYEE_BECOMES_SALESPERSON_REL parent)
            {
                Parent = parent;
            }

			public EmployeeNode Employee { get { return new EmployeeNode(Parent, DirectionEnum.In); } }
        }

        public EMPLOYEE_BECOMES_SALESPERSON_OUT Out { get { return new EMPLOYEE_BECOMES_SALESPERSON_OUT(this); } }
        public class EMPLOYEE_BECOMES_SALESPERSON_OUT
        {
            private EMPLOYEE_BECOMES_SALESPERSON_REL Parent;
            internal EMPLOYEE_BECOMES_SALESPERSON_OUT(EMPLOYEE_BECOMES_SALESPERSON_REL parent)
            {
                Parent = parent;
            }

			public SalesPersonNode SalesPerson { get { return new SalesPersonNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL
    {
		IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL Alias(out EMPLOYEE_BECOMES_SALESPERSON_ALIAS alias);
		IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL Repeat(int maxHops);
		IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL Repeat(int minHops, int maxHops);

        EMPLOYEE_BECOMES_SALESPERSON_REL.EMPLOYEE_BECOMES_SALESPERSON_OUT Out { get; }
    }
    public interface IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL
    {
		IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL Alias(out EMPLOYEE_BECOMES_SALESPERSON_ALIAS alias);
		IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL Repeat(int maxHops);
		IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL Repeat(int minHops, int maxHops);

        EMPLOYEE_BECOMES_SALESPERSON_REL.EMPLOYEE_BECOMES_SALESPERSON_IN In { get; }
    }

    public class EMPLOYEE_BECOMES_SALESPERSON_ALIAS : AliasResult
    {
		private EMPLOYEE_BECOMES_SALESPERSON_REL Parent;

        internal EMPLOYEE_BECOMES_SALESPERSON_ALIAS(EMPLOYEE_BECOMES_SALESPERSON_REL parent)
        {
			Parent = parent;
        }
    }
}
