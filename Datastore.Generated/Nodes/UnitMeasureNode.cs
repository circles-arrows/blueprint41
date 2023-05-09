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
		public static UnitMeasureNode UnitMeasure { get { return new UnitMeasureNode(); } }
	}

	public partial class UnitMeasureNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(UnitMeasureNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(UnitMeasureNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "UnitMeasure";
		}

		protected override Entity GetEntity()
        {
			return m.UnitMeasure.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.UnitMeasure.Entity.FunctionalId;
            }
        }

		internal UnitMeasureNode() { }
		internal UnitMeasureNode(UnitMeasureAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal UnitMeasureNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal UnitMeasureNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public UnitMeasureNode Where(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default, JsNotation<string> UnitMeasureCorde = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<UnitMeasureAlias> alias = new Lazy<UnitMeasureAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));
            if (UnitMeasureCorde.HasValue) conditions.Add(new QueryCondition(alias.Value.UnitMeasureCorde, Operator.Equals, ((IValue)UnitMeasureCorde).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public UnitMeasureNode Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default, JsNotation<string> UnitMeasureCorde = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<UnitMeasureAlias> alias = new Lazy<UnitMeasureAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));
            if (UnitMeasureCorde.HasValue) assignments.Add(new Assignment(alias.Value.UnitMeasureCorde, UnitMeasureCorde));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public UnitMeasureNode Alias(out UnitMeasureAlias alias)
        {
            if (NodeAlias is UnitMeasureAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new UnitMeasureAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public UnitMeasureNode Alias(out UnitMeasureAlias alias, string name)
        {
            if (NodeAlias is UnitMeasureAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new UnitMeasureAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public UnitMeasureNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public UnitMeasureOut Out { get { return new UnitMeasureOut(this); } }
		public class UnitMeasureOut
		{
			private UnitMeasureNode Parent;
			internal UnitMeasureOut(UnitMeasureNode parent)
			{
				Parent = parent;
			}
			public IFromOut_BILLOFMATERIALS_HAS_UNITMEASURE_REL BILLOFMATERIALS_HAS_UNITMEASURE { get { return new BILLOFMATERIALS_HAS_UNITMEASURE_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCTVENDOR_HAS_UNITMEASURE_REL PRODUCTVENDOR_HAS_UNITMEASURE { get { return new PRODUCTVENDOR_HAS_UNITMEASURE_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class UnitMeasureAlias : AliasResult<UnitMeasureAlias, UnitMeasureListAlias>
	{
		internal UnitMeasureAlias(UnitMeasureNode parent)
		{
			Node = parent;
		}
		internal UnitMeasureAlias(UnitMeasureNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  UnitMeasureAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  UnitMeasureAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  UnitMeasureAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default, JsNotation<string> UnitMeasureCorde = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
			if (UnitMeasureCorde.HasValue) assignments.Add(new Assignment(this.UnitMeasureCorde, UnitMeasureCorde));
            
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
						{ "UnitMeasureCorde", new StringResult(this, "UnitMeasureCorde", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["UnitMeasure"].Properties["UnitMeasureCorde"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["UnitMeasure"].Properties["Name"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["UnitMeasure"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public UnitMeasureNode.UnitMeasureOut Out { get { return new UnitMeasureNode.UnitMeasureOut(new UnitMeasureNode(this, true)); } }

		public StringResult UnitMeasureCorde
		{
			get
			{
				if (m_UnitMeasureCorde is null)
					m_UnitMeasureCorde = (StringResult)AliasFields["UnitMeasureCorde"];

				return m_UnitMeasureCorde;
			}
		}
		private StringResult m_UnitMeasureCorde = null;
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
		public AsResult As(string aliasName, out UnitMeasureAlias alias)
		{
			alias = new UnitMeasureAlias((UnitMeasureNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class UnitMeasureListAlias : ListResult<UnitMeasureListAlias, UnitMeasureAlias>, IAliasListResult
	{
		private UnitMeasureListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private UnitMeasureListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private UnitMeasureListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class UnitMeasureJaggedListAlias : ListResult<UnitMeasureJaggedListAlias, UnitMeasureListAlias>, IAliasJaggedListResult
	{
		private UnitMeasureJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private UnitMeasureJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private UnitMeasureJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
