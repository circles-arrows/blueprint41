using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static JobCandidateNode JobCandidate { get { return new JobCandidateNode(); } }
	}

	public partial class JobCandidateNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "JobCandidate";
        }

		internal JobCandidateNode() { }
		internal JobCandidateNode(JobCandidateAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal JobCandidateNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public JobCandidateNode Alias(out JobCandidateAlias alias)
		{
			alias = new JobCandidateAlias(this);
            NodeAlias = alias;
			return this;
		}

		public JobCandidateNode UseExistingAlias(AliasResult alias)
        {
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
        }

        public override IReadOnlyDictionary<string, FieldResult> AliasFields
        {
            get
            {
                if (m_AliasFields == null)
                {
                    m_AliasFields = new Dictionary<string, FieldResult>()
                    {
						{ "JobCandidateID", new NumericResult(this, "JobCandidateID", Datastore.AdventureWorks.Model.Entities["JobCandidate"], Datastore.AdventureWorks.Model.Entities["JobCandidate"].Properties["JobCandidateID"]) },
						{ "Resume", new StringResult(this, "Resume", Datastore.AdventureWorks.Model.Entities["JobCandidate"], Datastore.AdventureWorks.Model.Entities["JobCandidate"].Properties["Resume"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["JobCandidate"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["JobCandidate"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
        private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

        public JobCandidateNode.JobCandidateOut Out { get { return new JobCandidateNode.JobCandidateOut(new JobCandidateNode(this, true)); } }

        public NumericResult JobCandidateID
		{
			get
			{
				if ((object)m_JobCandidateID == null)
					m_JobCandidateID = (NumericResult)AliasFields["JobCandidateID"];

				return m_JobCandidateID;
			}
		} 
        private NumericResult m_JobCandidateID = null;
        public StringResult Resume
		{
			get
			{
				if ((object)m_Resume == null)
					m_Resume = (StringResult)AliasFields["Resume"];

				return m_Resume;
			}
		} 
        private StringResult m_Resume = null;
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
