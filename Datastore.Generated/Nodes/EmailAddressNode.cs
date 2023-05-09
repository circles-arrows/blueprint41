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
		public static EmailAddressNode EmailAddress { get { return new EmailAddressNode(); } }
	}

	public partial class EmailAddressNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(EmailAddressNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(EmailAddressNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "EmailAddress";
		}

		protected override Entity GetEntity()
        {
			return m.EmailAddress.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.EmailAddress.Entity.FunctionalId;
            }
        }

		internal EmailAddressNode() { }
		internal EmailAddressNode(EmailAddressAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal EmailAddressNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal EmailAddressNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public EmailAddressNode Where(JsNotation<string> EmailAddr = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmailAddressAlias> alias = new Lazy<EmailAddressAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (EmailAddr.HasValue) conditions.Add(new QueryCondition(alias.Value.EmailAddr, Operator.Equals, ((IValue)EmailAddr).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public EmailAddressNode Assign(JsNotation<string> EmailAddr = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmailAddressAlias> alias = new Lazy<EmailAddressAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (EmailAddr.HasValue) assignments.Add(new Assignment(alias.Value.EmailAddr, EmailAddr));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public EmailAddressNode Alias(out EmailAddressAlias alias)
        {
            if (NodeAlias is EmailAddressAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new EmailAddressAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public EmailAddressNode Alias(out EmailAddressAlias alias, string name)
        {
            if (NodeAlias is EmailAddressAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new EmailAddressAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public EmailAddressNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
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

	public class EmailAddressAlias : AliasResult<EmailAddressAlias, EmailAddressListAlias>
	{
		internal EmailAddressAlias(EmailAddressNode parent)
		{
			Node = parent;
		}
		internal EmailAddressAlias(EmailAddressNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  EmailAddressAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  EmailAddressAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  EmailAddressAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> EmailAddr = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (EmailAddr.HasValue) assignments.Add(new Assignment(this.EmailAddr, EmailAddr));
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
				if (m_EmailAddr is null)
					m_EmailAddr = (StringResult)AliasFields["EmailAddr"];

				return m_EmailAddr;
			}
		}
		private StringResult m_EmailAddr = null;
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
		public AsResult As(string aliasName, out EmailAddressAlias alias)
		{
			alias = new EmailAddressAlias((EmailAddressNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class EmailAddressListAlias : ListResult<EmailAddressListAlias, EmailAddressAlias>, IAliasListResult
	{
		private EmailAddressListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmailAddressListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmailAddressListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class EmailAddressJaggedListAlias : ListResult<EmailAddressJaggedListAlias, EmailAddressListAlias>, IAliasJaggedListResult
	{
		private EmailAddressJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmailAddressJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmailAddressJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
