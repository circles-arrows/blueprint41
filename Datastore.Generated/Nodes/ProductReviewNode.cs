
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductReviewNode ProductReview { get { return new ProductReviewNode(); } }
	}

	public partial class ProductReviewNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ProductReview";
            }
        }

		internal ProductReviewNode() { }
		internal ProductReviewNode(ProductReviewAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductReviewNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ProductReviewNode Alias(out ProductReviewAlias alias)
		{
			alias = new ProductReviewAlias(this);
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
            ReviewerName = new StringResult(this, "ReviewerName", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["ReviewerName"]);
            ReviewDate = new DateTimeResult(this, "ReviewDate", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["ReviewDate"]);
            EmailAddress = new StringResult(this, "EmailAddress", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["EmailAddress"]);
            Rating = new StringResult(this, "Rating", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["Rating"]);
            Comments = new StringResult(this, "Comments", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["Comments"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductReview"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ProductReviewNode.ProductReviewOut Out { get { return new ProductReviewNode.ProductReviewOut(new ProductReviewNode(this, true)); } }

        public StringResult ReviewerName { get; private set; } 
        public DateTimeResult ReviewDate { get; private set; } 
        public StringResult EmailAddress { get; private set; } 
        public StringResult Rating { get; private set; } 
        public StringResult Comments { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
