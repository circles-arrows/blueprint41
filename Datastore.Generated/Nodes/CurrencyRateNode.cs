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
		public static CurrencyRateNode CurrencyRate { get { return new CurrencyRateNode(); } }
	}

	public partial class CurrencyRateNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(CurrencyRateNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(CurrencyRateNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "CurrencyRate";
		}

		protected override Entity GetEntity()
        {
			return m.CurrencyRate.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.CurrencyRate.Entity.FunctionalId;
            }
        }

		internal CurrencyRateNode() { }
		internal CurrencyRateNode(CurrencyRateAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CurrencyRateNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal CurrencyRateNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public CurrencyRateNode Where(JsNotation<string> AverageRate = default, JsNotation<System.DateTime> CurrencyRateDate = default, JsNotation<string> EndOfDayRate = default, JsNotation<string> FromCurrencyCode = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> ToCurrencyCode = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CurrencyRateAlias> alias = new Lazy<CurrencyRateAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (AverageRate.HasValue) conditions.Add(new QueryCondition(alias.Value.AverageRate, Operator.Equals, ((IValue)AverageRate).GetValue()));
            if (CurrencyRateDate.HasValue) conditions.Add(new QueryCondition(alias.Value.CurrencyRateDate, Operator.Equals, ((IValue)CurrencyRateDate).GetValue()));
            if (EndOfDayRate.HasValue) conditions.Add(new QueryCondition(alias.Value.EndOfDayRate, Operator.Equals, ((IValue)EndOfDayRate).GetValue()));
            if (FromCurrencyCode.HasValue) conditions.Add(new QueryCondition(alias.Value.FromCurrencyCode, Operator.Equals, ((IValue)FromCurrencyCode).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (ToCurrencyCode.HasValue) conditions.Add(new QueryCondition(alias.Value.ToCurrencyCode, Operator.Equals, ((IValue)ToCurrencyCode).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public CurrencyRateNode Assign(JsNotation<string> AverageRate = default, JsNotation<System.DateTime> CurrencyRateDate = default, JsNotation<string> EndOfDayRate = default, JsNotation<string> FromCurrencyCode = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> ToCurrencyCode = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CurrencyRateAlias> alias = new Lazy<CurrencyRateAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (AverageRate.HasValue) assignments.Add(new Assignment(alias.Value.AverageRate, AverageRate));
            if (CurrencyRateDate.HasValue) assignments.Add(new Assignment(alias.Value.CurrencyRateDate, CurrencyRateDate));
            if (EndOfDayRate.HasValue) assignments.Add(new Assignment(alias.Value.EndOfDayRate, EndOfDayRate));
            if (FromCurrencyCode.HasValue) assignments.Add(new Assignment(alias.Value.FromCurrencyCode, FromCurrencyCode));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (ToCurrencyCode.HasValue) assignments.Add(new Assignment(alias.Value.ToCurrencyCode, ToCurrencyCode));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public CurrencyRateNode Alias(out CurrencyRateAlias alias)
        {
            if (NodeAlias is CurrencyRateAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new CurrencyRateAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public CurrencyRateNode Alias(out CurrencyRateAlias alias, string name)
        {
            if (NodeAlias is CurrencyRateAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new CurrencyRateAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public CurrencyRateNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public CurrencyRateIn  In  { get { return new CurrencyRateIn(this); } }
		public class CurrencyRateIn
		{
			private CurrencyRateNode Parent;
			internal CurrencyRateIn(CurrencyRateNode parent)
			{
				Parent = parent;
			}
			public IFromIn_CURRENCYRATE_HAS_CURRENCY_REL CURRENCYRATE_HAS_CURRENCY { get { return new CURRENCYRATE_HAS_CURRENCY_REL(Parent, DirectionEnum.In); } }

		}

		public CurrencyRateOut Out { get { return new CurrencyRateOut(this); } }
		public class CurrencyRateOut
		{
			private CurrencyRateNode Parent;
			internal CurrencyRateOut(CurrencyRateNode parent)
			{
				Parent = parent;
			}
			public IFromOut_SALESORDERHEADER_HAS_CURRENCYRATE_REL SALESORDERHEADER_HAS_CURRENCYRATE { get { return new SALESORDERHEADER_HAS_CURRENCYRATE_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class CurrencyRateAlias : AliasResult<CurrencyRateAlias, CurrencyRateListAlias>
	{
		internal CurrencyRateAlias(CurrencyRateNode parent)
		{
			Node = parent;
		}
		internal CurrencyRateAlias(CurrencyRateNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  CurrencyRateAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  CurrencyRateAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  CurrencyRateAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> AverageRate = default, JsNotation<System.DateTime> CurrencyRateDate = default, JsNotation<string> EndOfDayRate = default, JsNotation<string> FromCurrencyCode = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> ToCurrencyCode = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (AverageRate.HasValue) assignments.Add(new Assignment(this.AverageRate, AverageRate));
			if (CurrencyRateDate.HasValue) assignments.Add(new Assignment(this.CurrencyRateDate, CurrencyRateDate));
			if (EndOfDayRate.HasValue) assignments.Add(new Assignment(this.EndOfDayRate, EndOfDayRate));
			if (FromCurrencyCode.HasValue) assignments.Add(new Assignment(this.FromCurrencyCode, FromCurrencyCode));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (ToCurrencyCode.HasValue) assignments.Add(new Assignment(this.ToCurrencyCode, ToCurrencyCode));
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
						{ "CurrencyRateDate", new DateTimeResult(this, "CurrencyRateDate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["CurrencyRateDate"]) },
						{ "FromCurrencyCode", new StringResult(this, "FromCurrencyCode", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["FromCurrencyCode"]) },
						{ "ToCurrencyCode", new StringResult(this, "ToCurrencyCode", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["ToCurrencyCode"]) },
						{ "AverageRate", new StringResult(this, "AverageRate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["AverageRate"]) },
						{ "EndOfDayRate", new StringResult(this, "EndOfDayRate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["EndOfDayRate"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["CurrencyRate"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public CurrencyRateNode.CurrencyRateIn In { get { return new CurrencyRateNode.CurrencyRateIn(new CurrencyRateNode(this, true)); } }
		public CurrencyRateNode.CurrencyRateOut Out { get { return new CurrencyRateNode.CurrencyRateOut(new CurrencyRateNode(this, true)); } }

		public DateTimeResult CurrencyRateDate
		{
			get
			{
				if (m_CurrencyRateDate is null)
					m_CurrencyRateDate = (DateTimeResult)AliasFields["CurrencyRateDate"];

				return m_CurrencyRateDate;
			}
		}
		private DateTimeResult m_CurrencyRateDate = null;
		public StringResult FromCurrencyCode
		{
			get
			{
				if (m_FromCurrencyCode is null)
					m_FromCurrencyCode = (StringResult)AliasFields["FromCurrencyCode"];

				return m_FromCurrencyCode;
			}
		}
		private StringResult m_FromCurrencyCode = null;
		public StringResult ToCurrencyCode
		{
			get
			{
				if (m_ToCurrencyCode is null)
					m_ToCurrencyCode = (StringResult)AliasFields["ToCurrencyCode"];

				return m_ToCurrencyCode;
			}
		}
		private StringResult m_ToCurrencyCode = null;
		public StringResult AverageRate
		{
			get
			{
				if (m_AverageRate is null)
					m_AverageRate = (StringResult)AliasFields["AverageRate"];

				return m_AverageRate;
			}
		}
		private StringResult m_AverageRate = null;
		public StringResult EndOfDayRate
		{
			get
			{
				if (m_EndOfDayRate is null)
					m_EndOfDayRate = (StringResult)AliasFields["EndOfDayRate"];

				return m_EndOfDayRate;
			}
		}
		private StringResult m_EndOfDayRate = null;
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
		public AsResult As(string aliasName, out CurrencyRateAlias alias)
		{
			alias = new CurrencyRateAlias((CurrencyRateNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class CurrencyRateListAlias : ListResult<CurrencyRateListAlias, CurrencyRateAlias>, IAliasListResult
	{
		private CurrencyRateListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private CurrencyRateListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private CurrencyRateListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class CurrencyRateJaggedListAlias : ListResult<CurrencyRateJaggedListAlias, CurrencyRateListAlias>, IAliasJaggedListResult
	{
		private CurrencyRateJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private CurrencyRateJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private CurrencyRateJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
