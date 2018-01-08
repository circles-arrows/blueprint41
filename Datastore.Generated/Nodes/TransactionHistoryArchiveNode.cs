
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static TransactionHistoryArchiveNode TransactionHistoryArchive { get { return new TransactionHistoryArchiveNode(); } }
	}

	public partial class TransactionHistoryArchiveNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "TransactionHistoryArchive";
            }
        }

		internal TransactionHistoryArchiveNode() { }
		internal TransactionHistoryArchiveNode(TransactionHistoryArchiveAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal TransactionHistoryArchiveNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public TransactionHistoryArchiveNode Alias(out TransactionHistoryArchiveAlias alias)
		{
			alias = new TransactionHistoryArchiveAlias(this);
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
            ReferenceOrderID = new NumericResult(this, "ReferenceOrderID", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ReferenceOrderID"]);
            TransactionDate = new DateTimeResult(this, "TransactionDate", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["TransactionDate"]);
            TransactionType = new StringResult(this, "TransactionType", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["TransactionType"]);
            Quantity = new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["Quantity"]);
            ActualCost = new MiscResult(this, "ActualCost", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ActualCost"]);
            ReferenceOrderLineID = new NumericResult(this, "ReferenceOrderLineID", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"].Properties["ReferenceOrderLineID"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public TransactionHistoryArchiveNode.TransactionHistoryArchiveIn In { get { return new TransactionHistoryArchiveNode.TransactionHistoryArchiveIn(new TransactionHistoryArchiveNode(this, true)); } }

        public NumericResult ReferenceOrderID { get; private set; } 
        public DateTimeResult TransactionDate { get; private set; } 
        public StringResult TransactionType { get; private set; } 
        public NumericResult Quantity { get; private set; } 
        public MiscResult ActualCost { get; private set; } 
        public NumericResult ReferenceOrderLineID { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
