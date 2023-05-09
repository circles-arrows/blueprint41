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
		public static StateProvinceNode StateProvince { get { return new StateProvinceNode(); } }
	}

	public partial class StateProvinceNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(StateProvinceNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(StateProvinceNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "StateProvince";
		}

		protected override Entity GetEntity()
        {
			return m.StateProvince.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.StateProvince.Entity.FunctionalId;
            }
        }

		internal StateProvinceNode() { }
		internal StateProvinceNode(StateProvinceAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal StateProvinceNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal StateProvinceNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public StateProvinceNode Where(JsNotation<string> CountryRegionCode = default, JsNotation<bool> IsOnlyStateProvinceFlag = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> StateProvinceCode = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<StateProvinceAlias> alias = new Lazy<StateProvinceAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (CountryRegionCode.HasValue) conditions.Add(new QueryCondition(alias.Value.CountryRegionCode, Operator.Equals, ((IValue)CountryRegionCode).GetValue()));
            if (IsOnlyStateProvinceFlag.HasValue) conditions.Add(new QueryCondition(alias.Value.IsOnlyStateProvinceFlag, Operator.Equals, ((IValue)IsOnlyStateProvinceFlag).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (StateProvinceCode.HasValue) conditions.Add(new QueryCondition(alias.Value.StateProvinceCode, Operator.Equals, ((IValue)StateProvinceCode).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public StateProvinceNode Assign(JsNotation<string> CountryRegionCode = default, JsNotation<bool> IsOnlyStateProvinceFlag = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> StateProvinceCode = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<StateProvinceAlias> alias = new Lazy<StateProvinceAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (CountryRegionCode.HasValue) assignments.Add(new Assignment(alias.Value.CountryRegionCode, CountryRegionCode));
            if (IsOnlyStateProvinceFlag.HasValue) assignments.Add(new Assignment(alias.Value.IsOnlyStateProvinceFlag, IsOnlyStateProvinceFlag));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (StateProvinceCode.HasValue) assignments.Add(new Assignment(alias.Value.StateProvinceCode, StateProvinceCode));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public StateProvinceNode Alias(out StateProvinceAlias alias)
        {
            if (NodeAlias is StateProvinceAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new StateProvinceAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public StateProvinceNode Alias(out StateProvinceAlias alias, string name)
        {
            if (NodeAlias is StateProvinceAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new StateProvinceAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public StateProvinceNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public StateProvinceIn  In  { get { return new StateProvinceIn(this); } }
		public class StateProvinceIn
		{
			private StateProvinceNode Parent;
			internal StateProvinceIn(StateProvinceNode parent)
			{
				Parent = parent;
			}
			public IFromIn_STATEPROVINCE_HAS_COUNTRYREGION_REL STATEPROVINCE_HAS_COUNTRYREGION { get { return new STATEPROVINCE_HAS_COUNTRYREGION_REL(Parent, DirectionEnum.In); } }
			public IFromIn_STATEPROVINCE_HAS_SALESTERRITORY_REL STATEPROVINCE_HAS_SALESTERRITORY { get { return new STATEPROVINCE_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.In); } }

		}

		public StateProvinceOut Out { get { return new StateProvinceOut(this); } }
		public class StateProvinceOut
		{
			private StateProvinceNode Parent;
			internal StateProvinceOut(StateProvinceNode parent)
			{
				Parent = parent;
			}
			public IFromOut_ADDRESS_HAS_STATEPROVINCE_REL ADDRESS_HAS_STATEPROVINCE { get { return new ADDRESS_HAS_STATEPROVINCE_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESTAXRATE_HAS_STATEPROVINCE_REL SALESTAXRATE_HAS_STATEPROVINCE { get { return new SALESTAXRATE_HAS_STATEPROVINCE_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class StateProvinceAlias : AliasResult<StateProvinceAlias, StateProvinceListAlias>
	{
		internal StateProvinceAlias(StateProvinceNode parent)
		{
			Node = parent;
		}
		internal StateProvinceAlias(StateProvinceNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  StateProvinceAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  StateProvinceAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  StateProvinceAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> CountryRegionCode = default, JsNotation<bool> IsOnlyStateProvinceFlag = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> StateProvinceCode = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (CountryRegionCode.HasValue) assignments.Add(new Assignment(this.CountryRegionCode, CountryRegionCode));
			if (IsOnlyStateProvinceFlag.HasValue) assignments.Add(new Assignment(this.IsOnlyStateProvinceFlag, IsOnlyStateProvinceFlag));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (StateProvinceCode.HasValue) assignments.Add(new Assignment(this.StateProvinceCode, StateProvinceCode));
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
						{ "StateProvinceCode", new StringResult(this, "StateProvinceCode", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["StateProvinceCode"]) },
						{ "CountryRegionCode", new StringResult(this, "CountryRegionCode", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["CountryRegionCode"]) },
						{ "IsOnlyStateProvinceFlag", new BooleanResult(this, "IsOnlyStateProvinceFlag", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["IsOnlyStateProvinceFlag"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["Name"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["StateProvince"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["StateProvince"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public StateProvinceNode.StateProvinceIn In { get { return new StateProvinceNode.StateProvinceIn(new StateProvinceNode(this, true)); } }
		public StateProvinceNode.StateProvinceOut Out { get { return new StateProvinceNode.StateProvinceOut(new StateProvinceNode(this, true)); } }

		public StringResult StateProvinceCode
		{
			get
			{
				if (m_StateProvinceCode is null)
					m_StateProvinceCode = (StringResult)AliasFields["StateProvinceCode"];

				return m_StateProvinceCode;
			}
		}
		private StringResult m_StateProvinceCode = null;
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
		public BooleanResult IsOnlyStateProvinceFlag
		{
			get
			{
				if (m_IsOnlyStateProvinceFlag is null)
					m_IsOnlyStateProvinceFlag = (BooleanResult)AliasFields["IsOnlyStateProvinceFlag"];

				return m_IsOnlyStateProvinceFlag;
			}
		}
		private BooleanResult m_IsOnlyStateProvinceFlag = null;
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
		public AsResult As(string aliasName, out StateProvinceAlias alias)
		{
			alias = new StateProvinceAlias((StateProvinceNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class StateProvinceListAlias : ListResult<StateProvinceListAlias, StateProvinceAlias>, IAliasListResult
	{
		private StateProvinceListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private StateProvinceListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private StateProvinceListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class StateProvinceJaggedListAlias : ListResult<StateProvinceJaggedListAlias, StateProvinceListAlias>, IAliasJaggedListResult
	{
		private StateProvinceJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private StateProvinceJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private StateProvinceJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
