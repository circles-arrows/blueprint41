using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class PRODUCTVENDOR_HAS_UNITMEASURE_REL : RELATIONSHIP, IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL, IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_UNITMEASURE";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal PRODUCTVENDOR_HAS_UNITMEASURE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public PRODUCTVENDOR_HAS_UNITMEASURE_REL Alias(out PRODUCTVENDOR_HAS_UNITMEASURE_ALIAS alias)
		{
			alias = new PRODUCTVENDOR_HAS_UNITMEASURE_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public PRODUCTVENDOR_HAS_UNITMEASURE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new PRODUCTVENDOR_HAS_UNITMEASURE_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL.Alias(out PRODUCTVENDOR_HAS_UNITMEASURE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL.Alias(out PRODUCTVENDOR_HAS_UNITMEASURE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public PRODUCTVENDOR_HAS_UNITMEASURE_IN In { get { return new PRODUCTVENDOR_HAS_UNITMEASURE_IN(this); } }
		public class PRODUCTVENDOR_HAS_UNITMEASURE_IN
		{
			private PRODUCTVENDOR_HAS_UNITMEASURE_REL Parent;
			internal PRODUCTVENDOR_HAS_UNITMEASURE_IN(PRODUCTVENDOR_HAS_UNITMEASURE_REL parent)
			{
				Parent = parent;
			}

			public ProductVendorNode ProductVendor { get { return new ProductVendorNode(Parent, DirectionEnum.In); } }
		}

		public PRODUCTVENDOR_HAS_UNITMEASURE_OUT Out { get { return new PRODUCTVENDOR_HAS_UNITMEASURE_OUT(this); } }
		public class PRODUCTVENDOR_HAS_UNITMEASURE_OUT
		{
			private PRODUCTVENDOR_HAS_UNITMEASURE_REL Parent;
			internal PRODUCTVENDOR_HAS_UNITMEASURE_OUT(PRODUCTVENDOR_HAS_UNITMEASURE_REL parent)
			{
				Parent = parent;
			}

			public UnitMeasureNode UnitMeasure { get { return new UnitMeasureNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL
	{
		IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL Alias(out PRODUCTVENDOR_HAS_UNITMEASURE_ALIAS alias);
		IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL Repeat(int maxHops);
		IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL Repeat(int minHops, int maxHops);

		PRODUCTVENDOR_HAS_UNITMEASURE_REL.PRODUCTVENDOR_HAS_UNITMEASURE_OUT Out { get; }
	}
	public interface IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL
	{
		IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL Alias(out PRODUCTVENDOR_HAS_UNITMEASURE_ALIAS alias);
		IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL Repeat(int maxHops);
		IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL Repeat(int minHops, int maxHops);

		PRODUCTVENDOR_HAS_UNITMEASURE_REL.PRODUCTVENDOR_HAS_UNITMEASURE_IN In { get; }
	}

	public class PRODUCTVENDOR_HAS_UNITMEASURE_ALIAS : AliasResult
	{
		private PRODUCTVENDOR_HAS_UNITMEASURE_REL Parent;

		internal PRODUCTVENDOR_HAS_UNITMEASURE_ALIAS(PRODUCTVENDOR_HAS_UNITMEASURE_REL parent)
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
