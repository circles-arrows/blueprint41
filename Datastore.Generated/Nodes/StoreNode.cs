
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static StoreNode Store { get { return new StoreNode(); } }
	}

	public partial class StoreNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Store";
            }
        }

		internal StoreNode() { }
		internal StoreNode(StoreAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal StoreNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public StoreNode Alias(out StoreAlias alias)
		{
			alias = new StoreAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public StoreIn  In  { get { return new StoreIn(this); } }
		public class StoreIn
		{
			private StoreNode Parent;
			internal StoreIn(StoreNode parent)
			{
                Parent = parent;
			}
			public IFromIn_STORE_HAS_ADDRESS_REL STORE_HAS_ADDRESS { get { return new STORE_HAS_ADDRESS_REL(Parent, DirectionEnum.In); } }
			public IFromIn_STORE_VALID_FOR_SALESPERSON_REL STORE_VALID_FOR_SALESPERSON { get { return new STORE_VALID_FOR_SALESPERSON_REL(Parent, DirectionEnum.In); } }

		}

		public StoreOut Out { get { return new StoreOut(this); } }
		public class StoreOut
		{
			private StoreNode Parent;
			internal StoreOut(StoreNode parent)
			{
                Parent = parent;
			}
			public IFromOut_CUSTOMER_HAS_STORE_REL CUSTOMER_HAS_STORE { get { return new CUSTOMER_HAS_STORE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class StoreAlias : AliasResult
    {
        internal StoreAlias(StoreNode parent)
        {
			Node = parent;
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Store"].Properties["Name"]);
            Demographics = new StringResult(this, "Demographics", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Store"].Properties["Demographics"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Store"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public StoreNode.StoreIn In { get { return new StoreNode.StoreIn(new StoreNode(this, true)); } }
        public StoreNode.StoreOut Out { get { return new StoreNode.StoreOut(new StoreNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public StringResult Demographics { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
