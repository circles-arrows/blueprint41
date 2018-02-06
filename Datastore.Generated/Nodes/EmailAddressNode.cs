using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static EmailAddressNode EmailAddress { get { return new EmailAddressNode(); } }
	}

	public partial class EmailAddressNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "EmailAddress";
            }
        }

		internal EmailAddressNode() { }
		internal EmailAddressNode(EmailAddressAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal EmailAddressNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public EmailAddressNode Alias(out EmailAddressAlias alias)
		{
			alias = new EmailAddressAlias(this);
            NodeAlias = alias;
			return this;
		}


		public EmailAddressOut Out { get { return new EmailAddressOut(this); } }
		public class EmailAddressOut
		{
			private EmailAddressNode Parent;
			internal EmailAddressOut(EmailAddressNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PERSON_HAS_EMAILADDRESS_REL PERSON_HAS_EMAILADDRESS { get { return new PERSON_HAS_EMAILADDRESS_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class EmailAddressAlias : AliasResult
    {
        internal EmailAddressAlias(EmailAddressNode parent)
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
						{ "EmailAddr", new StringResult(this, "EmailAddr", Datastore.AdventureWorks.Model.Entities["EmailAddress"], Datastore.AdventureWorks.Model.Entities["EmailAddress"].Properties["EmailAddr"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["EmailAddress"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public EmailAddressNode.EmailAddressOut Out { get { return new EmailAddressNode.EmailAddressOut(new EmailAddressNode(this, true)); } }

        public StringResult EmailAddr
		{
			get
			{
				if ((object)m_EmailAddr == null)
					m_EmailAddr = (StringResult)AliasFields["EmailAddr"];

				return m_EmailAddr;
			}
		} 
        private StringResult m_EmailAddr = null;
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
