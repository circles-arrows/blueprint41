using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static CustomerNode Customer { get { return new CustomerNode(); } }
	}

	public partial class CustomerNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Customer";
        }

		internal CustomerNode() { }
		internal CustomerNode(CustomerAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal CustomerNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public CustomerNode Alias(out CustomerAlias alias)
		{
			alias = new CustomerAlias(this);
            NodeAlias = alias;
			return this;
		}

		public CustomerNode UseExistingAlias(AliasResult alias)
        {
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "AccountNumber", new StringResult(this, "AccountNumber", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["Customer"].Properties["AccountNumber"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["Customer"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Customer"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public CustomerNode.CustomerIn In { get { return new CustomerNode.CustomerIn(new CustomerNode(this, true)); } }

        public StringResult AccountNumber
		{
			get
			{
				if ((object)m_AccountNumber == null)
					m_AccountNumber = (StringResult)AliasFields["AccountNumber"];

				return m_AccountNumber;
			}
		} 
        private StringResult m_AccountNumber = null;
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
