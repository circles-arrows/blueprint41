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
		public static ProductDescriptionNode ProductDescription { get { return new ProductDescriptionNode(); } }
	}

	public partial class ProductDescriptionNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ProductDescriptionNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ProductDescriptionNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ProductDescription";
		}

		protected override Entity GetEntity()
        {
			return m.ProductDescription.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ProductDescription.Entity.FunctionalId;
            }
        }

		internal ProductDescriptionNode() { }
		internal ProductDescriptionNode(ProductDescriptionAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductDescriptionNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ProductDescriptionNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ProductDescriptionNode Where(JsNotation<string> Description = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductDescriptionAlias> alias = new Lazy<ProductDescriptionAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Description.HasValue) conditions.Add(new QueryCondition(alias.Value.Description, Operator.Equals, ((IValue)Description).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ProductDescriptionNode Assign(JsNotation<string> Description = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductDescriptionAlias> alias = new Lazy<ProductDescriptionAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Description.HasValue) assignments.Add(new Assignment(alias.Value.Description, Description));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ProductDescriptionNode Alias(out ProductDescriptionAlias alias)
        {
            if (NodeAlias is ProductDescriptionAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ProductDescriptionAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ProductDescriptionNode Alias(out ProductDescriptionAlias alias, string name)
        {
            if (NodeAlias is ProductDescriptionAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ProductDescriptionAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ProductDescriptionNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public ProductDescriptionOut Out { get { return new ProductDescriptionOut(this); } }
		public class ProductDescriptionOut
		{
			private ProductDescriptionNode Parent;
			internal ProductDescriptionOut(ProductDescriptionNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PRODUCTMODEL_HAS_PRODUCTDESCRIPTION_REL PRODUCTMODEL_HAS_PRODUCTDESCRIPTION { get { return new PRODUCTMODEL_HAS_PRODUCTDESCRIPTION_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class ProductDescriptionAlias : AliasResult<ProductDescriptionAlias, ProductDescriptionListAlias>
	{
		internal ProductDescriptionAlias(ProductDescriptionNode parent)
		{
			Node = parent;
		}
		internal ProductDescriptionAlias(ProductDescriptionNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ProductDescriptionAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ProductDescriptionAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ProductDescriptionAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Description = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Description.HasValue) assignments.Add(new Assignment(this.Description, Description));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
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
						{ "Description", new StringResult(this, "Description", Datastore.AdventureWorks.Model.Entities["ProductDescription"], Datastore.AdventureWorks.Model.Entities["ProductDescription"].Properties["Description"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["ProductDescription"], Datastore.AdventureWorks.Model.Entities["ProductDescription"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductDescription"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductDescription"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ProductDescriptionNode.ProductDescriptionOut Out { get { return new ProductDescriptionNode.ProductDescriptionOut(new ProductDescriptionNode(this, true)); } }

		public StringResult Description
		{
			get
			{
				if (m_Description is null)
					m_Description = (StringResult)AliasFields["Description"];

				return m_Description;
			}
		}
		private StringResult m_Description = null;
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
		public AsResult As(string aliasName, out ProductDescriptionAlias alias)
		{
			alias = new ProductDescriptionAlias((ProductDescriptionNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ProductDescriptionListAlias : ListResult<ProductDescriptionListAlias, ProductDescriptionAlias>, IAliasListResult
	{
		private ProductDescriptionListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductDescriptionListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductDescriptionListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ProductDescriptionJaggedListAlias : ListResult<ProductDescriptionJaggedListAlias, ProductDescriptionListAlias>, IAliasJaggedListResult
	{
		private ProductDescriptionJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductDescriptionJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductDescriptionJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
