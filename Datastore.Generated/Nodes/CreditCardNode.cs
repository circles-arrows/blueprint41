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
		public static CreditCardNode CreditCard { get { return new CreditCardNode(); } }
	}

	public partial class CreditCardNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(CreditCardNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(CreditCardNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "CreditCard";
		}

		protected override Entity GetEntity()
        {
			return m.CreditCard.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.CreditCard.Entity.FunctionalId;
            }
        }

		internal CreditCardNode() { }
		internal CreditCardNode(CreditCardAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CreditCardNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal CreditCardNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public CreditCardNode Where(JsNotation<string> CardNumber = default, JsNotation<string> CardType = default, JsNotation<string> ExpMonth = default, JsNotation<string> ExpYear = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CreditCardAlias> alias = new Lazy<CreditCardAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (CardNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.CardNumber, Operator.Equals, ((IValue)CardNumber).GetValue()));
            if (CardType.HasValue) conditions.Add(new QueryCondition(alias.Value.CardType, Operator.Equals, ((IValue)CardType).GetValue()));
            if (ExpMonth.HasValue) conditions.Add(new QueryCondition(alias.Value.ExpMonth, Operator.Equals, ((IValue)ExpMonth).GetValue()));
            if (ExpYear.HasValue) conditions.Add(new QueryCondition(alias.Value.ExpYear, Operator.Equals, ((IValue)ExpYear).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public CreditCardNode Assign(JsNotation<string> CardNumber = default, JsNotation<string> CardType = default, JsNotation<string> ExpMonth = default, JsNotation<string> ExpYear = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CreditCardAlias> alias = new Lazy<CreditCardAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (CardNumber.HasValue) assignments.Add(new Assignment(alias.Value.CardNumber, CardNumber));
            if (CardType.HasValue) assignments.Add(new Assignment(alias.Value.CardType, CardType));
            if (ExpMonth.HasValue) assignments.Add(new Assignment(alias.Value.ExpMonth, ExpMonth));
            if (ExpYear.HasValue) assignments.Add(new Assignment(alias.Value.ExpYear, ExpYear));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public CreditCardNode Alias(out CreditCardAlias alias)
        {
            if (NodeAlias is CreditCardAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new CreditCardAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public CreditCardNode Alias(out CreditCardAlias alias, string name)
        {
            if (NodeAlias is CreditCardAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new CreditCardAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public CreditCardNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public CreditCardOut Out { get { return new CreditCardOut(this); } }
		public class CreditCardOut
		{
			private CreditCardNode Parent;
			internal CreditCardOut(CreditCardNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PERSON_VALID_FOR_CREDITCARD_REL PERSON_VALID_FOR_CREDITCARD { get { return new PERSON_VALID_FOR_CREDITCARD_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESORDERHEADER_HAS_CREDITCARD_REL SALESORDERHEADER_HAS_CREDITCARD { get { return new SALESORDERHEADER_HAS_CREDITCARD_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class CreditCardAlias : AliasResult<CreditCardAlias, CreditCardListAlias>
	{
		internal CreditCardAlias(CreditCardNode parent)
		{
			Node = parent;
		}
		internal CreditCardAlias(CreditCardNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  CreditCardAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  CreditCardAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  CreditCardAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> CardNumber = default, JsNotation<string> CardType = default, JsNotation<string> ExpMonth = default, JsNotation<string> ExpYear = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (CardNumber.HasValue) assignments.Add(new Assignment(this.CardNumber, CardNumber));
			if (CardType.HasValue) assignments.Add(new Assignment(this.CardType, CardType));
			if (ExpMonth.HasValue) assignments.Add(new Assignment(this.ExpMonth, ExpMonth));
			if (ExpYear.HasValue) assignments.Add(new Assignment(this.ExpYear, ExpYear));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
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
						{ "CardType", new StringResult(this, "CardType", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["CardType"]) },
						{ "CardNumber", new StringResult(this, "CardNumber", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["CardNumber"]) },
						{ "ExpMonth", new StringResult(this, "ExpMonth", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["ExpMonth"]) },
						{ "ExpYear", new StringResult(this, "ExpYear", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["ExpYear"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["CreditCard"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public CreditCardNode.CreditCardOut Out { get { return new CreditCardNode.CreditCardOut(new CreditCardNode(this, true)); } }

		public StringResult CardType
		{
			get
			{
				if (m_CardType is null)
					m_CardType = (StringResult)AliasFields["CardType"];

				return m_CardType;
			}
		}
		private StringResult m_CardType = null;
		public StringResult CardNumber
		{
			get
			{
				if (m_CardNumber is null)
					m_CardNumber = (StringResult)AliasFields["CardNumber"];

				return m_CardNumber;
			}
		}
		private StringResult m_CardNumber = null;
		public StringResult ExpMonth
		{
			get
			{
				if (m_ExpMonth is null)
					m_ExpMonth = (StringResult)AliasFields["ExpMonth"];

				return m_ExpMonth;
			}
		}
		private StringResult m_ExpMonth = null;
		public StringResult ExpYear
		{
			get
			{
				if (m_ExpYear is null)
					m_ExpYear = (StringResult)AliasFields["ExpYear"];

				return m_ExpYear;
			}
		}
		private StringResult m_ExpYear = null;
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
		public AsResult As(string aliasName, out CreditCardAlias alias)
		{
			alias = new CreditCardAlias((CreditCardNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class CreditCardListAlias : ListResult<CreditCardListAlias, CreditCardAlias>, IAliasListResult
	{
		private CreditCardListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private CreditCardListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private CreditCardListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class CreditCardJaggedListAlias : ListResult<CreditCardJaggedListAlias, CreditCardListAlias>, IAliasJaggedListResult
	{
		private CreditCardJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private CreditCardJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private CreditCardJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
