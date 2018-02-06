using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductCategoryNode ProductCategory { get { return new ProductCategoryNode(); } }
	}

	public partial class ProductCategoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductCategory";
            }
        }

		internal ProductCategoryNode() { }
		internal ProductCategoryNode(ProductCategoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductCategoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductCategoryNode Alias(out ProductCategoryAlias alias)
		{
			alias = new ProductCategoryAlias(this);
            NodeAlias = alias;
			return this;
		}


		public ProductCategoryOut Out { get { return new ProductCategoryOut(this); } }
		public class ProductCategoryOut
		{
			private ProductCategoryNode Parent;
			internal ProductCategoryOut(ProductCategoryNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCTSUBCATEGORY_IN_PRODUCTCATEGORY_REL PRODUCTSUBCATEGORY_IN_PRODUCTCATEGORY { get { return new PRODUCTSUBCATEGORY_IN_PRODUCTCATEGORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductCategoryAlias : AliasResult
    {
        internal ProductCategoryAlias(ProductCategoryNode parent)
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ProductCategory"], Datastore.AdventureWorks.Model.Entities["ProductCategory"].Properties["Name"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ProductCategory"], Datastore.AdventureWorks.Model.Entities["ProductCategory"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductCategory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductCategory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ProductCategoryNode.ProductCategoryOut Out { get { return new ProductCategoryNode.ProductCategoryOut(new ProductCategoryNode(this, true)); } }

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
