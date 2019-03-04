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
	public interface IPasswordOriginalData : INeo4jBaseOriginalData
    {
		string PasswordHash { get; }
		string PasswordSalt { get; }
    }

	public partial class Password : OGM<Password, Password.PasswordData, System.String>, INeo4jBase, IPasswordOriginalData
	{
        #region Initialize

        static Password()
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

        public static Dictionary<System.String, Password> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.PasswordAlias, IWhereQuery> query)
        {
            q.PasswordAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Password.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Password => PasswordHash : {this.PasswordHash}, PasswordSalt : {this.PasswordSalt}, Uid : {this.Uid}";
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
                    OriginalData = new PasswordData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.PasswordHash == null)
				throw new PersistenceException(string.Format("Cannot save Password with key '{0}' because the PasswordHash cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.PasswordSalt == null)
				throw new PersistenceException(string.Format("Cannot save Password with key '{0}' because the PasswordSalt cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class PasswordData : Data<System.String>
		{
			public PasswordData()
            {

            }

            public PasswordData(PasswordData data)
            {
				PasswordHash = data.PasswordHash;
				PasswordSalt = data.PasswordSalt;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Password";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("PasswordHash",  PasswordHash);
				dictionary.Add("PasswordSalt",  PasswordSalt);
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("PasswordHash", out value))
					PasswordHash = (string)value;
				if (properties.TryGetValue("PasswordSalt", out value))
					PasswordSalt = (string)value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IPassword

			public string PasswordHash { get; set; }
			public string PasswordSalt { get; set; }

			#endregion
			#region Members for interface INeo4jBase

			public string Uid { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IPassword

		public string PasswordHash { get { LazyGet(); return InnerData.PasswordHash; } set { if (LazySet(Members.PasswordHash, InnerData.PasswordHash, value)) InnerData.PasswordHash = value; } }
		public string PasswordSalt { get { LazyGet(); return InnerData.PasswordSalt; } set { if (LazySet(Members.PasswordSalt, InnerData.PasswordSalt, value)) InnerData.PasswordSalt = value; } }

		#endregion
		#region Members for interface INeo4jBase

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

        private static PasswordMembers members = null;
        public static PasswordMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Password))
                    {
                        if (members == null)
                            members = new PasswordMembers();
                    }
                }
                return members;
            }
        }
        public class PasswordMembers
        {
            internal PasswordMembers() { }

			#region Members for interface IPassword

            public Property PasswordHash { get; } = Datastore.AdventureWorks.Model.Entities["Password"].Properties["PasswordHash"];
            public Property PasswordSalt { get; } = Datastore.AdventureWorks.Model.Entities["Password"].Properties["PasswordSalt"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static PasswordFullTextMembers fullTextMembers = null;
        public static PasswordFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Password))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new PasswordFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class PasswordFullTextMembers
        {
            internal PasswordFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Password))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["Password"];
                }
            }
            return entity;
        }

		private static PasswordEvents events = null;
        public static PasswordEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Password))
                    {
                        if (events == null)
                            events = new PasswordEvents();
                    }
                }
                return events;
            }
        }
        public class PasswordEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Password, EntityEventArgs> onNew;
            public event EventHandler<Password, EntityEventArgs> OnNew
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
                EventHandler<Password, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Password)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Password, EntityEventArgs> onDelete;
            public event EventHandler<Password, EntityEventArgs> OnDelete
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
                EventHandler<Password, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Password)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Password, EntityEventArgs> onSave;
            public event EventHandler<Password, EntityEventArgs> OnSave
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
                EventHandler<Password, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Password)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnPasswordHash

				private static bool onPasswordHashIsRegistered = false;

				private static EventHandler<Password, PropertyEventArgs> onPasswordHash;
				public static event EventHandler<Password, PropertyEventArgs> OnPasswordHash
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPasswordHashIsRegistered)
							{
								Members.PasswordHash.Events.OnChange -= onPasswordHashProxy;
								Members.PasswordHash.Events.OnChange += onPasswordHashProxy;
								onPasswordHashIsRegistered = true;
							}
							onPasswordHash += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPasswordHash -= value;
							if (onPasswordHash == null && onPasswordHashIsRegistered)
							{
								Members.PasswordHash.Events.OnChange -= onPasswordHashProxy;
								onPasswordHashIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPasswordHashProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Password, PropertyEventArgs> handler = onPasswordHash;
					if ((object)handler != null)
						handler.Invoke((Password)sender, args);
				}

				#endregion

				#region OnPasswordSalt

				private static bool onPasswordSaltIsRegistered = false;

				private static EventHandler<Password, PropertyEventArgs> onPasswordSalt;
				public static event EventHandler<Password, PropertyEventArgs> OnPasswordSalt
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onPasswordSaltIsRegistered)
							{
								Members.PasswordSalt.Events.OnChange -= onPasswordSaltProxy;
								Members.PasswordSalt.Events.OnChange += onPasswordSaltProxy;
								onPasswordSaltIsRegistered = true;
							}
							onPasswordSalt += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onPasswordSalt -= value;
							if (onPasswordSalt == null && onPasswordSaltIsRegistered)
							{
								Members.PasswordSalt.Events.OnChange -= onPasswordSaltProxy;
								onPasswordSaltIsRegistered = false;
							}
						}
					}
				}
            
				private static void onPasswordSaltProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Password, PropertyEventArgs> handler = onPasswordSalt;
					if ((object)handler != null)
						handler.Invoke((Password)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Password, PropertyEventArgs> onUid;
				public static event EventHandler<Password, PropertyEventArgs> OnUid
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
					EventHandler<Password, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Password)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IPasswordOriginalData

		public IPasswordOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IPassword

		string IPasswordOriginalData.PasswordHash { get { return OriginalData.PasswordHash; } }
		string IPasswordOriginalData.PasswordSalt { get { return OriginalData.PasswordSalt; } }

		#endregion
		#region Members for interface INeo4jBase

		INeo4jBaseOriginalData INeo4jBase.OriginalVersion { get { return this; } }

		string INeo4jBaseOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}