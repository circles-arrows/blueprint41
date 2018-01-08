
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static AddressNode Address { get { return new AddressNode(); } }
	}

	public partial class AddressNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Address";
            }
        }

		internal AddressNode() { }
		internal AddressNode(AddressAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal AddressNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public AddressNode Alias(out AddressAlias alias)
		{
			alias = new AddressAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public AddressIn  In  { get { return new AddressIn(this); } }
		public class AddressIn
		{
			private AddressNode Parent;
			internal AddressIn(AddressNode parent)
			{
                Parent = parent;
			}
			public IFromIn_ADDRESS_HAS_ADDRESSTYPE_REL ADDRESS_HAS_ADDRESSTYPE { get { return new ADDRESS_HAS_ADDRESSTYPE_REL(Parent, DirectionEnum.In); } }
			public IFromIn_ADDRESS_HAS_STATEPROVINCE_REL ADDRESS_HAS_STATEPROVINCE { get { return new ADDRESS_HAS_STATEPROVINCE_REL(Parent, DirectionEnum.In); } }

		}

		public AddressOut Out { get { return new AddressOut(this); } }
		public class AddressOut
		{
			private AddressNode Parent;
			internal AddressOut(AddressNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PERSON_HAS_ADDRESS_REL PERSON_HAS_ADDRESS { get { return new PERSON_HAS_ADDRESS_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_SALESORDERHEADER_HAS_ADDRESS_REL SALESORDERHEADER_HAS_ADDRESS { get { return new SALESORDERHEADER_HAS_ADDRESS_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_STORE_HAS_ADDRESS_REL STORE_HAS_ADDRESS { get { return new STORE_HAS_ADDRESS_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class AddressAlias : AliasResult
    {
        internal AddressAlias(AddressNode parent)
        {
			Node = parent;
            AddressLine1 = new StringResult(this, "AddressLine1", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["AddressLine1"]);
            AddressLine2 = new StringResult(this, "AddressLine2", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["AddressLine2"]);
            City = new StringResult(this, "City", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["City"]);
            PostalCode = new StringResult(this, "PostalCode", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["PostalCode"]);
            SpatialLocation = new StringResult(this, "SpatialLocation", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["SpatialLocation"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public AddressNode.AddressIn In { get { return new AddressNode.AddressIn(new AddressNode(this, true)); } }
        public AddressNode.AddressOut Out { get { return new AddressNode.AddressOut(new AddressNode(this, true)); } }

        public StringResult AddressLine1 { get; private set; } 
        public StringResult AddressLine2 { get; private set; } 
        public StringResult City { get; private set; } 
        public StringResult PostalCode { get; private set; } 
        public StringResult SpatialLocation { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
