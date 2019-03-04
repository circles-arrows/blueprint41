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
	public interface ISalesPersonQuotaHistoryOriginalData : ISchemaBaseOriginalData
    {
		System.DateTime QuotaDate { get; }
		string SalesQuota { get; }
		string rowguid { get; }
    }

	public partial class SalesPersonQuotaHistory : OGM<SalesPersonQuotaHistory, SalesPersonQuotaHistory.SalesPersonQuotaHistoryData, System.String>, ISchemaBase, INeo4jBase, ISalesPersonQuotaHistoryOriginalData
	{
        #region Initialize

        static SalesPersonQuotaHistory()
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

        public static Dictionary<System.String, SalesPersonQuotaHistory> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.SalesPersonQuotaHistoryAlias, IWhereQuery> query)
        {
            q.SalesPersonQuotaHistoryAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.SalesPersonQuotaHistory.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"SalesPersonQuotaHistory => QuotaDate : {this.QuotaDate}, SalesQuota : {this.SalesQuota}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new SalesPersonQuotaHistoryData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.QuotaDate == null)
				throw new PersistenceException(string.Format("Cannot save SalesPersonQuotaHistory with key '{0}' because the QuotaDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.SalesQuota == null)
				throw new PersistenceException(string.Format("Cannot save SalesPersonQuotaHistory with key '{0}' because the SalesQuota cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save SalesPersonQuotaHistory with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save SalesPersonQuotaHistory with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class SalesPersonQuotaHistoryData : Data<System.String>
		{
			public SalesPersonQuotaHistoryData()
            {

            }

            public SalesPersonQuotaHistoryData(SalesPersonQuotaHistoryData data)
            {
				QuotaDate = data.QuotaDate;
				SalesQuota = data.SalesQuota;
				rowguid = data.rowguid;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "SalesPersonQuotaHistory";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("QuotaDate",  Conversion<System.DateTime, long>.Convert(QuotaDate));
				dictionary.Add("SalesQuota",  SalesQuota);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("QuotaDate", out value))
					QuotaDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("SalesQuota", out value))
					SalesQuota = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ISalesPersonQuotaHistory

			public System.DateTime QuotaDate { get; set; }
			public string SalesQuota { get; set; }
			public string rowguid { get; set; }

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

		#region Members for interface ISalesPersonQuotaHistory

		public System.DateTime QuotaDate { get { LazyGet(); return InnerData.QuotaDate; } set { if (LazySet(Members.QuotaDate, InnerData.QuotaDate, value)) InnerData.QuotaDate = value; } }
		public string SalesQuota { get { LazyGet(); return InnerData.SalesQuota; } set { if (LazySet(Members.SalesQuota, InnerData.SalesQuota, value)) InnerData.SalesQuota = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }

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

        private static SalesPersonQuotaHistoryMembers members = null;
        public static SalesPersonQuotaHistoryMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(SalesPersonQuotaHistory))
                    {
                        if (members == null)
                            members = new SalesPersonQuotaHistoryMembers();
                    }
                }
                return members;
            }
        }
        public class SalesPersonQuotaHistoryMembers
        {
            internal SalesPersonQuotaHistoryMembers() { }

			#region Members for interface ISalesPersonQuotaHistory

            public Property QuotaDate { get; } = Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"].Properties["QuotaDate"];
            public Property SalesQuota { get; } = Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"].Properties["SalesQuota"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"].Properties["rowguid"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static SalesPersonQuotaHistoryFullTextMembers fullTextMembers = null;
        public static SalesPersonQuotaHistoryFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(SalesPersonQuotaHistory))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new SalesPersonQuotaHistoryFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class SalesPersonQuotaHistoryFullTextMembers
        {
            internal SalesPersonQuotaHistoryFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(SalesPersonQuotaHistory))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"];
                }
            }
            return entity;
        }

		private static SalesPersonQuotaHistoryEvents events = null;
        public static SalesPersonQuotaHistoryEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(SalesPersonQuotaHistory))
                    {
                        if (events == null)
                            events = new SalesPersonQuotaHistoryEvents();
                    }
                }
                return events;
            }
        }
        public class SalesPersonQuotaHistoryEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<SalesPersonQuotaHistory, EntityEventArgs> onNew;
            public event EventHandler<SalesPersonQuotaHistory, EntityEventArgs> OnNew
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
                EventHandler<SalesPersonQuotaHistory, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((SalesPersonQuotaHistory)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<SalesPersonQuotaHistory, EntityEventArgs> onDelete;
            public event EventHandler<SalesPersonQuotaHistory, EntityEventArgs> OnDelete
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
                EventHandler<SalesPersonQuotaHistory, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((SalesPersonQuotaHistory)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<SalesPersonQuotaHistory, EntityEventArgs> onSave;
            public event EventHandler<SalesPersonQuotaHistory, EntityEventArgs> OnSave
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
                EventHandler<SalesPersonQuotaHistory, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((SalesPersonQuotaHistory)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnQuotaDate

				private static bool onQuotaDateIsRegistered = false;

				private static EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> onQuotaDate;
				public static event EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> OnQuotaDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onQuotaDateIsRegistered)
							{
								Members.QuotaDate.Events.OnChange -= onQuotaDateProxy;
								Members.QuotaDate.Events.OnChange += onQuotaDateProxy;
								onQuotaDateIsRegistered = true;
							}
							onQuotaDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onQuotaDate -= value;
							if (onQuotaDate == null && onQuotaDateIsRegistered)
							{
								Members.QuotaDate.Events.OnChange -= onQuotaDateProxy;
								onQuotaDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onQuotaDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> handler = onQuotaDate;
					if ((object)handler != null)
						handler.Invoke((SalesPersonQuotaHistory)sender, args);
				}

				#endregion

				#region OnSalesQuota

				private static bool onSalesQuotaIsRegistered = false;

				private static EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> onSalesQuota;
				public static event EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> OnSalesQuota
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
					EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> handler = onSalesQuota;
					if ((object)handler != null)
						handler.Invoke((SalesPersonQuotaHistory)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> onrowguid;
				public static event EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> Onrowguid
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
					EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((SalesPersonQuotaHistory)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> OnModifiedDate
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
					EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((SalesPersonQuotaHistory)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> onUid;
				public static event EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> OnUid
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
					EventHandler<SalesPersonQuotaHistory, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((SalesPersonQuotaHistory)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region ISalesPersonQuotaHistoryOriginalData

		public ISalesPersonQuotaHistoryOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ISalesPersonQuotaHistory

		System.DateTime ISalesPersonQuotaHistoryOriginalData.QuotaDate { get { return OriginalData.QuotaDate; } }
		string ISalesPersonQuotaHistoryOriginalData.SalesQuota { get { return OriginalData.SalesQuota; } }
		string ISalesPersonQuotaHistoryOriginalData.rowguid { get { return OriginalData.rowguid; } }

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