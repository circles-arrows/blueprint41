using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Query;

using m = Domain.Data.Manipulation;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static EmployeeNode Employee { get { return new EmployeeNode(); } }
	}

	public partial class EmployeeNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(EmployeeNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(EmployeeNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Employee";
		}

		protected override Entity GetEntity()
        {
			return m.Employee.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Employee.Entity.FunctionalId;
            }
        }

		internal EmployeeNode() { }
		internal EmployeeNode(EmployeeAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal EmployeeNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal EmployeeNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public EmployeeNode Where(JsNotation<System.DateTime> BirthDate = default, JsNotation<bool> Currentflag = default, JsNotation<string> Gender = default, JsNotation<System.DateTime> HireDate = default, JsNotation<string> JobTitle = default, JsNotation<int> LoginID = default, JsNotation<string> MaritalStatus = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> NationalIDNumber = default, JsNotation<string> rowguid = default, JsNotation<bool> SalariedFlag = default, JsNotation<int> SickLeaveHours = default, JsNotation<string> Uid = default, JsNotation<int> VacationHours = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmployeeAlias> alias = new Lazy<EmployeeAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (BirthDate.HasValue) conditions.Add(new QueryCondition(alias.Value.BirthDate, Operator.Equals, ((IValue)BirthDate).GetValue()));
            if (Currentflag.HasValue) conditions.Add(new QueryCondition(alias.Value.Currentflag, Operator.Equals, ((IValue)Currentflag).GetValue()));
            if (Gender.HasValue) conditions.Add(new QueryCondition(alias.Value.Gender, Operator.Equals, ((IValue)Gender).GetValue()));
            if (HireDate.HasValue) conditions.Add(new QueryCondition(alias.Value.HireDate, Operator.Equals, ((IValue)HireDate).GetValue()));
            if (JobTitle.HasValue) conditions.Add(new QueryCondition(alias.Value.JobTitle, Operator.Equals, ((IValue)JobTitle).GetValue()));
            if (LoginID.HasValue) conditions.Add(new QueryCondition(alias.Value.LoginID, Operator.Equals, ((IValue)LoginID).GetValue()));
            if (MaritalStatus.HasValue) conditions.Add(new QueryCondition(alias.Value.MaritalStatus, Operator.Equals, ((IValue)MaritalStatus).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (NationalIDNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.NationalIDNumber, Operator.Equals, ((IValue)NationalIDNumber).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (SalariedFlag.HasValue) conditions.Add(new QueryCondition(alias.Value.SalariedFlag, Operator.Equals, ((IValue)SalariedFlag).GetValue()));
            if (SickLeaveHours.HasValue) conditions.Add(new QueryCondition(alias.Value.SickLeaveHours, Operator.Equals, ((IValue)SickLeaveHours).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));
            if (VacationHours.HasValue) conditions.Add(new QueryCondition(alias.Value.VacationHours, Operator.Equals, ((IValue)VacationHours).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public EmployeeNode Assign(JsNotation<System.DateTime> BirthDate = default, JsNotation<bool> Currentflag = default, JsNotation<string> Gender = default, JsNotation<System.DateTime> HireDate = default, JsNotation<string> JobTitle = default, JsNotation<int> LoginID = default, JsNotation<string> MaritalStatus = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> NationalIDNumber = default, JsNotation<string> rowguid = default, JsNotation<bool> SalariedFlag = default, JsNotation<int> SickLeaveHours = default, JsNotation<string> Uid = default, JsNotation<int> VacationHours = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<EmployeeAlias> alias = new Lazy<EmployeeAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (BirthDate.HasValue) assignments.Add(new Assignment(alias.Value.BirthDate, BirthDate));
            if (Currentflag.HasValue) assignments.Add(new Assignment(alias.Value.Currentflag, Currentflag));
            if (Gender.HasValue) assignments.Add(new Assignment(alias.Value.Gender, Gender));
            if (HireDate.HasValue) assignments.Add(new Assignment(alias.Value.HireDate, HireDate));
            if (JobTitle.HasValue) assignments.Add(new Assignment(alias.Value.JobTitle, JobTitle));
            if (LoginID.HasValue) assignments.Add(new Assignment(alias.Value.LoginID, LoginID));
            if (MaritalStatus.HasValue) assignments.Add(new Assignment(alias.Value.MaritalStatus, MaritalStatus));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (NationalIDNumber.HasValue) assignments.Add(new Assignment(alias.Value.NationalIDNumber, NationalIDNumber));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (SalariedFlag.HasValue) assignments.Add(new Assignment(alias.Value.SalariedFlag, SalariedFlag));
            if (SickLeaveHours.HasValue) assignments.Add(new Assignment(alias.Value.SickLeaveHours, SickLeaveHours));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));
            if (VacationHours.HasValue) assignments.Add(new Assignment(alias.Value.VacationHours, VacationHours));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public EmployeeNode Alias(out EmployeeAlias alias)
        {
            if (NodeAlias is EmployeeAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new EmployeeAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public EmployeeNode Alias(out EmployeeAlias alias, string name)
        {
            if (NodeAlias is EmployeeAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new EmployeeAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public EmployeeNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
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

	public class EmployeeAlias : AliasResult<EmployeeAlias, EmployeeListAlias>
	{
		internal EmployeeAlias(EmployeeNode parent)
		{
			Node = parent;
		}
		internal EmployeeAlias(EmployeeNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  EmployeeAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  EmployeeAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  EmployeeAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<System.DateTime> BirthDate = default, JsNotation<bool> Currentflag = default, JsNotation<string> Gender = default, JsNotation<System.DateTime> HireDate = default, JsNotation<string> JobTitle = default, JsNotation<int> LoginID = default, JsNotation<string> MaritalStatus = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> NationalIDNumber = default, JsNotation<string> rowguid = default, JsNotation<bool> SalariedFlag = default, JsNotation<int> SickLeaveHours = default, JsNotation<string> Uid = default, JsNotation<int> VacationHours = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (BirthDate.HasValue) assignments.Add(new Assignment(this.BirthDate, BirthDate));
			if (Currentflag.HasValue) assignments.Add(new Assignment(this.Currentflag, Currentflag));
			if (Gender.HasValue) assignments.Add(new Assignment(this.Gender, Gender));
			if (HireDate.HasValue) assignments.Add(new Assignment(this.HireDate, HireDate));
			if (JobTitle.HasValue) assignments.Add(new Assignment(this.JobTitle, JobTitle));
			if (LoginID.HasValue) assignments.Add(new Assignment(this.LoginID, LoginID));
			if (MaritalStatus.HasValue) assignments.Add(new Assignment(this.MaritalStatus, MaritalStatus));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (NationalIDNumber.HasValue) assignments.Add(new Assignment(this.NationalIDNumber, NationalIDNumber));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (SalariedFlag.HasValue) assignments.Add(new Assignment(this.SalariedFlag, SalariedFlag));
			if (SickLeaveHours.HasValue) assignments.Add(new Assignment(this.SickLeaveHours, SickLeaveHours));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
			if (VacationHours.HasValue) assignments.Add(new Assignment(this.VacationHours, VacationHours));
            
            return assignments.ToArray();
        }


		public override IReadOnlyDictionary<string, FieldResult> AliasFields
		{
			get
			{
				if (m_AliasFields is null)
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
				if (m_NationalIDNumber is null)
					m_NationalIDNumber = (StringResult)AliasFields["NationalIDNumber"];

				return m_NationalIDNumber;
			}
		}
		private StringResult m_NationalIDNumber = null;
		public NumericResult LoginID
		{
			get
			{
				if (m_LoginID is null)
					m_LoginID = (NumericResult)AliasFields["LoginID"];

				return m_LoginID;
			}
		}
		private NumericResult m_LoginID = null;
		public StringResult JobTitle
		{
			get
			{
				if (m_JobTitle is null)
					m_JobTitle = (StringResult)AliasFields["JobTitle"];

				return m_JobTitle;
			}
		}
		private StringResult m_JobTitle = null;
		public DateTimeResult BirthDate
		{
			get
			{
				if (m_BirthDate is null)
					m_BirthDate = (DateTimeResult)AliasFields["BirthDate"];

				return m_BirthDate;
			}
		}
		private DateTimeResult m_BirthDate = null;
		public StringResult MaritalStatus
		{
			get
			{
				if (m_MaritalStatus is null)
					m_MaritalStatus = (StringResult)AliasFields["MaritalStatus"];

				return m_MaritalStatus;
			}
		}
		private StringResult m_MaritalStatus = null;
		public StringResult Gender
		{
			get
			{
				if (m_Gender is null)
					m_Gender = (StringResult)AliasFields["Gender"];

				return m_Gender;
			}
		}
		private StringResult m_Gender = null;
		public DateTimeResult HireDate
		{
			get
			{
				if (m_HireDate is null)
					m_HireDate = (DateTimeResult)AliasFields["HireDate"];

				return m_HireDate;
			}
		}
		private DateTimeResult m_HireDate = null;
		public BooleanResult SalariedFlag
		{
			get
			{
				if (m_SalariedFlag is null)
					m_SalariedFlag = (BooleanResult)AliasFields["SalariedFlag"];

				return m_SalariedFlag;
			}
		}
		private BooleanResult m_SalariedFlag = null;
		public NumericResult VacationHours
		{
			get
			{
				if (m_VacationHours is null)
					m_VacationHours = (NumericResult)AliasFields["VacationHours"];

				return m_VacationHours;
			}
		}
		private NumericResult m_VacationHours = null;
		public NumericResult SickLeaveHours
		{
			get
			{
				if (m_SickLeaveHours is null)
					m_SickLeaveHours = (NumericResult)AliasFields["SickLeaveHours"];

				return m_SickLeaveHours;
			}
		}
		private NumericResult m_SickLeaveHours = null;
		public BooleanResult Currentflag
		{
			get
			{
				if (m_Currentflag is null)
					m_Currentflag = (BooleanResult)AliasFields["Currentflag"];

				return m_Currentflag;
			}
		}
		private BooleanResult m_Currentflag = null;
		public StringResult rowguid
		{
			get
			{
				if (m_rowguid is null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		}
		private StringResult m_rowguid = null;
		public DateTimeResult ModifiedDate
		{
			get
			{
				if (m_ModifiedDate is null)
					m_ModifiedDate = (DateTimeResult)AliasFields["ModifiedDate"];

				return m_ModifiedDate;
			}
		}
		private DateTimeResult m_ModifiedDate = null;
		public StringResult Uid
		{
			get
			{
				if (m_Uid is null)
					m_Uid = (StringResult)AliasFields["Uid"];

				return m_Uid;
			}
		}
		private StringResult m_Uid = null;
		public AsResult As(string aliasName, out EmployeeAlias alias)
		{
			alias = new EmployeeAlias((EmployeeNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class EmployeeListAlias : ListResult<EmployeeListAlias, EmployeeAlias>, IAliasListResult
	{
		private EmployeeListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmployeeListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmployeeListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class EmployeeJaggedListAlias : ListResult<EmployeeJaggedListAlias, EmployeeListAlias>, IAliasJaggedListResult
	{
		private EmployeeJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private EmployeeJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private EmployeeJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
