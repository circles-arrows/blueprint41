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
	public interface ISalesPersonOriginalData
    {
		#region Outer Data

		#region Members for interface ISalesPerson

		string SalesQuota { get; }
		string Bonus { get; }
		string CommissionPct { get; }
		string SalesYTD { get; }
		string SalesLastYear { get; }
		string rowguid { get; }
		SalesPersonQuotaHistory SalesPersonQuotaHistory { get; }
		SalesTerritory SalesTerritory { get; }
		Person Person { get; }
		IEnumerable<Store> Stores { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
    }

	public partial class SalesPerson : OGM<SalesPerson, SalesPerson.SalesPersonData, System.String>, ISchemaBase, INeo4jBase, ISalesPersonOriginalData
	{
        #region Initialize

        static SalesPerson()
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

        public static Dictionary<System.String, SalesPerson> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.SalesPersonAlias, IWhereQuery> query)
        {
            q.SalesPersonAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.SalesPerson.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"SalesPerson => SalesQuota : {this.SalesQuota?.ToString() ?? "null"}, Bonus : {this.Bonus}, CommissionPct : {this.CommissionPct}, SalesYTD : {this.SalesYTD}, SalesLastYear : {this.SalesLastYear}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new SalesPersonData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Bonus == null)
				throw new PersistenceException(string.Format("Cannot save SalesPerson with key '{0}' because the Bonus cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.CommissionPct == null)
				throw new PersistenceException(string.Format("Cannot save SalesPerson with key '{0}' because the CommissionPct cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SalesYTD == null)
				throw new PersistenceException(string.Format("Cannot save SalesPerson with key '{0}' because the SalesYTD cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SalesLastYear == null)
				throw new PersistenceException(string.Format("Cannot save SalesPerson with key '{0}' because the SalesLastYear cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save SalesPerson with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save SalesPerson with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class SalesPersonData : Data<System.String>
		{
			public SalesPersonData()
            {

            }

            public SalesPersonData(SalesPersonData data)
            {
				SalesQuota = data.SalesQuota;
				Bonus = data.Bonus;
				CommissionPct = data.CommissionPct;
				SalesYTD = data.SalesYTD;
				SalesLastYear = data.SalesLastYear;
				rowguid = data.rowguid;
				SalesPersonQuotaHistory = data.SalesPersonQuotaHistory;
				SalesTerritory = data.SalesTerritory;
				Person = data.Person;
				Stores = data.Stores;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "SalesPerson";

				SalesPersonQuotaHistory = new EntityCollection<SalesPersonQuotaHistory>(Wrapper, Members.SalesPersonQuotaHistory);
				SalesTerritory = new EntityCollection<SalesTerritory>(Wrapper, Members.SalesTerritory);
				Person = new EntityCollection<Person>(Wrapper, Members.Person, item => { if (Members.Person.Events.HasRegisteredChangeHandlers) { object loadHack = item.SalesPerson; } });
				Stores = new EntityCollection<Store>(Wrapper, Members.Stores, item => { if (Members.Stores.Events.HasRegisteredChangeHandlers) { object loadHack = item.SalesPerson; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("SalesQuota",  SalesQuota);
				dictionary.Add("Bonus",  Bonus);
				dictionary.Add("CommissionPct",  CommissionPct);
				dictionary.Add("SalesYTD",  SalesYTD);
				dictionary.Add("SalesLastYear",  SalesLastYear);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("SalesQuota", out value))
					SalesQuota = (string)value;
				if (properties.TryGetValue("Bonus", out value))
					Bonus = (string)value;
				if (properties.TryGetValue("CommissionPct", out value))
					CommissionPct = (string)value;
				if (properties.TryGetValue("SalesYTD", out value))
					SalesYTD = (string)value;
				if (properties.TryGetValue("SalesLastYear", out value))
					SalesLastYear = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ISalesPerson

			public string SalesQuota { get; set; }
			public string Bonus { get; set; }
			public string CommissionPct { get; set; }
			public string SalesYTD { get; set; }
			public string SalesLastYear { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<SalesPersonQuotaHistory> SalesPersonQuotaHistory { get; private set; }
			public EntityCollection<SalesTerritory> SalesTerritory { get; private set; }
			public EntityCollection<Person> Person { get; private set; }
			public EntityCollection<Store> Stores { get; private set; }

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

		#region Members for interface ISalesPerson

		public string SalesQuota { get { LazyGet(); return InnerData.SalesQuota; } set { if (LazySet(Members.SalesQuota, InnerData.SalesQuota, value)) InnerData.SalesQuota = value; } }
		public string Bonus { get { LazyGet(); return InnerData.Bonus; } set { if (LazySet(Members.Bonus, InnerData.Bonus, value)) InnerData.Bonus = value; } }
		public string CommissionPct { get { LazyGet(); return InnerData.CommissionPct; } set { if (LazySet(Members.CommissionPct, InnerData.CommissionPct, value)) InnerData.CommissionPct = value; } }
		public string SalesYTD { get { LazyGet(); return InnerData.SalesYTD; } set { if (LazySet(Members.SalesYTD, InnerData.SalesYTD, value)) InnerData.SalesYTD = value; } }
		public string SalesLastYear { get { LazyGet(); return InnerData.SalesLastYear; } set { if (LazySet(Members.SalesLastYear, InnerData.SalesLastYear, value)) InnerData.SalesLastYear = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public SalesPersonQuotaHistory SalesPersonQuotaHistory
		{
			get { return ((ILookupHelper<SalesPersonQuotaHistory>)InnerData.SalesPersonQuotaHistory).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SalesPersonQuotaHistory, ((ILookupHelper<SalesPersonQuotaHistory>)InnerData.SalesPersonQuotaHistory).GetItem(null), value))
					((ILookupHelper<SalesPersonQuotaHistory>)InnerData.SalesPersonQuotaHistory).SetItem(value, null); 
			}
		}
		public SalesTerritory SalesTerritory
		{
			get { return ((ILookupHelper<SalesTerritory>)InnerData.SalesTerritory).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SalesTerritory, ((ILookupHelper<SalesTerritory>)InnerData.SalesTerritory).GetItem(null), value))
					((ILookupHelper<SalesTerritory>)InnerData.SalesTerritory).SetItem(value, null); 
			}
		}
		public Person Person
		{
			get { return ((ILookupHelper<Person>)InnerData.Person).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Person, ((ILookupHelper<Person>)InnerData.Person).GetItem(null), value))
					((ILookupHelper<Person>)InnerData.Person).SetItem(value, null); 
			}
		}
		private void ClearPerson(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.Person).ClearLookup(moment);
		}
		public EntityCollection<Store> Stores { get { return InnerData.Stores; } }
		private void ClearStores(DateTime? moment)
		{
			((ILookupHelper<EntityCollection<Store>>)InnerData.Stores).ClearLookup(moment);
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

        private static SalesPersonMembers members = null;
        public static SalesPersonMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(SalesPerson))
                    {
                        if (members == null)
                            members = new SalesPersonMembers();
                    }
                }
                return members;
            }
        }
        public class SalesPersonMembers
        {
            internal SalesPersonMembers() { }

			#region Members for interface ISalesPerson

            public Property SalesQuota { get; } = Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesQuota"];
            public Property Bonus { get; } = Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["Bonus"];
            public Property CommissionPct { get; } = Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["CommissionPct"];
            public Property SalesYTD { get; } = Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesYTD"];
            public Property SalesLastYear { get; } = Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesLastYear"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["rowguid"];
            public Property SalesPersonQuotaHistory { get; } = Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesPersonQuotaHistory"];
            public Property SalesTerritory { get; } = Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["SalesTerritory"];
            public Property Person { get; } = Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["Person"];
            public Property Stores { get; } = Datastore.AdventureWorks.Model.Entities["SalesPerson"].Properties["Stores"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static SalesPersonFullTextMembers fullTextMembers = null;
        public static SalesPersonFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(SalesPerson))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new SalesPersonFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class SalesPersonFullTextMembers
        {
            internal SalesPersonFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(SalesPerson))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["SalesPerson"];
                }
            }
            return entity;
        }

		private static SalesPersonEvents events = null;
        public static SalesPersonEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(SalesPerson))
                    {
                        if (events == null)
                            events = new SalesPersonEvents();
                    }
                }
                return events;
            }
        }
        public class SalesPersonEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<SalesPerson, EntityEventArgs> onNew;
            public event EventHandler<SalesPerson, EntityEventArgs> OnNew
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
                EventHandler<SalesPerson, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((SalesPerson)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<SalesPerson, EntityEventArgs> onDelete;
            public event EventHandler<SalesPerson, EntityEventArgs> OnDelete
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
                EventHandler<SalesPerson, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((SalesPerson)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<SalesPerson, EntityEventArgs> onSave;
            public event EventHandler<SalesPerson, EntityEventArgs> OnSave
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
                EventHandler<SalesPerson, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((SalesPerson)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnSalesQuota

				private static bool onSalesQuotaIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onSalesQuota;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnSalesQuota
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesQuotaIsRegistered)
							{
								Members.SalesQuota.Events.OnChange -= onSalesQuotaProxy;
								Members.SalesQuota.Events.OnChange += onSalesQuotaProxy;
								onSalesQuotaIsRegistered = true;
							}
							onSalesQuota += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesQuota -= value;
							if (onSalesQuota == null && onSalesQuotaIsRegistered)
							{
								Members.SalesQuota.Events.OnChange -= onSalesQuotaProxy;
								onSalesQuotaIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesQuotaProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesPerson, PropertyEventArgs> handler = onSalesQuota;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region OnBonus

				private static bool onBonusIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onBonus;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnBonus
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onBonusIsRegistered)
							{
								Members.Bonus.Events.OnChange -= onBonusProxy;
								Members.Bonus.Events.OnChange += onBonusProxy;
								onBonusIsRegistered = true;
							}
							onBonus += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onBonus -= value;
							if (onBonus == null && onBonusIsRegistered)
							{
								Members.Bonus.Events.OnChange -= onBonusProxy;
								onBonusIsRegistered = false;
							}
						}
					}
				}
            
				private static void onBonusProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesPerson, PropertyEventArgs> handler = onBonus;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region OnCommissionPct

				private static bool onCommissionPctIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onCommissionPct;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnCommissionPct
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCommissionPctIsRegistered)
							{
								Members.CommissionPct.Events.OnChange -= onCommissionPctProxy;
								Members.CommissionPct.Events.OnChange += onCommissionPctProxy;
								onCommissionPctIsRegistered = true;
							}
							onCommissionPct += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCommissionPct -= value;
							if (onCommissionPct == null && onCommissionPctIsRegistered)
							{
								Members.CommissionPct.Events.OnChange -= onCommissionPctProxy;
								onCommissionPctIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCommissionPctProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesPerson, PropertyEventArgs> handler = onCommissionPct;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region OnSalesYTD

				private static bool onSalesYTDIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onSalesYTD;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnSalesYTD
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesYTDIsRegistered)
							{
								Members.SalesYTD.Events.OnChange -= onSalesYTDProxy;
								Members.SalesYTD.Events.OnChange += onSalesYTDProxy;
								onSalesYTDIsRegistered = true;
							}
							onSalesYTD += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesYTD -= value;
							if (onSalesYTD == null && onSalesYTDIsRegistered)
							{
								Members.SalesYTD.Events.OnChange -= onSalesYTDProxy;
								onSalesYTDIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesYTDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesPerson, PropertyEventArgs> handler = onSalesYTD;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region OnSalesLastYear

				private static bool onSalesLastYearIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onSalesLastYear;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnSalesLastYear
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesLastYearIsRegistered)
							{
								Members.SalesLastYear.Events.OnChange -= onSalesLastYearProxy;
								Members.SalesLastYear.Events.OnChange += onSalesLastYearProxy;
								onSalesLastYearIsRegistered = true;
							}
							onSalesLastYear += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesLastYear -= value;
							if (onSalesLastYear == null && onSalesLastYearIsRegistered)
							{
								Members.SalesLastYear.Events.OnChange -= onSalesLastYearProxy;
								onSalesLastYearIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesLastYearProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesPerson, PropertyEventArgs> handler = onSalesLastYear;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onrowguid;
				public static event EventHandler<SalesPerson, PropertyEventArgs> Onrowguid
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
					EventHandler<SalesPerson, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region OnSalesPersonQuotaHistory

				private static bool onSalesPersonQuotaHistoryIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onSalesPersonQuotaHistory;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnSalesPersonQuotaHistory
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesPersonQuotaHistoryIsRegistered)
							{
								Members.SalesPersonQuotaHistory.Events.OnChange -= onSalesPersonQuotaHistoryProxy;
								Members.SalesPersonQuotaHistory.Events.OnChange += onSalesPersonQuotaHistoryProxy;
								onSalesPersonQuotaHistoryIsRegistered = true;
							}
							onSalesPersonQuotaHistory += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesPersonQuotaHistory -= value;
							if (onSalesPersonQuotaHistory == null && onSalesPersonQuotaHistoryIsRegistered)
							{
								Members.SalesPersonQuotaHistory.Events.OnChange -= onSalesPersonQuotaHistoryProxy;
								onSalesPersonQuotaHistoryIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesPersonQuotaHistoryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesPerson, PropertyEventArgs> handler = onSalesPersonQuotaHistory;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region OnSalesTerritory

				private static bool onSalesTerritoryIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onSalesTerritory;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnSalesTerritory
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesTerritoryIsRegistered)
							{
								Members.SalesTerritory.Events.OnChange -= onSalesTerritoryProxy;
								Members.SalesTerritory.Events.OnChange += onSalesTerritoryProxy;
								onSalesTerritoryIsRegistered = true;
							}
							onSalesTerritory += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesTerritory -= value;
							if (onSalesTerritory == null && onSalesTerritoryIsRegistered)
							{
								Members.SalesTerritory.Events.OnChange -= onSalesTerritoryProxy;
								onSalesTerritoryIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesTerritoryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesPerson, PropertyEventArgs> handler = onSalesTerritory;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region OnPerson

				private static bool onPersonIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onPerson;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnPerson
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPersonIsRegistered)
							{
								Members.Person.Events.OnChange -= onPersonProxy;
								Members.Person.Events.OnChange += onPersonProxy;
								onPersonIsRegistered = true;
							}
							onPerson += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPerson -= value;
							if (onPerson == null && onPersonIsRegistered)
							{
								Members.Person.Events.OnChange -= onPersonProxy;
								onPersonIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPersonProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesPerson, PropertyEventArgs> handler = onPerson;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region OnStores

				private static bool onStoresIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onStores;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnStores
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStoresIsRegistered)
							{
								Members.Stores.Events.OnChange -= onStoresProxy;
								Members.Stores.Events.OnChange += onStoresProxy;
								onStoresIsRegistered = true;
							}
							onStores += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStores -= value;
							if (onStores == null && onStoresIsRegistered)
							{
								Members.Stores.Events.OnChange -= onStoresProxy;
								onStoresIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStoresProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesPerson, PropertyEventArgs> handler = onStores;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnModifiedDate
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
					EventHandler<SalesPerson, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<SalesPerson, PropertyEventArgs> onUid;
				public static event EventHandler<SalesPerson, PropertyEventArgs> OnUid
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
					EventHandler<SalesPerson, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((SalesPerson)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region ISalesPersonOriginalData

		public ISalesPersonOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ISalesPerson

		string ISalesPersonOriginalData.SalesQuota { get { return OriginalData.SalesQuota; } }
		string ISalesPersonOriginalData.Bonus { get { return OriginalData.Bonus; } }
		string ISalesPersonOriginalData.CommissionPct { get { return OriginalData.CommissionPct; } }
		string ISalesPersonOriginalData.SalesYTD { get { return OriginalData.SalesYTD; } }
		string ISalesPersonOriginalData.SalesLastYear { get { return OriginalData.SalesLastYear; } }
		string ISalesPersonOriginalData.rowguid { get { return OriginalData.rowguid; } }
		SalesPersonQuotaHistory ISalesPersonOriginalData.SalesPersonQuotaHistory { get { return ((ILookupHelper<SalesPersonQuotaHistory>)OriginalData.SalesPersonQuotaHistory).GetOriginalItem(null); } }
		SalesTerritory ISalesPersonOriginalData.SalesTerritory { get { return ((ILookupHelper<SalesTerritory>)OriginalData.SalesTerritory).GetOriginalItem(null); } }
		Person ISalesPersonOriginalData.Person { get { return ((ILookupHelper<Person>)OriginalData.Person).GetOriginalItem(null); } }
		IEnumerable<Store> ISalesPersonOriginalData.Stores { get { return OriginalData.Stores.OriginalData; } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ISalesPersonOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string ISalesPersonOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}