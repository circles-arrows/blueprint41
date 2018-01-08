 
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
	public interface ISpecialOfferOriginalData
    {
		#region Outer Data

		#region Members for interface ISpecialOffer

		string Description { get; }
		string DiscountPct { get; }
		string Type { get; }
		string Category { get; }
		System.DateTime StartDate { get; }
		System.DateTime EndDate { get; }
		int MinQty { get; }
		string MaxQty { get; }
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

	public partial class SpecialOffer : OGM<SpecialOffer, SpecialOffer.SpecialOfferData, System.String>, ISchemaBase, INeo4jBase, ISpecialOfferOriginalData
	{
        #region Initialize

        static SpecialOffer()
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

        public static Dictionary<System.String, SpecialOffer> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.SpecialOfferAlias, IWhereQuery> query)
        {
            q.SpecialOfferAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.SpecialOffer.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"SpecialOffer => Description : {this.Description}, DiscountPct : {this.DiscountPct}, Type : {this.Type}, Category : {this.Category}, StartDate : {this.StartDate}, EndDate : {this.EndDate}, MinQty : {this.MinQty}, MaxQty : {this.MaxQty}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new SpecialOfferData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Description == null)
				throw new PersistenceException(string.Format("Cannot save SpecialOffer with key '{0}' because the Description cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.DiscountPct == null)
				throw new PersistenceException(string.Format("Cannot save SpecialOffer with key '{0}' because the DiscountPct cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Type == null)
				throw new PersistenceException(string.Format("Cannot save SpecialOffer with key '{0}' because the Type cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Category == null)
				throw new PersistenceException(string.Format("Cannot save SpecialOffer with key '{0}' because the Category cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.StartDate == null)
				throw new PersistenceException(string.Format("Cannot save SpecialOffer with key '{0}' because the StartDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.EndDate == null)
				throw new PersistenceException(string.Format("Cannot save SpecialOffer with key '{0}' because the EndDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.MinQty == null)
				throw new PersistenceException(string.Format("Cannot save SpecialOffer with key '{0}' because the MinQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.MaxQty == null)
				throw new PersistenceException(string.Format("Cannot save SpecialOffer with key '{0}' because the MaxQty cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save SpecialOffer with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save SpecialOffer with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class SpecialOfferData : Data<System.String>
		{
			public SpecialOfferData()
            {

            }

            public SpecialOfferData(SpecialOfferData data)
            {
				Description = data.Description;
				DiscountPct = data.DiscountPct;
				Type = data.Type;
				Category = data.Category;
				StartDate = data.StartDate;
				EndDate = data.EndDate;
				MinQty = data.MinQty;
				MaxQty = data.MaxQty;
				rowguid = data.rowguid;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "SpecialOffer";

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
				dictionary.Add("DiscountPct",  DiscountPct);
				dictionary.Add("Type",  Type);
				dictionary.Add("Category",  Category);
				dictionary.Add("StartDate",  Conversion<System.DateTime, long>.Convert(StartDate));
				dictionary.Add("EndDate",  Conversion<System.DateTime, long>.Convert(EndDate));
				dictionary.Add("MinQty",  Conversion<int, long>.Convert(MinQty));
				dictionary.Add("MaxQty",  MaxQty);
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
				if (properties.TryGetValue("DiscountPct", out value))
					DiscountPct = (string)value;
				if (properties.TryGetValue("Type", out value))
					Type = (string)value;
				if (properties.TryGetValue("Category", out value))
					Category = (string)value;
				if (properties.TryGetValue("StartDate", out value))
					StartDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("EndDate", out value))
					EndDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("MinQty", out value))
					MinQty = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("MaxQty", out value))
					MaxQty = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ISpecialOffer

			public string Description { get; set; }
			public string DiscountPct { get; set; }
			public string Type { get; set; }
			public string Category { get; set; }
			public System.DateTime StartDate { get; set; }
			public System.DateTime EndDate { get; set; }
			public int MinQty { get; set; }
			public string MaxQty { get; set; }
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

		#region Members for interface ISpecialOffer

		public string Description { get { LazyGet(); return InnerData.Description; } set { if (LazySet(Members.Description, InnerData.Description, value)) InnerData.Description = value; } }
		public string DiscountPct { get { LazyGet(); return InnerData.DiscountPct; } set { if (LazySet(Members.DiscountPct, InnerData.DiscountPct, value)) InnerData.DiscountPct = value; } }
		public string Type { get { LazyGet(); return InnerData.Type; } set { if (LazySet(Members.Type, InnerData.Type, value)) InnerData.Type = value; } }
		public string Category { get { LazyGet(); return InnerData.Category; } set { if (LazySet(Members.Category, InnerData.Category, value)) InnerData.Category = value; } }
		public System.DateTime StartDate { get { LazyGet(); return InnerData.StartDate; } set { if (LazySet(Members.StartDate, InnerData.StartDate, value)) InnerData.StartDate = value; } }
		public System.DateTime EndDate { get { LazyGet(); return InnerData.EndDate; } set { if (LazySet(Members.EndDate, InnerData.EndDate, value)) InnerData.EndDate = value; } }
		public int MinQty { get { LazyGet(); return InnerData.MinQty; } set { if (LazySet(Members.MinQty, InnerData.MinQty, value)) InnerData.MinQty = value; } }
		public string MaxQty { get { LazyGet(); return InnerData.MaxQty; } set { if (LazySet(Members.MaxQty, InnerData.MaxQty, value)) InnerData.MaxQty = value; } }
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

        private static SpecialOfferMembers members = null;
        public static SpecialOfferMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(SpecialOffer))
                    {
                        if (members == null)
                            members = new SpecialOfferMembers();
                    }
                }
                return members;
            }
        }
        public class SpecialOfferMembers
        {
            internal SpecialOfferMembers() { }

			#region Members for interface ISpecialOffer

            public Property Description { get; } = Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Description"];
            public Property DiscountPct { get; } = Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["DiscountPct"];
            public Property Type { get; } = Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Type"];
            public Property Category { get; } = Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["Category"];
            public Property StartDate { get; } = Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["StartDate"];
            public Property EndDate { get; } = Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["EndDate"];
            public Property MinQty { get; } = Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["MinQty"];
            public Property MaxQty { get; } = Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["MaxQty"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["SpecialOffer"].Properties["rowguid"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static SpecialOfferFullTextMembers fullTextMembers = null;
        public static SpecialOfferFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(SpecialOffer))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new SpecialOfferFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class SpecialOfferFullTextMembers
        {
            internal SpecialOfferFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(SpecialOffer))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["SpecialOffer"];
                }
            }
            return entity;
        }

		private static SpecialOfferEvents events = null;
        public static SpecialOfferEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(SpecialOffer))
                    {
                        if (events == null)
                            events = new SpecialOfferEvents();
                    }
                }
                return events;
            }
        }
        public class SpecialOfferEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<SpecialOffer, EntityEventArgs> onNew;
            public event EventHandler<SpecialOffer, EntityEventArgs> OnNew
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
                EventHandler<SpecialOffer, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((SpecialOffer)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<SpecialOffer, EntityEventArgs> onDelete;
            public event EventHandler<SpecialOffer, EntityEventArgs> OnDelete
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
                EventHandler<SpecialOffer, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((SpecialOffer)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<SpecialOffer, EntityEventArgs> onSave;
            public event EventHandler<SpecialOffer, EntityEventArgs> OnSave
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
                EventHandler<SpecialOffer, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((SpecialOffer)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnDescription

				private static bool onDescriptionIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onDescription;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> OnDescription
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
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onDescription;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

				#region OnDiscountPct

				private static bool onDiscountPctIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onDiscountPct;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> OnDiscountPct
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDiscountPctIsRegistered)
							{
								Members.DiscountPct.Events.OnChange -= onDiscountPctProxy;
								Members.DiscountPct.Events.OnChange += onDiscountPctProxy;
								onDiscountPctIsRegistered = true;
							}
							onDiscountPct += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDiscountPct -= value;
							if (onDiscountPct == null && onDiscountPctIsRegistered)
							{
								Members.DiscountPct.Events.OnChange -= onDiscountPctProxy;
								onDiscountPctIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDiscountPctProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onDiscountPct;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

				#region OnType

				private static bool onTypeIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onType;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> OnType
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTypeIsRegistered)
							{
								Members.Type.Events.OnChange -= onTypeProxy;
								Members.Type.Events.OnChange += onTypeProxy;
								onTypeIsRegistered = true;
							}
							onType += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onType -= value;
							if (onType == null && onTypeIsRegistered)
							{
								Members.Type.Events.OnChange -= onTypeProxy;
								onTypeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onTypeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onType;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

				#region OnCategory

				private static bool onCategoryIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onCategory;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> OnCategory
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCategoryIsRegistered)
							{
								Members.Category.Events.OnChange -= onCategoryProxy;
								Members.Category.Events.OnChange += onCategoryProxy;
								onCategoryIsRegistered = true;
							}
							onCategory += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCategory -= value;
							if (onCategory == null && onCategoryIsRegistered)
							{
								Members.Category.Events.OnChange -= onCategoryProxy;
								onCategoryIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCategoryProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onCategory;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

				#region OnStartDate

				private static bool onStartDateIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onStartDate;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> OnStartDate
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
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onStartDate;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

				#region OnEndDate

				private static bool onEndDateIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onEndDate;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> OnEndDate
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
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onEndDate;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

				#region OnMinQty

				private static bool onMinQtyIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onMinQty;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> OnMinQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onMinQtyIsRegistered)
							{
								Members.MinQty.Events.OnChange -= onMinQtyProxy;
								Members.MinQty.Events.OnChange += onMinQtyProxy;
								onMinQtyIsRegistered = true;
							}
							onMinQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onMinQty -= value;
							if (onMinQty == null && onMinQtyIsRegistered)
							{
								Members.MinQty.Events.OnChange -= onMinQtyProxy;
								onMinQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onMinQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onMinQty;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

				#region OnMaxQty

				private static bool onMaxQtyIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onMaxQty;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> OnMaxQty
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onMaxQtyIsRegistered)
							{
								Members.MaxQty.Events.OnChange -= onMaxQtyProxy;
								Members.MaxQty.Events.OnChange += onMaxQtyProxy;
								onMaxQtyIsRegistered = true;
							}
							onMaxQty += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onMaxQty -= value;
							if (onMaxQty == null && onMaxQtyIsRegistered)
							{
								Members.MaxQty.Events.OnChange -= onMaxQtyProxy;
								onMaxQtyIsRegistered = false;
							}
						}
					}
				}
            
				private static void onMaxQtyProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onMaxQty;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onrowguid;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> Onrowguid
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
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> OnModifiedDate
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
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<SpecialOffer, PropertyEventArgs> onUid;
				public static event EventHandler<SpecialOffer, PropertyEventArgs> OnUid
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
					EventHandler<SpecialOffer, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((SpecialOffer)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region ISpecialOfferOriginalData

		public ISpecialOfferOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ISpecialOffer

		string ISpecialOfferOriginalData.Description { get { return OriginalData.Description; } }
		string ISpecialOfferOriginalData.DiscountPct { get { return OriginalData.DiscountPct; } }
		string ISpecialOfferOriginalData.Type { get { return OriginalData.Type; } }
		string ISpecialOfferOriginalData.Category { get { return OriginalData.Category; } }
		System.DateTime ISpecialOfferOriginalData.StartDate { get { return OriginalData.StartDate; } }
		System.DateTime ISpecialOfferOriginalData.EndDate { get { return OriginalData.EndDate; } }
		int ISpecialOfferOriginalData.MinQty { get { return OriginalData.MinQty; } }
		string ISpecialOfferOriginalData.MaxQty { get { return OriginalData.MaxQty; } }
		string ISpecialOfferOriginalData.rowguid { get { return OriginalData.rowguid; } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ISpecialOfferOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string ISpecialOfferOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}