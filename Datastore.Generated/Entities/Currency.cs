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
	public interface ICurrencyOriginalData : ISchemaBaseOriginalData
	{
		string CurrencyCode { get; }
		string Name { get; }
	}

	public partial class Currency : OGM<Currency, Currency.CurrencyData, System.String>, ISchemaBase, INeo4jBase, ICurrencyOriginalData
	{
		#region Initialize

		static Currency()
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

		public static Dictionary<System.String, Currency> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.CurrencyAlias, IWhereQuery> query)
		{
			q.CurrencyAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Currency.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"Currency => CurrencyCode : {this.CurrencyCode}, Name : {this.Name}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new CurrencyData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.CurrencyCode is null)
				throw new PersistenceException(string.Format("Cannot save Currency with key '{0}' because the CurrencyCode cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Name is null)
				throw new PersistenceException(string.Format("Cannot save Currency with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class CurrencyData : Data<System.String>
		{
			public CurrencyData()
			{

			}

			public CurrencyData(CurrencyData data)
			{
				CurrencyCode = data.CurrencyCode;
				Name = data.Name;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Currency";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("CurrencyCode",  CurrencyCode);
				dictionary.Add("Name",  Name);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("CurrencyCode", out value))
					CurrencyCode = (string)value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ICurrency

			public string CurrencyCode { get; set; }
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

		#region Members for interface ICurrency

		public string CurrencyCode { get { LazyGet(); return InnerData.CurrencyCode; } set { if (LazySet(Members.CurrencyCode, InnerData.CurrencyCode, value)) InnerData.CurrencyCode = value; } }
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

		private static CurrencyMembers members = null;
		public static CurrencyMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(Currency))
					{
						if (members is null)
							members = new CurrencyMembers();
					}
				}
				return members;
			}
		}
		public class CurrencyMembers
		{
			internal CurrencyMembers() { }

			#region Members for interface ICurrency

			public Property CurrencyCode { get; } = Datastore.AdventureWorks.Model.Entities["Currency"].Properties["CurrencyCode"];
			public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["Currency"].Properties["Name"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static CurrencyFullTextMembers fullTextMembers = null;
		public static CurrencyFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(Currency))
					{
						if (fullTextMembers is null)
							fullTextMembers = new CurrencyFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class CurrencyFullTextMembers
		{
			internal CurrencyFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(Currency))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["Currency"];
				}
			}
			return entity;
		}

		private static CurrencyEvents events = null;
		public static CurrencyEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(Currency))
					{
						if (events is null)
							events = new CurrencyEvents();
					}
				}
				return events;
			}
		}
		public class CurrencyEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<Currency, EntityEventArgs> onNew;
			public event EventHandler<Currency, EntityEventArgs> OnNew
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
				EventHandler<Currency, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((Currency)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<Currency, EntityEventArgs> onDelete;
			public event EventHandler<Currency, EntityEventArgs> OnDelete
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
				EventHandler<Currency, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((Currency)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<Currency, EntityEventArgs> onSave;
			public event EventHandler<Currency, EntityEventArgs> OnSave
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
				EventHandler<Currency, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((Currency)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<Currency, EntityEventArgs> onAfterSave;
			public event EventHandler<Currency, EntityEventArgs> OnAfterSave
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
				EventHandler<Currency, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((Currency)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnCurrencyCode

				private static bool onCurrencyCodeIsRegistered = false;

				private static EventHandler<Currency, PropertyEventArgs> onCurrencyCode;
				public static event EventHandler<Currency, PropertyEventArgs> OnCurrencyCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCurrencyCodeIsRegistered)
							{
								Members.CurrencyCode.Events.OnChange -= onCurrencyCodeProxy;
								Members.CurrencyCode.Events.OnChange += onCurrencyCodeProxy;
								onCurrencyCodeIsRegistered = true;
							}
							onCurrencyCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCurrencyCode -= value;
							if (onCurrencyCode is null && onCurrencyCodeIsRegistered)
							{
								Members.CurrencyCode.Events.OnChange -= onCurrencyCodeProxy;
								onCurrencyCodeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCurrencyCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Currency, PropertyEventArgs> handler = onCurrencyCode;
					if (handler is not null)
						handler.Invoke((Currency)sender, args);
				}

				#endregion

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Currency, PropertyEventArgs> onName;
				public static event EventHandler<Currency, PropertyEventArgs> OnName
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
					EventHandler<Currency, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((Currency)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Currency, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Currency, PropertyEventArgs> OnModifiedDate
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
					EventHandler<Currency, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((Currency)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Currency, PropertyEventArgs> onUid;
				public static event EventHandler<Currency, PropertyEventArgs> OnUid
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
					EventHandler<Currency, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((Currency)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region ICurrencyOriginalData

		public ICurrencyOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ICurrency

		string ICurrencyOriginalData.CurrencyCode { get { return OriginalData.CurrencyCode; } }
		string ICurrencyOriginalData.Name { get { return OriginalData.Name; } }

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