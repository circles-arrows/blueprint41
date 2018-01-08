
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static PersonNode Person { get { return new PersonNode(); } }
	}

	public partial class PersonNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Person";
            }
        }

		internal PersonNode() { }
		internal PersonNode(PersonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PersonNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public PersonNode Alias(out PersonAlias alias)
		{
			alias = new PersonAlias(this);
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
			public IFromIn_PERSON_BECOMES_EMPLOYEE_REL PERSON_BECOMES_EMPLOYEE { get { return new PERSON_BECOMES_EMPLOYEE_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_HAS_ADDRESS_REL PERSON_HAS_ADDRESS { get { return new PERSON_HAS_ADDRESS_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_HAS_CONTACTTYPE_REL PERSON_HAS_CONTACTTYPE { get { return new PERSON_HAS_CONTACTTYPE_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_HAS_EMAILADDRESS_REL PERSON_HAS_EMAILADDRESS { get { return new PERSON_HAS_EMAILADDRESS_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_HAS_PASSWORD_REL PERSON_HAS_PASSWORD { get { return new PERSON_HAS_PASSWORD_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_HAS_PHONENUMBERTYPE_REL PERSON_HAS_PHONENUMBERTYPE { get { return new PERSON_HAS_PHONENUMBERTYPE_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_VALID_FOR_CREDITCARD_REL PERSON_VALID_FOR_CREDITCARD { get { return new PERSON_VALID_FOR_CREDITCARD_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PERSON_VALID_FOR_DOCUMENT_REL PERSON_VALID_FOR_DOCUMENT { get { return new PERSON_VALID_FOR_DOCUMENT_REL(Parent, DirectionEnum.In); } }

		}

		public PersonOut Out { get { return new PersonOut(this); } }
		public class PersonOut
		{
			private PersonNode Parent;
			internal PersonOut(PersonNode parent)
			{
                Parent = parent;
			}
			public IFromOut_CUSTOMER_HAS_PERSON_REL CUSTOMER_HAS_PERSON { get { return new CUSTOMER_HAS_PERSON_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESPERSON_IS_PERSON_REL SALESPERSON_IS_PERSON { get { return new SALESPERSON_IS_PERSON_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class PersonAlias : AliasResult
    {
        internal PersonAlias(PersonNode parent)
        {
			Node = parent;
            PersonType = new NumericResult(this, "PersonType", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["PersonType"]);
            NameStyle = new StringResult(this, "NameStyle", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["NameStyle"]);
            Title = new StringResult(this, "Title", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["Title"]);
            FirstName = new StringResult(this, "FirstName", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["FirstName"]);
            MiddleName = new StringResult(this, "MiddleName", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["MiddleName"]);
            LastName = new StringResult(this, "LastName", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["LastName"]);
            Suffix = new StringResult(this, "Suffix", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["Suffix"]);
            EmailPromotion = new StringResult(this, "EmailPromotion", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["EmailPromotion"]);
            AdditionalContactInfo = new StringResult(this, "AdditionalContactInfo", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["AdditionalContactInfo"]);
            Demographics = new StringResult(this, "Demographics", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["Demographics"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public PersonNode.PersonIn In { get { return new PersonNode.PersonIn(new PersonNode(this, true)); } }
        public PersonNode.PersonOut Out { get { return new PersonNode.PersonOut(new PersonNode(this, true)); } }

        public NumericResult PersonType { get; private set; } 
        public StringResult NameStyle { get; private set; } 
        public StringResult Title { get; private set; } 
        public StringResult FirstName { get; private set; } 
        public StringResult MiddleName { get; private set; } 
        public StringResult LastName { get; private set; } 
        public StringResult Suffix { get; private set; } 
        public StringResult EmailPromotion { get; private set; } 
        public StringResult AdditionalContactInfo { get; private set; } 
        public StringResult Demographics { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
