
using System;
using Blueprint41;
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
            ReferenceOrderID = new NumericResult(this, "ReferenceOrderID", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["ReferenceOrderID"]);
            TransactionDate = new DateTimeResult(this, "TransactionDate", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["TransactionDate"]);
            TransactionType = new StringResult(this, "TransactionType", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["TransactionType"]);
            Quantity = new NumericResult(this, "Quantity", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["Quantity"]);
            ActualCost = new StringResult(this, "ActualCost", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["ActualCost"]);
            ReferenceOrderLineID = new NumericResult(this, "ReferenceOrderLineID", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["ReferenceOrderLineID"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["TransactionHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public TransactionHistoryNode.TransactionHistoryOut Out { get { return new TransactionHistoryNode.TransactionHistoryOut(new TransactionHistoryNode(this, true)); } }

        public NumericResult ReferenceOrderID { get; private set; } 
        public DateTimeResult TransactionDate { get; private set; } 
        public StringResult TransactionType { get; private set; } 
        public NumericResult Quantity { get; private set; } 
        public StringResult ActualCost { get; private set; } 
        public NumericResult ReferenceOrderLineID { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
