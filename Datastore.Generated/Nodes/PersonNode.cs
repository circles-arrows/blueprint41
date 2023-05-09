using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Domain.Data.Manipulation;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static PersonNode Person { get { return new PersonNode(); } }
	}

	public partial class PersonNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(PersonNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(PersonNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Person";
		}

		protected override Entity GetEntity()
        {
			return m.Person.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Person.Entity.FunctionalId;
            }
        }

		internal PersonNode() { }
		internal PersonNode(PersonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PersonNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal PersonNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public PersonNode Where(JsNotation<string> AdditionalContactInfo = default, JsNotation<string> Demographics = default, JsNotation<string> EmailPromotion = default, JsNotation<string> FirstName = default, JsNotation<string> LastName = default, JsNotation<string> MiddleName = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> NameStyle = default, JsNotation<int?> PersonType = default, JsNotation<string> rowguid = default, JsNotation<string> Suffix = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PersonAlias> alias = new Lazy<PersonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (AdditionalContactInfo.HasValue) conditions.Add(new QueryCondition(alias.Value.AdditionalContactInfo, Operator.Equals, ((IValue)AdditionalContactInfo).GetValue()));
            if (Demographics.HasValue) conditions.Add(new QueryCondition(alias.Value.Demographics, Operator.Equals, ((IValue)Demographics).GetValue()));
            if (EmailPromotion.HasValue) conditions.Add(new QueryCondition(alias.Value.EmailPromotion, Operator.Equals, ((IValue)EmailPromotion).GetValue()));
            if (FirstName.HasValue) conditions.Add(new QueryCondition(alias.Value.FirstName, Operator.Equals, ((IValue)FirstName).GetValue()));
            if (LastName.HasValue) conditions.Add(new QueryCondition(alias.Value.LastName, Operator.Equals, ((IValue)LastName).GetValue()));
            if (MiddleName.HasValue) conditions.Add(new QueryCondition(alias.Value.MiddleName, Operator.Equals, ((IValue)MiddleName).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (NameStyle.HasValue) conditions.Add(new QueryCondition(alias.Value.NameStyle, Operator.Equals, ((IValue)NameStyle).GetValue()));
            if (PersonType.HasValue) conditions.Add(new QueryCondition(alias.Value.PersonType, Operator.Equals, ((IValue)PersonType).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (Suffix.HasValue) conditions.Add(new QueryCondition(alias.Value.Suffix, Operator.Equals, ((IValue)Suffix).GetValue()));
            if (Title.HasValue) conditions.Add(new QueryCondition(alias.Value.Title, Operator.Equals, ((IValue)Title).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public PersonNode Assign(JsNotation<string> AdditionalContactInfo = default, JsNotation<string> Demographics = default, JsNotation<string> EmailPromotion = default, JsNotation<string> FirstName = default, JsNotation<string> LastName = default, JsNotation<string> MiddleName = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> NameStyle = default, JsNotation<int?> PersonType = default, JsNotation<string> rowguid = default, JsNotation<string> Suffix = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PersonAlias> alias = new Lazy<PersonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (AdditionalContactInfo.HasValue) assignments.Add(new Assignment(alias.Value.AdditionalContactInfo, AdditionalContactInfo));
            if (Demographics.HasValue) assignments.Add(new Assignment(alias.Value.Demographics, Demographics));
            if (EmailPromotion.HasValue) assignments.Add(new Assignment(alias.Value.EmailPromotion, EmailPromotion));
            if (FirstName.HasValue) assignments.Add(new Assignment(alias.Value.FirstName, FirstName));
            if (LastName.HasValue) assignments.Add(new Assignment(alias.Value.LastName, LastName));
            if (MiddleName.HasValue) assignments.Add(new Assignment(alias.Value.MiddleName, MiddleName));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (NameStyle.HasValue) assignments.Add(new Assignment(alias.Value.NameStyle, NameStyle));
            if (PersonType.HasValue) assignments.Add(new Assignment(alias.Value.PersonType, PersonType));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (Suffix.HasValue) assignments.Add(new Assignment(alias.Value.Suffix, Suffix));
            if (Title.HasValue) assignments.Add(new Assignment(alias.Value.Title, Title));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public PersonNode Alias(out PersonAlias alias)
        {
            if (NodeAlias is PersonAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new PersonAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public PersonNode Alias(out PersonAlias alias, string name)
        {
            if (NodeAlias is PersonAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new PersonAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public PersonNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
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

	public class PersonAlias : AliasResult<PersonAlias, PersonListAlias>
	{
		internal PersonAlias(PersonNode parent)
		{
			Node = parent;
		}
		internal PersonAlias(PersonNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  PersonAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  PersonAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  PersonAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> AdditionalContactInfo = default, JsNotation<string> Demographics = default, JsNotation<string> EmailPromotion = default, JsNotation<string> FirstName = default, JsNotation<string> LastName = default, JsNotation<string> MiddleName = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> NameStyle = default, JsNotation<int?> PersonType = default, JsNotation<string> rowguid = default, JsNotation<string> Suffix = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (AdditionalContactInfo.HasValue) assignments.Add(new Assignment(this.AdditionalContactInfo, AdditionalContactInfo));
			if (Demographics.HasValue) assignments.Add(new Assignment(this.Demographics, Demographics));
			if (EmailPromotion.HasValue) assignments.Add(new Assignment(this.EmailPromotion, EmailPromotion));
			if (FirstName.HasValue) assignments.Add(new Assignment(this.FirstName, FirstName));
			if (LastName.HasValue) assignments.Add(new Assignment(this.LastName, LastName));
			if (MiddleName.HasValue) assignments.Add(new Assignment(this.MiddleName, MiddleName));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (NameStyle.HasValue) assignments.Add(new Assignment(this.NameStyle, NameStyle));
			if (PersonType.HasValue) assignments.Add(new Assignment(this.PersonType, PersonType));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (Suffix.HasValue) assignments.Add(new Assignment(this.Suffix, Suffix));
			if (Title.HasValue) assignments.Add(new Assignment(this.Title, Title));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
            
            return assignments.ToArray();
        }


		public override IReadOnlyDictionary<string, FieldResult> AliasFields
		{
			get
			{
				if (m_AliasFields is null)
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
				if (m_PersonType is null)
					m_PersonType = (NumericResult)AliasFields["PersonType"];

				return m_PersonType;
			}
		}
		private NumericResult m_PersonType = null;
		public StringResult NameStyle
		{
			get
			{
				if (m_NameStyle is null)
					m_NameStyle = (StringResult)AliasFields["NameStyle"];

				return m_NameStyle;
			}
		}
		private StringResult m_NameStyle = null;
		public StringResult Title
		{
			get
			{
				if (m_Title is null)
					m_Title = (StringResult)AliasFields["Title"];

				return m_Title;
			}
		}
		private StringResult m_Title = null;
		public StringResult FirstName
		{
			get
			{
				if (m_FirstName is null)
					m_FirstName = (StringResult)AliasFields["FirstName"];

				return m_FirstName;
			}
		}
		private StringResult m_FirstName = null;
		public StringResult MiddleName
		{
			get
			{
				if (m_MiddleName is null)
					m_MiddleName = (StringResult)AliasFields["MiddleName"];

				return m_MiddleName;
			}
		}
		private StringResult m_MiddleName = null;
		public StringResult LastName
		{
			get
			{
				if (m_LastName is null)
					m_LastName = (StringResult)AliasFields["LastName"];

				return m_LastName;
			}
		}
		private StringResult m_LastName = null;
		public StringResult Suffix
		{
			get
			{
				if (m_Suffix is null)
					m_Suffix = (StringResult)AliasFields["Suffix"];

				return m_Suffix;
			}
		}
		private StringResult m_Suffix = null;
		public StringResult EmailPromotion
		{
			get
			{
				if (m_EmailPromotion is null)
					m_EmailPromotion = (StringResult)AliasFields["EmailPromotion"];

				return m_EmailPromotion;
			}
		}
		private StringResult m_EmailPromotion = null;
		public StringResult AdditionalContactInfo
		{
			get
			{
				if (m_AdditionalContactInfo is null)
					m_AdditionalContactInfo = (StringResult)AliasFields["AdditionalContactInfo"];

				return m_AdditionalContactInfo;
			}
		}
		private StringResult m_AdditionalContactInfo = null;
		public StringResult Demographics
		{
			get
			{
				if (m_Demographics is null)
					m_Demographics = (StringResult)AliasFields["Demographics"];

				return m_Demographics;
			}
		}
		private StringResult m_Demographics = null;
		public StringResult rowguid
		{
			get
			{
				if (m_rowguid is null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		}
		private StringResult m_rowguid = null;
		public DateTimeResult ModifiedDate
		{
			get
			{
				if (m_ModifiedDate is null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		}
		private DateTimeResult m_ModifiedDate = null;
		public StringResult Uid
		{
			get
			{
				if (m_Uid is null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		}
		private StringResult m_Uid = null;
		public AsResult As(string aliasName, out PersonAlias alias)
		{
			alias = new PersonAlias((PersonNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class PersonListAlias : ListResult<PersonListAlias, PersonAlias>, IAliasListResult
	{
		private PersonListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PersonListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PersonListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class PersonJaggedListAlias : ListResult<PersonJaggedListAlias, PersonListAlias>, IAliasJaggedListResult
	{
		private PersonJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PersonJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PersonJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
