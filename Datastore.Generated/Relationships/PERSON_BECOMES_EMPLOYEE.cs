using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PERSON_BECOMES_EMPLOYEE_REL : RELATIONSHIP, IFromIn_PERSON_BECOMES_EMPLOYEE_REL, IFromOut_PERSON_BECOMES_EMPLOYEE_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "BECOMES_EMPLOYEE";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal PERSON_BECOMES_EMPLOYEE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PERSON_BECOMES_EMPLOYEE_REL Alias(out PERSON_BECOMES_EMPLOYEE_ALIAS alias)
		{
			alias = new PERSON_BECOMES_EMPLOYEE_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public PERSON_BECOMES_EMPLOYEE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new PERSON_BECOMES_EMPLOYEE_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_PERSON_BECOMES_EMPLOYEE_REL IFromIn_PERSON_BECOMES_EMPLOYEE_REL.Alias(out PERSON_BECOMES_EMPLOYEE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PERSON_BECOMES_EMPLOYEE_REL IFromOut_PERSON_BECOMES_EMPLOYEE_REL.Alias(out PERSON_BECOMES_EMPLOYEE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PERSON_BECOMES_EMPLOYEE_REL IFromIn_PERSON_BECOMES_EMPLOYEE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PERSON_BECOMES_EMPLOYEE_REL IFromIn_PERSON_BECOMES_EMPLOYEE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PERSON_BECOMES_EMPLOYEE_REL IFromOut_PERSON_BECOMES_EMPLOYEE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PERSON_BECOMES_EMPLOYEE_REL IFromOut_PERSON_BECOMES_EMPLOYEE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PERSON_BECOMES_EMPLOYEE_IN In { get { return new PERSON_BECOMES_EMPLOYEE_IN(this); } }
		public class PERSON_BECOMES_EMPLOYEE_IN
		{
			private PERSON_BECOMES_EMPLOYEE_REL Parent;
			internal PERSON_BECOMES_EMPLOYEE_IN(PERSON_BECOMES_EMPLOYEE_REL parent)
			{
				Parent = parent;
			}

			public PersonNode Person { get { return new PersonNode(Parent, DirectionEnum.In); } }
		}

		public PERSON_BECOMES_EMPLOYEE_OUT Out { get { return new PERSON_BECOMES_EMPLOYEE_OUT(this); } }
		public class PERSON_BECOMES_EMPLOYEE_OUT
		{
			private PERSON_BECOMES_EMPLOYEE_REL Parent;
			internal PERSON_BECOMES_EMPLOYEE_OUT(PERSON_BECOMES_EMPLOYEE_REL parent)
			{
				Parent = parent;
			}

			public EmployeeNode Employee { get { return new EmployeeNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_PERSON_BECOMES_EMPLOYEE_REL
	{
		IFromIn_PERSON_BECOMES_EMPLOYEE_REL Alias(out PERSON_BECOMES_EMPLOYEE_ALIAS alias);
		IFromIn_PERSON_BECOMES_EMPLOYEE_REL Repeat(int maxHops);
		IFromIn_PERSON_BECOMES_EMPLOYEE_REL Repeat(int minHops, int maxHops);

		PERSON_BECOMES_EMPLOYEE_REL.PERSON_BECOMES_EMPLOYEE_OUT Out { get; }
	}
	public interface IFromOut_PERSON_BECOMES_EMPLOYEE_REL
	{
		IFromOut_PERSON_BECOMES_EMPLOYEE_REL Alias(out PERSON_BECOMES_EMPLOYEE_ALIAS alias);
		IFromOut_PERSON_BECOMES_EMPLOYEE_REL Repeat(int maxHops);
		IFromOut_PERSON_BECOMES_EMPLOYEE_REL Repeat(int minHops, int maxHops);

		PERSON_BECOMES_EMPLOYEE_REL.PERSON_BECOMES_EMPLOYEE_IN In { get; }
	}

	public class PERSON_BECOMES_EMPLOYEE_ALIAS : AliasResult
	{
		private PERSON_BECOMES_EMPLOYEE_REL Parent;

		internal PERSON_BECOMES_EMPLOYEE_ALIAS(PERSON_BECOMES_EMPLOYEE_REL parent)
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
