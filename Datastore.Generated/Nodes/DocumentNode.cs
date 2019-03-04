using System;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static DocumentNode Document { get { return new DocumentNode(); } }
	}

	public partial class DocumentNode : Blueprint41.Query.Node
	{
        protected override string GetNeo4jLabel()
        {
			return "Document";
        }

		internal DocumentNode() { }
		internal DocumentNode(DocumentAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal DocumentNode(RELATIONSHIP relationship, DirectionEnum direction, string neo4jLabel = null) : base(relationship, direction, neo4jLabel) { }

		public DocumentNode Alias(out DocumentAlias alias)
		{
			alias = new DocumentAlias(this);
            NodeAlias = alias;
			return this;
		}

		public DocumentNode UseExistingAlias(AliasResult alias)
        {
            NodeAlias = alias;
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

    public class DocumentAlias : AliasResult
    {
        internal DocumentAlias(DocumentNode parent)
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
				if ((object)m_DocumentNode == null)
					m_DocumentNode = (StringResult)AliasFields["DocumentNode"];

				return m_DocumentNode;
			}
		} 
        private StringResult m_DocumentNode = null;
        public StringResult DocumentLevel
		{
			get
			{
				if ((object)m_DocumentLevel == null)
					m_DocumentLevel = (StringResult)AliasFields["DocumentLevel"];

				return m_DocumentLevel;
			}
		} 
        private StringResult m_DocumentLevel = null;
        public StringResult Title
		{
			get
			{
				if ((object)m_Title == null)
					m_Title = (StringResult)AliasFields["Title"];

				return m_Title;
			}
		} 
        private StringResult m_Title = null;
        public StringResult Owner
		{
			get
			{
				if ((object)m_Owner == null)
					m_Owner = (StringResult)AliasFields["Owner"];

				return m_Owner;
			}
		} 
        private StringResult m_Owner = null;
        public StringResult FolderFlag
		{
			get
			{
				if ((object)m_FolderFlag == null)
					m_FolderFlag = (StringResult)AliasFields["FolderFlag"];

				return m_FolderFlag;
			}
		} 
        private StringResult m_FolderFlag = null;
        public StringResult FileName
		{
			get
			{
				if ((object)m_FileName == null)
					m_FileName = (StringResult)AliasFields["FileName"];

				return m_FileName;
			}
		} 
        private StringResult m_FileName = null;
        public StringResult FileExtension
		{
			get
			{
				if ((object)m_FileExtension == null)
					m_FileExtension = (StringResult)AliasFields["FileExtension"];

				return m_FileExtension;
			}
		} 
        private StringResult m_FileExtension = null;
        public StringResult Revision
		{
			get
			{
				if ((object)m_Revision == null)
					m_Revision = (StringResult)AliasFields["Revision"];

				return m_Revision;
			}
		} 
        private StringResult m_Revision = null;
        public StringResult ChangeNumber
		{
			get
			{
				if ((object)m_ChangeNumber == null)
					m_ChangeNumber = (StringResult)AliasFields["ChangeNumber"];

				return m_ChangeNumber;
			}
		} 
        private StringResult m_ChangeNumber = null;
        public StringResult Status
		{
			get
			{
				if ((object)m_Status == null)
					m_Status = (StringResult)AliasFields["Status"];

				return m_Status;
			}
		} 
        private StringResult m_Status = null;
        public StringResult DocumentSummary
		{
			get
			{
				if ((object)m_DocumentSummary == null)
					m_DocumentSummary = (StringResult)AliasFields["DocumentSummary"];

				return m_DocumentSummary;
			}
		} 
        private StringResult m_DocumentSummary = null;
        public StringResult Doc
		{
			get
			{
				if ((object)m_Doc == null)
					m_Doc = (StringResult)AliasFields["Doc"];

				return m_Doc;
			}
		} 
        private StringResult m_Doc = null;
        public StringResult rowguid
		{
			get
			{
				if ((object)m_rowguid == null)
					m_rowguid = (StringResult)AliasFields["rowguid"];

				return m_rowguid;
			}
		} 
        private StringResult m_rowguid = null;
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
