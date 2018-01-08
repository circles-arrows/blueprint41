
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CustomerNode Customer { get { return new CustomerNode(); } }
	}

	public partial class CustomerNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Customer";
            }
        }

		internal CustomerNode() { }
		internal CustomerNode(CustomerAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CustomerNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public CustomerNode Alias(out CustomerAlias alias)
		{
			alias = new CustomerAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public CustomerIn  In  { get { return new CustomerIn(this); } }
		public class CustomerIn
		{
			private CustomerNode Parent;
			internal CustomerIn(CustomerNode parent)
			{
                Parent = parent;
			}
			public IFromIn_CUSTOMER_HAS_PERSON_REL CUSTOMER_HAS_PERSON { get { return new CUSTOMER_HAS_PERSON_REL(Parent, DirectionEnum.In); } }
			public IFromIn_CUSTOMER_HAS_SALESTERRITORY_REL CUSTOMER_HAS_SALESTERRITORY { get { return new CUSTOMER_HAS_SALESTERRITORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_CUSTOMER_HAS_STORE_REL CUSTOMER_HAS_STORE { get { return new CUSTOMER_HAS_STORE_REL(Parent, DirectionEnum.In); } }

		}
	}

    public class CustomerAlias : AliasResult
    {
        internal CustomerAlias(CustomerNode parent)
        {
			Node = parent;
            AccountNumber = new StringResult(this, "AccountNumber", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["Customer"].Properties["AccountNumber"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["Customer"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public CustomerNode.CustomerIn In { get { return new CustomerNode.CustomerIn(new CustomerNode(this, true)); } }

        public StringResult AccountNumber { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
