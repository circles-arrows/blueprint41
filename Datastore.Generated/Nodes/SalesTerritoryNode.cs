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
		public static SalesTerritoryNode SalesTerritory { get { return new SalesTerritoryNode(); } }
	}

	public partial class SalesTerritoryNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(SalesTerritoryNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(SalesTerritoryNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "SalesTerritory";
		}

		protected override Entity GetEntity()
        {
			return m.SalesTerritory.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.SalesTerritory.Entity.FunctionalId;
            }
        }

		internal SalesTerritoryNode() { }
		internal SalesTerritoryNode(SalesTerritoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesTerritoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal SalesTerritoryNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public SalesTerritoryNode Where(JsNotation<string> CostLastYear = default, JsNotation<string> CostYTD = default, JsNotation<string> CountryRegionCode = default, JsNotation<string> Group = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> SalesLastYear = default, JsNotation<string> SalesYTD = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesTerritoryAlias> alias = new Lazy<SalesTerritoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (CostLastYear.HasValue) conditions.Add(new QueryCondition(alias.Value.CostLastYear, Operator.Equals, ((IValue)CostLastYear).GetValue()));
            if (CostYTD.HasValue) conditions.Add(new QueryCondition(alias.Value.CostYTD, Operator.Equals, ((IValue)CostYTD).GetValue()));
            if (CountryRegionCode.HasValue) conditions.Add(new QueryCondition(alias.Value.CountryRegionCode, Operator.Equals, ((IValue)CountryRegionCode).GetValue()));
            if (Group.HasValue) conditions.Add(new QueryCondition(alias.Value.Group, Operator.Equals, ((IValue)Group).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (SalesLastYear.HasValue) conditions.Add(new QueryCondition(alias.Value.SalesLastYear, Operator.Equals, ((IValue)SalesLastYear).GetValue()));
            if (SalesYTD.HasValue) conditions.Add(new QueryCondition(alias.Value.SalesYTD, Operator.Equals, ((IValue)SalesYTD).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public SalesTerritoryNode Assign(JsNotation<string> CostLastYear = default, JsNotation<string> CostYTD = default, JsNotation<string> CountryRegionCode = default, JsNotation<string> Group = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> SalesLastYear = default, JsNotation<string> SalesYTD = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SalesTerritoryAlias> alias = new Lazy<SalesTerritoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (CostLastYear.HasValue) assignments.Add(new Assignment(alias.Value.CostLastYear, CostLastYear));
            if (CostYTD.HasValue) assignments.Add(new Assignment(alias.Value.CostYTD, CostYTD));
            if (CountryRegionCode.HasValue) assignments.Add(new Assignment(alias.Value.CountryRegionCode, CountryRegionCode));
            if (Group.HasValue) assignments.Add(new Assignment(alias.Value.Group, Group));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (SalesLastYear.HasValue) assignments.Add(new Assignment(alias.Value.SalesLastYear, SalesLastYear));
            if (SalesYTD.HasValue) assignments.Add(new Assignment(alias.Value.SalesYTD, SalesYTD));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public SalesTerritoryNode Alias(out SalesTerritoryAlias alias)
        {
            if (NodeAlias is SalesTerritoryAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new SalesTerritoryAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public SalesTerritoryNode Alias(out SalesTerritoryAlias alias, string name)
        {
            if (NodeAlias is SalesTerritoryAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new SalesTerritoryAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public SalesTerritoryNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public SalesTerritoryIn  In  { get { return new SalesTerritoryIn(this); } }
		public class SalesTerritoryIn
		{
			private SalesTerritoryNode Parent;
			internal SalesTerritoryIn(SalesTerritoryNode parent)
			{
				Parent = parent;
			}
			public IFromIn_SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL SALESTERRITORY_HAS_SALESTERRITORYHISTORY { get { return new SALESTERRITORY_HAS_SALESTERRITORYHISTORY_REL(Parent, DirectionEnum.In); } }

		}

		public SalesTerritoryOut Out { get { return new SalesTerritoryOut(this); } }
		public class SalesTerritoryOut
		{
			private SalesTerritoryNode Parent;
			internal SalesTerritoryOut(SalesTerritoryNode parent)
			{
				Parent = parent;
			}
			public IFromOut_CUSTOMER_HAS_SALESTERRITORY_REL CUSTOMER_HAS_SALESTERRITORY { get { return new CUSTOMER_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL SALESORDERHEADER_CONTAINS_SALESTERRITORY { get { return new SALESORDERHEADER_CONTAINS_SALESTERRITORY_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESPERSON_HAS_SALESTERRITORY_REL SALESPERSON_HAS_SALESTERRITORY { get { return new SALESPERSON_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_STATEPROVINCE_HAS_SALESTERRITORY_REL STATEPROVINCE_HAS_SALESTERRITORY { get { return new STATEPROVINCE_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class SalesTerritoryAlias : AliasResult<SalesTerritoryAlias, SalesTerritoryListAlias>
	{
		internal SalesTerritoryAlias(SalesTerritoryNode parent)
		{
			Node = parent;
		}
		internal SalesTerritoryAlias(SalesTerritoryNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  SalesTerritoryAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  SalesTerritoryAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  SalesTerritoryAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> CostLastYear = default, JsNotation<string> CostYTD = default, JsNotation<string> CountryRegionCode = default, JsNotation<string> Group = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> SalesLastYear = default, JsNotation<string> SalesYTD = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (CostLastYear.HasValue) assignments.Add(new Assignment(this.CostLastYear, CostLastYear));
			if (CostYTD.HasValue) assignments.Add(new Assignment(this.CostYTD, CostYTD));
			if (CountryRegionCode.HasValue) assignments.Add(new Assignment(this.CountryRegionCode, CountryRegionCode));
			if (Group.HasValue) assignments.Add(new Assignment(this.Group, Group));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (SalesLastYear.HasValue) assignments.Add(new Assignment(this.SalesLastYear, SalesLastYear));
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["Name"]) },
						{ "CountryRegionCode", new StringResult(this, "CountryRegionCode", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CountryRegionCode"]) },
						{ "Group", new StringResult(this, "Group", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["Group"]) },
						{ "SalesYTD", new StringResult(this, "SalesYTD", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["SalesYTD"]) },
						{ "SalesLastYear", new StringResult(this, "SalesLastYear", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["SalesLastYear"]) },
						{ "CostYTD", new StringResult(this, "CostYTD", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CostYTD"]) },
						{ "CostLastYear", new StringResult(this, "CostLastYear", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["CostLastYear"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SalesTerritory"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesTerritory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public SalesTerritoryNode.SalesTerritoryIn In { get { return new SalesTerritoryNode.SalesTerritoryIn(new SalesTerritoryNode(this, true)); } }
		public SalesTerritoryNode.SalesTerritoryOut Out { get { return new SalesTerritoryNode.SalesTerritoryOut(new SalesTerritoryNode(this, true)); } }

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
		public StringResult CountryRegionCode
		{
			get
			{
				if (m_CountryRegionCode is null)
					m_CountryRegionCode = (StringResult)AliasFields["CountryRegionCode"];

				return m_CountryRegionCode;
			}
		}
		private StringResult m_CountryRegionCode = null;
		public StringResult Group
		{
			get
			{
				if (m_Group is null)
					m_Group = (StringResult)AliasFields["Group"];

				return m_Group;
			}
		}
		private StringResult m_Group = null;
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
		public StringResult CostYTD
		{
			get
			{
				if (m_CostYTD is null)
					m_CostYTD = (StringResult)AliasFields["CostYTD"];

				return m_CostYTD;
			}
		}
		private StringResult m_CostYTD = null;
		public StringResult CostLastYear
		{
			get
			{
				if (m_CostLastYear is null)
					m_CostLastYear = (StringResult)AliasFields["CostLastYear"];

				return m_CostLastYear;
			}
		}
		private StringResult m_CostLastYear = null;
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
		public AsResult As(string aliasName, out SalesTerritoryAlias alias)
		{
			alias = new SalesTerritoryAlias((SalesTerritoryNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class SalesTerritoryListAlias : ListResult<SalesTerritoryListAlias, SalesTerritoryAlias>, IAliasListResult
	{
		private SalesTerritoryListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesTerritoryListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesTerritoryListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class SalesTerritoryJaggedListAlias : ListResult<SalesTerritoryJaggedListAlias, SalesTerritoryListAlias>, IAliasJaggedListResult
	{
		private SalesTerritoryJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SalesTerritoryJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SalesTerritoryJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
