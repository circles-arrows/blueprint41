using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Datastore.Query
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
			public IFromIn_PERSON_ACTED_IN_FILM_REL PERSON_ACTED_IN_FILM { get { return new PERSON_ACTED_IN_FILM_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_DIRECTED_FILM_REL PERSON_DIRECTED_FILM { get { return new PERSON_DIRECTED_FILM_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_PRODUCED_FILM_REL PERSON_PRODUCED_FILM { get { return new PERSON_PRODUCED_FILM_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_WROTE_FILM_REL PERSON_WROTE_FILM { get { return new PERSON_WROTE_FILM_REL(Parent, DirectionEnum.In); } }

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
						{ "Name", new StringResult(this, "Name", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"].Properties["Name"]) },
						{ "Address", new StringResult(this, "Address", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"].Properties["Address"]) },
						{ "Uid", new StringResult(this, "Uid", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"].Properties["Uid"]) },
						{ "LastModifiedOn", new DateTimeResult(this, "LastModifiedOn", Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"], Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"].Properties["LastModifiedOn"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public PersonNode.PersonIn In { get { return new PersonNode.PersonIn(new PersonNode(this, true)); } }

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
        public StringResult Address
		{
			get
			{
				if ((object)m_Address == null)
					m_Address = (StringResult)AliasFields["Address"];

				return m_Address;
			}
		} 
        private StringResult m_Address = null;
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
