using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ContactTypeNode ContactType { get { return new ContactTypeNode(); } }
	}

	public partial class ContactTypeNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ContactType";
            }
        }

		internal ContactTypeNode() { }
		internal ContactTypeNode(ContactTypeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ContactTypeNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ContactTypeNode Alias(out ContactTypeAlias alias)
		{
			alias = new ContactTypeAlias(this);
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
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ContactType"], Datastore.AdventureWorks.Model.Entities["ContactType"].Properties["Name"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ContactType"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ContactType"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public ContactTypeNode.ContactTypeOut Out { get { return new ContactTypeNode.ContactTypeOut(new ContactTypeNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
