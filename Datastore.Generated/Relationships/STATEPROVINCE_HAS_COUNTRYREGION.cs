using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class STATEPROVINCE_HAS_COUNTRYREGION_REL : RELATIONSHIP, IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL, IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_COUNTRYREGION";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal STATEPROVINCE_HAS_COUNTRYREGION_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public STATEPROVINCE_HAS_COUNTRYREGION_REL Alias(out STATEPROVINCE_HAS_COUNTRYREGION_ALIAS alias)
		{
			alias = new STATEPROVINCE_HAS_COUNTRYREGION_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public STATEPROVINCE_HAS_COUNTRYREGION_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new STATEPROVINCE_HAS_COUNTRYREGION_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL.Alias(out STATEPROVINCE_HAS_COUNTRYREGION_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL.Alias(out STATEPROVINCE_HAS_COUNTRYREGION_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public STATEPROVINCE_HAS_COUNTRYREGION_IN In { get { return new STATEPROVINCE_HAS_COUNTRYREGION_IN(this); } }
		public class STATEPROVINCE_HAS_COUNTRYREGION_IN
		{
			private STATEPROVINCE_HAS_COUNTRYREGION_REL Parent;
			internal STATEPROVINCE_HAS_COUNTRYREGION_IN(STATEPROVINCE_HAS_COUNTRYREGION_REL parent)
			{
				Parent = parent;
			}

			public StateProvinceNode StateProvince { get { return new StateProvinceNode(Parent, DirectionEnum.In); } }
		}

		public STATEPROVINCE_HAS_COUNTRYREGION_OUT Out { get { return new STATEPROVINCE_HAS_COUNTRYREGION_OUT(this); } }
		public class STATEPROVINCE_HAS_COUNTRYREGION_OUT
		{
			private STATEPROVINCE_HAS_COUNTRYREGION_REL Parent;
			internal STATEPROVINCE_HAS_COUNTRYREGION_OUT(STATEPROVINCE_HAS_COUNTRYREGION_REL parent)
			{
				Parent = parent;
			}

			public CountryRegionNode CountryRegion { get { return new CountryRegionNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL
	{
		IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL Alias(out STATEPROVINCE_HAS_COUNTRYREGION_ALIAS alias);
		IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL Repeat(int maxHops);
		IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL Repeat(int minHops, int maxHops);

		STATEPROVINCE_HAS_COUNTRYREGION_REL.STATEPROVINCE_HAS_COUNTRYREGION_OUT Out { get; }
	}
	public interface IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL
	{
		IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL Alias(out STATEPROVINCE_HAS_COUNTRYREGION_ALIAS alias);
		IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL Repeat(int maxHops);
		IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL Repeat(int minHops, int maxHops);

		STATEPROVINCE_HAS_COUNTRYREGION_REL.STATEPROVINCE_HAS_COUNTRYREGION_IN In { get; }
	}

	public class STATEPROVINCE_HAS_COUNTRYREGION_ALIAS : AliasResult
	{
		private STATEPROVINCE_HAS_COUNTRYREGION_REL Parent;

		internal STATEPROVINCE_HAS_COUNTRYREGION_ALIAS(STATEPROVINCE_HAS_COUNTRYREGION_REL parent)
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
