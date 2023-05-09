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
		public static ProductModelNode ProductModel { get { return new ProductModelNode(); } }
	}

	public partial class ProductModelNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ProductModelNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ProductModelNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ProductModel";
		}

		protected override Entity GetEntity()
        {
			return m.ProductModel.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ProductModel.Entity.FunctionalId;
            }
        }

		internal ProductModelNode() { }
		internal ProductModelNode(ProductModelAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductModelNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ProductModelNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ProductModelNode Where(JsNotation<string> CatalogDescription = default, JsNotation<string> Instructions = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductModelAlias> alias = new Lazy<ProductModelAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (CatalogDescription.HasValue) conditions.Add(new QueryCondition(alias.Value.CatalogDescription, Operator.Equals, ((IValue)CatalogDescription).GetValue()));
            if (Instructions.HasValue) conditions.Add(new QueryCondition(alias.Value.Instructions, Operator.Equals, ((IValue)Instructions).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Name.HasValue) conditions.Add(new QueryCondition(alias.Value.Name, Operator.Equals, ((IValue)Name).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ProductModelNode Assign(JsNotation<string> CatalogDescription = default, JsNotation<string> Instructions = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductModelAlias> alias = new Lazy<ProductModelAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (CatalogDescription.HasValue) assignments.Add(new Assignment(alias.Value.CatalogDescription, CatalogDescription));
            if (Instructions.HasValue) assignments.Add(new Assignment(alias.Value.Instructions, Instructions));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Name.HasValue) assignments.Add(new Assignment(alias.Value.Name, Name));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ProductModelNode Alias(out ProductModelAlias alias)
        {
            if (NodeAlias is ProductModelAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ProductModelAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ProductModelNode Alias(out ProductModelAlias alias, string name)
        {
            if (NodeAlias is ProductModelAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ProductModelAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ProductModelNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public ProductModelIn  In  { get { return new ProductModelIn(this); } }
		public class ProductModelIn
		{
			private ProductModelNode Parent;
			internal ProductModelIn(ProductModelNode parent)
			{
				Parent = parent;
			}
			public IFromIn_PRODUCTMODEL_HAS_CULTURE_REL PRODUCTMODEL_HAS_CULTURE { get { return new PRODUCTMODEL_HAS_CULTURE_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTMODEL_HAS_ILLUSTRATION_REL PRODUCTMODEL_HAS_ILLUSTRATION { get { return new PRODUCTMODEL_HAS_ILLUSTRATION_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTMODEL_HAS_PRODUCTDESCRIPTION_REL PRODUCTMODEL_HAS_PRODUCTDESCRIPTION { get { return new PRODUCTMODEL_HAS_PRODUCTDESCRIPTION_REL(Parent, DirectionEnum.In); } }

		}

		public ProductModelOut Out { get { return new ProductModelOut(this); } }
		public class ProductModelOut
		{
			private ProductModelNode Parent;
			internal ProductModelOut(ProductModelNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PRODUCT_HAS_PRODUCTMODEL_REL PRODUCT_HAS_PRODUCTMODEL { get { return new PRODUCT_HAS_PRODUCTMODEL_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class ProductModelAlias : AliasResult<ProductModelAlias, ProductModelListAlias>
	{
		internal ProductModelAlias(ProductModelNode parent)
		{
			Node = parent;
		}
		internal ProductModelAlias(ProductModelNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ProductModelAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ProductModelAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ProductModelAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> CatalogDescription = default, JsNotation<string> Instructions = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Name = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (CatalogDescription.HasValue) assignments.Add(new Assignment(this.CatalogDescription, CatalogDescription));
			if (Instructions.HasValue) assignments.Add(new Assignment(this.Instructions, Instructions));
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["Name"]) },
						{ "CatalogDescription", new StringResult(this, "CatalogDescription", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["CatalogDescription"]) },
						{ "Instructions", new StringResult(this, "Instructions", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["Instructions"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductModel"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ProductModelNode.ProductModelIn In { get { return new ProductModelNode.ProductModelIn(new ProductModelNode(this, true)); } }
		public ProductModelNode.ProductModelOut Out { get { return new ProductModelNode.ProductModelOut(new ProductModelNode(this, true)); } }

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
		public StringResult CatalogDescription
		{
			get
			{
				if (m_CatalogDescription is null)
					m_CatalogDescription = (StringResult)AliasFields["CatalogDescription"];

				return m_CatalogDescription;
			}
		}
		private StringResult m_CatalogDescription = null;
		public StringResult Instructions
		{
			get
			{
				if (m_Instructions is null)
					m_Instructions = (StringResult)AliasFields["Instructions"];

				return m_Instructions;
			}
		}
		private StringResult m_Instructions = null;
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
		public AsResult As(string aliasName, out ProductModelAlias alias)
		{
			alias = new ProductModelAlias((ProductModelNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ProductModelListAlias : ListResult<ProductModelListAlias, ProductModelAlias>, IAliasListResult
	{
		private ProductModelListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductModelListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductModelListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ProductModelJaggedListAlias : ListResult<ProductModelJaggedListAlias, ProductModelListAlias>, IAliasJaggedListResult
	{
		private ProductModelJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductModelJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductModelJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
