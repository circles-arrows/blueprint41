using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
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


		public GenreOut Out { get { return new GenreOut(this); } }
		public class GenreOut
		{
			private GenreNode Parent;
			internal GenreOut(GenreNode parent)
			{
                Parent = parent;
			}
			public IFromOut_CONTAINS_GENRE_REL CONTAINS_GENRE { get { return new CONTAINS_GENRE_REL(Parent, DirectionEnum.Out); } }
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
						{ "Uid", new StringResult(this, "Uid", MovieGraph.Model.Datastore.Model.Entities["Genre"], MovieGraph.Model.Datastore.Model.Entities["Genre"].Properties["Uid"]) },
						{ "Name", new StringResult(this, "Name", MovieGraph.Model.Datastore.Model.Entities["Genre"], MovieGraph.Model.Datastore.Model.Entities["Genre"].Properties["Name"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public GenreNode.GenreOut Out { get { return new GenreNode.GenreOut(new GenreNode(this, true)); } }

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
    }
}
