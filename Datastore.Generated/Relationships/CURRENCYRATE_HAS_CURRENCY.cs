using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
public partial class CURRENCYRATE_HAS_CURRENCY_REL : RELATIONSHIP, IFromIn_CURRENCYRATE_HAS_CURRENCY_REL, IFromOut_CURRENCYRATE_HAS_CURRENCY_REL	{
		public override string NEO4J_TYPE
		{
			get
			{
				return "HAS_CURRENCY";
			}
		}
		public override AliasResult RelationshipAlias { get; protected set; }
		
		internal CURRENCYRATE_HAS_CURRENCY_REL(Blueprint41.Query.Node parent, DirectionEnum direction) : base(parent, direction) { }

		public CURRENCYRATE_HAS_CURRENCY_REL Alias(out CURRENCYRATE_HAS_CURRENCY_ALIAS alias)
		{
			alias = new CURRENCYRATE_HAS_CURRENCY_ALIAS(this);
			RelationshipAlias = alias;
			return this;
		} 
		public CURRENCYRATE_HAS_CURRENCY_REL Repeat(int maxHops)
		{
			return Repeat(1, maxHops);
		}
		public new CURRENCYRATE_HAS_CURRENCY_REL Repeat(int minHops, int maxHops)
		{
			base.Repeat(minHops, maxHops);
			return this;
		}

		IFromIn_CURRENCYRATE_HAS_CURRENCY_REL IFromIn_CURRENCYRATE_HAS_CURRENCY_REL.Alias(out CURRENCYRATE_HAS_CURRENCY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromOut_CURRENCYRATE_HAS_CURRENCY_REL IFromOut_CURRENCYRATE_HAS_CURRENCY_REL.Alias(out CURRENCYRATE_HAS_CURRENCY_ALIAS alias)
		{
			return Alias(out alias);
		}
		IFromIn_CURRENCYRATE_HAS_CURRENCY_REL IFromIn_CURRENCYRATE_HAS_CURRENCY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromIn_CURRENCYRATE_HAS_CURRENCY_REL IFromIn_CURRENCYRATE_HAS_CURRENCY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}
		IFromOut_CURRENCYRATE_HAS_CURRENCY_REL IFromOut_CURRENCYRATE_HAS_CURRENCY_REL.Repeat(int maxHops)
		{
			return Repeat(maxHops);
		}
		IFromOut_CURRENCYRATE_HAS_CURRENCY_REL IFromOut_CURRENCYRATE_HAS_CURRENCY_REL.Repeat(int minHops, int maxHops)
		{
			return Repeat(minHops, maxHops);
		}


		public CURRENCYRATE_HAS_CURRENCY_IN In { get { return new CURRENCYRATE_HAS_CURRENCY_IN(this); } }
		public class CURRENCYRATE_HAS_CURRENCY_IN
		{
			private CURRENCYRATE_HAS_CURRENCY_REL Parent;
			internal CURRENCYRATE_HAS_CURRENCY_IN(CURRENCYRATE_HAS_CURRENCY_REL parent)
			{
				Parent = parent;
			}

			public CurrencyRateNode CurrencyRate { get { return new CurrencyRateNode(Parent, DirectionEnum.In); } }
		}

		public CURRENCYRATE_HAS_CURRENCY_OUT Out { get { return new CURRENCYRATE_HAS_CURRENCY_OUT(this); } }
		public class CURRENCYRATE_HAS_CURRENCY_OUT
		{
			private CURRENCYRATE_HAS_CURRENCY_REL Parent;
			internal CURRENCYRATE_HAS_CURRENCY_OUT(CURRENCYRATE_HAS_CURRENCY_REL parent)
			{
				Parent = parent;
			}

			public CurrencyNode Currency { get { return new CurrencyNode(Parent, DirectionEnum.Out); } }
		}
	}

	public interface IFromIn_CURRENCYRATE_HAS_CURRENCY_REL
	{
		IFromIn_CURRENCYRATE_HAS_CURRENCY_REL Alias(out CURRENCYRATE_HAS_CURRENCY_ALIAS alias);
		IFromIn_CURRENCYRATE_HAS_CURRENCY_REL Repeat(int maxHops);
		IFromIn_CURRENCYRATE_HAS_CURRENCY_REL Repeat(int minHops, int maxHops);

		CURRENCYRATE_HAS_CURRENCY_REL.CURRENCYRATE_HAS_CURRENCY_OUT Out { get; }
	}
	public interface IFromOut_CURRENCYRATE_HAS_CURRENCY_REL
	{
		IFromOut_CURRENCYRATE_HAS_CURRENCY_REL Alias(out CURRENCYRATE_HAS_CURRENCY_ALIAS alias);
		IFromOut_CURRENCYRATE_HAS_CURRENCY_REL Repeat(int maxHops);
		IFromOut_CURRENCYRATE_HAS_CURRENCY_REL Repeat(int minHops, int maxHops);

		CURRENCYRATE_HAS_CURRENCY_REL.CURRENCYRATE_HAS_CURRENCY_IN In { get; }
	}

	public class CURRENCYRATE_HAS_CURRENCY_ALIAS : AliasResult
	{
		private CURRENCYRATE_HAS_CURRENCY_REL Parent;

		internal CURRENCYRATE_HAS_CURRENCY_ALIAS(CURRENCYRATE_HAS_CURRENCY_REL parent)
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
