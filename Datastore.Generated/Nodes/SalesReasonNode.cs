using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static SalesReasonNode SalesReason { get { return new SalesReasonNode(); } }
	}

	public partial class SalesReasonNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "SalesReason";
        }

		internal SalesReasonNode() { }
		internal SalesReasonNode(SalesReasonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal SalesReasonNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public SalesReasonNode Alias(out SalesReasonAlias alias)
		{
			alias = new SalesReasonAlias(this);
            NodeAlias = alias;
			return this;
		}

		public SalesReasonNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
			return this;
        }


		public SalesReasonOut Out { get { return new SalesReasonOut(this); } }
		public class SalesReasonOut
		{
			private SalesReasonNode Parent;
			internal SalesReasonOut(SalesReasonNode parent)
			{
                Parent = parent;
			}
			public IFromOut_SALESORDERHEADER_HAS_SALESREASON_REL SALESORDERHEADER_HAS_SALESREASON { get { return new SALESORDERHEADER_HAS_SALESREASON_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class SalesReasonAlias : AliasResult
    {
        internal SalesReasonAlias(SalesReasonNode parent)
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["SalesReason"].Properties["Name"]) },
						{ "ReasonType", new StringResult(this, "ReasonType", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["SalesReason"].Properties["ReasonType"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["SalesReason"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public SalesReasonNode.SalesReasonOut Out { get { return new SalesReasonNode.SalesReasonOut(new SalesReasonNode(this, true)); } }

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
        public StringResult ReasonType
		{
			get
			{
				if ((object)m_ReasonType == null)
					m_ReasonType = (StringResult)AliasFields["ReasonType"];

				return m_ReasonType;
			}
		} 
        private StringResult m_ReasonType = null;
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
