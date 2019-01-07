using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Datastore.Query
{
	public partial class Node
	{
		public static FilmNode Film { get { return new FilmNode(); } }
	}

	public partial class FilmNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Film";
        }

		internal FilmNode() { }
		internal FilmNode(FilmAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal FilmNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public FilmNode Alias(out FilmAlias alias)
		{
			alias = new FilmAlias(this);
            NodeAlias = alias;
			return this;
		}

		public FilmNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }


		public FilmOut Out { get { return new FilmOut(this); } }
		public class FilmOut
		{
			private FilmNode Parent;
			internal FilmOut(FilmNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PERSON_ACTED_IN_FILM_REL PERSON_ACTED_IN_FILM { get { return new PERSON_ACTED_IN_FILM_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PERSON_DIRECTED_FILM_REL PERSON_DIRECTED_FILM { get { return new PERSON_DIRECTED_FILM_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PERSON_PRODUCED_FILM_REL PERSON_PRODUCED_FILM { get { return new PERSON_PRODUCED_FILM_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PERSON_WROTE_FILM_REL PERSON_WROTE_FILM { get { return new PERSON_WROTE_FILM_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class FilmAlias : AliasResult
    {
        internal FilmAlias(FilmNode parent)
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
						{ "Uid", new StringResult(this, "Uid", Blueprint41Test.MovieModel.Model.Entities["Film"], Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["Uid"]) },
						{ "title", new StringResult(this, "title", Blueprint41Test.MovieModel.Model.Entities["Film"], Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["title"]) },
						{ "tagline", new StringResult(this, "tagline", Blueprint41Test.MovieModel.Model.Entities["Film"], Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["tagline"]) },
						{ "release", new NumericResult(this, "release", Blueprint41Test.MovieModel.Model.Entities["Film"], Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["release"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public FilmNode.FilmOut Out { get { return new FilmNode.FilmOut(new FilmNode(this, true)); } }

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
        public NumericResult release
		{
			get
			{
				if ((object)m_release == null)
					m_release = (NumericResult)AliasFields["release"];

				return m_release;
			}
		} 
        private NumericResult m_release = null;
    }
}
