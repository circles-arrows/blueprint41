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
		public static ProductVendorNode ProductVendor { get { return new ProductVendorNode(); } }
	}

	public partial class ProductVendorNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(ProductVendorNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(ProductVendorNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "ProductVendor";
		}

		protected override Entity GetEntity()
        {
			return m.ProductVendor.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.ProductVendor.Entity.FunctionalId;
            }
        }

		internal ProductVendorNode() { }
		internal ProductVendorNode(ProductVendorAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ProductVendorNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal ProductVendorNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public ProductVendorNode Where(JsNotation<string> AverageLeadTime = default, JsNotation<string> LastReceiptCost = default, JsNotation<System.DateTime?> LastReceiptDate = default, JsNotation<int> MaxOrderQty = default, JsNotation<int> MinOrderQty = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OnOrderQty = default, JsNotation<string> StandardPrice = default, JsNotation<string> Uid = default, JsNotation<string> UnitMeasureCode = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductVendorAlias> alias = new Lazy<ProductVendorAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (AverageLeadTime.HasValue) conditions.Add(new QueryCondition(alias.Value.AverageLeadTime, Operator.Equals, ((IValue)AverageLeadTime).GetValue()));
            if (LastReceiptCost.HasValue) conditions.Add(new QueryCondition(alias.Value.LastReceiptCost, Operator.Equals, ((IValue)LastReceiptCost).GetValue()));
            if (LastReceiptDate.HasValue) conditions.Add(new QueryCondition(alias.Value.LastReceiptDate, Operator.Equals, ((IValue)LastReceiptDate).GetValue()));
            if (MaxOrderQty.HasValue) conditions.Add(new QueryCondition(alias.Value.MaxOrderQty, Operator.Equals, ((IValue)MaxOrderQty).GetValue()));
            if (MinOrderQty.HasValue) conditions.Add(new QueryCondition(alias.Value.MinOrderQty, Operator.Equals, ((IValue)MinOrderQty).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (OnOrderQty.HasValue) conditions.Add(new QueryCondition(alias.Value.OnOrderQty, Operator.Equals, ((IValue)OnOrderQty).GetValue()));
            if (StandardPrice.HasValue) conditions.Add(new QueryCondition(alias.Value.StandardPrice, Operator.Equals, ((IValue)StandardPrice).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));
            if (UnitMeasureCode.HasValue) conditions.Add(new QueryCondition(alias.Value.UnitMeasureCode, Operator.Equals, ((IValue)UnitMeasureCode).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public ProductVendorNode Assign(JsNotation<string> AverageLeadTime = default, JsNotation<string> LastReceiptCost = default, JsNotation<System.DateTime?> LastReceiptDate = default, JsNotation<int> MaxOrderQty = default, JsNotation<int> MinOrderQty = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OnOrderQty = default, JsNotation<string> StandardPrice = default, JsNotation<string> Uid = default, JsNotation<string> UnitMeasureCode = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<ProductVendorAlias> alias = new Lazy<ProductVendorAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (AverageLeadTime.HasValue) assignments.Add(new Assignment(alias.Value.AverageLeadTime, AverageLeadTime));
            if (LastReceiptCost.HasValue) assignments.Add(new Assignment(alias.Value.LastReceiptCost, LastReceiptCost));
            if (LastReceiptDate.HasValue) assignments.Add(new Assignment(alias.Value.LastReceiptDate, LastReceiptDate));
            if (MaxOrderQty.HasValue) assignments.Add(new Assignment(alias.Value.MaxOrderQty, MaxOrderQty));
            if (MinOrderQty.HasValue) assignments.Add(new Assignment(alias.Value.MinOrderQty, MinOrderQty));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (OnOrderQty.HasValue) assignments.Add(new Assignment(alias.Value.OnOrderQty, OnOrderQty));
            if (StandardPrice.HasValue) assignments.Add(new Assignment(alias.Value.StandardPrice, StandardPrice));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));
            if (UnitMeasureCode.HasValue) assignments.Add(new Assignment(alias.Value.UnitMeasureCode, UnitMeasureCode));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public ProductVendorNode Alias(out ProductVendorAlias alias)
        {
            if (NodeAlias is ProductVendorAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new ProductVendorAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public ProductVendorNode Alias(out ProductVendorAlias alias, string name)
        {
            if (NodeAlias is ProductVendorAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new ProductVendorAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public ProductVendorNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public ProductVendorIn  In  { get { return new ProductVendorIn(this); } }
		public class ProductVendorIn
		{
			private ProductVendorNode Parent;
			internal ProductVendorIn(ProductVendorNode parent)
			{
				Parent = parent;
			}
			public IFromIn_PRODUCTVENDOR_HAS_PRODUCT_REL PRODUCTVENDOR_HAS_PRODUCT { get { return new PRODUCTVENDOR_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_PRODUCTVENDOR_HAS_UNITMEASURE_REL PRODUCTVENDOR_HAS_UNITMEASURE { get { return new PRODUCTVENDOR_HAS_UNITMEASURE_REL(Parent, DirectionEnum.In); } }

		}

		public ProductVendorOut Out { get { return new ProductVendorOut(this); } }
		public class ProductVendorOut
		{
			private ProductVendorNode Parent;
			internal ProductVendorOut(ProductVendorNode parent)
			{
				Parent = parent;
			}
			public IFromOut_VENDOR_BECOMES_PRODUCTVENDOR_REL VENDOR_BECOMES_PRODUCTVENDOR { get { return new VENDOR_BECOMES_PRODUCTVENDOR_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class ProductVendorAlias : AliasResult<ProductVendorAlias, ProductVendorListAlias>
	{
		internal ProductVendorAlias(ProductVendorNode parent)
		{
			Node = parent;
		}
		internal ProductVendorAlias(ProductVendorNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  ProductVendorAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  ProductVendorAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  ProductVendorAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> AverageLeadTime = default, JsNotation<string> LastReceiptCost = default, JsNotation<System.DateTime?> LastReceiptDate = default, JsNotation<int> MaxOrderQty = default, JsNotation<int> MinOrderQty = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> OnOrderQty = default, JsNotation<string> StandardPrice = default, JsNotation<string> Uid = default, JsNotation<string> UnitMeasureCode = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (AverageLeadTime.HasValue) assignments.Add(new Assignment(this.AverageLeadTime, AverageLeadTime));
			if (LastReceiptCost.HasValue) assignments.Add(new Assignment(this.LastReceiptCost, LastReceiptCost));
			if (LastReceiptDate.HasValue) assignments.Add(new Assignment(this.LastReceiptDate, LastReceiptDate));
			if (MaxOrderQty.HasValue) assignments.Add(new Assignment(this.MaxOrderQty, MaxOrderQty));
			if (MinOrderQty.HasValue) assignments.Add(new Assignment(this.MinOrderQty, MinOrderQty));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (OnOrderQty.HasValue) assignments.Add(new Assignment(this.OnOrderQty, OnOrderQty));
			if (StandardPrice.HasValue) assignments.Add(new Assignment(this.StandardPrice, StandardPrice));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
			if (UnitMeasureCode.HasValue) assignments.Add(new Assignment(this.UnitMeasureCode, UnitMeasureCode));
            
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
						{ "AverageLeadTime", new StringResult(this, "AverageLeadTime", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["AverageLeadTime"]) },
						{ "StandardPrice", new StringResult(this, "StandardPrice", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["StandardPrice"]) },
						{ "LastReceiptCost", new StringResult(this, "LastReceiptCost", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["LastReceiptCost"]) },
						{ "LastReceiptDate", new DateTimeResult(this, "LastReceiptDate", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["LastReceiptDate"]) },
						{ "MinOrderQty", new NumericResult(this, "MinOrderQty", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["MinOrderQty"]) },
						{ "MaxOrderQty", new NumericResult(this, "MaxOrderQty", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["MaxOrderQty"]) },
						{ "OnOrderQty", new NumericResult(this, "OnOrderQty", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["OnOrderQty"]) },
						{ "UnitMeasureCode", new StringResult(this, "UnitMeasureCode", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["ProductVendor"].Properties["UnitMeasureCode"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ProductVendor"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public ProductVendorNode.ProductVendorIn In { get { return new ProductVendorNode.ProductVendorIn(new ProductVendorNode(this, true)); } }
		public ProductVendorNode.ProductVendorOut Out { get { return new ProductVendorNode.ProductVendorOut(new ProductVendorNode(this, true)); } }

		public StringResult AverageLeadTime
		{
			get
			{
				if (m_AverageLeadTime is null)
					m_AverageLeadTime = (StringResult)AliasFields["AverageLeadTime"];

				return m_AverageLeadTime;
			}
		}
		private StringResult m_AverageLeadTime = null;
		public StringResult StandardPrice
		{
			get
			{
				if (m_StandardPrice is null)
					m_StandardPrice = (StringResult)AliasFields["StandardPrice"];

				return m_StandardPrice;
			}
		}
		private StringResult m_StandardPrice = null;
		public StringResult LastReceiptCost
		{
			get
			{
				if (m_LastReceiptCost is null)
					m_LastReceiptCost = (StringResult)AliasFields["LastReceiptCost"];

				return m_LastReceiptCost;
			}
		}
		private StringResult m_LastReceiptCost = null;
		public DateTimeResult LastReceiptDate
		{
			get
			{
				if (m_LastReceiptDate is null)
					m_LastReceiptDate = (DateTimeResult)AliasFields["LastReceiptDate"];

				return m_LastReceiptDate;
			}
		}
		private DateTimeResult m_LastReceiptDate = null;
		public NumericResult MinOrderQty
		{
			get
			{
				if (m_MinOrderQty is null)
					m_MinOrderQty = (NumericResult)AliasFields["MinOrderQty"];

				return m_MinOrderQty;
			}
		}
		private NumericResult m_MinOrderQty = null;
		public NumericResult MaxOrderQty
		{
			get
			{
				if (m_MaxOrderQty is null)
					m_MaxOrderQty = (NumericResult)AliasFields["MaxOrderQty"];

				return m_MaxOrderQty;
			}
		}
		private NumericResult m_MaxOrderQty = null;
		public NumericResult OnOrderQty
		{
			get
			{
				if (m_OnOrderQty is null)
					m_OnOrderQty = (NumericResult)AliasFields["OnOrderQty"];

				return m_OnOrderQty;
			}
		}
		private NumericResult m_OnOrderQty = null;
		public StringResult UnitMeasureCode
		{
			get
			{
				if (m_UnitMeasureCode is null)
					m_UnitMeasureCode = (StringResult)AliasFields["UnitMeasureCode"];

				return m_UnitMeasureCode;
			}
		}
		private StringResult m_UnitMeasureCode = null;
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
		public AsResult As(string aliasName, out ProductVendorAlias alias)
		{
			alias = new ProductVendorAlias((ProductVendorNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class ProductVendorListAlias : ListResult<ProductVendorListAlias, ProductVendorAlias>, IAliasListResult
	{
		private ProductVendorListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductVendorListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductVendorListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class ProductVendorJaggedListAlias : ListResult<ProductVendorJaggedListAlias, ProductVendorListAlias>, IAliasJaggedListResult
	{
		private ProductVendorJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private ProductVendorJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private ProductVendorJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
