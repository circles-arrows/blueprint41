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
		public static ShoppingCartItemNode ShoppingCartItem { get { return new ShoppingCartItemNode(); } }
	}

	public partial class ShoppingCartItemNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ShoppingCartItemNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ShoppingCartItemNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ShoppingCartItem";
		}

		protected override Entity GetEntity()
        {
			return m.ShoppingCartItem.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ShoppingCartItem.Entity.FunctionalId;
            }
        }

		internal ShoppingCartItemNode() { }
		internal ShoppingCartItemNode(ShoppingCartItemAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ShoppingCartItemNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ShoppingCartItemNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ShoppingCartItemNode Where(JsNotation<System.DateTime> DateCreated = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ShoppingCartItemAlias> alias = new Lazy<ShoppingCartItemAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (DateCreated.HasValue) conditions.Add(new QueryCondition(alias.Value.DateCreated, Operator.Equals, ((IValue)DateCreated).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Quantity.HasValue) conditions.Add(new QueryCondition(alias.Value.Quantity, Operator.Equals, ((IValue)Quantity).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ShoppingCartItemNode Assign(JsNotation<System.DateTime> DateCreated = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ShoppingCartItemAlias> alias = new Lazy<ShoppingCartItemAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (DateCreated.HasValue) assignments.Add(new Assignment(alias.Value.DateCreated, DateCreated));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Quantity.HasValue) assignments.Add(new Assignment(alias.Value.Quantity, Quantity));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ShoppingCartItemNode Alias(out ShoppingCartItemAlias alias)
        {
            if (NodeAlias is ShoppingCartItemAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ShoppingCartItemAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ShoppingCartItemNode Alias(out ShoppingCartItemAlias alias, string name)
        {
            if (NodeAlias is ShoppingCartItemAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ShoppingCartItemAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ShoppingCartItemNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public ShoppingCartItemIn  In  { get { return new ShoppingCartItemIn(this); } }
		public class ShoppingCartItemIn
		{
			private ShoppingCartItemNode Parent;
			internal ShoppingCartItemIn(ShoppingCartItemNode parent)
			{
				Parent = parent;
			}
			public IFromIn_SHOPPINGCARTITEM_HAS_PRODUCT_REL SHOPPINGCARTITEM_HAS_PRODUCT { get { return new SHOPPINGCARTITEM_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class ShoppingCartItemAlias : AliasResult<ShoppingCartItemAlias, ShoppingCartItemListAlias>
	{
		internal ShoppingCartItemAlias(ShoppingCartItemNode parent)
		{
			Node = parent;
		}
		internal ShoppingCartItemAlias(ShoppingCartItemNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ShoppingCartItemAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ShoppingCartItemAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ShoppingCartItemAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> DateCreated = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (DateCreated.HasValue) assignments.Add(new Assignment(this.DateCreated, DateCreated));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Quantity.HasValue) assignments.Add(new Assignment(this.Quantity, Quantity));
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
						{ "Quantity", new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"].Properties["Quantity"]) },
						{ "DateCreated", new DateTimeResult(this, "DateCreated", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"].Properties["DateCreated"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ShoppingCartItemNode.ShoppingCartItemIn In { get { return new ShoppingCartItemNode.ShoppingCartItemIn(new ShoppingCartItemNode(this, true)); } }

		public NumericResult Quantity
		{
			get
			{
				if (m_Quantity is null)
					m_Quantity = (NumericResult)AliasFields["Quantity"];

				return m_Quantity;
			}
		}
		private NumericResult m_Quantity = null;
		public DateTimeResult DateCreated
		{
			get
			{
				if (m_DateCreated is null)
					m_DateCreated = (DateTimeResult)AliasFields["DateCreated"];

				return m_DateCreated;
			}
		}
		private DateTimeResult m_DateCreated = null;
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
		public AsResult As(string aliasName, out ShoppingCartItemAlias alias)
		{
			alias = new ShoppingCartItemAlias((ShoppingCartItemNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ShoppingCartItemListAlias : ListResult<ShoppingCartItemListAlias, ShoppingCartItemAlias>, IAliasListResult
	{
		private ShoppingCartItemListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ShoppingCartItemListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ShoppingCartItemListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ShoppingCartItemJaggedListAlias : ListResult<ShoppingCartItemJaggedListAlias, ShoppingCartItemListAlias>, IAliasJaggedListResult
	{
		private ShoppingCartItemJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ShoppingCartItemJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ShoppingCartItemJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
