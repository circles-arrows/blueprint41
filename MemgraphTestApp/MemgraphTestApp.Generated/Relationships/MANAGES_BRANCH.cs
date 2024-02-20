using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class MANAGES_BRANCH_REL : RELATIONSHIP, IFromIn_MANAGES_BRANCH_REL, IFromOut_MANAGES_BRANCH_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "MANAGES_BRANCH";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal MANAGES_BRANCH_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public MANAGES_BRANCH_REL Alias(out MANAGES_BRANCH_ALIAS alias)
		{
			alias = new MANAGES_BRANCH_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public MANAGES_BRANCH_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new MANAGES_BRANCH_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_MANAGES_BRANCH_REL IFromIn_MANAGES_BRANCH_REL.Alias(out MANAGES_BRANCH_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_MANAGES_BRANCH_REL IFromOut_MANAGES_BRANCH_REL.Alias(out MANAGES_BRANCH_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_MANAGES_BRANCH_REL IFromIn_MANAGES_BRANCH_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_MANAGES_BRANCH_REL IFromIn_MANAGES_BRANCH_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_MANAGES_BRANCH_REL IFromOut_MANAGES_BRANCH_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_MANAGES_BRANCH_REL IFromOut_MANAGES_BRANCH_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public MANAGES_BRANCH_IN In { get { return new MANAGES_BRANCH_IN(this); } }
		public class MANAGES_BRANCH_IN
		{
			private MANAGES_BRANCH_REL Parent;
			internal MANAGES_BRANCH_IN(MANAGES_BRANCH_REL parent)
			{
				Parent = parent;
			}

			public HeadEmployeeNode HeadEmployee { get { return new HeadEmployeeNode(Parent, DirectionEnum.In); } }
		}

		public MANAGES_BRANCH_OUT Out { get { return new MANAGES_BRANCH_OUT(this); } }
		public class MANAGES_BRANCH_OUT
		{
			private MANAGES_BRANCH_REL Parent;
			internal MANAGES_BRANCH_OUT(MANAGES_BRANCH_REL parent)
			{
				Parent = parent;
			}

			public BranchNode Branch { get { return new BranchNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_MANAGES_BRANCH_REL
	{
		IFromIn_MANAGES_BRANCH_REL Alias(out MANAGES_BRANCH_ALIAS alias);
		IFromIn_MANAGES_BRANCH_REL Repeat(int maxHops);
		IFromIn_MANAGES_BRANCH_REL Repeat(int minHops, int maxHops);

		MANAGES_BRANCH_REL.MANAGES_BRANCH_OUT Out { get; }
	}
	public interface IFromOut_MANAGES_BRANCH_REL
	{
		IFromOut_MANAGES_BRANCH_REL Alias(out MANAGES_BRANCH_ALIAS alias);
		IFromOut_MANAGES_BRANCH_REL Repeat(int maxHops);
		IFromOut_MANAGES_BRANCH_REL Repeat(int minHops, int maxHops);

		MANAGES_BRANCH_REL.MANAGES_BRANCH_IN In { get; }
	}

	public class MANAGES_BRANCH_ALIAS : AliasResult
	{
		private MANAGES_BRANCH_REL Parent;

		internal MANAGES_BRANCH_ALIAS(MANAGES_BRANCH_REL parent)
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
