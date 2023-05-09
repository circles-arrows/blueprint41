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
		public static ProductCostHistoryNode ProductCostHistory { get { return new ProductCostHistoryNode(); } }
	}

	public partial class ProductCostHistoryNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ProductCostHistoryNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ProductCostHistoryNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ProductCostHistory";
		}

		protected override Entity GetEntity()
        {
			return m.ProductCostHistory.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ProductCostHistory.Entity.FunctionalId;
            }
        }

		internal ProductCostHistoryNode() { }
		internal ProductCostHistoryNode(ProductCostHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductCostHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ProductCostHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ProductCostHistoryNode Where(JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> StandardCost = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductCostHistoryAlias> alias = new Lazy<ProductCostHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (EndDate.HasValue) conditions.Add(new QueryCondition(alias.Value.EndDate, Operator.Equals, ((IValue)EndDate).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (StandardCost.HasValue) conditions.Add(new QueryCondition(alias.Value.StandardCost, Operator.Equals, ((IValue)StandardCost).GetValue()));
            if (StartDate.HasValue) conditions.Add(new QueryCondition(alias.Value.StartDate, Operator.Equals, ((IValue)StartDate).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ProductCostHistoryNode Assign(JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> StandardCost = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductCostHistoryAlias> alias = new Lazy<ProductCostHistoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (EndDate.HasValue) assignments.Add(new Assignment(alias.Value.EndDate, EndDate));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (StandardCost.HasValue) assignments.Add(new Assignment(alias.Value.StandardCost, StandardCost));
            if (StartDate.HasValue) assignments.Add(new Assignment(alias.Value.StartDate, StartDate));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ProductCostHistoryNode Alias(out ProductCostHistoryAlias alias)
        {
            if (NodeAlias is ProductCostHistoryAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ProductCostHistoryAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ProductCostHistoryNode Alias(out ProductCostHistoryAlias alias, string name)
        {
            if (NodeAlias is ProductCostHistoryAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ProductCostHistoryAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ProductCostHistoryNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public ProductCostHistoryIn  In  { get { return new ProductCostHistoryIn(this); } }
		public class ProductCostHistoryIn
		{
			private ProductCostHistoryNode Parent;
			internal ProductCostHistoryIn(ProductCostHistoryNode parent)
			{
				Parent = parent;
			}
			public IFromIn_PRODUCTCOSTHISTORY_HAS_PRODUCT_REL PRODUCTCOSTHISTORY_HAS_PRODUCT { get { return new PRODUCTCOSTHISTORY_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class ProductCostHistoryAlias : AliasResult<ProductCostHistoryAlias, ProductCostHistoryListAlias>
	{
		internal ProductCostHistoryAlias(ProductCostHistoryNode parent)
		{
			Node = parent;
		}
		internal ProductCostHistoryAlias(ProductCostHistoryNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ProductCostHistoryAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ProductCostHistoryAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ProductCostHistoryAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> EndDate = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> StandardCost = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (EndDate.HasValue) assignments.Add(new Assignment(this.EndDate, EndDate));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (StandardCost.HasValue) assignments.Add(new Assignment(this.StandardCost, StandardCost));
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
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["EndDate"]) },
						{ "StandardCost", new StringResult(this, "StandardCost", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["StandardCost"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductCostHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ProductCostHistoryNode.ProductCostHistoryIn In { get { return new ProductCostHistoryNode.ProductCostHistoryIn(new ProductCostHistoryNode(this, true)); } }

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
		public StringResult StandardCost
		{
			get
			{
				if (m_StandardCost is null)
					m_StandardCost = (StringResult)AliasFields["StandardCost"];

				return m_StandardCost;
			}
		}
		private StringResult m_StandardCost = null;
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
		public AsResult As(string aliasName, out ProductCostHistoryAlias alias)
		{
			alias = new ProductCostHistoryAlias((ProductCostHistoryNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ProductCostHistoryListAlias : ListResult<ProductCostHistoryListAlias, ProductCostHistoryAlias>, IAliasListResult
	{
		private ProductCostHistoryListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductCostHistoryListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductCostHistoryListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ProductCostHistoryJaggedListAlias : ListResult<ProductCostHistoryJaggedListAlias, ProductCostHistoryListAlias>, IAliasJaggedListResult
	{
		private ProductCostHistoryJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductCostHistoryJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductCostHistoryJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
