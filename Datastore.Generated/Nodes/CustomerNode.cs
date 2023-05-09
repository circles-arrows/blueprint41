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
		public static CustomerNode Customer { get { return new CustomerNode(); } }
	}

	public partial class CustomerNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(CustomerNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(CustomerNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Customer";
		}

		protected override Entity GetEntity()
        {
			return m.Customer.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Customer.Entity.FunctionalId;
            }
        }

		internal CustomerNode() { }
		internal CustomerNode(CustomerAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CustomerNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal CustomerNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public CustomerNode Where(JsNotation<string> AccountNumber = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CustomerAlias> alias = new Lazy<CustomerAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (AccountNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.AccountNumber, Operator.Equals, ((IValue)AccountNumber).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public CustomerNode Assign(JsNotation<string> AccountNumber = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CustomerAlias> alias = new Lazy<CustomerAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (AccountNumber.HasValue) assignments.Add(new Assignment(alias.Value.AccountNumber, AccountNumber));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public CustomerNode Alias(out CustomerAlias alias)
        {
            if (NodeAlias is CustomerAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new CustomerAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public CustomerNode Alias(out CustomerAlias alias, string name)
        {
            if (NodeAlias is CustomerAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new CustomerAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public CustomerNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public CustomerIn  In  { get { return new CustomerIn(this); } }
		public class CustomerIn
		{
			private CustomerNode Parent;
			internal CustomerIn(CustomerNode parent)
			{
				Parent = parent;
			}
			public IFromIn_CUSTOMER_HAS_PERSON_REL CUSTOMER_HAS_PERSON { get { return new CUSTOMER_HAS_PERSON_REL(Parent, DirectionEnum.In); } }
			public IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL CUSTOMER_HAS_SALESTERRITORY { get { return new CUSTOMER_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_CUSTOMER_HAS_STORE_REL CUSTOMER_HAS_STORE { get { return new CUSTOMER_HAS_STORE_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class CustomerAlias : AliasResult<CustomerAlias, CustomerListAlias>
	{
		internal CustomerAlias(CustomerNode parent)
		{
			Node = parent;
		}
		internal CustomerAlias(CustomerNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  CustomerAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  CustomerAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  CustomerAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> AccountNumber = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (AccountNumber.HasValue) assignments.Add(new Assignment(this.AccountNumber, AccountNumber));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
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
						{ "AccountNumber", new StringResult(this, "AccountNumber", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["Customer"].Properties["AccountNumber"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["Customer"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public CustomerNode.CustomerIn In { get { return new CustomerNode.CustomerIn(new CustomerNode(this, true)); } }

		public StringResult AccountNumber
		{
			get
			{
				if (m_AccountNumber is null)
					m_AccountNumber = (StringResult)AliasFields["AccountNumber"];

				return m_AccountNumber;
			}
		}
		private StringResult m_AccountNumber = null;
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
		public AsResult As(string aliasName, out CustomerAlias alias)
		{
			alias = new CustomerAlias((CustomerNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class CustomerListAlias : ListResult<CustomerListAlias, CustomerAlias>, IAliasListResult
	{
		private CustomerListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private CustomerListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private CustomerListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class CustomerJaggedListAlias : ListResult<CustomerJaggedListAlias, CustomerListAlias>, IAliasJaggedListResult
	{
		private CustomerJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private CustomerJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private CustomerJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
