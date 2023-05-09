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
		public static TransactionHistoryNode TransactionHistory { get { return new TransactionHistoryNode(); } }
	}

	public partial class TransactionHistoryNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(TransactionHistoryNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(TransactionHistoryNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "TransactionHistory";
		}

		protected override Entity GetEntity()
        {
			return m.TransactionHistory.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.TransactionHistory.Entity.FunctionalId;
            }
        }

		internal TransactionHistoryNode() { }
		internal TransactionHistoryNode(TransactionHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal TransactionHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal TransactionHistoryNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public TransactionHistoryNode Where(JsNotation<string> ActualCost = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<int> ReferenceOrderID = default, JsNotation<int> ReferenceOrderLineID = default, JsNotation<System.DateTime> TransactionDate = default, JsNotation<string> TransactionType = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<TransactionHistoryAlias> alias = new Lazy<TransactionHistoryAlias>(delegate()
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
        public TransactionHistoryNode Assign(JsNotation<string> ActualCost = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<int> ReferenceOrderID = default, JsNotation<int> ReferenceOrderLineID = default, JsNotation<System.DateTime> TransactionDate = default, JsNotation<string> TransactionType = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<TransactionHistoryAlias> alias = new Lazy<TransactionHistoryAlias>(delegate()
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

		public TransactionHistoryNode Alias(out TransactionHistoryAlias alias)
        {
            if (NodeAlias is TransactionHistoryAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new TransactionHistoryAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public TransactionHistoryNode Alias(out TransactionHistoryAlias alias, string name)
        {
            if (NodeAlias is TransactionHistoryAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new TransactionHistoryAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public TransactionHistoryNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public TransactionHistoryOut Out { get { return new TransactionHistoryOut(this); } }
		public class TransactionHistoryOut
		{
			private TransactionHistoryNode Parent;
			internal TransactionHistoryOut(TransactionHistoryNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PRODUCT_HAS_TRANSACTIONHISTORY_REL PRODUCT_HAS_TRANSACTIONHISTORY { get { return new PRODUCT_HAS_TRANSACTIONHISTORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class TransactionHistoryAlias : AliasResult<TransactionHistoryAlias, TransactionHistoryListAlias>
	{
		internal TransactionHistoryAlias(TransactionHistoryNode parent)
		{
			Node = parent;
		}
		internal TransactionHistoryAlias(TransactionHistoryNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  TransactionHistoryAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  TransactionHistoryAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  TransactionHistoryAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> ActualCost = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<int> Quantity = default, JsNotation<int> ReferenceOrderID = default, JsNotation<int> ReferenceOrderLineID = default, JsNotation<System.DateTime> TransactionDate = default, JsNotation<string> TransactionType = default, JsNotation<string> Uid = default)
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
						{ "ReferenceOrderID", new NumericResult(this, "ReferenceOrderID", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["ReferenceOrderID"]) },
						{ "TransactionDate", new DateTimeResult(this, "TransactionDate", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["TransactionDate"]) },
						{ "TransactionType", new StringResult(this, "TransactionType", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["TransactionType"]) },
						{ "Quantity", new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["Quantity"]) },
						{ "ActualCost", new StringResult(this, "ActualCost", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["ActualCost"]) },
						{ "ReferenceOrderLineID", new NumericResult(this, "ReferenceOrderLineID", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["ReferenceOrderLineID"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public TransactionHistoryNode.TransactionHistoryOut Out { get { return new TransactionHistoryNode.TransactionHistoryOut(new TransactionHistoryNode(this, true)); } }

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
		public StringResult ActualCost
		{
			get
			{
				if (m_ActualCost is null)
					m_ActualCost = (StringResult)AliasFields["ActualCost"];

				return m_ActualCost;
			}
		}
		private StringResult m_ActualCost = null;
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
		public AsResult As(string aliasName, out TransactionHistoryAlias alias)
		{
			alias = new TransactionHistoryAlias((TransactionHistoryNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class TransactionHistoryListAlias : ListResult<TransactionHistoryListAlias, TransactionHistoryAlias>, IAliasListResult
	{
		private TransactionHistoryListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private TransactionHistoryListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private TransactionHistoryListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class TransactionHistoryJaggedListAlias : ListResult<TransactionHistoryJaggedListAlias, TransactionHistoryListAlias>, IAliasJaggedListResult
	{
		private TransactionHistoryJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private TransactionHistoryJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private TransactionHistoryJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
