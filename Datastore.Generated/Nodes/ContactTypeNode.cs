using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ContactTypeNode ContactType { get { return new ContactTypeNode(); } }
	}

	public partial class ContactTypeNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "ContactType";
        }

		internal ContactTypeNode() { }
		internal ContactTypeNode(ContactTypeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ContactTypeNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public ContactTypeNode Alias(out ContactTypeAlias alias)
		{
			alias = new ContactTypeAlias(this);
            NodeAlias = alias;
			return this;
		}

		public ContactTypeNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }


		public ContactTypeOut Out { get { return new ContactTypeOut(this); } }
		public class ContactTypeOut
		{
			private ContactTypeNode Parent;
			internal ContactTypeOut(ContactTypeNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PERSON_HAS_CONTACTTYPE_REL PERSON_HAS_CONTACTTYPE { get { return new PERSON_HAS_CONTACTTYPE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ContactTypeAlias : AliasResult
    {
        internal ContactTypeAlias(ContactTypeNode parent)
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ContactType"], Datastore.AdventureWorks.Model.Entities["ContactType"].Properties["Name"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ContactType"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ContactType"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ContactTypeNode.ContactTypeOut Out { get { return new ContactTypeNode.ContactTypeOut(new ContactTypeNode(this, true)); } }

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
        public DateTimeResult ModifiedDate
		{
			get
			{
				if ((object)m_ModifiedDate == null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		} 
        private DateTimeResult m_ModifiedDate = null;
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
