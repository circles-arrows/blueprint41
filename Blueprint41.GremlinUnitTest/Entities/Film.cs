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
	public interface IFilmOriginalData : IBaseOriginalData
    {
		string Title { get; }
		string TagLine { get; }
		System.DateTime? ReleaseDate { get; }
		IEnumerable<Person> Actors { get; }
		IEnumerable<Person> Directors { get; }
		IEnumerable<Person> Producers { get; }
		IEnumerable<Person> Writers { get; }
    }

	public partial class Film : OGM<Film, Film.FilmData, System.String>, IBase, IFilmOriginalData
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

			AdditionalGeneratedStoredQueries();
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
            return $"Film => Title : {this.Title}, TagLine : {this.TagLine?.ToString() ?? "null"}, ReleaseDate : {this.ReleaseDate?.ToString() ?? "null"}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
			if (InnerData.Title == null)
				throw new PersistenceException(string.Format("Cannot save Film with key '{0}' because the Title cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Uid == null)
				throw new PersistenceException(string.Format("Cannot save Film with key '{0}' because the Uid cannot be null.", this.Uid?.ToString() ?? "<null>"));
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
				Title = data.Title;
				TagLine = data.TagLine;
				ReleaseDate = data.ReleaseDate;
				Actors = data.Actors;
				Directors = data.Directors;
				Producers = data.Producers;
				Writers = data.Writers;
				Uid = data.Uid;
				LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Film";

				Actors = new EntityCollection<Person>(Wrapper, Members.Actors, item => { if (Members.Actors.Events.HasRegisteredChangeHandlers) { int loadHack = item.ActedFilms.Count; } });
				Directors = new EntityCollection<Person>(Wrapper, Members.Directors, item => { if (Members.Directors.Events.HasRegisteredChangeHandlers) { int loadHack = item.DirectedFilms.Count; } });
				Producers = new EntityCollection<Person>(Wrapper, Members.Producers, item => { if (Members.Producers.Events.HasRegisteredChangeHandlers) { int loadHack = item.ProducedFilms.Count; } });
				Writers = new EntityCollection<Person>(Wrapper, Members.Writers, item => { if (Members.Writers.Events.HasRegisteredChangeHandlers) { int loadHack = item.FilmsWrited.Count; } });
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
				dictionary.Add("TagLine",  TagLine);
				dictionary.Add("ReleaseDate",  Conversion<System.DateTime?, long?>.Convert(ReleaseDate));
				dictionary.Add("Uid",  Uid);
				dictionary.Add("LastModifiedOn",  Conversion<System.DateTime, long>.Convert(LastModifiedOn));
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Title", out value))
					Title = (string)value;
				if (properties.TryGetValue("TagLine", out value))
					TagLine = (string)value;
				if (properties.TryGetValue("ReleaseDate", out value))
					ReleaseDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("LastModifiedOn", out value))
					LastModifiedOn = Conversion<long, System.DateTime>.Convert((long)value);
			}

			#endregion

			#region Members for interface IFilm

			public string Title { get; set; }
			public string TagLine { get; set; }
			public System.DateTime? ReleaseDate { get; set; }
			public EntityCollection<Person> Actors { get; private set; }
			public EntityCollection<Person> Directors { get; private set; }
			public EntityCollection<Person> Producers { get; private set; }
			public EntityCollection<Person> Writers { get; private set; }

			#endregion
			#region Members for interface IBase

			public string Uid { get; set; }
			public System.DateTime LastModifiedOn { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IFilm

		public string Title { get { LazyGet(); return InnerData.Title; } set { if (LazySet(Members.Title, InnerData.Title, value)) InnerData.Title = value; } }
		public string TagLine { get { LazyGet(); return InnerData.TagLine; } set { if (LazySet(Members.TagLine, InnerData.TagLine, value)) InnerData.TagLine = value; } }
		public System.DateTime? ReleaseDate { get { LazyGet(); return InnerData.ReleaseDate; } set { if (LazySet(Members.ReleaseDate, InnerData.ReleaseDate, value)) InnerData.ReleaseDate = value; } }
		public EntityCollection<Person> Actors { get { return InnerData.Actors; } }
		private void ClearActors(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.Actors).ClearLookup(moment);
		}
		public EntityCollection<Person> Directors { get { return InnerData.Directors; } }
		private void ClearDirectors(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.Directors).ClearLookup(moment);
		}
		public EntityCollection<Person> Producers { get { return InnerData.Producers; } }
		private void ClearProducers(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.Producers).ClearLookup(moment);
		}
		public EntityCollection<Person> Writers { get { return InnerData.Writers; } }
		private void ClearWriters(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.Writers).ClearLookup(moment);
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

            public Property Title { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Film"].Properties["Title"];
            public Property TagLine { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Film"].Properties["TagLine"];
            public Property ReleaseDate { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Film"].Properties["ReleaseDate"];
            public Property Actors { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Film"].Properties["Actors"];
            public Property Directors { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Film"].Properties["Directors"];
            public Property Producers { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Film"].Properties["Producers"];
            public Property Writers { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Film"].Properties["Writers"];
			#endregion

			#region Members for interface IBase

            public Property Uid { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Base"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Base"].Properties["LastModifiedOn"];
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
                        entity = Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Film"];
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

				#region OnTitle

				private static bool onTitleIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> onTitle;
				public static event EventHandler<Film, PropertyEventArgs> OnTitle
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
					EventHandler<Film, PropertyEventArgs> handler = onTitle;
					if ((object)handler != null)
						handler.Invoke((Film)sender, args);
				}

				#endregion

				#region OnTagLine

				private static bool onTagLineIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> onTagLine;
				public static event EventHandler<Film, PropertyEventArgs> OnTagLine
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onTagLineIsRegistered)
							{
								Members.TagLine.Events.OnChange -= onTagLineProxy;
								Members.TagLine.Events.OnChange += onTagLineProxy;
								onTagLineIsRegistered = true;
							}
							onTagLine += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onTagLine -= value;
							if (onTagLine == null && onTagLineIsRegistered)
							{
								Members.TagLine.Events.OnChange -= onTagLineProxy;
								onTagLineIsRegistered = false;
							}
						}
					}
				}
            
				private static void onTagLineProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Film, PropertyEventArgs> handler = onTagLine;
					if ((object)handler != null)
						handler.Invoke((Film)sender, args);
				}

				#endregion

				#region OnReleaseDate

				private static bool onReleaseDateIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> onReleaseDate;
				public static event EventHandler<Film, PropertyEventArgs> OnReleaseDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onReleaseDateIsRegistered)
							{
								Members.ReleaseDate.Events.OnChange -= onReleaseDateProxy;
								Members.ReleaseDate.Events.OnChange += onReleaseDateProxy;
								onReleaseDateIsRegistered = true;
							}
							onReleaseDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onReleaseDate -= value;
							if (onReleaseDate == null && onReleaseDateIsRegistered)
							{
								Members.ReleaseDate.Events.OnChange -= onReleaseDateProxy;
								onReleaseDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onReleaseDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Film, PropertyEventArgs> handler = onReleaseDate;
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

				#region OnLastModifiedOn

				private static bool onLastModifiedOnIsRegistered = false;

				private static EventHandler<Film, PropertyEventArgs> onLastModifiedOn;
				public static event EventHandler<Film, PropertyEventArgs> OnLastModifiedOn
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
					EventHandler<Film, PropertyEventArgs> handler = onLastModifiedOn;
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

		string IFilmOriginalData.Title { get { return OriginalData.Title; } }
		string IFilmOriginalData.TagLine { get { return OriginalData.TagLine; } }
		System.DateTime? IFilmOriginalData.ReleaseDate { get { return OriginalData.ReleaseDate; } }
		IEnumerable<Person> IFilmOriginalData.Actors { get { return OriginalData.Actors.OriginalData; } }
		IEnumerable<Person> IFilmOriginalData.Directors { get { return OriginalData.Directors.OriginalData; } }
		IEnumerable<Person> IFilmOriginalData.Producers { get { return OriginalData.Producers.OriginalData; } }
		IEnumerable<Person> IFilmOriginalData.Writers { get { return OriginalData.Writers.OriginalData; } }

		#endregion
		#region Members for interface IBase

		IBaseOriginalData IBase.OriginalVersion { get { return this; } }

		string IBaseOriginalData.Uid { get { return OriginalData.Uid; } }
		System.DateTime IBaseOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

		#endregion
		#endregion
	}
}