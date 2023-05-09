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
		public static LocationNode Location { get { return new LocationNode(); } }
	}

	public partial class LocationNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(LocationNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(LocationNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Location";
		}

		protected override Entity GetEntity()
        {
			return m.Location.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Location.Entity.FunctionalId;
            }
        }

		internal LocationNode() { }
		internal LocationNode(LocationAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal LocationNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal LocationNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public LocationNode Where(JsNotation<string> Availability = default, JsNotation<string> CostRate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<LocationAlias> alias = new Lazy<LocationAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Availability.HasValue) conditions.Add(new QueryCondition(alias.Value.Availability, Operator.Equals, ((IValue)Availability).GetValue()));
            if (CostRate.HasValue) conditions.Add(new QueryCondition(alias.Value.CostRate, Operator.Equals, ((IValue)CostRate).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public LocationNode Assign(JsNotation<string> Availability = default, JsNotation<string> CostRate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<LocationAlias> alias = new Lazy<LocationAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Availability.HasValue) assignments.Add(new Assignment(alias.Value.Availability, Availability));
            if (CostRate.HasValue) assignments.Add(new Assignment(alias.Value.CostRate, CostRate));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public LocationNode Alias(out LocationAlias alias)
        {
            if (NodeAlias is LocationAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new LocationAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public LocationNode Alias(out LocationAlias alias, string name)
        {
            if (NodeAlias is LocationAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new LocationAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public LocationNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public LocationOut Out { get { return new LocationOut(this); } }
		public class LocationOut
		{
			private LocationNode Parent;
			internal LocationOut(LocationNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL PRODUCTINVENTORY_HAS_LOCATION { get { return new PRODUCTINVENTORY_HAS_LOCATION_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_WORKORDERROUTING_HAS_LOCATION_REL WORKORDERROUTING_HAS_LOCATION { get { return new WORKORDERROUTING_HAS_LOCATION_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class LocationAlias : AliasResult<LocationAlias, LocationListAlias>
	{
		internal LocationAlias(LocationNode parent)
		{
			Node = parent;
		}
		internal LocationAlias(LocationNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  LocationAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  LocationAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  LocationAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Availability = default, JsNotation<string> CostRate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Availability.HasValue) assignments.Add(new Assignment(this.Availability, Availability));
			if (CostRate.HasValue) assignments.Add(new Assignment(this.CostRate, CostRate));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Name.HasValue) assignments.Add(new Assignment(this.Name, Name));
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Location"].Properties["Name"]) },
						{ "CostRate", new StringResult(this, "CostRate", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Location"].Properties["CostRate"]) },
						{ "Availability", new StringResult(this, "Availability", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Location"].Properties["Availability"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public LocationNode.LocationOut Out { get { return new LocationNode.LocationOut(new LocationNode(this, true)); } }

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
		public StringResult CostRate
		{
			get
			{
				if (m_CostRate is null)
					m_CostRate = (StringResult)AliasFields["CostRate"];

				return m_CostRate;
			}
		}
		private StringResult m_CostRate = null;
		public StringResult Availability
		{
			get
			{
				if (m_Availability is null)
					m_Availability = (StringResult)AliasFields["Availability"];

				return m_Availability;
			}
		}
		private StringResult m_Availability = null;
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
		public AsResult As(string aliasName, out LocationAlias alias)
		{
			alias = new LocationAlias((LocationNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class LocationListAlias : ListResult<LocationListAlias, LocationAlias>, IAliasListResult
	{
		private LocationListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private LocationListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private LocationListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class LocationJaggedListAlias : ListResult<LocationJaggedListAlias, LocationListAlias>, IAliasJaggedListResult
	{
		private LocationJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private LocationJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private LocationJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
