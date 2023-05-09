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
		public static ShipMethodNode ShipMethod { get { return new ShipMethodNode(); } }
	}

	public partial class ShipMethodNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ShipMethodNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ShipMethodNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ShipMethod";
		}

		protected override Entity GetEntity()
        {
			return m.ShipMethod.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ShipMethod.Entity.FunctionalId;
            }
        }

		internal ShipMethodNode() { }
		internal ShipMethodNode(ShipMethodAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ShipMethodNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ShipMethodNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ShipMethodNode Where(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> ShipBase = default, JsNotation<string> ShipRate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ShipMethodAlias> alias = new Lazy<ShipMethodAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (ShipBase.HasValue) conditions.Add(new QueryCondition(alias.Value.ShipBase, Operator.Equals, ((IValue)ShipBase).GetValue()));
            if (ShipRate.HasValue) conditions.Add(new QueryCondition(alias.Value.ShipRate, Operator.Equals, ((IValue)ShipRate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ShipMethodNode Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> ShipBase = default, JsNotation<string> ShipRate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ShipMethodAlias> alias = new Lazy<ShipMethodAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (ShipBase.HasValue) assignments.Add(new Assignment(alias.Value.ShipBase, ShipBase));
            if (ShipRate.HasValue) assignments.Add(new Assignment(alias.Value.ShipRate, ShipRate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ShipMethodNode Alias(out ShipMethodAlias alias)
        {
            if (NodeAlias is ShipMethodAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ShipMethodAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ShipMethodNode Alias(out ShipMethodAlias alias, string name)
        {
            if (NodeAlias is ShipMethodAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ShipMethodAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ShipMethodNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public ShipMethodOut Out { get { return new ShipMethodOut(this); } }
		public class ShipMethodOut
		{
			private ShipMethodNode Parent;
			internal ShipMethodOut(ShipMethodNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PURCHASEORDERHEADER_HAS_SHIPMETHOD_REL PURCHASEORDERHEADER_HAS_SHIPMETHOD { get { return new PURCHASEORDERHEADER_HAS_SHIPMETHOD_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESORDERHEADER_HAS_SHIPMETHOD_REL SALESORDERHEADER_HAS_SHIPMETHOD { get { return new SALESORDERHEADER_HAS_SHIPMETHOD_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class ShipMethodAlias : AliasResult<ShipMethodAlias, ShipMethodListAlias>
	{
		internal ShipMethodAlias(ShipMethodNode parent)
		{
			Node = parent;
		}
		internal ShipMethodAlias(ShipMethodNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ShipMethodAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ShipMethodAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ShipMethodAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> ShipBase = default, JsNotation<string> ShipRate = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (ShipBase.HasValue) assignments.Add(new Assignment(this.ShipBase, ShipBase));
			if (ShipRate.HasValue) assignments.Add(new Assignment(this.ShipRate, ShipRate));
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["Name"]) },
						{ "ShipBase", new StringResult(this, "ShipBase", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["ShipBase"]) },
						{ "ShipRate", new StringResult(this, "ShipRate", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["ShipRate"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ShipMethod"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ShipMethodNode.ShipMethodOut Out { get { return new ShipMethodNode.ShipMethodOut(new ShipMethodNode(this, true)); } }

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
		public StringResult ShipBase
		{
			get
			{
				if (m_ShipBase is null)
					m_ShipBase = (StringResult)AliasFields["ShipBase"];

				return m_ShipBase;
			}
		}
		private StringResult m_ShipBase = null;
		public StringResult ShipRate
		{
			get
			{
				if (m_ShipRate is null)
					m_ShipRate = (StringResult)AliasFields["ShipRate"];

				return m_ShipRate;
			}
		}
		private StringResult m_ShipRate = null;
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
		public AsResult As(string aliasName, out ShipMethodAlias alias)
		{
			alias = new ShipMethodAlias((ShipMethodNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ShipMethodListAlias : ListResult<ShipMethodListAlias, ShipMethodAlias>, IAliasListResult
	{
		private ShipMethodListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ShipMethodListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ShipMethodListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ShipMethodJaggedListAlias : ListResult<ShipMethodJaggedListAlias, ShipMethodListAlias>, IAliasJaggedListResult
	{
		private ShipMethodJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ShipMethodJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ShipMethodJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
