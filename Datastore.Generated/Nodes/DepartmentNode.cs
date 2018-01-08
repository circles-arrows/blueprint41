
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static DepartmentNode Department { get { return new DepartmentNode(); } }
	}

	public partial class DepartmentNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Department";
            }
        }

		internal DepartmentNode() { }
		internal DepartmentNode(DepartmentAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal DepartmentNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public DepartmentNode Alias(out DepartmentAlias alias)
		{
			alias = new DepartmentAlias(this);
            NodeAlias = alias;
			return this;
		}

	
		public DepartmentIn  In  { get { return new DepartmentIn(this); } }
		public class DepartmentIn
		{
			private DepartmentNode Parent;
			internal DepartmentIn(DepartmentNode parent)
			{
                Parent = parent;
			}
			public IFromIn_DEPARTMENT_CONTAINS_EMPLOYEE_REL DEPARTMENT_CONTAINS_EMPLOYEE { get { return new DEPARTMENT_CONTAINS_EMPLOYEE_REL(Parent, DirectionEnum.In); } }

		}

		public DepartmentOut Out { get { return new DepartmentOut(this); } }
		public class DepartmentOut
		{
			private DepartmentNode Parent;
			internal DepartmentOut(DepartmentNode parent)
			{
                Parent = parent;
			}
			public IFromOut_EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT_REL EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT { get { return new EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class DepartmentAlias : AliasResult
    {
        internal DepartmentAlias(DepartmentNode parent)
        {
			Node = parent;
            Name = new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["Department"].Properties["Name"]);
            GroupName = new StringResult(this, "GroupName", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["Department"].Properties["GroupName"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public DepartmentNode.DepartmentIn In { get { return new DepartmentNode.DepartmentIn(new DepartmentNode(this, true)); } }
        public DepartmentNode.DepartmentOut Out { get { return new DepartmentNode.DepartmentOut(new DepartmentNode(this, true)); } }

        public StringResult Name { get; private set; } 
        public StringResult GroupName { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
