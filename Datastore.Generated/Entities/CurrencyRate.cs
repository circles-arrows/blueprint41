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
	public interface ICurrencyRateOriginalData : ISchemaBaseOriginalData
	{
		System.DateTime CurrencyRateDate { get; }
		string FromCurrencyCode { get; }
		string ToCurrencyCode { get; }
		string AverageRate { get; }
		string EndOfDayRate { get; }
		Currency Currency { get; }
	}

	public partial class CurrencyRate : OGM<CurrencyRate, CurrencyRate.CurrencyRateData, System.String>, ISchemaBase, INeo4jBase, ICurrencyRateOriginalData
	{
		#region Initialize

		static CurrencyRate()
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

		public static Dictionary<System.String, CurrencyRate> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.CurrencyRateAlias, IWhereQuery> query)
		{
			q.CurrencyRateAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.CurrencyRate.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"CurrencyRate => CurrencyRateDate : {this.CurrencyRateDate}, FromCurrencyCode : {this.FromCurrencyCode}, ToCurrencyCode : {this.ToCurrencyCode}, AverageRate : {this.AverageRate}, EndOfDayRate : {this.EndOfDayRate}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new CurrencyRateData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.FromCurrencyCode is null)
				throw new PersistenceException(string.Format("Cannot save CurrencyRate with key '{0}' because the FromCurrencyCode cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ToCurrencyCode is null)
				throw new PersistenceException(string.Format("Cannot save CurrencyRate with key '{0}' because the ToCurrencyCode cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.AverageRate is null)
				throw new PersistenceException(string.Format("Cannot save CurrencyRate with key '{0}' because the AverageRate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.EndOfDayRate is null)
				throw new PersistenceException(string.Format("Cannot save CurrencyRate with key '{0}' because the EndOfDayRate cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class CurrencyRateData : Data<System.String>
		{
			public CurrencyRateData()
			{

			}

			public CurrencyRateData(CurrencyRateData data)
			{
				CurrencyRateDate = data.CurrencyRateDate;
				FromCurrencyCode = data.FromCurrencyCode;
				ToCurrencyCode = data.ToCurrencyCode;
				AverageRate = data.AverageRate;
				EndOfDayRate = data.EndOfDayRate;
				Currency = data.Currency;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "CurrencyRate";

				Currency = new EntityCollection<Currency>(Wrapper, Members.Currency);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("CurrencyRateDate",  Conversion<System.DateTime, long>.Convert(CurrencyRateDate));
				dictionary.Add("FromCurrencyCode",  FromCurrencyCode);
				dictionary.Add("ToCurrencyCode",  ToCurrencyCode);
				dictionary.Add("AverageRate",  AverageRate);
				dictionary.Add("EndOfDayRate",  EndOfDayRate);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("CurrencyRateDate", out value))
					CurrencyRateDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("FromCurrencyCode", out value))
					FromCurrencyCode = (string)value;
				if (properties.TryGetValue("ToCurrencyCode", out value))
					ToCurrencyCode = (string)value;
				if (properties.TryGetValue("AverageRate", out value))
					AverageRate = (string)value;
				if (properties.TryGetValue("EndOfDayRate", out value))
					EndOfDayRate = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ICurrencyRate

			public System.DateTime CurrencyRateDate { get; set; }
			public string FromCurrencyCode { get; set; }
			public string ToCurrencyCode { get; set; }
			public string AverageRate { get; set; }
			public string EndOfDayRate { get; set; }
			public EntityCollection<Currency> Currency { get; private set; }

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

		#region Members for interface ICurrencyRate

		public System.DateTime CurrencyRateDate { get { LazyGet(); return InnerData.CurrencyRateDate; } set { if (LazySet(Members.CurrencyRateDate, InnerData.CurrencyRateDate, value)) InnerData.CurrencyRateDate = value; } }
		public string FromCurrencyCode { get { LazyGet(); return InnerData.FromCurrencyCode; } set { if (LazySet(Members.FromCurrencyCode, InnerData.FromCurrencyCode, value)) InnerData.FromCurrencyCode = value; } }
		public string ToCurrencyCode { get { LazyGet(); return InnerData.ToCurrencyCode; } set { if (LazySet(Members.ToCurrencyCode, InnerData.ToCurrencyCode, value)) InnerData.ToCurrencyCode = value; } }
		public string AverageRate { get { LazyGet(); return InnerData.AverageRate; } set { if (LazySet(Members.AverageRate, InnerData.AverageRate, value)) InnerData.AverageRate = value; } }
		public string EndOfDayRate { get { LazyGet(); return InnerData.EndOfDayRate; } set { if (LazySet(Members.EndOfDayRate, InnerData.EndOfDayRate, value)) InnerData.EndOfDayRate = value; } }
		public Currency Currency
		{
			get { return ((ILookupHelper<Currency>)InnerData.Currency).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Currency, ((ILookupHelper<Currency>)InnerData.Currency).GetItem(null), value))
					((ILookupHelper<Currency>)InnerData.Currency).SetItem(value, null); 
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

		private static CurrencyRateMembers members = null;
		public static CurrencyRateMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(CurrencyRate))
					{
						if (members is null)
							members = new CurrencyRateMembers();
					}
				}
				return members;
			}
		}
		public class CurrencyRateMembers
		{
			internal CurrencyRateMembers() { }

			#region Members for interface ICurrencyRate

			public Property CurrencyRateDate { get; } = Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["CurrencyRateDate"];
			public Property FromCurrencyCode { get; } = Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["FromCurrencyCode"];
			public Property ToCurrencyCode { get; } = Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["ToCurrencyCode"];
			public Property AverageRate { get; } = Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["AverageRate"];
			public Property EndOfDayRate { get; } = Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["EndOfDayRate"];
			public Property Currency { get; } = Datastore.AdventureWorks.Model.Entities["CurrencyRate"].Properties["Currency"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static CurrencyRateFullTextMembers fullTextMembers = null;
		public static CurrencyRateFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(CurrencyRate))
					{
						if (fullTextMembers is null)
							fullTextMembers = new CurrencyRateFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class CurrencyRateFullTextMembers
		{
			internal CurrencyRateFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(CurrencyRate))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["CurrencyRate"];
				}
			}
			return entity;
		}

		private static CurrencyRateEvents events = null;
		public static CurrencyRateEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(CurrencyRate))
					{
						if (events is null)
							events = new CurrencyRateEvents();
					}
				}
				return events;
			}
		}
		public class CurrencyRateEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<CurrencyRate, EntityEventArgs> onNew;
			public event EventHandler<CurrencyRate, EntityEventArgs> OnNew
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
				EventHandler<CurrencyRate, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((CurrencyRate)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<CurrencyRate, EntityEventArgs> onDelete;
			public event EventHandler<CurrencyRate, EntityEventArgs> OnDelete
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
				EventHandler<CurrencyRate, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((CurrencyRate)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<CurrencyRate, EntityEventArgs> onSave;
			public event EventHandler<CurrencyRate, EntityEventArgs> OnSave
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
				EventHandler<CurrencyRate, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((CurrencyRate)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<CurrencyRate, EntityEventArgs> onAfterSave;
			public event EventHandler<CurrencyRate, EntityEventArgs> OnAfterSave
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
				EventHandler<CurrencyRate, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((CurrencyRate)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnCurrencyRateDate

				private static bool onCurrencyRateDateIsRegistered = false;

				private static EventHandler<CurrencyRate, PropertyEventArgs> onCurrencyRateDate;
				public static event EventHandler<CurrencyRate, PropertyEventArgs> OnCurrencyRateDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCurrencyRateDateIsRegistered)
							{
								Members.CurrencyRateDate.Events.OnChange -= onCurrencyRateDateProxy;
								Members.CurrencyRateDate.Events.OnChange += onCurrencyRateDateProxy;
								onCurrencyRateDateIsRegistered = true;
							}
							onCurrencyRateDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCurrencyRateDate -= value;
							if (onCurrencyRateDate is null && onCurrencyRateDateIsRegistered)
							{
								Members.CurrencyRateDate.Events.OnChange -= onCurrencyRateDateProxy;
								onCurrencyRateDateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCurrencyRateDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CurrencyRate, PropertyEventArgs> handler = onCurrencyRateDate;
					if (handler is not null)
						handler.Invoke((CurrencyRate)sender, args);
				}

				#endregion

				#region OnFromCurrencyCode

				private static bool onFromCurrencyCodeIsRegistered = false;

				private static EventHandler<CurrencyRate, PropertyEventArgs> onFromCurrencyCode;
				public static event EventHandler<CurrencyRate, PropertyEventArgs> OnFromCurrencyCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFromCurrencyCodeIsRegistered)
							{
								Members.FromCurrencyCode.Events.OnChange -= onFromCurrencyCodeProxy;
								Members.FromCurrencyCode.Events.OnChange += onFromCurrencyCodeProxy;
								onFromCurrencyCodeIsRegistered = true;
							}
							onFromCurrencyCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFromCurrencyCode -= value;
							if (onFromCurrencyCode is null && onFromCurrencyCodeIsRegistered)
							{
								Members.FromCurrencyCode.Events.OnChange -= onFromCurrencyCodeProxy;
								onFromCurrencyCodeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onFromCurrencyCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CurrencyRate, PropertyEventArgs> handler = onFromCurrencyCode;
					if (handler is not null)
						handler.Invoke((CurrencyRate)sender, args);
				}

				#endregion

				#region OnToCurrencyCode

				private static bool onToCurrencyCodeIsRegistered = false;

				private static EventHandler<CurrencyRate, PropertyEventArgs> onToCurrencyCode;
				public static event EventHandler<CurrencyRate, PropertyEventArgs> OnToCurrencyCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onToCurrencyCodeIsRegistered)
							{
								Members.ToCurrencyCode.Events.OnChange -= onToCurrencyCodeProxy;
								Members.ToCurrencyCode.Events.OnChange += onToCurrencyCodeProxy;
								onToCurrencyCodeIsRegistered = true;
							}
							onToCurrencyCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onToCurrencyCode -= value;
							if (onToCurrencyCode is null && onToCurrencyCodeIsRegistered)
							{
								Members.ToCurrencyCode.Events.OnChange -= onToCurrencyCodeProxy;
								onToCurrencyCodeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onToCurrencyCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CurrencyRate, PropertyEventArgs> handler = onToCurrencyCode;
					if (handler is not null)
						handler.Invoke((CurrencyRate)sender, args);
				}

				#endregion

				#region OnAverageRate

				private static bool onAverageRateIsRegistered = false;

				private static EventHandler<CurrencyRate, PropertyEventArgs> onAverageRate;
				public static event EventHandler<CurrencyRate, PropertyEventArgs> OnAverageRate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAverageRateIsRegistered)
							{
								Members.AverageRate.Events.OnChange -= onAverageRateProxy;
								Members.AverageRate.Events.OnChange += onAverageRateProxy;
								onAverageRateIsRegistered = true;
							}
							onAverageRate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAverageRate -= value;
							if (onAverageRate is null && onAverageRateIsRegistered)
							{
								Members.AverageRate.Events.OnChange -= onAverageRateProxy;
								onAverageRateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onAverageRateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CurrencyRate, PropertyEventArgs> handler = onAverageRate;
					if (handler is not null)
						handler.Invoke((CurrencyRate)sender, args);
				}

				#endregion

				#region OnEndOfDayRate

				private static bool onEndOfDayRateIsRegistered = false;

				private static EventHandler<CurrencyRate, PropertyEventArgs> onEndOfDayRate;
				public static event EventHandler<CurrencyRate, PropertyEventArgs> OnEndOfDayRate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEndOfDayRateIsRegistered)
							{
								Members.EndOfDayRate.Events.OnChange -= onEndOfDayRateProxy;
								Members.EndOfDayRate.Events.OnChange += onEndOfDayRateProxy;
								onEndOfDayRateIsRegistered = true;
							}
							onEndOfDayRate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEndOfDayRate -= value;
							if (onEndOfDayRate is null && onEndOfDayRateIsRegistered)
							{
								Members.EndOfDayRate.Events.OnChange -= onEndOfDayRateProxy;
								onEndOfDayRateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onEndOfDayRateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CurrencyRate, PropertyEventArgs> handler = onEndOfDayRate;
					if (handler is not null)
						handler.Invoke((CurrencyRate)sender, args);
				}

				#endregion

				#region OnCurrency

				private static bool onCurrencyIsRegistered = false;

				private static EventHandler<CurrencyRate, PropertyEventArgs> onCurrency;
				public static event EventHandler<CurrencyRate, PropertyEventArgs> OnCurrency
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCurrencyIsRegistered)
							{
								Members.Currency.Events.OnChange -= onCurrencyProxy;
								Members.Currency.Events.OnChange += onCurrencyProxy;
								onCurrencyIsRegistered = true;
							}
							onCurrency += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCurrency -= value;
							if (onCurrency is null && onCurrencyIsRegistered)
							{
								Members.Currency.Events.OnChange -= onCurrencyProxy;
								onCurrencyIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCurrencyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CurrencyRate, PropertyEventArgs> handler = onCurrency;
					if (handler is not null)
						handler.Invoke((CurrencyRate)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<CurrencyRate, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<CurrencyRate, PropertyEventArgs> OnModifiedDate
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
					EventHandler<CurrencyRate, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((CurrencyRate)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<CurrencyRate, PropertyEventArgs> onUid;
				public static event EventHandler<CurrencyRate, PropertyEventArgs> OnUid
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
					EventHandler<CurrencyRate, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((CurrencyRate)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region ICurrencyRateOriginalData

		public ICurrencyRateOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ICurrencyRate

		System.DateTime ICurrencyRateOriginalData.CurrencyRateDate { get { return OriginalData.CurrencyRateDate; } }
		string ICurrencyRateOriginalData.FromCurrencyCode { get { return OriginalData.FromCurrencyCode; } }
		string ICurrencyRateOriginalData.ToCurrencyCode { get { return OriginalData.ToCurrencyCode; } }
		string ICurrencyRateOriginalData.AverageRate { get { return OriginalData.AverageRate; } }
		string ICurrencyRateOriginalData.EndOfDayRate { get { return OriginalData.EndOfDayRate; } }
		Currency ICurrencyRateOriginalData.Currency { get { return ((ILookupHelper<Currency>)OriginalData.Currency).GetOriginalItem(null); } }

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