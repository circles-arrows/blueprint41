using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;

namespace Datastore.Manipulation
{
	public interface IGenreOriginalData : IBaseOriginalData
    {
		string Name { get; }
		IEnumerable<Genre> SubGenre { get; }
		IEnumerable<Genre> ParentGenre { get; }
    }

	public partial class Genre : OGM<Genre, Genre.GenreData, System.String>, IBase, IGenreOriginalData
	{
        #region Initialize

        static Genre()
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

        public static Dictionary<System.String, Genre> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.GenreAlias, IWhereQuery> query)
        {
            q.GenreAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Genre.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Genre => Name : {this.Name}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
                    OriginalData = new GenreData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Name == null)
				throw new PersistenceException(string.Format("Cannot save Genre with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Uid == null)
				throw new PersistenceException(string.Format("Cannot save Genre with key '{0}' because the Uid cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class GenreData : Data<System.String>
		{
			public GenreData()
            {

            }

            public GenreData(GenreData data)
            {
				Name = data.Name;
				SubGenre = data.SubGenre;
				ParentGenre = data.ParentGenre;
				Uid = data.Uid;
				LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Genre";

				SubGenre = new EntityCollection<Genre>(Wrapper, Members.SubGenre, item => { if (Members.SubGenre.Events.HasRegisteredChangeHandlers) { int loadHack = item.ParentGenre.Count; } });
				ParentGenre = new EntityCollection<Genre>(Wrapper, Members.ParentGenre, item => { if (Members.ParentGenre.Events.HasRegisteredChangeHandlers) { int loadHack = item.SubGenre.Count; } });
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
				dictionary.Add("LastModifiedOn",  Conversion<System.DateTime, long>.Convert(LastModifiedOn));
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Name", out value))
					Name = (string)value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("LastModifiedOn", out value))
					LastModifiedOn = Conversion<long, System.DateTime>.Convert((long)value);
			}

			#endregion

			#region Members for interface IGenre

			public string Name { get; set; }
			public EntityCollection<Genre> SubGenre { get; private set; }
			public EntityCollection<Genre> ParentGenre { get; private set; }

			#endregion
			#region Members for interface IBase

			public string Uid { get; set; }
			public System.DateTime LastModifiedOn { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IGenre

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
		public EntityCollection<Genre> SubGenre { get { return InnerData.SubGenre; } }
		private void ClearSubGenre(DateTime? moment)
		{
			((ILookupHelper<Genre>)InnerData.SubGenre).ClearLookup(moment);
		}
		public EntityCollection<Genre> ParentGenre { get { return InnerData.ParentGenre; } }
		private void ClearParentGenre(DateTime? moment)
		{
			((ILookupHelper<Genre>)InnerData.ParentGenre).ClearLookup(moment);
		}

		#endregion
		#region Members for interface IBase

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
		public System.DateTime LastModifiedOn { get { LazyGet(); return InnerData.LastModifiedOn; } set { if (LazySet(Members.LastModifiedOn, InnerData.LastModifiedOn, value)) InnerData.LastModifiedOn = value; } }
		protected override DateTime GetRowVersion() { return LastModifiedOn; }
		public override void SetRowVersion(DateTime? value) { LastModifiedOn = value ?? DateTime.MinValue; }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

        private static GenreMembers members = null;
        public static GenreMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Genre))
                    {
                        if (members == null)
                            members = new GenreMembers();
                    }
                }
                return members;
            }
        }
        public class GenreMembers
        {
            internal GenreMembers() { }

			#region Members for interface IGenre

            public Property Name { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Genre"].Properties["Name"];
            public Property SubGenre { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Genre"].Properties["SubGenre"];
            public Property ParentGenre { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Genre"].Properties["ParentGenre"];
			#endregion

			#region Members for interface IBase

            public Property Uid { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Base"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Base"].Properties["LastModifiedOn"];
			#endregion

        }

        private static GenreFullTextMembers fullTextMembers = null;
        public static GenreFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Genre))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new GenreFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class GenreFullTextMembers
        {
            internal GenreFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Genre))
                {
                    if (entity == null)
                        entity = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Genre"];
                }
            }
            return entity;
        }

		private static GenreEvents events = null;
        public static GenreEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Genre))
                    {
                        if (events == null)
                            events = new GenreEvents();
                    }
                }
                return events;
            }
        }
        public class GenreEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Genre, EntityEventArgs> onNew;
            public event EventHandler<Genre, EntityEventArgs> OnNew
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
                EventHandler<Genre, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Genre)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Genre, EntityEventArgs> onDelete;
            public event EventHandler<Genre, EntityEventArgs> OnDelete
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
                EventHandler<Genre, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Genre)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Genre, EntityEventArgs> onSave;
            public event EventHandler<Genre, EntityEventArgs> OnSave
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
                EventHandler<Genre, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Genre)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Genre, PropertyEventArgs> onName;
				public static event EventHandler<Genre, PropertyEventArgs> OnName
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
					EventHandler<Genre, PropertyEventArgs> handler = onName;
					if ((object)handler != null)
						handler.Invoke((Genre)sender, args);
				}

				#endregion

				#region OnSubGenre

				private static bool onSubGenreIsRegistered = false;

				private static EventHandler<Genre, PropertyEventArgs> onSubGenre;
				public static event EventHandler<Genre, PropertyEventArgs> OnSubGenre
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onSubGenreIsRegistered)
							{
								Members.SubGenre.Events.OnChange -= onSubGenreProxy;
								Members.SubGenre.Events.OnChange += onSubGenreProxy;
								onSubGenreIsRegistered = true;
							}
							onSubGenre += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onSubGenre -= value;
							if (onSubGenre == null && onSubGenreIsRegistered)
							{
								Members.SubGenre.Events.OnChange -= onSubGenreProxy;
								onSubGenreIsRegistered = false;
							}
						}
					}
				}
            
				private static void onSubGenreProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Genre, PropertyEventArgs> handler = onSubGenre;
					if ((object)handler != null)
						handler.Invoke((Genre)sender, args);
				}

				#endregion

				#region OnParentGenre

				private static bool onParentGenreIsRegistered = false;

				private static EventHandler<Genre, PropertyEventArgs> onParentGenre;
				public static event EventHandler<Genre, PropertyEventArgs> OnParentGenre
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onParentGenreIsRegistered)
							{
								Members.ParentGenre.Events.OnChange -= onParentGenreProxy;
								Members.ParentGenre.Events.OnChange += onParentGenreProxy;
								onParentGenreIsRegistered = true;
							}
							onParentGenre += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onParentGenre -= value;
							if (onParentGenre == null && onParentGenreIsRegistered)
							{
								Members.ParentGenre.Events.OnChange -= onParentGenreProxy;
								onParentGenreIsRegistered = false;
							}
						}
					}
				}
            
				private static void onParentGenreProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Genre, PropertyEventArgs> handler = onParentGenre;
					if ((object)handler != null)
						handler.Invoke((Genre)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Genre, PropertyEventArgs> onUid;
				public static event EventHandler<Genre, PropertyEventArgs> OnUid
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
					EventHandler<Genre, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Genre)sender, args);
				}

				#endregion

				#region OnLastModifiedOn

				private static bool onLastModifiedOnIsRegistered = false;

				private static EventHandler<Genre, PropertyEventArgs> onLastModifiedOn;
				public static event EventHandler<Genre, PropertyEventArgs> OnLastModifiedOn
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onLastModifiedOnIsRegistered)
							{
								Members.LastModifiedOn.Events.OnChange -= onLastModifiedOnProxy;
								Members.LastModifiedOn.Events.OnChange += onLastModifiedOnProxy;
								onLastModifiedOnIsRegistered = true;
							}
							onLastModifiedOn += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onLastModifiedOn -= value;
							if (onLastModifiedOn == null && onLastModifiedOnIsRegistered)
							{
								Members.LastModifiedOn.Events.OnChange -= onLastModifiedOnProxy;
								onLastModifiedOnIsRegistered = false;
							}
						}
					}
				}
            
				private static void onLastModifiedOnProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Genre, PropertyEventArgs> handler = onLastModifiedOn;
					if ((object)handler != null)
						handler.Invoke((Genre)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region Static Data

        public static class StaticData
        {
            /// <summary>
            /// Get the 'Uid' value for the corresponding Name
            /// </summary>
            public static class Name
            {
                public const string Action = "1";
                public const string Horror = "2";
                public const string Comedy = "3";
                public const string Documentary = "4";
				public static bool Exist(string name)
                {
                    if (name == "Action")
                        return true;
                    if (name == "Horror")
                        return true;
                    if (name == "Comedy")
                        return true;
                    if (name == "Documentary")
                        return true;
                    return false;
                }
            }
            /// <summary>
            /// Get the 'Uid' value for the corresponding Uid
            /// </summary>
            public static class Uid
            {
                public const string _1 = "1";
                public const string _2 = "2";
                public const string _3 = "3";
                public const string _4 = "4";
				public static bool Exist(string uid)
                {
                    if (uid == "1")
                        return true;
                    if (uid == "2")
                        return true;
                    if (uid == "3")
                        return true;
                    if (uid == "4")
                        return true;
                    return false;
                }
            }
        }

		#endregion

		#region IGenreOriginalData

		public IGenreOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IGenre

		string IGenreOriginalData.Name { get { return OriginalData.Name; } }
		IEnumerable<Genre> IGenreOriginalData.SubGenre { get { return OriginalData.SubGenre.OriginalData; } }
		IEnumerable<Genre> IGenreOriginalData.ParentGenre { get { return OriginalData.ParentGenre.OriginalData; } }

		#endregion
		#region Members for interface IBase

		IBaseOriginalData IBase.OriginalVersion { get { return this; } }

		string IBaseOriginalData.Uid { get { return OriginalData.Uid; } }
		System.DateTime IBaseOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

		#endregion
		#endregion
	}
}