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
		public static TransactionHistoryArchiveNode TransactionHistoryArchive { get { return new TransactionHistoryArchiveNode(); } }
	}

	public partial class TransactionHistoryArchiveNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(TransactionHistoryArchiveNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(TransactionHistoryArchiveNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "TransactionHistoryArchive";
		}

		protected override Entity GetEntity()
        {
			return m.TransactionHistoryArchive.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.TransactionHistoryArchive.Entity.FunctionalId;
            }
        }

		internal TransactionHistoryArchiveNode() { }
		internal TransactionHistoryArchiveNode(TransactionHistoryArchiveAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal TransactionHistoryArchiveNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal TransactionHistoryArchiveNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public TransactionHistoryArchiveNode Where(JsNotation<decimal> ActualCost = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<int> ReferenceOrderID = default, JsNotation<int> ReferenceOrderLineID = default, JsNotation<System.DateTime> TransactionDate = default, JsNotation<string> TransactionType = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<TransactionHistoryArchiveAlias> alias = new Lazy<TransactionHistoryArchiveAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ActualCost.HasValue) conditions.Add(new QueryCondition(alias.Value.ActualCost, Operator.Equals, ((IValue)ActualCost).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Quantity.HasValue) conditions.Add(new QueryCondition(alias.Value.Quantity, Operator.Equals, ((IValue)Quantity).GetValue()));
            if (ReferenceOrderID.HasValue) conditions.Add(new QueryCondition(alias.Value.ReferenceOrderID, Operator.Equals, ((IValue)ReferenceOrderID).GetValue()));
            if (ReferenceOrderLineID.HasValue) conditions.Add(new QueryCondition(alias.Value.ReferenceOrderLineID, Operator.Equals, ((IValue)ReferenceOrderLineID).GetValue()));
            if (TransactionDate.HasValue) conditions.Add(new QueryCondition(alias.Value.TransactionDate, Operator.Equals, ((IValue)TransactionDate).GetValue()));
            if (TransactionType.HasValue) conditions.Add(new QueryCondition(alias.Value.TransactionType, Operator.Equals, ((IValue)TransactionType).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public TransactionHistoryArchiveNode Assign(JsNotation<decimal> ActualCost = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<int> ReferenceOrderID = default, JsNotation<int> ReferenceOrderLineID = default, JsNotation<System.DateTime> TransactionDate = default, JsNotation<string> TransactionType = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<TransactionHistoryArchiveAlias> alias = new Lazy<TransactionHistoryArchiveAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ActualCost.HasValue) assignments.Add(new Assignment(alias.Value.ActualCost, ActualCost));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Quantity.HasValue) assignments.Add(new Assignment(alias.Value.Quantity, Quantity));
            if (ReferenceOrderID.HasValue) assignments.Add(new Assignment(alias.Value.ReferenceOrderID, ReferenceOrderID));
            if (ReferenceOrderLineID.HasValue) assignments.Add(new Assignment(alias.Value.ReferenceOrderLineID, ReferenceOrderLineID));
            if (TransactionDate.HasValue) assignments.Add(new Assignment(alias.Value.TransactionDate, TransactionDate));
            if (TransactionType.HasValue) assignments.Add(new Assignment(alias.Value.TransactionType, TransactionType));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public TransactionHistoryArchiveNode Alias(out TransactionHistoryArchiveAlias alias)
        {
            if (NodeAlias is TransactionHistoryArchiveAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new TransactionHistoryArchiveAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public TransactionHistoryArchiveNode Alias(out TransactionHistoryArchiveAlias alias, string name)
        {
            if (NodeAlias is TransactionHistoryArchiveAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new TransactionHistoryArchiveAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public TransactionHistoryArchiveNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}

		public TransactionHistoryArchiveIn  In  { get { return new TransactionHistoryArchiveIn(this); } }
		public class TransactionHistoryArchiveIn
		{
			private TransactionHistoryArchiveNode Parent;
			internal TransactionHistoryArchiveIn(TransactionHistoryArchiveNode parent)
			{
				Parent = parent;
			}
			public IFromIn_TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT { get { return new TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }

		}
	}

	public class TransactionHistoryArchiveAlias : AliasResult<TransactionHistoryArchiveAlias, TransactionHistoryArchiveListAlias>
	{
		internal TransactionHistoryArchiveAlias(TransactionHistoryArchiveNode parent)
		{
			Node = parent;
		}
		internal TransactionHistoryArchiveAlias(TransactionHistoryArchiveNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  TransactionHistoryArchiveAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  TransactionHistoryArchiveAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  TransactionHistoryArchiveAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<decimal> ActualCost = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<int> ReferenceOrderID = default, JsNotation<int> ReferenceOrderLineID = default, JsNotation<System.DateTime> TransactionDate = default, JsNotation<string> TransactionType = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (ActualCost.HasValue) assignments.Add(new Assignment(this.ActualCost, ActualCost));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Quantity.HasValue) assignments.Add(new Assignment(this.Quantity, Quantity));
			if (ReferenceOrderID.HasValue) assignments.Add(new Assignment(this.ReferenceOrderID, ReferenceOrderID));
			if (ReferenceOrderLineID.HasValue) assignments.Add(new Assignment(this.ReferenceOrderLineID, ReferenceOrderLineID));
			if (TransactionDate.HasValue) assignments.Add(new Assignment(this.TransactionDate, TransactionDate));
			if (TransactionType.HasValue) assignments.Add(new Assignment(this.TransactionType, TransactionType));
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
						{ "ReferenceOrderID", new NumericResult(this, "ReferenceOrderID", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ReferenceOrderID"]) },
						{ "TransactionDate", new DateTimeResult(this, "TransactionDate", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["TransactionDate"]) },
						{ "TransactionType", new StringResult(this, "TransactionType", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["TransactionType"]) },
						{ "Quantity", new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["Quantity"]) },
						{ "ActualCost", new NumericResult(this, "ActualCost", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ActualCost"]) },
						{ "ReferenceOrderLineID", new NumericResult(this, "ReferenceOrderLineID", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ReferenceOrderLineID"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public TransactionHistoryArchiveNode.TransactionHistoryArchiveIn In { get { return new TransactionHistoryArchiveNode.TransactionHistoryArchiveIn(new TransactionHistoryArchiveNode(this, true)); } }

		public NumericResult ReferenceOrderID
		{
			get
			{
				if (m_ReferenceOrderID is null)
					m_ReferenceOrderID = (NumericResult)AliasFields["ReferenceOrderID"];

				return m_ReferenceOrderID;
			}
		}
		private NumericResult m_ReferenceOrderID = null;
		public DateTimeResult TransactionDate
		{
			get
			{
				if (m_TransactionDate is null)
					m_TransactionDate = (DateTimeResult)AliasFields["TransactionDate"];

				return m_TransactionDate;
			}
		}
		private DateTimeResult m_TransactionDate = null;
		public StringResult TransactionType
		{
			get
			{
				if (m_TransactionType is null)
					m_TransactionType = (StringResult)AliasFields["TransactionType"];

				return m_TransactionType;
			}
		}
		private StringResult m_TransactionType = null;
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
		public NumericResult ActualCost
		{
			get
			{
				if (m_ActualCost is null)
					m_ActualCost = (NumericResult)AliasFields["ActualCost"];

				return m_ActualCost;
			}
		}
		private NumericResult m_ActualCost = null;
		public NumericResult ReferenceOrderLineID
		{
			get
			{
				if (m_ReferenceOrderLineID is null)
					m_ReferenceOrderLineID = (NumericResult)AliasFields["ReferenceOrderLineID"];

				return m_ReferenceOrderLineID;
			}
		}
		private NumericResult m_ReferenceOrderLineID = null;
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
		public AsResult As(string aliasName, out TransactionHistoryArchiveAlias alias)
		{
			alias = new TransactionHistoryArchiveAlias((TransactionHistoryArchiveNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class TransactionHistoryArchiveListAlias : ListResult<TransactionHistoryArchiveListAlias, TransactionHistoryArchiveAlias>, IAliasListResult
	{
		private TransactionHistoryArchiveListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private TransactionHistoryArchiveListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private TransactionHistoryArchiveListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class TransactionHistoryArchiveJaggedListAlias : ListResult<TransactionHistoryArchiveJaggedListAlias, TransactionHistoryArchiveListAlias>, IAliasJaggedListResult
	{
		private TransactionHistoryArchiveJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private TransactionHistoryArchiveJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private TransactionHistoryArchiveJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
