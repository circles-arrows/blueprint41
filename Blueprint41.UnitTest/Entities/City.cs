using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;

namespace Datastore.Manipulation
{
	public interface ICityOriginalData : IBaseEntityOriginalData
	{
		string Name { get; }
		IEnumerable<Restaurant> Restraurants { get; }
	}

	public partial class City : OGM<City, City.CityData, System.String>, IBaseEntity, ICityOriginalData
	{
		#region Initialize

		static City()
		{
			Register.Types();
		}


		protected override void RegisterGeneratedStoredQueries()
		{
			#region LoadByKeys
			
			RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
				Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

			#endregion

			#region LoadByName

			RegisterQuery(nameof(LoadByName), (query, alias) => query.
				Where(alias.Name == Parameter.New<string>(Param0)));

			#endregion

			AdditionalGeneratedStoredQueries();
		}
		public static City LoadByName(string name)
		{
			return FromQuery(nameof(LoadByName), new Parameter(Param0, name)).FirstOrDefault();
		}
		partial void AdditionalGeneratedStoredQueries();

		public static Dictionary<System.String, City> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.CityAlias, IWhereQuery> query)
		{
			q.CityAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.City.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"City => Name : {this.Name?.ToString() ?? "null"}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
					OriginalData = new CityData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class CityData : Data<System.String>
		{
			public CityData()
			{

			}

			public CityData(CityData data)
			{
				Name = data.Name;
				Restraurants = data.Restraurants;
				Uid = data.Uid;
				LastModifiedOn = data.LastModifiedOn;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "City";

				Restraurants = new EntityCollection<Restaurant>(Wrapper, Members.Restraurants, item => { if (Members.Restraurants.Events.HasRegisteredChangeHandlers) { object loadHack = item.City; } });
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
				dictionary.Add("LastModifiedOn",  Conversion<System.DateTime, long>.Convert(LastModifiedOn));
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("LastModifiedOn", out value))
					LastModifiedOn = Conversion<long, System.DateTime>.Convert((long)value);
			}

			#endregion

			#region Members for interface ICity

			public string Name { get; set; }
			public EntityCollection<Restaurant> Restraurants { get; private set; }

			#endregion
			#region Members for interface IBaseEntity

			public string Uid { get; set; }
			public System.DateTime LastModifiedOn { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface ICity

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public EntityCollection<Restaurant> Restraurants { get { return InnerData.Restraurants; } }
		private void ClearRestraurants(DateTime? moment)
		{
			((ILookupHelper<Restaurant>)InnerData.Restraurants).ClearLookup(moment);
		}

		#endregion
		#region Members for interface IBaseEntity

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
		public System.DateTime LastModifiedOn { get { LazyGet(); return InnerData.LastModifiedOn; } set { if (LazySet(Members.LastModifiedOn, InnerData.LastModifiedOn, value)) InnerData.LastModifiedOn = value; } }
		protected override DateTime GetRowVersion() { return LastModifiedOn; }
		public override void SetRowVersion(DateTime? value) { LastModifiedOn = value ?? DateTime.MinValue; }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

		private static CityMembers members = null;
		public static CityMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(City))
					{
						if (members is null)
							members = new CityMembers();
					}
				}
				return members;
			}
		}
		public class CityMembers
		{
			internal CityMembers() { }

			#region Members for interface ICity

			public Property Name { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Name"];
			public Property Restraurants { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"].Properties["Restraurants"];
			#endregion

			#region Members for interface IBaseEntity

			public Property Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
			public Property LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
			#endregion

		}

		private static CityFullTextMembers fullTextMembers = null;
		public static CityFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(City))
					{
						if (fullTextMembers is null)
							fullTextMembers = new CityFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class CityFullTextMembers
		{
			internal CityFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(City))
				{
					if (entity is null)
						entity = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"];
				}
			}
			return entity;
		}

		private static CityEvents events = null;
		public static CityEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(City))
					{
						if (events is null)
							events = new CityEvents();
					}
				}
				return events;
			}
		}
		public class CityEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<City, EntityEventArgs> onNew;
			public event EventHandler<City, EntityEventArgs> OnNew
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
				EventHandler<City, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((City)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<City, EntityEventArgs> onDelete;
			public event EventHandler<City, EntityEventArgs> OnDelete
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
				EventHandler<City, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((City)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<City, EntityEventArgs> onSave;
			public event EventHandler<City, EntityEventArgs> OnSave
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
				EventHandler<City, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((City)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<City, EntityEventArgs> onAfterSave;
			public event EventHandler<City, EntityEventArgs> OnAfterSave
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
				EventHandler<City, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((City)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<City, PropertyEventArgs> onName;
				public static event EventHandler<City, PropertyEventArgs> OnName
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
					EventHandler<City, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((City)sender, args);
				}

				#endregion

				#region OnRestraurants

				private static bool onRestraurantsIsRegistered = false;

				private static EventHandler<City, PropertyEventArgs> onRestraurants;
				public static event EventHandler<City, PropertyEventArgs> OnRestraurants
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onRestraurantsIsRegistered)
							{
								Members.Restraurants.Events.OnChange -= onRestraurantsProxy;
								Members.Restraurants.Events.OnChange += onRestraurantsProxy;
								onRestraurantsIsRegistered = true;
							}
							onRestraurants += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onRestraurants -= value;
							if (onRestraurants is null && onRestraurantsIsRegistered)
							{
								Members.Restraurants.Events.OnChange -= onRestraurantsProxy;
								onRestraurantsIsRegistered = false;
							}
						}
					}
				}
			
				private static void onRestraurantsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<City, PropertyEventArgs> handler = onRestraurants;
					if (handler is not null)
						handler.Invoke((City)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<City, PropertyEventArgs> onUid;
				public static event EventHandler<City, PropertyEventArgs> OnUid
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
					EventHandler<City, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((City)sender, args);
				}

				#endregion

				#region OnLastModifiedOn

				private static bool onLastModifiedOnIsRegistered = false;

				private static EventHandler<City, PropertyEventArgs> onLastModifiedOn;
				public static event EventHandler<City, PropertyEventArgs> OnLastModifiedOn
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
					EventHandler<City, PropertyEventArgs> handler = onLastModifiedOn;
					if (handler is not null)
						handler.Invoke((City)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region ICityOriginalData

		public ICityOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ICity

		string ICityOriginalData.Name { get { return OriginalData.Name; } }
		IEnumerable<Restaurant> ICityOriginalData.Restraurants { get { return OriginalData.Restraurants.OriginalData; } }

		#endregion
		#region Members for interface IBaseEntity

		IBaseEntityOriginalData IBaseEntity.OriginalVersion { get { return this; } }

		string IBaseEntityOriginalData.Uid { get { return OriginalData.Uid; } }
		System.DateTime IBaseEntityOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

		#endregion
		#endregion
	}
}