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
	public interface IProductReviewOriginalData : ISchemaBaseOriginalData
    {
		string ReviewerName { get; }
		System.DateTime ReviewDate { get; }
		string EmailAddress { get; }
		string Rating { get; }
		string Comments { get; }
		IEnumerable<Product> Products { get; }
    }

	public partial class ProductReview : OGM<ProductReview, ProductReview.ProductReviewData, System.String>, ISchemaBase, INeo4jBase, IProductReviewOriginalData
	{
        #region Initialize

        static ProductReview()
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

        public static Dictionary<System.String, ProductReview> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ProductReviewAlias, IWhereQuery> query)
        {
            q.ProductReviewAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ProductReview.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"ProductReview => ReviewerName : {this.ReviewerName}, ReviewDate : {this.ReviewDate}, EmailAddress : {this.EmailAddress}, Rating : {this.Rating}, Comments : {this.Comments}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new ProductReviewData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.ReviewerName == null)
				throw new PersistenceException(string.Format("Cannot save ProductReview with key '{0}' because the ReviewerName cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ReviewDate == null)
				throw new PersistenceException(string.Format("Cannot save ProductReview with key '{0}' because the ReviewDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.EmailAddress == null)
				throw new PersistenceException(string.Format("Cannot save ProductReview with key '{0}' because the EmailAddress cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Rating == null)
				throw new PersistenceException(string.Format("Cannot save ProductReview with key '{0}' because the Rating cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Comments == null)
				throw new PersistenceException(string.Format("Cannot save ProductReview with key '{0}' because the Comments cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save ProductReview with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ProductReviewData : Data<System.String>
		{
			public ProductReviewData()
            {

            }

            public ProductReviewData(ProductReviewData data)
            {
				ReviewerName = data.ReviewerName;
				ReviewDate = data.ReviewDate;
				EmailAddress = data.EmailAddress;
				Rating = data.Rating;
				Comments = data.Comments;
				Products = data.Products;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "ProductReview";

				Products = new EntityCollection<Product>(Wrapper, Members.Products, item => { if (Members.Products.Events.HasRegisteredChangeHandlers) { object loadHack = item.ProductReview; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("ReviewerName",  ReviewerName);
				dictionary.Add("ReviewDate",  Conversion<System.DateTime, long>.Convert(ReviewDate));
				dictionary.Add("EmailAddress",  EmailAddress);
				dictionary.Add("Rating",  Rating);
				dictionary.Add("Comments",  Comments);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("ReviewerName", out value))
					ReviewerName = (string)value;
				if (properties.TryGetValue("ReviewDate", out value))
					ReviewDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("EmailAddress", out value))
					EmailAddress = (string)value;
				if (properties.TryGetValue("Rating", out value))
					Rating = (string)value;
				if (properties.TryGetValue("Comments", out value))
					Comments = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IProductReview

			public string ReviewerName { get; set; }
			public System.DateTime ReviewDate { get; set; }
			public string EmailAddress { get; set; }
			public string Rating { get; set; }
			public string Comments { get; set; }
			public EntityCollection<Product> Products { get; private set; }

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

		#region Members for interface IProductReview

		public string ReviewerName { get { LazyGet(); return InnerData.ReviewerName; } set { if (LazySet(Members.ReviewerName, InnerData.ReviewerName, value)) InnerData.ReviewerName = value; } }
		public System.DateTime ReviewDate { get { LazyGet(); return InnerData.ReviewDate; } set { if (LazySet(Members.ReviewDate, InnerData.ReviewDate, value)) InnerData.ReviewDate = value; } }
		public string EmailAddress { get { LazyGet(); return InnerData.EmailAddress; } set { if (LazySet(Members.EmailAddress, InnerData.EmailAddress, value)) InnerData.EmailAddress = value; } }
		public string Rating { get { LazyGet(); return InnerData.Rating; } set { if (LazySet(Members.Rating, InnerData.Rating, value)) InnerData.Rating = value; } }
		public string Comments { get { LazyGet(); return InnerData.Comments; } set { if (LazySet(Members.Comments, InnerData.Comments, value)) InnerData.Comments = value; } }
		public EntityCollection<Product> Products { get { return InnerData.Products; } }
		private void ClearProducts(DateTime? moment)
		{
			((ILookupHelper<Product>)InnerData.Products).ClearLookup(moment);
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

        private static ProductReviewMembers members = null;
        public static ProductReviewMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(ProductReview))
                    {
                        if (members == null)
                            members = new ProductReviewMembers();
                    }
                }
                return members;
            }
        }
        public class ProductReviewMembers
        {
            internal ProductReviewMembers() { }

			#region Members for interface IProductReview

            public Property ReviewerName { get; } = Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["ReviewerName"];
            public Property ReviewDate { get; } = Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["ReviewDate"];
            public Property EmailAddress { get; } = Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["EmailAddress"];
            public Property Rating { get; } = Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["Rating"];
            public Property Comments { get; } = Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["Comments"];
            public Property Products { get; } = Datastore.AdventureWorks.Model.Entities["ProductReview"].Properties["Products"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static ProductReviewFullTextMembers fullTextMembers = null;
        public static ProductReviewFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(ProductReview))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new ProductReviewFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class ProductReviewFullTextMembers
        {
            internal ProductReviewFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(ProductReview))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["ProductReview"];
                }
            }
            return entity;
        }

		private static ProductReviewEvents events = null;
        public static ProductReviewEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(ProductReview))
                    {
                        if (events == null)
                            events = new ProductReviewEvents();
                    }
                }
                return events;
            }
        }
        public class ProductReviewEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<ProductReview, EntityEventArgs> onNew;
            public event EventHandler<ProductReview, EntityEventArgs> OnNew
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
                EventHandler<ProductReview, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((ProductReview)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<ProductReview, EntityEventArgs> onDelete;
            public event EventHandler<ProductReview, EntityEventArgs> OnDelete
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
                EventHandler<ProductReview, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((ProductReview)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<ProductReview, EntityEventArgs> onSave;
            public event EventHandler<ProductReview, EntityEventArgs> OnSave
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
                EventHandler<ProductReview, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((ProductReview)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnReviewerName

				private static bool onReviewerNameIsRegistered = false;

				private static EventHandler<ProductReview, PropertyEventArgs> onReviewerName;
				public static event EventHandler<ProductReview, PropertyEventArgs> OnReviewerName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onReviewerNameIsRegistered)
							{
								Members.ReviewerName.Events.OnChange -= onReviewerNameProxy;
								Members.ReviewerName.Events.OnChange += onReviewerNameProxy;
								onReviewerNameIsRegistered = true;
							}
							onReviewerName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onReviewerName -= value;
							if (onReviewerName == null && onReviewerNameIsRegistered)
							{
								Members.ReviewerName.Events.OnChange -= onReviewerNameProxy;
								onReviewerNameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onReviewerNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductReview, PropertyEventArgs> handler = onReviewerName;
					if ((object)handler != null)
						handler.Invoke((ProductReview)sender, args);
				}

				#endregion

				#region OnReviewDate

				private static bool onReviewDateIsRegistered = false;

				private static EventHandler<ProductReview, PropertyEventArgs> onReviewDate;
				public static event EventHandler<ProductReview, PropertyEventArgs> OnReviewDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onReviewDateIsRegistered)
							{
								Members.ReviewDate.Events.OnChange -= onReviewDateProxy;
								Members.ReviewDate.Events.OnChange += onReviewDateProxy;
								onReviewDateIsRegistered = true;
							}
							onReviewDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onReviewDate -= value;
							if (onReviewDate == null && onReviewDateIsRegistered)
							{
								Members.ReviewDate.Events.OnChange -= onReviewDateProxy;
								onReviewDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onReviewDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductReview, PropertyEventArgs> handler = onReviewDate;
					if ((object)handler != null)
						handler.Invoke((ProductReview)sender, args);
				}

				#endregion

				#region OnEmailAddress

				private static bool onEmailAddressIsRegistered = false;

				private static EventHandler<ProductReview, PropertyEventArgs> onEmailAddress;
				public static event EventHandler<ProductReview, PropertyEventArgs> OnEmailAddress
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmailAddressIsRegistered)
							{
								Members.EmailAddress.Events.OnChange -= onEmailAddressProxy;
								Members.EmailAddress.Events.OnChange += onEmailAddressProxy;
								onEmailAddressIsRegistered = true;
							}
							onEmailAddress += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmailAddress -= value;
							if (onEmailAddress == null && onEmailAddressIsRegistered)
							{
								Members.EmailAddress.Events.OnChange -= onEmailAddressProxy;
								onEmailAddressIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEmailAddressProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductReview, PropertyEventArgs> handler = onEmailAddress;
					if ((object)handler != null)
						handler.Invoke((ProductReview)sender, args);
				}

				#endregion

				#region OnRating

				private static bool onRatingIsRegistered = false;

				private static EventHandler<ProductReview, PropertyEventArgs> onRating;
				public static event EventHandler<ProductReview, PropertyEventArgs> OnRating
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onRatingIsRegistered)
							{
								Members.Rating.Events.OnChange -= onRatingProxy;
								Members.Rating.Events.OnChange += onRatingProxy;
								onRatingIsRegistered = true;
							}
							onRating += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onRating -= value;
							if (onRating == null && onRatingIsRegistered)
							{
								Members.Rating.Events.OnChange -= onRatingProxy;
								onRatingIsRegistered = false;
							}
						}
					}
				}
            
				private static void onRatingProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductReview, PropertyEventArgs> handler = onRating;
					if ((object)handler != null)
						handler.Invoke((ProductReview)sender, args);
				}

				#endregion

				#region OnComments

				private static bool onCommentsIsRegistered = false;

				private static EventHandler<ProductReview, PropertyEventArgs> onComments;
				public static event EventHandler<ProductReview, PropertyEventArgs> OnComments
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCommentsIsRegistered)
							{
								Members.Comments.Events.OnChange -= onCommentsProxy;
								Members.Comments.Events.OnChange += onCommentsProxy;
								onCommentsIsRegistered = true;
							}
							onComments += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onComments -= value;
							if (onComments == null && onCommentsIsRegistered)
							{
								Members.Comments.Events.OnChange -= onCommentsProxy;
								onCommentsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCommentsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductReview, PropertyEventArgs> handler = onComments;
					if ((object)handler != null)
						handler.Invoke((ProductReview)sender, args);
				}

				#endregion

				#region OnProducts

				private static bool onProductsIsRegistered = false;

				private static EventHandler<ProductReview, PropertyEventArgs> onProducts;
				public static event EventHandler<ProductReview, PropertyEventArgs> OnProducts
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductsIsRegistered)
							{
								Members.Products.Events.OnChange -= onProductsProxy;
								Members.Products.Events.OnChange += onProductsProxy;
								onProductsIsRegistered = true;
							}
							onProducts += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProducts -= value;
							if (onProducts == null && onProductsIsRegistered)
							{
								Members.Products.Events.OnChange -= onProductsProxy;
								onProductsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductReview, PropertyEventArgs> handler = onProducts;
					if ((object)handler != null)
						handler.Invoke((ProductReview)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<ProductReview, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<ProductReview, PropertyEventArgs> OnModifiedDate
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
					EventHandler<ProductReview, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((ProductReview)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<ProductReview, PropertyEventArgs> onUid;
				public static event EventHandler<ProductReview, PropertyEventArgs> OnUid
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
					EventHandler<ProductReview, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((ProductReview)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IProductReviewOriginalData

		public IProductReviewOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IProductReview

		string IProductReviewOriginalData.ReviewerName { get { return OriginalData.ReviewerName; } }
		System.DateTime IProductReviewOriginalData.ReviewDate { get { return OriginalData.ReviewDate; } }
		string IProductReviewOriginalData.EmailAddress { get { return OriginalData.EmailAddress; } }
		string IProductReviewOriginalData.Rating { get { return OriginalData.Rating; } }
		string IProductReviewOriginalData.Comments { get { return OriginalData.Comments; } }
		IEnumerable<Product> IProductReviewOriginalData.Products { get { return OriginalData.Products.OriginalData; } }

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