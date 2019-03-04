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
	public interface IBillOfMaterialsOriginalData : ISchemaBaseOriginalData
    {
		System.DateTime StartDate { get; }
		System.DateTime? EndDate { get; }
		string UnitMeasureCode { get; }
		string BOMLevel { get; }
		int PerAssemblyQty { get; }
		UnitMeasure UnitMeasure { get; }
		Product Product { get; }
    }

	public partial class BillOfMaterials : OGM<BillOfMaterials, BillOfMaterials.BillOfMaterialsData, System.String>, ISchemaBase, INeo4jBase, IBillOfMaterialsOriginalData
	{
        #region Initialize

        static BillOfMaterials()
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

        public static Dictionary<System.String, BillOfMaterials> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.BillOfMaterialsAlias, IWhereQuery> query)
        {
            q.BillOfMaterialsAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.BillOfMaterials.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"BillOfMaterials => StartDate : {this.StartDate}, EndDate : {this.EndDate?.ToString() ?? "null"}, UnitMeasureCode : {this.UnitMeasureCode}, BOMLevel : {this.BOMLevel}, PerAssemblyQty : {this.PerAssemblyQty}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new BillOfMaterialsData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.StartDate == null)
				throw new PersistenceException(string.Format("Cannot save BillOfMaterials with key '{0}' because the StartDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.UnitMeasureCode == null)
				throw new PersistenceException(string.Format("Cannot save BillOfMaterials with key '{0}' because the UnitMeasureCode cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.BOMLevel == null)
				throw new PersistenceException(string.Format("Cannot save BillOfMaterials with key '{0}' because the BOMLevel cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.PerAssemblyQty == null)
				throw new PersistenceException(string.Format("Cannot save BillOfMaterials with key '{0}' because the PerAssemblyQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save BillOfMaterials with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class BillOfMaterialsData : Data<System.String>
		{
			public BillOfMaterialsData()
            {

            }

            public BillOfMaterialsData(BillOfMaterialsData data)
            {
				StartDate = data.StartDate;
				EndDate = data.EndDate;
				UnitMeasureCode = data.UnitMeasureCode;
				BOMLevel = data.BOMLevel;
				PerAssemblyQty = data.PerAssemblyQty;
				UnitMeasure = data.UnitMeasure;
				Product = data.Product;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "BillOfMaterials";

				UnitMeasure = new EntityCollection<UnitMeasure>(Wrapper, Members.UnitMeasure);
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
				dictionary.Add("EndDate",  Conversion<System.DateTime?, long?>.Convert(EndDate));
				dictionary.Add("UnitMeasureCode",  UnitMeasureCode);
				dictionary.Add("BOMLevel",  BOMLevel);
				dictionary.Add("PerAssemblyQty",  Conversion<int, long>.Convert(PerAssemblyQty));
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
				if (properties.TryGetValue("UnitMeasureCode", out value))
					UnitMeasureCode = (string)value;
				if (properties.TryGetValue("BOMLevel", out value))
					BOMLevel = (string)value;
				if (properties.TryGetValue("PerAssemblyQty", out value))
					PerAssemblyQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IBillOfMaterials

			public System.DateTime StartDate { get; set; }
			public System.DateTime? EndDate { get; set; }
			public string UnitMeasureCode { get; set; }
			public string BOMLevel { get; set; }
			public int PerAssemblyQty { get; set; }
			public EntityCollection<UnitMeasure> UnitMeasure { get; private set; }
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

		#region Members for interface IBillOfMaterials

		public System.DateTime StartDate { get { LazyGet(); return InnerData.StartDate; } set { if (LazySet(Members.StartDate, InnerData.StartDate, value)) InnerData.StartDate = value; } }
		public System.DateTime? EndDate { get { LazyGet(); return InnerData.EndDate; } set { if (LazySet(Members.EndDate, InnerData.EndDate, value)) InnerData.EndDate = value; } }
		public string UnitMeasureCode { get { LazyGet(); return InnerData.UnitMeasureCode; } set { if (LazySet(Members.UnitMeasureCode, InnerData.UnitMeasureCode, value)) InnerData.UnitMeasureCode = value; } }
		public string BOMLevel { get { LazyGet(); return InnerData.BOMLevel; } set { if (LazySet(Members.BOMLevel, InnerData.BOMLevel, value)) InnerData.BOMLevel = value; } }
		public int PerAssemblyQty { get { LazyGet(); return InnerData.PerAssemblyQty; } set { if (LazySet(Members.PerAssemblyQty, InnerData.PerAssemblyQty, value)) InnerData.PerAssemblyQty = value; } }
		public UnitMeasure UnitMeasure
		{
			get { return ((ILookupHelper<UnitMeasure>)InnerData.UnitMeasure).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.UnitMeasure, ((ILookupHelper<UnitMeasure>)InnerData.UnitMeasure).GetItem(null), value))
					((ILookupHelper<UnitMeasure>)InnerData.UnitMeasure).SetItem(value, null); 
			}
		}
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

        private static BillOfMaterialsMembers members = null;
        public static BillOfMaterialsMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(BillOfMaterials))
                    {
                        if (members == null)
                            members = new BillOfMaterialsMembers();
                    }
                }
                return members;
            }
        }
        public class BillOfMaterialsMembers
        {
            internal BillOfMaterialsMembers() { }

			#region Members for interface IBillOfMaterials

            public Property StartDate { get; } = Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["StartDate"];
            public Property EndDate { get; } = Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["EndDate"];
            public Property UnitMeasureCode { get; } = Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["UnitMeasureCode"];
            public Property BOMLevel { get; } = Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["BOMLevel"];
            public Property PerAssemblyQty { get; } = Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["PerAssemblyQty"];
            public Property UnitMeasure { get; } = Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["UnitMeasure"];
            public Property Product { get; } = Datastore.AdventureWorks.Model.Entities["BillOfMaterials"].Properties["Product"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static BillOfMaterialsFullTextMembers fullTextMembers = null;
        public static BillOfMaterialsFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(BillOfMaterials))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new BillOfMaterialsFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class BillOfMaterialsFullTextMembers
        {
            internal BillOfMaterialsFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(BillOfMaterials))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["BillOfMaterials"];
                }
            }
            return entity;
        }

		private static BillOfMaterialsEvents events = null;
        public static BillOfMaterialsEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(BillOfMaterials))
                    {
                        if (events == null)
                            events = new BillOfMaterialsEvents();
                    }
                }
                return events;
            }
        }
        public class BillOfMaterialsEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<BillOfMaterials, EntityEventArgs> onNew;
            public event EventHandler<BillOfMaterials, EntityEventArgs> OnNew
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
                EventHandler<BillOfMaterials, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((BillOfMaterials)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<BillOfMaterials, EntityEventArgs> onDelete;
            public event EventHandler<BillOfMaterials, EntityEventArgs> OnDelete
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
                EventHandler<BillOfMaterials, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((BillOfMaterials)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<BillOfMaterials, EntityEventArgs> onSave;
            public event EventHandler<BillOfMaterials, EntityEventArgs> OnSave
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
                EventHandler<BillOfMaterials, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((BillOfMaterials)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnStartDate

				private static bool onStartDateIsRegistered = false;

				private static EventHandler<BillOfMaterials, PropertyEventArgs> onStartDate;
				public static event EventHandler<BillOfMaterials, PropertyEventArgs> OnStartDate
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
							if (onStartDate == null && onStartDateIsRegistered)
							{
								Members.StartDate.Events.OnChange -= onStartDateProxy;
								onStartDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onStartDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<BillOfMaterials, PropertyEventArgs> handler = onStartDate;
					if ((object)handler != null)
						handler.Invoke((BillOfMaterials)sender, args);
				}

				#endregion

				#region OnEndDate

				private static bool onEndDateIsRegistered = false;

				private static EventHandler<BillOfMaterials, PropertyEventArgs> onEndDate;
				public static event EventHandler<BillOfMaterials, PropertyEventArgs> OnEndDate
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
							if (onEndDate == null && onEndDateIsRegistered)
							{
								Members.EndDate.Events.OnChange -= onEndDateProxy;
								onEndDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEndDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<BillOfMaterials, PropertyEventArgs> handler = onEndDate;
					if ((object)handler != null)
						handler.Invoke((BillOfMaterials)sender, args);
				}

				#endregion

				#region OnUnitMeasureCode

				private static bool onUnitMeasureCodeIsRegistered = false;

				private static EventHandler<BillOfMaterials, PropertyEventArgs> onUnitMeasureCode;
				public static event EventHandler<BillOfMaterials, PropertyEventArgs> OnUnitMeasureCode
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onUnitMeasureCodeIsRegistered)
							{
								Members.UnitMeasureCode.Events.OnChange -= onUnitMeasureCodeProxy;
								Members.UnitMeasureCode.Events.OnChange += onUnitMeasureCodeProxy;
								onUnitMeasureCodeIsRegistered = true;
							}
							onUnitMeasureCode += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onUnitMeasureCode -= value;
							if (onUnitMeasureCode == null && onUnitMeasureCodeIsRegistered)
							{
								Members.UnitMeasureCode.Events.OnChange -= onUnitMeasureCodeProxy;
								onUnitMeasureCodeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onUnitMeasureCodeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<BillOfMaterials, PropertyEventArgs> handler = onUnitMeasureCode;
					if ((object)handler != null)
						handler.Invoke((BillOfMaterials)sender, args);
				}

				#endregion

				#region OnBOMLevel

				private static bool onBOMLevelIsRegistered = false;

				private static EventHandler<BillOfMaterials, PropertyEventArgs> onBOMLevel;
				public static event EventHandler<BillOfMaterials, PropertyEventArgs> OnBOMLevel
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onBOMLevelIsRegistered)
							{
								Members.BOMLevel.Events.OnChange -= onBOMLevelProxy;
								Members.BOMLevel.Events.OnChange += onBOMLevelProxy;
								onBOMLevelIsRegistered = true;
							}
							onBOMLevel += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onBOMLevel -= value;
							if (onBOMLevel == null && onBOMLevelIsRegistered)
							{
								Members.BOMLevel.Events.OnChange -= onBOMLevelProxy;
								onBOMLevelIsRegistered = false;
							}
						}
					}
				}
            
				private static void onBOMLevelProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<BillOfMaterials, PropertyEventArgs> handler = onBOMLevel;
					if ((object)handler != null)
						handler.Invoke((BillOfMaterials)sender, args);
				}

				#endregion

				#region OnPerAssemblyQty

				private static bool onPerAssemblyQtyIsRegistered = false;

				private static EventHandler<BillOfMaterials, PropertyEventArgs> onPerAssemblyQty;
				public static event EventHandler<BillOfMaterials, PropertyEventArgs> OnPerAssemblyQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPerAssemblyQtyIsRegistered)
							{
								Members.PerAssemblyQty.Events.OnChange -= onPerAssemblyQtyProxy;
								Members.PerAssemblyQty.Events.OnChange += onPerAssemblyQtyProxy;
								onPerAssemblyQtyIsRegistered = true;
							}
							onPerAssemblyQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPerAssemblyQty -= value;
							if (onPerAssemblyQty == null && onPerAssemblyQtyIsRegistered)
							{
								Members.PerAssemblyQty.Events.OnChange -= onPerAssemblyQtyProxy;
								onPerAssemblyQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPerAssemblyQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<BillOfMaterials, PropertyEventArgs> handler = onPerAssemblyQty;
					if ((object)handler != null)
						handler.Invoke((BillOfMaterials)sender, args);
				}

				#endregion

				#region OnUnitMeasure

				private static bool onUnitMeasureIsRegistered = false;

				private static EventHandler<BillOfMaterials, PropertyEventArgs> onUnitMeasure;
				public static event EventHandler<BillOfMaterials, PropertyEventArgs> OnUnitMeasure
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onUnitMeasureIsRegistered)
							{
								Members.UnitMeasure.Events.OnChange -= onUnitMeasureProxy;
								Members.UnitMeasure.Events.OnChange += onUnitMeasureProxy;
								onUnitMeasureIsRegistered = true;
							}
							onUnitMeasure += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onUnitMeasure -= value;
							if (onUnitMeasure == null && onUnitMeasureIsRegistered)
							{
								Members.UnitMeasure.Events.OnChange -= onUnitMeasureProxy;
								onUnitMeasureIsRegistered = false;
							}
						}
					}
				}
            
				private static void onUnitMeasureProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<BillOfMaterials, PropertyEventArgs> handler = onUnitMeasure;
					if ((object)handler != null)
						handler.Invoke((BillOfMaterials)sender, args);
				}

				#endregion

				#region OnProduct

				private static bool onProductIsRegistered = false;

				private static EventHandler<BillOfMaterials, PropertyEventArgs> onProduct;
				public static event EventHandler<BillOfMaterials, PropertyEventArgs> OnProduct
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
							if (onProduct == null && onProductIsRegistered)
							{
								Members.Product.Events.OnChange -= onProductProxy;
								onProductIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProductProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<BillOfMaterials, PropertyEventArgs> handler = onProduct;
					if ((object)handler != null)
						handler.Invoke((BillOfMaterials)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<BillOfMaterials, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<BillOfMaterials, PropertyEventArgs> OnModifiedDate
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
					EventHandler<BillOfMaterials, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((BillOfMaterials)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<BillOfMaterials, PropertyEventArgs> onUid;
				public static event EventHandler<BillOfMaterials, PropertyEventArgs> OnUid
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
					EventHandler<BillOfMaterials, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((BillOfMaterials)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IBillOfMaterialsOriginalData

		public IBillOfMaterialsOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IBillOfMaterials

		System.DateTime IBillOfMaterialsOriginalData.StartDate { get { return OriginalData.StartDate; } }
		System.DateTime? IBillOfMaterialsOriginalData.EndDate { get { return OriginalData.EndDate; } }
		string IBillOfMaterialsOriginalData.UnitMeasureCode { get { return OriginalData.UnitMeasureCode; } }
		string IBillOfMaterialsOriginalData.BOMLevel { get { return OriginalData.BOMLevel; } }
		int IBillOfMaterialsOriginalData.PerAssemblyQty { get { return OriginalData.PerAssemblyQty; } }
		UnitMeasure IBillOfMaterialsOriginalData.UnitMeasure { get { return ((ILookupHelper<UnitMeasure>)OriginalData.UnitMeasure).GetOriginalItem(null); } }
		Product IBillOfMaterialsOriginalData.Product { get { return ((ILookupHelper<Product>)OriginalData.Product).GetOriginalItem(null); } }

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