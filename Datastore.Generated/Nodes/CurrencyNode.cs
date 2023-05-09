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
		public static CurrencyNode Currency { get { return new CurrencyNode(); } }
	}

	public partial class CurrencyNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(CurrencyNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(CurrencyNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Currency";
		}

		protected override Entity GetEntity()
        {
			return m.Currency.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Currency.Entity.FunctionalId;
            }
        }

		internal CurrencyNode() { }
		internal CurrencyNode(CurrencyAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CurrencyNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal CurrencyNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public CurrencyNode Where(JsNotation<string> CurrencyCode = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CurrencyAlias> alias = new Lazy<CurrencyAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (CurrencyCode.HasValue) conditions.Add(new QueryCondition(alias.Value.CurrencyCode, Operator.Equals, ((IValue)CurrencyCode).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public CurrencyNode Assign(JsNotation<string> CurrencyCode = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CurrencyAlias> alias = new Lazy<CurrencyAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (CurrencyCode.HasValue) assignments.Add(new Assignment(alias.Value.CurrencyCode, CurrencyCode));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public CurrencyNode Alias(out CurrencyAlias alias)
        {
            if (NodeAlias is CurrencyAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new CurrencyAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public CurrencyNode Alias(out CurrencyAlias alias, string name)
        {
            if (NodeAlias is CurrencyAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new CurrencyAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public CurrencyNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public CurrencyOut Out { get { return new CurrencyOut(this); } }
		public class CurrencyOut
		{
			private CurrencyNode Parent;
			internal CurrencyOut(CurrencyNode parent)
			{
				Parent = parent;
			}
			public IFromOut_CURRENCYRATE_HAS_CURRENCY_REL CURRENCYRATE_HAS_CURRENCY { get { return new CURRENCYRATE_HAS_CURRENCY_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class CurrencyAlias : AliasResult<CurrencyAlias, CurrencyListAlias>
	{
		internal CurrencyAlias(CurrencyNode parent)
		{
			Node = parent;
		}
		internal CurrencyAlias(CurrencyNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  CurrencyAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  CurrencyAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  CurrencyAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> CurrencyCode = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (CurrencyCode.HasValue) assignments.Add(new Assignment(this.CurrencyCode, CurrencyCode));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
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
						{ "CurrencyCode", new StringResult(this, "CurrencyCode", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["Currency"].Properties["CurrencyCode"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["Currency"].Properties["Name"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Currency"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public CurrencyNode.CurrencyOut Out { get { return new CurrencyNode.CurrencyOut(new CurrencyNode(this, true)); } }

		public StringResult CurrencyCode
		{
			get
			{
				if (m_CurrencyCode is null)
					m_CurrencyCode = (StringResult)AliasFields["CurrencyCode"];

				return m_CurrencyCode;
			}
		}
		private StringResult m_CurrencyCode = null;
		public StringResult Name
		{
			get
			{
				if (m_Name is null)
					m_Name = (StringResult)AliasFields["Name"];

				return m_Name;
			}
		}
		private StringResult m_Name = null;
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
		public AsResult As(string aliasName, out CurrencyAlias alias)
		{
			alias = new CurrencyAlias((CurrencyNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class CurrencyListAlias : ListResult<CurrencyListAlias, CurrencyAlias>, IAliasListResult
	{
		private CurrencyListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private CurrencyListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private CurrencyListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class CurrencyJaggedListAlias : ListResult<CurrencyJaggedListAlias, CurrencyListAlias>, IAliasJaggedListResult
	{
		private CurrencyJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private CurrencyJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private CurrencyJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
