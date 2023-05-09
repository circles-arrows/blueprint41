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
		public static CountryRegionNode CountryRegion { get { return new CountryRegionNode(); } }
	}

	public partial class CountryRegionNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(CountryRegionNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(CountryRegionNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "CountryRegion";
		}

		protected override Entity GetEntity()
        {
			return m.CountryRegion.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.CountryRegion.Entity.FunctionalId;
            }
        }

		internal CountryRegionNode() { }
		internal CountryRegionNode(CountryRegionAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CountryRegionNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal CountryRegionNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public CountryRegionNode Where(JsNotation<int> Code = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CountryRegionAlias> alias = new Lazy<CountryRegionAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Code.HasValue) conditions.Add(new QueryCondition(alias.Value.Code, Operator.Equals, ((IValue)Code).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public CountryRegionNode Assign(JsNotation<int> Code = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<CountryRegionAlias> alias = new Lazy<CountryRegionAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Code.HasValue) assignments.Add(new Assignment(alias.Value.Code, Code));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public CountryRegionNode Alias(out CountryRegionAlias alias)
        {
            if (NodeAlias is CountryRegionAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new CountryRegionAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public CountryRegionNode Alias(out CountryRegionAlias alias, string name)
        {
            if (NodeAlias is CountryRegionAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new CountryRegionAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public CountryRegionNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public CountryRegionOut Out { get { return new CountryRegionOut(this); } }
		public class CountryRegionOut
		{
			private CountryRegionNode Parent;
			internal CountryRegionOut(CountryRegionNode parent)
			{
				Parent = parent;
			}
			public IFromOut_STATEPROVINCE_HAS_COUNTRYREGION_REL STATEPROVINCE_HAS_COUNTRYREGION { get { return new STATEPROVINCE_HAS_COUNTRYREGION_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class CountryRegionAlias : AliasResult<CountryRegionAlias, CountryRegionListAlias>
	{
		internal CountryRegionAlias(CountryRegionNode parent)
		{
			Node = parent;
		}
		internal CountryRegionAlias(CountryRegionNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  CountryRegionAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  CountryRegionAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  CountryRegionAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<int> Code = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Code.HasValue) assignments.Add(new Assignment(this.Code, Code));
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
						{ "Code", new NumericResult(this, "Code", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["CountryRegion"].Properties["Code"]) },
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["CountryRegion"].Properties["Name"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["CountryRegion"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public CountryRegionNode.CountryRegionOut Out { get { return new CountryRegionNode.CountryRegionOut(new CountryRegionNode(this, true)); } }

		public NumericResult Code
		{
			get
			{
				if (m_Code is null)
					m_Code = (NumericResult)AliasFields["Code"];

				return m_Code;
			}
		}
		private NumericResult m_Code = null;
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
		public AsResult As(string aliasName, out CountryRegionAlias alias)
		{
			alias = new CountryRegionAlias((CountryRegionNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class CountryRegionListAlias : ListResult<CountryRegionListAlias, CountryRegionAlias>, IAliasListResult
	{
		private CountryRegionListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private CountryRegionListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private CountryRegionListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class CountryRegionJaggedListAlias : ListResult<CountryRegionJaggedListAlias, CountryRegionListAlias>, IAliasJaggedListResult
	{
		private CountryRegionJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private CountryRegionJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private CountryRegionJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
