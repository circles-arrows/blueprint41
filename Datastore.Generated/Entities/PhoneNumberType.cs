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
	public interface IPhoneNumberTypeOriginalData
    {
		#region Outer Data

		#region Members for interface IPhoneNumberType

		string Name { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
    }

	public partial class PhoneNumberType : OGM<PhoneNumberType, PhoneNumberType.PhoneNumberTypeData, System.String>, INeo4jBase, IPhoneNumberTypeOriginalData
	{
        #region Initialize

        static PhoneNumberType()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadFromNaturalKey
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

			AdditionalGeneratedStoredQueries();
        }
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, PhoneNumberType> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.PhoneNumberTypeAlias, IWhereQuery> query)
        {
            q.PhoneNumberTypeAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.PhoneNumberType.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"PhoneNumberType => Name : {this.Name?.ToString() ?? "null"}, Uid : {this.Uid}";
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
                    OriginalData = new PhoneNumberTypeData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class PhoneNumberTypeData : Data<System.String>
		{
			public PhoneNumberTypeData()
            {

            }

            public PhoneNumberTypeData(PhoneNumberTypeData data)
            {
				Name = data.Name;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "PhoneNumberType";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Name",  Name);
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IPhoneNumberType

			public string Name { get; set; }

			#endregion
			#region Members for interface INeo4jBase

			public string Uid { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IPhoneNumberType

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }

		#endregion
		#region Members for interface INeo4jBase

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

        private static PhoneNumberTypeMembers members = null;
        public static PhoneNumberTypeMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(PhoneNumberType))
                    {
                        if (members == null)
                            members = new PhoneNumberTypeMembers();
                    }
                }
                return members;
            }
        }
        public class PhoneNumberTypeMembers
        {
            internal PhoneNumberTypeMembers() { }

			#region Members for interface IPhoneNumberType

            public Property Name { get; } = Datastore.AdventureWorks.Model.Entities["PhoneNumberType"].Properties["Name"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static PhoneNumberTypeFullTextMembers fullTextMembers = null;
        public static PhoneNumberTypeFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(PhoneNumberType))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new PhoneNumberTypeFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class PhoneNumberTypeFullTextMembers
        {
            internal PhoneNumberTypeFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(PhoneNumberType))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["PhoneNumberType"];
                }
            }
            return entity;
        }

		private static PhoneNumberTypeEvents events = null;
        public static PhoneNumberTypeEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(PhoneNumberType))
                    {
                        if (events == null)
                            events = new PhoneNumberTypeEvents();
                    }
                }
                return events;
            }
        }
        public class PhoneNumberTypeEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<PhoneNumberType, EntityEventArgs> onNew;
            public event EventHandler<PhoneNumberType, EntityEventArgs> OnNew
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
                EventHandler<PhoneNumberType, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((PhoneNumberType)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<PhoneNumberType, EntityEventArgs> onDelete;
            public event EventHandler<PhoneNumberType, EntityEventArgs> OnDelete
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
                EventHandler<PhoneNumberType, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((PhoneNumberType)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<PhoneNumberType, EntityEventArgs> onSave;
            public event EventHandler<PhoneNumberType, EntityEventArgs> OnSave
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
                EventHandler<PhoneNumberType, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((PhoneNumberType)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<PhoneNumberType, PropertyEventArgs> onName;
				public static event EventHandler<PhoneNumberType, PropertyEventArgs> OnName
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
							if (onName == null && onNameIsRegistered)
							{
								Members.Name.Events.OnChange -= onNameProxy;
								onNameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onNameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<PhoneNumberType, PropertyEventArgs> handler = onName;
					if ((object)handler != null)
						handler.Invoke((PhoneNumberType)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<PhoneNumberType, PropertyEventArgs> onUid;
				public static event EventHandler<PhoneNumberType, PropertyEventArgs> OnUid
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
					EventHandler<PhoneNumberType, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((PhoneNumberType)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IPhoneNumberTypeOriginalData

		public IPhoneNumberTypeOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IPhoneNumberType

		string IPhoneNumberTypeOriginalData.Name { get { return OriginalData.Name; } }

		#endregion
		#region Members for interface INeo4jBase

		string IPhoneNumberTypeOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}