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
	public interface IUnitMeasureOriginalData : ISchemaBaseOriginalData
	{
		string UnitMeasureCorde { get; }
		string Name { get; }
	}

	public partial class UnitMeasure : OGM<UnitMeasure, UnitMeasure.UnitMeasureData, System.String>, ISchemaBase, INeo4jBase, IUnitMeasureOriginalData
	{
		#region Initialize

		static UnitMeasure()
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

		public static Dictionary<System.String, UnitMeasure> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.UnitMeasureAlias, IWhereQuery> query)
		{
			q.UnitMeasureAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.UnitMeasure.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"UnitMeasure => UnitMeasureCorde : {this.UnitMeasureCorde}, Name : {this.Name}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new UnitMeasureData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.UnitMeasureCorde is null)
				throw new PersistenceException(string.Format("Cannot save UnitMeasure with key '{0}' because the UnitMeasureCorde cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Name is null)
				throw new PersistenceException(string.Format("Cannot save UnitMeasure with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class UnitMeasureData : Data<System.String>
		{
			public UnitMeasureData()
			{

			}

			public UnitMeasureData(UnitMeasureData data)
			{
				UnitMeasureCorde = data.UnitMeasureCorde;
				Name = data.Name;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "UnitMeasure";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("UnitMeasureCorde",  UnitMeasureCorde);
				dictionary.Add("Name",  Name);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("UnitMeasureCorde", out value))
					UnitMeasureCorde = (string)value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IUnitMeasure

			public string UnitMeasureCorde { get; set; }
			public string Name { get; set; }

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

		#region Members for interface IUnitMeasure

		public string UnitMeasureCorde { get { LazyGet(); return InnerData.UnitMeasureCorde; } set { if (LazySet(Members.UnitMeasureCorde, InnerData.UnitMeasureCorde, value)) InnerData.UnitMeasureCorde = value; } }
		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }

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

		private static UnitMeasureMembers members = null;
		public static UnitMeasureMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(UnitMeasure))
					{
						if (members is null)
							members = new UnitMeasureMembers();
					}
				}
				return members;
			}
		}
		public class UnitMeasureMembers
		{
			internal UnitMeasureMembers() { }

			#region Members for interface IUnitMeasure

			public Property UnitMeasureCorde { get; } = Datastore.AdventureWorks.Model.Entities["UnitMeasure"].Properties["UnitMeasureCorde"];
			public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["UnitMeasure"].Properties["Name"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static UnitMeasureFullTextMembers fullTextMembers = null;
		public static UnitMeasureFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(UnitMeasure))
					{
						if (fullTextMembers is null)
							fullTextMembers = new UnitMeasureFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class UnitMeasureFullTextMembers
		{
			internal UnitMeasureFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(UnitMeasure))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["UnitMeasure"];
				}
			}
			return entity;
		}

		private static UnitMeasureEvents events = null;
		public static UnitMeasureEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(UnitMeasure))
					{
						if (events is null)
							events = new UnitMeasureEvents();
					}
				}
				return events;
			}
		}
		public class UnitMeasureEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<UnitMeasure, EntityEventArgs> onNew;
			public event EventHandler<UnitMeasure, EntityEventArgs> OnNew
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
				EventHandler<UnitMeasure, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((UnitMeasure)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<UnitMeasure, EntityEventArgs> onDelete;
			public event EventHandler<UnitMeasure, EntityEventArgs> OnDelete
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
				EventHandler<UnitMeasure, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((UnitMeasure)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<UnitMeasure, EntityEventArgs> onSave;
			public event EventHandler<UnitMeasure, EntityEventArgs> OnSave
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
				EventHandler<UnitMeasure, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((UnitMeasure)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<UnitMeasure, EntityEventArgs> onAfterSave;
			public event EventHandler<UnitMeasure, EntityEventArgs> OnAfterSave
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
				EventHandler<UnitMeasure, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((UnitMeasure)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnUnitMeasureCorde

				private static bool onUnitMeasureCordeIsRegistered = false;

				private static EventHandler<UnitMeasure, PropertyEventArgs> onUnitMeasureCorde;
				public static event EventHandler<UnitMeasure, PropertyEventArgs> OnUnitMeasureCorde
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onUnitMeasureCordeIsRegistered)
							{
								Members.UnitMeasureCorde.Events.OnChange -= onUnitMeasureCordeProxy;
								Members.UnitMeasureCorde.Events.OnChange += onUnitMeasureCordeProxy;
								onUnitMeasureCordeIsRegistered = true;
							}
							onUnitMeasureCorde += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onUnitMeasureCorde -= value;
							if (onUnitMeasureCorde is null && onUnitMeasureCordeIsRegistered)
							{
								Members.UnitMeasureCorde.Events.OnChange -= onUnitMeasureCordeProxy;
								onUnitMeasureCordeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onUnitMeasureCordeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<UnitMeasure, PropertyEventArgs> handler = onUnitMeasureCorde;
					if (handler is not null)
						handler.Invoke((UnitMeasure)sender, args);
				}

				#endregion

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<UnitMeasure, PropertyEventArgs> onName;
				public static event EventHandler<UnitMeasure, PropertyEventArgs> OnName
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
					EventHandler<UnitMeasure, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((UnitMeasure)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<UnitMeasure, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<UnitMeasure, PropertyEventArgs> OnModifiedDate
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
							if (onModifiedDate is null && onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								onModifiedDateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onModifiedDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<UnitMeasure, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((UnitMeasure)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<UnitMeasure, PropertyEventArgs> onUid;
				public static event EventHandler<UnitMeasure, PropertyEventArgs> OnUid
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
					EventHandler<UnitMeasure, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((UnitMeasure)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IUnitMeasureOriginalData

		public IUnitMeasureOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IUnitMeasure

		string IUnitMeasureOriginalData.UnitMeasureCorde { get { return OriginalData.UnitMeasureCorde; } }
		string IUnitMeasureOriginalData.Name { get { return OriginalData.Name; } }

		#endregion
		#region Members for interface ISchemaBase

		ISchemaBaseOriginalData ISchemaBase.OriginalVersion { get { return this; } }

		System.DateTime ISchemaBaseOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		INeo4jBaseOriginalData INeo4jBase.OriginalVersion { get { return this; } }

		string INeo4jBaseOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}