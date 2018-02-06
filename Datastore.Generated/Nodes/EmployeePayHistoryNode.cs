using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static EmployeePayHistoryNode EmployeePayHistory { get { return new EmployeePayHistoryNode(); } }
	}

	public partial class EmployeePayHistoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "EmployeePayHistory";
            }
        }

		internal EmployeePayHistoryNode() { }
		internal EmployeePayHistoryNode(EmployeePayHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal EmployeePayHistoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public EmployeePayHistoryNode Alias(out EmployeePayHistoryAlias alias)
		{
			alias = new EmployeePayHistoryAlias(this);
            NodeAlias = alias;
			return this;
		}


		public EmployeePayHistoryOut Out { get { return new EmployeePayHistoryOut(this); } }
		public class EmployeePayHistoryOut
		{
			private EmployeePayHistoryNode Parent;
			internal EmployeePayHistoryOut(EmployeePayHistoryNode parent)
			{
                Parent = parent;
			}
			public IFromOut_EMPLOYEE_HAS_EMPLOYEEPAYHISTORY_REL EMPLOYEE_HAS_EMPLOYEEPAYHISTORY { get { return new EMPLOYEE_HAS_EMPLOYEEPAYHISTORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class EmployeePayHistoryAlias : AliasResult
    {
        internal EmployeePayHistoryAlias(EmployeePayHistoryNode parent)
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
						{ "RateChangeDate", new DateTimeResult(this, "RateChangeDate", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["RateChangeDate"]) },
						{ "Rate", new StringResult(this, "Rate", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["Rate"]) },
						{ "PayFrequency", new StringResult(this, "PayFrequency", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["PayFrequency"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public EmployeePayHistoryNode.EmployeePayHistoryOut Out { get { return new EmployeePayHistoryNode.EmployeePayHistoryOut(new EmployeePayHistoryNode(this, true)); } }

        public DateTimeResult RateChangeDate
		{
			get
			{
				if ((object)m_RateChangeDate == null)
					m_RateChangeDate = (DateTimeResult)AliasFields["RateChangeDate"];

				return m_RateChangeDate;
			}
		} 
        private DateTimeResult m_RateChangeDate = null;
        public StringResult Rate
		{
			get
			{
				if ((object)m_Rate == null)
					m_Rate = (StringResult)AliasFields["Rate"];

				return m_Rate;
			}
		} 
        private StringResult m_Rate = null;
        public StringResult PayFrequency
		{
			get
			{
				if ((object)m_PayFrequency == null)
					m_PayFrequency = (StringResult)AliasFields["PayFrequency"];

				return m_PayFrequency;
			}
		} 
        private StringResult m_PayFrequency = null;
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
