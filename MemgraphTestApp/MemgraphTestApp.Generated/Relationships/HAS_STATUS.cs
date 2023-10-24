using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class HAS_STATUS_REL : RELATIONSHIP, IFromIn_HAS_STATUS_REL, IFromOut_HAS_STATUS_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_STATUS";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal HAS_STATUS_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public HAS_STATUS_REL Alias(out HAS_STATUS_ALIAS alias)
		{
			alias = new HAS_STATUS_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public HAS_STATUS_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new HAS_STATUS_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_HAS_STATUS_REL IFromIn_HAS_STATUS_REL.Alias(out HAS_STATUS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_HAS_STATUS_REL IFromOut_HAS_STATUS_REL.Alias(out HAS_STATUS_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_HAS_STATUS_REL IFromIn_HAS_STATUS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_HAS_STATUS_REL IFromIn_HAS_STATUS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_HAS_STATUS_REL IFromOut_HAS_STATUS_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_HAS_STATUS_REL IFromOut_HAS_STATUS_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public HAS_STATUS_IN In { get { return new HAS_STATUS_IN(this); } }
		public class HAS_STATUS_IN
		{
			private HAS_STATUS_REL Parent;
			internal HAS_STATUS_IN(HAS_STATUS_REL parent)
			{
				Parent = parent;
			}

			public PersonnelNode Personnel { get { return new PersonnelNode(Parent, DirectionEnum.In); } }
			public EmployeeNode Employee { get { return new EmployeeNode(Parent, DirectionEnum.In); } }
			public HeadEmployeeNode HeadEmployee { get { return new HeadEmployeeNode(Parent, DirectionEnum.In); } }
		}

		public HAS_STATUS_OUT Out { get { return new HAS_STATUS_OUT(this); } }
		public class HAS_STATUS_OUT
		{
			private HAS_STATUS_REL Parent;
			internal HAS_STATUS_OUT(HAS_STATUS_REL parent)
			{
				Parent = parent;
			}

			public EmploymentStatusNode EmploymentStatus { get { return new EmploymentStatusNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_HAS_STATUS_REL
	{
		IFromIn_HAS_STATUS_REL Alias(out HAS_STATUS_ALIAS alias);
		IFromIn_HAS_STATUS_REL Repeat(int maxHops);
		IFromIn_HAS_STATUS_REL Repeat(int minHops, int maxHops);

		HAS_STATUS_REL.HAS_STATUS_OUT Out { get; }
	}
	public interface IFromOut_HAS_STATUS_REL
	{
		IFromOut_HAS_STATUS_REL Alias(out HAS_STATUS_ALIAS alias);
		IFromOut_HAS_STATUS_REL Repeat(int maxHops);
		IFromOut_HAS_STATUS_REL Repeat(int minHops, int maxHops);

		HAS_STATUS_REL.HAS_STATUS_IN In { get; }
	}

	public class HAS_STATUS_ALIAS : AliasResult
	{
		private HAS_STATUS_REL Parent;

		internal HAS_STATUS_ALIAS(HAS_STATUS_REL parent)
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
