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
	public interface IProductProductPhotoOriginalData : ISchemaBaseOriginalData
    {
		string Primary { get; }
		ProductPhoto ProductPhoto { get; }
    }

	public partial class ProductProductPhoto : OGM<ProductProductPhoto, ProductProductPhoto.ProductProductPhotoData, System.String>, ISchemaBase, INeo4jBase, IProductProductPhotoOriginalData
	{
        #region Initialize

        static ProductProductPhoto()
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

        public static Dictionary<System.String, ProductProductPhoto> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ProductProductPhotoAlias, IWhereQuery> query)
        {
            q.ProductProductPhotoAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ProductProductPhoto.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"ProductProductPhoto => Primary : {this.Primary}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new ProductProductPhotoData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Primary == null)
				throw new PersistenceException(string.Format("Cannot save ProductProductPhoto with key '{0}' because the Primary cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save ProductProductPhoto with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ProductProductPhotoData : Data<System.String>
		{
			public ProductProductPhotoData()
            {

            }

            public ProductProductPhotoData(ProductProductPhotoData data)
            {
				Primary = data.Primary;
				ProductPhoto = data.ProductPhoto;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "ProductProductPhoto";

				ProductPhoto = new EntityCollection<ProductPhoto>(Wrapper, Members.ProductPhoto);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Primary",  Primary);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Primary", out value))
					Primary = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IProductProductPhoto

			public string Primary { get; set; }
			public EntityCollection<ProductPhoto> ProductPhoto { get; private set; }

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

		#region Members for interface IProductProductPhoto

		public string Primary { get { LazyGet(); return InnerData.Primary; } set { if (LazySet(Members.Primary, InnerData.Primary, value)) InnerData.Primary = value; } }
		public ProductPhoto ProductPhoto
		{
			get { return ((ILookupHelper<ProductPhoto>)InnerData.ProductPhoto).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.ProductPhoto, ((ILookupHelper<ProductPhoto>)InnerData.ProductPhoto).GetItem(null), value))
					((ILookupHelper<ProductPhoto>)InnerData.ProductPhoto).SetItem(value, null); 
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

        private static ProductProductPhotoMembers members = null;
        public static ProductProductPhotoMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(ProductProductPhoto))
                    {
                        if (members == null)
                            members = new ProductProductPhotoMembers();
                    }
                }
                return members;
            }
        }
        public class ProductProductPhotoMembers
        {
            internal ProductProductPhotoMembers() { }

			#region Members for interface IProductProductPhoto

            public Property Primary { get; } = Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"].Properties["Primary"];
            public Property ProductPhoto { get; } = Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"].Properties["ProductPhoto"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static ProductProductPhotoFullTextMembers fullTextMembers = null;
        public static ProductProductPhotoFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(ProductProductPhoto))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new ProductProductPhotoFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class ProductProductPhotoFullTextMembers
        {
            internal ProductProductPhotoFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(ProductProductPhoto))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"];
                }
            }
            return entity;
        }

		private static ProductProductPhotoEvents events = null;
        public static ProductProductPhotoEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(ProductProductPhoto))
                    {
                        if (events == null)
                            events = new ProductProductPhotoEvents();
                    }
                }
                return events;
            }
        }
        public class ProductProductPhotoEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<ProductProductPhoto, EntityEventArgs> onNew;
            public event EventHandler<ProductProductPhoto, EntityEventArgs> OnNew
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
                EventHandler<ProductProductPhoto, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((ProductProductPhoto)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<ProductProductPhoto, EntityEventArgs> onDelete;
            public event EventHandler<ProductProductPhoto, EntityEventArgs> OnDelete
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
                EventHandler<ProductProductPhoto, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((ProductProductPhoto)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<ProductProductPhoto, EntityEventArgs> onSave;
            public event EventHandler<ProductProductPhoto, EntityEventArgs> OnSave
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
                EventHandler<ProductProductPhoto, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((ProductProductPhoto)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnPrimary

				private static bool onPrimaryIsRegistered = false;

				private static EventHandler<ProductProductPhoto, PropertyEventArgs> onPrimary;
				public static event EventHandler<ProductProductPhoto, PropertyEventArgs> OnPrimary
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPrimaryIsRegistered)
							{
								Members.Primary.Events.OnChange -= onPrimaryProxy;
								Members.Primary.Events.OnChange += onPrimaryProxy;
								onPrimaryIsRegistered = true;
							}
							onPrimary += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPrimary -= value;
							if (onPrimary == null && onPrimaryIsRegistered)
							{
								Members.Primary.Events.OnChange -= onPrimaryProxy;
								onPrimaryIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPrimaryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductProductPhoto, PropertyEventArgs> handler = onPrimary;
					if ((object)handler != null)
						handler.Invoke((ProductProductPhoto)sender, args);
				}

				#endregion

				#region OnProductPhoto

				private static bool onProductPhotoIsRegistered = false;

				private static EventHandler<ProductProductPhoto, PropertyEventArgs> onProductPhoto;
				public static event EventHandler<ProductProductPhoto, PropertyEventArgs> OnProductPhoto
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductPhotoIsRegistered)
							{
								Members.ProductPhoto.Events.OnChange -= onProductPhotoProxy;
								Members.ProductPhoto.Events.OnChange += onProductPhotoProxy;
								onProductPhotoIsRegistered = true;
							}
							onProductPhoto += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProductPhoto -= value;
							if (onProductPhoto == null && onProductPhotoIsRegistered)
							{
								Members.ProductPhoto.Events.OnChange -= onProductPhotoProxy;
								onProductPhotoIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductPhotoProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductProductPhoto, PropertyEventArgs> handler = onProductPhoto;
					if ((object)handler != null)
						handler.Invoke((ProductProductPhoto)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<ProductProductPhoto, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<ProductProductPhoto, PropertyEventArgs> OnModifiedDate
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
					EventHandler<ProductProductPhoto, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((ProductProductPhoto)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<ProductProductPhoto, PropertyEventArgs> onUid;
				public static event EventHandler<ProductProductPhoto, PropertyEventArgs> OnUid
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
					EventHandler<ProductProductPhoto, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((ProductProductPhoto)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IProductProductPhotoOriginalData

		public IProductProductPhotoOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IProductProductPhoto

		string IProductProductPhotoOriginalData.Primary { get { return OriginalData.Primary; } }
		ProductPhoto IProductProductPhotoOriginalData.ProductPhoto { get { return ((ILookupHelper<ProductPhoto>)OriginalData.ProductPhoto).GetOriginalItem(null); } }

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