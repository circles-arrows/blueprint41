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
		public static AddressNode Address { get { return new AddressNode(); } }
	}

	public partial class AddressNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(AddressNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(AddressNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Address";
		}

		protected override Entity GetEntity()
        {
			return m.Address.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Address.Entity.FunctionalId;
            }
        }

		internal AddressNode() { }
		internal AddressNode(AddressAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal AddressNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal AddressNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public AddressNode Where(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> City = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> PostalCode = default, JsNotation<string> rowguid = default, JsNotation<string> SpatialLocation = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<AddressAlias> alias = new Lazy<AddressAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (AddressLine1.HasValue) conditions.Add(new QueryCondition(alias.Value.AddressLine1, Operator.Equals, ((IValue)AddressLine1).GetValue()));
            if (AddressLine2.HasValue) conditions.Add(new QueryCondition(alias.Value.AddressLine2, Operator.Equals, ((IValue)AddressLine2).GetValue()));
            if (City.HasValue) conditions.Add(new QueryCondition(alias.Value.City, Operator.Equals, ((IValue)City).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (PostalCode.HasValue) conditions.Add(new QueryCondition(alias.Value.PostalCode, Operator.Equals, ((IValue)PostalCode).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (SpatialLocation.HasValue) conditions.Add(new QueryCondition(alias.Value.SpatialLocation, Operator.Equals, ((IValue)SpatialLocation).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public AddressNode Assign(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> City = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> PostalCode = default, JsNotation<string> rowguid = default, JsNotation<string> SpatialLocation = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<AddressAlias> alias = new Lazy<AddressAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (AddressLine1.HasValue) assignments.Add(new Assignment(alias.Value.AddressLine1, AddressLine1));
            if (AddressLine2.HasValue) assignments.Add(new Assignment(alias.Value.AddressLine2, AddressLine2));
            if (City.HasValue) assignments.Add(new Assignment(alias.Value.City, City));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (PostalCode.HasValue) assignments.Add(new Assignment(alias.Value.PostalCode, PostalCode));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (SpatialLocation.HasValue) assignments.Add(new Assignment(alias.Value.SpatialLocation, SpatialLocation));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public AddressNode Alias(out AddressAlias alias)
        {
            if (NodeAlias is AddressAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new AddressAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public AddressNode Alias(out AddressAlias alias, string name)
        {
            if (NodeAlias is AddressAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new AddressAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public AddressNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public AddressIn  In  { get { return new AddressIn(this); } }
		public class AddressIn
		{
			private AddressNode Parent;
			internal AddressIn(AddressNode parent)
			{
				Parent = parent;
			}
			public IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL ADDRESS_HAS_ADDRESSTYPE { get { return new ADDRESS_HAS_ADDRESSTYPE_REL(Parent, DirectionEnum.In); } }
			public IFromIn_ADDRESS_HAS_STATEPROVINCE_REL ADDRESS_HAS_STATEPROVINCE { get { return new ADDRESS_HAS_STATEPROVINCE_REL(Parent, DirectionEnum.In); } }

		}

		public AddressOut Out { get { return new AddressOut(this); } }
		public class AddressOut
		{
			private AddressNode Parent;
			internal AddressOut(AddressNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PERSON_HAS_ADDRESS_REL PERSON_HAS_ADDRESS { get { return new PERSON_HAS_ADDRESS_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL SALESORDERHEADER_HAS_ADDRESS { get { return new SALESORDERHEADER_HAS_ADDRESS_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_STORE_HAS_ADDRESS_REL STORE_HAS_ADDRESS { get { return new STORE_HAS_ADDRESS_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class AddressAlias : AliasResult<AddressAlias, AddressListAlias>
	{
		internal AddressAlias(AddressNode parent)
		{
			Node = parent;
		}
		internal AddressAlias(AddressNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  AddressAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  AddressAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  AddressAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> AddressLine1 = default, JsNotation<string> AddressLine2 = default, JsNotation<string> City = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> PostalCode = default, JsNotation<string> rowguid = default, JsNotation<string> SpatialLocation = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (AddressLine1.HasValue) assignments.Add(new Assignment(this.AddressLine1, AddressLine1));
			if (AddressLine2.HasValue) assignments.Add(new Assignment(this.AddressLine2, AddressLine2));
			if (City.HasValue) assignments.Add(new Assignment(this.City, City));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (PostalCode.HasValue) assignments.Add(new Assignment(this.PostalCode, PostalCode));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (SpatialLocation.HasValue) assignments.Add(new Assignment(this.SpatialLocation, SpatialLocation));
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
						{ "AddressLine1", new StringResult(this, "AddressLine1", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["AddressLine1"]) },
						{ "AddressLine2", new StringResult(this, "AddressLine2", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["AddressLine2"]) },
						{ "City", new StringResult(this, "City", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["City"]) },
						{ "PostalCode", new StringResult(this, "PostalCode", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["PostalCode"]) },
						{ "SpatialLocation", new StringResult(this, "SpatialLocation", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["SpatialLocation"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public AddressNode.AddressIn In { get { return new AddressNode.AddressIn(new AddressNode(this, true)); } }
		public AddressNode.AddressOut Out { get { return new AddressNode.AddressOut(new AddressNode(this, true)); } }

		public StringResult AddressLine1
		{
			get
			{
				if (m_AddressLine1 is null)
					m_AddressLine1 = (StringResult)AliasFields["AddressLine1"];

				return m_AddressLine1;
			}
		}
		private StringResult m_AddressLine1 = null;
		public StringResult AddressLine2
		{
			get
			{
				if (m_AddressLine2 is null)
					m_AddressLine2 = (StringResult)AliasFields["AddressLine2"];

				return m_AddressLine2;
			}
		}
		private StringResult m_AddressLine2 = null;
		public StringResult City
		{
			get
			{
				if (m_City is null)
					m_City = (StringResult)AliasFields["City"];

				return m_City;
			}
		}
		private StringResult m_City = null;
		public StringResult PostalCode
		{
			get
			{
				if (m_PostalCode is null)
					m_PostalCode = (StringResult)AliasFields["PostalCode"];

				return m_PostalCode;
			}
		}
		private StringResult m_PostalCode = null;
		public StringResult SpatialLocation
		{
			get
			{
				if (m_SpatialLocation is null)
					m_SpatialLocation = (StringResult)AliasFields["SpatialLocation"];

				return m_SpatialLocation;
			}
		}
		private StringResult m_SpatialLocation = null;
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
		public AsResult As(string aliasName, out AddressAlias alias)
		{
			alias = new AddressAlias((AddressNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class AddressListAlias : ListResult<AddressListAlias, AddressAlias>, IAliasListResult
	{
		private AddressListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private AddressListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private AddressListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class AddressJaggedListAlias : ListResult<AddressJaggedListAlias, AddressListAlias>, IAliasJaggedListResult
	{
		private AddressJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private AddressJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private AddressJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
