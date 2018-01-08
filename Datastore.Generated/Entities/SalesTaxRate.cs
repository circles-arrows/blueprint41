 
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
	public interface ISalesTaxRateOriginalData
    {
		#region Outer Data

		#region Members for interface ISalesTaxRate

		string TaxType { get; }
		string TaxRate { get; }
		string Name { get; }
		string rowguid { get; }
		StateProvince StateProvince { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
   
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
            #region LoadFromNaturalKey
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

        }

        public static Dictionary<System.String, SalesTaxRate> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.SalesTaxRateAlias, IWhereQuery> query)
        {
            q.SalesTaxRateAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.SalesTaxRate.Alias(out alias));
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
                if ((object)InnerData == (object)OriginalData)
                    OriginalData = new SalesTaxRateData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.TaxType == null)
				throw new PersistenceException(string.Format("Cannot save SalesTaxRate with key '{0}' because the TaxType cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.TaxRate == null)
				throw new PersistenceException(string.Format("Cannot save SalesTaxRate with key '{0}' because the TaxRate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Name == null)
				throw new PersistenceException(string.Format("Cannot save SalesTaxRate with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save SalesTaxRate with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save SalesTaxRate with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
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
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(key); base.SetKey(Uid); }

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
                if (members == null)
                {
                    lock (typeof(SalesTaxRate))
                    {
                        if (members == null)
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
                if (fullTextMembers == null)
                {
                    lock (typeof(SalesTaxRate))
                    {
                        if (fullTextMembers == null)
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
            if (entity == null)
            {
                lock (typeof(SalesTaxRate))
                {
                    if (entity == null)
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
                if (events == null)
                {
                    lock (typeof(SalesTaxRate))
                    {
                        if (events == null)
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
                EventHandler<SalesTaxRate, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
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
                EventHandler<SalesTaxRate, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
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
                EventHandler<SalesTaxRate, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
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
							if (onTaxType == null && onTaxTypeIsRegistered)
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
					if ((object)handler != null)
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
							if (onTaxRate == null && onTaxRateIsRegistered)
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
					if ((object)handler != null)
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
							if (onName == null && onNameIsRegistered)
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
					if ((object)handler != null)
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
					EventHandler<SalesTaxRate, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
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
							if (onStateProvince == null && onStateProvinceIsRegistered)
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
					if ((object)handler != null)
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
					EventHandler<SalesTaxRate, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
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
					EventHandler<SalesTaxRate, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
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

		System.DateTime ISalesTaxRateOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string ISalesTaxRateOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}