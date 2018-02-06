using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static TransactionHistoryNode TransactionHistory { get { return new TransactionHistoryNode(); } }
	}

	public partial class TransactionHistoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "TransactionHistory";
            }
        }

		internal TransactionHistoryNode() { }
		internal TransactionHistoryNode(TransactionHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal TransactionHistoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public TransactionHistoryNode Alias(out TransactionHistoryAlias alias)
		{
			alias = new TransactionHistoryAlias(this);
            NodeAlias = alias;
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

    public class TransactionHistoryAlias : AliasResult
    {
        internal TransactionHistoryAlias(TransactionHistoryNode parent)
        {
			Node = parent;
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
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
				if ((object)m_ReferenceOrderID == null)
					m_ReferenceOrderID = (NumericResult)AliasFields["ReferenceOrderID"];

				return m_ReferenceOrderID;
			}
		} 
        private NumericResult m_ReferenceOrderID = null;
        public DateTimeResult TransactionDate
		{
			get
			{
				if ((object)m_TransactionDate == null)
					m_TransactionDate = (DateTimeResult)AliasFields["TransactionDate"];

				return m_TransactionDate;
			}
		} 
        private DateTimeResult m_TransactionDate = null;
        public StringResult TransactionType
		{
			get
			{
				if ((object)m_TransactionType == null)
					m_TransactionType = (StringResult)AliasFields["TransactionType"];

				return m_TransactionType;
			}
		} 
        private StringResult m_TransactionType = null;
        public NumericResult Quantity
		{
			get
			{
				if ((object)m_Quantity == null)
					m_Quantity = (NumericResult)AliasFields["Quantity"];

				return m_Quantity;
			}
		} 
        private NumericResult m_Quantity = null;
        public StringResult ActualCost
		{
			get
			{
				if ((object)m_ActualCost == null)
					m_ActualCost = (StringResult)AliasFields["ActualCost"];

				return m_ActualCost;
			}
		} 
        private StringResult m_ActualCost = null;
        public NumericResult ReferenceOrderLineID
		{
			get
			{
				if ((object)m_ReferenceOrderLineID == null)
					m_ReferenceOrderLineID = (NumericResult)AliasFields["ReferenceOrderLineID"];

				return m_ReferenceOrderLineID;
			}
		} 
        private NumericResult m_ReferenceOrderLineID = null;
        public DateTimeResult ModifiedDate
		{
			get
			{
				if ((object)m_ModifiedDate == null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		} 
        private DateTimeResult m_ModifiedDate = null;
        public StringResult Uid
		{
			get
			{
				if ((object)m_Uid == null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		} 
        private StringResult m_Uid = null;
    }
}
