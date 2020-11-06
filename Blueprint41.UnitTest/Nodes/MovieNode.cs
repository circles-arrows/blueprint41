using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Datastore.Query
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
		internal MovieNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }

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

        protected override Entity GetEntity()
        {
            throw new NotImplementedException();
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
			public IFromOut_PERSON_DIRECTED_REL PERSON_DIRECTED { get { return new PERSON_DIRECTED_REL(Parent, DirectionEnum.Out); } }
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
						{ "Title", new StringResult(this, "Title", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"].Properties["Title"]) },
						{ "Uid", new StringResult(this, "Uid", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"], Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public MovieNode.MovieOut Out { get { return new MovieNode.MovieOut(new MovieNode(this, true)); } }

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
