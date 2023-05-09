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
	public interface IEmailAddressOriginalData : INeo4jBaseOriginalData
	{
		string EmailAddr { get; }
		IEnumerable<Person> EmailAddresses { get; }
	}

	public partial class EmailAddress : OGM<EmailAddress, EmailAddress.EmailAddressData, System.String>, INeo4jBase, IEmailAddressOriginalData
	{
		#region Initialize

		static EmailAddress()
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

		public static Dictionary<System.String, EmailAddress> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.EmailAddressAlias, IWhereQuery> query)
		{
			q.EmailAddressAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.EmailAddress.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"EmailAddress => EmailAddr : {this.EmailAddr?.ToString() ?? "null"}, Uid : {this.Uid}";
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
					OriginalData = new EmailAddressData(InnerData);
			}
		}


		#endregion

		#region Validations

		protected override void ValidateSave()
		{
			bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class EmailAddressData : Data<System.String>
		{
			public EmailAddressData()
			{

			}

			public EmailAddressData(EmailAddressData data)
			{
				EmailAddr = data.EmailAddr;
				EmailAddresses = data.EmailAddresses;
				Uid = data.Uid;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "EmailAddress";

				EmailAddresses = new EntityCollection<Person>(Wrapper, Members.EmailAddresses, item => { if (Members.EmailAddresses.Events.HasRegisteredChangeHandlers) { object loadHack = item.EmailAddress; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("EmailAddr",  EmailAddr);
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("EmailAddr", out value))
					EmailAddr = (string)value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IEmailAddress

			public string EmailAddr { get; set; }
			public EntityCollection<Person> EmailAddresses { get; private set; }

			#endregion
			#region Members for interface INeo4jBase

			public string Uid { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IEmailAddress

		public string EmailAddr { get { LazyGet(); return InnerData.EmailAddr; } set { if (LazySet(Members.EmailAddr, InnerData.EmailAddr, value)) InnerData.EmailAddr = value; } }
		public EntityCollection<Person> EmailAddresses { get { return InnerData.EmailAddresses; } }
		private void ClearEmailAddresses(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.EmailAddresses).ClearLookup(moment);
		}

		#endregion
		#region Members for interface INeo4jBase

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

		private static EmailAddressMembers members = null;
		public static EmailAddressMembers Members
		{
			get
			{
				if (members is null)
				{
					lock (typeof(EmailAddress))
					{
						if (members is null)
							members = new EmailAddressMembers();
					}
				}
				return members;
			}
		}
		public class EmailAddressMembers
		{
			internal EmailAddressMembers() { }

			#region Members for interface IEmailAddress

			public Property EmailAddr { get; } = Datastore.AdventureWorks.Model.Entities["EmailAddress"].Properties["EmailAddr"];
			public Property EmailAddresses { get; } = Datastore.AdventureWorks.Model.Entities["EmailAddress"].Properties["EmailAddresses"];
			#endregion

			#region Members for interface INeo4jBase

			public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

		}

		private static EmailAddressFullTextMembers fullTextMembers = null;
		public static EmailAddressFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(EmailAddress))
					{
						if (fullTextMembers is null)
							fullTextMembers = new EmailAddressFullTextMembers();
					}
				}
				return fullTextMembers;
			}
		}

		public class EmailAddressFullTextMembers
		{
			internal EmailAddressFullTextMembers() { }

		}

		sealed public override Entity GetEntity()
		{
			if (entity is null)
			{
				lock (typeof(EmailAddress))
				{
					if (entity is null)
						entity = Datastore.AdventureWorks.Model.Entities["EmailAddress"];
				}
			}
			return entity;
		}

		private static EmailAddressEvents events = null;
		public static EmailAddressEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(EmailAddress))
					{
						if (events is null)
							events = new EmailAddressEvents();
					}
				}
				return events;
			}
		}
		public class EmailAddressEvents
		{

			#region OnNew

			private bool onNewIsRegistered = false;

			private EventHandler<EmailAddress, EntityEventArgs> onNew;
			public event EventHandler<EmailAddress, EntityEventArgs> OnNew
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
				EventHandler<EmailAddress, EntityEventArgs> handler = onNew;
				if (handler is not null)
					handler.Invoke((EmailAddress)sender, args);
			}

			#endregion

			#region OnDelete

			private bool onDeleteIsRegistered = false;

			private EventHandler<EmailAddress, EntityEventArgs> onDelete;
			public event EventHandler<EmailAddress, EntityEventArgs> OnDelete
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
				EventHandler<EmailAddress, EntityEventArgs> handler = onDelete;
				if (handler is not null)
					handler.Invoke((EmailAddress)sender, args);
			}

			#endregion

			#region OnSave

			private bool onSaveIsRegistered = false;

			private EventHandler<EmailAddress, EntityEventArgs> onSave;
			public event EventHandler<EmailAddress, EntityEventArgs> OnSave
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
				EventHandler<EmailAddress, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((EmailAddress)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<EmailAddress, EntityEventArgs> onAfterSave;
			public event EventHandler<EmailAddress, EntityEventArgs> OnAfterSave
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
				EventHandler<EmailAddress, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((EmailAddress)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region OnEmailAddr

				private static bool onEmailAddrIsRegistered = false;

				private static EventHandler<EmailAddress, PropertyEventArgs> onEmailAddr;
				public static event EventHandler<EmailAddress, PropertyEventArgs> OnEmailAddr
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmailAddrIsRegistered)
							{
								Members.EmailAddr.Events.OnChange -= onEmailAddrProxy;
								Members.EmailAddr.Events.OnChange += onEmailAddrProxy;
								onEmailAddrIsRegistered = true;
							}
							onEmailAddr += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmailAddr -= value;
							if (onEmailAddr is null && onEmailAddrIsRegistered)
							{
								Members.EmailAddr.Events.OnChange -= onEmailAddrProxy;
								onEmailAddrIsRegistered = false;
							}
						}
					}
				}
			
				private static void onEmailAddrProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmailAddress, PropertyEventArgs> handler = onEmailAddr;
					if (handler is not null)
						handler.Invoke((EmailAddress)sender, args);
				}

				#endregion

				#region OnEmailAddresses

				private static bool onEmailAddressesIsRegistered = false;

				private static EventHandler<EmailAddress, PropertyEventArgs> onEmailAddresses;
				public static event EventHandler<EmailAddress, PropertyEventArgs> OnEmailAddresses
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onEmailAddressesIsRegistered)
							{
								Members.EmailAddresses.Events.OnChange -= onEmailAddressesProxy;
								Members.EmailAddresses.Events.OnChange += onEmailAddressesProxy;
								onEmailAddressesIsRegistered = true;
							}
							onEmailAddresses += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onEmailAddresses -= value;
							if (onEmailAddresses is null && onEmailAddressesIsRegistered)
							{
								Members.EmailAddresses.Events.OnChange -= onEmailAddressesProxy;
								onEmailAddressesIsRegistered = false;
							}
						}
					}
				}
			
				private static void onEmailAddressesProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<EmailAddress, PropertyEventArgs> handler = onEmailAddresses;
					if (handler is not null)
						handler.Invoke((EmailAddress)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<EmailAddress, PropertyEventArgs> onUid;
				public static event EventHandler<EmailAddress, PropertyEventArgs> OnUid
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
					EventHandler<EmailAddress, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((EmailAddress)sender, args);
				}

				#endregion

			}

			#endregion
		}

		#endregion

		#region IEmailAddressOriginalData

		public IEmailAddressOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IEmailAddress

		string IEmailAddressOriginalData.EmailAddr { get { return OriginalData.EmailAddr; } }
		IEnumerable<Person> IEmailAddressOriginalData.EmailAddresses { get { return OriginalData.EmailAddresses.OriginalData; } }

		#endregion
		#region Members for interface INeo4jBase

		INeo4jBaseOriginalData INeo4jBase.OriginalVersion { get { return this; } }

		string INeo4jBaseOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}