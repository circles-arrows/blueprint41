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
		public static ProductProductPhotoNode ProductProductPhoto { get { return new ProductProductPhotoNode(); } }
	}

	public partial class ProductProductPhotoNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ProductProductPhotoNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ProductProductPhotoNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ProductProductPhoto";
		}

		protected override Entity GetEntity()
        {
			return m.ProductProductPhoto.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ProductProductPhoto.Entity.FunctionalId;
            }
        }

		internal ProductProductPhotoNode() { }
		internal ProductProductPhotoNode(ProductProductPhotoAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductProductPhotoNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ProductProductPhotoNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ProductProductPhotoNode Where(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Primary = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductProductPhotoAlias> alias = new Lazy<ProductProductPhotoAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Primary.HasValue) conditions.Add(new QueryCondition(alias.Value.Primary, Operator.Equals, ((IValue)Primary).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ProductProductPhotoNode Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Primary = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductProductPhotoAlias> alias = new Lazy<ProductProductPhotoAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Primary.HasValue) assignments.Add(new Assignment(alias.Value.Primary, Primary));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ProductProductPhotoNode Alias(out ProductProductPhotoAlias alias)
        {
            if (NodeAlias is ProductProductPhotoAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ProductProductPhotoAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ProductProductPhotoNode Alias(out ProductProductPhotoAlias alias, string name)
        {
            if (NodeAlias is ProductProductPhotoAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ProductProductPhotoAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ProductProductPhotoNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public ProductProductPhotoIn  In  { get { return new ProductProductPhotoIn(this); } }
		public class ProductProductPhotoIn
		{
			private ProductProductPhotoNode Parent;
			internal ProductProductPhotoIn(ProductProductPhotoNode parent)
			{
				Parent = parent;
			}
			public IFromIn_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO { get { return new PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL(Parent, DirectionEnum.In); } }

		}

		public ProductProductPhotoOut Out { get { return new ProductProductPhotoOut(this); } }
		public class ProductProductPhotoOut
		{
			private ProductProductPhotoNode Parent;
			internal ProductProductPhotoOut(ProductProductPhotoNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL PRODUCT_HAS_PRODUCTPRODUCTPHOTO { get { return new PRODUCT_HAS_PRODUCTPRODUCTPHOTO_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class ProductProductPhotoAlias : AliasResult<ProductProductPhotoAlias, ProductProductPhotoListAlias>
	{
		internal ProductProductPhotoAlias(ProductProductPhotoNode parent)
		{
			Node = parent;
		}
		internal ProductProductPhotoAlias(ProductProductPhotoNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ProductProductPhotoAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ProductProductPhotoAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ProductProductPhotoAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Primary = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Primary.HasValue) assignments.Add(new Assignment(this.Primary, Primary));
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
						{ "Primary", new StringResult(this, "Primary", Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"].Properties["Primary"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ProductProductPhotoNode.ProductProductPhotoIn In { get { return new ProductProductPhotoNode.ProductProductPhotoIn(new ProductProductPhotoNode(this, true)); } }
		public ProductProductPhotoNode.ProductProductPhotoOut Out { get { return new ProductProductPhotoNode.ProductProductPhotoOut(new ProductProductPhotoNode(this, true)); } }

		public StringResult Primary
		{
			get
			{
				if (m_Primary is null)
					m_Primary = (StringResult)AliasFields["Primary"];

				return m_Primary;
			}
		}
		private StringResult m_Primary = null;
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
		public AsResult As(string aliasName, out ProductProductPhotoAlias alias)
		{
			alias = new ProductProductPhotoAlias((ProductProductPhotoNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ProductProductPhotoListAlias : ListResult<ProductProductPhotoListAlias, ProductProductPhotoAlias>, IAliasListResult
	{
		private ProductProductPhotoListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductProductPhotoListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductProductPhotoListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ProductProductPhotoJaggedListAlias : ListResult<ProductProductPhotoJaggedListAlias, ProductProductPhotoListAlias>, IAliasJaggedListResult
	{
		private ProductProductPhotoJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductProductPhotoJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductProductPhotoJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
