
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static PhoneNumberTypeNode PhoneNumberType { get { return new PhoneNumberTypeNode(); } }
	}

	public partial class PhoneNumberTypeNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "PhoneNumberType";
            }
        }

		internal PhoneNumberTypeNode() { }
		internal PhoneNumberTypeNode(PhoneNumberTypeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PhoneNumberTypeNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public PhoneNumberTypeNode Alias(out PhoneNumberTypeAlias alias)
		{
			alias = new PhoneNumberTypeAlias(this);
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
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["PhoneNumberType"], Datastore.AdventureWorks.Model.Entities["PhoneNumberType"].Properties["Name"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["PhoneNumberType"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public PhoneNumberTypeNode.PhoneNumberTypeOut Out { get { return new PhoneNumberTypeNode.PhoneNumberTypeOut(new PhoneNumberTypeNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
