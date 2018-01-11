using System;
using Blueprint41;
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
            RateChangeDate = new DateTimeResult(this, "RateChangeDate", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["RateChangeDate"]);
            Rate = new StringResult(this, "Rate", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["Rate"]);
            PayFrequency = new StringResult(this, "PayFrequency", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"].Properties["PayFrequency"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public EmployeePayHistoryNode.EmployeePayHistoryOut Out { get { return new EmployeePayHistoryNode.EmployeePayHistoryOut(new EmployeePayHistoryNode(this, true)); } }

        public DateTimeResult RateChangeDate { get; private set; } 
        public StringResult Rate { get; private set; } 
        public StringResult PayFrequency { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
