using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Datastore.Query
{
	public partial class Node
	{
		public static ActorNode Actor { get { return new ActorNode(); } }
	}

	public partial class ActorNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Actor";
        }

		internal ActorNode() { }
		internal ActorNode(ActorAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ActorNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public ActorNode Alias(out ActorAlias alias)
		{
			alias = new ActorAlias(this);
            NodeAlias = alias;
			return this;
		}

		public ActorNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public ActorIn  In  { get { return new ActorIn(this); } }
		public class ActorIn
		{
			private ActorNode Parent;
			internal ActorIn(ActorNode parent)
			{
                Parent = parent;
			}
			public IFromIn_PERSON_ACTED_IN_FILM_REL PERSON_ACTED_IN_FILM { get { return new PERSON_ACTED_IN_FILM_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_DIRECTED_FILM_REL PERSON_DIRECTED_FILM { get { return new PERSON_DIRECTED_FILM_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_PRODUCED_FILM_REL PERSON_PRODUCED_FILM { get { return new PERSON_PRODUCED_FILM_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_WROTE_FILM_REL PERSON_WROTE_FILM { get { return new PERSON_WROTE_FILM_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class ActorAlias : AliasResult
    {
        internal ActorAlias(ActorNode parent)
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
						{ "Uid", new StringResult(this, "Uid", Blueprint41Test.MovieModel.Model.Entities["Actor"], Blueprint41Test.MovieModel.Model.Entities["Actor"].Properties["Uid"]) },
						{ "name", new StringResult(this, "name", Blueprint41Test.MovieModel.Model.Entities["Actor"], Blueprint41Test.MovieModel.Model.Entities["Actor"].Properties["name"]) },
						{ "born", new StringListResult(this, "born", Blueprint41Test.MovieModel.Model.Entities["Actor"], Blueprint41Test.MovieModel.Model.Entities["Actor"].Properties["born"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ActorNode.ActorIn In { get { return new ActorNode.ActorIn(new ActorNode(this, true)); } }

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
        public StringResult name
		{
			get
			{
				if ((object)m_name == null)
					m_name = (StringResult)AliasFields["name"];

				return m_name;
			}
		} 
        private StringResult m_name = null;
        public StringListResult born
		{
			get
			{
				if ((object)m_born == null)
					m_born = (StringListResult)AliasFields["born"];

				return m_born;
			}
		} 
        private StringListResult m_born = null;
    }
}
