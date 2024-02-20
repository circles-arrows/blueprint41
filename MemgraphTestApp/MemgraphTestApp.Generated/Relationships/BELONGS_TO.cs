using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class BELONGS_TO_REL : RELATIONSHIP, IFromIn_BELONGS_TO_REL, IFromOut_BELONGS_TO_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "BELONGS_TO";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal BELONGS_TO_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public BELONGS_TO_REL Alias(out BELONGS_TO_ALIAS alias)
		{
			alias = new BELONGS_TO_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public BELONGS_TO_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new BELONGS_TO_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_BELONGS_TO_REL IFromIn_BELONGS_TO_REL.Alias(out BELONGS_TO_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_BELONGS_TO_REL IFromOut_BELONGS_TO_REL.Alias(out BELONGS_TO_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_BELONGS_TO_REL IFromIn_BELONGS_TO_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_BELONGS_TO_REL IFromIn_BELONGS_TO_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_BELONGS_TO_REL IFromOut_BELONGS_TO_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_BELONGS_TO_REL IFromOut_BELONGS_TO_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public BELONGS_TO_IN In { get { return new BELONGS_TO_IN(this); } }
		public class BELONGS_TO_IN
		{
			private BELONGS_TO_REL Parent;
			internal BELONGS_TO_IN(BELONGS_TO_REL parent)
			{
				Parent = parent;
			}

			public BranchNode Branch { get { return new BranchNode(Parent, DirectionEnum.In); } }
		}

		public BELONGS_TO_OUT Out { get { return new BELONGS_TO_OUT(this); } }
		public class BELONGS_TO_OUT
		{
			private BELONGS_TO_REL Parent;
			internal BELONGS_TO_OUT(BELONGS_TO_REL parent)
			{
				Parent = parent;
			}

			public DepartmentNode Department { get { return new DepartmentNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_BELONGS_TO_REL
	{
		IFromIn_BELONGS_TO_REL Alias(out BELONGS_TO_ALIAS alias);
		IFromIn_BELONGS_TO_REL Repeat(int maxHops);
		IFromIn_BELONGS_TO_REL Repeat(int minHops, int maxHops);

		BELONGS_TO_REL.BELONGS_TO_OUT Out { get; }
	}
	public interface IFromOut_BELONGS_TO_REL
	{
		IFromOut_BELONGS_TO_REL Alias(out BELONGS_TO_ALIAS alias);
		IFromOut_BELONGS_TO_REL Repeat(int maxHops);
		IFromOut_BELONGS_TO_REL Repeat(int minHops, int maxHops);

		BELONGS_TO_REL.BELONGS_TO_IN In { get; }
	}

	public class BELONGS_TO_ALIAS : AliasResult
	{
		private BELONGS_TO_REL Parent;

		internal BELONGS_TO_ALIAS(BELONGS_TO_REL parent)
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
