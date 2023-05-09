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
		public static ShiftNode Shift { get { return new ShiftNode(); } }
	}

	public partial class ShiftNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ShiftNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ShiftNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Shift";
		}

		protected override Entity GetEntity()
        {
			return m.Shift.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Shift.Entity.FunctionalId;
            }
        }

		internal ShiftNode() { }
		internal ShiftNode(ShiftAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ShiftNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ShiftNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ShiftNode Where(JsNotation<System.DateTime> EndTime = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<System.DateTime> StartTime = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ShiftAlias> alias = new Lazy<ShiftAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (EndTime.HasValue) conditions.Add(new QueryCondition(alias.Value.EndTime, Operator.Equals, ((IValue)EndTime).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (StartTime.HasValue) conditions.Add(new QueryCondition(alias.Value.StartTime, Operator.Equals, ((IValue)StartTime).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ShiftNode Assign(JsNotation<System.DateTime> EndTime = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<System.DateTime> StartTime = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ShiftAlias> alias = new Lazy<ShiftAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (EndTime.HasValue) assignments.Add(new Assignment(alias.Value.EndTime, EndTime));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (StartTime.HasValue) assignments.Add(new Assignment(alias.Value.StartTime, StartTime));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ShiftNode Alias(out ShiftAlias alias)
        {
            if (NodeAlias is ShiftAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ShiftAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ShiftNode Alias(out ShiftAlias alias, string name)
        {
            if (NodeAlias is ShiftAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ShiftAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ShiftNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public ShiftOut Out { get { return new ShiftOut(this); } }
		public class ShiftOut
		{
			private ShiftNode Parent;
			internal ShiftOut(ShiftNode parent)
			{
				Parent = parent;
			}
			public IFromOut_EMPLOYEE_HAS_SHIFT_REL EMPLOYEE_HAS_SHIFT { get { return new EMPLOYEE_HAS_SHIFT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT { get { return new EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class ShiftAlias : AliasResult<ShiftAlias, ShiftListAlias>
	{
		internal ShiftAlias(ShiftNode parent)
		{
			Node = parent;
		}
		internal ShiftAlias(ShiftNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ShiftAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ShiftAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ShiftAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> EndTime = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<System.DateTime> StartTime = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (EndTime.HasValue) assignments.Add(new Assignment(this.EndTime, EndTime));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
			if (StartTime.HasValue) assignments.Add(new Assignment(this.StartTime, StartTime));
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Shift"], Datastore.AdventureWorks.Model.Entities["Shift"].Properties["Name"]) },
						{ "StartTime", new DateTimeResult(this, "StartTime", Datastore.AdventureWorks.Model.Entities["Shift"], Datastore.AdventureWorks.Model.Entities["Shift"].Properties["StartTime"]) },
						{ "EndTime", new DateTimeResult(this, "EndTime", Datastore.AdventureWorks.Model.Entities["Shift"], Datastore.AdventureWorks.Model.Entities["Shift"].Properties["EndTime"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Shift"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Shift"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ShiftNode.ShiftOut Out { get { return new ShiftNode.ShiftOut(new ShiftNode(this, true)); } }

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
		public DateTimeResult StartTime
		{
			get
			{
				if (m_StartTime is null)
					m_StartTime = (DateTimeResult)AliasFields["StartTime"];

				return m_StartTime;
			}
		}
		private DateTimeResult m_StartTime = null;
		public DateTimeResult EndTime
		{
			get
			{
				if (m_EndTime is null)
					m_EndTime = (DateTimeResult)AliasFields["EndTime"];

				return m_EndTime;
			}
		}
		private DateTimeResult m_EndTime = null;
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
		public AsResult As(string aliasName, out ShiftAlias alias)
		{
			alias = new ShiftAlias((ShiftNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ShiftListAlias : ListResult<ShiftListAlias, ShiftAlias>, IAliasListResult
	{
		private ShiftListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ShiftListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ShiftListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ShiftJaggedListAlias : ListResult<ShiftJaggedListAlias, ShiftListAlias>, IAliasJaggedListResult
	{
		private ShiftJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ShiftJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ShiftJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
