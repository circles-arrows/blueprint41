using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class SALESORDERDETAIL_HAS_PRODUCT_REL : RELATIONSHIP, IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL, IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_PRODUCT";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal SALESORDERDETAIL_HAS_PRODUCT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public SALESORDERDETAIL_HAS_PRODUCT_REL Alias(out SALESORDERDETAIL_HAS_PRODUCT_ALIAS alias)
		{
			alias = new SALESORDERDETAIL_HAS_PRODUCT_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public SALESORDERDETAIL_HAS_PRODUCT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new SALESORDERDETAIL_HAS_PRODUCT_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL.Alias(out SALESORDERDETAIL_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL.Alias(out SALESORDERDETAIL_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public SALESORDERDETAIL_HAS_PRODUCT_IN In { get { return new SALESORDERDETAIL_HAS_PRODUCT_IN(this); } }
		public class SALESORDERDETAIL_HAS_PRODUCT_IN
		{
			private SALESORDERDETAIL_HAS_PRODUCT_REL Parent;
			internal SALESORDERDETAIL_HAS_PRODUCT_IN(SALESORDERDETAIL_HAS_PRODUCT_REL parent)
			{
				Parent = parent;
			}

			public SalesOrderDetailNode SalesOrderDetail { get { return new SalesOrderDetailNode(Parent, DirectionEnum.In); } }
		}

		public SALESORDERDETAIL_HAS_PRODUCT_OUT Out { get { return new SALESORDERDETAIL_HAS_PRODUCT_OUT(this); } }
		public class SALESORDERDETAIL_HAS_PRODUCT_OUT
		{
			private SALESORDERDETAIL_HAS_PRODUCT_REL Parent;
			internal SALESORDERDETAIL_HAS_PRODUCT_OUT(SALESORDERDETAIL_HAS_PRODUCT_REL parent)
			{
				Parent = parent;
			}

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL
	{
		IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL Alias(out SALESORDERDETAIL_HAS_PRODUCT_ALIAS alias);
		IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromIn_SALESORDERDETAIL_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

		SALESORDERDETAIL_HAS_PRODUCT_REL.SALESORDERDETAIL_HAS_PRODUCT_OUT Out { get; }
	}
	public interface IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL
	{
		IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL Alias(out SALESORDERDETAIL_HAS_PRODUCT_ALIAS alias);
		IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromOut_SALESORDERDETAIL_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

		SALESORDERDETAIL_HAS_PRODUCT_REL.SALESORDERDETAIL_HAS_PRODUCT_IN In { get; }
	}

	public class SALESORDERDETAIL_HAS_PRODUCT_ALIAS : AliasResult
	{
		private SALESORDERDETAIL_HAS_PRODUCT_REL Parent;

		internal SALESORDERDETAIL_HAS_PRODUCT_ALIAS(SALESORDERDETAIL_HAS_PRODUCT_REL parent)
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
