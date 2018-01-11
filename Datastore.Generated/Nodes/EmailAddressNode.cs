using System;
using Blueprint41;
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
            EmailAddr = new StringResult(this, "EmailAddr", Datastore.AdventureWorks.Model.Entities["EmailAddress"], Datastore.AdventureWorks.Model.Entities["EmailAddress"].Properties["EmailAddr"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["EmailAddress"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public EmailAddressNode.EmailAddressOut Out { get { return new EmailAddressNode.EmailAddressOut(new EmailAddressNode(this, true)); } }

        public StringResult EmailAddr { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
