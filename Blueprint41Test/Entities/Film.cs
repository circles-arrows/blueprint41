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
	public interface IFilmOriginalData
    {
		string Uid { get; }
		string title { get; }
		string tagline { get; }
		int release { get; }
		IEnumerable<Actor> Actors { get; }
		IEnumerable<Actor> Directors { get; }
		IEnumerable<Actor> Producers { get; }
		IEnumerable<Actor> Writers { get; }
    }

	public partial class Film : OGM<Film, Film.FilmData, System.String>, IFilmOriginalData
	{
        #region Initialize

        static Film()
        {
            Register.Types();
        }

        protected override void RegisterGeneratedStoredQueries()
        {
            #region LoadByKeys
            
            RegisterQuery(nameof(LoadByKeys), (query, alias) => query.
                Where(alias.Uid.In(Parameter.New<System.String>(Param0))));

            #endregion

			#region LoadByUid

			RegisterQuery(nameof(LoadByUid), (query, alias) => query.
                Where(alias.Uid == Parameter.New<string>(Param0)));

			#endregion

			AdditionalGeneratedStoredQueries();
        }
		public static Film LoadByUid(string uid)
		{
			return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
		}
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, Film> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.FilmAlias, IWhereQuery> query)
        {
            q.FilmAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Film.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Film => Uid : {this.Uid}, title : {this.title}, tagline : {this.tagline?.ToString() ?? "null"}, release : {this.release}";
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
                    OriginalData = new FilmData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.title == null)
				throw new PersistenceException(string.Format("Cannot save Film with key '{0}' because the title cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.release == null)
				throw new PersistenceException(string.Format("Cannot save Film with key '{0}' because the release cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class FilmData : Data<System.String>
		{
			public FilmData()
            {

            }

            public FilmData(FilmData data)
            {
				Uid = data.Uid;
				title = data.title;
				tagline = data.tagline;
				release = data.release;
				Actors = data.Actors;
				Directors = data.Directors;
				Producers = data.Producers;
				Writers = data.Writers;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Film";

				Actors = new EntityCollection<Actor>(Wrapper, Members.Actors, item => { if (Members.Actors.Events.HasRegisteredChangeHandlers) { int loadHack = item.ActedFilms.Count; } });
				Directors = new EntityCollection<Actor>(Wrapper, Members.Directors, item => { if (Members.Directors.Events.HasRegisteredChangeHandlers) { int loadHack = item.DirectedFilms.Count; } });
				Producers = new EntityCollection<Actor>(Wrapper, Members.Producers, item => { if (Members.Producers.Events.HasRegisteredChangeHandlers) { int loadHack = item.ProducedFilms.Count; } });
				Writers = new EntityCollection<Actor>(Wrapper, Members.Writers, item => { if (Members.Writers.Events.HasRegisteredChangeHandlers) { int loadHack = item.FilmsWrited.Count; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Uid",  Uid);
				dictionary.Add("title",  title);
				dictionary.Add("tagline",  tagline);
				dictionary.Add("release",  Conversion<int, long>.Convert(release));
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("title", out value))
					title = (string)value;
				if (properties.TryGetValue("tagline", out value))
					tagline = (string)value;
				if (properties.TryGetValue("release", out value))
					release = Conversion<long, int>.Convert((long)value);
			}

			#endregion

			#region Members for interface IFilm

			public string Uid { get; set; }
			public string title { get; set; }
			public string tagline { get; set; }
			public int release { get; set; }
			public EntityCollection<Actor> Actors { get; private set; }
			public EntityCollection<Actor> Directors { get; private set; }
			public EntityCollection<Actor> Producers { get; private set; }
			public EntityCollection<Actor> Writers { get; private set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IFilm

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
		public string title { get { LazyGet(); return InnerData.title; } set { if (LazySet(Members.title, InnerData.title, value)) InnerData.title = value; } }
		public string tagline { get { LazyGet(); return InnerData.tagline; } set { if (LazySet(Members.tagline, InnerData.tagline, value)) InnerData.tagline = value; } }
		public int release { get { LazyGet(); return InnerData.release; } set { if (LazySet(Members.release, InnerData.release, value)) InnerData.release = value; } }
		public EntityCollection<Actor> Actors { get { return InnerData.Actors; } }
		private void ClearActors(DateTime? moment)
		{
			((ILookupHelper<Actor>)InnerData.Actors).ClearLookup(moment);
		}
		public EntityCollection<Actor> Directors { get { return InnerData.Directors; } }
		private void ClearDirectors(DateTime? moment)
		{
			((ILookupHelper<Actor>)InnerData.Directors).ClearLookup(moment);
		}
		public EntityCollection<Actor> Producers { get { return InnerData.Producers; } }
		private void ClearProducers(DateTime? moment)
		{
			((ILookupHelper<Actor>)InnerData.Producers).ClearLookup(moment);
		}
		public EntityCollection<Actor> Writers { get { return InnerData.Writers; } }
		private void ClearWriters(DateTime? moment)
		{
			((ILookupHelper<Actor>)InnerData.Writers).ClearLookup(moment);
		}

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

        private static FilmMembers members = null;
        public static FilmMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Film))
                    {
                        if (members == null)
                            members = new FilmMembers();
                    }
                }
                return members;
            }
        }
        public class FilmMembers
        {
            internal FilmMembers() { }

			#region Members for interface IFilm

            public Property Uid { get; } = Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["Uid"];
            public Property title { get; } = Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["title"];
            public Property tagline { get; } = Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["tagline"];
            public Property release { get; } = Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["release"];
            public Property Actors { get; } = Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["Actors"];
            public Property Directors { get; } = Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["Directors"];
            public Property Producers { get; } = Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["Producers"];
            public Property Writers { get; } = Blueprint41Test.MovieModel.Model.Entities["Film"].Properties["Writers"];
			#endregion

        }

        private static FilmFullTextMembers fullTextMembers = null;
        public static FilmFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Film))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new FilmFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class FilmFullTextMembers
        {
            internal FilmFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Film))
                {
                    if (entity == null)
                        entity = Blueprint41Test.MovieModel.Model.Entities["Film"];
                }
            }
            return entity;
        }

		private static FilmEvents events = null;
        public static FilmEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Film))
                    {
                        if (events == null)
                            events = new FilmEvents();
                    }
                }
                return events;
            }
        }
        public class FilmEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Film, EntityEventArgs> onNew;
            public event EventHandler<Film, EntityEventArgs> OnNew
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
                EventHandler<Film, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Film)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Film, EntityEventArgs> onDelete;
            public event EventHandler<Film, EntityEventArgs> OnDelete
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
                EventHandler<Film, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Film)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Film, EntityEventArgs> onSave;
            public event EventHandler<Film, EntityEventArgs> OnSave
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
                EventHandler<Film, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Film)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> onUid;
				public static event EventHandler<Film, PropertyEventArgs> OnUid
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
					EventHandler<Film, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Film)sender, args);
				}

				#endregion

				#region Ontitle

				private static bool ontitleIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> ontitle;
				public static event EventHandler<Film, PropertyEventArgs> Ontitle
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!ontitleIsRegistered)
							{
								Members.title.Events.OnChange -= ontitleProxy;
								Members.title.Events.OnChange += ontitleProxy;
								ontitleIsRegistered = true;
							}
							ontitle += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							ontitle -= value;
							if (ontitle == null && ontitleIsRegistered)
							{
								Members.title.Events.OnChange -= ontitleProxy;
								ontitleIsRegistered = false;
							}
						}
					}
				}
            
				private static void ontitleProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Film, PropertyEventArgs> handler = ontitle;
					if ((object)handler != null)
						handler.Invoke((Film)sender, args);
				}

				#endregion

				#region Ontagline

				private static bool ontaglineIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> ontagline;
				public static event EventHandler<Film, PropertyEventArgs> Ontagline
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!ontaglineIsRegistered)
							{
								Members.tagline.Events.OnChange -= ontaglineProxy;
								Members.tagline.Events.OnChange += ontaglineProxy;
								ontaglineIsRegistered = true;
							}
							ontagline += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							ontagline -= value;
							if (ontagline == null && ontaglineIsRegistered)
							{
								Members.tagline.Events.OnChange -= ontaglineProxy;
								ontaglineIsRegistered = false;
							}
						}
					}
				}
            
				private static void ontaglineProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Film, PropertyEventArgs> handler = ontagline;
					if ((object)handler != null)
						handler.Invoke((Film)sender, args);
				}

				#endregion

				#region Onrelease

				private static bool onreleaseIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> onrelease;
				public static event EventHandler<Film, PropertyEventArgs> Onrelease
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onreleaseIsRegistered)
							{
								Members.release.Events.OnChange -= onreleaseProxy;
								Members.release.Events.OnChange += onreleaseProxy;
								onreleaseIsRegistered = true;
							}
							onrelease += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onrelease -= value;
							if (onrelease == null && onreleaseIsRegistered)
							{
								Members.release.Events.OnChange -= onreleaseProxy;
								onreleaseIsRegistered = false;
							}
						}
					}
				}
            
				private static void onreleaseProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Film, PropertyEventArgs> handler = onrelease;
					if ((object)handler != null)
						handler.Invoke((Film)sender, args);
				}

				#endregion

				#region OnActors

				private static bool onActorsIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> onActors;
				public static event EventHandler<Film, PropertyEventArgs> OnActors
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
					EventHandler<Film, PropertyEventArgs> handler = onActors;
					if ((object)handler != null)
						handler.Invoke((Film)sender, args);
				}

				#endregion

				#region OnDirectors

				private static bool onDirectorsIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> onDirectors;
				public static event EventHandler<Film, PropertyEventArgs> OnDirectors
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDirectorsIsRegistered)
							{
								Members.Directors.Events.OnChange -= onDirectorsProxy;
								Members.Directors.Events.OnChange += onDirectorsProxy;
								onDirectorsIsRegistered = true;
							}
							onDirectors += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDirectors -= value;
							if (onDirectors == null && onDirectorsIsRegistered)
							{
								Members.Directors.Events.OnChange -= onDirectorsProxy;
								onDirectorsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDirectorsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Film, PropertyEventArgs> handler = onDirectors;
					if ((object)handler != null)
						handler.Invoke((Film)sender, args);
				}

				#endregion

				#region OnProducers

				private static bool onProducersIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> onProducers;
				public static event EventHandler<Film, PropertyEventArgs> OnProducers
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProducersIsRegistered)
							{
								Members.Producers.Events.OnChange -= onProducersProxy;
								Members.Producers.Events.OnChange += onProducersProxy;
								onProducersIsRegistered = true;
							}
							onProducers += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProducers -= value;
							if (onProducers == null && onProducersIsRegistered)
							{
								Members.Producers.Events.OnChange -= onProducersProxy;
								onProducersIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProducersProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Film, PropertyEventArgs> handler = onProducers;
					if ((object)handler != null)
						handler.Invoke((Film)sender, args);
				}

				#endregion

				#region OnWriters

				private static bool onWritersIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> onWriters;
				public static event EventHandler<Film, PropertyEventArgs> OnWriters
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onWritersIsRegistered)
							{
								Members.Writers.Events.OnChange -= onWritersProxy;
								Members.Writers.Events.OnChange += onWritersProxy;
								onWritersIsRegistered = true;
							}
							onWriters += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onWriters -= value;
							if (onWriters == null && onWritersIsRegistered)
							{
								Members.Writers.Events.OnChange -= onWritersProxy;
								onWritersIsRegistered = false;
							}
						}
					}
				}
            
				private static void onWritersProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Film, PropertyEventArgs> handler = onWriters;
					if ((object)handler != null)
						handler.Invoke((Film)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IFilmOriginalData

		public IFilmOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IFilm

		string IFilmOriginalData.Uid { get { return OriginalData.Uid; } }
		string IFilmOriginalData.title { get { return OriginalData.title; } }
		string IFilmOriginalData.tagline { get { return OriginalData.tagline; } }
		int IFilmOriginalData.release { get { return OriginalData.release; } }
		IEnumerable<Actor> IFilmOriginalData.Actors { get { return OriginalData.Actors.OriginalData; } }
		IEnumerable<Actor> IFilmOriginalData.Directors { get { return OriginalData.Directors.OriginalData; } }
		IEnumerable<Actor> IFilmOriginalData.Producers { get { return OriginalData.Producers.OriginalData; } }
		IEnumerable<Actor> IFilmOriginalData.Writers { get { return OriginalData.Writers.OriginalData; } }

		#endregion
		#endregion
	}
}