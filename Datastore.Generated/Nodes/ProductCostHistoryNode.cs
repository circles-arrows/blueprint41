using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductCostHistoryNode ProductCostHistory { get { return new ProductCostHistoryNode(); } }
	}

	public partial class ProductCostHistoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductCostHistory";
            }
        }

		internal ProductCostHistoryNode() { }
		internal ProductCostHistoryNode(ProductCostHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductCostHistoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductCostHistoryNode Alias(out ProductCostHistoryAlias alias)
		{
			alias = new ProductCostHistoryAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public ProductCostHistoryIn  In  { get { return new ProductCostHistoryIn(this); } }
		public class ProductCostHistoryIn
		{
			private ProductCostHistoryNode Parent;
			internal ProductCostHistoryIn(ProductCostHistoryNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL PRODUCTCOSTHISTORY_HAS_PRODUCT { get { return new PRODUCTCOSTHISTORY_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class ProductCostHistoryAlias : AliasResult
    {
        internal ProductCostHistoryAlias(ProductCostHistoryNode parent)
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
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["EndDate"]) },
						{ "StandardCost", new StringResult(this, "StandardCost", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["StandardCost"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ProductCostHistoryNode.ProductCostHistoryIn In { get { return new ProductCostHistoryNode.ProductCostHistoryIn(new ProductCostHistoryNode(this, true)); } }

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
        public StringResult StandardCost
		{
			get
			{
				if ((object)m_StandardCost == null)
					m_StandardCost = (StringResult)AliasFields["StandardCost"];

				return m_StandardCost;
			}
		} 
        private StringResult m_StandardCost = null;
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
