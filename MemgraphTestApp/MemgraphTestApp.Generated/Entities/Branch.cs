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
	public interface IBranchOriginalData : INeo4jBaseOriginalData
	{
		string Name { get; }
		IEnumerable<Department> Departments { get; }
		HeadEmployee BranchHead { get; }
	}

	public partial class Branch : OGM<Branch, Branch.BranchData, System.String>, INeo4jBase, IBranchOriginalData
	{
		#region Initialize

		static Branch()
		{
			Register.Types();
		}


		protected override void RegisterGeneratedStoredQueries()
		{
			#region LoadByKeys
			
			RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
				Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

			#endregion

			AdditionalGeneratedStoredQueries();
		}
		partial void AdditionalGeneratedStoredQueries();

		public static Dictionary<System.String, Branch> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.BranchAlias, IWhereQuery> query)
		{
			q.BranchAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Branch.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"Branch => Name : {this.Name?.ToString() ?? "null"}, Uid : {this.Uid}, CreatedOn : {this.CreatedOn?.ToString() ?? "null"}, CreatedBy : {this.CreatedBy?.ToString() ?? "null"}, LastModifiedOn : {this.LastModifiedOn}, LastModifiedBy : {this.LastModifiedBy?.ToString() ?? "null"}, Description : {this.Description?.ToString() ?? "null"}";
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
				if (ReferenceEquals(InnerData, OriginalData))
					OriginalData = new BranchData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.Uid is null)
				throw new PersistenceException(string.Format("Cannot save Branch with key '{0}' because the Uid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class BranchData : Data<System.String>
		{
			public BranchData()
			{

			}

			public BranchData(BranchData data)
			{
				Name = data.Name;
				Departments = data.Departments;
				BranchHead = data.BranchHead;
				Uid = data.Uid;
				CreatedOn = data.CreatedOn;
				CreatedBy = data.CreatedBy;
				LastModifiedOn = data.LastModifiedOn;
				LastModifiedBy = data.LastModifiedBy;
				Description = data.Description;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Branch";

				Departments = new EntityCollection<Department>(Wrapper, Members.Departments, item => { if (Members.Departments.Events.HasRegisteredChangeHandlers) { object loadHack = item.Branch; } });
				BranchHead = new EntityCollection<HeadEmployee>(Wrapper, Members.BranchHead, item => { if (Members.BranchHead.Events.HasRegisteredChangeHandlers) { object loadHack = item.Branch; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Name",  Name);
				dictionary.Add("Uid",  Uid);
				dictionary.Add("CreatedOn",  Conversion<System.DateTime?, long?>.Convert(CreatedOn));
				dictionary.Add("CreatedBy",  CreatedBy);
				dictionary.Add("LastModifiedOn",  Conversion<System.DateTime, long>.Convert(LastModifiedOn));
				dictionary.Add("LastModifiedBy",  LastModifiedBy);
				dictionary.Add("Description",  Description);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("CreatedOn", out value))
					CreatedOn = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("CreatedBy", out value))
					CreatedBy = (string)value;
				if (properties.TryGetValue("LastModifiedOn", out value))
					LastModifiedOn = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("LastModifiedBy", out value))
					LastModifiedBy = (string)value;
				if (properties.TryGetValue("Description", out value))
					Description = (string)value;
			}

			#endregion

			#region Members for interface IBranch

			public string Name { get; set; }
			public EntityCollection<Department> Departments { get; private set; }
			public EntityCollection<HeadEmployee> BranchHead { get; private set; }

			#endregion
			#region Members for interface INeo4jBase

			public string Uid { get; set; }
			public System.DateTime? CreatedOn { get; set; }
			public string CreatedBy { get; set; }
			public System.DateTime LastModifiedOn { get; set; }
			public string LastModifiedBy { get; set; }
			public string Description { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IBranch

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public EntityCollection<Department> Departments { get { return InnerData.Departments; } }
		private void ClearDepartments(DateTime? moment)
		{
			((ILookupHelper<Department>)InnerData.Departments).ClearLookup(moment);
		}
		public HeadEmployee BranchHead
		{
			get { return ((ILookupHelper<HeadEmployee>)InnerData.BranchHead).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.BranchHead, ((ILookupHelper<HeadEmployee>)InnerData.BranchHead).GetItem(null), value))
					((ILookupHelper<HeadEmployee>)InnerData.BranchHead).SetItem(value, null); 
			}
		}
		private void ClearBranchHead(DateTime? moment)
		{
			((ILookupHelper<HeadEmployee>)InnerData.BranchHead).ClearLookup(moment);
		}

		#endregion
		#region Members for interface INeo4jBase

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
		public System.DateTime? CreatedOn { get { LazyGet(); return InnerData.CreatedOn; } set { if (LazySet(Members.CreatedOn, InnerData.CreatedOn, value)) InnerData.CreatedOn = value; } }
		public string CreatedBy { get { LazyGet(); return InnerData.CreatedBy; } set { if (LazySet(Members.CreatedBy, InnerData.CreatedBy, value)) InnerData.CreatedBy = value; } }
		public System.DateTime LastModifiedOn { get { LazyGet(); return InnerData.LastModifiedOn; } set { if (LazySet(Members.LastModifiedOn, InnerData.LastModifiedOn, value)) InnerData.LastModifiedOn = value; } }
		protected override DateTime GetRowVersion() { return LastModifiedOn; }
		public override void SetRowVersion(DateTime? value) { LastModifiedOn = value ?? DateTime.MinValue; }
		public string LastModifiedBy { get { LazyGet(); return InnerData.LastModifiedBy; } set { if (LazySet(Members.LastModifiedBy, InnerData.LastModifiedBy, value)) InnerData.LastModifiedBy = value; } }
		public string Description { get { LazyGet(); return InnerData.Description; } set { if (LazySet(Members.Description, InnerData.Description, value)) InnerData.Description = value; } }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

		private static BranchMembers members = null;
		public static BranchMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(Branch))
					{
						if (members is null)
							members = new BranchMembers();
					}
				}
				return members;
			}
		}
		public class BranchMembers
		{
			internal BranchMembers() { }

			#region Members for interface IBranch

			public Property Name { get; } = MemgraphTestApp.HumanResources.Model.Entities["Branch"].Properties["Name"];
			public Property Departments { get; } = MemgraphTestApp.HumanResources.Model.Entities["Branch"].Properties["Departments"];
			public Property BranchHead { get; } = MemgraphTestApp.HumanResources.Model.Entities["Branch"].Properties["BranchHead"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Uid"];
			public Property CreatedOn { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedOn"];
			public Property CreatedBy { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["CreatedBy"];
			public Property LastModifiedOn { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedOn"];
			public Property LastModifiedBy { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["LastModifiedBy"];
			public Property Description { get; } = MemgraphTestApp.HumanResources.Model.Entities["Neo4jBase"].Properties["Description"];
			#endregion

		}

		private static BranchFullTextMembers fullTextMembers = null;
		public static BranchFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(Branch))
					{
						if (fullTextMembers is null)
							fullTextMembers = new BranchFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class BranchFullTextMembers
		{
			internal BranchFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(Branch))
				{
					if (entity is null)
						entity = MemgraphTestApp.HumanResources.Model.Entities["Branch"];
				}
			}
			return entity;
		}

		private static BranchEvents events = null;
		public static BranchEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(Branch))
					{
						if (events is null)
							events = new BranchEvents();
					}
				}
				return events;
			}
		}
		public class BranchEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<Branch, EntityEventArgs> onNew;
			public event EventHandler<Branch, EntityEventArgs> OnNew
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
						if (onNew is null && onNewIsRegistered)
						{
							Entity.Events.OnNew -= onNewProxy;
							onNewIsRegistered = false;
						}
					}
				}
			}
			
			private void onNewProxy(object sender, EntityEventArgs args)
			{
				EventHandler<Branch, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((Branch)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<Branch, EntityEventArgs> onDelete;
			public event EventHandler<Branch, EntityEventArgs> OnDelete
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
						if (onDelete is null && onDeleteIsRegistered)
						{
							Entity.Events.OnDelete -= onDeleteProxy;
							onDeleteIsRegistered = false;
						}
					}
				}
			}
			
			private void onDeleteProxy(object sender, EntityEventArgs args)
			{
				EventHandler<Branch, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((Branch)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<Branch, EntityEventArgs> onSave;
			public event EventHandler<Branch, EntityEventArgs> OnSave
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
						if (onSave is null && onSaveIsRegistered)
						{
							Entity.Events.OnSave -= onSaveProxy;
							onSaveIsRegistered = false;
						}
					}
				}
			}
			
			private void onSaveProxy(object sender, EntityEventArgs args)
			{
				EventHandler<Branch, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((Branch)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<Branch, EntityEventArgs> onAfterSave;
			public event EventHandler<Branch, EntityEventArgs> OnAfterSave
			{
				add
				{
					lock (this)
					{
						if (!onAfterSaveIsRegistered)
						{
							Entity.Events.OnAfterSave -= onAfterSaveProxy;
							Entity.Events.OnAfterSave += onAfterSaveProxy;
							onAfterSaveIsRegistered = true;
						}
						onAfterSave += value;
					}
				}
				remove
				{
					lock (this)
					{
						onAfterSave -= value;
						if (onAfterSave is null && onAfterSaveIsRegistered)
						{
							Entity.Events.OnAfterSave -= onAfterSaveProxy;
							onAfterSaveIsRegistered = false;
						}
					}
				}
			}
			
			private void onAfterSaveProxy(object sender, EntityEventArgs args)
			{
				EventHandler<Branch, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((Branch)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Branch, PropertyEventArgs> onName;
				public static event EventHandler<Branch, PropertyEventArgs> OnName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onNameIsRegistered)
							{
								Members.Name.Events.OnChange -= onNameProxy;
								Members.Name.Events.OnChange += onNameProxy;
								onNameIsRegistered = true;
							}
							onName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onName -= value;
							if (onName is null && onNameIsRegistered)
							{
								Members.Name.Events.OnChange -= onNameProxy;
								onNameIsRegistered = false;
							}
						}
					}
				}
			
				private static void onNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Branch, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((Branch)sender, args);
				}

				#endregion

				#region OnDepartments

				private static bool onDepartmentsIsRegistered = false;

				private static EventHandler<Branch, PropertyEventArgs> onDepartments;
				public static event EventHandler<Branch, PropertyEventArgs> OnDepartments
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDepartmentsIsRegistered)
							{
								Members.Departments.Events.OnChange -= onDepartmentsProxy;
								Members.Departments.Events.OnChange += onDepartmentsProxy;
								onDepartmentsIsRegistered = true;
							}
							onDepartments += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDepartments -= value;
							if (onDepartments is null && onDepartmentsIsRegistered)
							{
								Members.Departments.Events.OnChange -= onDepartmentsProxy;
								onDepartmentsIsRegistered = false;
							}
						}
					}
				}
			
				private static void onDepartmentsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Branch, PropertyEventArgs> handler = onDepartments;
					if (handler is not null)
						handler.Invoke((Branch)sender, args);
				}

				#endregion

				#region OnBranchHead

				private static bool onBranchHeadIsRegistered = false;

				private static EventHandler<Branch, PropertyEventArgs> onBranchHead;
				public static event EventHandler<Branch, PropertyEventArgs> OnBranchHead
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onBranchHeadIsRegistered)
							{
								Members.BranchHead.Events.OnChange -= onBranchHeadProxy;
								Members.BranchHead.Events.OnChange += onBranchHeadProxy;
								onBranchHeadIsRegistered = true;
							}
							onBranchHead += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onBranchHead -= value;
							if (onBranchHead is null && onBranchHeadIsRegistered)
							{
								Members.BranchHead.Events.OnChange -= onBranchHeadProxy;
								onBranchHeadIsRegistered = false;
							}
						}
					}
				}
			
				private static void onBranchHeadProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Branch, PropertyEventArgs> handler = onBranchHead;
					if (handler is not null)
						handler.Invoke((Branch)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Branch, PropertyEventArgs> onUid;
				public static event EventHandler<Branch, PropertyEventArgs> OnUid
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
							if (onUid is null && onUidIsRegistered)
							{
								Members.Uid.Events.OnChange -= onUidProxy;
								onUidIsRegistered = false;
							}
						}
					}
				}
			
				private static void onUidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Branch, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((Branch)sender, args);
				}

				#endregion

				#region OnCreatedOn

				private static bool onCreatedOnIsRegistered = false;

				private static EventHandler<Branch, PropertyEventArgs> onCreatedOn;
				public static event EventHandler<Branch, PropertyEventArgs> OnCreatedOn
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCreatedOnIsRegistered)
							{
								Members.CreatedOn.Events.OnChange -= onCreatedOnProxy;
								Members.CreatedOn.Events.OnChange += onCreatedOnProxy;
								onCreatedOnIsRegistered = true;
							}
							onCreatedOn += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCreatedOn -= value;
							if (onCreatedOn is null && onCreatedOnIsRegistered)
							{
								Members.CreatedOn.Events.OnChange -= onCreatedOnProxy;
								onCreatedOnIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCreatedOnProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Branch, PropertyEventArgs> handler = onCreatedOn;
					if (handler is not null)
						handler.Invoke((Branch)sender, args);
				}

				#endregion

				#region OnCreatedBy

				private static bool onCreatedByIsRegistered = false;

				private static EventHandler<Branch, PropertyEventArgs> onCreatedBy;
				public static event EventHandler<Branch, PropertyEventArgs> OnCreatedBy
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCreatedByIsRegistered)
							{
								Members.CreatedBy.Events.OnChange -= onCreatedByProxy;
								Members.CreatedBy.Events.OnChange += onCreatedByProxy;
								onCreatedByIsRegistered = true;
							}
							onCreatedBy += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCreatedBy -= value;
							if (onCreatedBy is null && onCreatedByIsRegistered)
							{
								Members.CreatedBy.Events.OnChange -= onCreatedByProxy;
								onCreatedByIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCreatedByProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Branch, PropertyEventArgs> handler = onCreatedBy;
					if (handler is not null)
						handler.Invoke((Branch)sender, args);
				}

				#endregion

				#region OnLastModifiedOn

				private static bool onLastModifiedOnIsRegistered = false;

				private static EventHandler<Branch, PropertyEventArgs> onLastModifiedOn;
				public static event EventHandler<Branch, PropertyEventArgs> OnLastModifiedOn
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLastModifiedOnIsRegistered)
							{
								Members.LastModifiedOn.Events.OnChange -= onLastModifiedOnProxy;
								Members.LastModifiedOn.Events.OnChange += onLastModifiedOnProxy;
								onLastModifiedOnIsRegistered = true;
							}
							onLastModifiedOn += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLastModifiedOn -= value;
							if (onLastModifiedOn is null && onLastModifiedOnIsRegistered)
							{
								Members.LastModifiedOn.Events.OnChange -= onLastModifiedOnProxy;
								onLastModifiedOnIsRegistered = false;
							}
						}
					}
				}
			
				private static void onLastModifiedOnProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Branch, PropertyEventArgs> handler = onLastModifiedOn;
					if (handler is not null)
						handler.Invoke((Branch)sender, args);
				}

				#endregion

				#region OnLastModifiedBy

				private static bool onLastModifiedByIsRegistered = false;

				private static EventHandler<Branch, PropertyEventArgs> onLastModifiedBy;
				public static event EventHandler<Branch, PropertyEventArgs> OnLastModifiedBy
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLastModifiedByIsRegistered)
							{
								Members.LastModifiedBy.Events.OnChange -= onLastModifiedByProxy;
								Members.LastModifiedBy.Events.OnChange += onLastModifiedByProxy;
								onLastModifiedByIsRegistered = true;
							}
							onLastModifiedBy += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLastModifiedBy -= value;
							if (onLastModifiedBy is null && onLastModifiedByIsRegistered)
							{
								Members.LastModifiedBy.Events.OnChange -= onLastModifiedByProxy;
								onLastModifiedByIsRegistered = false;
							}
						}
					}
				}
			
				private static void onLastModifiedByProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Branch, PropertyEventArgs> handler = onLastModifiedBy;
					if (handler is not null)
						handler.Invoke((Branch)sender, args);
				}

				#endregion

				#region OnDescription

				private static bool onDescriptionIsRegistered = false;

				private static EventHandler<Branch, PropertyEventArgs> onDescription;
				public static event EventHandler<Branch, PropertyEventArgs> OnDescription
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDescriptionIsRegistered)
							{
								Members.Description.Events.OnChange -= onDescriptionProxy;
								Members.Description.Events.OnChange += onDescriptionProxy;
								onDescriptionIsRegistered = true;
							}
							onDescription += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDescription -= value;
							if (onDescription is null && onDescriptionIsRegistered)
							{
								Members.Description.Events.OnChange -= onDescriptionProxy;
								onDescriptionIsRegistered = false;
							}
						}
					}
				}
			
				private static void onDescriptionProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Branch, PropertyEventArgs> handler = onDescription;
					if (handler is not null)
						handler.Invoke((Branch)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IBranchOriginalData

		public IBranchOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IBranch

		string IBranchOriginalData.Name { get { return OriginalData.Name; } }
		IEnumerable<Department> IBranchOriginalData.Departments { get { return OriginalData.Departments.OriginalData; } }
		HeadEmployee IBranchOriginalData.BranchHead { get { return ((ILookupHelper<HeadEmployee>)OriginalData.BranchHead).GetOriginalItem(null); } }

		#endregion
		#region Members for interface INeo4jBase

		INeo4jBaseOriginalData INeo4jBase.OriginalVersion { get { return this; } }

		string INeo4jBaseOriginalData.Uid { get { return OriginalData.Uid; } }
		System.DateTime? INeo4jBaseOriginalData.CreatedOn { get { return OriginalData.CreatedOn; } }
		string INeo4jBaseOriginalData.CreatedBy { get { return OriginalData.CreatedBy; } }
		System.DateTime INeo4jBaseOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }
		string INeo4jBaseOriginalData.LastModifiedBy { get { return OriginalData.LastModifiedBy; } }
		string INeo4jBaseOriginalData.Description { get { return OriginalData.Description; } }

		#endregion
		#endregion
	}
}