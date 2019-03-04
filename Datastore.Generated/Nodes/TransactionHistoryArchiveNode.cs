using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static TransactionHistoryArchiveNode TransactionHistoryArchive { get { return new TransactionHistoryArchiveNode(); } }
	}

	public partial class TransactionHistoryArchiveNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "TransactionHistoryArchive";
        }

		internal TransactionHistoryArchiveNode() { }
		internal TransactionHistoryArchiveNode(TransactionHistoryArchiveAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal TransactionHistoryArchiveNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public TransactionHistoryArchiveNode Alias(out TransactionHistoryArchiveAlias alias)
		{
			alias = new TransactionHistoryArchiveAlias(this);
            NodeAlias = alias;
			return this;
		}

		public TransactionHistoryArchiveNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
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

    public class TransactionHistoryArchiveAlias : AliasResult
    {
        internal TransactionHistoryArchiveAlias(TransactionHistoryArchiveNode parent)
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
						{ "ReferenceOrderID", new NumericResult(this, "ReferenceOrderID", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ReferenceOrderID"]) },
						{ "TransactionDate", new DateTimeResult(this, "TransactionDate", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["TransactionDate"]) },
						{ "TransactionType", new StringResult(this, "TransactionType", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["TransactionType"]) },
						{ "Quantity", new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["Quantity"]) },
						{ "ActualCost", new MiscResult(this, "ActualCost", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ActualCost"]) },
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
        public MiscResult ActualCost
		{
			get
			{
				if ((object)m_ActualCost == null)
					m_ActualCost = (MiscResult)AliasFields["ActualCost"];

				return m_ActualCost;
			}
		} 
        private MiscResult m_ActualCost = null;
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
