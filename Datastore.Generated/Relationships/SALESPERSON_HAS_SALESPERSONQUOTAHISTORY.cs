using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL : RELATIONSHIP, IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL, IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_SALESPERSONQUOTAHISTORY";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Alias(out SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_ALIAS alias)
		{
			alias = new SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL.Alias(out SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL.Alias(out SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_IN In { get { return new SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_IN(this); } }
		public class SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_IN
		{
			private SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Parent;
			internal SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_IN(SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL parent)
			{
				Parent = parent;
			}

			public SalesPersonNode SalesPerson { get { return new SalesPersonNode(Parent, DirectionEnum.In); } }
		}

		public SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_OUT Out { get { return new SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_OUT(this); } }
		public class SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_OUT
		{
			private SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Parent;
			internal SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_OUT(SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL parent)
			{
				Parent = parent;
			}

			public SalesPersonQuotaHistoryNode SalesPersonQuotaHistory { get { return new SalesPersonQuotaHistoryNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL
	{
		IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Alias(out SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_ALIAS alias);
		IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Repeat(int maxHops);
		IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Repeat(int minHops, int maxHops);

		SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL.SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_OUT Out { get; }
	}
	public interface IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL
	{
		IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Alias(out SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_ALIAS alias);
		IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Repeat(int maxHops);
		IFromOut_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Repeat(int minHops, int maxHops);

		SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL.SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_IN In { get; }
	}

	public class SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_ALIAS : AliasResult
	{
		private SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL Parent;

		internal SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_ALIAS(SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL parent)
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
