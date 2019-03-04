using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static PhoneNumberTypeNode PhoneNumberType { get { return new PhoneNumberTypeNode(); } }
	}

	public partial class PhoneNumberTypeNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "PhoneNumberType";
        }

		internal PhoneNumberTypeNode() { }
		internal PhoneNumberTypeNode(PhoneNumberTypeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PhoneNumberTypeNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public PhoneNumberTypeNode Alias(out PhoneNumberTypeAlias alias)
		{
			alias = new PhoneNumberTypeAlias(this);
            NodeAlias = alias;
			return this;
		}

		public PhoneNumberTypeNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }


		public PhoneNumberTypeOut Out { get { return new PhoneNumberTypeOut(this); } }
		public class PhoneNumberTypeOut
		{
			private PhoneNumberTypeNode Parent;
			internal PhoneNumberTypeOut(PhoneNumberTypeNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PERSON_HAS_PHONENUMBERTYPE_REL PERSON_HAS_PHONENUMBERTYPE { get { return new PERSON_HAS_PHONENUMBERTYPE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class PhoneNumberTypeAlias : AliasResult
    {
        internal PhoneNumberTypeAlias(PhoneNumberTypeNode parent)
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["PhoneNumberType"], Datastore.AdventureWorks.Model.Entities["PhoneNumberType"].Properties["Name"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["PhoneNumberType"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public PhoneNumberTypeNode.PhoneNumberTypeOut Out { get { return new PhoneNumberTypeNode.PhoneNumberTypeOut(new PhoneNumberTypeNode(this, true)); } }

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
    }
}
