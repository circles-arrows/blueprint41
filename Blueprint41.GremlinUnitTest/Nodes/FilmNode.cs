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
						{ "Title", new StringResult(this, "Title", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Film"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Film"].Properties["Title"]) },
						{ "TagLine", new StringResult(this, "TagLine", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Film"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Film"].Properties["TagLine"]) },
						{ "ReleaseDate", new DateTimeResult(this, "ReleaseDate", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Film"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Film"].Properties["ReleaseDate"]) },
						{ "Uid", new StringResult(this, "Uid", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Film"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"].Properties["Uid"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Film"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"].Properties["LastModifiedOn"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public FilmNode.FilmOut Out { get { return new FilmNode.FilmOut(new FilmNode(this, true)); } }

        public StringResult Title
		{
			get
			{
				if ((object)m_Title == null)
					m_Title = (StringResult)AliasFields["Title"];

				return m_Title;
			}
		} 
        private StringResult m_Title = null;
        public StringResult TagLine
		{
			get
			{
				if ((object)m_TagLine == null)
					m_TagLine = (StringResult)AliasFields["TagLine"];

				return m_TagLine;
			}
		} 
        private StringResult m_TagLine = null;
        public DateTimeResult ReleaseDate
		{
			get
			{
				if ((object)m_ReleaseDate == null)
					m_ReleaseDate = (DateTimeResult)AliasFields["ReleaseDate"];

				return m_ReleaseDate;
			}
		} 
        private DateTimeResult m_ReleaseDate = null;
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
        public DateTimeResult LastModifiedOn
		{
			get
			{
				if ((object)m_LastModifiedOn == null)
					m_LastModifiedOn = (DateTimeResult)AliasFields["LastModifiedOn"];

				return m_LastModifiedOn;
			}
		} 
        private DateTimeResult m_LastModifiedOn = null;
    }
}
