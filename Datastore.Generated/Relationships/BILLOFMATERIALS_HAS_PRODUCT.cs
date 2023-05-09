using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class BILLOFMATERIALS_HAS_PRODUCT_REL : RELATIONSHIP, IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL, IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_PRODUCT";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal BILLOFMATERIALS_HAS_PRODUCT_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public BILLOFMATERIALS_HAS_PRODUCT_REL Alias(out BILLOFMATERIALS_HAS_PRODUCT_ALIAS alias)
		{
			alias = new BILLOFMATERIALS_HAS_PRODUCT_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public BILLOFMATERIALS_HAS_PRODUCT_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new BILLOFMATERIALS_HAS_PRODUCT_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL.Alias(out BILLOFMATERIALS_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL.Alias(out BILLOFMATERIALS_HAS_PRODUCT_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public BILLOFMATERIALS_HAS_PRODUCT_IN In { get { return new BILLOFMATERIALS_HAS_PRODUCT_IN(this); } }
		public class BILLOFMATERIALS_HAS_PRODUCT_IN
		{
			private BILLOFMATERIALS_HAS_PRODUCT_REL Parent;
			internal BILLOFMATERIALS_HAS_PRODUCT_IN(BILLOFMATERIALS_HAS_PRODUCT_REL parent)
			{
				Parent = parent;
			}

			public BillOfMaterialsNode BillOfMaterials { get { return new BillOfMaterialsNode(Parent, DirectionEnum.In); } }
		}

		public BILLOFMATERIALS_HAS_PRODUCT_OUT Out { get { return new BILLOFMATERIALS_HAS_PRODUCT_OUT(this); } }
		public class BILLOFMATERIALS_HAS_PRODUCT_OUT
		{
			private BILLOFMATERIALS_HAS_PRODUCT_REL Parent;
			internal BILLOFMATERIALS_HAS_PRODUCT_OUT(BILLOFMATERIALS_HAS_PRODUCT_REL parent)
			{
				Parent = parent;
			}

			public ProductNode Product { get { return new ProductNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL
	{
		IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL Alias(out BILLOFMATERIALS_HAS_PRODUCT_ALIAS alias);
		IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

		BILLOFMATERIALS_HAS_PRODUCT_REL.BILLOFMATERIALS_HAS_PRODUCT_OUT Out { get; }
	}
	public interface IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL
	{
		IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL Alias(out BILLOFMATERIALS_HAS_PRODUCT_ALIAS alias);
		IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL Repeat(int maxHops);
		IFromOut_BILLOFMATERIALS_HAS_PRODUCT_REL Repeat(int minHops, int maxHops);

		BILLOFMATERIALS_HAS_PRODUCT_REL.BILLOFMATERIALS_HAS_PRODUCT_IN In { get; }
	}

	public class BILLOFMATERIALS_HAS_PRODUCT_ALIAS : AliasResult
	{
		private BILLOFMATERIALS_HAS_PRODUCT_REL Parent;

		internal BILLOFMATERIALS_HAS_PRODUCT_ALIAS(BILLOFMATERIALS_HAS_PRODUCT_REL parent)
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
