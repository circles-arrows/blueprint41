using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static PersonNode Person { get { return new PersonNode(); } }
	}

	public partial class PersonNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Person";
        }

		internal PersonNode() { }
		internal PersonNode(PersonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PersonNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public PersonNode Alias(out PersonAlias alias)
		{
			alias = new PersonAlias(this);
            NodeAlias = alias;
			return this;
		}

		public PersonNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }

	
		public PersonIn  In  { get { return new PersonIn(this); } }
		public class PersonIn
		{
			private PersonNode Parent;
			internal PersonIn(PersonNode parent)
			{
                Parent = parent;
			}
			public IFromIn_ACTED_IN_REL ACTED_IN { get { return new ACTED_IN_REL(Parent, DirectionEnum.In); } }
			public IFromIn_DIRECTED_REL DIRECTED { get { return new DIRECTED_REL(Parent, DirectionEnum.In); } }
			public IFromIn_FOLLOWS_REL FOLLOWS { get { return new FOLLOWS_REL(Parent, DirectionEnum.In); } }
			public IFromIn_MOVIE_REVIEWS_REL MOVIE_REVIEWS { get { return new MOVIE_REVIEWS_REL(Parent, DirectionEnum.In); } }
			public IFromIn_MOVIE_ROLES_REL MOVIE_ROLES { get { return new MOVIE_ROLES_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCED_REL PRODUCED { get { return new PRODUCED_REL(Parent, DirectionEnum.In); } }
			public IFromIn_WROTE_REL WROTE { get { return new WROTE_REL(Parent, DirectionEnum.In); } }

		}

		public PersonOut Out { get { return new PersonOut(this); } }
		public class PersonOut
		{
			private PersonNode Parent;
			internal PersonOut(PersonNode parent)
			{
                Parent = parent;
			}
			public IFromOut_FOLLOWS_REL FOLLOWS { get { return new FOLLOWS_REL(Parent, DirectionEnum.Out); } }
		}

		public PersonAny Any { get { return new PersonAny(this); } }
		public class PersonAny
		{
			private PersonNode Parent;
			internal PersonAny(PersonNode parent)
			{
                Parent = parent;
			}
			public IFromAny_FOLLOWS_REL FOLLOWS { get { return new FOLLOWS_REL(Parent, DirectionEnum.None); } }
		}
	}

    public class PersonAlias : AliasResult
    {
        internal PersonAlias(PersonNode parent)
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
						{ "name", new StringResult(this, "name", MovieGraph.Model.Datastore.Model.Entities["Person"], MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["name"]) },
						{ "born", new NumericResult(this, "born", MovieGraph.Model.Datastore.Model.Entities["Person"], MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["born"]) },
						{ "Uid", new StringResult(this, "Uid", MovieGraph.Model.Datastore.Model.Entities["Person"], MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public PersonNode.PersonIn In { get { return new PersonNode.PersonIn(new PersonNode(this, true)); } }
        public PersonNode.PersonOut Out { get { return new PersonNode.PersonOut(new PersonNode(this, true)); } }
        public PersonNode.PersonAny Any { get { return new PersonNode.PersonAny(new PersonNode(this, true)); } }

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
        public NumericResult born
		{
			get
			{
				if ((object)m_born == null)
					m_born = (NumericResult)AliasFields["born"];

				return m_born;
			}
		} 
        private NumericResult m_born = null;
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
