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
		public static MovieReviewNode MovieReview { get { return new MovieReviewNode(); } }
	}

	public partial class MovieReviewNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(MovieReviewNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(MovieReviewNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "MovieReview";
		}

		protected override Entity GetEntity()
        {
			return m.MovieReview.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.MovieReview.Entity.FunctionalId;
            }
        }

		internal MovieReviewNode() { }
		internal MovieReviewNode(MovieReviewAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal MovieReviewNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal MovieReviewNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public MovieReviewNode Where(JsNotation<decimal?> Rating = default, JsNotation<string> Review = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<MovieReviewAlias> alias = new Lazy<MovieReviewAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Rating.HasValue) conditions.Add(new QueryCondition(alias.Value.Rating, Operator.Equals, ((IValue)Rating).GetValue()));
            if (Review.HasValue) conditions.Add(new QueryCondition(alias.Value.Review, Operator.Equals, ((IValue)Review).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public MovieReviewNode Assign(JsNotation<decimal?> Rating = default, JsNotation<string> Review = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<MovieReviewAlias> alias = new Lazy<MovieReviewAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Rating.HasValue) assignments.Add(new Assignment(alias.Value.Rating, Rating));
            if (Review.HasValue) assignments.Add(new Assignment(alias.Value.Review, Review));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public MovieReviewNode Alias(out MovieReviewAlias alias)
        {
            if (NodeAlias is MovieReviewAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new MovieReviewAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public MovieReviewNode Alias(out MovieReviewAlias alias, string name)
        {
            if (NodeAlias is MovieReviewAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new MovieReviewAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public MovieReviewNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public MovieReviewIn  In  { get { return new MovieReviewIn(this); } }
		public class MovieReviewIn
		{
			private MovieReviewNode Parent;
			internal MovieReviewIn(MovieReviewNode parent)
			{
				Parent = parent;
			}
			public IFromIn_MOVIEREVIEW_HAS_MOVIE_REL MOVIEREVIEW_HAS_MOVIE { get { return new MOVIEREVIEW_HAS_MOVIE_REL(Parent, DirectionEnum.In); } }

		}

		public MovieReviewOut Out { get { return new MovieReviewOut(this); } }
		public class MovieReviewOut
		{
			private MovieReviewNode Parent;
			internal MovieReviewOut(MovieReviewNode parent)
			{
				Parent = parent;
			}
			public IFromOut_MOVIE_REVIEWS_REL MOVIE_REVIEWS { get { return new MOVIE_REVIEWS_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class MovieReviewAlias : AliasResult<MovieReviewAlias, MovieReviewListAlias>
	{
		internal MovieReviewAlias(MovieReviewNode parent)
		{
			Node = parent;
		}
		internal MovieReviewAlias(MovieReviewNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  MovieReviewAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  MovieReviewAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  MovieReviewAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<decimal?> Rating = default, JsNotation<string> Review = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Rating.HasValue) assignments.Add(new Assignment(this.Rating, Rating));
			if (Review.HasValue) assignments.Add(new Assignment(this.Review, Review));
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
						{ "Uid", new StringResult(this, "Uid", MovieGraph.Model.Datastore.Model.Entities["MovieReview"], MovieGraph.Model.Datastore.Model.Entities["MovieReview"].Properties["Uid"]) },
						{ "Review", new StringResult(this, "Review", MovieGraph.Model.Datastore.Model.Entities["MovieReview"], MovieGraph.Model.Datastore.Model.Entities["MovieReview"].Properties["Review"]) },
						{ "Rating", new NumericResult(this, "Rating", MovieGraph.Model.Datastore.Model.Entities["MovieReview"], MovieGraph.Model.Datastore.Model.Entities["MovieReview"].Properties["Rating"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public MovieReviewNode.MovieReviewIn In { get { return new MovieReviewNode.MovieReviewIn(new MovieReviewNode(this, true)); } }
		public MovieReviewNode.MovieReviewOut Out { get { return new MovieReviewNode.MovieReviewOut(new MovieReviewNode(this, true)); } }

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
		public StringResult Review
		{
			get
			{
				if (m_Review is null)
					m_Review = (StringResult)AliasFields["Review"];

				return m_Review;
			}
		}
		private StringResult m_Review = null;
		public NumericResult Rating
		{
			get
			{
				if (m_Rating is null)
					m_Rating = (NumericResult)AliasFields["Rating"];

				return m_Rating;
			}
		}
		private NumericResult m_Rating = null;
		public AsResult As(string aliasName, out MovieReviewAlias alias)
		{
			alias = new MovieReviewAlias((MovieReviewNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class MovieReviewListAlias : ListResult<MovieReviewListAlias, MovieReviewAlias>, IAliasListResult
	{
		private MovieReviewListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private MovieReviewListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private MovieReviewListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class MovieReviewJaggedListAlias : ListResult<MovieReviewJaggedListAlias, MovieReviewListAlias>, IAliasJaggedListResult
	{
		private MovieReviewJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private MovieReviewJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private MovieReviewJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
