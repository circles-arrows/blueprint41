using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CountryRegionNode CountryRegion { get { return new CountryRegionNode(); } }
	}

	public partial class CountryRegionNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "CountryRegion";
        }

		internal CountryRegionNode() { }
		internal CountryRegionNode(CountryRegionAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CountryRegionNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public CountryRegionNode Alias(out CountryRegionAlias alias)
		{
			alias = new CountryRegionAlias(this);
            NodeAlias = alias;
			return this;
		}

		public CountryRegionNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }


		public CountryRegionOut Out { get { return new CountryRegionOut(this); } }
		public class CountryRegionOut
		{
			private CountryRegionNode Parent;
			internal CountryRegionOut(CountryRegionNode parent)
			{
                Parent = parent;
			}
			public IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL STATEPROVINCE_HAS_COUNTRYREGION { get { return new STATEPROVINCE_HAS_COUNTRYREGION_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class CountryRegionAlias : AliasResult
    {
        internal CountryRegionAlias(CountryRegionNode parent)
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
						{ "Code", new NumericResult(this, "Code", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["CountryRegion"].Properties["Code"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["CountryRegion"].Properties["Name"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public CountryRegionNode.CountryRegionOut Out { get { return new CountryRegionNode.CountryRegionOut(new CountryRegionNode(this, true)); } }

        public NumericResult Code
		{
			get
			{
				if ((object)m_Code == null)
					m_Code = (NumericResult)AliasFields["Code"];

				return m_Code;
			}
		} 
        private NumericResult m_Code = null;
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
