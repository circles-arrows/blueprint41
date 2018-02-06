using System;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static ShiftNode Shift { get { return new ShiftNode(); } }
	}

	public partial class ShiftNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Shift";
            }
        }

		internal ShiftNode() { }
		internal ShiftNode(ShiftAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal ShiftNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public ShiftNode Alias(out ShiftAlias alias)
		{
			alias = new ShiftAlias(this);
            NodeAlias = alias;
			return this;
		}


		public ShiftOut Out { get { return new ShiftOut(this); } }
		public class ShiftOut
		{
			private ShiftNode Parent;
			internal ShiftOut(ShiftNode parent)
			{
                Parent = parent;
			}
			public IFromOut_EMPLOYEE_HAS_SHIFT_REL EMPLOYEE_HAS_SHIFT { get { return new EMPLOYEE_HAS_SHIFT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT { get { return new EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class ShiftAlias : AliasResult
    {
        internal ShiftAlias(ShiftNode parent)
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
						{ "Name", new StringResult(this, "Name", Datastore.AdventureWorks.Model.Entities["Shift"], Datastore.AdventureWorks.Model.Entities["Shift"].Properties["Name"]) },
						{ "StartTime", new DateTimeResult(this, "StartTime", Datastore.AdventureWorks.Model.Entities["Shift"], Datastore.AdventureWorks.Model.Entities["Shift"].Properties["StartTime"]) },
						{ "EndTime", new DateTimeResult(this, "EndTime", Datastore.AdventureWorks.Model.Entities["Shift"], Datastore.AdventureWorks.Model.Entities["Shift"].Properties["EndTime"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Shift"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Shift"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public ShiftNode.ShiftOut Out { get { return new ShiftNode.ShiftOut(new ShiftNode(this, true)); } }

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
        public DateTimeResult StartTime
		{
			get
			{
				if ((object)m_StartTime == null)
					m_StartTime = (DateTimeResult)AliasFields["StartTime"];

				return m_StartTime;
			}
		} 
        private DateTimeResult m_StartTime = null;
        public DateTimeResult EndTime
		{
			get
			{
				if ((object)m_EndTime == null)
					m_EndTime = (DateTimeResult)AliasFields["EndTime"];

				return m_EndTime;
			}
		} 
        private DateTimeResult m_EndTime = null;
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
