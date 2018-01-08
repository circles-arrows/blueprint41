
using System;
using Blueprint41;
using Blueprint41.Query;

namespace Domain.Data.Query
{
	public partial class Node
	{
		public static DocumentNode Document { get { return new DocumentNode(); } }
	}

	public partial class DocumentNode : Blueprint41.Query.Node
	{
        public override string Neo4jLabel
        {
            get
            {
				return "Document";
            }
        }

		internal DocumentNode() { }
		internal DocumentNode(DocumentAlias alias, bool isReference = false)
		{
			NodeAlias = alias;
			IsReference = isReference;
		}
		internal DocumentNode(RELATIONSHIP relationship, DirectionEnum direction) : base(relationship, direction) { }

		public DocumentNode Alias(out DocumentAlias alias)
		{
			alias = new DocumentAlias(this);
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
            DocumentNode = new StringResult(this, "DocumentNode", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["DocumentNode"]);
            DocumentLevel = new StringResult(this, "DocumentLevel", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["DocumentLevel"]);
            Title = new StringResult(this, "Title", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["Title"]);
            Owner = new StringResult(this, "Owner", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["Owner"]);
            FolderFlag = new StringResult(this, "FolderFlag", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["FolderFlag"]);
            FileName = new StringResult(this, "FileName", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["FileName"]);
            FileExtension = new StringResult(this, "FileExtension", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["FileExtension"]);
            Revision = new StringResult(this, "Revision", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["Revision"]);
            ChangeNumber = new StringResult(this, "ChangeNumber", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["ChangeNumber"]);
            Status = new StringResult(this, "Status", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["Status"]);
            DocumentSummary = new StringResult(this, "DocumentSummary", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["DocumentSummary"]);
            Doc = new StringResult(this, "Doc", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["Doc"]);
            rowguid = new StringResult(this, "rowguid", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Document"].Properties["rowguid"]);
            ModifiedDate = new DateTimeResult(this, "ModifiedDate", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"]);
            Uid = new StringResult(this, "Uid", Datastore.AdventureWorks.Model.Entities["Document"], Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"]);
        }

        public DocumentNode.DocumentOut Out { get { return new DocumentNode.DocumentOut(new DocumentNode(this, true)); } }

        public StringResult DocumentNode { get; private set; } 
        public StringResult DocumentLevel { get; private set; } 
        public StringResult Title { get; private set; } 
        public StringResult Owner { get; private set; } 
        public StringResult FolderFlag { get; private set; } 
        public StringResult FileName { get; private set; } 
        public StringResult FileExtension { get; private set; } 
        public StringResult Revision { get; private set; } 
        public StringResult ChangeNumber { get; private set; } 
        public StringResult Status { get; private set; } 
        public StringResult DocumentSummary { get; private set; } 
        public StringResult Doc { get; private set; } 
        public StringResult rowguid { get; private set; } 
        public DateTimeResult ModifiedDate { get; private set; } 
        public StringResult Uid { get; private set; } 
    }
}
