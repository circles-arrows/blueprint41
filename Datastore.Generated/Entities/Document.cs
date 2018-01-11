using System;
using System.Linq;
using System.Collections.Generic;
using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Domain.Data.Query;

namespace Domain.Data.Manipulation
{
	public interface IDocumentOriginalData
    {
		#region Outer Data

		#region Members for interface IDocument

		string DocumentNode { get; }
		string DocumentLevel { get; }
		string Title { get; }
		string Owner { get; }
		string FolderFlag { get; }
		string FileName { get; }
		string FileExtension { get; }
		string Revision { get; }
		string ChangeNumber { get; }
		string Status { get; }
		string DocumentSummary { get; }
		string Doc { get; }
		string rowguid { get; }
		IEnumerable<Person> Persons { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
    }

	public partial class Document : OGM<Document, Document.DocumentData, System.String>, ISchemaBase, INeo4jBase, IDocumentOriginalData
	{
        #region Initialize

        static Document()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadFromNaturalKey
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

			AdditionalGeneratedStoredQueries();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, Document> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.DocumentAlias, IWhereQuery> query)
        {
            q.DocumentAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Document.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Document => DocumentNode : {this.DocumentNode}, DocumentLevel : {this.DocumentLevel}, Title : {this.Title}, Owner : {this.Owner}, FolderFlag : {this.FolderFlag}, FileName : {this.FileName}, FileExtension : {this.FileExtension}, Revision : {this.Revision}, ChangeNumber : {this.ChangeNumber}, Status : {this.Status}, DocumentSummary : {this.DocumentSummary?.ToString() ?? "null"}, Doc : {this.Doc?.ToString() ?? "null"}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

		protected override void LazySet()
        {
            base.LazySet();
            if (PersistenceState == PersistenceState.NewAndChanged || PersistenceState == PersistenceState.LoadedAndChanged)
            {
                if ((object)InnerData == (object)OriginalData)
                    OriginalData = new DocumentData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.DocumentNode == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the DocumentNode cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.DocumentLevel == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the DocumentLevel cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Title == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the Title cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Owner == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the Owner cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.FolderFlag == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the FolderFlag cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.FileName == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the FileName cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.FileExtension == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the FileExtension cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Revision == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the Revision cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ChangeNumber == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the ChangeNumber cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Status == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the Status cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save Document with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class DocumentData : Data<System.String>
		{
			public DocumentData()
            {

            }

            public DocumentData(DocumentData data)
            {
				DocumentNode = data.DocumentNode;
				DocumentLevel = data.DocumentLevel;
				Title = data.Title;
				Owner = data.Owner;
				FolderFlag = data.FolderFlag;
				FileName = data.FileName;
				FileExtension = data.FileExtension;
				Revision = data.Revision;
				ChangeNumber = data.ChangeNumber;
				Status = data.Status;
				DocumentSummary = data.DocumentSummary;
				Doc = data.Doc;
				rowguid = data.rowguid;
				Persons = data.Persons;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Document";

				Persons = new EntityCollection<Person>(Wrapper, Members.Persons, item => { if (Members.Persons.Events.HasRegisteredChangeHandlers) { object loadHack = item.Document; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("DocumentNode",  DocumentNode);
				dictionary.Add("DocumentLevel",  DocumentLevel);
				dictionary.Add("Title",  Title);
				dictionary.Add("Owner",  Owner);
				dictionary.Add("FolderFlag",  FolderFlag);
				dictionary.Add("FileName",  FileName);
				dictionary.Add("FileExtension",  FileExtension);
				dictionary.Add("Revision",  Revision);
				dictionary.Add("ChangeNumber",  ChangeNumber);
				dictionary.Add("Status",  Status);
				dictionary.Add("DocumentSummary",  DocumentSummary);
				dictionary.Add("Doc",  Doc);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("DocumentNode", out value))
					DocumentNode = (string)value;
				if (properties.TryGetValue("DocumentLevel", out value))
					DocumentLevel = (string)value;
				if (properties.TryGetValue("Title", out value))
					Title = (string)value;
				if (properties.TryGetValue("Owner", out value))
					Owner = (string)value;
				if (properties.TryGetValue("FolderFlag", out value))
					FolderFlag = (string)value;
				if (properties.TryGetValue("FileName", out value))
					FileName = (string)value;
				if (properties.TryGetValue("FileExtension", out value))
					FileExtension = (string)value;
				if (properties.TryGetValue("Revision", out value))
					Revision = (string)value;
				if (properties.TryGetValue("ChangeNumber", out value))
					ChangeNumber = (string)value;
				if (properties.TryGetValue("Status", out value))
					Status = (string)value;
				if (properties.TryGetValue("DocumentSummary", out value))
					DocumentSummary = (string)value;
				if (properties.TryGetValue("Doc", out value))
					Doc = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IDocument

			public string DocumentNode { get; set; }
			public string DocumentLevel { get; set; }
			public string Title { get; set; }
			public string Owner { get; set; }
			public string FolderFlag { get; set; }
			public string FileName { get; set; }
			public string FileExtension { get; set; }
			public string Revision { get; set; }
			public string ChangeNumber { get; set; }
			public string Status { get; set; }
			public string DocumentSummary { get; set; }
			public string Doc { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<Person> Persons { get; private set; }

			#endregion
			#region Members for interface ISchemaBase

			public System.DateTime ModifiedDate { get; set; }

			#endregion
			#region Members for interface INeo4jBase

			public string Uid { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IDocument

		public string DocumentNode { get { LazyGet(); return InnerData.DocumentNode; } set { if (LazySet(Members.DocumentNode, InnerData.DocumentNode, value)) InnerData.DocumentNode = value; } }
		public string DocumentLevel { get { LazyGet(); return InnerData.DocumentLevel; } set { if (LazySet(Members.DocumentLevel, InnerData.DocumentLevel, value)) InnerData.DocumentLevel = value; } }
		public string Title { get { LazyGet(); return InnerData.Title; } set { if (LazySet(Members.Title, InnerData.Title, value)) InnerData.Title = value; } }
		public string Owner { get { LazyGet(); return InnerData.Owner; } set { if (LazySet(Members.Owner, InnerData.Owner, value)) InnerData.Owner = value; } }
		public string FolderFlag { get { LazyGet(); return InnerData.FolderFlag; } set { if (LazySet(Members.FolderFlag, InnerData.FolderFlag, value)) InnerData.FolderFlag = value; } }
		public string FileName { get { LazyGet(); return InnerData.FileName; } set { if (LazySet(Members.FileName, InnerData.FileName, value)) InnerData.FileName = value; } }
		public string FileExtension { get { LazyGet(); return InnerData.FileExtension; } set { if (LazySet(Members.FileExtension, InnerData.FileExtension, value)) InnerData.FileExtension = value; } }
		public string Revision { get { LazyGet(); return InnerData.Revision; } set { if (LazySet(Members.Revision, InnerData.Revision, value)) InnerData.Revision = value; } }
		public string ChangeNumber { get { LazyGet(); return InnerData.ChangeNumber; } set { if (LazySet(Members.ChangeNumber, InnerData.ChangeNumber, value)) InnerData.ChangeNumber = value; } }
		public string Status { get { LazyGet(); return InnerData.Status; } set { if (LazySet(Members.Status, InnerData.Status, value)) InnerData.Status = value; } }
		public string DocumentSummary { get { LazyGet(); return InnerData.DocumentSummary; } set { if (LazySet(Members.DocumentSummary, InnerData.DocumentSummary, value)) InnerData.DocumentSummary = value; } }
		public string Doc { get { LazyGet(); return InnerData.Doc; } set { if (LazySet(Members.Doc, InnerData.Doc, value)) InnerData.Doc = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public EntityCollection<Person> Persons { get { return InnerData.Persons; } }
		private void ClearPersons(DateTime? moment)
		{
			((ILookupHelper<EntityCollection<Person>>)InnerData.Persons).ClearLookup(moment);
		}

		#endregion
		#region Members for interface ISchemaBase

		public System.DateTime ModifiedDate { get { LazyGet(); return InnerData.ModifiedDate; } set { if (LazySet(Members.ModifiedDate, InnerData.ModifiedDate, value)) InnerData.ModifiedDate = value; } }

		#endregion
		#region Members for interface INeo4jBase

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

        private static DocumentMembers members = null;
        public static DocumentMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Document))
                    {
                        if (members == null)
                            members = new DocumentMembers();
                    }
                }
                return members;
            }
        }
        public class DocumentMembers
        {
            internal DocumentMembers() { }

			#region Members for interface IDocument

            public Property DocumentNode { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["DocumentNode"];
            public Property DocumentLevel { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["DocumentLevel"];
            public Property Title { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["Title"];
            public Property Owner { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["Owner"];
            public Property FolderFlag { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["FolderFlag"];
            public Property FileName { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["FileName"];
            public Property FileExtension { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["FileExtension"];
            public Property Revision { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["Revision"];
            public Property ChangeNumber { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["ChangeNumber"];
            public Property Status { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["Status"];
            public Property DocumentSummary { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["DocumentSummary"];
            public Property Doc { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["Doc"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["rowguid"];
            public Property Persons { get; } = Datastore.AdventureWorks.Model.Entities["Document"].Properties["Persons"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static DocumentFullTextMembers fullTextMembers = null;
        public static DocumentFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Document))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new DocumentFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class DocumentFullTextMembers
        {
            internal DocumentFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Document))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["Document"];
                }
            }
            return entity;
        }

		private static DocumentEvents events = null;
        public static DocumentEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Document))
                    {
                        if (events == null)
                            events = new DocumentEvents();
                    }
                }
                return events;
            }
        }
        public class DocumentEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Document, EntityEventArgs> onNew;
            public event EventHandler<Document, EntityEventArgs> OnNew
            {
                add
                {
                    lock (this)
                    {
                        if (!onNewIsRegistered)
                        {
                            Entity.Events.OnNew -= onNewProxy;
                            Entity.Events.OnNew += onNewProxy;
                            onNewIsRegistered = true;
                        }
                        onNew += value;
                    }
                }
                remove
                {
                    lock (this)
                    {
                        onNew -= value;
                        if (onNew == null && onNewIsRegistered)
                        {
                            Entity.Events.OnNew -= onNewProxy;
                            onNewIsRegistered = false;
                        }
                    }
                }
            }
            
			private void onNewProxy(object sender, EntityEventArgs args)
            {
                EventHandler<Document, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Document)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Document, EntityEventArgs> onDelete;
            public event EventHandler<Document, EntityEventArgs> OnDelete
            {
                add
                {
                    lock (this)
                    {
                        if (!onDeleteIsRegistered)
                        {
                            Entity.Events.OnDelete -= onDeleteProxy;
                            Entity.Events.OnDelete += onDeleteProxy;
                            onDeleteIsRegistered = true;
                        }
                        onDelete += value;
                    }
                }
                remove
                {
                    lock (this)
                    {
                        onDelete -= value;
                        if (onDelete == null && onDeleteIsRegistered)
                        {
                            Entity.Events.OnDelete -= onDeleteProxy;
                            onDeleteIsRegistered = false;
                        }
                    }
                }
            }
            
			private void onDeleteProxy(object sender, EntityEventArgs args)
            {
                EventHandler<Document, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Document)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Document, EntityEventArgs> onSave;
            public event EventHandler<Document, EntityEventArgs> OnSave
            {
                add
                {
                    lock (this)
                    {
                        if (!onSaveIsRegistered)
                        {
                            Entity.Events.OnSave -= onSaveProxy;
                            Entity.Events.OnSave += onSaveProxy;
                            onSaveIsRegistered = true;
                        }
                        onSave += value;
                    }
                }
                remove
                {
                    lock (this)
                    {
                        onSave -= value;
                        if (onSave == null && onSaveIsRegistered)
                        {
                            Entity.Events.OnSave -= onSaveProxy;
                            onSaveIsRegistered = false;
                        }
                    }
                }
            }
            
			private void onSaveProxy(object sender, EntityEventArgs args)
            {
                EventHandler<Document, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Document)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnDocumentNode

				private static bool onDocumentNodeIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onDocumentNode;
				public static event EventHandler<Document, PropertyEventArgs> OnDocumentNode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDocumentNodeIsRegistered)
							{
								Members.DocumentNode.Events.OnChange -= onDocumentNodeProxy;
								Members.DocumentNode.Events.OnChange += onDocumentNodeProxy;
								onDocumentNodeIsRegistered = true;
							}
							onDocumentNode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDocumentNode -= value;
							if (onDocumentNode == null && onDocumentNodeIsRegistered)
							{
								Members.DocumentNode.Events.OnChange -= onDocumentNodeProxy;
								onDocumentNodeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDocumentNodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onDocumentNode;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnDocumentLevel

				private static bool onDocumentLevelIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onDocumentLevel;
				public static event EventHandler<Document, PropertyEventArgs> OnDocumentLevel
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDocumentLevelIsRegistered)
							{
								Members.DocumentLevel.Events.OnChange -= onDocumentLevelProxy;
								Members.DocumentLevel.Events.OnChange += onDocumentLevelProxy;
								onDocumentLevelIsRegistered = true;
							}
							onDocumentLevel += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDocumentLevel -= value;
							if (onDocumentLevel == null && onDocumentLevelIsRegistered)
							{
								Members.DocumentLevel.Events.OnChange -= onDocumentLevelProxy;
								onDocumentLevelIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDocumentLevelProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onDocumentLevel;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnTitle

				private static bool onTitleIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onTitle;
				public static event EventHandler<Document, PropertyEventArgs> OnTitle
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTitleIsRegistered)
							{
								Members.Title.Events.OnChange -= onTitleProxy;
								Members.Title.Events.OnChange += onTitleProxy;
								onTitleIsRegistered = true;
							}
							onTitle += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTitle -= value;
							if (onTitle == null && onTitleIsRegistered)
							{
								Members.Title.Events.OnChange -= onTitleProxy;
								onTitleIsRegistered = false;
							}
						}
					}
				}
            
				private static void onTitleProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onTitle;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnOwner

				private static bool onOwnerIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onOwner;
				public static event EventHandler<Document, PropertyEventArgs> OnOwner
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onOwnerIsRegistered)
							{
								Members.Owner.Events.OnChange -= onOwnerProxy;
								Members.Owner.Events.OnChange += onOwnerProxy;
								onOwnerIsRegistered = true;
							}
							onOwner += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onOwner -= value;
							if (onOwner == null && onOwnerIsRegistered)
							{
								Members.Owner.Events.OnChange -= onOwnerProxy;
								onOwnerIsRegistered = false;
							}
						}
					}
				}
            
				private static void onOwnerProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onOwner;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnFolderFlag

				private static bool onFolderFlagIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onFolderFlag;
				public static event EventHandler<Document, PropertyEventArgs> OnFolderFlag
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFolderFlagIsRegistered)
							{
								Members.FolderFlag.Events.OnChange -= onFolderFlagProxy;
								Members.FolderFlag.Events.OnChange += onFolderFlagProxy;
								onFolderFlagIsRegistered = true;
							}
							onFolderFlag += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFolderFlag -= value;
							if (onFolderFlag == null && onFolderFlagIsRegistered)
							{
								Members.FolderFlag.Events.OnChange -= onFolderFlagProxy;
								onFolderFlagIsRegistered = false;
							}
						}
					}
				}
            
				private static void onFolderFlagProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onFolderFlag;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnFileName

				private static bool onFileNameIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onFileName;
				public static event EventHandler<Document, PropertyEventArgs> OnFileName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFileNameIsRegistered)
							{
								Members.FileName.Events.OnChange -= onFileNameProxy;
								Members.FileName.Events.OnChange += onFileNameProxy;
								onFileNameIsRegistered = true;
							}
							onFileName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFileName -= value;
							if (onFileName == null && onFileNameIsRegistered)
							{
								Members.FileName.Events.OnChange -= onFileNameProxy;
								onFileNameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onFileNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onFileName;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnFileExtension

				private static bool onFileExtensionIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onFileExtension;
				public static event EventHandler<Document, PropertyEventArgs> OnFileExtension
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFileExtensionIsRegistered)
							{
								Members.FileExtension.Events.OnChange -= onFileExtensionProxy;
								Members.FileExtension.Events.OnChange += onFileExtensionProxy;
								onFileExtensionIsRegistered = true;
							}
							onFileExtension += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFileExtension -= value;
							if (onFileExtension == null && onFileExtensionIsRegistered)
							{
								Members.FileExtension.Events.OnChange -= onFileExtensionProxy;
								onFileExtensionIsRegistered = false;
							}
						}
					}
				}
            
				private static void onFileExtensionProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onFileExtension;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnRevision

				private static bool onRevisionIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onRevision;
				public static event EventHandler<Document, PropertyEventArgs> OnRevision
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onRevisionIsRegistered)
							{
								Members.Revision.Events.OnChange -= onRevisionProxy;
								Members.Revision.Events.OnChange += onRevisionProxy;
								onRevisionIsRegistered = true;
							}
							onRevision += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onRevision -= value;
							if (onRevision == null && onRevisionIsRegistered)
							{
								Members.Revision.Events.OnChange -= onRevisionProxy;
								onRevisionIsRegistered = false;
							}
						}
					}
				}
            
				private static void onRevisionProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onRevision;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnChangeNumber

				private static bool onChangeNumberIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onChangeNumber;
				public static event EventHandler<Document, PropertyEventArgs> OnChangeNumber
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onChangeNumberIsRegistered)
							{
								Members.ChangeNumber.Events.OnChange -= onChangeNumberProxy;
								Members.ChangeNumber.Events.OnChange += onChangeNumberProxy;
								onChangeNumberIsRegistered = true;
							}
							onChangeNumber += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onChangeNumber -= value;
							if (onChangeNumber == null && onChangeNumberIsRegistered)
							{
								Members.ChangeNumber.Events.OnChange -= onChangeNumberProxy;
								onChangeNumberIsRegistered = false;
							}
						}
					}
				}
            
				private static void onChangeNumberProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onChangeNumber;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnStatus

				private static bool onStatusIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onStatus;
				public static event EventHandler<Document, PropertyEventArgs> OnStatus
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStatusIsRegistered)
							{
								Members.Status.Events.OnChange -= onStatusProxy;
								Members.Status.Events.OnChange += onStatusProxy;
								onStatusIsRegistered = true;
							}
							onStatus += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStatus -= value;
							if (onStatus == null && onStatusIsRegistered)
							{
								Members.Status.Events.OnChange -= onStatusProxy;
								onStatusIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStatusProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onStatus;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnDocumentSummary

				private static bool onDocumentSummaryIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onDocumentSummary;
				public static event EventHandler<Document, PropertyEventArgs> OnDocumentSummary
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDocumentSummaryIsRegistered)
							{
								Members.DocumentSummary.Events.OnChange -= onDocumentSummaryProxy;
								Members.DocumentSummary.Events.OnChange += onDocumentSummaryProxy;
								onDocumentSummaryIsRegistered = true;
							}
							onDocumentSummary += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDocumentSummary -= value;
							if (onDocumentSummary == null && onDocumentSummaryIsRegistered)
							{
								Members.DocumentSummary.Events.OnChange -= onDocumentSummaryProxy;
								onDocumentSummaryIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDocumentSummaryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onDocumentSummary;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnDoc

				private static bool onDocIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onDoc;
				public static event EventHandler<Document, PropertyEventArgs> OnDoc
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDocIsRegistered)
							{
								Members.Doc.Events.OnChange -= onDocProxy;
								Members.Doc.Events.OnChange += onDocProxy;
								onDocIsRegistered = true;
							}
							onDoc += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDoc -= value;
							if (onDoc == null && onDocIsRegistered)
							{
								Members.Doc.Events.OnChange -= onDocProxy;
								onDocIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDocProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onDoc;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onrowguid;
				public static event EventHandler<Document, PropertyEventArgs> Onrowguid
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								Members.rowguid.Events.OnChange += onrowguidProxy;
								onrowguidIsRegistered = true;
							}
							onrowguid += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onrowguid -= value;
							if (onrowguid == null && onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								onrowguidIsRegistered = false;
							}
						}
					}
				}
            
				private static void onrowguidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnPersons

				private static bool onPersonsIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onPersons;
				public static event EventHandler<Document, PropertyEventArgs> OnPersons
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPersonsIsRegistered)
							{
								Members.Persons.Events.OnChange -= onPersonsProxy;
								Members.Persons.Events.OnChange += onPersonsProxy;
								onPersonsIsRegistered = true;
							}
							onPersons += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPersons -= value;
							if (onPersons == null && onPersonsIsRegistered)
							{
								Members.Persons.Events.OnChange -= onPersonsProxy;
								onPersonsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPersonsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onPersons;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Document, PropertyEventArgs> OnModifiedDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								Members.ModifiedDate.Events.OnChange += onModifiedDateProxy;
								onModifiedDateIsRegistered = true;
							}
							onModifiedDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onModifiedDate -= value;
							if (onModifiedDate == null && onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								onModifiedDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onModifiedDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Document, PropertyEventArgs> onUid;
				public static event EventHandler<Document, PropertyEventArgs> OnUid
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onUidIsRegistered)
							{
								Members.Uid.Events.OnChange -= onUidProxy;
								Members.Uid.Events.OnChange += onUidProxy;
								onUidIsRegistered = true;
							}
							onUid += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onUid -= value;
							if (onUid == null && onUidIsRegistered)
							{
								Members.Uid.Events.OnChange -= onUidProxy;
								onUidIsRegistered = false;
							}
						}
					}
				}
            
				private static void onUidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Document, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Document)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IDocumentOriginalData

		public IDocumentOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IDocument

		string IDocumentOriginalData.DocumentNode { get { return OriginalData.DocumentNode; } }
		string IDocumentOriginalData.DocumentLevel { get { return OriginalData.DocumentLevel; } }
		string IDocumentOriginalData.Title { get { return OriginalData.Title; } }
		string IDocumentOriginalData.Owner { get { return OriginalData.Owner; } }
		string IDocumentOriginalData.FolderFlag { get { return OriginalData.FolderFlag; } }
		string IDocumentOriginalData.FileName { get { return OriginalData.FileName; } }
		string IDocumentOriginalData.FileExtension { get { return OriginalData.FileExtension; } }
		string IDocumentOriginalData.Revision { get { return OriginalData.Revision; } }
		string IDocumentOriginalData.ChangeNumber { get { return OriginalData.ChangeNumber; } }
		string IDocumentOriginalData.Status { get { return OriginalData.Status; } }
		string IDocumentOriginalData.DocumentSummary { get { return OriginalData.DocumentSummary; } }
		string IDocumentOriginalData.Doc { get { return OriginalData.Doc; } }
		string IDocumentOriginalData.rowguid { get { return OriginalData.rowguid; } }
		IEnumerable<Person> IDocumentOriginalData.Persons { get { return OriginalData.Persons.OriginalData; } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime IDocumentOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string IDocumentOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}