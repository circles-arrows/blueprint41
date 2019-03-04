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
	public interface IPersonOriginalData : ISchemaBaseOriginalData
    {
		int? PersonType { get; }
		string NameStyle { get; }
		string Title { get; }
		string FirstName { get; }
		string MiddleName { get; }
		string LastName { get; }
		string Suffix { get; }
		string EmailPromotion { get; }
		string AdditionalContactInfo { get; }
		string Demographics { get; }
		string rowguid { get; }
		Password Password { get; }
		EmailAddress EmailAddress { get; }
		Document Document { get; }
		Employee Employee { get; }
		CreditCard CreditCard { get; }
		ContactType ContactType { get; }
		PhoneNumberType PhoneNumberType { get; }
		Address Address { get; }
		SalesPerson SalesPerson { get; }
    }

	public partial class Person : OGM<Person, Person.PersonData, System.String>, ISchemaBase, INeo4jBase, IPersonOriginalData
	{
        #region Initialize

        static Person()
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

        public static Dictionary<System.String, Person> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.PersonAlias, IWhereQuery> query)
        {
            q.PersonAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Person.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Person => PersonType : {this.PersonType?.ToString() ?? "null"}, NameStyle : {this.NameStyle}, Title : {this.Title?.ToString() ?? "null"}, FirstName : {this.FirstName}, MiddleName : {this.MiddleName?.ToString() ?? "null"}, LastName : {this.LastName}, Suffix : {this.Suffix?.ToString() ?? "null"}, EmailPromotion : {this.EmailPromotion}, AdditionalContactInfo : {this.AdditionalContactInfo?.ToString() ?? "null"}, Demographics : {this.Demographics?.ToString() ?? "null"}, rowguid : {this.rowguid}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new PersonData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.NameStyle == null)
				throw new PersistenceException(string.Format("Cannot save Person with key '{0}' because the NameStyle cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.FirstName == null)
				throw new PersistenceException(string.Format("Cannot save Person with key '{0}' because the FirstName cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.LastName == null)
				throw new PersistenceException(string.Format("Cannot save Person with key '{0}' because the LastName cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.EmailPromotion == null)
				throw new PersistenceException(string.Format("Cannot save Person with key '{0}' because the EmailPromotion cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.rowguid == null)
				throw new PersistenceException(string.Format("Cannot save Person with key '{0}' because the rowguid cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save Person with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class PersonData : Data<System.String>
		{
			public PersonData()
            {

            }

            public PersonData(PersonData data)
            {
				PersonType = data.PersonType;
				NameStyle = data.NameStyle;
				Title = data.Title;
				FirstName = data.FirstName;
				MiddleName = data.MiddleName;
				LastName = data.LastName;
				Suffix = data.Suffix;
				EmailPromotion = data.EmailPromotion;
				AdditionalContactInfo = data.AdditionalContactInfo;
				Demographics = data.Demographics;
				rowguid = data.rowguid;
				Password = data.Password;
				EmailAddress = data.EmailAddress;
				Document = data.Document;
				Employee = data.Employee;
				CreditCard = data.CreditCard;
				ContactType = data.ContactType;
				PhoneNumberType = data.PhoneNumberType;
				Address = data.Address;
				SalesPerson = data.SalesPerson;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Person";

				Password = new EntityCollection<Password>(Wrapper, Members.Password);
				EmailAddress = new EntityCollection<EmailAddress>(Wrapper, Members.EmailAddress, item => { if (Members.EmailAddress.Events.HasRegisteredChangeHandlers) { int loadHack = item.EmailAddresses.Count; } });
				Document = new EntityCollection<Document>(Wrapper, Members.Document, item => { if (Members.Document.Events.HasRegisteredChangeHandlers) { int loadHack = item.Persons.Count; } });
				Employee = new EntityCollection<Employee>(Wrapper, Members.Employee);
				CreditCard = new EntityCollection<CreditCard>(Wrapper, Members.CreditCard, item => { if (Members.CreditCard.Events.HasRegisteredChangeHandlers) { int loadHack = item.Persons.Count; } });
				ContactType = new EntityCollection<ContactType>(Wrapper, Members.ContactType);
				PhoneNumberType = new EntityCollection<PhoneNumberType>(Wrapper, Members.PhoneNumberType);
				Address = new EntityCollection<Address>(Wrapper, Members.Address);
				SalesPerson = new EntityCollection<SalesPerson>(Wrapper, Members.SalesPerson, item => { if (Members.SalesPerson.Events.HasRegisteredChangeHandlers) { object loadHack = item.Person; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("PersonType",  Conversion<int?, long?>.Convert(PersonType));
				dictionary.Add("NameStyle",  NameStyle);
				dictionary.Add("Title",  Title);
				dictionary.Add("FirstName",  FirstName);
				dictionary.Add("MiddleName",  MiddleName);
				dictionary.Add("LastName",  LastName);
				dictionary.Add("Suffix",  Suffix);
				dictionary.Add("EmailPromotion",  EmailPromotion);
				dictionary.Add("AdditionalContactInfo",  AdditionalContactInfo);
				dictionary.Add("Demographics",  Demographics);
				dictionary.Add("rowguid",  rowguid);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("PersonType", out value))
					PersonType = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("NameStyle", out value))
					NameStyle = (string)value;
				if (properties.TryGetValue("Title", out value))
					Title = (string)value;
				if (properties.TryGetValue("FirstName", out value))
					FirstName = (string)value;
				if (properties.TryGetValue("MiddleName", out value))
					MiddleName = (string)value;
				if (properties.TryGetValue("LastName", out value))
					LastName = (string)value;
				if (properties.TryGetValue("Suffix", out value))
					Suffix = (string)value;
				if (properties.TryGetValue("EmailPromotion", out value))
					EmailPromotion = (string)value;
				if (properties.TryGetValue("AdditionalContactInfo", out value))
					AdditionalContactInfo = (string)value;
				if (properties.TryGetValue("Demographics", out value))
					Demographics = (string)value;
				if (properties.TryGetValue("rowguid", out value))
					rowguid = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IPerson

			public int? PersonType { get; set; }
			public string NameStyle { get; set; }
			public string Title { get; set; }
			public string FirstName { get; set; }
			public string MiddleName { get; set; }
			public string LastName { get; set; }
			public string Suffix { get; set; }
			public string EmailPromotion { get; set; }
			public string AdditionalContactInfo { get; set; }
			public string Demographics { get; set; }
			public string rowguid { get; set; }
			public EntityCollection<Password> Password { get; private set; }
			public EntityCollection<EmailAddress> EmailAddress { get; private set; }
			public EntityCollection<Document> Document { get; private set; }
			public EntityCollection<Employee> Employee { get; private set; }
			public EntityCollection<CreditCard> CreditCard { get; private set; }
			public EntityCollection<ContactType> ContactType { get; private set; }
			public EntityCollection<PhoneNumberType> PhoneNumberType { get; private set; }
			public EntityCollection<Address> Address { get; private set; }
			public EntityCollection<SalesPerson> SalesPerson { get; private set; }

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

		#region Members for interface IPerson

		public int? PersonType { get { LazyGet(); return InnerData.PersonType; } set { if (LazySet(Members.PersonType, InnerData.PersonType, value)) InnerData.PersonType = value; } }
		public string NameStyle { get { LazyGet(); return InnerData.NameStyle; } set { if (LazySet(Members.NameStyle, InnerData.NameStyle, value)) InnerData.NameStyle = value; } }
		public string Title { get { LazyGet(); return InnerData.Title; } set { if (LazySet(Members.Title, InnerData.Title, value)) InnerData.Title = value; } }
		public string FirstName { get { LazyGet(); return InnerData.FirstName; } set { if (LazySet(Members.FirstName, InnerData.FirstName, value)) InnerData.FirstName = value; } }
		public string MiddleName { get { LazyGet(); return InnerData.MiddleName; } set { if (LazySet(Members.MiddleName, InnerData.MiddleName, value)) InnerData.MiddleName = value; } }
		public string LastName { get { LazyGet(); return InnerData.LastName; } set { if (LazySet(Members.LastName, InnerData.LastName, value)) InnerData.LastName = value; } }
		public string Suffix { get { LazyGet(); return InnerData.Suffix; } set { if (LazySet(Members.Suffix, InnerData.Suffix, value)) InnerData.Suffix = value; } }
		public string EmailPromotion { get { LazyGet(); return InnerData.EmailPromotion; } set { if (LazySet(Members.EmailPromotion, InnerData.EmailPromotion, value)) InnerData.EmailPromotion = value; } }
		public string AdditionalContactInfo { get { LazyGet(); return InnerData.AdditionalContactInfo; } set { if (LazySet(Members.AdditionalContactInfo, InnerData.AdditionalContactInfo, value)) InnerData.AdditionalContactInfo = value; } }
		public string Demographics { get { LazyGet(); return InnerData.Demographics; } set { if (LazySet(Members.Demographics, InnerData.Demographics, value)) InnerData.Demographics = value; } }
		public string rowguid { get { LazyGet(); return InnerData.rowguid; } set { if (LazySet(Members.rowguid, InnerData.rowguid, value)) InnerData.rowguid = value; } }
		public Password Password
		{
			get { return ((ILookupHelper<Password>)InnerData.Password).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Password, ((ILookupHelper<Password>)InnerData.Password).GetItem(null), value))
					((ILookupHelper<Password>)InnerData.Password).SetItem(value, null); 
			}
		}
		public EmailAddress EmailAddress
		{
			get { return ((ILookupHelper<EmailAddress>)InnerData.EmailAddress).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.EmailAddress, ((ILookupHelper<EmailAddress>)InnerData.EmailAddress).GetItem(null), value))
					((ILookupHelper<EmailAddress>)InnerData.EmailAddress).SetItem(value, null); 
			}
		}
		private void ClearEmailAddress(DateTime? moment)
		{
			((ILookupHelper<EmailAddress>)InnerData.EmailAddress).ClearLookup(moment);
		}
		public Document Document
		{
			get { return ((ILookupHelper<Document>)InnerData.Document).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Document, ((ILookupHelper<Document>)InnerData.Document).GetItem(null), value))
					((ILookupHelper<Document>)InnerData.Document).SetItem(value, null); 
			}
		}
		private void ClearDocument(DateTime? moment)
		{
			((ILookupHelper<Document>)InnerData.Document).ClearLookup(moment);
		}
		public Employee Employee
		{
			get { return ((ILookupHelper<Employee>)InnerData.Employee).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Employee, ((ILookupHelper<Employee>)InnerData.Employee).GetItem(null), value))
					((ILookupHelper<Employee>)InnerData.Employee).SetItem(value, null); 
			}
		}
		public CreditCard CreditCard
		{
			get { return ((ILookupHelper<CreditCard>)InnerData.CreditCard).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.CreditCard, ((ILookupHelper<CreditCard>)InnerData.CreditCard).GetItem(null), value))
					((ILookupHelper<CreditCard>)InnerData.CreditCard).SetItem(value, null); 
			}
		}
		private void ClearCreditCard(DateTime? moment)
		{
			((ILookupHelper<CreditCard>)InnerData.CreditCard).ClearLookup(moment);
		}
		public ContactType ContactType
		{
			get { return ((ILookupHelper<ContactType>)InnerData.ContactType).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.ContactType, ((ILookupHelper<ContactType>)InnerData.ContactType).GetItem(null), value))
					((ILookupHelper<ContactType>)InnerData.ContactType).SetItem(value, null); 
			}
		}
		public PhoneNumberType PhoneNumberType
		{
			get { return ((ILookupHelper<PhoneNumberType>)InnerData.PhoneNumberType).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.PhoneNumberType, ((ILookupHelper<PhoneNumberType>)InnerData.PhoneNumberType).GetItem(null), value))
					((ILookupHelper<PhoneNumberType>)InnerData.PhoneNumberType).SetItem(value, null); 
			}
		}
		public Address Address
		{
			get { return ((ILookupHelper<Address>)InnerData.Address).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Address, ((ILookupHelper<Address>)InnerData.Address).GetItem(null), value))
					((ILookupHelper<Address>)InnerData.Address).SetItem(value, null); 
			}
		}
		public SalesPerson SalesPerson
		{
			get { return ((ILookupHelper<SalesPerson>)InnerData.SalesPerson).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.SalesPerson, ((ILookupHelper<SalesPerson>)InnerData.SalesPerson).GetItem(null), value))
					((ILookupHelper<SalesPerson>)InnerData.SalesPerson).SetItem(value, null); 
			}
		}
		private void ClearSalesPerson(DateTime? moment)
		{
			((ILookupHelper<SalesPerson>)InnerData.SalesPerson).ClearLookup(moment);
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

        private static PersonMembers members = null;
        public static PersonMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Person))
                    {
                        if (members == null)
                            members = new PersonMembers();
                    }
                }
                return members;
            }
        }
        public class PersonMembers
        {
            internal PersonMembers() { }

			#region Members for interface IPerson

            public Property PersonType { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["PersonType"];
            public Property NameStyle { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["NameStyle"];
            public Property Title { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["Title"];
            public Property FirstName { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["FirstName"];
            public Property MiddleName { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["MiddleName"];
            public Property LastName { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["LastName"];
            public Property Suffix { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["Suffix"];
            public Property EmailPromotion { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["EmailPromotion"];
            public Property AdditionalContactInfo { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["AdditionalContactInfo"];
            public Property Demographics { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["Demographics"];
            public Property rowguid { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["rowguid"];
            public Property Password { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["Password"];
            public Property EmailAddress { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["EmailAddress"];
            public Property Document { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["Document"];
            public Property Employee { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["Employee"];
            public Property CreditCard { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["CreditCard"];
            public Property ContactType { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["ContactType"];
            public Property PhoneNumberType { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["PhoneNumberType"];
            public Property Address { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["Address"];
            public Property SalesPerson { get; } = Datastore.AdventureWorks.Model.Entities["Person"].Properties["SalesPerson"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static PersonFullTextMembers fullTextMembers = null;
        public static PersonFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Person))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new PersonFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class PersonFullTextMembers
        {
            internal PersonFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Person))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["Person"];
                }
            }
            return entity;
        }

		private static PersonEvents events = null;
        public static PersonEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Person))
                    {
                        if (events == null)
                            events = new PersonEvents();
                    }
                }
                return events;
            }
        }
        public class PersonEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Person, EntityEventArgs> onNew;
            public event EventHandler<Person, EntityEventArgs> OnNew
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
                EventHandler<Person, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Person)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Person, EntityEventArgs> onDelete;
            public event EventHandler<Person, EntityEventArgs> OnDelete
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
                EventHandler<Person, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Person)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Person, EntityEventArgs> onSave;
            public event EventHandler<Person, EntityEventArgs> OnSave
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
                EventHandler<Person, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Person)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnPersonType

				private static bool onPersonTypeIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onPersonType;
				public static event EventHandler<Person, PropertyEventArgs> OnPersonType
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPersonTypeIsRegistered)
							{
								Members.PersonType.Events.OnChange -= onPersonTypeProxy;
								Members.PersonType.Events.OnChange += onPersonTypeProxy;
								onPersonTypeIsRegistered = true;
							}
							onPersonType += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPersonType -= value;
							if (onPersonType == null && onPersonTypeIsRegistered)
							{
								Members.PersonType.Events.OnChange -= onPersonTypeProxy;
								onPersonTypeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPersonTypeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onPersonType;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnNameStyle

				private static bool onNameStyleIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onNameStyle;
				public static event EventHandler<Person, PropertyEventArgs> OnNameStyle
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onNameStyleIsRegistered)
							{
								Members.NameStyle.Events.OnChange -= onNameStyleProxy;
								Members.NameStyle.Events.OnChange += onNameStyleProxy;
								onNameStyleIsRegistered = true;
							}
							onNameStyle += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onNameStyle -= value;
							if (onNameStyle == null && onNameStyleIsRegistered)
							{
								Members.NameStyle.Events.OnChange -= onNameStyleProxy;
								onNameStyleIsRegistered = false;
							}
						}
					}
				}
            
				private static void onNameStyleProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onNameStyle;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnTitle

				private static bool onTitleIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onTitle;
				public static event EventHandler<Person, PropertyEventArgs> OnTitle
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTitleIsRegistered)
							{
								Members.Title.Events.OnChange -= onTitleProxy;
								Members.Title.Events.OnChange += onTitleProxy;
								onTitleIsRegistered = true;
							}
							onTitle += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTitle -= value;
							if (onTitle == null && onTitleIsRegistered)
							{
								Members.Title.Events.OnChange -= onTitleProxy;
								onTitleIsRegistered = false;
							}
						}
					}
				}
            
				private static void onTitleProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onTitle;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnFirstName

				private static bool onFirstNameIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onFirstName;
				public static event EventHandler<Person, PropertyEventArgs> OnFirstName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFirstNameIsRegistered)
							{
								Members.FirstName.Events.OnChange -= onFirstNameProxy;
								Members.FirstName.Events.OnChange += onFirstNameProxy;
								onFirstNameIsRegistered = true;
							}
							onFirstName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFirstName -= value;
							if (onFirstName == null && onFirstNameIsRegistered)
							{
								Members.FirstName.Events.OnChange -= onFirstNameProxy;
								onFirstNameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onFirstNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onFirstName;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnMiddleName

				private static bool onMiddleNameIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onMiddleName;
				public static event EventHandler<Person, PropertyEventArgs> OnMiddleName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onMiddleNameIsRegistered)
							{
								Members.MiddleName.Events.OnChange -= onMiddleNameProxy;
								Members.MiddleName.Events.OnChange += onMiddleNameProxy;
								onMiddleNameIsRegistered = true;
							}
							onMiddleName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onMiddleName -= value;
							if (onMiddleName == null && onMiddleNameIsRegistered)
							{
								Members.MiddleName.Events.OnChange -= onMiddleNameProxy;
								onMiddleNameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onMiddleNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onMiddleName;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnLastName

				private static bool onLastNameIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onLastName;
				public static event EventHandler<Person, PropertyEventArgs> OnLastName
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLastNameIsRegistered)
							{
								Members.LastName.Events.OnChange -= onLastNameProxy;
								Members.LastName.Events.OnChange += onLastNameProxy;
								onLastNameIsRegistered = true;
							}
							onLastName += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLastName -= value;
							if (onLastName == null && onLastNameIsRegistered)
							{
								Members.LastName.Events.OnChange -= onLastNameProxy;
								onLastNameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLastNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onLastName;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnSuffix

				private static bool onSuffixIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onSuffix;
				public static event EventHandler<Person, PropertyEventArgs> OnSuffix
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSuffixIsRegistered)
							{
								Members.Suffix.Events.OnChange -= onSuffixProxy;
								Members.Suffix.Events.OnChange += onSuffixProxy;
								onSuffixIsRegistered = true;
							}
							onSuffix += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSuffix -= value;
							if (onSuffix == null && onSuffixIsRegistered)
							{
								Members.Suffix.Events.OnChange -= onSuffixProxy;
								onSuffixIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSuffixProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onSuffix;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnEmailPromotion

				private static bool onEmailPromotionIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onEmailPromotion;
				public static event EventHandler<Person, PropertyEventArgs> OnEmailPromotion
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmailPromotionIsRegistered)
							{
								Members.EmailPromotion.Events.OnChange -= onEmailPromotionProxy;
								Members.EmailPromotion.Events.OnChange += onEmailPromotionProxy;
								onEmailPromotionIsRegistered = true;
							}
							onEmailPromotion += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmailPromotion -= value;
							if (onEmailPromotion == null && onEmailPromotionIsRegistered)
							{
								Members.EmailPromotion.Events.OnChange -= onEmailPromotionProxy;
								onEmailPromotionIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEmailPromotionProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onEmailPromotion;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnAdditionalContactInfo

				private static bool onAdditionalContactInfoIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onAdditionalContactInfo;
				public static event EventHandler<Person, PropertyEventArgs> OnAdditionalContactInfo
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAdditionalContactInfoIsRegistered)
							{
								Members.AdditionalContactInfo.Events.OnChange -= onAdditionalContactInfoProxy;
								Members.AdditionalContactInfo.Events.OnChange += onAdditionalContactInfoProxy;
								onAdditionalContactInfoIsRegistered = true;
							}
							onAdditionalContactInfo += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAdditionalContactInfo -= value;
							if (onAdditionalContactInfo == null && onAdditionalContactInfoIsRegistered)
							{
								Members.AdditionalContactInfo.Events.OnChange -= onAdditionalContactInfoProxy;
								onAdditionalContactInfoIsRegistered = false;
							}
						}
					}
				}
            
				private static void onAdditionalContactInfoProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onAdditionalContactInfo;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnDemographics

				private static bool onDemographicsIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onDemographics;
				public static event EventHandler<Person, PropertyEventArgs> OnDemographics
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDemographicsIsRegistered)
							{
								Members.Demographics.Events.OnChange -= onDemographicsProxy;
								Members.Demographics.Events.OnChange += onDemographicsProxy;
								onDemographicsIsRegistered = true;
							}
							onDemographics += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDemographics -= value;
							if (onDemographics == null && onDemographicsIsRegistered)
							{
								Members.Demographics.Events.OnChange -= onDemographicsProxy;
								onDemographicsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDemographicsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onDemographics;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region Onrowguid

				private static bool onrowguidIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onrowguid;
				public static event EventHandler<Person, PropertyEventArgs> Onrowguid
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
					EventHandler<Person, PropertyEventArgs> handler = onrowguid;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnPassword

				private static bool onPasswordIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onPassword;
				public static event EventHandler<Person, PropertyEventArgs> OnPassword
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPasswordIsRegistered)
							{
								Members.Password.Events.OnChange -= onPasswordProxy;
								Members.Password.Events.OnChange += onPasswordProxy;
								onPasswordIsRegistered = true;
							}
							onPassword += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPassword -= value;
							if (onPassword == null && onPasswordIsRegistered)
							{
								Members.Password.Events.OnChange -= onPasswordProxy;
								onPasswordIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPasswordProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onPassword;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnEmailAddress

				private static bool onEmailAddressIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onEmailAddress;
				public static event EventHandler<Person, PropertyEventArgs> OnEmailAddress
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
					EventHandler<Person, PropertyEventArgs> handler = onEmailAddress;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnDocument

				private static bool onDocumentIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onDocument;
				public static event EventHandler<Person, PropertyEventArgs> OnDocument
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDocumentIsRegistered)
							{
								Members.Document.Events.OnChange -= onDocumentProxy;
								Members.Document.Events.OnChange += onDocumentProxy;
								onDocumentIsRegistered = true;
							}
							onDocument += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDocument -= value;
							if (onDocument == null && onDocumentIsRegistered)
							{
								Members.Document.Events.OnChange -= onDocumentProxy;
								onDocumentIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDocumentProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onDocument;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnEmployee

				private static bool onEmployeeIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onEmployee;
				public static event EventHandler<Person, PropertyEventArgs> OnEmployee
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmployeeIsRegistered)
							{
								Members.Employee.Events.OnChange -= onEmployeeProxy;
								Members.Employee.Events.OnChange += onEmployeeProxy;
								onEmployeeIsRegistered = true;
							}
							onEmployee += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmployee -= value;
							if (onEmployee == null && onEmployeeIsRegistered)
							{
								Members.Employee.Events.OnChange -= onEmployeeProxy;
								onEmployeeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onEmployeeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onEmployee;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnCreditCard

				private static bool onCreditCardIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onCreditCard;
				public static event EventHandler<Person, PropertyEventArgs> OnCreditCard
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onCreditCardIsRegistered)
							{
								Members.CreditCard.Events.OnChange -= onCreditCardProxy;
								Members.CreditCard.Events.OnChange += onCreditCardProxy;
								onCreditCardIsRegistered = true;
							}
							onCreditCard += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onCreditCard -= value;
							if (onCreditCard == null && onCreditCardIsRegistered)
							{
								Members.CreditCard.Events.OnChange -= onCreditCardProxy;
								onCreditCardIsRegistered = false;
							}
						}
					}
				}
            
				private static void onCreditCardProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onCreditCard;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnContactType

				private static bool onContactTypeIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onContactType;
				public static event EventHandler<Person, PropertyEventArgs> OnContactType
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onContactTypeIsRegistered)
							{
								Members.ContactType.Events.OnChange -= onContactTypeProxy;
								Members.ContactType.Events.OnChange += onContactTypeProxy;
								onContactTypeIsRegistered = true;
							}
							onContactType += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onContactType -= value;
							if (onContactType == null && onContactTypeIsRegistered)
							{
								Members.ContactType.Events.OnChange -= onContactTypeProxy;
								onContactTypeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onContactTypeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onContactType;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnPhoneNumberType

				private static bool onPhoneNumberTypeIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onPhoneNumberType;
				public static event EventHandler<Person, PropertyEventArgs> OnPhoneNumberType
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPhoneNumberTypeIsRegistered)
							{
								Members.PhoneNumberType.Events.OnChange -= onPhoneNumberTypeProxy;
								Members.PhoneNumberType.Events.OnChange += onPhoneNumberTypeProxy;
								onPhoneNumberTypeIsRegistered = true;
							}
							onPhoneNumberType += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPhoneNumberType -= value;
							if (onPhoneNumberType == null && onPhoneNumberTypeIsRegistered)
							{
								Members.PhoneNumberType.Events.OnChange -= onPhoneNumberTypeProxy;
								onPhoneNumberTypeIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPhoneNumberTypeProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onPhoneNumberType;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnAddress

				private static bool onAddressIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onAddress;
				public static event EventHandler<Person, PropertyEventArgs> OnAddress
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onAddressIsRegistered)
							{
								Members.Address.Events.OnChange -= onAddressProxy;
								Members.Address.Events.OnChange += onAddressProxy;
								onAddressIsRegistered = true;
							}
							onAddress += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onAddress -= value;
							if (onAddress == null && onAddressIsRegistered)
							{
								Members.Address.Events.OnChange -= onAddressProxy;
								onAddressIsRegistered = false;
							}
						}
					}
				}
            
				private static void onAddressProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onAddress;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnSalesPerson

				private static bool onSalesPersonIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onSalesPerson;
				public static event EventHandler<Person, PropertyEventArgs> OnSalesPerson
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSalesPersonIsRegistered)
							{
								Members.SalesPerson.Events.OnChange -= onSalesPersonProxy;
								Members.SalesPerson.Events.OnChange += onSalesPersonProxy;
								onSalesPersonIsRegistered = true;
							}
							onSalesPerson += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSalesPerson -= value;
							if (onSalesPerson == null && onSalesPersonIsRegistered)
							{
								Members.SalesPerson.Events.OnChange -= onSalesPersonProxy;
								onSalesPersonIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSalesPersonProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onSalesPerson;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Person, PropertyEventArgs> OnModifiedDate
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
					EventHandler<Person, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onUid;
				public static event EventHandler<Person, PropertyEventArgs> OnUid
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
					EventHandler<Person, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IPersonOriginalData

		public IPersonOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IPerson

		int? IPersonOriginalData.PersonType { get { return OriginalData.PersonType; } }
		string IPersonOriginalData.NameStyle { get { return OriginalData.NameStyle; } }
		string IPersonOriginalData.Title { get { return OriginalData.Title; } }
		string IPersonOriginalData.FirstName { get { return OriginalData.FirstName; } }
		string IPersonOriginalData.MiddleName { get { return OriginalData.MiddleName; } }
		string IPersonOriginalData.LastName { get { return OriginalData.LastName; } }
		string IPersonOriginalData.Suffix { get { return OriginalData.Suffix; } }
		string IPersonOriginalData.EmailPromotion { get { return OriginalData.EmailPromotion; } }
		string IPersonOriginalData.AdditionalContactInfo { get { return OriginalData.AdditionalContactInfo; } }
		string IPersonOriginalData.Demographics { get { return OriginalData.Demographics; } }
		string IPersonOriginalData.rowguid { get { return OriginalData.rowguid; } }
		Password IPersonOriginalData.Password { get { return ((ILookupHelper<Password>)OriginalData.Password).GetOriginalItem(null); } }
		EmailAddress IPersonOriginalData.EmailAddress { get { return ((ILookupHelper<EmailAddress>)OriginalData.EmailAddress).GetOriginalItem(null); } }
		Document IPersonOriginalData.Document { get { return ((ILookupHelper<Document>)OriginalData.Document).GetOriginalItem(null); } }
		Employee IPersonOriginalData.Employee { get { return ((ILookupHelper<Employee>)OriginalData.Employee).GetOriginalItem(null); } }
		CreditCard IPersonOriginalData.CreditCard { get { return ((ILookupHelper<CreditCard>)OriginalData.CreditCard).GetOriginalItem(null); } }
		ContactType IPersonOriginalData.ContactType { get { return ((ILookupHelper<ContactType>)OriginalData.ContactType).GetOriginalItem(null); } }
		PhoneNumberType IPersonOriginalData.PhoneNumberType { get { return ((ILookupHelper<PhoneNumberType>)OriginalData.PhoneNumberType).GetOriginalItem(null); } }
		Address IPersonOriginalData.Address { get { return ((ILookupHelper<Address>)OriginalData.Address).GetOriginalItem(null); } }
		SalesPerson IPersonOriginalData.SalesPerson { get { return ((ILookupHelper<SalesPerson>)OriginalData.SalesPerson).GetOriginalItem(null); } }

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