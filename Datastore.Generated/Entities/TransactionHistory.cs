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
	public interface ITransactionHistoryOriginalData : ISchemaBaseOriginalData
	{
		int ReferenceOrderID { get; }
		System.DateTime TransactionDate { get; }
		string TransactionType { get; }
		int Quantity { get; }
		string ActualCost { get; }
		int ReferenceOrderLineID { get; }
	}

	public partial class TransactionHistory : OGM<TransactionHistory, TransactionHistory.TransactionHistoryData, System.String>, ISchemaBase, INeo4jBase, ITransactionHistoryOriginalData
	{
		#region Initialize

		static TransactionHistory()
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

		public static Dictionary<System.String, TransactionHistory> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.TransactionHistoryAlias, IWhereQuery> query)
		{
			q.TransactionHistoryAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.TransactionHistory.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"TransactionHistory => ReferenceOrderID : {this.ReferenceOrderID}, TransactionDate : {this.TransactionDate}, TransactionType : {this.TransactionType}, Quantity : {this.Quantity}, ActualCost : {this.ActualCost}, ReferenceOrderLineID : {this.ReferenceOrderLineID}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new TransactionHistoryData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.TransactionType is null)
				throw new PersistenceException(string.Format("Cannot save TransactionHistory with key '{0}' because the TransactionType cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ActualCost is null)
				throw new PersistenceException(string.Format("Cannot save TransactionHistory with key '{0}' because the ActualCost cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class TransactionHistoryData : Data<System.String>
		{
			public TransactionHistoryData()
			{

			}

			public TransactionHistoryData(TransactionHistoryData data)
			{
				ReferenceOrderID = data.ReferenceOrderID;
				TransactionDate = data.TransactionDate;
				TransactionType = data.TransactionType;
				Quantity = data.Quantity;
				ActualCost = data.ActualCost;
				ReferenceOrderLineID = data.ReferenceOrderLineID;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "TransactionHistory";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("ReferenceOrderID",  Conversion<int, long>.Convert(ReferenceOrderID));
				dictionary.Add("TransactionDate",  Conversion<System.DateTime, long>.Convert(TransactionDate));
				dictionary.Add("TransactionType",  TransactionType);
				dictionary.Add("Quantity",  Conversion<int, long>.Convert(Quantity));
				dictionary.Add("ActualCost",  ActualCost);
				dictionary.Add("ReferenceOrderLineID",  Conversion<int, long>.Convert(ReferenceOrderLineID));
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("ReferenceOrderID", out value))
					ReferenceOrderID = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("TransactionDate", out value))
					TransactionDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("TransactionType", out value))
					TransactionType = (string)value;
				if (properties.TryGetValue("Quantity", out value))
					Quantity = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("ActualCost", out value))
					ActualCost = (string)value;
				if (properties.TryGetValue("ReferenceOrderLineID", out value))
					ReferenceOrderLineID = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ITransactionHistory

			public int ReferenceOrderID { get; set; }
			public System.DateTime TransactionDate { get; set; }
			public string TransactionType { get; set; }
			public int Quantity { get; set; }
			public string ActualCost { get; set; }
			public int ReferenceOrderLineID { get; set; }

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

		#region Members for interface ITransactionHistory

		public int ReferenceOrderID { get { LazyGet(); return InnerData.ReferenceOrderID; } set { if (LazySet(Members.ReferenceOrderID, InnerData.ReferenceOrderID, value)) InnerData.ReferenceOrderID = value; } }
		public System.DateTime TransactionDate { get { LazyGet(); return InnerData.TransactionDate; } set { if (LazySet(Members.TransactionDate, InnerData.TransactionDate, value)) InnerData.TransactionDate = value; } }
		public string TransactionType { get { LazyGet(); return InnerData.TransactionType; } set { if (LazySet(Members.TransactionType, InnerData.TransactionType, value)) InnerData.TransactionType = value; } }
		public int Quantity { get { LazyGet(); return InnerData.Quantity; } set { if (LazySet(Members.Quantity, InnerData.Quantity, value)) InnerData.Quantity = value; } }
		public string ActualCost { get { LazyGet(); return InnerData.ActualCost; } set { if (LazySet(Members.ActualCost, InnerData.ActualCost, value)) InnerData.ActualCost = value; } }
		public int ReferenceOrderLineID { get { LazyGet(); return InnerData.ReferenceOrderLineID; } set { if (LazySet(Members.ReferenceOrderLineID, InnerData.ReferenceOrderLineID, value)) InnerData.ReferenceOrderLineID = value; } }

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

		private static TransactionHistoryMembers members = null;
		public static TransactionHistoryMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(TransactionHistory))
					{
						if (members is null)
							members = new TransactionHistoryMembers();
					}
				}
				return members;
			}
		}
		public class TransactionHistoryMembers
		{
			internal TransactionHistoryMembers() { }

			#region Members for interface ITransactionHistory

			public Property ReferenceOrderID { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["ReferenceOrderID"];
			public Property TransactionDate { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["TransactionDate"];
			public Property TransactionType { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["TransactionType"];
			public Property Quantity { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["Quantity"];
			public Property ActualCost { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["ActualCost"];
			public Property ReferenceOrderLineID { get; } = Datastore.AdventureWorks.Model.Entities["TransactionHistory"].Properties["ReferenceOrderLineID"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static TransactionHistoryFullTextMembers fullTextMembers = null;
		public static TransactionHistoryFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(TransactionHistory))
					{
						if (fullTextMembers is null)
							fullTextMembers = new TransactionHistoryFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class TransactionHistoryFullTextMembers
		{
			internal TransactionHistoryFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(TransactionHistory))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["TransactionHistory"];
				}
			}
			return entity;
		}

		private static TransactionHistoryEvents events = null;
		public static TransactionHistoryEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(TransactionHistory))
					{
						if (events is null)
							events = new TransactionHistoryEvents();
					}
				}
				return events;
			}
		}
		public class TransactionHistoryEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<TransactionHistory, EntityEventArgs> onNew;
			public event EventHandler<TransactionHistory, EntityEventArgs> OnNew
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
				EventHandler<TransactionHistory, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((TransactionHistory)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<TransactionHistory, EntityEventArgs> onDelete;
			public event EventHandler<TransactionHistory, EntityEventArgs> OnDelete
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
				EventHandler<TransactionHistory, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((TransactionHistory)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<TransactionHistory, EntityEventArgs> onSave;
			public event EventHandler<TransactionHistory, EntityEventArgs> OnSave
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
				EventHandler<TransactionHistory, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((TransactionHistory)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<TransactionHistory, EntityEventArgs> onAfterSave;
			public event EventHandler<TransactionHistory, EntityEventArgs> OnAfterSave
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
				EventHandler<TransactionHistory, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((TransactionHistory)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnReferenceOrderID

				private static bool onReferenceOrderIDIsRegistered = false;

				private static EventHandler<TransactionHistory, PropertyEventArgs> onReferenceOrderID;
				public static event EventHandler<TransactionHistory, PropertyEventArgs> OnReferenceOrderID
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onReferenceOrderIDIsRegistered)
							{
								Members.ReferenceOrderID.Events.OnChange -= onReferenceOrderIDProxy;
								Members.ReferenceOrderID.Events.OnChange += onReferenceOrderIDProxy;
								onReferenceOrderIDIsRegistered = true;
							}
							onReferenceOrderID += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onReferenceOrderID -= value;
							if (onReferenceOrderID is null && onReferenceOrderIDIsRegistered)
							{
								Members.ReferenceOrderID.Events.OnChange -= onReferenceOrderIDProxy;
								onReferenceOrderIDIsRegistered = false;
							}
						}
					}
				}
			
				private static void onReferenceOrderIDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistory, PropertyEventArgs> handler = onReferenceOrderID;
					if (handler is not null)
						handler.Invoke((TransactionHistory)sender, args);
				}

				#endregion

				#region OnTransactionDate

				private static bool onTransactionDateIsRegistered = false;

				private static EventHandler<TransactionHistory, PropertyEventArgs> onTransactionDate;
				public static event EventHandler<TransactionHistory, PropertyEventArgs> OnTransactionDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTransactionDateIsRegistered)
							{
								Members.TransactionDate.Events.OnChange -= onTransactionDateProxy;
								Members.TransactionDate.Events.OnChange += onTransactionDateProxy;
								onTransactionDateIsRegistered = true;
							}
							onTransactionDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTransactionDate -= value;
							if (onTransactionDate is null && onTransactionDateIsRegistered)
							{
								Members.TransactionDate.Events.OnChange -= onTransactionDateProxy;
								onTransactionDateIsRegistered = false;
							}
						}
					}
				}
			
				private static void onTransactionDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistory, PropertyEventArgs> handler = onTransactionDate;
					if (handler is not null)
						handler.Invoke((TransactionHistory)sender, args);
				}

				#endregion

				#region OnTransactionType

				private static bool onTransactionTypeIsRegistered = false;

				private static EventHandler<TransactionHistory, PropertyEventArgs> onTransactionType;
				public static event EventHandler<TransactionHistory, PropertyEventArgs> OnTransactionType
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTransactionTypeIsRegistered)
							{
								Members.TransactionType.Events.OnChange -= onTransactionTypeProxy;
								Members.TransactionType.Events.OnChange += onTransactionTypeProxy;
								onTransactionTypeIsRegistered = true;
							}
							onTransactionType += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTransactionType -= value;
							if (onTransactionType is null && onTransactionTypeIsRegistered)
							{
								Members.TransactionType.Events.OnChange -= onTransactionTypeProxy;
								onTransactionTypeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onTransactionTypeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistory, PropertyEventArgs> handler = onTransactionType;
					if (handler is not null)
						handler.Invoke((TransactionHistory)sender, args);
				}

				#endregion

				#region OnQuantity

				private static bool onQuantityIsRegistered = false;

				private static EventHandler<TransactionHistory, PropertyEventArgs> onQuantity;
				public static event EventHandler<TransactionHistory, PropertyEventArgs> OnQuantity
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onQuantityIsRegistered)
							{
								Members.Quantity.Events.OnChange -= onQuantityProxy;
								Members.Quantity.Events.OnChange += onQuantityProxy;
								onQuantityIsRegistered = true;
							}
							onQuantity += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onQuantity -= value;
							if (onQuantity is null && onQuantityIsRegistered)
							{
								Members.Quantity.Events.OnChange -= onQuantityProxy;
								onQuantityIsRegistered = false;
							}
						}
					}
				}
			
				private static void onQuantityProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistory, PropertyEventArgs> handler = onQuantity;
					if (handler is not null)
						handler.Invoke((TransactionHistory)sender, args);
				}

				#endregion

				#region OnActualCost

				private static bool onActualCostIsRegistered = false;

				private static EventHandler<TransactionHistory, PropertyEventArgs> onActualCost;
				public static event EventHandler<TransactionHistory, PropertyEventArgs> OnActualCost
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onActualCostIsRegistered)
							{
								Members.ActualCost.Events.OnChange -= onActualCostProxy;
								Members.ActualCost.Events.OnChange += onActualCostProxy;
								onActualCostIsRegistered = true;
							}
							onActualCost += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onActualCost -= value;
							if (onActualCost is null && onActualCostIsRegistered)
							{
								Members.ActualCost.Events.OnChange -= onActualCostProxy;
								onActualCostIsRegistered = false;
							}
						}
					}
				}
			
				private static void onActualCostProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistory, PropertyEventArgs> handler = onActualCost;
					if (handler is not null)
						handler.Invoke((TransactionHistory)sender, args);
				}

				#endregion

				#region OnReferenceOrderLineID

				private static bool onReferenceOrderLineIDIsRegistered = false;

				private static EventHandler<TransactionHistory, PropertyEventArgs> onReferenceOrderLineID;
				public static event EventHandler<TransactionHistory, PropertyEventArgs> OnReferenceOrderLineID
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onReferenceOrderLineIDIsRegistered)
							{
								Members.ReferenceOrderLineID.Events.OnChange -= onReferenceOrderLineIDProxy;
								Members.ReferenceOrderLineID.Events.OnChange += onReferenceOrderLineIDProxy;
								onReferenceOrderLineIDIsRegistered = true;
							}
							onReferenceOrderLineID += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onReferenceOrderLineID -= value;
							if (onReferenceOrderLineID is null && onReferenceOrderLineIDIsRegistered)
							{
								Members.ReferenceOrderLineID.Events.OnChange -= onReferenceOrderLineIDProxy;
								onReferenceOrderLineIDIsRegistered = false;
							}
						}
					}
				}
			
				private static void onReferenceOrderLineIDProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<TransactionHistory, PropertyEventArgs> handler = onReferenceOrderLineID;
					if (handler is not null)
						handler.Invoke((TransactionHistory)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<TransactionHistory, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<TransactionHistory, PropertyEventArgs> OnModifiedDate
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
					EventHandler<TransactionHistory, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((TransactionHistory)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<TransactionHistory, PropertyEventArgs> onUid;
				public static event EventHandler<TransactionHistory, PropertyEventArgs> OnUid
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
					EventHandler<TransactionHistory, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((TransactionHistory)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region ITransactionHistoryOriginalData

		public ITransactionHistoryOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ITransactionHistory

		int ITransactionHistoryOriginalData.ReferenceOrderID { get { return OriginalData.ReferenceOrderID; } }
		System.DateTime ITransactionHistoryOriginalData.TransactionDate { get { return OriginalData.TransactionDate; } }
		string ITransactionHistoryOriginalData.TransactionType { get { return OriginalData.TransactionType; } }
		int ITransactionHistoryOriginalData.Quantity { get { return OriginalData.Quantity; } }
		string ITransactionHistoryOriginalData.ActualCost { get { return OriginalData.ActualCost; } }
		int ITransactionHistoryOriginalData.ReferenceOrderLineID { get { return OriginalData.ReferenceOrderLineID; } }

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