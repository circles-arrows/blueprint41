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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "PersonType", new NumericResult(this, "PersonType", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["PersonType"]) },
						{ "NameStyle", new StringResult(this, "NameStyle", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["NameStyle"]) },
						{ "Title", new StringResult(this, "Title", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["Title"]) },
						{ "FirstName", new StringResult(this, "FirstName", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["FirstName"]) },
						{ "MiddleName", new StringResult(this, "MiddleName", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["MiddleName"]) },
						{ "LastName", new StringResult(this, "LastName", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["LastName"]) },
						{ "Suffix", new StringResult(this, "Suffix", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["Suffix"]) },
						{ "EmailPromotion", new StringResult(this, "EmailPromotion", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["EmailPromotion"]) },
						{ "AdditionalContactInfo", new StringResult(this, "AdditionalContactInfo", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["AdditionalContactInfo"]) },
						{ "Demographics", new StringResult(this, "Demographics", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["Demographics"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Person"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Person"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public PersonNode.PersonIn In { get { return new PersonNode.PersonIn(new PersonNode(this, true)); } }
        public PersonNode.PersonOut Out { get { return new PersonNode.PersonOut(new PersonNode(this, true)); } }

        public NumericResult PersonType
		{
			get
			{
				if ((object)m_PersonType == null)
					m_PersonType = (NumericResult)AliasFields["PersonType"];

				return m_PersonType;
			}
		} 
        private NumericResult m_PersonType = null;
        public StringResult NameStyle
		{
			get
			{
				if ((object)m_NameStyle == null)
					m_NameStyle = (StringResult)AliasFields["NameStyle"];

				return m_NameStyle;
			}
		} 
        private StringResult m_NameStyle = null;
        public StringResult Title
		{
			get
			{
				if ((object)m_Title == null)
					m_Title = (StringResult)AliasFields["Title"];

				return m_Title;
			}
		} 
        private StringResult m_Title = null;
        public StringResult FirstName
		{
			get
			{
				if ((object)m_FirstName == null)
					m_FirstName = (StringResult)AliasFields["FirstName"];

				return m_FirstName;
			}
		} 
        private StringResult m_FirstName = null;
        public StringResult MiddleName
		{
			get
			{
				if ((object)m_MiddleName == null)
					m_MiddleName = (StringResult)AliasFields["MiddleName"];

				return m_MiddleName;
			}
		} 
        private StringResult m_MiddleName = null;
        public StringResult LastName
		{
			get
			{
				if ((object)m_LastName == null)
					m_LastName = (StringResult)AliasFields["LastName"];

				return m_LastName;
			}
		} 
        private StringResult m_LastName = null;
        public StringResult Suffix
		{
			get
			{
				if ((object)m_Suffix == null)
					m_Suffix = (StringResult)AliasFields["Suffix"];

				return m_Suffix;
			}
		} 
        private StringResult m_Suffix = null;
        public StringResult EmailPromotion
		{
			get
			{
				if ((object)m_EmailPromotion == null)
					m_EmailPromotion = (StringResult)AliasFields["EmailPromotion"];

				return m_EmailPromotion;
			}
		} 
        private StringResult m_EmailPromotion = null;
        public StringResult AdditionalContactInfo
		{
			get
			{
				if ((object)m_AdditionalContactInfo == null)
					m_AdditionalContactInfo = (StringResult)AliasFields["AdditionalContactInfo"];

				return m_AdditionalContactInfo;
			}
		} 
        private StringResult m_AdditionalContactInfo = null;
        public StringResult Demographics
		{
			get
			{
				if ((object)m_Demographics == null)
					m_Demographics = (StringResult)AliasFields["Demographics"];

				return m_Demographics;
			}
		} 
        private StringResult m_Demographics = null;
        public StringResult rowguid
		{
			get
			{
				if ((object)m_rowguid == null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		} 
        private StringResult m_rowguid = null;
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
