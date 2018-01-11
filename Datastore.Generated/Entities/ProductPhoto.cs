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
	public interface IProductPhotoOriginalData
    {
		#region Outer Data

		#region Members for interface IProductPhoto

		string ThumbNailPhoto { get; }
		string ThumbNailPhotoFileName { get; }
		string LargePhoto { get; }
		string LargePhotoFileName { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
    }

	public partial class ProductPhoto : OGM<ProductPhoto, ProductPhoto.ProductPhotoData, System.String>, ISchemaBase, INeo4jBase, IProductPhotoOriginalData
	{
        #region Initialize

        static ProductPhoto()
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

        public static Dictionary<System.String, ProductPhoto> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ProductPhotoAlias, IWhereQuery> query)
        {
            q.ProductPhotoAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ProductPhoto.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"ProductPhoto => ThumbNailPhoto : {this.ThumbNailPhoto?.ToString() ?? "null"}, ThumbNailPhotoFileName : {this.ThumbNailPhotoFileName?.ToString() ?? "null"}, LargePhoto : {this.LargePhoto?.ToString() ?? "null"}, LargePhotoFileName : {this.LargePhotoFileName?.ToString() ?? "null"}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new ProductPhotoData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save ProductPhoto with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ProductPhotoData : Data<System.String>
		{
			public ProductPhotoData()
            {

            }

            public ProductPhotoData(ProductPhotoData data)
            {
				ThumbNailPhoto = data.ThumbNailPhoto;
				ThumbNailPhotoFileName = data.ThumbNailPhotoFileName;
				LargePhoto = data.LargePhoto;
				LargePhotoFileName = data.LargePhotoFileName;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "ProductPhoto";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("ThumbNailPhoto",  ThumbNailPhoto);
				dictionary.Add("ThumbNailPhotoFileName",  ThumbNailPhotoFileName);
				dictionary.Add("LargePhoto",  LargePhoto);
				dictionary.Add("LargePhotoFileName",  LargePhotoFileName);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("ThumbNailPhoto", out value))
					ThumbNailPhoto = (string)value;
				if (properties.TryGetValue("ThumbNailPhotoFileName", out value))
					ThumbNailPhotoFileName = (string)value;
				if (properties.TryGetValue("LargePhoto", out value))
					LargePhoto = (string)value;
				if (properties.TryGetValue("LargePhotoFileName", out value))
					LargePhotoFileName = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IProductPhoto

			public string ThumbNailPhoto { get; set; }
			public string ThumbNailPhotoFileName { get; set; }
			public string LargePhoto { get; set; }
			public string LargePhotoFileName { get; set; }

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

		#region Members for interface IProductPhoto

		public string ThumbNailPhoto { get { LazyGet(); return InnerData.ThumbNailPhoto; } set { if (LazySet(Members.ThumbNailPhoto, InnerData.ThumbNailPhoto, value)) InnerData.ThumbNailPhoto = value; } }
		public string ThumbNailPhotoFileName { get { LazyGet(); return InnerData.ThumbNailPhotoFileName; } set { if (LazySet(Members.ThumbNailPhotoFileName, InnerData.ThumbNailPhotoFileName, value)) InnerData.ThumbNailPhotoFileName = value; } }
		public string LargePhoto { get { LazyGet(); return InnerData.LargePhoto; } set { if (LazySet(Members.LargePhoto, InnerData.LargePhoto, value)) InnerData.LargePhoto = value; } }
		public string LargePhotoFileName { get { LazyGet(); return InnerData.LargePhotoFileName; } set { if (LazySet(Members.LargePhotoFileName, InnerData.LargePhotoFileName, value)) InnerData.LargePhotoFileName = value; } }

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

        private static ProductPhotoMembers members = null;
        public static ProductPhotoMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(ProductPhoto))
                    {
                        if (members == null)
                            members = new ProductPhotoMembers();
                    }
                }
                return members;
            }
        }
        public class ProductPhotoMembers
        {
            internal ProductPhotoMembers() { }

			#region Members for interface IProductPhoto

            public Property ThumbNailPhoto { get; } = Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["ThumbNailPhoto"];
            public Property ThumbNailPhotoFileName { get; } = Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["ThumbNailPhotoFileName"];
            public Property LargePhoto { get; } = Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["LargePhoto"];
            public Property LargePhotoFileName { get; } = Datastore.AdventureWorks.Model.Entities["ProductPhoto"].Properties["LargePhotoFileName"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static ProductPhotoFullTextMembers fullTextMembers = null;
        public static ProductPhotoFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(ProductPhoto))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new ProductPhotoFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class ProductPhotoFullTextMembers
        {
            internal ProductPhotoFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(ProductPhoto))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["ProductPhoto"];
                }
            }
            return entity;
        }

		private static ProductPhotoEvents events = null;
        public static ProductPhotoEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(ProductPhoto))
                    {
                        if (events == null)
                            events = new ProductPhotoEvents();
                    }
                }
                return events;
            }
        }
        public class ProductPhotoEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<ProductPhoto, EntityEventArgs> onNew;
            public event EventHandler<ProductPhoto, EntityEventArgs> OnNew
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
                EventHandler<ProductPhoto, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((ProductPhoto)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<ProductPhoto, EntityEventArgs> onDelete;
            public event EventHandler<ProductPhoto, EntityEventArgs> OnDelete
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
                EventHandler<ProductPhoto, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((ProductPhoto)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<ProductPhoto, EntityEventArgs> onSave;
            public event EventHandler<ProductPhoto, EntityEventArgs> OnSave
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
                EventHandler<ProductPhoto, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((ProductPhoto)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnThumbNailPhoto

				private static bool onThumbNailPhotoIsRegistered = false;

				private static EventHandler<ProductPhoto, PropertyEventArgs> onThumbNailPhoto;
				public static event EventHandler<ProductPhoto, PropertyEventArgs> OnThumbNailPhoto
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onThumbNailPhotoIsRegistered)
							{
								Members.ThumbNailPhoto.Events.OnChange -= onThumbNailPhotoProxy;
								Members.ThumbNailPhoto.Events.OnChange += onThumbNailPhotoProxy;
								onThumbNailPhotoIsRegistered = true;
							}
							onThumbNailPhoto += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onThumbNailPhoto -= value;
							if (onThumbNailPhoto == null && onThumbNailPhotoIsRegistered)
							{
								Members.ThumbNailPhoto.Events.OnChange -= onThumbNailPhotoProxy;
								onThumbNailPhotoIsRegistered = false;
							}
						}
					}
				}
            
				private static void onThumbNailPhotoProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductPhoto, PropertyEventArgs> handler = onThumbNailPhoto;
					if ((object)handler != null)
						handler.Invoke((ProductPhoto)sender, args);
				}

				#endregion

				#region OnThumbNailPhotoFileName

				private static bool onThumbNailPhotoFileNameIsRegistered = false;

				private static EventHandler<ProductPhoto, PropertyEventArgs> onThumbNailPhotoFileName;
				public static event EventHandler<ProductPhoto, PropertyEventArgs> OnThumbNailPhotoFileName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onThumbNailPhotoFileNameIsRegistered)
							{
								Members.ThumbNailPhotoFileName.Events.OnChange -= onThumbNailPhotoFileNameProxy;
								Members.ThumbNailPhotoFileName.Events.OnChange += onThumbNailPhotoFileNameProxy;
								onThumbNailPhotoFileNameIsRegistered = true;
							}
							onThumbNailPhotoFileName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onThumbNailPhotoFileName -= value;
							if (onThumbNailPhotoFileName == null && onThumbNailPhotoFileNameIsRegistered)
							{
								Members.ThumbNailPhotoFileName.Events.OnChange -= onThumbNailPhotoFileNameProxy;
								onThumbNailPhotoFileNameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onThumbNailPhotoFileNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductPhoto, PropertyEventArgs> handler = onThumbNailPhotoFileName;
					if ((object)handler != null)
						handler.Invoke((ProductPhoto)sender, args);
				}

				#endregion

				#region OnLargePhoto

				private static bool onLargePhotoIsRegistered = false;

				private static EventHandler<ProductPhoto, PropertyEventArgs> onLargePhoto;
				public static event EventHandler<ProductPhoto, PropertyEventArgs> OnLargePhoto
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLargePhotoIsRegistered)
							{
								Members.LargePhoto.Events.OnChange -= onLargePhotoProxy;
								Members.LargePhoto.Events.OnChange += onLargePhotoProxy;
								onLargePhotoIsRegistered = true;
							}
							onLargePhoto += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLargePhoto -= value;
							if (onLargePhoto == null && onLargePhotoIsRegistered)
							{
								Members.LargePhoto.Events.OnChange -= onLargePhotoProxy;
								onLargePhotoIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLargePhotoProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductPhoto, PropertyEventArgs> handler = onLargePhoto;
					if ((object)handler != null)
						handler.Invoke((ProductPhoto)sender, args);
				}

				#endregion

				#region OnLargePhotoFileName

				private static bool onLargePhotoFileNameIsRegistered = false;

				private static EventHandler<ProductPhoto, PropertyEventArgs> onLargePhotoFileName;
				public static event EventHandler<ProductPhoto, PropertyEventArgs> OnLargePhotoFileName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLargePhotoFileNameIsRegistered)
							{
								Members.LargePhotoFileName.Events.OnChange -= onLargePhotoFileNameProxy;
								Members.LargePhotoFileName.Events.OnChange += onLargePhotoFileNameProxy;
								onLargePhotoFileNameIsRegistered = true;
							}
							onLargePhotoFileName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLargePhotoFileName -= value;
							if (onLargePhotoFileName == null && onLargePhotoFileNameIsRegistered)
							{
								Members.LargePhotoFileName.Events.OnChange -= onLargePhotoFileNameProxy;
								onLargePhotoFileNameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLargePhotoFileNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductPhoto, PropertyEventArgs> handler = onLargePhotoFileName;
					if ((object)handler != null)
						handler.Invoke((ProductPhoto)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<ProductPhoto, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<ProductPhoto, PropertyEventArgs> OnModifiedDate
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
					EventHandler<ProductPhoto, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((ProductPhoto)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<ProductPhoto, PropertyEventArgs> onUid;
				public static event EventHandler<ProductPhoto, PropertyEventArgs> OnUid
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
					EventHandler<ProductPhoto, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((ProductPhoto)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IProductPhotoOriginalData

		public IProductPhotoOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IProductPhoto

		string IProductPhotoOriginalData.ThumbNailPhoto { get { return OriginalData.ThumbNailPhoto; } }
		string IProductPhotoOriginalData.ThumbNailPhotoFileName { get { return OriginalData.ThumbNailPhotoFileName; } }
		string IProductPhotoOriginalData.LargePhoto { get { return OriginalData.LargePhoto; } }
		string IProductPhotoOriginalData.LargePhotoFileName { get { return OriginalData.LargePhotoFileName; } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime IProductPhotoOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string IProductPhotoOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}