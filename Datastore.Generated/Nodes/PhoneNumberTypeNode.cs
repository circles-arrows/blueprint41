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
		public static PhoneNumberTypeNode PhoneNumberType { get { return new PhoneNumberTypeNode(); } }
	}

	public partial class PhoneNumberTypeNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(PhoneNumberTypeNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(PhoneNumberTypeNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "PhoneNumberType";
		}

		protected override Entity GetEntity()
        {
			return m.PhoneNumberType.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.PhoneNumberType.Entity.FunctionalId;
            }
        }

		internal PhoneNumberTypeNode() { }
		internal PhoneNumberTypeNode(PhoneNumberTypeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal PhoneNumberTypeNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal PhoneNumberTypeNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public PhoneNumberTypeNode Where(JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PhoneNumberTypeAlias> alias = new Lazy<PhoneNumberTypeAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public PhoneNumberTypeNode Assign(JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<PhoneNumberTypeAlias> alias = new Lazy<PhoneNumberTypeAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public PhoneNumberTypeNode Alias(out PhoneNumberTypeAlias alias)
        {
            if (NodeAlias is PhoneNumberTypeAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new PhoneNumberTypeAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public PhoneNumberTypeNode Alias(out PhoneNumberTypeAlias alias, string name)
        {
            if (NodeAlias is PhoneNumberTypeAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new PhoneNumberTypeAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public PhoneNumberTypeNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public PhoneNumberTypeOut Out { get { return new PhoneNumberTypeOut(this); } }
		public class PhoneNumberTypeOut
		{
			private PhoneNumberTypeNode Parent;
			internal PhoneNumberTypeOut(PhoneNumberTypeNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PERSON_HAS_PHONENUMBERTYPE_REL PERSON_HAS_PHONENUMBERTYPE { get { return new PERSON_HAS_PHONENUMBERTYPE_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class PhoneNumberTypeAlias : AliasResult<PhoneNumberTypeAlias, PhoneNumberTypeListAlias>
	{
		internal PhoneNumberTypeAlias(PhoneNumberTypeNode parent)
		{
			Node = parent;
		}
		internal PhoneNumberTypeAlias(PhoneNumberTypeNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  PhoneNumberTypeAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  PhoneNumberTypeAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  PhoneNumberTypeAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Name = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["PhoneNumberType"], Datastore.AdventureWorks.Model.Entities["PhoneNumberType"].Properties["Name"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["PhoneNumberType"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public PhoneNumberTypeNode.PhoneNumberTypeOut Out { get { return new PhoneNumberTypeNode.PhoneNumberTypeOut(new PhoneNumberTypeNode(this, true)); } }

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
		public AsResult As(string aliasName, out PhoneNumberTypeAlias alias)
		{
			alias = new PhoneNumberTypeAlias((PhoneNumberTypeNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class PhoneNumberTypeListAlias : ListResult<PhoneNumberTypeListAlias, PhoneNumberTypeAlias>, IAliasListResult
	{
		private PhoneNumberTypeListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PhoneNumberTypeListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PhoneNumberTypeListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class PhoneNumberTypeJaggedListAlias : ListResult<PhoneNumberTypeJaggedListAlias, PhoneNumberTypeListAlias>, IAliasJaggedListResult
	{
		private PhoneNumberTypeJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private PhoneNumberTypeJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private PhoneNumberTypeJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
