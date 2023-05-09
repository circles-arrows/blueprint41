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
		public static ProductListPriceHistoryNode ProductListPriceHistory { get { return new ProductListPriceHistoryNode(); } }
	}

	public partial class ProductListPriceHistoryNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ProductListPriceHistoryNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ProductListPriceHistoryNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ProductListPriceHistory";
		}

		protected override Entity GetEntity()
        {
			return m.ProductListPriceHistory.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ProductListPriceHistory.Entity.FunctionalId;
            }
        }

		internal ProductListPriceHistoryNode() { }
		internal ProductListPriceHistoryNode(ProductListPriceHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductListPriceHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ProductListPriceHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ProductListPriceHistoryNode Where(JsNotation<System.DateTime> EndDate = default, JsNotation<string> ListPrice = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductListPriceHistoryAlias> alias = new Lazy<ProductListPriceHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (EndDate.HasValue) conditions.Add(new QueryCondition(alias.Value.EndDate, Operator.Equals, ((IValue)EndDate).GetValue()));
            if (ListPrice.HasValue) conditions.Add(new QueryCondition(alias.Value.ListPrice, Operator.Equals, ((IValue)ListPrice).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (StartDate.HasValue) conditions.Add(new QueryCondition(alias.Value.StartDate, Operator.Equals, ((IValue)StartDate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ProductListPriceHistoryNode Assign(JsNotation<System.DateTime> EndDate = default, JsNotation<string> ListPrice = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductListPriceHistoryAlias> alias = new Lazy<ProductListPriceHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (EndDate.HasValue) assignments.Add(new Assignment(alias.Value.EndDate, EndDate));
            if (ListPrice.HasValue) assignments.Add(new Assignment(alias.Value.ListPrice, ListPrice));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (StartDate.HasValue) assignments.Add(new Assignment(alias.Value.StartDate, StartDate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ProductListPriceHistoryNode Alias(out ProductListPriceHistoryAlias alias)
        {
            if (NodeAlias is ProductListPriceHistoryAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ProductListPriceHistoryAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ProductListPriceHistoryNode Alias(out ProductListPriceHistoryAlias alias, string name)
        {
            if (NodeAlias is ProductListPriceHistoryAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ProductListPriceHistoryAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ProductListPriceHistoryNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public ProductListPriceHistoryIn  In  { get { return new ProductListPriceHistoryIn(this); } }
		public class ProductListPriceHistoryIn
		{
			private ProductListPriceHistoryNode Parent;
			internal ProductListPriceHistoryIn(ProductListPriceHistoryNode parent)
			{
				Parent = parent;
			}
			public IFromIn_PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT { get { return new PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class ProductListPriceHistoryAlias : AliasResult<ProductListPriceHistoryAlias, ProductListPriceHistoryListAlias>
	{
		internal ProductListPriceHistoryAlias(ProductListPriceHistoryNode parent)
		{
			Node = parent;
		}
		internal ProductListPriceHistoryAlias(ProductListPriceHistoryNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ProductListPriceHistoryAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ProductListPriceHistoryAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ProductListPriceHistoryAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> EndDate = default, JsNotation<string> ListPrice = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (EndDate.HasValue) assignments.Add(new Assignment(this.EndDate, EndDate));
			if (ListPrice.HasValue) assignments.Add(new Assignment(this.ListPrice, ListPrice));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (StartDate.HasValue) assignments.Add(new Assignment(this.StartDate, StartDate));
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
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"].Properties["EndDate"]) },
						{ "ListPrice", new StringResult(this, "ListPrice", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"].Properties["ListPrice"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ProductListPriceHistoryNode.ProductListPriceHistoryIn In { get { return new ProductListPriceHistoryNode.ProductListPriceHistoryIn(new ProductListPriceHistoryNode(this, true)); } }

		public DateTimeResult StartDate
		{
			get
			{
				if (m_StartDate is null)
					m_StartDate = (DateTimeResult)AliasFields["StartDate"];

				return m_StartDate;
			}
		}
		private DateTimeResult m_StartDate = null;
		public DateTimeResult EndDate
		{
			get
			{
				if (m_EndDate is null)
					m_EndDate = (DateTimeResult)AliasFields["EndDate"];

				return m_EndDate;
			}
		}
		private DateTimeResult m_EndDate = null;
		public StringResult ListPrice
		{
			get
			{
				if (m_ListPrice is null)
					m_ListPrice = (StringResult)AliasFields["ListPrice"];

				return m_ListPrice;
			}
		}
		private StringResult m_ListPrice = null;
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
		public AsResult As(string aliasName, out ProductListPriceHistoryAlias alias)
		{
			alias = new ProductListPriceHistoryAlias((ProductListPriceHistoryNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ProductListPriceHistoryListAlias : ListResult<ProductListPriceHistoryListAlias, ProductListPriceHistoryAlias>, IAliasListResult
	{
		private ProductListPriceHistoryListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductListPriceHistoryListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductListPriceHistoryListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ProductListPriceHistoryJaggedListAlias : ListResult<ProductListPriceHistoryJaggedListAlias, ProductListPriceHistoryListAlias>, IAliasJaggedListResult
	{
		private ProductListPriceHistoryJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductListPriceHistoryJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductListPriceHistoryJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
