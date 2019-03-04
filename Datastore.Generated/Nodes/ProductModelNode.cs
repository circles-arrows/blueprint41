using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductModelNode ProductModel { get { return new ProductModelNode(); } }
	}

	public partial class ProductModelNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "ProductModel";
        }

		internal ProductModelNode() { }
		internal ProductModelNode(ProductModelAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductModelNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public ProductModelNode Alias(out ProductModelAlias alias)
		{
			alias = new ProductModelAlias(this);
            NodeAlias = alias;
			return this;
		}

		public ProductModelNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public ProductModelIn  In  { get { return new ProductModelIn(this); } }
		public class ProductModelIn
		{
			private ProductModelNode Parent;
			internal ProductModelIn(ProductModelNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTMODEL_HAS_CULTURE_REL PRODUCTMODEL_HAS_CULTURE { get { return new PRODUCTMODEL_HAS_CULTURE_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTMODEL_HAS_ILLUSTRATION_REL PRODUCTMODEL_HAS_ILLUSTRATION { get { return new PRODUCTMODEL_HAS_ILLUSTRATION_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTMODEL_HAS_PRODUCTDESCRIPTION_REL PRODUCTMODEL_HAS_PRODUCTDESCRIPTION { get { return new PRODUCTMODEL_HAS_PRODUCTDESCRIPTION_REL(Parent, DirectionEnum.In); } }

		}

		public ProductModelOut Out { get { return new ProductModelOut(this); } }
		public class ProductModelOut
		{
			private ProductModelNode Parent;
			internal ProductModelOut(ProductModelNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCT_HAS_PRODUCTMODEL_REL PRODUCT_HAS_PRODUCTMODEL { get { return new PRODUCT_HAS_PRODUCTMODEL_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductModelAlias : AliasResult
    {
        internal ProductModelAlias(ProductModelNode parent)
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["Name"]) },
						{ "CatalogDescription", new StringResult(this, "CatalogDescription", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["CatalogDescription"]) },
						{ "Instructions", new StringResult(this, "Instructions", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["Instructions"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ProductModelNode.ProductModelIn In { get { return new ProductModelNode.ProductModelIn(new ProductModelNode(this, true)); } }
        public ProductModelNode.ProductModelOut Out { get { return new ProductModelNode.ProductModelOut(new ProductModelNode(this, true)); } }

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
        public StringResult CatalogDescription
		{
			get
			{
				if ((object)m_CatalogDescription == null)
					m_CatalogDescription = (StringResult)AliasFields["CatalogDescription"];

				return m_CatalogDescription;
			}
		} 
        private StringResult m_CatalogDescription = null;
        public StringResult Instructions
		{
			get
			{
				if ((object)m_Instructions == null)
					m_Instructions = (StringResult)AliasFields["Instructions"];

				return m_Instructions;
			}
		} 
        private StringResult m_Instructions = null;
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
