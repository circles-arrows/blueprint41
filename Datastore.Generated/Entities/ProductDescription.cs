 
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
	public interface IProductDescriptionOriginalData
    {
		#region Outer Data

		#region Members for interface IProductDescription

		string Description { get; }
		string rowguid { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
   
	}

	public partial class ProductDescription : OGM<ProductDescription, ProductDescription.ProductDescriptionData, System.String>, ISchemaBase, INeo4jBase, IProductDescriptionOriginalData
	{
        #region Initialize

        static ProductDescription()
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

        public static Dictionary<System.String, ProductDescription> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ProductDescriptionAlias, IWhereQuery> query)
        {
            q.ProductDescriptionAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ProductDescription.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"ProductDescription => Description : {this.Description}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new ProductDescriptionData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Description == null)
				throw new PersistenceException(string.Format("Cannot save ProductDescription with key '{0}' because the Description cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save ProductDescription with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save ProductDescription with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ProductDescriptionData : Data<System.String>
		{
			public ProductDescriptionData()
            {

            }

            public ProductDescriptionData(ProductDescriptionData data)
            {
				Description = data.Description;
				rowguid = data.rowguid;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "ProductDescription";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Description",  Description);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Description", out value))
					Description = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IProductDescription

			public string Description { get; set; }
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

		#region Members for interface IProductDescription

		public string Description { get { LazyGet(); return InnerData.Description; } set { if (LazySet(Members.Description, InnerData.Description, value)) InnerData.Description = value; } }
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

        private static ProductDescriptionMembers members = null;
        public static ProductDescriptionMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(ProductDescription))
                    {
                        if (members == null)
                            members = new ProductDescriptionMembers();
                    }
                }
                return members;
            }
        }
        public class ProductDescriptionMembers
        {
            internal ProductDescriptionMembers() { }

			#region Members for interface IProductDescription

            public Property Description { get; } = Datastore.AdventureWorks.Model.Entities["ProductDescription"].Properties["Description"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["ProductDescription"].Properties["rowguid"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static ProductDescriptionFullTextMembers fullTextMembers = null;
        public static ProductDescriptionFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(ProductDescription))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new ProductDescriptionFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class ProductDescriptionFullTextMembers
        {
            internal ProductDescriptionFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(ProductDescription))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["ProductDescription"];
                }
            }
            return entity;
        }

		private static ProductDescriptionEvents events = null;
        public static ProductDescriptionEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(ProductDescription))
                    {
                        if (events == null)
                            events = new ProductDescriptionEvents();
                    }
                }
                return events;
            }
        }
        public class ProductDescriptionEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<ProductDescription, EntityEventArgs> onNew;
            public event EventHandler<ProductDescription, EntityEventArgs> OnNew
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
                EventHandler<ProductDescription, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((ProductDescription)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<ProductDescription, EntityEventArgs> onDelete;
            public event EventHandler<ProductDescription, EntityEventArgs> OnDelete
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
                EventHandler<ProductDescription, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((ProductDescription)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<ProductDescription, EntityEventArgs> onSave;
            public event EventHandler<ProductDescription, EntityEventArgs> OnSave
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
                EventHandler<ProductDescription, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((ProductDescription)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnDescription

				private static bool onDescriptionIsRegistered = false;

				private static EventHandler<ProductDescription, PropertyEventArgs> onDescription;
				public static event EventHandler<ProductDescription, PropertyEventArgs> OnDescription
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
							if (onDescription == null && onDescriptionIsRegistered)
							{
								Members.Description.Events.OnChange -= onDescriptionProxy;
								onDescriptionIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDescriptionProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductDescription, PropertyEventArgs> handler = onDescription;
					if ((object)handler != null)
						handler.Invoke((ProductDescription)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<ProductDescription, PropertyEventArgs> onrowguid;
				public static event EventHandler<ProductDescription, PropertyEventArgs> Onrowguid
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
					EventHandler<ProductDescription, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((ProductDescription)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<ProductDescription, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<ProductDescription, PropertyEventArgs> OnModifiedDate
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
					EventHandler<ProductDescription, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((ProductDescription)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<ProductDescription, PropertyEventArgs> onUid;
				public static event EventHandler<ProductDescription, PropertyEventArgs> OnUid
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
					EventHandler<ProductDescription, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((ProductDescription)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IProductDescriptionOriginalData

		public IProductDescriptionOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IProductDescription

		string IProductDescriptionOriginalData.Description { get { return OriginalData.Description; } }
		string IProductDescriptionOriginalData.rowguid { get { return OriginalData.rowguid; } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime IProductDescriptionOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string IProductDescriptionOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}