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
		public static SalesPersonNode SalesPerson { get { return new SalesPersonNode(); } }
	}

	public partial class SalesPersonNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(SalesPersonNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(SalesPersonNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "SalesPerson";
		}

		protected override Entity GetEntity()
        {
			return m.SalesPerson.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.SalesPerson.Entity.FunctionalId;
            }
        }

		internal SalesPersonNode() { }
		internal SalesPersonNode(SalesPersonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesPersonNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal SalesPersonNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public SalesPersonNode Where(JsNotation<string> Bonus = default, JsNotation<string> CommissionPct = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<string> SalesLastYear = default, JsNotation<string> SalesQuota = default, JsNotation<string> SalesYTD = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesPersonAlias> alias = new Lazy<SalesPersonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Bonus.HasValue) conditions.Add(new QueryCondition(alias.Value.Bonus, Operator.Equals, ((IValue)Bonus).GetValue()));
            if (CommissionPct.HasValue) conditions.Add(new QueryCondition(alias.Value.CommissionPct, Operator.Equals, ((IValue)CommissionPct).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (SalesLastYear.HasValue) conditions.Add(new QueryCondition(alias.Value.SalesLastYear, Operator.Equals, ((IValue)SalesLastYear).GetValue()));
            if (SalesQuota.HasValue) conditions.Add(new QueryCondition(alias.Value.SalesQuota, Operator.Equals, ((IValue)SalesQuota).GetValue()));
            if (SalesYTD.HasValue) conditions.Add(new QueryCondition(alias.Value.SalesYTD, Operator.Equals, ((IValue)SalesYTD).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public SalesPersonNode Assign(JsNotation<string> Bonus = default, JsNotation<string> CommissionPct = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<string> SalesLastYear = default, JsNotation<string> SalesQuota = default, JsNotation<string> SalesYTD = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesPersonAlias> alias = new Lazy<SalesPersonAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Bonus.HasValue) assignments.Add(new Assignment(alias.Value.Bonus, Bonus));
            if (CommissionPct.HasValue) assignments.Add(new Assignment(alias.Value.CommissionPct, CommissionPct));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (SalesLastYear.HasValue) assignments.Add(new Assignment(alias.Value.SalesLastYear, SalesLastYear));
            if (SalesQuota.HasValue) assignments.Add(new Assignment(alias.Value.SalesQuota, SalesQuota));
            if (SalesYTD.HasValue) assignments.Add(new Assignment(alias.Value.SalesYTD, SalesYTD));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public SalesPersonNode Alias(out SalesPersonAlias alias)
        {
            if (NodeAlias is SalesPersonAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new SalesPersonAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public SalesPersonNode Alias(out SalesPersonAlias alias, string name)
        {
            if (NodeAlias is SalesPersonAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new SalesPersonAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public SalesPersonNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public SalesPersonIn  In  { get { return new SalesPersonIn(this); } }
		public class SalesPersonIn
		{
			private SalesPersonNode Parent;
			internal SalesPersonIn(SalesPersonNode parent)
			{
				Parent = parent;
			}
			public IFromIn_SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL SALESPERSON_HAS_SALESPERSONQUOTAHISTORY { get { return new SALESPERSON_HAS_SALESPERSONQUOTAHISTORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESPERSON_HAS_SALESTERRITORY_REL SALESPERSON_HAS_SALESTERRITORY { get { return new SALESPERSON_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_SALESPERSON_IS_PERSON_REL SALESPERSON_IS_PERSON { get { return new SALESPERSON_IS_PERSON_REL(Parent, DirectionEnum.In); } }

		}

		public SalesPersonOut Out { get { return new SalesPersonOut(this); } }
		public class SalesPersonOut
		{
			private SalesPersonNode Parent;
			internal SalesPersonOut(SalesPersonNode parent)
			{
				Parent = parent;
			}
			public IFromOut_EMPLOYEE_BECOMES_SALESPERSON_REL EMPLOYEE_BECOMES_SALESPERSON { get { return new EMPLOYEE_BECOMES_SALESPERSON_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_STORE_VALID_FOR_SALESPERSON_REL STORE_VALID_FOR_SALESPERSON { get { return new STORE_VALID_FOR_SALESPERSON_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class SalesPersonAlias : AliasResult<SalesPersonAlias, SalesPersonListAlias>
	{
		internal SalesPersonAlias(SalesPersonNode parent)
		{
			Node = parent;
		}
		internal SalesPersonAlias(SalesPersonNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  SalesPersonAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  SalesPersonAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  SalesPersonAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Bonus = default, JsNotation<string> CommissionPct = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<string> SalesLastYear = default, JsNotation<string> SalesQuota = default, JsNotation<string> SalesYTD = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Bonus.HasValue) assignments.Add(new Assignment(this.Bonus, Bonus));
			if (CommissionPct.HasValue) assignments.Add(new Assignment(this.CommissionPct, CommissionPct));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (SalesLastYear.HasValue) assignments.Add(new Assignment(this.SalesLastYear, SalesLastYear));
			if (SalesQuota.HasValue) assignments.Add(new Assignment(this.SalesQuota, SalesQuota));
			if (SalesYTD.HasValue) assignments.Add(new Assignment(this.SalesYTD, SalesYTD));
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
						{ "SalesQuota", new StringResult(this, "SalesQuota", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesQuota"]) },
						{ "Bonus", new StringResult(this, "Bonus", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["Bonus"]) },
						{ "CommissionPct", new StringResult(this, "CommissionPct", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["CommissionPct"]) },
						{ "SalesYTD", new StringResult(this, "SalesYTD", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesYTD"]) },
						{ "SalesLastYear", new StringResult(this, "SalesLastYear", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesLastYear"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesPerson"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public SalesPersonNode.SalesPersonIn In { get { return new SalesPersonNode.SalesPersonIn(new SalesPersonNode(this, true)); } }
		public SalesPersonNode.SalesPersonOut Out { get { return new SalesPersonNode.SalesPersonOut(new SalesPersonNode(this, true)); } }

		public StringResult SalesQuota
		{
			get
			{
				if (m_SalesQuota is null)
					m_SalesQuota = (StringResult)AliasFields["SalesQuota"];

				return m_SalesQuota;
			}
		}
		private StringResult m_SalesQuota = null;
		public StringResult Bonus
		{
			get
			{
				if (m_Bonus is null)
					m_Bonus = (StringResult)AliasFields["Bonus"];

				return m_Bonus;
			}
		}
		private StringResult m_Bonus = null;
		public StringResult CommissionPct
		{
			get
			{
				if (m_CommissionPct is null)
					m_CommissionPct = (StringResult)AliasFields["CommissionPct"];

				return m_CommissionPct;
			}
		}
		private StringResult m_CommissionPct = null;
		public StringResult SalesYTD
		{
			get
			{
				if (m_SalesYTD is null)
					m_SalesYTD = (StringResult)AliasFields["SalesYTD"];

				return m_SalesYTD;
			}
		}
		private StringResult m_SalesYTD = null;
		public StringResult SalesLastYear
		{
			get
			{
				if (m_SalesLastYear is null)
					m_SalesLastYear = (StringResult)AliasFields["SalesLastYear"];

				return m_SalesLastYear;
			}
		}
		private StringResult m_SalesLastYear = null;
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
		public AsResult As(string aliasName, out SalesPersonAlias alias)
		{
			alias = new SalesPersonAlias((SalesPersonNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class SalesPersonListAlias : ListResult<SalesPersonListAlias, SalesPersonAlias>, IAliasListResult
	{
		private SalesPersonListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesPersonListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesPersonListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class SalesPersonJaggedListAlias : ListResult<SalesPersonJaggedListAlias, SalesPersonListAlias>, IAliasJaggedListResult
	{
		private SalesPersonJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesPersonJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesPersonJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
