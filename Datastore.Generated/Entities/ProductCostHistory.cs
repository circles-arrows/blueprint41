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
	public interface IProductCostHistoryOriginalData : ISchemaBaseOriginalData
	{
		System.DateTime StartDate { get; }
		System.DateTime EndDate { get; }
		string StandardCost { get; }
		Product Product { get; }
	}

	public partial class ProductCostHistory : OGM<ProductCostHistory, ProductCostHistory.ProductCostHistoryData, System.String>, ISchemaBase, INeo4jBase, IProductCostHistoryOriginalData
	{
		#region Initialize

		static ProductCostHistory()
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

		public static Dictionary<System.String, ProductCostHistory> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ProductCostHistoryAlias, IWhereQuery> query)
		{
			q.ProductCostHistoryAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ProductCostHistory.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"ProductCostHistory => StartDate : {this.StartDate}, EndDate : {this.EndDate}, StandardCost : {this.StandardCost}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new ProductCostHistoryData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.StandardCost is null)
				throw new PersistenceException(string.Format("Cannot save ProductCostHistory with key '{0}' because the StandardCost cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ProductCostHistoryData : Data<System.String>
		{
			public ProductCostHistoryData()
			{

			}

			public ProductCostHistoryData(ProductCostHistoryData data)
			{
				StartDate = data.StartDate;
				EndDate = data.EndDate;
				StandardCost = data.StandardCost;
				Product = data.Product;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "ProductCostHistory";

				Product = new EntityCollection<Product>(Wrapper, Members.Product);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("StartDate",  Conversion<System.DateTime, long>.Convert(StartDate));
				dictionary.Add("EndDate",  Conversion<System.DateTime, long>.Convert(EndDate));
				dictionary.Add("StandardCost",  StandardCost);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("StartDate", out value))
					StartDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("EndDate", out value))
					EndDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("StandardCost", out value))
					StandardCost = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IProductCostHistory

			public System.DateTime StartDate { get; set; }
			public System.DateTime EndDate { get; set; }
			public string StandardCost { get; set; }
			public EntityCollection<Product> Product { get; private set; }

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

		#region Members for interface IProductCostHistory

		public System.DateTime StartDate { get { LazyGet(); return InnerData.StartDate; } set { if (LazySet(Members.StartDate, InnerData.StartDate, value)) InnerData.StartDate = value; } }
		public System.DateTime EndDate { get { LazyGet(); return InnerData.EndDate; } set { if (LazySet(Members.EndDate, InnerData.EndDate, value)) InnerData.EndDate = value; } }
		public string StandardCost { get { LazyGet(); return InnerData.StandardCost; } set { if (LazySet(Members.StandardCost, InnerData.StandardCost, value)) InnerData.StandardCost = value; } }
		public Product Product
		{
			get { return ((ILookupHelper<Product>)InnerData.Product).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Product, ((ILookupHelper<Product>)InnerData.Product).GetItem(null), value))
					((ILookupHelper<Product>)InnerData.Product).SetItem(value, null); 
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

		private static ProductCostHistoryMembers members = null;
		public static ProductCostHistoryMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(ProductCostHistory))
					{
						if (members is null)
							members = new ProductCostHistoryMembers();
					}
				}
				return members;
			}
		}
		public class ProductCostHistoryMembers
		{
			internal ProductCostHistoryMembers() { }

			#region Members for interface IProductCostHistory

			public Property StartDate { get; } = Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["StartDate"];
			public Property EndDate { get; } = Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["EndDate"];
			public Property StandardCost { get; } = Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["StandardCost"];
			public Property Product { get; } = Datastore.AdventureWorks.Model.Entities["ProductCostHistory"].Properties["Product"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static ProductCostHistoryFullTextMembers fullTextMembers = null;
		public static ProductCostHistoryFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(ProductCostHistory))
					{
						if (fullTextMembers is null)
							fullTextMembers = new ProductCostHistoryFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class ProductCostHistoryFullTextMembers
		{
			internal ProductCostHistoryFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(ProductCostHistory))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["ProductCostHistory"];
				}
			}
			return entity;
		}

		private static ProductCostHistoryEvents events = null;
		public static ProductCostHistoryEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(ProductCostHistory))
					{
						if (events is null)
							events = new ProductCostHistoryEvents();
					}
				}
				return events;
			}
		}
		public class ProductCostHistoryEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<ProductCostHistory, EntityEventArgs> onNew;
			public event EventHandler<ProductCostHistory, EntityEventArgs> OnNew
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
				EventHandler<ProductCostHistory, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((ProductCostHistory)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<ProductCostHistory, EntityEventArgs> onDelete;
			public event EventHandler<ProductCostHistory, EntityEventArgs> OnDelete
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
				EventHandler<ProductCostHistory, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((ProductCostHistory)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<ProductCostHistory, EntityEventArgs> onSave;
			public event EventHandler<ProductCostHistory, EntityEventArgs> OnSave
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
				EventHandler<ProductCostHistory, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((ProductCostHistory)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<ProductCostHistory, EntityEventArgs> onAfterSave;
			public event EventHandler<ProductCostHistory, EntityEventArgs> OnAfterSave
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
				EventHandler<ProductCostHistory, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((ProductCostHistory)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnStartDate

				private static bool onStartDateIsRegistered = false;

				private static EventHandler<ProductCostHistory, PropertyEventArgs> onStartDate;
				public static event EventHandler<ProductCostHistory, PropertyEventArgs> OnStartDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStartDateIsRegistered)
							{
								Members.StartDate.Events.OnChange -= onStartDateProxy;
								Members.StartDate.Events.OnChange += onStartDateProxy;
								onStartDateIsRegistered = true;
							}
							onStartDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStartDate -= value;
							if (onStartDate is null && onStartDateIsRegistered)
							{
								Members.StartDate.Events.OnChange -= onStartDateProxy;
								onStartDateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onStartDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductCostHistory, PropertyEventArgs> handler = onStartDate;
					if (handler is not null)
						handler.Invoke((ProductCostHistory)sender, args);
				}

				#endregion

				#region OnEndDate

				private static bool onEndDateIsRegistered = false;

				private static EventHandler<ProductCostHistory, PropertyEventArgs> onEndDate;
				public static event EventHandler<ProductCostHistory, PropertyEventArgs> OnEndDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEndDateIsRegistered)
							{
								Members.EndDate.Events.OnChange -= onEndDateProxy;
								Members.EndDate.Events.OnChange += onEndDateProxy;
								onEndDateIsRegistered = true;
							}
							onEndDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEndDate -= value;
							if (onEndDate is null && onEndDateIsRegistered)
							{
								Members.EndDate.Events.OnChange -= onEndDateProxy;
								onEndDateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onEndDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductCostHistory, PropertyEventArgs> handler = onEndDate;
					if (handler is not null)
						handler.Invoke((ProductCostHistory)sender, args);
				}

				#endregion

				#region OnStandardCost

				private static bool onStandardCostIsRegistered = false;

				private static EventHandler<ProductCostHistory, PropertyEventArgs> onStandardCost;
				public static event EventHandler<ProductCostHistory, PropertyEventArgs> OnStandardCost
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onStandardCostIsRegistered)
							{
								Members.StandardCost.Events.OnChange -= onStandardCostProxy;
								Members.StandardCost.Events.OnChange += onStandardCostProxy;
								onStandardCostIsRegistered = true;
							}
							onStandardCost += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onStandardCost -= value;
							if (onStandardCost is null && onStandardCostIsRegistered)
							{
								Members.StandardCost.Events.OnChange -= onStandardCostProxy;
								onStandardCostIsRegistered = false;
							}
						}
					}
				}
			
				private static void onStandardCostProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductCostHistory, PropertyEventArgs> handler = onStandardCost;
					if (handler is not null)
						handler.Invoke((ProductCostHistory)sender, args);
				}

				#endregion

				#region OnProduct

				private static bool onProductIsRegistered = false;

				private static EventHandler<ProductCostHistory, PropertyEventArgs> onProduct;
				public static event EventHandler<ProductCostHistory, PropertyEventArgs> OnProduct
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductIsRegistered)
							{
								Members.Product.Events.OnChange -= onProductProxy;
								Members.Product.Events.OnChange += onProductProxy;
								onProductIsRegistered = true;
							}
							onProduct += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProduct -= value;
							if (onProduct is null && onProductIsRegistered)
							{
								Members.Product.Events.OnChange -= onProductProxy;
								onProductIsRegistered = false;
							}
						}
					}
				}
			
				private static void onProductProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductCostHistory, PropertyEventArgs> handler = onProduct;
					if (handler is not null)
						handler.Invoke((ProductCostHistory)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<ProductCostHistory, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<ProductCostHistory, PropertyEventArgs> OnModifiedDate
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
					EventHandler<ProductCostHistory, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((ProductCostHistory)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<ProductCostHistory, PropertyEventArgs> onUid;
				public static event EventHandler<ProductCostHistory, PropertyEventArgs> OnUid
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
					EventHandler<ProductCostHistory, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((ProductCostHistory)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IProductCostHistoryOriginalData

		public IProductCostHistoryOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IProductCostHistory

		System.DateTime IProductCostHistoryOriginalData.StartDate { get { return OriginalData.StartDate; } }
		System.DateTime IProductCostHistoryOriginalData.EndDate { get { return OriginalData.EndDate; } }
		string IProductCostHistoryOriginalData.StandardCost { get { return OriginalData.StandardCost; } }
		Product IProductCostHistoryOriginalData.Product { get { return ((ILookupHelper<Product>)OriginalData.Product).GetOriginalItem(null); } }

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