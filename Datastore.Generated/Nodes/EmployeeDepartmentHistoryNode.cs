using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static EmployeeDepartmentHistoryNode EmployeeDepartmentHistory { get { return new EmployeeDepartmentHistoryNode(); } }
	}

	public partial class EmployeeDepartmentHistoryNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "EmployeeDepartmentHistory";
            }
        }

		internal EmployeeDepartmentHistoryNode() { }
		internal EmployeeDepartmentHistoryNode(EmployeeDepartmentHistoryAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal EmployeeDepartmentHistoryNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public EmployeeDepartmentHistoryNode Alias(out EmployeeDepartmentHistoryAlias alias)
		{
			alias = new EmployeeDepartmentHistoryAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public EmployeeDepartmentHistoryIn  In  { get { return new EmployeeDepartmentHistoryIn(this); } }
		public class EmployeeDepartmentHistoryIn
		{
			private EmployeeDepartmentHistoryNode Parent;
			internal EmployeeDepartmentHistoryIn(EmployeeDepartmentHistoryNode parent)
			{
                Parent = parent;
			}
			public IFromIn_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT { get { return new EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT_REL EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT { get { return new EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT_REL(Parent, DirectionEnum.In); } }

		}

		public EmployeeDepartmentHistoryOut Out { get { return new EmployeeDepartmentHistoryOut(this); } }
		public class EmployeeDepartmentHistoryOut
		{
			private EmployeeDepartmentHistoryNode Parent;
			internal EmployeeDepartmentHistoryOut(EmployeeDepartmentHistoryNode parent)
			{
                Parent = parent;
			}
			public IFromOut_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY { get { return new EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class EmployeeDepartmentHistoryAlias : AliasResult
    {
        internal EmployeeDepartmentHistoryAlias(EmployeeDepartmentHistoryNode parent)
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
						{ "StartDate", new DateTimeResult(this, "StartDate", Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"], Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"].Properties["StartDate"]) },
						{ "EndDate", new StringResult(this, "EndDate", Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"], Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"].Properties["EndDate"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public EmployeeDepartmentHistoryNode.EmployeeDepartmentHistoryIn In { get { return new EmployeeDepartmentHistoryNode.EmployeeDepartmentHistoryIn(new EmployeeDepartmentHistoryNode(this, true)); } }
        public EmployeeDepartmentHistoryNode.EmployeeDepartmentHistoryOut Out { get { return new EmployeeDepartmentHistoryNode.EmployeeDepartmentHistoryOut(new EmployeeDepartmentHistoryNode(this, true)); } }

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
        public StringResult EndDate
		{
			get
			{
				if ((object)m_EndDate == null)
					m_EndDate = (StringResult)AliasFields["EndDate"];

				return m_EndDate;
			}
		} 
        private StringResult m_EndDate = null;
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
