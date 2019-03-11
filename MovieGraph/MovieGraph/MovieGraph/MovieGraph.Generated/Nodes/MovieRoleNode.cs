using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static MovieRoleNode MovieRole { get { return new MovieRoleNode(); } }
	}

	public partial class MovieRoleNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "MovieRole";
        }

		internal MovieRoleNode() { }
		internal MovieRoleNode(MovieRoleAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal MovieRoleNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public MovieRoleNode Alias(out MovieRoleAlias alias)
		{
			alias = new MovieRoleAlias(this);
            NodeAlias = alias;
			return this;
		}

		public MovieRoleNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public MovieRoleIn  In  { get { return new MovieRoleIn(this); } }
		public class MovieRoleIn
		{
			private MovieRoleNode Parent;
			internal MovieRoleIn(MovieRoleNode parent)
			{
                Parent = parent;
			}
			public IFromIn_MOVIEROLE_HAS_MOVIE_REL MOVIEROLE_HAS_MOVIE { get { return new MOVIEROLE_HAS_MOVIE_REL(Parent, DirectionEnum.In); } }

		}

		public MovieRoleOut Out { get { return new MovieRoleOut(this); } }
		public class MovieRoleOut
		{
			private MovieRoleNode Parent;
			internal MovieRoleOut(MovieRoleNode parent)
			{
                Parent = parent;
			}
			public IFromOut_MOVIE_ROLES_REL MOVIE_ROLES { get { return new MOVIE_ROLES_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class MovieRoleAlias : AliasResult
    {
        internal MovieRoleAlias(MovieRoleNode parent)
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
						{ "Uid", new StringResult(this, "Uid", MovieGraph.Model.Datastore.Model.Entities["MovieRole"], MovieGraph.Model.Datastore.Model.Entities["MovieRole"].Properties["Uid"]) },
						{ "Role", new StringListResult(this, "Role", MovieGraph.Model.Datastore.Model.Entities["MovieRole"], MovieGraph.Model.Datastore.Model.Entities["MovieRole"].Properties["Role"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public MovieRoleNode.MovieRoleIn In { get { return new MovieRoleNode.MovieRoleIn(new MovieRoleNode(this, true)); } }
        public MovieRoleNode.MovieRoleOut Out { get { return new MovieRoleNode.MovieRoleOut(new MovieRoleNode(this, true)); } }

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
        public StringListResult Role
		{
			get
			{
				if ((object)m_Role == null)
					m_Role = (StringListResult)AliasFields["Role"];

				return m_Role;
			}
		} 
        private StringListResult m_Role = null;
    }
}
