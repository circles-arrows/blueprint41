
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static LocationNode Location { get { return new LocationNode(); } }
	}

	public partial class LocationNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Location";
            }
        }

		internal LocationNode() { }
		internal LocationNode(LocationAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal LocationNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public LocationNode Alias(out LocationAlias alias)
		{
			alias = new LocationAlias(this);
            NodeAlias = alias;
			return this;
		}


		public LocationOut Out { get { return new LocationOut(this); } }
		public class LocationOut
		{
			private LocationNode Parent;
			internal LocationOut(LocationNode parent)
			{
                Parent = parent;
			}
			public IFromOut_PRODUCTINVENTORY_HAS_LOCATION_REL PRODUCTINVENTORY_HAS_LOCATION { get { return new PRODUCTINVENTORY_HAS_LOCATION_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_WORKORDERROUTING_HAS_LOCATION_REL WORKORDERROUTING_HAS_LOCATION { get { return new WORKORDERROUTING_HAS_LOCATION_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class LocationAlias : AliasResult
    {
        internal LocationAlias(LocationNode parent)
        {
			Node = parent;
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Location"].Properties["Name"]);
            CostRate = new StringResult(this, "CostRate", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Location"].Properties["CostRate"]);
            Availability = new StringResult(this, "Availability", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Location"].Properties["Availability"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public LocationNode.LocationOut Out { get { return new LocationNode.LocationOut(new LocationNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public StringResult CostRate { get; private set; } 
        public StringResult Availability { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
