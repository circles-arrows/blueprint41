using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class WORKS_IN_REL : RELATIONSHIP, IFromIn_WORKS_IN_REL, IFromOut_WORKS_IN_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "WORKS_IN";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal WORKS_IN_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public WORKS_IN_REL Alias(out WORKS_IN_ALIAS alias)
		{
			alias = new WORKS_IN_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public WORKS_IN_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new WORKS_IN_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_WORKS_IN_REL IFromIn_WORKS_IN_REL.Alias(out WORKS_IN_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_WORKS_IN_REL IFromOut_WORKS_IN_REL.Alias(out WORKS_IN_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_WORKS_IN_REL IFromIn_WORKS_IN_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_WORKS_IN_REL IFromIn_WORKS_IN_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_WORKS_IN_REL IFromOut_WORKS_IN_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_WORKS_IN_REL IFromOut_WORKS_IN_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public WORKS_IN_IN In { get { return new WORKS_IN_IN(this); } }
		public class WORKS_IN_IN
		{
			private WORKS_IN_REL Parent;
			internal WORKS_IN_IN(WORKS_IN_REL parent)
			{
				Parent = parent;
			}

			public EmployeeNode Employee { get { return new EmployeeNode(Parent, DirectionEnum.In); } }
		}

		public WORKS_IN_OUT Out { get { return new WORKS_IN_OUT(this); } }
		public class WORKS_IN_OUT
		{
			private WORKS_IN_REL Parent;
			internal WORKS_IN_OUT(WORKS_IN_REL parent)
			{
				Parent = parent;
			}

			public DepartmentNode Department { get { return new DepartmentNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_WORKS_IN_REL
	{
		IFromIn_WORKS_IN_REL Alias(out WORKS_IN_ALIAS alias);
		IFromIn_WORKS_IN_REL Repeat(int maxHops);
		IFromIn_WORKS_IN_REL Repeat(int minHops, int maxHops);

		WORKS_IN_REL.WORKS_IN_OUT Out { get; }
	}
	public interface IFromOut_WORKS_IN_REL
	{
		IFromOut_WORKS_IN_REL Alias(out WORKS_IN_ALIAS alias);
		IFromOut_WORKS_IN_REL Repeat(int maxHops);
		IFromOut_WORKS_IN_REL Repeat(int minHops, int maxHops);

		WORKS_IN_REL.WORKS_IN_IN In { get; }
	}

	public class WORKS_IN_ALIAS : AliasResult
	{
		private WORKS_IN_REL Parent;

		internal WORKS_IN_ALIAS(WORKS_IN_REL parent)
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
