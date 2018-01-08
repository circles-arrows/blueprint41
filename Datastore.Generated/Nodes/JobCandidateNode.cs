
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static JobCandidateNode JobCandidate { get { return new JobCandidateNode(); } }
	}

	public partial class JobCandidateNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "JobCandidate";
            }
        }

		internal JobCandidateNode() { }
		internal JobCandidateNode(JobCandidateAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal JobCandidateNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public JobCandidateNode Alias(out JobCandidateAlias alias)
		{
			alias = new JobCandidateAlias(this);
            NodeAlias = alias;
			return this;
		}


		public JobCandidateOut Out { get { return new JobCandidateOut(this); } }
		public class JobCandidateOut
		{
			private JobCandidateNode Parent;
			internal JobCandidateOut(JobCandidateNode parent)
			{
                Parent = parent;
			}
			public IFromOut_EMPLOYEE_IS_JOBCANDIDATE_REL EMPLOYEE_IS_JOBCANDIDATE { get { return new EMPLOYEE_IS_JOBCANDIDATE_REL(Parent, DirectionEnum.Out); } }
		}
	}

    public class JobCandidateAlias : AliasResult
    {
        internal JobCandidateAlias(JobCandidateNode parent)
        {
			Node = parent;
            JobCandidateID = new NumericResult(this, "JobCandidateID", Datastore.AdventureWorks.Model.Entities["JobCandidate"], Datastore.AdventureWorks.Model.Entities["JobCandidate"].Properties["JobCandidateID"]);
            Resume = new StringResult(this, "Resume", Datastore.AdventureWorks.Model.Entities["JobCandidate"], Datastore.AdventureWorks.Model.Entities["JobCandidate"].Properties["Resume"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["JobCandidate"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["JobCandidate"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public JobCandidateNode.JobCandidateOut Out { get { return new JobCandidateNode.JobCandidateOut(new JobCandidateNode(this, true)); } }

        public NumericResult JobCandidateID { get; private set; } 
        public StringResult Resume { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
