using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class EMPLOYEE_HAS_SHIFT_REL : RELATIONSHIP, IFromIn_EMPLOYEE_HAS_SHIFT_REL, IFromOut_EMPLOYEE_HAS_SHIFT_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_SHIFT";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal EMPLOYEE_HAS_SHIFT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public EMPLOYEE_HAS_SHIFT_REL Alias(out EMPLOYEE_HAS_SHIFT_ALIAS alias)
		{
			alias = new EMPLOYEE_HAS_SHIFT_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public EMPLOYEE_HAS_SHIFT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new EMPLOYEE_HAS_SHIFT_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_EMPLOYEE_HAS_SHIFT_REL IFromIn_EMPLOYEE_HAS_SHIFT_REL.Alias(out EMPLOYEE_HAS_SHIFT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_EMPLOYEE_HAS_SHIFT_REL IFromOut_EMPLOYEE_HAS_SHIFT_REL.Alias(out EMPLOYEE_HAS_SHIFT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_EMPLOYEE_HAS_SHIFT_REL IFromIn_EMPLOYEE_HAS_SHIFT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_EMPLOYEE_HAS_SHIFT_REL IFromIn_EMPLOYEE_HAS_SHIFT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_EMPLOYEE_HAS_SHIFT_REL IFromOut_EMPLOYEE_HAS_SHIFT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_EMPLOYEE_HAS_SHIFT_REL IFromOut_EMPLOYEE_HAS_SHIFT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public EMPLOYEE_HAS_SHIFT_IN In { get { return new EMPLOYEE_HAS_SHIFT_IN(this); } }
		public class EMPLOYEE_HAS_SHIFT_IN
		{
			private EMPLOYEE_HAS_SHIFT_REL Parent;
			internal EMPLOYEE_HAS_SHIFT_IN(EMPLOYEE_HAS_SHIFT_REL parent)
			{
				Parent = parent;
			}

			public EmployeeNode Employee { get { return new EmployeeNode(Parent, DirectionEnum.In); } }
		}

		public EMPLOYEE_HAS_SHIFT_OUT Out { get { return new EMPLOYEE_HAS_SHIFT_OUT(this); } }
		public class EMPLOYEE_HAS_SHIFT_OUT
		{
			private EMPLOYEE_HAS_SHIFT_REL Parent;
			internal EMPLOYEE_HAS_SHIFT_OUT(EMPLOYEE_HAS_SHIFT_REL parent)
			{
				Parent = parent;
			}

			public ShiftNode Shift { get { return new ShiftNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_EMPLOYEE_HAS_SHIFT_REL
	{
		IFromIn_EMPLOYEE_HAS_SHIFT_REL Alias(out EMPLOYEE_HAS_SHIFT_ALIAS alias);
		IFromIn_EMPLOYEE_HAS_SHIFT_REL Repeat(int maxHops);
		IFromIn_EMPLOYEE_HAS_SHIFT_REL Repeat(int minHops, int maxHops);

		EMPLOYEE_HAS_SHIFT_REL.EMPLOYEE_HAS_SHIFT_OUT Out { get; }
	}
	public interface IFromOut_EMPLOYEE_HAS_SHIFT_REL
	{
		IFromOut_EMPLOYEE_HAS_SHIFT_REL Alias(out EMPLOYEE_HAS_SHIFT_ALIAS alias);
		IFromOut_EMPLOYEE_HAS_SHIFT_REL Repeat(int maxHops);
		IFromOut_EMPLOYEE_HAS_SHIFT_REL Repeat(int minHops, int maxHops);

		EMPLOYEE_HAS_SHIFT_REL.EMPLOYEE_HAS_SHIFT_IN In { get; }
	}

	public class EMPLOYEE_HAS_SHIFT_ALIAS : AliasResult
	{
		private EMPLOYEE_HAS_SHIFT_REL Parent;

		internal EMPLOYEE_HAS_SHIFT_ALIAS(EMPLOYEE_HAS_SHIFT_REL parent)
		{
			Parent = parent;

			CreationDate = new RelationFieldResult(this, "CreationDate");
		}

        public Assignment[] Assign(JsNotation<DateTime> CreationDate = default)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (CreationDate.HasValue) assignments.Add(new Assignment(this.CreationDate, CreationDate));

            return assignments.ToArray();
        }

		public RelationFieldResult CreationDate { get; private set; } 
	}
}
