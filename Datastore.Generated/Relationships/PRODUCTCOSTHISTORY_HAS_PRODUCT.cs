using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PRODUCTCOSTHISTORY_HAS_PRODUCT_REL : RELATIONSHIP, IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL, IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_PRODUCT";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal PRODUCTCOSTHISTORY_HAS_PRODUCT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Alias(out PRODUCTCOSTHISTORY_HAS_PRODUCT_ALIAS alias)
		{
			alias = new PRODUCTCOSTHISTORY_HAS_PRODUCT_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL.Alias(out PRODUCTCOSTHISTORY_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL.Alias(out PRODUCTCOSTHISTORY_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCTCOSTHISTORY_HAS_PRODUCT_IN In { get { return new PRODUCTCOSTHISTORY_HAS_PRODUCT_IN(this); } }
		public class PRODUCTCOSTHISTORY_HAS_PRODUCT_IN
		{
			private PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Parent;
			internal PRODUCTCOSTHISTORY_HAS_PRODUCT_IN(PRODUCTCOSTHISTORY_HAS_PRODUCT_REL parent)
			{
				Parent = parent;
			}

			public ProductCostHistoryNode ProductCostHistory { get { return new ProductCostHistoryNode(Parent, DirectionEnum.In); } }
		}

		public PRODUCTCOSTHISTORY_HAS_PRODUCT_OUT Out { get { return new PRODUCTCOSTHISTORY_HAS_PRODUCT_OUT(this); } }
		public class PRODUCTCOSTHISTORY_HAS_PRODUCT_OUT
		{
			private PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Parent;
			internal PRODUCTCOSTHISTORY_HAS_PRODUCT_OUT(PRODUCTCOSTHISTORY_HAS_PRODUCT_REL parent)
			{
				Parent = parent;
			}

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL
	{
		IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Alias(out PRODUCTCOSTHISTORY_HAS_PRODUCT_ALIAS alias);
		IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

		PRODUCTCOSTHISTORY_HAS_PRODUCT_REL.PRODUCTCOSTHISTORY_HAS_PRODUCT_OUT Out { get; }
	}
	public interface IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL
	{
		IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Alias(out PRODUCTCOSTHISTORY_HAS_PRODUCT_ALIAS alias);
		IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromOut_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

		PRODUCTCOSTHISTORY_HAS_PRODUCT_REL.PRODUCTCOSTHISTORY_HAS_PRODUCT_IN In { get; }
	}

	public class PRODUCTCOSTHISTORY_HAS_PRODUCT_ALIAS : AliasResult
	{
		private PRODUCTCOSTHISTORY_HAS_PRODUCT_REL Parent;

		internal PRODUCTCOSTHISTORY_HAS_PRODUCT_ALIAS(PRODUCTCOSTHISTORY_HAS_PRODUCT_REL parent)
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
