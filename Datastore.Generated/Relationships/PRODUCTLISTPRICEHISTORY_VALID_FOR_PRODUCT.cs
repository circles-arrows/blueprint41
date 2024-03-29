using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL : RELATIONSHIP, IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL, IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "VALID_FOR_PRODUCT";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Alias(out PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_ALIAS alias)
		{
			alias = new PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL.Alias(out PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL.Alias(out PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_IN In { get { return new PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_IN(this); } }
		public class PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_IN
		{
			private PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Parent;
			internal PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_IN(PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL parent)
			{
				Parent = parent;
			}

			public ProductListPriceHistoryNode ProductListPriceHistory { get { return new ProductListPriceHistoryNode(Parent, DirectionEnum.In); } }
		}

		public PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_OUT Out { get { return new PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_OUT(this); } }
		public class PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_OUT
		{
			private PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Parent;
			internal PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_OUT(PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL parent)
			{
				Parent = parent;
			}

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL
	{
		IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Alias(out PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_ALIAS alias);
		IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Repeat(int maxHops);
		IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Repeat(int minHops, int maxHops);

		PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL.PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_OUT Out { get; }
	}
	public interface IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL
	{
		IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Alias(out PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_ALIAS alias);
		IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Repeat(int maxHops);
		IFromOut_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Repeat(int minHops, int maxHops);

		PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL.PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_IN In { get; }
	}

	public class PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_ALIAS : AliasResult
	{
		private PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL Parent;

		internal PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_ALIAS(PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL parent)
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
