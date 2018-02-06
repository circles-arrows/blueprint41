using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "AddressLine1", new StringResult(this, "AddressLine1", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["AddressLine1"]) },
						{ "AddressLine2", new StringResult(this, "AddressLine2", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["AddressLine2"]) },
						{ "City", new StringResult(this, "City", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["City"]) },
						{ "PostalCode", new StringResult(this, "PostalCode", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["PostalCode"]) },
						{ "SpatialLocation", new StringResult(this, "SpatialLocation", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["SpatialLocation"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Address"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Address"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public AddressNode.AddressIn In { get { return new AddressNode.AddressIn(new AddressNode(this, true)); } }
        public AddressNode.AddressOut Out { get { return new AddressNode.AddressOut(new AddressNode(this, true)); } }

        public StringResult AddressLine1
		{
			get
			{
				if ((object)m_AddressLine1 == null)
					m_AddressLine1 = (StringResult)AliasFields["AddressLine1"];

				return m_AddressLine1;
			}
		} 
        private StringResult m_AddressLine1 = null;
        public StringResult AddressLine2
		{
			get
			{
				if ((object)m_AddressLine2 == null)
					m_AddressLine2 = (StringResult)AliasFields["AddressLine2"];

				return m_AddressLine2;
			}
		} 
        private StringResult m_AddressLine2 = null;
        public StringResult City
		{
			get
			{
				if ((object)m_City == null)
					m_City = (StringResult)AliasFields["City"];

				return m_City;
			}
		} 
        private StringResult m_City = null;
        public StringResult PostalCode
		{
			get
			{
				if ((object)m_PostalCode == null)
					m_PostalCode = (StringResult)AliasFields["PostalCode"];

				return m_PostalCode;
			}
		} 
        private StringResult m_PostalCode = null;
        public StringResult SpatialLocation
		{
			get
			{
				if ((object)m_SpatialLocation == null)
					m_SpatialLocation = (StringResult)AliasFields["SpatialLocation"];

				return m_SpatialLocation;
			}
		} 
        private StringResult m_SpatialLocation = null;
        public StringResult rowguid
		{
			get
			{
				if ((object)m_rowguid == null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		} 
        private StringResult m_rowguid = null;
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
