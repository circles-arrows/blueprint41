using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static EmployeeNode Employee { get { return new EmployeeNode(); } }
	}

	public partial class EmployeeNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Employee";
            }
        }

		internal EmployeeNode() { }
		internal EmployeeNode(EmployeeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal EmployeeNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public EmployeeNode Alias(out EmployeeAlias alias)
		{
			alias = new EmployeeAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public EmployeeIn  In  { get { return new EmployeeIn(this); } }
		public class EmployeeIn
		{
			private EmployeeNode Parent;
			internal EmployeeIn(EmployeeNode parent)
			{
                Parent = parent;
			}
			public IFromIn_EMPLOYEE_BECOMES_SALESPERSON_REL EMPLOYEE_BECOMES_SALESPERSON { get { return new EMPLOYEE_BECOMES_SALESPERSON_REL(Parent, DirectionEnum.In); } }
			public IFromIn_EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY { get { return new EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_EMPLOYEE_HAS_EMPLOYEEPAYHISTORY_REL EMPLOYEE_HAS_EMPLOYEEPAYHISTORY { get { return new EMPLOYEE_HAS_EMPLOYEEPAYHISTORY_REL(Parent, DirectionEnum.In); } }
			public IFromIn_EMPLOYEE_HAS_SHIFT_REL EMPLOYEE_HAS_SHIFT { get { return new EMPLOYEE_HAS_SHIFT_REL(Parent, DirectionEnum.In); } }
			public IFromIn_EMPLOYEE_IS_JOBCANDIDATE_REL EMPLOYEE_IS_JOBCANDIDATE { get { return new EMPLOYEE_IS_JOBCANDIDATE_REL(Parent, DirectionEnum.In); } }

		}

		public EmployeeOut Out { get { return new EmployeeOut(this); } }
		public class EmployeeOut
		{
			private EmployeeNode Parent;
			internal EmployeeOut(EmployeeNode parent)
			{
                Parent = parent;
			}
			public IFromOut_DEPARTMENT_CONTAINS_EMPLOYEE_REL DEPARTMENT_CONTAINS_EMPLOYEE { get { return new DEPARTMENT_CONTAINS_EMPLOYEE_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PERSON_BECOMES_EMPLOYEE_REL PERSON_BECOMES_EMPLOYEE { get { return new PERSON_BECOMES_EMPLOYEE_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_VENDOR_VALID_FOR_EMPLOYEE_REL VENDOR_VALID_FOR_EMPLOYEE { get { return new VENDOR_VALID_FOR_EMPLOYEE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class EmployeeAlias : AliasResult
    {
        internal EmployeeAlias(EmployeeNode parent)
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
						{ "NationalIDNumber", new StringResult(this, "NationalIDNumber", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["NationalIDNumber"]) },
						{ "LoginID", new NumericResult(this, "LoginID", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["LoginID"]) },
						{ "JobTitle", new StringResult(this, "JobTitle", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["JobTitle"]) },
						{ "BirthDate", new DateTimeResult(this, "BirthDate", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["BirthDate"]) },
						{ "MaritalStatus", new StringResult(this, "MaritalStatus", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["MaritalStatus"]) },
						{ "Gender", new StringResult(this, "Gender", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["Gender"]) },
						{ "HireDate", new DateTimeResult(this, "HireDate", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["HireDate"]) },
						{ "SalariedFlag", new BooleanResult(this, "SalariedFlag", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["SalariedFlag"]) },
						{ "VacationHours", new NumericResult(this, "VacationHours", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["VacationHours"]) },
						{ "SickLeaveHours", new NumericResult(this, "SickLeaveHours", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["SickLeaveHours"]) },
						{ "Currentflag", new BooleanResult(this, "Currentflag", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["Currentflag"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public EmployeeNode.EmployeeIn In { get { return new EmployeeNode.EmployeeIn(new EmployeeNode(this, true)); } }
        public EmployeeNode.EmployeeOut Out { get { return new EmployeeNode.EmployeeOut(new EmployeeNode(this, true)); } }

        public StringResult NationalIDNumber
		{
			get
			{
				if ((object)m_NationalIDNumber == null)
					m_NationalIDNumber = (StringResult)AliasFields["NationalIDNumber"];

				return m_NationalIDNumber;
			}
		} 
        private StringResult m_NationalIDNumber = null;
        public NumericResult LoginID
		{
			get
			{
				if ((object)m_LoginID == null)
					m_LoginID = (NumericResult)AliasFields["LoginID"];

				return m_LoginID;
			}
		} 
        private NumericResult m_LoginID = null;
        public StringResult JobTitle
		{
			get
			{
				if ((object)m_JobTitle == null)
					m_JobTitle = (StringResult)AliasFields["JobTitle"];

				return m_JobTitle;
			}
		} 
        private StringResult m_JobTitle = null;
        public DateTimeResult BirthDate
		{
			get
			{
				if ((object)m_BirthDate == null)
					m_BirthDate = (DateTimeResult)AliasFields["BirthDate"];

				return m_BirthDate;
			}
		} 
        private DateTimeResult m_BirthDate = null;
        public StringResult MaritalStatus
		{
			get
			{
				if ((object)m_MaritalStatus == null)
					m_MaritalStatus = (StringResult)AliasFields["MaritalStatus"];

				return m_MaritalStatus;
			}
		} 
        private StringResult m_MaritalStatus = null;
        public StringResult Gender
		{
			get
			{
				if ((object)m_Gender == null)
					m_Gender = (StringResult)AliasFields["Gender"];

				return m_Gender;
			}
		} 
        private StringResult m_Gender = null;
        public DateTimeResult HireDate
		{
			get
			{
				if ((object)m_HireDate == null)
					m_HireDate = (DateTimeResult)AliasFields["HireDate"];

				return m_HireDate;
			}
		} 
        private DateTimeResult m_HireDate = null;
        public BooleanResult SalariedFlag
		{
			get
			{
				if ((object)m_SalariedFlag == null)
					m_SalariedFlag = (BooleanResult)AliasFields["SalariedFlag"];

				return m_SalariedFlag;
			}
		} 
        private BooleanResult m_SalariedFlag = null;
        public NumericResult VacationHours
		{
			get
			{
				if ((object)m_VacationHours == null)
					m_VacationHours = (NumericResult)AliasFields["VacationHours"];

				return m_VacationHours;
			}
		} 
        private NumericResult m_VacationHours = null;
        public NumericResult SickLeaveHours
		{
			get
			{
				if ((object)m_SickLeaveHours == null)
					m_SickLeaveHours = (NumericResult)AliasFields["SickLeaveHours"];

				return m_SickLeaveHours;
			}
		} 
        private NumericResult m_SickLeaveHours = null;
        public BooleanResult Currentflag
		{
			get
			{
				if ((object)m_Currentflag == null)
					m_Currentflag = (BooleanResult)AliasFields["Currentflag"];

				return m_Currentflag;
			}
		} 
        private BooleanResult m_Currentflag = null;
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
