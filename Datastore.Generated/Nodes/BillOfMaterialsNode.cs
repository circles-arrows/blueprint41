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
		public static BillOfMaterialsNode BillOfMaterials { get { return new BillOfMaterialsNode(); } }
	}

	public partial class BillOfMaterialsNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(BillOfMaterialsNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(BillOfMaterialsNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "BillOfMaterials";
		}

		protected override Entity GetEntity()
        {
			return m.BillOfMaterials.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.BillOfMaterials.Entity.FunctionalId;
            }
        }

		internal BillOfMaterialsNode() { }
		internal BillOfMaterialsNode(BillOfMaterialsAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal BillOfMaterialsNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal BillOfMaterialsNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public BillOfMaterialsNode Where(JsNotation<string> BOMLevel = default, JsNotation<System.DateTime?> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> PerAssemblyQty = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default, JsNotation<string> UnitMeasureCode = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<BillOfMaterialsAlias> alias = new Lazy<BillOfMaterialsAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (BOMLevel.HasValue) conditions.Add(new QueryCondition(alias.Value.BOMLevel, Operator.Equals, ((IValue)BOMLevel).GetValue()));
            if (EndDate.HasValue) conditions.Add(new QueryCondition(alias.Value.EndDate, Operator.Equals, ((IValue)EndDate).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (PerAssemblyQty.HasValue) conditions.Add(new QueryCondition(alias.Value.PerAssemblyQty, Operator.Equals, ((IValue)PerAssemblyQty).GetValue()));
            if (StartDate.HasValue) conditions.Add(new QueryCondition(alias.Value.StartDate, Operator.Equals, ((IValue)StartDate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));
            if (UnitMeasureCode.HasValue) conditions.Add(new QueryCondition(alias.Value.UnitMeasureCode, Operator.Equals, ((IValue)UnitMeasureCode).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public BillOfMaterialsNode Assign(JsNotation<string> BOMLevel = default, JsNotation<System.DateTime?> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> PerAssemblyQty = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default, JsNotation<string> UnitMeasureCode = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<BillOfMaterialsAlias> alias = new Lazy<BillOfMaterialsAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (BOMLevel.HasValue) assignments.Add(new Assignment(alias.Value.BOMLevel, BOMLevel));
            if (EndDate.HasValue) assignments.Add(new Assignment(alias.Value.EndDate, EndDate));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (PerAssemblyQty.HasValue) assignments.Add(new Assignment(alias.Value.PerAssemblyQty, PerAssemblyQty));
            if (StartDate.HasValue) assignments.Add(new Assignment(alias.Value.StartDate, StartDate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));
            if (UnitMeasureCode.HasValue) assignments.Add(new Assignment(alias.Value.UnitMeasureCode, UnitMeasureCode));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public BillOfMaterialsNode Alias(out BillOfMaterialsAlias alias)
        {
            if (NodeAlias is BillOfMaterialsAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new BillOfMaterialsAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public BillOfMaterialsNode Alias(out BillOfMaterialsAlias alias, string name)
        {
            if (NodeAlias is BillOfMaterialsAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new BillOfMaterialsAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public BillOfMaterialsNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public BillOfMaterialsIn  In  { get { return new BillOfMaterialsIn(this); } }
		public class BillOfMaterialsIn
		{
			private BillOfMaterialsNode Parent;
			internal BillOfMaterialsIn(BillOfMaterialsNode parent)
			{
				Parent = parent;
			}
			public IFromIn_BILLOFMATERIALS_HAS_PRODUCT_REL BILLOFMATERIALS_HAS_PRODUCT { get { return new BILLOFMATERIALS_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_BILLOFMATERIALS_HAS_UNITMEASURE_REL BILLOFMATERIALS_HAS_UNITMEASURE { get { return new BILLOFMATERIALS_HAS_UNITMEASURE_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class BillOfMaterialsAlias : AliasResult<BillOfMaterialsAlias, BillOfMaterialsListAlias>
	{
		internal BillOfMaterialsAlias(BillOfMaterialsNode parent)
		{
			Node = parent;
		}
		internal BillOfMaterialsAlias(BillOfMaterialsNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  BillOfMaterialsAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  BillOfMaterialsAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  BillOfMaterialsAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> BOMLevel = default, JsNotation<System.DateTime?> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> PerAssemblyQty = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default, JsNotation<string> UnitMeasureCode = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (BOMLevel.HasValue) assignments.Add(new Assignment(this.BOMLevel, BOMLevel));
			if (EndDate.HasValue) assignments.Add(new Assignment(this.EndDate, EndDate));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (PerAssemblyQty.HasValue) assignments.Add(new Assignment(this.PerAssemblyQty, PerAssemblyQty));
			if (StartDate.HasValue) assignments.Add(new Assignment(this.StartDate, StartDate));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
			if (UnitMeasureCode.HasValue) assignments.Add(new Assignment(this.UnitMeasureCode, UnitMeasureCode));
            
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
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["EndDate"]) },
						{ "UnitMeasureCode", new StringResult(this, "UnitMeasureCode", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["UnitMeasureCode"]) },
						{ "BOMLevel", new StringResult(this, "BOMLevel", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["BOMLevel"]) },
						{ "PerAssemblyQty", new NumericResult(this, "PerAssemblyQty", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["PerAssemblyQty"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["BillOfMaterials"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public BillOfMaterialsNode.BillOfMaterialsIn In { get { return new BillOfMaterialsNode.BillOfMaterialsIn(new BillOfMaterialsNode(this, true)); } }

		public DateTimeResult StartDate
		{
			get
			{
				if (m_StartDate is null)
					m_StartDate = (DateTimeResult)AliasFields["StartDate"];

				return m_StartDate;
			}
		}
		private DateTimeResult m_StartDate = null;
		public DateTimeResult EndDate
		{
			get
			{
				if (m_EndDate is null)
					m_EndDate = (DateTimeResult)AliasFields["EndDate"];

				return m_EndDate;
			}
		}
		private DateTimeResult m_EndDate = null;
		public StringResult UnitMeasureCode
		{
			get
			{
				if (m_UnitMeasureCode is null)
					m_UnitMeasureCode = (StringResult)AliasFields["UnitMeasureCode"];

				return m_UnitMeasureCode;
			}
		}
		private StringResult m_UnitMeasureCode = null;
		public StringResult BOMLevel
		{
			get
			{
				if (m_BOMLevel is null)
					m_BOMLevel = (StringResult)AliasFields["BOMLevel"];

				return m_BOMLevel;
			}
		}
		private StringResult m_BOMLevel = null;
		public NumericResult PerAssemblyQty
		{
			get
			{
				if (m_PerAssemblyQty is null)
					m_PerAssemblyQty = (NumericResult)AliasFields["PerAssemblyQty"];

				return m_PerAssemblyQty;
			}
		}
		private NumericResult m_PerAssemblyQty = null;
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
		public AsResult As(string aliasName, out BillOfMaterialsAlias alias)
		{
			alias = new BillOfMaterialsAlias((BillOfMaterialsNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class BillOfMaterialsListAlias : ListResult<BillOfMaterialsListAlias, BillOfMaterialsAlias>, IAliasListResult
	{
		private BillOfMaterialsListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private BillOfMaterialsListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private BillOfMaterialsListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class BillOfMaterialsJaggedListAlias : ListResult<BillOfMaterialsJaggedListAlias, BillOfMaterialsListAlias>, IAliasJaggedListResult
	{
		private BillOfMaterialsJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private BillOfMaterialsJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private BillOfMaterialsJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
