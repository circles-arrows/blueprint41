using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static MovieNode Movie { get { return new MovieNode(); } }
	}

	public partial class MovieNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Movie";
        }

		internal MovieNode() { }
		internal MovieNode(MovieAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal MovieNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public MovieNode Alias(out MovieAlias alias)
		{
			alias = new MovieAlias(this);
            NodeAlias = alias;
			return this;
		}

		public MovieNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public MovieIn  In  { get { return new MovieIn(this); } }
		public class MovieIn
		{
			private MovieNode Parent;
			internal MovieIn(MovieNode parent)
			{
                Parent = parent;
			}
			public IFromIn_CONTAINS_GENRE_REL CONTAINS_GENRE { get { return new CONTAINS_GENRE_REL(Parent, DirectionEnum.In); } }

		}

		public MovieOut Out { get { return new MovieOut(this); } }
		public class MovieOut
		{
			private MovieNode Parent;
			internal MovieOut(MovieNode parent)
			{
                Parent = parent;
			}
			public IFromOut_ACTED_IN_REL ACTED_IN { get { return new ACTED_IN_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_DIRECTED_REL DIRECTED { get { return new DIRECTED_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_MOVIEREVIEW_HAS_MOVIE_REL MOVIEREVIEW_HAS_MOVIE { get { return new MOVIEREVIEW_HAS_MOVIE_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_MOVIEROLE_HAS_MOVIE_REL MOVIEROLE_HAS_MOVIE { get { return new MOVIEROLE_HAS_MOVIE_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCED_REL PRODUCED { get { return new PRODUCED_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_WROTE_REL WROTE { get { return new WROTE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class MovieAlias : AliasResult
    {
        internal MovieAlias(MovieNode parent)
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
						{ "title", new StringResult(this, "title", MovieGraph.Model.Datastore.Model.Entities["Movie"], MovieGraph.Model.Datastore.Model.Entities["Movie"].Properties["title"]) },
						{ "tagline", new StringResult(this, "tagline", MovieGraph.Model.Datastore.Model.Entities["Movie"], MovieGraph.Model.Datastore.Model.Entities["Movie"].Properties["tagline"]) },
						{ "released", new NumericResult(this, "released", MovieGraph.Model.Datastore.Model.Entities["Movie"], MovieGraph.Model.Datastore.Model.Entities["Movie"].Properties["released"]) },
						{ "Uid", new StringResult(this, "Uid", MovieGraph.Model.Datastore.Model.Entities["Movie"], MovieGraph.Model.Datastore.Model.Entities["Movie"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public MovieNode.MovieIn In { get { return new MovieNode.MovieIn(new MovieNode(this, true)); } }
        public MovieNode.MovieOut Out { get { return new MovieNode.MovieOut(new MovieNode(this, true)); } }

        public StringResult title
		{
			get
			{
				if ((object)m_title == null)
					m_title = (StringResult)AliasFields["title"];

				return m_title;
			}
		} 
        private StringResult m_title = null;
        public StringResult tagline
		{
			get
			{
				if ((object)m_tagline == null)
					m_tagline = (StringResult)AliasFields["tagline"];

				return m_tagline;
			}
		} 
        private StringResult m_tagline = null;
        public NumericResult released
		{
			get
			{
				if ((object)m_released == null)
					m_released = (NumericResult)AliasFields["released"];

				return m_released;
			}
		} 
        private NumericResult m_released = null;
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
