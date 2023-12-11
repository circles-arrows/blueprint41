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
		public static MovieRoleNode MovieRole { get { return new MovieRoleNode(); } }
	}

	public partial class MovieRoleNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(MovieRoleNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(MovieRoleNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "MovieRole";
		}

		protected override Entity GetEntity()
        {
			return m.MovieRole.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.MovieRole.Entity.FunctionalId;
            }
        }

		internal MovieRoleNode() { }
		internal MovieRoleNode(MovieRoleAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal MovieRoleNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal MovieRoleNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public MovieRoleNode Where(JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<MovieRoleAlias> alias = new Lazy<MovieRoleAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public MovieRoleNode Assign(JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<MovieRoleAlias> alias = new Lazy<MovieRoleAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public MovieRoleNode Alias(out MovieRoleAlias alias)
        {
            if (NodeAlias is MovieRoleAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new MovieRoleAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public MovieRoleNode Alias(out MovieRoleAlias alias, string name)
        {
            if (NodeAlias is MovieRoleAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new MovieRoleAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public MovieRoleNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
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

	public class MovieRoleAlias : AliasResult<MovieRoleAlias, MovieRoleListAlias>
	{
		internal MovieRoleAlias(MovieRoleNode parent)
		{
			Node = parent;
		}
		internal MovieRoleAlias(MovieRoleNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  MovieRoleAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  MovieRoleAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  MovieRoleAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
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
				if (m_Uid is null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		}
		private StringResult m_Uid = null;
		public StringListResult Role
		{
			get
			{
				if (m_Role is null)
					m_Role = (StringListResult)AliasFields["Role"];

				return m_Role;
			}
		}
		private StringListResult m_Role = null;
		public AsResult As(string aliasName, out MovieRoleAlias alias)
		{
			alias = new MovieRoleAlias((MovieRoleNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class MovieRoleListAlias : ListResult<MovieRoleListAlias, MovieRoleAlias>, IAliasListResult
	{
		private MovieRoleListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private MovieRoleListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private MovieRoleListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class MovieRoleJaggedListAlias : ListResult<MovieRoleJaggedListAlias, MovieRoleListAlias>, IAliasJaggedListResult
	{
		private MovieRoleJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private MovieRoleJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private MovieRoleJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
