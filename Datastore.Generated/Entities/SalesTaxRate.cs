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
	public interface ISalesTaxRateOriginalData : ISchemaBaseOriginalData
	{
		string TaxType { get; }
		string TaxRate { get; }
		string Name { get; }
		string rowguid { get; }
		StateProvince StateProvince { get; }
	}

	public partial class SalesTaxRate : OGM<SalesTaxRate, SalesTaxRate.SalesTaxRateData, System.String>, ISchemaBase, INeo4jBase, ISalesTaxRateOriginalData
	{
		#region Initialize

		static SalesTaxRate()
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

		public static Dictionary<System.String, SalesTaxRate> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.SalesTaxRateAlias, IWhereQuery> query)
		{
			q.SalesTaxRateAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.SalesTaxRate.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"SalesTaxRate => TaxType : {this.TaxType}, TaxRate : {this.TaxRate}, Name : {this.Name}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new SalesTaxRateData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.TaxType is null)
				throw new PersistenceException(string.Format("Cannot save SalesTaxRate with key '{0}' because the TaxType cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.TaxRate is null)
				throw new PersistenceException(string.Format("Cannot save SalesTaxRate with key '{0}' because the TaxRate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Name is null)
				throw new PersistenceException(string.Format("Cannot save SalesTaxRate with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid is null)
				throw new PersistenceException(string.Format("Cannot save SalesTaxRate with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class SalesTaxRateData : Data<System.String>
		{
			public SalesTaxRateData()
			{

			}

			public SalesTaxRateData(SalesTaxRateData data)
			{
				TaxType = data.TaxType;
				TaxRate = data.TaxRate;
				Name = data.Name;
				rowguid = data.rowguid;
				StateProvince = data.StateProvince;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "SalesTaxRate";

				StateProvince = new EntityCollection<StateProvince>(Wrapper, Members.StateProvince);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("TaxType",  TaxType);
				dictionary.Add("TaxRate",  TaxRate);
				dictionary.Add("Name",  Name);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("TaxType", out value))
					TaxType = (string)value;
				if (properties.TryGetValue("TaxRate", out value))
					TaxRate = (string)value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ISalesTaxRate

			public string TaxType { get; set; }
			public string TaxRate { get; set; }
			public string Name { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<StateProvince> StateProvince { get; private set; }

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

		#region Members for interface ISalesTaxRate

		public string TaxType { get { LazyGet(); return InnerData.TaxType; } set { if (LazySet(Members.TaxType, InnerData.TaxType, value)) InnerData.TaxType = value; } }
		public string TaxRate { get { LazyGet(); return InnerData.TaxRate; } set { if (LazySet(Members.TaxRate, InnerData.TaxRate, value)) InnerData.TaxRate = value; } }
		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public StateProvince StateProvince
		{
			get { return ((ILookupHelper<StateProvince>)InnerData.StateProvince).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.StateProvince, ((ILookupHelper<StateProvince>)InnerData.StateProvince).GetItem(null), value))
					((ILookupHelper<StateProvince>)InnerData.StateProvince).SetItem(value, null); 
			}
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

		private static SalesTaxRateMembers members = null;
		public static SalesTaxRateMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(SalesTaxRate))
					{
						if (members is null)
							members = new SalesTaxRateMembers();
					}
				}
				return members;
			}
		}
		public class SalesTaxRateMembers
		{
			internal SalesTaxRateMembers() { }

			#region Members for interface ISalesTaxRate

			public Property TaxType { get; } = Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["TaxType"];
			public Property TaxRate { get; } = Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["TaxRate"];
			public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["Name"];
			public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["rowguid"];
			public Property StateProvince { get; } = Datastore.AdventureWorks.Model.Entities["SalesTaxRate"].Properties["StateProvince"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static SalesTaxRateFullTextMembers fullTextMembers = null;
		public static SalesTaxRateFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(SalesTaxRate))
					{
						if (fullTextMembers is null)
							fullTextMembers = new SalesTaxRateFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class SalesTaxRateFullTextMembers
		{
			internal SalesTaxRateFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(SalesTaxRate))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["SalesTaxRate"];
				}
			}
			return entity;
		}

		private static SalesTaxRateEvents events = null;
		public static SalesTaxRateEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(SalesTaxRate))
					{
						if (events is null)
							events = new SalesTaxRateEvents();
					}
				}
				return events;
			}
		}
		public class SalesTaxRateEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<SalesTaxRate, EntityEventArgs> onNew;
			public event EventHandler<SalesTaxRate, EntityEventArgs> OnNew
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
				EventHandler<SalesTaxRate, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((SalesTaxRate)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<SalesTaxRate, EntityEventArgs> onDelete;
			public event EventHandler<SalesTaxRate, EntityEventArgs> OnDelete
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
				EventHandler<SalesTaxRate, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((SalesTaxRate)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<SalesTaxRate, EntityEventArgs> onSave;
			public event EventHandler<SalesTaxRate, EntityEventArgs> OnSave
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
				EventHandler<SalesTaxRate, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((SalesTaxRate)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<SalesTaxRate, EntityEventArgs> onAfterSave;
			public event EventHandler<SalesTaxRate, EntityEventArgs> OnAfterSave
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
				EventHandler<SalesTaxRate, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((SalesTaxRate)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnTaxType

				private static bool onTaxTypeIsRegistered = false;

				private static EventHandler<SalesTaxRate, PropertyEventArgs> onTaxType;
				public static event EventHandler<SalesTaxRate, PropertyEventArgs> OnTaxType
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTaxTypeIsRegistered)
							{
								Members.TaxType.Events.OnChange -= onTaxTypeProxy;
								Members.TaxType.Events.OnChange += onTaxTypeProxy;
								onTaxTypeIsRegistered = true;
							}
							onTaxType += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTaxType -= value;
							if (onTaxType is null && onTaxTypeIsRegistered)
							{
								Members.TaxType.Events.OnChange -= onTaxTypeProxy;
								onTaxTypeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onTaxTypeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTaxRate, PropertyEventArgs> handler = onTaxType;
					if (handler is not null)
						handler.Invoke((SalesTaxRate)sender, args);
				}

				#endregion

				#region OnTaxRate

				private static bool onTaxRateIsRegistered = false;

				private static EventHandler<SalesTaxRate, PropertyEventArgs> onTaxRate;
				public static event EventHandler<SalesTaxRate, PropertyEventArgs> OnTaxRate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTaxRateIsRegistered)
							{
								Members.TaxRate.Events.OnChange -= onTaxRateProxy;
								Members.TaxRate.Events.OnChange += onTaxRateProxy;
								onTaxRateIsRegistered = true;
							}
							onTaxRate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTaxRate -= value;
							if (onTaxRate is null && onTaxRateIsRegistered)
							{
								Members.TaxRate.Events.OnChange -= onTaxRateProxy;
								onTaxRateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onTaxRateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTaxRate, PropertyEventArgs> handler = onTaxRate;
					if (handler is not null)
						handler.Invoke((SalesTaxRate)sender, args);
				}

				#endregion

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<SalesTaxRate, PropertyEventArgs> onName;
				public static event EventHandler<SalesTaxRate, PropertyEventArgs> OnName
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
					EventHandler<SalesTaxRate, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((SalesTaxRate)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<SalesTaxRate, PropertyEventArgs> onrowguid;
				public static event EventHandler<SalesTaxRate, PropertyEventArgs> Onrowguid
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
							if (onrowguid is null && onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								onrowguidIsRegistered = false;
							}
						}
					}
				}
			
				private static void onrowguidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTaxRate, PropertyEventArgs> handler = onrowguid;
					if (handler is not null)
						handler.Invoke((SalesTaxRate)sender, args);
				}

				#endregion

				#region OnStateProvince

				private static bool onStateProvinceIsRegistered = false;

				private static EventHandler<SalesTaxRate, PropertyEventArgs> onStateProvince;
				public static event EventHandler<SalesTaxRate, PropertyEventArgs> OnStateProvince
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStateProvinceIsRegistered)
							{
								Members.StateProvince.Events.OnChange -= onStateProvinceProxy;
								Members.StateProvince.Events.OnChange += onStateProvinceProxy;
								onStateProvinceIsRegistered = true;
							}
							onStateProvince += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStateProvince -= value;
							if (onStateProvince is null && onStateProvinceIsRegistered)
							{
								Members.StateProvince.Events.OnChange -= onStateProvinceProxy;
								onStateProvinceIsRegistered = false;
							}
						}
					}
				}
			
				private static void onStateProvinceProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesTaxRate, PropertyEventArgs> handler = onStateProvince;
					if (handler is not null)
						handler.Invoke((SalesTaxRate)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<SalesTaxRate, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<SalesTaxRate, PropertyEventArgs> OnModifiedDate
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
					EventHandler<SalesTaxRate, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((SalesTaxRate)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<SalesTaxRate, PropertyEventArgs> onUid;
				public static event EventHandler<SalesTaxRate, PropertyEventArgs> OnUid
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
					EventHandler<SalesTaxRate, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((SalesTaxRate)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region ISalesTaxRateOriginalData

		public ISalesTaxRateOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ISalesTaxRate

		string ISalesTaxRateOriginalData.TaxType { get { return OriginalData.TaxType; } }
		string ISalesTaxRateOriginalData.TaxRate { get { return OriginalData.TaxRate; } }
		string ISalesTaxRateOriginalData.Name { get { return OriginalData.Name; } }
		string ISalesTaxRateOriginalData.rowguid { get { return OriginalData.rowguid; } }
		StateProvince ISalesTaxRateOriginalData.StateProvince { get { return ((ILookupHelper<StateProvince>)OriginalData.StateProvince).GetOriginalItem(null); } }

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