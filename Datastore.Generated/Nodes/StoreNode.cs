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
		public static StoreNode Store { get { return new StoreNode(); } }
	}

	public partial class StoreNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(StoreNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(StoreNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Store";
		}

		protected override Entity GetEntity()
        {
			return m.Store.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Store.Entity.FunctionalId;
            }
        }

		internal StoreNode() { }
		internal StoreNode(StoreAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal StoreNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal StoreNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public StoreNode Where(JsNotation<string> Demographics = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<StoreAlias> alias = new Lazy<StoreAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Demographics.HasValue) conditions.Add(new QueryCondition(alias.Value.Demographics, Operator.Equals, ((IValue)Demographics).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public StoreNode Assign(JsNotation<string> Demographics = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<StoreAlias> alias = new Lazy<StoreAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Demographics.HasValue) assignments.Add(new Assignment(alias.Value.Demographics, Demographics));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public StoreNode Alias(out StoreAlias alias)
        {
            if (NodeAlias is StoreAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new StoreAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public StoreNode Alias(out StoreAlias alias, string name)
        {
            if (NodeAlias is StoreAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new StoreAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public StoreNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public StoreIn  In  { get { return new StoreIn(this); } }
		public class StoreIn
		{
			private StoreNode Parent;
			internal StoreIn(StoreNode parent)
			{
				Parent = parent;
			}
			public IFromIn_STORE_HAS_ADDRESS_REL STORE_HAS_ADDRESS { get { return new STORE_HAS_ADDRESS_REL(Parent, DirectionEnum.In); } }
			public IFromIn_STORE_VALID_FOR_SALESPERSON_REL STORE_VALID_FOR_SALESPERSON { get { return new STORE_VALID_FOR_SALESPERSON_REL(Parent, DirectionEnum.In); } }

		}

		public StoreOut Out { get { return new StoreOut(this); } }
		public class StoreOut
		{
			private StoreNode Parent;
			internal StoreOut(StoreNode parent)
			{
				Parent = parent;
			}
			public IFromOut_CUSTOMER_HAS_STORE_REL CUSTOMER_HAS_STORE { get { return new CUSTOMER_HAS_STORE_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class StoreAlias : AliasResult<StoreAlias, StoreListAlias>
	{
		internal StoreAlias(StoreNode parent)
		{
			Node = parent;
		}
		internal StoreAlias(StoreNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  StoreAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  StoreAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  StoreAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Demographics = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Demographics.HasValue) assignments.Add(new Assignment(this.Demographics, Demographics));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Store"].Properties["Name"]) },
						{ "Demographics", new StringResult(this, "Demographics", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Store"].Properties["Demographics"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Store"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public StoreNode.StoreIn In { get { return new StoreNode.StoreIn(new StoreNode(this, true)); } }
		public StoreNode.StoreOut Out { get { return new StoreNode.StoreOut(new StoreNode(this, true)); } }

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
		public StringResult Demographics
		{
			get
			{
				if (m_Demographics is null)
					m_Demographics = (StringResult)AliasFields["Demographics"];

				return m_Demographics;
			}
		}
		private StringResult m_Demographics = null;
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
		public AsResult As(string aliasName, out StoreAlias alias)
		{
			alias = new StoreAlias((StoreNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class StoreListAlias : ListResult<StoreListAlias, StoreAlias>, IAliasListResult
	{
		private StoreListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private StoreListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private StoreListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class StoreJaggedListAlias : ListResult<StoreJaggedListAlias, StoreListAlias>, IAliasJaggedListResult
	{
		private StoreJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private StoreJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private StoreJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
