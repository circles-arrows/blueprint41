using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CurrencyNode Currency { get { return new CurrencyNode(); } }
	}

	public partial class CurrencyNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Currency";
            }
        }

		internal CurrencyNode() { }
		internal CurrencyNode(CurrencyAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CurrencyNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public CurrencyNode Alias(out CurrencyAlias alias)
		{
			alias = new CurrencyAlias(this);
            NodeAlias = alias;
			return this;
		}


		public CurrencyOut Out { get { return new CurrencyOut(this); } }
		public class CurrencyOut
		{
			private CurrencyNode Parent;
			internal CurrencyOut(CurrencyNode parent)
			{
                Parent = parent;
			}
			public IFromOut_CURRENCYRATE_HAS_CURRENCY_REL CURRENCYRATE_HAS_CURRENCY { get { return new CURRENCYRATE_HAS_CURRENCY_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class CurrencyAlias : AliasResult
    {
        internal CurrencyAlias(CurrencyNode parent)
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
						{ "CurrencyCode", new StringResult(this, "CurrencyCode", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["Currency"].Properties["CurrencyCode"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["Currency"].Properties["Name"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public CurrencyNode.CurrencyOut Out { get { return new CurrencyNode.CurrencyOut(new CurrencyNode(this, true)); } }

        public StringResult CurrencyCode
		{
			get
			{
				if ((object)m_CurrencyCode == null)
					m_CurrencyCode = (StringResult)AliasFields["CurrencyCode"];

				return m_CurrencyCode;
			}
		} 
        private StringResult m_CurrencyCode = null;
        public StringResult Name
		{
			get
			{
				if ((object)m_Name == null)
					m_Name = (StringResult)AliasFields["Name"];

				return m_Name;
			}
		} 
        private StringResult m_Name = null;
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
