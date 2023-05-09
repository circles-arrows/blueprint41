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
	public interface ICreditCardOriginalData : ISchemaBaseOriginalData
	{
		string CardType { get; }
		string CardNumber { get; }
		string ExpMonth { get; }
		string ExpYear { get; }
		IEnumerable<Person> Persons { get; }
	}

	public partial class CreditCard : OGM<CreditCard, CreditCard.CreditCardData, System.String>, ISchemaBase, INeo4jBase, ICreditCardOriginalData
	{
		#region Initialize

		static CreditCard()
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

		public static Dictionary<System.String, CreditCard> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.CreditCardAlias, IWhereQuery> query)
		{
			q.CreditCardAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.CreditCard.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"CreditCard => CardType : {this.CardType}, CardNumber : {this.CardNumber}, ExpMonth : {this.ExpMonth}, ExpYear : {this.ExpYear}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
					OriginalData = new CreditCardData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

			if (InnerData.CardType is null)
				throw new PersistenceException(string.Format("Cannot save CreditCard with key '{0}' because the CardType cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.CardNumber is null)
				throw new PersistenceException(string.Format("Cannot save CreditCard with key '{0}' because the CardNumber cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ExpMonth is null)
				throw new PersistenceException(string.Format("Cannot save CreditCard with key '{0}' because the ExpMonth cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ExpYear is null)
				throw new PersistenceException(string.Format("Cannot save CreditCard with key '{0}' because the ExpYear cannot be null.", this.Uid?.ToString() ?? "<null>"));
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class CreditCardData : Data<System.String>
		{
			public CreditCardData()
			{

			}

			public CreditCardData(CreditCardData data)
			{
				CardType = data.CardType;
				CardNumber = data.CardNumber;
				ExpMonth = data.ExpMonth;
				ExpYear = data.ExpYear;
				Persons = data.Persons;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "CreditCard";

				Persons = new EntityCollection<Person>(Wrapper, Members.Persons, item => { if (Members.Persons.Events.HasRegisteredChangeHandlers) { object loadHack = item.CreditCard; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("CardType",  CardType);
				dictionary.Add("CardNumber",  CardNumber);
				dictionary.Add("ExpMonth",  ExpMonth);
				dictionary.Add("ExpYear",  ExpYear);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("CardType", out value))
					CardType = (string)value;
				if (properties.TryGetValue("CardNumber", out value))
					CardNumber = (string)value;
				if (properties.TryGetValue("ExpMonth", out value))
					ExpMonth = (string)value;
				if (properties.TryGetValue("ExpYear", out value))
					ExpYear = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface ICreditCard

			public string CardType { get; set; }
			public string CardNumber { get; set; }
			public string ExpMonth { get; set; }
			public string ExpYear { get; set; }
			public EntityCollection<Person> Persons { get; private set; }

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

		#region Members for interface ICreditCard

		public string CardType { get { LazyGet(); return InnerData.CardType; } set { if (LazySet(Members.CardType, InnerData.CardType, value)) InnerData.CardType = value; } }
		public string CardNumber { get { LazyGet(); return InnerData.CardNumber; } set { if (LazySet(Members.CardNumber, InnerData.CardNumber, value)) InnerData.CardNumber = value; } }
		public string ExpMonth { get { LazyGet(); return InnerData.ExpMonth; } set { if (LazySet(Members.ExpMonth, InnerData.ExpMonth, value)) InnerData.ExpMonth = value; } }
		public string ExpYear { get { LazyGet(); return InnerData.ExpYear; } set { if (LazySet(Members.ExpYear, InnerData.ExpYear, value)) InnerData.ExpYear = value; } }
		public EntityCollection<Person> Persons { get { return InnerData.Persons; } }
		private void ClearPersons(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.Persons).ClearLookup(moment);
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

		private static CreditCardMembers members = null;
		public static CreditCardMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(CreditCard))
					{
						if (members is null)
							members = new CreditCardMembers();
					}
				}
				return members;
			}
		}
		public class CreditCardMembers
		{
			internal CreditCardMembers() { }

			#region Members for interface ICreditCard

			public Property CardType { get; } = Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["CardType"];
			public Property CardNumber { get; } = Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["CardNumber"];
			public Property ExpMonth { get; } = Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["ExpMonth"];
			public Property ExpYear { get; } = Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["ExpYear"];
			public Property Persons { get; } = Datastore.AdventureWorks.Model.Entities["CreditCard"].Properties["Persons"];
			#endregion

			#region Members for interface ISchemaBase

			public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static CreditCardFullTextMembers fullTextMembers = null;
		public static CreditCardFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(CreditCard))
					{
						if (fullTextMembers is null)
							fullTextMembers = new CreditCardFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class CreditCardFullTextMembers
		{
			internal CreditCardFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(CreditCard))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["CreditCard"];
				}
			}
			return entity;
		}

		private static CreditCardEvents events = null;
		public static CreditCardEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(CreditCard))
					{
						if (events is null)
							events = new CreditCardEvents();
					}
				}
				return events;
			}
		}
		public class CreditCardEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<CreditCard, EntityEventArgs> onNew;
			public event EventHandler<CreditCard, EntityEventArgs> OnNew
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
				EventHandler<CreditCard, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((CreditCard)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<CreditCard, EntityEventArgs> onDelete;
			public event EventHandler<CreditCard, EntityEventArgs> OnDelete
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
				EventHandler<CreditCard, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((CreditCard)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<CreditCard, EntityEventArgs> onSave;
			public event EventHandler<CreditCard, EntityEventArgs> OnSave
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
				EventHandler<CreditCard, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((CreditCard)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<CreditCard, EntityEventArgs> onAfterSave;
			public event EventHandler<CreditCard, EntityEventArgs> OnAfterSave
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
				EventHandler<CreditCard, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((CreditCard)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnCardType

				private static bool onCardTypeIsRegistered = false;

				private static EventHandler<CreditCard, PropertyEventArgs> onCardType;
				public static event EventHandler<CreditCard, PropertyEventArgs> OnCardType
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCardTypeIsRegistered)
							{
								Members.CardType.Events.OnChange -= onCardTypeProxy;
								Members.CardType.Events.OnChange += onCardTypeProxy;
								onCardTypeIsRegistered = true;
							}
							onCardType += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCardType -= value;
							if (onCardType is null && onCardTypeIsRegistered)
							{
								Members.CardType.Events.OnChange -= onCardTypeProxy;
								onCardTypeIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCardTypeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CreditCard, PropertyEventArgs> handler = onCardType;
					if (handler is not null)
						handler.Invoke((CreditCard)sender, args);
				}

				#endregion

				#region OnCardNumber

				private static bool onCardNumberIsRegistered = false;

				private static EventHandler<CreditCard, PropertyEventArgs> onCardNumber;
				public static event EventHandler<CreditCard, PropertyEventArgs> OnCardNumber
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCardNumberIsRegistered)
							{
								Members.CardNumber.Events.OnChange -= onCardNumberProxy;
								Members.CardNumber.Events.OnChange += onCardNumberProxy;
								onCardNumberIsRegistered = true;
							}
							onCardNumber += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCardNumber -= value;
							if (onCardNumber is null && onCardNumberIsRegistered)
							{
								Members.CardNumber.Events.OnChange -= onCardNumberProxy;
								onCardNumberIsRegistered = false;
							}
						}
					}
				}
			
				private static void onCardNumberProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CreditCard, PropertyEventArgs> handler = onCardNumber;
					if (handler is not null)
						handler.Invoke((CreditCard)sender, args);
				}

				#endregion

				#region OnExpMonth

				private static bool onExpMonthIsRegistered = false;

				private static EventHandler<CreditCard, PropertyEventArgs> onExpMonth;
				public static event EventHandler<CreditCard, PropertyEventArgs> OnExpMonth
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onExpMonthIsRegistered)
							{
								Members.ExpMonth.Events.OnChange -= onExpMonthProxy;
								Members.ExpMonth.Events.OnChange += onExpMonthProxy;
								onExpMonthIsRegistered = true;
							}
							onExpMonth += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onExpMonth -= value;
							if (onExpMonth is null && onExpMonthIsRegistered)
							{
								Members.ExpMonth.Events.OnChange -= onExpMonthProxy;
								onExpMonthIsRegistered = false;
							}
						}
					}
				}
			
				private static void onExpMonthProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CreditCard, PropertyEventArgs> handler = onExpMonth;
					if (handler is not null)
						handler.Invoke((CreditCard)sender, args);
				}

				#endregion

				#region OnExpYear

				private static bool onExpYearIsRegistered = false;

				private static EventHandler<CreditCard, PropertyEventArgs> onExpYear;
				public static event EventHandler<CreditCard, PropertyEventArgs> OnExpYear
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onExpYearIsRegistered)
							{
								Members.ExpYear.Events.OnChange -= onExpYearProxy;
								Members.ExpYear.Events.OnChange += onExpYearProxy;
								onExpYearIsRegistered = true;
							}
							onExpYear += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onExpYear -= value;
							if (onExpYear is null && onExpYearIsRegistered)
							{
								Members.ExpYear.Events.OnChange -= onExpYearProxy;
								onExpYearIsRegistered = false;
							}
						}
					}
				}
			
				private static void onExpYearProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CreditCard, PropertyEventArgs> handler = onExpYear;
					if (handler is not null)
						handler.Invoke((CreditCard)sender, args);
				}

				#endregion

				#region OnPersons

				private static bool onPersonsIsRegistered = false;

				private static EventHandler<CreditCard, PropertyEventArgs> onPersons;
				public static event EventHandler<CreditCard, PropertyEventArgs> OnPersons
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPersonsIsRegistered)
							{
								Members.Persons.Events.OnChange -= onPersonsProxy;
								Members.Persons.Events.OnChange += onPersonsProxy;
								onPersonsIsRegistered = true;
							}
							onPersons += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPersons -= value;
							if (onPersons is null && onPersonsIsRegistered)
							{
								Members.Persons.Events.OnChange -= onPersonsProxy;
								onPersonsIsRegistered = false;
							}
						}
					}
				}
			
				private static void onPersonsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<CreditCard, PropertyEventArgs> handler = onPersons;
					if (handler is not null)
						handler.Invoke((CreditCard)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<CreditCard, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<CreditCard, PropertyEventArgs> OnModifiedDate
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
					EventHandler<CreditCard, PropertyEventArgs> handler = onModifiedDate;
					if (handler is not null)
						handler.Invoke((CreditCard)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<CreditCard, PropertyEventArgs> onUid;
				public static event EventHandler<CreditCard, PropertyEventArgs> OnUid
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
					EventHandler<CreditCard, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((CreditCard)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region ICreditCardOriginalData

		public ICreditCardOriginalData OriginalVersion { get { return this; } }

		#region Members for interface ICreditCard

		string ICreditCardOriginalData.CardType { get { return OriginalData.CardType; } }
		string ICreditCardOriginalData.CardNumber { get { return OriginalData.CardNumber; } }
		string ICreditCardOriginalData.ExpMonth { get { return OriginalData.ExpMonth; } }
		string ICreditCardOriginalData.ExpYear { get { return OriginalData.ExpYear; } }
		IEnumerable<Person> ICreditCardOriginalData.Persons { get { return OriginalData.Persons.OriginalData; } }

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