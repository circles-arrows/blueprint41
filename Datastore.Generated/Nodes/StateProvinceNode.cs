using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static StateProvinceNode StateProvince { get { return new StateProvinceNode(); } }
	}

	public partial class StateProvinceNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "StateProvince";
            }
        }

		internal StateProvinceNode() { }
		internal StateProvinceNode(StateProvinceAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal StateProvinceNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public StateProvinceNode Alias(out StateProvinceAlias alias)
		{
			alias = new StateProvinceAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public StateProvinceIn  In  { get { return new StateProvinceIn(this); } }
		public class StateProvinceIn
		{
			private StateProvinceNode Parent;
			internal StateProvinceIn(StateProvinceNode parent)
			{
                Parent = parent;
			}
			public IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL STATEPROVINCE_HAS_COUNTRYREGION { get { return new STATEPROVINCE_HAS_COUNTRYREGION_REL(Parent, DirectionEnum.In); } }
			public IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL STATEPROVINCE_HAS_SALESTERRITORY { get { return new STATEPROVINCE_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.In); } }

		}

		public StateProvinceOut Out { get { return new StateProvinceOut(this); } }
		public class StateProvinceOut
		{
			private StateProvinceNode Parent;
			internal StateProvinceOut(StateProvinceNode parent)
			{
                Parent = parent;
			}
			public IFromOut_ADDRESS_HAS_STATEPROVINCE_REL ADDRESS_HAS_STATEPROVINCE { get { return new ADDRESS_HAS_STATEPROVINCE_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL SALESTAXRATE_HAS_STATEPROVINCE { get { return new SALESTAXRATE_HAS_STATEPROVINCE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class StateProvinceAlias : AliasResult
    {
        internal StateProvinceAlias(StateProvinceNode parent)
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
						{ "StateProvinceCode", new StringResult(this, "StateProvinceCode", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["StateProvinceCode"]) },
						{ "CountryRegionCode", new StringResult(this, "CountryRegionCode", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["CountryRegionCode"]) },
						{ "IsOnlyStateProvinceFlag", new BooleanResult(this, "IsOnlyStateProvinceFlag", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["IsOnlyStateProvinceFlag"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["Name"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public StateProvinceNode.StateProvinceIn In { get { return new StateProvinceNode.StateProvinceIn(new StateProvinceNode(this, true)); } }
        public StateProvinceNode.StateProvinceOut Out { get { return new StateProvinceNode.StateProvinceOut(new StateProvinceNode(this, true)); } }

        public StringResult StateProvinceCode
		{
			get
			{
				if ((object)m_StateProvinceCode == null)
					m_StateProvinceCode = (StringResult)AliasFields["StateProvinceCode"];

				return m_StateProvinceCode;
			}
		} 
        private StringResult m_StateProvinceCode = null;
        public StringResult CountryRegionCode
		{
			get
			{
				if ((object)m_CountryRegionCode == null)
					m_CountryRegionCode = (StringResult)AliasFields["CountryRegionCode"];

				return m_CountryRegionCode;
			}
		} 
        private StringResult m_CountryRegionCode = null;
        public BooleanResult IsOnlyStateProvinceFlag
		{
			get
			{
				if ((object)m_IsOnlyStateProvinceFlag == null)
					m_IsOnlyStateProvinceFlag = (BooleanResult)AliasFields["IsOnlyStateProvinceFlag"];

				return m_IsOnlyStateProvinceFlag;
			}
		} 
        private BooleanResult m_IsOnlyStateProvinceFlag = null;
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
