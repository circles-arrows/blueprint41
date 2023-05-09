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
		public static SpecialOfferNode SpecialOffer { get { return new SpecialOfferNode(); } }
	}

	public partial class SpecialOfferNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(SpecialOfferNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(SpecialOfferNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "SpecialOffer";
		}

		protected override Entity GetEntity()
        {
			return m.SpecialOffer.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.SpecialOffer.Entity.FunctionalId;
            }
        }

		internal SpecialOfferNode() { }
		internal SpecialOfferNode(SpecialOfferAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SpecialOfferNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal SpecialOfferNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public SpecialOfferNode Where(JsNotation<string> Category = default, JsNotation<string> Description = default, JsNotation<string> DiscountPct = default, JsNotation<System.DateTime> EndDate = default, JsNotation<string> MaxQty = default, JsNotation<int> MinQty = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Type = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SpecialOfferAlias> alias = new Lazy<SpecialOfferAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (Category.HasValue) conditions.Add(new QueryCondition(alias.Value.Category, Operator.Equals, ((IValue)Category).GetValue()));
            if (Description.HasValue) conditions.Add(new QueryCondition(alias.Value.Description, Operator.Equals, ((IValue)Description).GetValue()));
            if (DiscountPct.HasValue) conditions.Add(new QueryCondition(alias.Value.DiscountPct, Operator.Equals, ((IValue)DiscountPct).GetValue()));
            if (EndDate.HasValue) conditions.Add(new QueryCondition(alias.Value.EndDate, Operator.Equals, ((IValue)EndDate).GetValue()));
            if (MaxQty.HasValue) conditions.Add(new QueryCondition(alias.Value.MaxQty, Operator.Equals, ((IValue)MaxQty).GetValue()));
            if (MinQty.HasValue) conditions.Add(new QueryCondition(alias.Value.MinQty, Operator.Equals, ((IValue)MinQty).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (StartDate.HasValue) conditions.Add(new QueryCondition(alias.Value.StartDate, Operator.Equals, ((IValue)StartDate).GetValue()));
            if (Type.HasValue) conditions.Add(new QueryCondition(alias.Value.Type, Operator.Equals, ((IValue)Type).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public SpecialOfferNode Assign(JsNotation<string> Category = default, JsNotation<string> Description = default, JsNotation<string> DiscountPct = default, JsNotation<System.DateTime> EndDate = default, JsNotation<string> MaxQty = default, JsNotation<int> MinQty = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Type = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<SpecialOfferAlias> alias = new Lazy<SpecialOfferAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (Category.HasValue) assignments.Add(new Assignment(alias.Value.Category, Category));
            if (Description.HasValue) assignments.Add(new Assignment(alias.Value.Description, Description));
            if (DiscountPct.HasValue) assignments.Add(new Assignment(alias.Value.DiscountPct, DiscountPct));
            if (EndDate.HasValue) assignments.Add(new Assignment(alias.Value.EndDate, EndDate));
            if (MaxQty.HasValue) assignments.Add(new Assignment(alias.Value.MaxQty, MaxQty));
            if (MinQty.HasValue) assignments.Add(new Assignment(alias.Value.MinQty, MinQty));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (StartDate.HasValue) assignments.Add(new Assignment(alias.Value.StartDate, StartDate));
            if (Type.HasValue) assignments.Add(new Assignment(alias.Value.Type, Type));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public SpecialOfferNode Alias(out SpecialOfferAlias alias)
        {
            if (NodeAlias is SpecialOfferAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new SpecialOfferAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public SpecialOfferNode Alias(out SpecialOfferAlias alias, string name)
        {
            if (NodeAlias is SpecialOfferAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new SpecialOfferAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public SpecialOfferNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public SpecialOfferOut Out { get { return new SpecialOfferOut(this); } }
		public class SpecialOfferOut
		{
			private SpecialOfferNode Parent;
			internal SpecialOfferOut(SpecialOfferNode parent)
			{
				Parent = parent;
			}
			public IFromOut_SALESORDERDETAIL_HAS_SPECIALOFFER_REL SALESORDERDETAIL_HAS_SPECIALOFFER { get { return new SALESORDERDETAIL_HAS_SPECIALOFFER_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class SpecialOfferAlias : AliasResult<SpecialOfferAlias, SpecialOfferListAlias>
	{
		internal SpecialOfferAlias(SpecialOfferNode parent)
		{
			Node = parent;
		}
		internal SpecialOfferAlias(SpecialOfferNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  SpecialOfferAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  SpecialOfferAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  SpecialOfferAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> Category = default, JsNotation<string> Description = default, JsNotation<string> DiscountPct = default, JsNotation<System.DateTime> EndDate = default, JsNotation<string> MaxQty = default, JsNotation<int> MinQty = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> rowguid = default, JsNotation<System.DateTime> StartDate = default, JsNotation<string> Type = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (Category.HasValue) assignments.Add(new Assignment(this.Category, Category));
			if (Description.HasValue) assignments.Add(new Assignment(this.Description, Description));
			if (DiscountPct.HasValue) assignments.Add(new Assignment(this.DiscountPct, DiscountPct));
			if (EndDate.HasValue) assignments.Add(new Assignment(this.EndDate, EndDate));
			if (MaxQty.HasValue) assignments.Add(new Assignment(this.MaxQty, MaxQty));
			if (MinQty.HasValue) assignments.Add(new Assignment(this.MinQty, MinQty));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (StartDate.HasValue) assignments.Add(new Assignment(this.StartDate, StartDate));
			if (Type.HasValue) assignments.Add(new Assignment(this.Type, Type));
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
						{ "Description", new StringResult(this, "Description", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Description"]) },
						{ "DiscountPct", new StringResult(this, "DiscountPct", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["DiscountPct"]) },
						{ "Type", new StringResult(this, "Type", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Type"]) },
						{ "Category", new StringResult(this, "Category", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Category"]) },
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["EndDate"]) },
						{ "MinQty", new NumericResult(this, "MinQty", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["MinQty"]) },
						{ "MaxQty", new StringResult(this, "MaxQty", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["MaxQty"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SpecialOffer"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public SpecialOfferNode.SpecialOfferOut Out { get { return new SpecialOfferNode.SpecialOfferOut(new SpecialOfferNode(this, true)); } }

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
		public StringResult DiscountPct
		{
			get
			{
				if (m_DiscountPct is null)
					m_DiscountPct = (StringResult)AliasFields["DiscountPct"];

				return m_DiscountPct;
			}
		}
		private StringResult m_DiscountPct = null;
		public StringResult Type
		{
			get
			{
				if (m_Type is null)
					m_Type = (StringResult)AliasFields["Type"];

				return m_Type;
			}
		}
		private StringResult m_Type = null;
		public StringResult Category
		{
			get
			{
				if (m_Category is null)
					m_Category = (StringResult)AliasFields["Category"];

				return m_Category;
			}
		}
		private StringResult m_Category = null;
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
		public NumericResult MinQty
		{
			get
			{
				if (m_MinQty is null)
					m_MinQty = (NumericResult)AliasFields["MinQty"];

				return m_MinQty;
			}
		}
		private NumericResult m_MinQty = null;
		public StringResult MaxQty
		{
			get
			{
				if (m_MaxQty is null)
					m_MaxQty = (StringResult)AliasFields["MaxQty"];

				return m_MaxQty;
			}
		}
		private StringResult m_MaxQty = null;
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
		public AsResult As(string aliasName, out SpecialOfferAlias alias)
		{
			alias = new SpecialOfferAlias((SpecialOfferNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class SpecialOfferListAlias : ListResult<SpecialOfferListAlias, SpecialOfferAlias>, IAliasListResult
	{
		private SpecialOfferListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SpecialOfferListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SpecialOfferListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class SpecialOfferJaggedListAlias : ListResult<SpecialOfferJaggedListAlias, SpecialOfferListAlias>, IAliasJaggedListResult
	{
		private SpecialOfferJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private SpecialOfferJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private SpecialOfferJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
