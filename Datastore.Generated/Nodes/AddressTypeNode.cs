using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static AddressTypeNode AddressType { get { return new AddressTypeNode(); } }
	}

	public partial class AddressTypeNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "AddressType";
            }
        }

		internal AddressTypeNode() { }
		internal AddressTypeNode(AddressTypeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal AddressTypeNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public AddressTypeNode Alias(out AddressTypeAlias alias)
		{
			alias = new AddressTypeAlias(this);
            NodeAlias = alias;
			return this;
		}


		public AddressTypeOut Out { get { return new AddressTypeOut(this); } }
		public class AddressTypeOut
		{
			private AddressTypeNode Parent;
			internal AddressTypeOut(AddressTypeNode parent)
			{
                Parent = parent;
			}
			public IFromOut_ADDRESS_HAS_ADDRESSTYPE_REL ADDRESS_HAS_ADDRESSTYPE { get { return new ADDRESS_HAS_ADDRESSTYPE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class AddressTypeAlias : AliasResult
    {
        internal AddressTypeAlias(AddressTypeNode parent)
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["AddressType"], Datastore.AdventureWorks.Model.Entities["AddressType"].Properties["Name"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["AddressType"], Datastore.AdventureWorks.Model.Entities["AddressType"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["AddressType"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["AddressType"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public AddressTypeNode.AddressTypeOut Out { get { return new AddressTypeNode.AddressTypeOut(new AddressTypeNode(this, true)); } }

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
