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
		public static DocumentNode Document { get { return new DocumentNode(); } }
	}

	public partial class DocumentNode : Blueprint41.Query.Node
	{
        public static implicit operator QueryCondition(DocumentNode a)
        {
            return new QueryCondition(a);
        }
        public static QueryCondition operator !(DocumentNode a)
        {
            return new QueryCondition(a, true);
        } 

		protected override string GetNeo4jLabel()
		{
			return "Document";
		}

		protected override Entity GetEntity()
        {
			return m.Document.Entity;
        }
		public FunctionalId FunctionalId
        {
            get
            {
                return m.Document.Entity.FunctionalId;
            }
        }

		internal DocumentNode() { }
		internal DocumentNode(DocumentAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal DocumentNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity) { }
		internal DocumentNode(RELATIONSHIP relationship, DirectionEnum direction, AliasResult nodeAlias, string neo4jLabel = null, Entity entity = null) : base(relationship, direction, neo4jLabel, entity)
		{
			NodeAlias = nodeAlias;
		}

        public DocumentNode Where(JsNotation<string> ChangeNumber = default, JsNotation<string> Doc = default, JsNotation<string> DocumentLevel = default, JsNotation<string> DocumentNode = default, JsNotation<string> DocumentSummary = default, JsNotation<string> FileExtension = default, JsNotation<string> FileName = default, JsNotation<string> FolderFlag = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Owner = default, JsNotation<string> Revision = default, JsNotation<string> rowguid = default, JsNotation<string> Status = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<DocumentAlias> alias = new Lazy<DocumentAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<QueryCondition> conditions = new List<QueryCondition>();
            if (ChangeNumber.HasValue) conditions.Add(new QueryCondition(alias.Value.ChangeNumber, Operator.Equals, ((IValue)ChangeNumber).GetValue()));
            if (Doc.HasValue) conditions.Add(new QueryCondition(alias.Value.Doc, Operator.Equals, ((IValue)Doc).GetValue()));
            if (DocumentLevel.HasValue) conditions.Add(new QueryCondition(alias.Value.DocumentLevel, Operator.Equals, ((IValue)DocumentLevel).GetValue()));
            if (DocumentNode.HasValue) conditions.Add(new QueryCondition(alias.Value.DocumentNode, Operator.Equals, ((IValue)DocumentNode).GetValue()));
            if (DocumentSummary.HasValue) conditions.Add(new QueryCondition(alias.Value.DocumentSummary, Operator.Equals, ((IValue)DocumentSummary).GetValue()));
            if (FileExtension.HasValue) conditions.Add(new QueryCondition(alias.Value.FileExtension, Operator.Equals, ((IValue)FileExtension).GetValue()));
            if (FileName.HasValue) conditions.Add(new QueryCondition(alias.Value.FileName, Operator.Equals, ((IValue)FileName).GetValue()));
            if (FolderFlag.HasValue) conditions.Add(new QueryCondition(alias.Value.FolderFlag, Operator.Equals, ((IValue)FolderFlag).GetValue()));
            if (ModifiedDate.HasValue) conditions.Add(new QueryCondition(alias.Value.ModifiedDate, Operator.Equals, ((IValue)ModifiedDate).GetValue()));
            if (Owner.HasValue) conditions.Add(new QueryCondition(alias.Value.Owner, Operator.Equals, ((IValue)Owner).GetValue()));
            if (Revision.HasValue) conditions.Add(new QueryCondition(alias.Value.Revision, Operator.Equals, ((IValue)Revision).GetValue()));
            if (rowguid.HasValue) conditions.Add(new QueryCondition(alias.Value.rowguid, Operator.Equals, ((IValue)rowguid).GetValue()));
            if (Status.HasValue) conditions.Add(new QueryCondition(alias.Value.Status, Operator.Equals, ((IValue)Status).GetValue()));
            if (Title.HasValue) conditions.Add(new QueryCondition(alias.Value.Title, Operator.Equals, ((IValue)Title).GetValue()));
            if (Uid.HasValue) conditions.Add(new QueryCondition(alias.Value.Uid, Operator.Equals, ((IValue)Uid).GetValue()));

            InlineConditions = conditions.ToArray();

            return this;
        }
        public DocumentNode Assign(JsNotation<string> ChangeNumber = default, JsNotation<string> Doc = default, JsNotation<string> DocumentLevel = default, JsNotation<string> DocumentNode = default, JsNotation<string> DocumentSummary = default, JsNotation<string> FileExtension = default, JsNotation<string> FileName = default, JsNotation<string> FolderFlag = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Owner = default, JsNotation<string> Revision = default, JsNotation<string> rowguid = default, JsNotation<string> Status = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            if (InlineConditions is not null || InlineAssignments is not null)
                throw new NotSupportedException("You cannot, at the same time, have inline-assignments and inline-conditions defined on a node.");

            Lazy<DocumentAlias> alias = new Lazy<DocumentAlias>(delegate()
            {
                this.Alias(out var a);
                return a;
            });
            List<Assignment> assignments = new List<Assignment>();
            if (ChangeNumber.HasValue) assignments.Add(new Assignment(alias.Value.ChangeNumber, ChangeNumber));
            if (Doc.HasValue) assignments.Add(new Assignment(alias.Value.Doc, Doc));
            if (DocumentLevel.HasValue) assignments.Add(new Assignment(alias.Value.DocumentLevel, DocumentLevel));
            if (DocumentNode.HasValue) assignments.Add(new Assignment(alias.Value.DocumentNode, DocumentNode));
            if (DocumentSummary.HasValue) assignments.Add(new Assignment(alias.Value.DocumentSummary, DocumentSummary));
            if (FileExtension.HasValue) assignments.Add(new Assignment(alias.Value.FileExtension, FileExtension));
            if (FileName.HasValue) assignments.Add(new Assignment(alias.Value.FileName, FileName));
            if (FolderFlag.HasValue) assignments.Add(new Assignment(alias.Value.FolderFlag, FolderFlag));
            if (ModifiedDate.HasValue) assignments.Add(new Assignment(alias.Value.ModifiedDate, ModifiedDate));
            if (Owner.HasValue) assignments.Add(new Assignment(alias.Value.Owner, Owner));
            if (Revision.HasValue) assignments.Add(new Assignment(alias.Value.Revision, Revision));
            if (rowguid.HasValue) assignments.Add(new Assignment(alias.Value.rowguid, rowguid));
            if (Status.HasValue) assignments.Add(new Assignment(alias.Value.Status, Status));
            if (Title.HasValue) assignments.Add(new Assignment(alias.Value.Title, Title));
            if (Uid.HasValue) assignments.Add(new Assignment(alias.Value.Uid, Uid));

            InlineAssignments = assignments.ToArray();

            return this;
        }

		public DocumentNode Alias(out DocumentAlias alias)
        {
            if (NodeAlias is DocumentAlias a)
            {
                alias = a;
            }
            else
            {
                alias = new DocumentAlias(this);
                NodeAlias = alias;
            }
            return this;
        }
		public DocumentNode Alias(out DocumentAlias alias, string name)
        {
            if (NodeAlias is DocumentAlias a)
            {
                a.SetAlias(name);
                alias = a;
            }
            else
            {
                alias = new DocumentAlias(this, name);
                NodeAlias = alias;
            }
            return this;
        }

		public DocumentNode UseExistingAlias(AliasResult alias)
		{
			NodeAlias = alias;
            IsReference = true;
			return this;
		}


		public DocumentOut Out { get { return new DocumentOut(this); } }
		public class DocumentOut
		{
			private DocumentNode Parent;
			internal DocumentOut(DocumentNode parent)
			{
				Parent = parent;
			}
			public IFromOut_PERSON_VALID_FOR_DOCUMENT_REL PERSON_VALID_FOR_DOCUMENT { get { return new PERSON_VALID_FOR_DOCUMENT_REL(Parent, DirectionEnum.Out); } }
			public IFromOut_PRODUCT_HAS_DOCUMENT_REL PRODUCT_HAS_DOCUMENT { get { return new PRODUCT_HAS_DOCUMENT_REL(Parent, DirectionEnum.Out); } }
		}
	}

	public class DocumentAlias : AliasResult<DocumentAlias, DocumentListAlias>
	{
		internal DocumentAlias(DocumentNode parent)
		{
			Node = parent;
		}
		internal DocumentAlias(DocumentNode parent, string name)
		{
			Node = parent;
			AliasName = name;
		}
		internal void SetAlias(string name) => AliasName = name;

		private  DocumentAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private  DocumentAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private  DocumentAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type)
		{
			Node = alias.Node;
		}

		public Assignment[] Assign(JsNotation<string> ChangeNumber = default, JsNotation<string> Doc = default, JsNotation<string> DocumentLevel = default, JsNotation<string> DocumentNode = default, JsNotation<string> DocumentSummary = default, JsNotation<string> FileExtension = default, JsNotation<string> FileName = default, JsNotation<string> FolderFlag = default, JsNotation<System.DateTime> ModifiedDate = default, JsNotation<string> Owner = default, JsNotation<string> Revision = default, JsNotation<string> rowguid = default, JsNotation<string> Status = default, JsNotation<string> Title = default, JsNotation<string> Uid = default)
        {
            List<Assignment> assignments = new List<Assignment>();
			if (ChangeNumber.HasValue) assignments.Add(new Assignment(this.ChangeNumber, ChangeNumber));
			if (Doc.HasValue) assignments.Add(new Assignment(this.Doc, Doc));
			if (DocumentLevel.HasValue) assignments.Add(new Assignment(this.DocumentLevel, DocumentLevel));
			if (DocumentNode.HasValue) assignments.Add(new Assignment(this.DocumentNode, DocumentNode));
			if (DocumentSummary.HasValue) assignments.Add(new Assignment(this.DocumentSummary, DocumentSummary));
			if (FileExtension.HasValue) assignments.Add(new Assignment(this.FileExtension, FileExtension));
			if (FileName.HasValue) assignments.Add(new Assignment(this.FileName, FileName));
			if (FolderFlag.HasValue) assignments.Add(new Assignment(this.FolderFlag, FolderFlag));
			if (ModifiedDate.HasValue) assignments.Add(new Assignment(this.ModifiedDate, ModifiedDate));
			if (Owner.HasValue) assignments.Add(new Assignment(this.Owner, Owner));
			if (Revision.HasValue) assignments.Add(new Assignment(this.Revision, Revision));
			if (rowguid.HasValue) assignments.Add(new Assignment(this.rowguid, rowguid));
			if (Status.HasValue) assignments.Add(new Assignment(this.Status, Status));
			if (Title.HasValue) assignments.Add(new Assignment(this.Title, Title));
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
						{ "DocumentNode", new StringResult(this, "DocumentNode", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["DocumentNode"]) },
						{ "DocumentLevel", new StringResult(this, "DocumentLevel", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["DocumentLevel"]) },
						{ "Title", new StringResult(this, "Title", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["Title"]) },
						{ "Owner", new StringResult(this, "Owner", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["Owner"]) },
						{ "FolderFlag", new StringResult(this, "FolderFlag", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["FolderFlag"]) },
						{ "FileName", new StringResult(this, "FileName", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["FileName"]) },
						{ "FileExtension", new StringResult(this, "FileExtension", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["FileExtension"]) },
						{ "Revision", new StringResult(this, "Revision", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["Revision"]) },
						{ "ChangeNumber", new StringResult(this, "ChangeNumber", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["ChangeNumber"]) },
						{ "Status", new StringResult(this, "Status", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["Status"]) },
						{ "DocumentSummary", new StringResult(this, "DocumentSummary", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["DocumentSummary"]) },
						{ "Doc", new StringResult(this, "Doc", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["Doc"]) },
						{ "rowguid", new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["rowguid"]) },
						{ "ModifiedDate", new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]) },
						{ "Uid", new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]) },
					};
				}
				return m_AliasFields;
			}
		}
		private IReadOnlyDictionary<string, FieldResult> m_AliasFields = null;

		public DocumentNode.DocumentOut Out { get { return new DocumentNode.DocumentOut(new DocumentNode(this, true)); } }

		public StringResult DocumentNode
		{
			get
			{
				if (m_DocumentNode is null)
					m_DocumentNode = (StringResult)AliasFields["DocumentNode"];

				return m_DocumentNode;
			}
		}
		private StringResult m_DocumentNode = null;
		public StringResult DocumentLevel
		{
			get
			{
				if (m_DocumentLevel is null)
					m_DocumentLevel = (StringResult)AliasFields["DocumentLevel"];

				return m_DocumentLevel;
			}
		}
		private StringResult m_DocumentLevel = null;
		public StringResult Title
		{
			get
			{
				if (m_Title is null)
					m_Title = (StringResult)AliasFields["Title"];

				return m_Title;
			}
		}
		private StringResult m_Title = null;
		public StringResult Owner
		{
			get
			{
				if (m_Owner is null)
					m_Owner = (StringResult)AliasFields["Owner"];

				return m_Owner;
			}
		}
		private StringResult m_Owner = null;
		public StringResult FolderFlag
		{
			get
			{
				if (m_FolderFlag is null)
					m_FolderFlag = (StringResult)AliasFields["FolderFlag"];

				return m_FolderFlag;
			}
		}
		private StringResult m_FolderFlag = null;
		public StringResult FileName
		{
			get
			{
				if (m_FileName is null)
					m_FileName = (StringResult)AliasFields["FileName"];

				return m_FileName;
			}
		}
		private StringResult m_FileName = null;
		public StringResult FileExtension
		{
			get
			{
				if (m_FileExtension is null)
					m_FileExtension = (StringResult)AliasFields["FileExtension"];

				return m_FileExtension;
			}
		}
		private StringResult m_FileExtension = null;
		public StringResult Revision
		{
			get
			{
				if (m_Revision is null)
					m_Revision = (StringResult)AliasFields["Revision"];

				return m_Revision;
			}
		}
		private StringResult m_Revision = null;
		public StringResult ChangeNumber
		{
			get
			{
				if (m_ChangeNumber is null)
					m_ChangeNumber = (StringResult)AliasFields["ChangeNumber"];

				return m_ChangeNumber;
			}
		}
		private StringResult m_ChangeNumber = null;
		public StringResult Status
		{
			get
			{
				if (m_Status is null)
					m_Status = (StringResult)AliasFields["Status"];

				return m_Status;
			}
		}
		private StringResult m_Status = null;
		public StringResult DocumentSummary
		{
			get
			{
				if (m_DocumentSummary is null)
					m_DocumentSummary = (StringResult)AliasFields["DocumentSummary"];

				return m_DocumentSummary;
			}
		}
		private StringResult m_DocumentSummary = null;
		public StringResult Doc
		{
			get
			{
				if (m_Doc is null)
					m_Doc = (StringResult)AliasFields["Doc"];

				return m_Doc;
			}
		}
		private StringResult m_Doc = null;
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
		public AsResult As(string aliasName, out DocumentAlias alias)
		{
			alias = new DocumentAlias((DocumentNode)Node)
			{
				AliasName = aliasName
			};
			return this.As(aliasName);
		}
	}

	public class DocumentListAlias : ListResult<DocumentListAlias, DocumentAlias>, IAliasListResult
	{
		private DocumentListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private DocumentListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private DocumentListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
	public class DocumentJaggedListAlias : ListResult<DocumentJaggedListAlias, DocumentListAlias>, IAliasJaggedListResult
	{
		private DocumentJaggedListAlias(Func<QueryTranslator, string> function, object[] arguments, Type type) : base(function, arguments, type) { }
		private DocumentJaggedListAlias(FieldResult parent, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
		private DocumentJaggedListAlias(AliasResult alias, Func<QueryTranslator, string> function, object[] arguments = null, Type type = null) : base(alias, function, arguments, type) { }
	}
}
