using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static WorkOrderNode WorkOrder { get { return new WorkOrderNode(); } }
	}

	public partial class WorkOrderNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "WorkOrder";
            }
        }

		internal WorkOrderNode() { }
		internal WorkOrderNode(WorkOrderAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal WorkOrderNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public WorkOrderNode Alias(out WorkOrderAlias alias)
		{
			alias = new WorkOrderAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public WorkOrderIn  In  { get { return new WorkOrderIn(this); } }
		public class WorkOrderIn
		{
			private WorkOrderNode Parent;
			internal WorkOrderIn(WorkOrderNode parent)
			{
                Parent = parent;
			}
			public IFromIn_WORKORDER_HAS_PRODUCT_REL WORKORDER_HAS_PRODUCT { get { return new WORKORDER_HAS_PRODUCT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_WORKORDER_HAS_SCRAPREASON_REL WORKORDER_HAS_SCRAPREASON { get { return new WORKORDER_HAS_SCRAPREASON_REL(Parent, DirectionEnum.In); } }

		}

		public WorkOrderOut Out { get { return new WorkOrderOut(this); } }
		public class WorkOrderOut
		{
			private WorkOrderNode Parent;
			internal WorkOrderOut(WorkOrderNode parent)
			{
                Parent = parent;
			}
			public IFromOut_WORKORDERROUTING_HAS_WORKORDER_REL WORKORDERROUTING_HAS_WORKORDER { get { return new WORKORDERROUTING_HAS_WORKORDER_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class WorkOrderAlias : AliasResult
    {
        internal WorkOrderAlias(WorkOrderNode parent)
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
						{ "OrderQty", new NumericResult(this, "OrderQty", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["OrderQty"]) },
						{ "StockedQty", new NumericResult(this, "StockedQty", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["StockedQty"]) },
						{ "ScrappedQty", new NumericResult(this, "ScrappedQty", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["ScrappedQty"]) },
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["StartDate"]) },
						{ "EndDate", new DateTimeResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["EndDate"]) },
						{ "DueDate", new DateTimeResult(this, "DueDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["WorkOrder"].Properties["DueDate"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["WorkOrder"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public WorkOrderNode.WorkOrderIn In { get { return new WorkOrderNode.WorkOrderIn(new WorkOrderNode(this, true)); } }
        public WorkOrderNode.WorkOrderOut Out { get { return new WorkOrderNode.WorkOrderOut(new WorkOrderNode(this, true)); } }

        public NumericResult OrderQty
		{
			get
			{
				if ((object)m_OrderQty == null)
					m_OrderQty = (NumericResult)AliasFields["OrderQty"];

				return m_OrderQty;
			}
		} 
        private NumericResult m_OrderQty = null;
        public NumericResult StockedQty
		{
			get
			{
				if ((object)m_StockedQty == null)
					m_StockedQty = (NumericResult)AliasFields["StockedQty"];

				return m_StockedQty;
			}
		} 
        private NumericResult m_StockedQty = null;
        public NumericResult ScrappedQty
		{
			get
			{
				if ((object)m_ScrappedQty == null)
					m_ScrappedQty = (NumericResult)AliasFields["ScrappedQty"];

				return m_ScrappedQty;
			}
		} 
        private NumericResult m_ScrappedQty = null;
        public DateTimeResult StartDate
		{
			get
			{
				if ((object)m_StartDate == null)
					m_StartDate = (DateTimeResult)AliasFields["StartDate"];

				return m_StartDate;
			}
		} 
        private DateTimeResult m_StartDate = null;
        public DateTimeResult EndDate
		{
			get
			{
				if ((object)m_EndDate == null)
					m_EndDate = (DateTimeResult)AliasFields["EndDate"];

				return m_EndDate;
			}
		} 
        private DateTimeResult m_EndDate = null;
        public DateTimeResult DueDate
		{
			get
			{
				if ((object)m_DueDate == null)
					m_DueDate = (DateTimeResult)AliasFields["DueDate"];

				return m_DueDate;
			}
		} 
        private DateTimeResult m_DueDate = null;
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
