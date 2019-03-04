using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static DepartmentNode Department { get { return new DepartmentNode(); } }
	}

	public partial class DepartmentNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Department";
        }

		internal DepartmentNode() { }
		internal DepartmentNode(DepartmentAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal DepartmentNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public DepartmentNode Alias(out DepartmentAlias alias)
		{
			alias = new DepartmentAlias(this);
            NodeAlias = alias;
			return this;
		}

		public DepartmentNode UseExistingAlias(AliasResult alias)
        {
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["Department"].Properties["Name"]) },
						{ "GroupName", new StringResult(this, "GroupName", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["Department"].Properties["GroupName"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Department"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public DepartmentNode.DepartmentIn In { get { return new DepartmentNode.DepartmentIn(new DepartmentNode(this, true)); } }
        public DepartmentNode.DepartmentOut Out { get { return new DepartmentNode.DepartmentOut(new DepartmentNode(this, true)); } }

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
        public StringResult GroupName
		{
			get
			{
				if ((object)m_GroupName == null)
					m_GroupName = (StringResult)AliasFields["GroupName"];

				return m_GroupName;
			}
		} 
        private StringResult m_GroupName = null;
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
