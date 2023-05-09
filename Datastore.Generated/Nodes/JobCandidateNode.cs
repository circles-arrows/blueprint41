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
		public static JobCandidateNode JobCandidate { get { return new JobCandidateNode(); } }
	}

	public partial class JobCandidateNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(JobCandidateNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(JobCandidateNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "JobCandidate";
		}

		protected override Entity GetEntity()
        {
			return m.JobCandidate.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.JobCandidate.Entity.FunctionalId;
            }
        }

		internal JobCandidateNode() { }
		internal JobCandidateNode(JobCandidateAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal JobCandidateNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal JobCandidateNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public JobCandidateNode Where(JsNotation<int> JobCandidateID = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Resume = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<JobCandidateAlias> alias = new Lazy<JobCandidateAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (JobCandidateID.HasValue) conditions.Add(new QueryCondition(alias.Value.JobCandidateID, Operator.Equals, ((IValue)JobCandidateID).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Resume.HasValue) conditions.Add(new QueryCondition(alias.Value.Resume, Operator.Equals, ((IValue)Resume).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public JobCandidateNode Assign(JsNotation<int> JobCandidateID = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Resume = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<JobCandidateAlias> alias = new Lazy<JobCandidateAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (JobCandidateID.HasValue) assignments.Add(new Assignment(alias.Value.JobCandidateID, JobCandidateID));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Resume.HasValue) assignments.Add(new Assignment(alias.Value.Resume, Resume));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public JobCandidateNode Alias(out JobCandidateAlias alias)
        {
            if (NodeAlias is JobCandidateAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new JobCandidateAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public JobCandidateNode Alias(out JobCandidateAlias alias, string name)
        {
            if (NodeAlias is JobCandidateAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new JobCandidateAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public JobCandidateNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
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

	public class JobCandidateAlias : AliasResult<JobCandidateAlias, JobCandidateListAlias>
	{
		internal JobCandidateAlias(JobCandidateNode parent)
		{
			Node = parent;
		}
		internal JobCandidateAlias(JobCandidateNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  JobCandidateAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  JobCandidateAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  JobCandidateAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<int> JobCandidateID = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Resume = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (JobCandidateID.HasValue) assignments.Add(new Assignment(this.JobCandidateID, JobCandidateID));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Resume.HasValue) assignments.Add(new Assignment(this.Resume, Resume));
			if (Uid.HasValue) assignments.Add(new Assignment(this.Uid, Uid));
            
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
				if (m_JobCandidateID is null)
					m_JobCandidateID = (NumericResult)AliasFields["JobCandidateID"];

				return m_JobCandidateID;
			}
		}
		private NumericResult m_JobCandidateID = null;
		public StringResult Resume
		{
			get
			{
				if (m_Resume is null)
					m_Resume = (StringResult)AliasFields["Resume"];

				return m_Resume;
			}
		}
		private StringResult m_Resume = null;
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
		public AsResult As(string aliasName, out JobCandidateAlias alias)
		{
			alias = new JobCandidateAlias((JobCandidateNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class JobCandidateListAlias : ListResult<JobCandidateListAlias, JobCandidateAlias>, IAliasListResult
	{
		private JobCandidateListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private JobCandidateListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private JobCandidateListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class JobCandidateJaggedListAlias : ListResult<JobCandidateJaggedListAlias, JobCandidateListAlias>, IAliasJaggedListResult
	{
		private JobCandidateJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private JobCandidateJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private JobCandidateJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
