using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Datastore.Query
{
	public partial class Node
	{
		public static GenreNode Genre { get { return new GenreNode(); } }
	}

	public partial class GenreNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Genre";
        }

		internal GenreNode() { }
		internal GenreNode(GenreAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal GenreNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public GenreNode Alias(out GenreAlias alias)
		{
			alias = new GenreAlias(this);
            NodeAlias = alias;
			return this;
		}

		public GenreNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public GenreIn  In  { get { return new GenreIn(this); } }
		public class GenreIn
		{
			private GenreNode Parent;
			internal GenreIn(GenreNode parent)
			{
                Parent = parent;
			}
			public IFromIn_GENRE_HAS_SUBGENRE_REL GENRE_HAS_SUBGENRE { get { return new GENRE_HAS_SUBGENRE_REL(Parent, DirectionEnum.In); } }

		}

		public GenreOut Out { get { return new GenreOut(this); } }
		public class GenreOut
		{
			private GenreNode Parent;
			internal GenreOut(GenreNode parent)
			{
                Parent = parent;
			}
			public IFromOut_GENRE_HAS_SUBGENRE_REL GENRE_HAS_SUBGENRE { get { return new GENRE_HAS_SUBGENRE_REL(Parent, DirectionEnum.Out); } }
		}

		public GenreAny Any { get { return new GenreAny(this); } }
		public class GenreAny
		{
			private GenreNode Parent;
			internal GenreAny(GenreNode parent)
			{
                Parent = parent;
			}
			public IFromAny_GENRE_HAS_SUBGENRE_REL GENRE_HAS_SUBGENRE { get { return new GENRE_HAS_SUBGENRE_REL(Parent, DirectionEnum.None); } }
		}
	}

    public class GenreAlias : AliasResult
    {
        internal GenreAlias(GenreNode parent)
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
						{ "Name", new StringResult(this, "Name", Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Genre"], Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Genre"].Properties["Name"]) },
						{ "Uid", new StringResult(this, "Uid", Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Genre"], Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Base"].Properties["Uid"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Genre"], Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Base"].Properties["LastModifiedOn"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public GenreNode.GenreIn In { get { return new GenreNode.GenreIn(new GenreNode(this, true)); } }
        public GenreNode.GenreOut Out { get { return new GenreNode.GenreOut(new GenreNode(this, true)); } }
        public GenreNode.GenreAny Any { get { return new GenreNode.GenreAny(new GenreNode(this, true)); } }

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
