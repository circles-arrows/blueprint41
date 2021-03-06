using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static StoreNode Store { get { return new StoreNode(); } }
	}

	public partial class StoreNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Store";
        }

		internal StoreNode() { }
		internal StoreNode(StoreAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal StoreNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public StoreNode Alias(out StoreAlias alias)
		{
			alias = new StoreAlias(this);
            NodeAlias = alias;
			return this;
		}

		public StoreNode UseExistingAlias(AliasResult alias)
        {
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Store"].Properties["Name"]) },
						{ "Demographics", new StringResult(this, "Demographics", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Store"].Properties["Demographics"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Store"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Store"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public StoreNode.StoreIn In { get { return new StoreNode.StoreIn(new StoreNode(this, true)); } }
        public StoreNode.StoreOut Out { get { return new StoreNode.StoreOut(new StoreNode(this, true)); } }

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
        public StringResult Demographics
		{
			get
			{
				if ((object)m_Demographics == null)
					m_Demographics = (StringResult)AliasFields["Demographics"];

				return m_Demographics;
			}
		} 
        private StringResult m_Demographics = null;
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
