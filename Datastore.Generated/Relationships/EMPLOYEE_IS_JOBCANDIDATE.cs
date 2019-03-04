using System;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class EMPLOYEE_IS_JOBCANDIDATE_REL : RELATIONSHIP, IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL, IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL	{
        public override string NEO4J_TYPE
        {
            get
            {
                return "IS_JOBCANDIDATE";
            }
        }
        public override AliasResult RelationshipAlias { get; protected set; }
        
		internal EMPLOYEE_IS_JOBCANDIDATE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public EMPLOYEE_IS_JOBCANDIDATE_REL Alias(out EMPLOYEE_IS_JOBCANDIDATE_ALIAS alias)
		{
			alias = new EMPLOYEE_IS_JOBCANDIDATE_ALIAS(this);
            RelationshipAlias = alias;
			return this;
		} 
		public EMPLOYEE_IS_JOBCANDIDATE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public EMPLOYEE_IS_JOBCANDIDATE_REL Repeat(int minHops, int maxHops)
		{
			return this;
		}

		IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL.Alias(out EMPLOYEE_IS_JOBCANDIDATE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL.Alias(out EMPLOYEE_IS_JOBCANDIDATE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public EMPLOYEE_IS_JOBCANDIDATE_IN In { get { return new EMPLOYEE_IS_JOBCANDIDATE_IN(this); } }
        public class EMPLOYEE_IS_JOBCANDIDATE_IN
        {
            private EMPLOYEE_IS_JOBCANDIDATE_REL Parent;
            internal EMPLOYEE_IS_JOBCANDIDATE_IN(EMPLOYEE_IS_JOBCANDIDATE_REL parent)
            {
                Parent = parent;
            }

			public EmployeeNode Employee { get { return new EmployeeNode(Parent, DirectionEnum.In); } }
        }

        public EMPLOYEE_IS_JOBCANDIDATE_OUT Out { get { return new EMPLOYEE_IS_JOBCANDIDATE_OUT(this); } }
        public class EMPLOYEE_IS_JOBCANDIDATE_OUT
        {
            private EMPLOYEE_IS_JOBCANDIDATE_REL Parent;
            internal EMPLOYEE_IS_JOBCANDIDATE_OUT(EMPLOYEE_IS_JOBCANDIDATE_REL parent)
            {
                Parent = parent;
            }

			public JobCandidateNode JobCandidate { get { return new JobCandidateNode(Parent, DirectionEnum.Out); } }
        }
	}

    public interface IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL
    {
		IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL Alias(out EMPLOYEE_IS_JOBCANDIDATE_ALIAS alias);
		IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL Repeat(int maxHops);
		IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL Repeat(int minHops, int maxHops);

        EMPLOYEE_IS_JOBCANDIDATE_REL.EMPLOYEE_IS_JOBCANDIDATE_OUT Out { get; }
    }
    public interface IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL
    {
		IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL Alias(out EMPLOYEE_IS_JOBCANDIDATE_ALIAS alias);
		IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL Repeat(int maxHops);
		IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL Repeat(int minHops, int maxHops);

        EMPLOYEE_IS_JOBCANDIDATE_REL.EMPLOYEE_IS_JOBCANDIDATE_IN In { get; }
    }

    public class EMPLOYEE_IS_JOBCANDIDATE_ALIAS : AliasResult
    {
		private EMPLOYEE_IS_JOBCANDIDATE_REL Parent;

        internal EMPLOYEE_IS_JOBCANDIDATE_ALIAS(EMPLOYEE_IS_JOBCANDIDATE_REL parent)
        {
			Parent = parent;
        }
    }
}
