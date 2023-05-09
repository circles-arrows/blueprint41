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
		public static AddressTypeNode AddressType { get { return new AddressTypeNode(); } }
	}

	public partial class AddressTypeNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(AddressTypeNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(AddressTypeNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "AddressType";
		}

		protected override Entity GetEntity()
        {
			return m.AddressType.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.AddressType.Entity.FunctionalId;
            }
        }

		internal AddressTypeNode() { }
		internal AddressTypeNode(AddressTypeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal AddressTypeNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal AddressTypeNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public AddressTypeNode Where(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<AddressTypeAlias> alias = new Lazy<AddressTypeAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public AddressTypeNode Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<AddressTypeAlias> alias = new Lazy<AddressTypeAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public AddressTypeNode Alias(out AddressTypeAlias alias)
        {
            if (NodeAlias is AddressTypeAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new AddressTypeAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public AddressTypeNode Alias(out AddressTypeAlias alias, string name)
        {
            if (NodeAlias is AddressTypeAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new AddressTypeAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public AddressTypeNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public AddressTypeOut Out { get { return new AddressTypeOut(this); } }
		public class AddressTypeOut
		{
			private AddressTypeNode Parent;
			internal AddressTypeOut(AddressTypeNode parent)
			{
				Parent = parent;
			}
			public IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL ADDRESS_HAS_ADDRESSTYPE { get { return new ADDRESS_HAS_ADDRESSTYPE_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class AddressTypeAlias : AliasResult<AddressTypeAlias, AddressTypeListAlias>
	{
		internal AddressTypeAlias(AddressTypeNode parent)
		{
			Node = parent;
		}
		internal AddressTypeAlias(AddressTypeNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  AddressTypeAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  AddressTypeAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  AddressTypeAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["AddressType"], Datastore.AdventureWorks.Model.Entities["AddressType"].Properties["Name"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["AddressType"], Datastore.AdventureWorks.Model.Entities["AddressType"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["AddressType"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["AddressType"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public AddressTypeNode.AddressTypeOut Out { get { return new AddressTypeNode.AddressTypeOut(new AddressTypeNode(this, true)); } }

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
		public AsResult As(string aliasName, out AddressTypeAlias alias)
		{
			alias = new AddressTypeAlias((AddressTypeNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class AddressTypeListAlias : ListResult<AddressTypeListAlias, AddressTypeAlias>, IAliasListResult
	{
		private AddressTypeListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private AddressTypeListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private AddressTypeListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class AddressTypeJaggedListAlias : ListResult<AddressTypeJaggedListAlias, AddressTypeListAlias>, IAliasJaggedListResult
	{
		private AddressTypeJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private AddressTypeJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private AddressTypeJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
