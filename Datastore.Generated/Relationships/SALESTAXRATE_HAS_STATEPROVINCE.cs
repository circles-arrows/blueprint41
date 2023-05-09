using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class SALESTAXRATE_HAS_STATEPROVINCE_REL : RELATIONSHIP, IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL, IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_STATEPROVINCE";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal SALESTAXRATE_HAS_STATEPROVINCE_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public SALESTAXRATE_HAS_STATEPROVINCE_REL Alias(out SALESTAXRATE_HAS_STATEPROVINCE_ALIAS alias)
		{
			alias = new SALESTAXRATE_HAS_STATEPROVINCE_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public SALESTAXRATE_HAS_STATEPROVINCE_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new SALESTAXRATE_HAS_STATEPROVINCE_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL.Alias(out SALESTAXRATE_HAS_STATEPROVINCE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL.Alias(out SALESTAXRATE_HAS_STATEPROVINCE_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public SALESTAXRATE_HAS_STATEPROVINCE_IN In { get { return new SALESTAXRATE_HAS_STATEPROVINCE_IN(this); } }
		public class SALESTAXRATE_HAS_STATEPROVINCE_IN
		{
			private SALESTAXRATE_HAS_STATEPROVINCE_REL Parent;
			internal SALESTAXRATE_HAS_STATEPROVINCE_IN(SALESTAXRATE_HAS_STATEPROVINCE_REL parent)
			{
				Parent = parent;
			}

			public SalesTaxRateNode SalesTaxRate { get { return new SalesTaxRateNode(Parent, DirectionEnum.In); } }
		}

		public SALESTAXRATE_HAS_STATEPROVINCE_OUT Out { get { return new SALESTAXRATE_HAS_STATEPROVINCE_OUT(this); } }
		public class SALESTAXRATE_HAS_STATEPROVINCE_OUT
		{
			private SALESTAXRATE_HAS_STATEPROVINCE_REL Parent;
			internal SALESTAXRATE_HAS_STATEPROVINCE_OUT(SALESTAXRATE_HAS_STATEPROVINCE_REL parent)
			{
				Parent = parent;
			}

			public StateProvinceNode StateProvince { get { return new StateProvinceNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL
	{
		IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL Alias(out SALESTAXRATE_HAS_STATEPROVINCE_ALIAS alias);
		IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL Repeat(int maxHops);
		IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL Repeat(int minHops, int maxHops);

		SALESTAXRATE_HAS_STATEPROVINCE_REL.SALESTAXRATE_HAS_STATEPROVINCE_OUT Out { get; }
	}
	public interface IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL
	{
		IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL Alias(out SALESTAXRATE_HAS_STATEPROVINCE_ALIAS alias);
		IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL Repeat(int maxHops);
		IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL Repeat(int minHops, int maxHops);

		SALESTAXRATE_HAS_STATEPROVINCE_REL.SALESTAXRATE_HAS_STATEPROVINCE_IN In { get; }
	}

	public class SALESTAXRATE_HAS_STATEPROVINCE_ALIAS : AliasResult
	{
		private SALESTAXRATE_HAS_STATEPROVINCE_REL Parent;

		internal SALESTAXRATE_HAS_STATEPROVINCE_ALIAS(SALESTAXRATE_HAS_STATEPROVINCE_REL parent)
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
