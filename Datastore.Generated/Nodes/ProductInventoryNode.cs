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
		public static ProductInventoryNode ProductInventory { get { return new ProductInventoryNode(); } }
	}

	public partial class ProductInventoryNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ProductInventoryNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ProductInventoryNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ProductInventory";
		}

		protected override Entity GetEntity()
        {
			return m.ProductInventory.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ProductInventory.Entity.FunctionalId;
            }
        }

		internal ProductInventoryNode() { }
		internal ProductInventoryNode(ProductInventoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductInventoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ProductInventoryNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ProductInventoryNode Where(JsNotation<string> Bin = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<string> rowguid = default, JsNotation<string> Shelf = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductInventoryAlias> alias = new Lazy<ProductInventoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Bin.HasValue) conditions.Add(new QueryCondition(alias.Value.Bin, Operator.Equals, ((IValue)Bin).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Quantity.HasValue) conditions.Add(new QueryCondition(alias.Value.Quantity, Operator.Equals, ((IValue)Quantity).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (Shelf.HasValue) conditions.Add(new QueryCondition(alias.Value.Shelf, Operator.Equals, ((IValue)Shelf).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ProductInventoryNode Assign(JsNotation<string> Bin = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<string> rowguid = default, JsNotation<string> Shelf = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductInventoryAlias> alias = new Lazy<ProductInventoryAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Bin.HasValue) assignments.Add(new Assignment(alias.Value.Bin, Bin));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Quantity.HasValue) assignments.Add(new Assignment(alias.Value.Quantity, Quantity));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (Shelf.HasValue) assignments.Add(new Assignment(alias.Value.Shelf, Shelf));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ProductInventoryNode Alias(out ProductInventoryAlias alias)
        {
            if (NodeAlias is ProductInventoryAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ProductInventoryAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ProductInventoryNode Alias(out ProductInventoryAlias alias, string name)
        {
            if (NodeAlias is ProductInventoryAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ProductInventoryAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ProductInventoryNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public ProductInventoryIn  In  { get { return new ProductInventoryIn(this); } }
		public class ProductInventoryIn
		{
			private ProductInventoryNode Parent;
			internal ProductInventoryIn(ProductInventoryNode parent)
			{
				Parent = parent;
			}
			public IFromIn_PRODUCTINVENTORY_HAS_LOCATION_REL PRODUCTINVENTORY_HAS_LOCATION { get { return new PRODUCTINVENTORY_HAS_LOCATION_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTINVENTORY_HAS_PRODUCT_REL PRODUCTINVENTORY_HAS_PRODUCT { get { return new PRODUCTINVENTORY_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class ProductInventoryAlias : AliasResult<ProductInventoryAlias, ProductInventoryListAlias>
	{
		internal ProductInventoryAlias(ProductInventoryNode parent)
		{
			Node = parent;
		}
		internal ProductInventoryAlias(ProductInventoryNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ProductInventoryAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ProductInventoryAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ProductInventoryAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Bin = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<string> rowguid = default, JsNotation<string> Shelf = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Bin.HasValue) assignments.Add(new Assignment(this.Bin, Bin));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Quantity.HasValue) assignments.Add(new Assignment(this.Quantity, Quantity));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (Shelf.HasValue) assignments.Add(new Assignment(this.Shelf, Shelf));
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
						{ "Shelf", new StringResult(this, "Shelf", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Shelf"]) },
						{ "Bin", new StringResult(this, "Bin", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Bin"]) },
						{ "Quantity", new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["Quantity"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["ProductInventory"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductInventory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ProductInventoryNode.ProductInventoryIn In { get { return new ProductInventoryNode.ProductInventoryIn(new ProductInventoryNode(this, true)); } }

		public StringResult Shelf
		{
			get
			{
				if (m_Shelf is null)
					m_Shelf = (StringResult)AliasFields["Shelf"];

				return m_Shelf;
			}
		}
		private StringResult m_Shelf = null;
		public StringResult Bin
		{
			get
			{
				if (m_Bin is null)
					m_Bin = (StringResult)AliasFields["Bin"];

				return m_Bin;
			}
		}
		private StringResult m_Bin = null;
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
		public AsResult As(string aliasName, out ProductInventoryAlias alias)
		{
			alias = new ProductInventoryAlias((ProductInventoryNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ProductInventoryListAlias : ListResult<ProductInventoryListAlias, ProductInventoryAlias>, IAliasListResult
	{
		private ProductInventoryListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductInventoryListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductInventoryListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ProductInventoryJaggedListAlias : ListResult<ProductInventoryJaggedListAlias, ProductInventoryListAlias>, IAliasJaggedListResult
	{
		private ProductInventoryJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductInventoryJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductInventoryJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
