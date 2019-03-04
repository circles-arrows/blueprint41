using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CurrencyRateNode CurrencyRate { get { return new CurrencyRateNode(); } }
	}

	public partial class CurrencyRateNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "CurrencyRate";
        }

		internal CurrencyRateNode() { }
		internal CurrencyRateNode(CurrencyRateAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CurrencyRateNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public CurrencyRateNode Alias(out CurrencyRateAlias alias)
		{
			alias = new CurrencyRateAlias(this);
            NodeAlias = alias;
			return this;
		}

		public CurrencyRateNode UseExistingAlias(AliasResult alias)
        {
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "CurrencyRateDate", new DateTimeResult(this, "CurrencyRateDate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["CurrencyRateDate"]) },
						{ "FromCurrencyCode", new StringResult(this, "FromCurrencyCode", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["FromCurrencyCode"]) },
						{ "ToCurrencyCode", new StringResult(this, "ToCurrencyCode", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["ToCurrencyCode"]) },
						{ "AverageRate", new StringResult(this, "AverageRate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["AverageRate"]) },
						{ "EndOfDayRate", new StringResult(this, "EndOfDayRate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["EndOfDayRate"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public CurrencyRateNode.CurrencyRateIn In { get { return new CurrencyRateNode.CurrencyRateIn(new CurrencyRateNode(this, true)); } }
        public CurrencyRateNode.CurrencyRateOut Out { get { return new CurrencyRateNode.CurrencyRateOut(new CurrencyRateNode(this, true)); } }

        public DateTimeResult CurrencyRateDate
		{
			get
			{
				if ((object)m_CurrencyRateDate == null)
					m_CurrencyRateDate = (DateTimeResult)AliasFields["CurrencyRateDate"];

				return m_CurrencyRateDate;
			}
		} 
        private DateTimeResult m_CurrencyRateDate = null;
        public StringResult FromCurrencyCode
		{
			get
			{
				if ((object)m_FromCurrencyCode == null)
					m_FromCurrencyCode = (StringResult)AliasFields["FromCurrencyCode"];

				return m_FromCurrencyCode;
			}
		} 
        private StringResult m_FromCurrencyCode = null;
        public StringResult ToCurrencyCode
		{
			get
			{
				if ((object)m_ToCurrencyCode == null)
					m_ToCurrencyCode = (StringResult)AliasFields["ToCurrencyCode"];

				return m_ToCurrencyCode;
			}
		} 
        private StringResult m_ToCurrencyCode = null;
        public StringResult AverageRate
		{
			get
			{
				if ((object)m_AverageRate == null)
					m_AverageRate = (StringResult)AliasFields["AverageRate"];

				return m_AverageRate;
			}
		} 
        private StringResult m_AverageRate = null;
        public StringResult EndOfDayRate
		{
			get
			{
				if ((object)m_EndOfDayRate == null)
					m_EndOfDayRate = (StringResult)AliasFields["EndOfDayRate"];

				return m_EndOfDayRate;
			}
		} 
        private StringResult m_EndOfDayRate = null;
        public DateTimeResult ModifiedDate
		{
			get
			{
				if ((object)m_ModifiedDate == null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		} 
        private DateTimeResult m_ModifiedDate = null;
        public StringResult Uid
		{
			get
			{
				if ((object)m_Uid == null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		} 
        private StringResult m_Uid = null;
    }
}
