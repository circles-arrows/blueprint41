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
	public interface IProductModelOriginalData : ISchemaBaseOriginalData
	{
		string Name { get; }
		string CatalogDescription { get; }
		string Instructions { get; }
		string rowguid { get; }
		Illustration Illustration { get; }
		ProductDescription ProductDescription { get; }
		Culture Culture { get; }
	}

	public partial class ProductModel : OGM<ProductModel, ProductModel.ProductModelData, System.String>, ISchemaBase, INeo4jBase, IProductModelOriginalData
	{
		#region Initialize

		static ProductModel()
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

		public static Dictionary<System.String, ProductModel> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ProductModelAlias, IWhereQuery> query)
		{
			q.ProductModelAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ProductModel.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"ProductModel => Name : {this.Name}, CatalogDescription : {this.CatalogDescription?.ToString() ?? "null"}, Instructions : {this.Instructions?.ToString() ?? "null"}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new ProductModelData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.Name is null)
				throw new PersistenceException(string.Format("Cannot save ProductModel with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid is null)
				throw new PersistenceException(string.Format("Cannot save ProductModel with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class ProductModelData : Data<System.String>
		{
			public ProductModelData()
			{

			}

			public ProductModelData(ProductModelData data)
			{
				Name = data.Name;
				CatalogDescription = data.CatalogDescription;
				Instructions = data.Instructions;
				rowguid = data.rowguid;
				Illustration = data.Illustration;
				ProductDescription = data.ProductDescription;
				Culture = data.Culture;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "ProductModel";

				Illustration = new EntityCollection<Illustration>(Wrapper, Members.Illustration);
				ProductDescription = new EntityCollection<ProductDescription>(Wrapper, Members.ProductDescription);
				Culture = new EntityCollection<Culture>(Wrapper, Members.Culture);
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Name",  Name);
				dictionary.Add("CatalogDescription",  CatalogDescription);
				dictionary.Add("Instructions",  Instructions);
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
				if (properties.TryGetValue("CatalogDescription", out value))
					CatalogDescription = (string)value;
				if (properties.TryGetValue("Instructions", out value))
					Instructions = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IProductModel

			public string Name { get; set; }
			public string CatalogDescription { get; set; }
			public string Instructions { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<Illustration> Illustration { get; private set; }
			public EntityCollection<ProductDescription> ProductDescription { get; private set; }
			public EntityCollection<Culture> Culture { get; private set; }

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

		#region Members for interface IProductModel

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public string CatalogDescription { get { LazyGet(); return InnerData.CatalogDescription; } set { if (LazySet(Members.CatalogDescription, InnerData.CatalogDescription, value)) InnerData.CatalogDescription = value; } }
		public string Instructions { get { LazyGet(); return InnerData.Instructions; } set { if (LazySet(Members.Instructions, InnerData.Instructions, value)) InnerData.Instructions = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public Illustration Illustration
		{
			get { return ((ILookupHelper<Illustration>)InnerData.Illustration).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Illustration, ((ILookupHelper<Illustration>)InnerData.Illustration).GetItem(null), value))
					((ILookupHelper<Illustration>)InnerData.Illustration).SetItem(value, null); 
			}
		}
		public ProductDescription ProductDescription
		{
			get { return ((ILookupHelper<ProductDescription>)InnerData.ProductDescription).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.ProductDescription, ((ILookupHelper<ProductDescription>)InnerData.ProductDescription).GetItem(null), value))
					((ILookupHelper<ProductDescription>)InnerData.ProductDescription).SetItem(value, null); 
			}
		}
		public Culture Culture
		{
			get { return ((ILookupHelper<Culture>)InnerData.Culture).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Culture, ((ILookupHelper<Culture>)InnerData.Culture).GetItem(null), value))
					((ILookupHelper<Culture>)InnerData.Culture).SetItem(value, null); 
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

		private static ProductModelMembers members = null;
		public static ProductModelMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(ProductModel))
					{
						if (members is null)
							members = new ProductModelMembers();
					}
				}
				return members;
			}
		}
		public class ProductModelMembers
		{
			internal ProductModelMembers() { }

			#region Members for interface IProductModel

			public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["Name"];
			public Property CatalogDescription { get; } = Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["CatalogDescription"];
			public Property Instructions { get; } = Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["Instructions"];
			public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["rowguid"];
			public Property Illustration { get; } = Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["Illustration"];
			public Property ProductDescription { get; } = Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["ProductDescription"];
			public Property Culture { get; } = Datastore.AdventureWorks.Model.Entities["ProductModel"].Properties["Culture"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static ProductModelFullTextMembers fullTextMembers = null;
		public static ProductModelFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(ProductModel))
					{
						if (fullTextMembers is null)
							fullTextMembers = new ProductModelFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class ProductModelFullTextMembers
		{
			internal ProductModelFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(ProductModel))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["ProductModel"];
				}
			}
			return entity;
		}

		private static ProductModelEvents events = null;
		public static ProductModelEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(ProductModel))
					{
						if (events is null)
							events = new ProductModelEvents();
					}
				}
				return events;
			}
		}
		public class ProductModelEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<ProductModel, EntityEventArgs> onNew;
			public event EventHandler<ProductModel, EntityEventArgs> OnNew
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
				EventHandler<ProductModel, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((ProductModel)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<ProductModel, EntityEventArgs> onDelete;
			public event EventHandler<ProductModel, EntityEventArgs> OnDelete
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
				EventHandler<ProductModel, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((ProductModel)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<ProductModel, EntityEventArgs> onSave;
			public event EventHandler<ProductModel, EntityEventArgs> OnSave
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
				EventHandler<ProductModel, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((ProductModel)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<ProductModel, EntityEventArgs> onAfterSave;
			public event EventHandler<ProductModel, EntityEventArgs> OnAfterSave
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
				EventHandler<ProductModel, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((ProductModel)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<ProductModel, PropertyEventArgs> onName;
				public static event EventHandler<ProductModel, PropertyEventArgs> OnName
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
							if (onName is null && onNameIsRegistered)
							{
								Members.Name.Events.OnChange -= onNameProxy;
								onNameIsRegistered = false;
							}
						}
					}
				}
			
				private static void onNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductModel, PropertyEventArgs> handler = onName;
					if (handler is not null)
						handler.Invoke((ProductModel)sender, args);
				}

				#endregion

				#region OnCatalogDescription

				private static bool onCatalogDescriptionIsRegistered = false;

				private static EventHandler<ProductModel, PropertyEventArgs> onCatalogDescription;
				public static event EventHandler<ProductModel, PropertyEventArgs> OnCatalogDescription
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCatalogDescriptionIsRegistered)
							{
								Members.CatalogDescription.Events.OnChange -= onCatalogDescriptionProxy;
								Members.CatalogDescription.Events.OnChange += onCatalogDescriptionProxy;
								onCatalogDescriptionIsRegistered = true;
							}
							onCatalogDescription += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCatalogDescription -= value;
							if (onCatalogDescription is null && onCatalogDescriptionIsRegistered)
							{
								Members.CatalogDescription.Events.OnChange -= onCatalogDescriptionProxy;
								onCatalogDescriptionIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCatalogDescriptionProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductModel, PropertyEventArgs> handler = onCatalogDescription;
					if (handler is not null)
						handler.Invoke((ProductModel)sender, args);
				}

				#endregion

				#region OnInstructions

				private static bool onInstructionsIsRegistered = false;

				private static EventHandler<ProductModel, PropertyEventArgs> onInstructions;
				public static event EventHandler<ProductModel, PropertyEventArgs> OnInstructions
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onInstructionsIsRegistered)
							{
								Members.Instructions.Events.OnChange -= onInstructionsProxy;
								Members.Instructions.Events.OnChange += onInstructionsProxy;
								onInstructionsIsRegistered = true;
							}
							onInstructions += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onInstructions -= value;
							if (onInstructions is null && onInstructionsIsRegistered)
							{
								Members.Instructions.Events.OnChange -= onInstructionsProxy;
								onInstructionsIsRegistered = false;
							}
						}
					}
				}
			
				private static void onInstructionsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductModel, PropertyEventArgs> handler = onInstructions;
					if (handler is not null)
						handler.Invoke((ProductModel)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<ProductModel, PropertyEventArgs> onrowguid;
				public static event EventHandler<ProductModel, PropertyEventArgs> Onrowguid
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
							if (onrowguid is null && onrowguidIsRegistered)
							{
								Members.rowguid.Events.OnChange -= onrowguidProxy;
								onrowguidIsRegistered = false;
							}
						}
					}
				}
			
				private static void onrowguidProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductModel, PropertyEventArgs> handler = onrowguid;
					if (handler is not null)
						handler.Invoke((ProductModel)sender, args);
				}

				#endregion

				#region OnIllustration

				private static bool onIllustrationIsRegistered = false;

				private static EventHandler<ProductModel, PropertyEventArgs> onIllustration;
				public static event EventHandler<ProductModel, PropertyEventArgs> OnIllustration
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onIllustrationIsRegistered)
							{
								Members.Illustration.Events.OnChange -= onIllustrationProxy;
								Members.Illustration.Events.OnChange += onIllustrationProxy;
								onIllustrationIsRegistered = true;
							}
							onIllustration += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onIllustration -= value;
							if (onIllustration is null && onIllustrationIsRegistered)
							{
								Members.Illustration.Events.OnChange -= onIllustrationProxy;
								onIllustrationIsRegistered = false;
							}
						}
					}
				}
			
				private static void onIllustrationProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductModel, PropertyEventArgs> handler = onIllustration;
					if (handler is not null)
						handler.Invoke((ProductModel)sender, args);
				}

				#endregion

				#region OnProductDescription

				private static bool onProductDescriptionIsRegistered = false;

				private static EventHandler<ProductModel, PropertyEventArgs> onProductDescription;
				public static event EventHandler<ProductModel, PropertyEventArgs> OnProductDescription
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProductDescriptionIsRegistered)
							{
								Members.ProductDescription.Events.OnChange -= onProductDescriptionProxy;
								Members.ProductDescription.Events.OnChange += onProductDescriptionProxy;
								onProductDescriptionIsRegistered = true;
							}
							onProductDescription += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProductDescription -= value;
							if (onProductDescription is null && onProductDescriptionIsRegistered)
							{
								Members.ProductDescription.Events.OnChange -= onProductDescriptionProxy;
								onProductDescriptionIsRegistered = false;
							}
						}
					}
				}
			
				private static void onProductDescriptionProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductModel, PropertyEventArgs> handler = onProductDescription;
					if (handler is not null)
						handler.Invoke((ProductModel)sender, args);
				}

				#endregion

				#region OnCulture

				private static bool onCultureIsRegistered = false;

				private static EventHandler<ProductModel, PropertyEventArgs> onCulture;
				public static event EventHandler<ProductModel, PropertyEventArgs> OnCulture
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCultureIsRegistered)
							{
								Members.Culture.Events.OnChange -= onCultureProxy;
								Members.Culture.Events.OnChange += onCultureProxy;
								onCultureIsRegistered = true;
							}
							onCulture += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCulture -= value;
							if (onCulture is null && onCultureIsRegistered)
							{
								Members.Culture.Events.OnChange -= onCultureProxy;
								onCultureIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCultureProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<ProductModel, PropertyEventArgs> handler = onCulture;
					if (handler is not null)
						handler.Invoke((ProductModel)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<ProductModel, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<ProductModel, PropertyEventArgs> OnModifiedDate
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
					EventHandler<ProductModel, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((ProductModel)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<ProductModel, PropertyEventArgs> onUid;
				public static event EventHandler<ProductModel, PropertyEventArgs> OnUid
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
					EventHandler<ProductModel, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((ProductModel)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IProductModelOriginalData

		public IProductModelOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IProductModel

		string IProductModelOriginalData.Name { get { return OriginalData.Name; } }
		string IProductModelOriginalData.CatalogDescription { get { return OriginalData.CatalogDescription; } }
		string IProductModelOriginalData.Instructions { get { return OriginalData.Instructions; } }
		string IProductModelOriginalData.rowguid { get { return OriginalData.rowguid; } }
		Illustration IProductModelOriginalData.Illustration { get { return ((ILookupHelper<Illustration>)OriginalData.Illustration).GetOriginalItem(null); } }
		ProductDescription IProductModelOriginalData.ProductDescription { get { return ((ILookupHelper<ProductDescription>)OriginalData.ProductDescription).GetOriginalItem(null); } }
		Culture IProductModelOriginalData.Culture { get { return ((ILookupHelper<Culture>)OriginalData.Culture).GetOriginalItem(null); } }

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