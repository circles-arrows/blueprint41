using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesPersonNode SalesPerson { get { return new SalesPersonNode(); } }
	}

	public partial class SalesPersonNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesPerson";
            }
        }

		internal SalesPersonNode() { }
		internal SalesPersonNode(SalesPersonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesPersonNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesPersonNode Alias(out SalesPersonAlias alias)
		{
			alias = new SalesPersonAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public SalesPersonIn  In  { get { return new SalesPersonIn(this); } }
		public class SalesPersonIn
		{
			private SalesPersonNode Parent;
			internal SalesPersonIn(SalesPersonNode parent)
			{
                Parent = parent;
			}
			public IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL SALESPERSON_HAS_SALESPERSONQUOTAHISTORY { get { return new SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL SALESPERSON_HAS_SALESTERRITORY { get { return new SALESPERSON_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESPERSON_IS_PERSON_REL SALESPERSON_IS_PERSON { get { return new SALESPERSON_IS_PERSON_REL(Parent, DirectionEnum.In); } }

		}

		public SalesPersonOut Out { get { return new SalesPersonOut(this); } }
		public class SalesPersonOut
		{
			private SalesPersonNode Parent;
			internal SalesPersonOut(SalesPersonNode parent)
			{
                Parent = parent;
			}
			public IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL EMPLOYEE_BECOMES_SALESPERSON { get { return new EMPLOYEE_BECOMES_SALESPERSON_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_STORE_VALID_FOR_SALESPERSON_REL STORE_VALID_FOR_SALESPERSON { get { return new STORE_VALID_FOR_SALESPERSON_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class SalesPersonAlias : AliasResult
    {
        internal SalesPersonAlias(SalesPersonNode parent)
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
						{ "SalesQuota", new StringResult(this, "SalesQuota", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesQuota"]) },
						{ "Bonus", new StringResult(this, "Bonus", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["Bonus"]) },
						{ "CommissionPct", new StringResult(this, "CommissionPct", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["CommissionPct"]) },
						{ "SalesYTD", new StringResult(this, "SalesYTD", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesYTD"]) },
						{ "SalesLastYear", new StringResult(this, "SalesLastYear", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesLastYear"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public SalesPersonNode.SalesPersonIn In { get { return new SalesPersonNode.SalesPersonIn(new SalesPersonNode(this, true)); } }
        public SalesPersonNode.SalesPersonOut Out { get { return new SalesPersonNode.SalesPersonOut(new SalesPersonNode(this, true)); } }

        public StringResult SalesQuota
		{
			get
			{
				if ((object)m_SalesQuota == null)
					m_SalesQuota = (StringResult)AliasFields["SalesQuota"];

				return m_SalesQuota;
			}
		} 
        private StringResult m_SalesQuota = null;
        public StringResult Bonus
		{
			get
			{
				if ((object)m_Bonus == null)
					m_Bonus = (StringResult)AliasFields["Bonus"];

				return m_Bonus;
			}
		} 
        private StringResult m_Bonus = null;
        public StringResult CommissionPct
		{
			get
			{
				if ((object)m_CommissionPct == null)
					m_CommissionPct = (StringResult)AliasFields["CommissionPct"];

				return m_CommissionPct;
			}
		} 
        private StringResult m_CommissionPct = null;
        public StringResult SalesYTD
		{
			get
			{
				if ((object)m_SalesYTD == null)
					m_SalesYTD = (StringResult)AliasFields["SalesYTD"];

				return m_SalesYTD;
			}
		} 
        private StringResult m_SalesYTD = null;
        public StringResult SalesLastYear
		{
			get
			{
				if ((object)m_SalesLastYear == null)
					m_SalesLastYear = (StringResult)AliasFields["SalesLastYear"];

				return m_SalesLastYear;
			}
		} 
        private StringResult m_SalesLastYear = null;
        public StringResult rowguid
		{
			get
			{
				if ((object)m_rowguid == null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		} 
        private StringResult m_rowguid = null;
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
