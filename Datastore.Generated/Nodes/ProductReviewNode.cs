using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductReviewNode ProductReview { get { return new ProductReviewNode(); } }
	}

	public partial class ProductReviewNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "ProductReview";
        }

		internal ProductReviewNode() { }
		internal ProductReviewNode(ProductReviewAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductReviewNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public ProductReviewNode Alias(out ProductReviewAlias alias)
		{
			alias = new ProductReviewAlias(this);
            NodeAlias = alias;
			return this;
		}

		public ProductReviewNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }


		public ProductReviewOut Out { get { return new ProductReviewOut(this); } }
		public class ProductReviewOut
		{
			private ProductReviewNode Parent;
			internal ProductReviewOut(ProductReviewNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCT_VALID_FOR_PRODUCTREVIEW_REL PRODUCT_VALID_FOR_PRODUCTREVIEW { get { return new PRODUCT_VALID_FOR_PRODUCTREVIEW_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ProductReviewAlias : AliasResult
    {
        internal ProductReviewAlias(ProductReviewNode parent)
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
						{ "ReviewerName", new StringResult(this, "ReviewerName", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["ReviewerName"]) },
						{ "ReviewDate", new DateTimeResult(this, "ReviewDate", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["ReviewDate"]) },
						{ "EmailAddress", new StringResult(this, "EmailAddress", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["EmailAddress"]) },
						{ "Rating", new StringResult(this, "Rating", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["Rating"]) },
						{ "Comments", new StringResult(this, "Comments", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["Comments"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ProductReviewNode.ProductReviewOut Out { get { return new ProductReviewNode.ProductReviewOut(new ProductReviewNode(this, true)); } }

        public StringResult ReviewerName
		{
			get
			{
				if ((object)m_ReviewerName == null)
					m_ReviewerName = (StringResult)AliasFields["ReviewerName"];

				return m_ReviewerName;
			}
		} 
        private StringResult m_ReviewerName = null;
        public DateTimeResult ReviewDate
		{
			get
			{
				if ((object)m_ReviewDate == null)
					m_ReviewDate = (DateTimeResult)AliasFields["ReviewDate"];

				return m_ReviewDate;
			}
		} 
        private DateTimeResult m_ReviewDate = null;
        public StringResult EmailAddress
		{
			get
			{
				if ((object)m_EmailAddress == null)
					m_EmailAddress = (StringResult)AliasFields["EmailAddress"];

				return m_EmailAddress;
			}
		} 
        private StringResult m_EmailAddress = null;
        public StringResult Rating
		{
			get
			{
				if ((object)m_Rating == null)
					m_Rating = (StringResult)AliasFields["Rating"];

				return m_Rating;
			}
		} 
        private StringResult m_Rating = null;
        public StringResult Comments
		{
			get
			{
				if ((object)m_Comments == null)
					m_Comments = (StringResult)AliasFields["Comments"];

				return m_Comments;
			}
		} 
        private StringResult m_Comments = null;
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
