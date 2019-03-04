using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static LocationNode Location { get { return new LocationNode(); } }
	}

	public partial class LocationNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Location";
        }

		internal LocationNode() { }
		internal LocationNode(LocationAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal LocationNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public LocationNode Alias(out LocationAlias alias)
		{
			alias = new LocationAlias(this);
            NodeAlias = alias;
			return this;
		}

		public LocationNode UseExistingAlias(AliasResult alias)
        {
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Location"].Properties["Name"]) },
						{ "CostRate", new StringResult(this, "CostRate", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Location"].Properties["CostRate"]) },
						{ "Availability", new StringResult(this, "Availability", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Location"].Properties["Availability"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Location"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public LocationNode.LocationOut Out { get { return new LocationNode.LocationOut(new LocationNode(this, true)); } }

        public StringResult Name
		{
			get
			{
				if ((object)m_Name == null)
					m_Name = (StringResult)AliasFields["Name"];

				return m_Name;
			}
		} 
        private StringResult m_Name = null;
        public StringResult CostRate
		{
			get
			{
				if ((object)m_CostRate == null)
					m_CostRate = (StringResult)AliasFields["CostRate"];

				return m_CostRate;
			}
		} 
        private StringResult m_CostRate = null;
        public StringResult Availability
		{
			get
			{
				if ((object)m_Availability == null)
					m_Availability = (StringResult)AliasFields["Availability"];

				return m_Availability;
			}
		} 
        private StringResult m_Availability = null;
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
