using System;
using Blueprint41;
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
            NationalIDNumber = new StringResult(this, "NationalIDNumber", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["NationalIDNumber"]);
            LoginID = new NumericResult(this, "LoginID", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["LoginID"]);
            JobTitle = new StringResult(this, "JobTitle", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["JobTitle"]);
            BirthDate = new DateTimeResult(this, "BirthDate", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["BirthDate"]);
            MaritalStatus = new StringResult(this, "MaritalStatus", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["MaritalStatus"]);
            Gender = new StringResult(this, "Gender", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["Gender"]);
            HireDate = new StringResult(this, "HireDate", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["HireDate"]);
            SalariedFlag = new StringResult(this, "SalariedFlag", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["SalariedFlag"]);
            VacationHours = new StringResult(this, "VacationHours", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["VacationHours"]);
            SickLeaveHours = new StringResult(this, "SickLeaveHours", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["SickLeaveHours"]);
            Currentflag = new StringResult(this, "Currentflag", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["Currentflag"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Employee"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Employee"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public EmployeeNode.EmployeeIn In { get { return new EmployeeNode.EmployeeIn(new EmployeeNode(this, true)); } }
        public EmployeeNode.EmployeeOut Out { get { return new EmployeeNode.EmployeeOut(new EmployeeNode(this, true)); } }

        public StringResult NationalIDNumber { get; private set; } 
        public NumericResult LoginID { get; private set; } 
        public StringResult JobTitle { get; private set; } 
        public DateTimeResult BirthDate { get; private set; } 
        public StringResult MaritalStatus { get; private set; } 
        public StringResult Gender { get; private set; } 
        public StringResult HireDate { get; private set; } 
        public StringResult SalariedFlag { get; private set; } 
        public StringResult VacationHours { get; private set; } 
        public StringResult SickLeaveHours { get; private set; } 
        public StringResult Currentflag { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
