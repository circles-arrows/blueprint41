using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static MovieReviewNode MovieReview { get { return new MovieReviewNode(); } }
	}

	public partial class MovieReviewNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "MovieReview";
        }

		internal MovieReviewNode() { }
		internal MovieReviewNode(MovieReviewAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal MovieReviewNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public MovieReviewNode Alias(out MovieReviewAlias alias)
		{
			alias = new MovieReviewAlias(this);
            NodeAlias = alias;
			return this;
		}

		public MovieReviewNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
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

    public class MovieReviewAlias : AliasResult
    {
        internal MovieReviewAlias(MovieReviewNode parent)
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
						{ "Uid", new StringResult(this, "Uid", MovieGraph.Model.Datastore.Model.Entities["MovieReview"], MovieGraph.Model.Datastore.Model.Entities["MovieReview"].Properties["Uid"]) },
						{ "Review", new StringResult(this, "Review", MovieGraph.Model.Datastore.Model.Entities["MovieReview"], MovieGraph.Model.Datastore.Model.Entities["MovieReview"].Properties["Review"]) },
						{ "Rating", new MiscResult(this, "Rating", MovieGraph.Model.Datastore.Model.Entities["MovieReview"], MovieGraph.Model.Datastore.Model.Entities["MovieReview"].Properties["Rating"]) },
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
				if ((object)m_Uid == null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		} 
        private StringResult m_Uid = null;
        public StringResult Review
		{
			get
			{
				if ((object)m_Review == null)
					m_Review = (StringResult)AliasFields["Review"];

				return m_Review;
			}
		} 
        private StringResult m_Review = null;
        public MiscResult Rating
		{
			get
			{
				if ((object)m_Rating == null)
					m_Rating = (MiscResult)AliasFields["Rating"];

				return m_Rating;
			}
		} 
        private MiscResult m_Rating = null;
    }
}
