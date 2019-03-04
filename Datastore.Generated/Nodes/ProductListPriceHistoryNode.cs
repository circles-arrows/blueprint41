using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductListPriceHistoryNode ProductListPriceHistory { get { return new ProductListPriceHistoryNode(); } }
	}

	public partial class ProductListPriceHistoryNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "ProductListPriceHistory";
        }

		internal ProductListPriceHistoryNode() { }
		internal ProductListPriceHistoryNode(ProductListPriceHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductListPriceHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public ProductListPriceHistoryNode Alias(out ProductListPriceHistoryAlias alias)
		{
			alias = new ProductListPriceHistoryAlias(this);
            NodeAlias = alias;
			return this;
		}

		public ProductListPriceHistoryNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public ProductListPriceHistoryIn  In  { get { return new ProductListPriceHistoryIn(this); } }
		public class ProductListPriceHistoryIn
		{
			private ProductListPriceHistoryNode Parent;
			internal ProductListPriceHistoryIn(ProductListPriceHistoryNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT { get { return new PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class ProductListPriceHistoryAlias : AliasResult
    {
        internal ProductListPriceHistoryAlias(ProductListPriceHistoryNode parent)
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
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"].Properties["EndDate"]) },
						{ "ListPrice", new StringResult(this, "ListPrice", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"].Properties["ListPrice"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ProductListPriceHistoryNode.ProductListPriceHistoryIn In { get { return new ProductListPriceHistoryNode.ProductListPriceHistoryIn(new ProductListPriceHistoryNode(this, true)); } }

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
        public StringResult ListPrice
		{
			get
			{
				if ((object)m_ListPrice == null)
					m_ListPrice = (StringResult)AliasFields["ListPrice"];

				return m_ListPrice;
			}
		} 
        private StringResult m_ListPrice = null;
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
