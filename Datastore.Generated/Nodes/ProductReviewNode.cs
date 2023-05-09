using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Domain.Data.Manipulation;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ProductReviewNode ProductReview { get { return new ProductReviewNode(); } }
	}

	public partial class ProductReviewNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ProductReviewNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ProductReviewNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ProductReview";
		}

		protected override Entity GetEntity()
        {
			return m.ProductReview.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ProductReview.Entity.FunctionalId;
            }
        }

		internal ProductReviewNode() { }
		internal ProductReviewNode(ProductReviewAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductReviewNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ProductReviewNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ProductReviewNode Where(JsNotation<string> Comments = default, JsNotation<string> EmailAddress = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Rating = default, JsNotation<System.DateTime> ReviewDate = default, JsNotation<string> ReviewerName = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductReviewAlias> alias = new Lazy<ProductReviewAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Comments.HasValue) conditions.Add(new QueryCondition(alias.Value.Comments, Operator.Equals, ((IValue)Comments).GetValue()));
            if (EmailAddress.HasValue) conditions.Add(new QueryCondition(alias.Value.EmailAddress, Operator.Equals, ((IValue)EmailAddress).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Rating.HasValue) conditions.Add(new QueryCondition(alias.Value.Rating, Operator.Equals, ((IValue)Rating).GetValue()));
            if (ReviewDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ReviewDate, Operator.Equals, ((IValue)ReviewDate).GetValue()));
            if (ReviewerName.HasValue) conditions.Add(new QueryCondition(alias.Value.ReviewerName, Operator.Equals, ((IValue)ReviewerName).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ProductReviewNode Assign(JsNotation<string> Comments = default, JsNotation<string> EmailAddress = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Rating = default, JsNotation<System.DateTime> ReviewDate = default, JsNotation<string> ReviewerName = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductReviewAlias> alias = new Lazy<ProductReviewAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Comments.HasValue) assignments.Add(new Assignment(alias.Value.Comments, Comments));
            if (EmailAddress.HasValue) assignments.Add(new Assignment(alias.Value.EmailAddress, EmailAddress));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Rating.HasValue) assignments.Add(new Assignment(alias.Value.Rating, Rating));
            if (ReviewDate.HasValue) assignments.Add(new Assignment(alias.Value.ReviewDate, ReviewDate));
            if (ReviewerName.HasValue) assignments.Add(new Assignment(alias.Value.ReviewerName, ReviewerName));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ProductReviewNode Alias(out ProductReviewAlias alias)
        {
            if (NodeAlias is ProductReviewAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ProductReviewAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ProductReviewNode Alias(out ProductReviewAlias alias, string name)
        {
            if (NodeAlias is ProductReviewAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ProductReviewAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ProductReviewNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
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

	public class ProductReviewAlias : AliasResult<ProductReviewAlias, ProductReviewListAlias>
	{
		internal ProductReviewAlias(ProductReviewNode parent)
		{
			Node = parent;
		}
		internal ProductReviewAlias(ProductReviewNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ProductReviewAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ProductReviewAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ProductReviewAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Comments = default, JsNotation<string> EmailAddress = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Rating = default, JsNotation<System.DateTime> ReviewDate = default, JsNotation<string> ReviewerName = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Comments.HasValue) assignments.Add(new Assignment(this.Comments, Comments));
			if (EmailAddress.HasValue) assignments.Add(new Assignment(this.EmailAddress, EmailAddress));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Rating.HasValue) assignments.Add(new Assignment(this.Rating, Rating));
			if (ReviewDate.HasValue) assignments.Add(new Assignment(this.ReviewDate, ReviewDate));
			if (ReviewerName.HasValue) assignments.Add(new Assignment(this.ReviewerName, ReviewerName));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
            
            return assignments.ToArray();
        }


		public override IReadOnlyDictionary<string, FieldResult> AliasFields
		{
			get
			{
				if (m_AliasFields is null)
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
				if (m_ReviewerName is null)
					m_ReviewerName = (StringResult)AliasFields["ReviewerName"];

				return m_ReviewerName;
			}
		}
		private StringResult m_ReviewerName = null;
		public DateTimeResult ReviewDate
		{
			get
			{
				if (m_ReviewDate is null)
					m_ReviewDate = (DateTimeResult)AliasFields["ReviewDate"];

				return m_ReviewDate;
			}
		}
		private DateTimeResult m_ReviewDate = null;
		public StringResult EmailAddress
		{
			get
			{
				if (m_EmailAddress is null)
					m_EmailAddress = (StringResult)AliasFields["EmailAddress"];

				return m_EmailAddress;
			}
		}
		private StringResult m_EmailAddress = null;
		public StringResult Rating
		{
			get
			{
				if (m_Rating is null)
					m_Rating = (StringResult)AliasFields["Rating"];

				return m_Rating;
			}
		}
		private StringResult m_Rating = null;
		public StringResult Comments
		{
			get
			{
				if (m_Comments is null)
					m_Comments = (StringResult)AliasFields["Comments"];

				return m_Comments;
			}
		}
		private StringResult m_Comments = null;
		public DateTimeResult ModifiedDate
		{
			get
			{
				if (m_ModifiedDate is null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		}
		private DateTimeResult m_ModifiedDate = null;
		public StringResult Uid
		{
			get
			{
				if (m_Uid is null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		}
		private StringResult m_Uid = null;
		public AsResult As(string aliasName, out ProductReviewAlias alias)
		{
			alias = new ProductReviewAlias((ProductReviewNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ProductReviewListAlias : ListResult<ProductReviewListAlias, ProductReviewAlias>, IAliasListResult
	{
		private ProductReviewListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductReviewListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductReviewListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ProductReviewJaggedListAlias : ListResult<ProductReviewJaggedListAlias, ProductReviewListAlias>, IAliasJaggedListResult
	{
		private ProductReviewJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductReviewJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductReviewJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
