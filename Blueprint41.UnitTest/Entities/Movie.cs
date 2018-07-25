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
	public interface IMovieOriginalData : IBaseEntityOriginalData
    {
		string Title { get; }
		Person Director { get; }
		IEnumerable<Person> Actors { get; }
    }

	public partial class Movie : OGM<Movie, Movie.MovieData, System.String>, IBaseEntity, IMovieOriginalData
	{
        #region Initialize

        static Movie()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadFromNaturalKey
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

			#region LoadByTitle

			RegisterQuery(nameof(LoadByTitle), (query, alias) => query.
                Where(alias.Title == Parameter.New<string>(Param0)));

			#endregion

			AdditionalGeneratedStoredQueries();
        }
		public static Movie LoadByTitle(string title)
		{
			return FromQuery(nameof(LoadByTitle), new Parameter(Param0, title)).FirstOrDefault();
		}
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, Movie> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.MovieAlias, IWhereQuery> query)
        {
            q.MovieAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Movie.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Movie => Title : {this.Title?.ToString() ?? "null"}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
                    OriginalData = new MovieData(InnerData);
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

		public class MovieData : Data<System.String>
		{
			public MovieData()
            {

            }

            public MovieData(MovieData data)
            {
				Title = data.Title;
				Director = data.Director;
				Actors = data.Actors;
				Uid = data.Uid;
				LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Movie";

				Director = new EntityCollection<Person>(Wrapper, Members.Director, item => { if (Members.Director.Events.HasRegisteredChangeHandlers) { int loadHack = item.DirectedMovies.Count; } });
				Actors = new EntityCollection<Person>(Wrapper, Members.Actors, item => { if (Members.Actors.Events.HasRegisteredChangeHandlers) { int loadHack = item.ActedInMovies.Count; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Title",  Title);
				dictionary.Add("Uid",  Uid);
				dictionary.Add("LastModifiedOn",  Conversion<System.DateTime, long>.Convert(LastModifiedOn));
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Title", out value))
					Title = (string)value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("LastModifiedOn", out value))
					LastModifiedOn = Conversion<long, System.DateTime>.Convert((long)value);
			}

			#endregion

			#region Members for interface IMovie

			public string Title { get; set; }
			public EntityCollection<Person> Director { get; private set; }
			public EntityCollection<Person> Actors { get; private set; }

			#endregion
			#region Members for interface IBaseEntity

			public string Uid { get; set; }
			public System.DateTime LastModifiedOn { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IMovie

		public string Title { get { LazyGet(); return InnerData.Title; } set { if (LazySet(Members.Title, InnerData.Title, value)) InnerData.Title = value; } }
		public Person Director
		{
			get { return ((ILookupHelper<Person>)InnerData.Director).GetItem(null); }
			set 
			{ 
				if (LazySet(Members.Director, ((ILookupHelper<Person>)InnerData.Director).GetItem(null), value))
					((ILookupHelper<Person>)InnerData.Director).SetItem(value, null); 
			}
		}
		private void ClearDirector(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.Director).ClearLookup(moment);
		}
		public EntityCollection<Person> Actors { get { return InnerData.Actors; } }
		private void ClearActors(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.Actors).ClearLookup(moment);
		}

		#endregion
		#region Members for interface IBaseEntity

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

        private static MovieMembers members = null;
        public static MovieMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Movie))
                    {
                        if (members == null)
                            members = new MovieMembers();
                    }
                }
                return members;
            }
        }
        public class MovieMembers
        {
            internal MovieMembers() { }

			#region Members for interface IMovie

            public Property Title { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"].Properties["Title"];
            public Property Director { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"].Properties["Director"];
            public Property Actors { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"].Properties["Actors"];
			#endregion

			#region Members for interface IBaseEntity

            public Property Uid { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"].Properties["LastModifiedOn"];
			#endregion

        }

        private static MovieFullTextMembers fullTextMembers = null;
        public static MovieFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Movie))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new MovieFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class MovieFullTextMembers
        {
            internal MovieFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Movie))
                {
                    if (entity == null)
                        entity = Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"];
                }
            }
            return entity;
        }

		private static MovieEvents events = null;
        public static MovieEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Movie))
                    {
                        if (events == null)
                            events = new MovieEvents();
                    }
                }
                return events;
            }
        }
        public class MovieEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Movie, EntityEventArgs> onNew;
            public event EventHandler<Movie, EntityEventArgs> OnNew
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
                EventHandler<Movie, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Movie)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Movie, EntityEventArgs> onDelete;
            public event EventHandler<Movie, EntityEventArgs> OnDelete
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
                EventHandler<Movie, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Movie)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Movie, EntityEventArgs> onSave;
            public event EventHandler<Movie, EntityEventArgs> OnSave
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
                EventHandler<Movie, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Movie)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnTitle

				private static bool onTitleIsRegistered = false;

				private static EventHandler<Movie, PropertyEventArgs> onTitle;
				public static event EventHandler<Movie, PropertyEventArgs> OnTitle
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
					EventHandler<Movie, PropertyEventArgs> handler = onTitle;
					if ((object)handler != null)
						handler.Invoke((Movie)sender, args);
				}

				#endregion

				#region OnDirector

				private static bool onDirectorIsRegistered = false;

				private static EventHandler<Movie, PropertyEventArgs> onDirector;
				public static event EventHandler<Movie, PropertyEventArgs> OnDirector
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDirectorIsRegistered)
							{
								Members.Director.Events.OnChange -= onDirectorProxy;
								Members.Director.Events.OnChange += onDirectorProxy;
								onDirectorIsRegistered = true;
							}
							onDirector += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDirector -= value;
							if (onDirector == null && onDirectorIsRegistered)
							{
								Members.Director.Events.OnChange -= onDirectorProxy;
								onDirectorIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDirectorProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Movie, PropertyEventArgs> handler = onDirector;
					if ((object)handler != null)
						handler.Invoke((Movie)sender, args);
				}

				#endregion

				#region OnActors

				private static bool onActorsIsRegistered = false;

				private static EventHandler<Movie, PropertyEventArgs> onActors;
				public static event EventHandler<Movie, PropertyEventArgs> OnActors
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onActorsIsRegistered)
							{
								Members.Actors.Events.OnChange -= onActorsProxy;
								Members.Actors.Events.OnChange += onActorsProxy;
								onActorsIsRegistered = true;
							}
							onActors += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onActors -= value;
							if (onActors == null && onActorsIsRegistered)
							{
								Members.Actors.Events.OnChange -= onActorsProxy;
								onActorsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onActorsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Movie, PropertyEventArgs> handler = onActors;
					if ((object)handler != null)
						handler.Invoke((Movie)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Movie, PropertyEventArgs> onUid;
				public static event EventHandler<Movie, PropertyEventArgs> OnUid
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
					EventHandler<Movie, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Movie)sender, args);
				}

				#endregion

				#region OnLastModifiedOn

				private static bool onLastModifiedOnIsRegistered = false;

				private static EventHandler<Movie, PropertyEventArgs> onLastModifiedOn;
				public static event EventHandler<Movie, PropertyEventArgs> OnLastModifiedOn
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
					EventHandler<Movie, PropertyEventArgs> handler = onLastModifiedOn;
					if ((object)handler != null)
						handler.Invoke((Movie)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IMovieOriginalData

		public IMovieOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IMovie

		string IMovieOriginalData.Title { get { return OriginalData.Title; } }
		Person IMovieOriginalData.Director { get { return ((ILookupHelper<Person>)OriginalData.Director).GetOriginalItem(null); } }
		IEnumerable<Person> IMovieOriginalData.Actors { get { return OriginalData.Actors.OriginalData; } }

		#endregion
		#region Members for interface IBaseEntity

		IBaseEntityOriginalData IBaseEntity.OriginalVersion { get { return this; } }

		string IBaseEntityOriginalData.Uid { get { return OriginalData.Uid; } }
		System.DateTime IBaseEntityOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

		#endregion
		#endregion
	}
}