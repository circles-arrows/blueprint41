using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class STORE_VALID_FOR_SALESPERSON_REL : RELATIONSHIP, IFromIn_STORE_VALID_FOR_SALESPERSON_REL, IFromOut_STORE_VALID_FOR_SALESPERSON_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "VALID_FOR_SALESPERSON";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal STORE_VALID_FOR_SALESPERSON_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public STORE_VALID_FOR_SALESPERSON_REL Alias(out STORE_VALID_FOR_SALESPERSON_ALIAS alias)
		{
			alias = new STORE_VALID_FOR_SALESPERSON_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public STORE_VALID_FOR_SALESPERSON_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new STORE_VALID_FOR_SALESPERSON_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_STORE_VALID_FOR_SALESPERSON_REL IFromIn_STORE_VALID_FOR_SALESPERSON_REL.Alias(out STORE_VALID_FOR_SALESPERSON_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_STORE_VALID_FOR_SALESPERSON_REL IFromOut_STORE_VALID_FOR_SALESPERSON_REL.Alias(out STORE_VALID_FOR_SALESPERSON_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_STORE_VALID_FOR_SALESPERSON_REL IFromIn_STORE_VALID_FOR_SALESPERSON_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_STORE_VALID_FOR_SALESPERSON_REL IFromIn_STORE_VALID_FOR_SALESPERSON_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_STORE_VALID_FOR_SALESPERSON_REL IFromOut_STORE_VALID_FOR_SALESPERSON_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_STORE_VALID_FOR_SALESPERSON_REL IFromOut_STORE_VALID_FOR_SALESPERSON_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public STORE_VALID_FOR_SALESPERSON_IN In { get { return new STORE_VALID_FOR_SALESPERSON_IN(this); } }
		public class STORE_VALID_FOR_SALESPERSON_IN
		{
			private STORE_VALID_FOR_SALESPERSON_REL Parent;
			internal STORE_VALID_FOR_SALESPERSON_IN(STORE_VALID_FOR_SALESPERSON_REL parent)
			{
				Parent = parent;
			}

			public StoreNode Store { get { return new StoreNode(Parent, DirectionEnum.In); } }
		}

		public STORE_VALID_FOR_SALESPERSON_OUT Out { get { return new STORE_VALID_FOR_SALESPERSON_OUT(this); } }
		public class STORE_VALID_FOR_SALESPERSON_OUT
		{
			private STORE_VALID_FOR_SALESPERSON_REL Parent;
			internal STORE_VALID_FOR_SALESPERSON_OUT(STORE_VALID_FOR_SALESPERSON_REL parent)
			{
				Parent = parent;
			}

			public SalesPersonNode SalesPerson { get { return new SalesPersonNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_STORE_VALID_FOR_SALESPERSON_REL
	{
		IFromIn_STORE_VALID_FOR_SALESPERSON_REL Alias(out STORE_VALID_FOR_SALESPERSON_ALIAS alias);
		IFromIn_STORE_VALID_FOR_SALESPERSON_REL Repeat(int maxHops);
		IFromIn_STORE_VALID_FOR_SALESPERSON_REL Repeat(int minHops, int maxHops);

		STORE_VALID_FOR_SALESPERSON_REL.STORE_VALID_FOR_SALESPERSON_OUT Out { get; }
	}
	public interface IFromOut_STORE_VALID_FOR_SALESPERSON_REL
	{
		IFromOut_STORE_VALID_FOR_SALESPERSON_REL Alias(out STORE_VALID_FOR_SALESPERSON_ALIAS alias);
		IFromOut_STORE_VALID_FOR_SALESPERSON_REL Repeat(int maxHops);
		IFromOut_STORE_VALID_FOR_SALESPERSON_REL Repeat(int minHops, int maxHops);

		STORE_VALID_FOR_SALESPERSON_REL.STORE_VALID_FOR_SALESPERSON_IN In { get; }
	}

	public class STORE_VALID_FOR_SALESPERSON_ALIAS : AliasResult
	{
		private STORE_VALID_FOR_SALESPERSON_REL Parent;

		internal STORE_VALID_FOR_SALESPERSON_ALIAS(STORE_VALID_FOR_SALESPERSON_REL parent)
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
