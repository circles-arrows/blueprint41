using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static BillOfMaterialsNode BillOfMaterials { get { return new BillOfMaterialsNode(); } }
	}

	public partial class BillOfMaterialsNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "BillOfMaterials";
        }

		internal BillOfMaterialsNode() { }
		internal BillOfMaterialsNode(BillOfMaterialsAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal BillOfMaterialsNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public BillOfMaterialsNode Alias(out BillOfMaterialsAlias alias)
		{
			alias = new BillOfMaterialsAlias(this);
            NodeAlias = alias;
			return this;
		}

		public BillOfMaterialsNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public BillOfMaterialsIn  In  { get { return new BillOfMaterialsIn(this); } }
		public class BillOfMaterialsIn
		{
			private BillOfMaterialsNode Parent;
			internal BillOfMaterialsIn(BillOfMaterialsNode parent)
			{
                Parent = parent;
			}
			public IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL BILLOFMATERIALS_HAS_PRODUCT { get { return new BILLOFMATERIALS_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL BILLOFMATERIALS_HAS_UNITMEASURE { get { return new BILLOFMATERIALS_HAS_UNITMEASURE_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class BillOfMaterialsAlias : AliasResult
    {
        internal BillOfMaterialsAlias(BillOfMaterialsNode parent)
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
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["EndDate"]) },
						{ "UnitMeasureCode", new StringResult(this, "UnitMeasureCode", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["UnitMeasureCode"]) },
						{ "BOMLevel", new StringResult(this, "BOMLevel", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["BOMLevel"]) },
						{ "PerAssemblyQty", new NumericResult(this, "PerAssemblyQty", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["PerAssemblyQty"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public BillOfMaterialsNode.BillOfMaterialsIn In { get { return new BillOfMaterialsNode.BillOfMaterialsIn(new BillOfMaterialsNode(this, true)); } }

        public DateTimeResult StartDate
		{
			get
			{
				if ((object)m_StartDate == null)
					m_StartDate = (DateTimeResult)AliasFields["StartDate"];

				return m_StartDate;
			}
		} 
        private DateTimeResult m_StartDate = null;
        public DateTimeResult EndDate
		{
			get
			{
				if ((object)m_EndDate == null)
					m_EndDate = (DateTimeResult)AliasFields["EndDate"];

				return m_EndDate;
			}
		} 
        private DateTimeResult m_EndDate = null;
        public StringResult UnitMeasureCode
		{
			get
			{
				if ((object)m_UnitMeasureCode == null)
					m_UnitMeasureCode = (StringResult)AliasFields["UnitMeasureCode"];

				return m_UnitMeasureCode;
			}
		} 
        private StringResult m_UnitMeasureCode = null;
        public StringResult BOMLevel
		{
			get
			{
				if ((object)m_BOMLevel == null)
					m_BOMLevel = (StringResult)AliasFields["BOMLevel"];

				return m_BOMLevel;
			}
		} 
        private StringResult m_BOMLevel = null;
        public NumericResult PerAssemblyQty
		{
			get
			{
				if ((object)m_PerAssemblyQty == null)
					m_PerAssemblyQty = (NumericResult)AliasFields["PerAssemblyQty"];

				return m_PerAssemblyQty;
			}
		} 
        private NumericResult m_PerAssemblyQty = null;
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
