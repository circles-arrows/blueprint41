using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static UnitMeasureNode UnitMeasure { get { return new UnitMeasureNode(); } }
	}

	public partial class UnitMeasureNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "UnitMeasure";
        }

		internal UnitMeasureNode() { }
		internal UnitMeasureNode(UnitMeasureAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal UnitMeasureNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public UnitMeasureNode Alias(out UnitMeasureAlias alias)
		{
			alias = new UnitMeasureAlias(this);
            NodeAlias = alias;
			return this;
		}

		public UnitMeasureNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }


		public UnitMeasureOut Out { get { return new UnitMeasureOut(this); } }
		public class UnitMeasureOut
		{
			private UnitMeasureNode Parent;
			internal UnitMeasureOut(UnitMeasureNode parent)
			{
                Parent = parent;
			}
			public IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL BILLOFMATERIALS_HAS_UNITMEASURE { get { return new BILLOFMATERIALS_HAS_UNITMEASURE_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL PRODUCTVENDOR_HAS_UNITMEASURE { get { return new PRODUCTVENDOR_HAS_UNITMEASURE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class UnitMeasureAlias : AliasResult
    {
        internal UnitMeasureAlias(UnitMeasureNode parent)
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
						{ "UnitMeasureCorde", new StringResult(this, "UnitMeasureCorde", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["UnitMeasure"].Properties["UnitMeasureCorde"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["UnitMeasure"].Properties["Name"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public UnitMeasureNode.UnitMeasureOut Out { get { return new UnitMeasureNode.UnitMeasureOut(new UnitMeasureNode(this, true)); } }

        public StringResult UnitMeasureCorde
		{
			get
			{
				if ((object)m_UnitMeasureCorde == null)
					m_UnitMeasureCorde = (StringResult)AliasFields["UnitMeasureCorde"];

				return m_UnitMeasureCorde;
			}
		} 
        private StringResult m_UnitMeasureCorde = null;
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
