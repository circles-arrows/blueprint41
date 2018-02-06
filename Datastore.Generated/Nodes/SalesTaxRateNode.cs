using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesTaxRateNode SalesTaxRate { get { return new SalesTaxRateNode(); } }
	}

	public partial class SalesTaxRateNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "SalesTaxRate";
            }
        }

		internal SalesTaxRateNode() { }
		internal SalesTaxRateNode(SalesTaxRateAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesTaxRateNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public SalesTaxRateNode Alias(out SalesTaxRateAlias alias)
		{
			alias = new SalesTaxRateAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public SalesTaxRateIn  In  { get { return new SalesTaxRateIn(this); } }
		public class SalesTaxRateIn
		{
			private SalesTaxRateNode Parent;
			internal SalesTaxRateIn(SalesTaxRateNode parent)
			{
                Parent = parent;
			}
			public IFromIn_SALESTAXRATE_HAS_STATEPROVINCE_REL SALESTAXRATE_HAS_STATEPROVINCE { get { return new SALESTAXRATE_HAS_STATEPROVINCE_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class SalesTaxRateAlias : AliasResult
    {
        internal SalesTaxRateAlias(SalesTaxRateNode parent)
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
						{ "TaxType", new StringResult(this, "TaxType", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["TaxType"]) },
						{ "TaxRate", new StringResult(this, "TaxRate", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["TaxRate"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["Name"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesTaxRate"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public SalesTaxRateNode.SalesTaxRateIn In { get { return new SalesTaxRateNode.SalesTaxRateIn(new SalesTaxRateNode(this, true)); } }

        public StringResult TaxType
		{
			get
			{
				if ((object)m_TaxType == null)
					m_TaxType = (StringResult)AliasFields["TaxType"];

				return m_TaxType;
			}
		} 
        private StringResult m_TaxType = null;
        public StringResult TaxRate
		{
			get
			{
				if ((object)m_TaxRate == null)
					m_TaxRate = (StringResult)AliasFields["TaxRate"];

				return m_TaxRate;
			}
		} 
        private StringResult m_TaxRate = null;
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
