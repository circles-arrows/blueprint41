using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ScrapReasonNode ScrapReason { get { return new ScrapReasonNode(); } }
	}

	public partial class ScrapReasonNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "ScrapReason";
            }
        }

		internal ScrapReasonNode() { }
		internal ScrapReasonNode(ScrapReasonAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ScrapReasonNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ScrapReasonNode Alias(out ScrapReasonAlias alias)
		{
			alias = new ScrapReasonAlias(this);
            NodeAlias = alias;
			return this;
		}


		public ScrapReasonOut Out { get { return new ScrapReasonOut(this); } }
		public class ScrapReasonOut
		{
			private ScrapReasonNode Parent;
			internal ScrapReasonOut(ScrapReasonNode parent)
			{
                Parent = parent;
			}
			public IFromOut_WORKORDER_HAS_SCRAPREASON_REL WORKORDER_HAS_SCRAPREASON { get { return new WORKORDER_HAS_SCRAPREASON_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ScrapReasonAlias : AliasResult
    {
        internal ScrapReasonAlias(ScrapReasonNode parent)
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["ScrapReason"], Datastore.AdventureWorks.Model.Entities["ScrapReason"].Properties["Name"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["ScrapReason"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["ScrapReason"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ScrapReasonNode.ScrapReasonOut Out { get { return new ScrapReasonNode.ScrapReasonOut(new ScrapReasonNode(this, true)); } }

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
