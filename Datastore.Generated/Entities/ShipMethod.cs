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
	public interface IShipMethodOriginalData : ISchemaBaseOriginalData
    {
		string Name { get; }
		string ShipBase { get; }
		string ShipRate { get; }
		string rowguid { get; }
    }

	public partial class ShipMethod : OGM<ShipMethod, ShipMethod.ShipMethodData, System.String>, ISchemaBase, INeo4jBase, IShipMethodOriginalData
	{
        #region Initialize

        static ShipMethod()
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

        public static Dictionary<System.String, ShipMethod> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ShipMethodAlias, IWhereQuery> query)
        {
            q.ShipMethodAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ShipMethod.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"ShipMethod => Name : {this.Name}, ShipBase : {this.ShipBase}, ShipRate : {this.ShipRate}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new ShipMethodData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Name == null)
				throw new PersistenceException(string.Format("Cannot save ShipMethod with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ShipBase == null)
				throw new PersistenceException(string.Format("Cannot save ShipMethod with key '{0}' because the ShipBase cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ShipRate == null)
				throw new PersistenceException(string.Format("Cannot save ShipMethod with key '{0}' because the ShipRate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save ShipMethod with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save ShipMethod with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ShipMethodData : Data<System.String>
		{
			public ShipMethodData()
            {

            }

            public ShipMethodData(ShipMethodData data)
            {
				Name = data.Name;
				ShipBase = data.ShipBase;
				ShipRate = data.ShipRate;
				rowguid = data.rowguid;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "ShipMethod";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Name",  Name);
				dictionary.Add("ShipBase",  ShipBase);
				dictionary.Add("ShipRate",  ShipRate);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("ShipBase", out value))
					ShipBase = (string)value;
				if (properties.TryGetValue("ShipRate", out value))
					ShipRate = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IShipMethod

			public string Name { get; set; }
			public string ShipBase { get; set; }
			public string ShipRate { get; set; }
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

		#region Members for interface IShipMethod

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string ShipBase { get { LazyGet(); return InnerData.ShipBase; } set { if (LazySet(Members.ShipBase, InnerData.ShipBase, value)) InnerData.ShipBase = value; } }
		public string ShipRate { get { LazyGet(); return InnerData.ShipRate; } set { if (LazySet(Members.ShipRate, InnerData.ShipRate, value)) InnerData.ShipRate = value; } }
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

        private static ShipMethodMembers members = null;
        public static ShipMethodMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(ShipMethod))
                    {
                        if (members == null)
                            members = new ShipMethodMembers();
                    }
                }
                return members;
            }
        }
        public class ShipMethodMembers
        {
            internal ShipMethodMembers() { }

			#region Members for interface IShipMethod

            public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["Name"];
            public Property ShipBase { get; } = Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["ShipBase"];
            public Property ShipRate { get; } = Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["ShipRate"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["ShipMethod"].Properties["rowguid"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static ShipMethodFullTextMembers fullTextMembers = null;
        public static ShipMethodFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(ShipMethod))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new ShipMethodFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class ShipMethodFullTextMembers
        {
            internal ShipMethodFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(ShipMethod))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["ShipMethod"];
                }
            }
            return entity;
        }

		private static ShipMethodEvents events = null;
        public static ShipMethodEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(ShipMethod))
                    {
                        if (events == null)
                            events = new ShipMethodEvents();
                    }
                }
                return events;
            }
        }
        public class ShipMethodEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<ShipMethod, EntityEventArgs> onNew;
            public event EventHandler<ShipMethod, EntityEventArgs> OnNew
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
                EventHandler<ShipMethod, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((ShipMethod)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<ShipMethod, EntityEventArgs> onDelete;
            public event EventHandler<ShipMethod, EntityEventArgs> OnDelete
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
                EventHandler<ShipMethod, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((ShipMethod)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<ShipMethod, EntityEventArgs> onSave;
            public event EventHandler<ShipMethod, EntityEventArgs> OnSave
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
                EventHandler<ShipMethod, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((ShipMethod)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<ShipMethod, PropertyEventArgs> onName;
				public static event EventHandler<ShipMethod, PropertyEventArgs> OnName
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
					EventHandler<ShipMethod, PropertyEventArgs> handler = onName;
					if ((object)handler != null)
						handler.Invoke((ShipMethod)sender, args);
				}

				#endregion

				#region OnShipBase

				private static bool onShipBaseIsRegistered = false;

				private static EventHandler<ShipMethod, PropertyEventArgs> onShipBase;
				public static event EventHandler<ShipMethod, PropertyEventArgs> OnShipBase
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onShipBaseIsRegistered)
							{
								Members.ShipBase.Events.OnChange -= onShipBaseProxy;
								Members.ShipBase.Events.OnChange += onShipBaseProxy;
								onShipBaseIsRegistered = true;
							}
							onShipBase += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onShipBase -= value;
							if (onShipBase == null && onShipBaseIsRegistered)
							{
								Members.ShipBase.Events.OnChange -= onShipBaseProxy;
								onShipBaseIsRegistered = false;
							}
						}
					}
				}
            
				private static void onShipBaseProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ShipMethod, PropertyEventArgs> handler = onShipBase;
					if ((object)handler != null)
						handler.Invoke((ShipMethod)sender, args);
				}

				#endregion

				#region OnShipRate

				private static bool onShipRateIsRegistered = false;

				private static EventHandler<ShipMethod, PropertyEventArgs> onShipRate;
				public static event EventHandler<ShipMethod, PropertyEventArgs> OnShipRate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onShipRateIsRegistered)
							{
								Members.ShipRate.Events.OnChange -= onShipRateProxy;
								Members.ShipRate.Events.OnChange += onShipRateProxy;
								onShipRateIsRegistered = true;
							}
							onShipRate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onShipRate -= value;
							if (onShipRate == null && onShipRateIsRegistered)
							{
								Members.ShipRate.Events.OnChange -= onShipRateProxy;
								onShipRateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onShipRateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ShipMethod, PropertyEventArgs> handler = onShipRate;
					if ((object)handler != null)
						handler.Invoke((ShipMethod)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<ShipMethod, PropertyEventArgs> onrowguid;
				public static event EventHandler<ShipMethod, PropertyEventArgs> Onrowguid
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
					EventHandler<ShipMethod, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((ShipMethod)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<ShipMethod, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<ShipMethod, PropertyEventArgs> OnModifiedDate
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
					EventHandler<ShipMethod, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((ShipMethod)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<ShipMethod, PropertyEventArgs> onUid;
				public static event EventHandler<ShipMethod, PropertyEventArgs> OnUid
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
					EventHandler<ShipMethod, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((ShipMethod)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IShipMethodOriginalData

		public IShipMethodOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IShipMethod

		string IShipMethodOriginalData.Name { get { return OriginalData.Name; } }
		string IShipMethodOriginalData.ShipBase { get { return OriginalData.ShipBase; } }
		string IShipMethodOriginalData.ShipRate { get { return OriginalData.ShipRate; } }
		string IShipMethodOriginalData.rowguid { get { return OriginalData.rowguid; } }

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