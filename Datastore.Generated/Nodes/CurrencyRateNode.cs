
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CurrencyRateNode CurrencyRate { get { return new CurrencyRateNode(); } }
	}

	public partial class CurrencyRateNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "CurrencyRate";
            }
        }

		internal CurrencyRateNode() { }
		internal CurrencyRateNode(CurrencyRateAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CurrencyRateNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public CurrencyRateNode Alias(out CurrencyRateAlias alias)
		{
			alias = new CurrencyRateAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public CurrencyRateIn  In  { get { return new CurrencyRateIn(this); } }
		public class CurrencyRateIn
		{
			private CurrencyRateNode Parent;
			internal CurrencyRateIn(CurrencyRateNode parent)
			{
                Parent = parent;
			}
			public IFromIn_CURRENCYRATE_HAS_CURRENCY_REL CURRENCYRATE_HAS_CURRENCY { get { return new CURRENCYRATE_HAS_CURRENCY_REL(Parent, DirectionEnum.In); } }

		}

		public CurrencyRateOut Out { get { return new CurrencyRateOut(this); } }
		public class CurrencyRateOut
		{
			private CurrencyRateNode Parent;
			internal CurrencyRateOut(CurrencyRateNode parent)
			{
                Parent = parent;
			}
			public IFromOut_SALESORDERHEADER_HAS_CURRENCYRATE_REL SALESORDERHEADER_HAS_CURRENCYRATE { get { return new SALESORDERHEADER_HAS_CURRENCYRATE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class CurrencyRateAlias : AliasResult
    {
        internal CurrencyRateAlias(CurrencyRateNode parent)
        {
			Node = parent;
            CurrencyRateDate = new DateTimeResult(this, "CurrencyRateDate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["CurrencyRateDate"]);
            FromCurrencyCode = new StringResult(this, "FromCurrencyCode", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["FromCurrencyCode"]);
            ToCurrencyCode = new StringResult(this, "ToCurrencyCode", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["ToCurrencyCode"]);
            AverageRate = new StringResult(this, "AverageRate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["AverageRate"]);
            EndOfDayRate = new StringResult(this, "EndOfDayRate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["EndOfDayRate"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public CurrencyRateNode.CurrencyRateIn In { get { return new CurrencyRateNode.CurrencyRateIn(new CurrencyRateNode(this, true)); } }
        public CurrencyRateNode.CurrencyRateOut Out { get { return new CurrencyRateNode.CurrencyRateOut(new CurrencyRateNode(this, true)); } }

        public DateTimeResult CurrencyRateDate { get; private set; } 
        public StringResult FromCurrencyCode { get; private set; } 
        public StringResult ToCurrencyCode { get; private set; } 
        public StringResult AverageRate { get; private set; } 
        public StringResult EndOfDayRate { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
