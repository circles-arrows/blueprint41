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
		public static ProductPhotoNode ProductPhoto { get { return new ProductPhotoNode(); } }
	}

	public partial class ProductPhotoNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ProductPhotoNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ProductPhotoNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ProductPhoto";
		}

		protected override Entity GetEntity()
        {
			return m.ProductPhoto.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ProductPhoto.Entity.FunctionalId;
            }
        }

		internal ProductPhotoNode() { }
		internal ProductPhotoNode(ProductPhotoAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductPhotoNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ProductPhotoNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ProductPhotoNode Where(JsNotation<string> LargePhoto = default, JsNotation<string> LargePhotoFileName = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> ThumbNailPhoto = default, JsNotation<string> ThumbNailPhotoFileName = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductPhotoAlias> alias = new Lazy<ProductPhotoAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (LargePhoto.HasValue) conditions.Add(new QueryCondition(alias.Value.LargePhoto, Operator.Equals, ((IValue)LargePhoto).GetValue()));
            if (LargePhotoFileName.HasValue) conditions.Add(new QueryCondition(alias.Value.LargePhotoFileName, Operator.Equals, ((IValue)LargePhotoFileName).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (ThumbNailPhoto.HasValue) conditions.Add(new QueryCondition(alias.Value.ThumbNailPhoto, Operator.Equals, ((IValue)ThumbNailPhoto).GetValue()));
            if (ThumbNailPhotoFileName.HasValue) conditions.Add(new QueryCondition(alias.Value.ThumbNailPhotoFileName, Operator.Equals, ((IValue)ThumbNailPhotoFileName).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ProductPhotoNode Assign(JsNotation<string> LargePhoto = default, JsNotation<string> LargePhotoFileName = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> ThumbNailPhoto = default, JsNotation<string> ThumbNailPhotoFileName = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductPhotoAlias> alias = new Lazy<ProductPhotoAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (LargePhoto.HasValue) assignments.Add(new Assignment(alias.Value.LargePhoto, LargePhoto));
            if (LargePhotoFileName.HasValue) assignments.Add(new Assignment(alias.Value.LargePhotoFileName, LargePhotoFileName));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (ThumbNailPhoto.HasValue) assignments.Add(new Assignment(alias.Value.ThumbNailPhoto, ThumbNailPhoto));
            if (ThumbNailPhotoFileName.HasValue) assignments.Add(new Assignment(alias.Value.ThumbNailPhotoFileName, ThumbNailPhotoFileName));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ProductPhotoNode Alias(out ProductPhotoAlias alias)
        {
            if (NodeAlias is ProductPhotoAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ProductPhotoAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ProductPhotoNode Alias(out ProductPhotoAlias alias, string name)
        {
            if (NodeAlias is ProductPhotoAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ProductPhotoAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ProductPhotoNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public ProductPhotoOut Out { get { return new ProductPhotoOut(this); } }
		public class ProductPhotoOut
		{
			private ProductPhotoNode Parent;
			internal ProductPhotoOut(ProductPhotoNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO { get { return new PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class ProductPhotoAlias : AliasResult<ProductPhotoAlias, ProductPhotoListAlias>
	{
		internal ProductPhotoAlias(ProductPhotoNode parent)
		{
			Node = parent;
		}
		internal ProductPhotoAlias(ProductPhotoNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ProductPhotoAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ProductPhotoAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ProductPhotoAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> LargePhoto = default, JsNotation<string> LargePhotoFileName = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> ThumbNailPhoto = default, JsNotation<string> ThumbNailPhotoFileName = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (LargePhoto.HasValue) assignments.Add(new Assignment(this.LargePhoto, LargePhoto));
			if (LargePhotoFileName.HasValue) assignments.Add(new Assignment(this.LargePhotoFileName, LargePhotoFileName));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (ThumbNailPhoto.HasValue) assignments.Add(new Assignment(this.ThumbNailPhoto, ThumbNailPhoto));
			if (ThumbNailPhotoFileName.HasValue) assignments.Add(new Assignment(this.ThumbNailPhotoFileName, ThumbNailPhotoFileName));
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
						{ "ThumbNailPhoto", new StringResult(this, "ThumbNailPhoto", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["ThumbNailPhoto"]) },
						{ "ThumbNailPhotoFileName", new StringResult(this, "ThumbNailPhotoFileName", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["ThumbNailPhotoFileName"]) },
						{ "LargePhoto", new StringResult(this, "LargePhoto", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["LargePhoto"]) },
						{ "LargePhotoFileName", new StringResult(this, "LargePhotoFileName", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["LargePhotoFileName"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductPhoto"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ProductPhotoNode.ProductPhotoOut Out { get { return new ProductPhotoNode.ProductPhotoOut(new ProductPhotoNode(this, true)); } }

		public StringResult ThumbNailPhoto
		{
			get
			{
				if (m_ThumbNailPhoto is null)
					m_ThumbNailPhoto = (StringResult)AliasFields["ThumbNailPhoto"];

				return m_ThumbNailPhoto;
			}
		}
		private StringResult m_ThumbNailPhoto = null;
		public StringResult ThumbNailPhotoFileName
		{
			get
			{
				if (m_ThumbNailPhotoFileName is null)
					m_ThumbNailPhotoFileName = (StringResult)AliasFields["ThumbNailPhotoFileName"];

				return m_ThumbNailPhotoFileName;
			}
		}
		private StringResult m_ThumbNailPhotoFileName = null;
		public StringResult LargePhoto
		{
			get
			{
				if (m_LargePhoto is null)
					m_LargePhoto = (StringResult)AliasFields["LargePhoto"];

				return m_LargePhoto;
			}
		}
		private StringResult m_LargePhoto = null;
		public StringResult LargePhotoFileName
		{
			get
			{
				if (m_LargePhotoFileName is null)
					m_LargePhotoFileName = (StringResult)AliasFields["LargePhotoFileName"];

				return m_LargePhotoFileName;
			}
		}
		private StringResult m_LargePhotoFileName = null;
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
		public AsResult As(string aliasName, out ProductPhotoAlias alias)
		{
			alias = new ProductPhotoAlias((ProductPhotoNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ProductPhotoListAlias : ListResult<ProductPhotoListAlias, ProductPhotoAlias>, IAliasListResult
	{
		private ProductPhotoListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductPhotoListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductPhotoListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ProductPhotoJaggedListAlias : ListResult<ProductPhotoJaggedListAlias, ProductPhotoListAlias>, IAliasJaggedListResult
	{
		private ProductPhotoJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductPhotoJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductPhotoJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
