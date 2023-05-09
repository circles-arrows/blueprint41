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
	public interface IProductDescriptionOriginalData : ISchemaBaseOriginalData
	{
		string Description { get; }
		string rowguid { get; }
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
			#region LoadByKeys
			
			RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
				Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

			#endregion

			AdditionalGeneratedStoredQueries();
		}
		partial void AdditionalGeneratedStoredQueries();

		public static Dictionary<System.String, ProductDescription> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ProductDescriptionAlias, IWhereQuery> query)
		{
			q.ProductDescriptionAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.ProductDescription.Alias(out alias, "node"));
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
				if (ReferenceEquals(InnerData, OriginalData))
					OriginalData = new ProductDescriptionData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.Description is null)
				throw new PersistenceException(string.Format("Cannot save ProductDescription with key '{0}' because the Description cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid is null)
				throw new PersistenceException(string.Format("Cannot save ProductDescription with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
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
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

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
				if (members is null)
				{
					lock (typeof(ProductDescription))
					{
						if (members is null)
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
				if (fullTextMembers is null)
				{
					lock (typeof(ProductDescription))
					{
						if (fullTextMembers is null)
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
			if (entity is null)
			{
				lock (typeof(ProductDescription))
				{
					if (entity is null)
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
				if (events is null)
				{
					lock (typeof(ProductDescription))
					{
						if (events is null)
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
				EventHandler<ProductDescription, EntityEventArgs> handler = onNew;
				if (handler is not null)
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
				EventHandler<ProductDescription, EntityEventArgs> handler = onDelete;
				if (handler is not null)
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
				EventHandler<ProductDescription, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((ProductDescription)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<ProductDescription, EntityEventArgs> onAfterSave;
			public event EventHandler<ProductDescription, EntityEventArgs> OnAfterSave
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
				EventHandler<ProductDescription, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
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
							if (onDescription is null && onDescriptionIsRegistered)
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
					if (handler is not null)
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
					EventHandler<ProductDescription, PropertyEventArgs> handler = onrowguid;
					if (handler is not null)
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
					EventHandler<ProductDescription, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
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
					EventHandler<ProductDescription, PropertyEventArgs> handler = onUid;
					if (handler is not null)
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