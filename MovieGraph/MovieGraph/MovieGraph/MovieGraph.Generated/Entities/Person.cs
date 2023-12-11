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
	public interface IPersonOriginalData
	{
		string name { get; }
		int? born { get; }
		string Uid { get; }
		IEnumerable<Movie> ActedMovies { get; }
		IEnumerable<Movie> DirectedMovies { get; }
		IEnumerable<Movie> ProducedMovies { get; }
		IEnumerable<Movie> WritedMovies { get; }
		IEnumerable<MovieReview> MovieReviews { get; }
		IEnumerable<MovieRole> MovieRoles { get; }
		IEnumerable<Person> FollowedPersons { get; }
		IEnumerable<Person> Followers { get; }
	}

	public partial class Person : OGM<Person, Person.PersonData, System.String>, IPersonOriginalData
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

			#region LoadByUid

			RegisterQuery(nameof(LoadByUid), (query, alias) => query.
				Where(alias.Uid == Parameter.New<string>(Param0)));

			#endregion

			AdditionalGeneratedStoredQueries();
		}
		public static Person LoadByUid(string uid)
		{
			return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
		}
		partial void AdditionalGeneratedStoredQueries();

		public static Dictionary<System.String, Person> LoadByKeys(IEnumerable<System.String> uids)
		{
			return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
		}

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.PersonAlias, IWhereQuery> query)
		{
			q.PersonAlias alias;

			IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Person.Alias(out alias, "node"));
			IWhereQuery partial = query.Invoke(matchQuery, alias);
			ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
		}

		public override string ToString()
		{
			return $"Person => name : {this.name?.ToString() ?? "null"}, born : {this.born?.ToString() ?? "null"}, Uid : {this.Uid}";
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
					OriginalData = new PersonData(InnerData);
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

		public class PersonData : Data<System.String>
		{
			public PersonData()
			{

			}

			public PersonData(PersonData data)
			{
				name = data.name;
				born = data.born;
				Uid = data.Uid;
				ActedMovies = data.ActedMovies;
				DirectedMovies = data.DirectedMovies;
				ProducedMovies = data.ProducedMovies;
				WritedMovies = data.WritedMovies;
				MovieReviews = data.MovieReviews;
				MovieRoles = data.MovieRoles;
				FollowedPersons = data.FollowedPersons;
				Followers = data.Followers;
			}


			#region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Person";

				ActedMovies = new EntityCollection<Movie>(Wrapper, Members.ActedMovies, item => { if (Members.ActedMovies.Events.HasRegisteredChangeHandlers) { int loadHack = item.Actors.Count; } });
				DirectedMovies = new EntityCollection<Movie>(Wrapper, Members.DirectedMovies, item => { if (Members.DirectedMovies.Events.HasRegisteredChangeHandlers) { int loadHack = item.Directors.Count; } });
				ProducedMovies = new EntityCollection<Movie>(Wrapper, Members.ProducedMovies, item => { if (Members.ProducedMovies.Events.HasRegisteredChangeHandlers) { int loadHack = item.Producers.Count; } });
				WritedMovies = new EntityCollection<Movie>(Wrapper, Members.WritedMovies, item => { if (Members.WritedMovies.Events.HasRegisteredChangeHandlers) { int loadHack = item.Writers.Count; } });
				MovieReviews = new EntityCollection<MovieReview>(Wrapper, Members.MovieReviews);
				MovieRoles = new EntityCollection<MovieRole>(Wrapper, Members.MovieRoles);
				FollowedPersons = new EntityCollection<Person>(Wrapper, Members.FollowedPersons, item => { if (Members.FollowedPersons.Events.HasRegisteredChangeHandlers) { int loadHack = item.Followers.Count; } });
				Followers = new EntityCollection<Person>(Wrapper, Members.Followers, item => { if (Members.Followers.Events.HasRegisteredChangeHandlers) { int loadHack = item.FollowedPersons.Count; } });
			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Entity.Parent.PersistenceProvider.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Entity.Parent.PersistenceProvider.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("name",  name);
				dictionary.Add("born",  Conversion<int?, long?>.Convert(born));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("name", out value))
					name = (string)value;
				if (properties.TryGetValue("born", out value))
					born = Conversion<long, int>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IPerson

			public string name { get; set; }
			public int? born { get; set; }
			public string Uid { get; set; }
			public EntityCollection<Movie> ActedMovies { get; private set; }
			public EntityCollection<Movie> DirectedMovies { get; private set; }
			public EntityCollection<Movie> ProducedMovies { get; private set; }
			public EntityCollection<Movie> WritedMovies { get; private set; }
			public EntityCollection<MovieReview> MovieReviews { get; private set; }
			public EntityCollection<MovieRole> MovieRoles { get; private set; }
			public EntityCollection<Person> FollowedPersons { get; private set; }
			public EntityCollection<Person> Followers { get; private set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IPerson

		public string name { get { LazyGet(); return InnerData.name; } set { if (LazySet(Members.name, InnerData.name, value)) InnerData.name = value; } }
		public int? born { get { LazyGet(); return InnerData.born; } set { if (LazySet(Members.born, InnerData.born, value)) InnerData.born = value; } }
		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
		public EntityCollection<Movie> ActedMovies { get { return InnerData.ActedMovies; } }
		private void ClearActedMovies(DateTime? moment)
		{
			((ILookupHelper<Movie>)InnerData.ActedMovies).ClearLookup(moment);
		}
		public EntityCollection<Movie> DirectedMovies { get { return InnerData.DirectedMovies; } }
		private void ClearDirectedMovies(DateTime? moment)
		{
			((ILookupHelper<Movie>)InnerData.DirectedMovies).ClearLookup(moment);
		}
		public EntityCollection<Movie> ProducedMovies { get { return InnerData.ProducedMovies; } }
		private void ClearProducedMovies(DateTime? moment)
		{
			((ILookupHelper<Movie>)InnerData.ProducedMovies).ClearLookup(moment);
		}
		public EntityCollection<Movie> WritedMovies { get { return InnerData.WritedMovies; } }
		private void ClearWritedMovies(DateTime? moment)
		{
			((ILookupHelper<Movie>)InnerData.WritedMovies).ClearLookup(moment);
		}
		public EntityCollection<MovieReview> MovieReviews { get { return InnerData.MovieReviews; } }
		public EntityCollection<MovieRole> MovieRoles { get { return InnerData.MovieRoles; } }
		public EntityCollection<Person> FollowedPersons { get { return InnerData.FollowedPersons; } }
		private void ClearFollowedPersons(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.FollowedPersons).ClearLookup(moment);
		}
		public EntityCollection<Person> Followers { get { return InnerData.Followers; } }
		private void ClearFollowers(DateTime? moment)
		{
			((ILookupHelper<Person>)InnerData.Followers).ClearLookup(moment);
		}

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
				if (members is null)
				{
					lock (typeof(Person))
					{
						if (members is null)
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

			public Property name { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["name"];
			public Property born { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["born"];
			public Property Uid { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["Uid"];
			public Property ActedMovies { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["ActedMovies"];
			public Property DirectedMovies { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["DirectedMovies"];
			public Property ProducedMovies { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["ProducedMovies"];
			public Property WritedMovies { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["WritedMovies"];
			public Property MovieReviews { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["MovieReviews"];
			public Property MovieRoles { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["MovieRoles"];
			public Property FollowedPersons { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["FollowedPersons"];
			public Property Followers { get; } = MovieGraph.Model.Datastore.Model.Entities["Person"].Properties["Followers"];
			#endregion

		}

		private static PersonFullTextMembers fullTextMembers = null;
		public static PersonFullTextMembers FullTextMembers
		{
			get
			{
				if (fullTextMembers is null)
				{
					lock (typeof(Person))
					{
						if (fullTextMembers is null)
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
			if (entity is null)
			{
				lock (typeof(Person))
				{
					if (entity is null)
						entity = MovieGraph.Model.Datastore.Model.Entities["Person"];
				}
			}
			return entity;
		}

		private static PersonEvents events = null;
		public static PersonEvents Events
		{
			get
			{
				if (events is null)
				{
					lock (typeof(Person))
					{
						if (events is null)
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
				EventHandler<Person, EntityEventArgs> handler = onNew;
				if (handler is not null)
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
				EventHandler<Person, EntityEventArgs> handler = onDelete;
				if (handler is not null)
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
				EventHandler<Person, EntityEventArgs> handler = onSave;
				if (handler is not null)
					handler.Invoke((Person)sender, args);
			}

			#endregion

			#region OnAfterSave

			private bool onAfterSaveIsRegistered = false;

			private EventHandler<Person, EntityEventArgs> onAfterSave;
			public event EventHandler<Person, EntityEventArgs> OnAfterSave
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
				EventHandler<Person, EntityEventArgs> handler = onAfterSave;
				if (handler is not null)
					handler.Invoke((Person)sender, args);
			}

			#endregion

			#region OnPropertyChange

			public static class OnPropertyChange
			{

				#region Onname

				private static bool onnameIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onname;
				public static event EventHandler<Person, PropertyEventArgs> Onname
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onnameIsRegistered)
							{
								Members.name.Events.OnChange -= onnameProxy;
								Members.name.Events.OnChange += onnameProxy;
								onnameIsRegistered = true;
							}
							onname += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onname -= value;
							if (onname is null && onnameIsRegistered)
							{
								Members.name.Events.OnChange -= onnameProxy;
								onnameIsRegistered = false;
							}
						}
					}
				}
			
				private static void onnameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onname;
					if (handler is not null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region Onborn

				private static bool onbornIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onborn;
				public static event EventHandler<Person, PropertyEventArgs> Onborn
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onbornIsRegistered)
							{
								Members.born.Events.OnChange -= onbornProxy;
								Members.born.Events.OnChange += onbornProxy;
								onbornIsRegistered = true;
							}
							onborn += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onborn -= value;
							if (onborn is null && onbornIsRegistered)
							{
								Members.born.Events.OnChange -= onbornProxy;
								onbornIsRegistered = false;
							}
						}
					}
				}
			
				private static void onbornProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onborn;
					if (handler is not null)
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
					EventHandler<Person, PropertyEventArgs> handler = onUid;
					if (handler is not null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnActedMovies

				private static bool onActedMoviesIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onActedMovies;
				public static event EventHandler<Person, PropertyEventArgs> OnActedMovies
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onActedMoviesIsRegistered)
							{
								Members.ActedMovies.Events.OnChange -= onActedMoviesProxy;
								Members.ActedMovies.Events.OnChange += onActedMoviesProxy;
								onActedMoviesIsRegistered = true;
							}
							onActedMovies += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onActedMovies -= value;
							if (onActedMovies is null && onActedMoviesIsRegistered)
							{
								Members.ActedMovies.Events.OnChange -= onActedMoviesProxy;
								onActedMoviesIsRegistered = false;
							}
						}
					}
				}
			
				private static void onActedMoviesProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onActedMovies;
					if (handler is not null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnDirectedMovies

				private static bool onDirectedMoviesIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onDirectedMovies;
				public static event EventHandler<Person, PropertyEventArgs> OnDirectedMovies
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDirectedMoviesIsRegistered)
							{
								Members.DirectedMovies.Events.OnChange -= onDirectedMoviesProxy;
								Members.DirectedMovies.Events.OnChange += onDirectedMoviesProxy;
								onDirectedMoviesIsRegistered = true;
							}
							onDirectedMovies += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDirectedMovies -= value;
							if (onDirectedMovies is null && onDirectedMoviesIsRegistered)
							{
								Members.DirectedMovies.Events.OnChange -= onDirectedMoviesProxy;
								onDirectedMoviesIsRegistered = false;
							}
						}
					}
				}
			
				private static void onDirectedMoviesProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onDirectedMovies;
					if (handler is not null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnProducedMovies

				private static bool onProducedMoviesIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onProducedMovies;
				public static event EventHandler<Person, PropertyEventArgs> OnProducedMovies
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProducedMoviesIsRegistered)
							{
								Members.ProducedMovies.Events.OnChange -= onProducedMoviesProxy;
								Members.ProducedMovies.Events.OnChange += onProducedMoviesProxy;
								onProducedMoviesIsRegistered = true;
							}
							onProducedMovies += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProducedMovies -= value;
							if (onProducedMovies is null && onProducedMoviesIsRegistered)
							{
								Members.ProducedMovies.Events.OnChange -= onProducedMoviesProxy;
								onProducedMoviesIsRegistered = false;
							}
						}
					}
				}
			
				private static void onProducedMoviesProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onProducedMovies;
					if (handler is not null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnWritedMovies

				private static bool onWritedMoviesIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onWritedMovies;
				public static event EventHandler<Person, PropertyEventArgs> OnWritedMovies
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onWritedMoviesIsRegistered)
							{
								Members.WritedMovies.Events.OnChange -= onWritedMoviesProxy;
								Members.WritedMovies.Events.OnChange += onWritedMoviesProxy;
								onWritedMoviesIsRegistered = true;
							}
							onWritedMovies += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onWritedMovies -= value;
							if (onWritedMovies is null && onWritedMoviesIsRegistered)
							{
								Members.WritedMovies.Events.OnChange -= onWritedMoviesProxy;
								onWritedMoviesIsRegistered = false;
							}
						}
					}
				}
			
				private static void onWritedMoviesProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onWritedMovies;
					if (handler is not null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnMovieReviews

				private static bool onMovieReviewsIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onMovieReviews;
				public static event EventHandler<Person, PropertyEventArgs> OnMovieReviews
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onMovieReviewsIsRegistered)
							{
								Members.MovieReviews.Events.OnChange -= onMovieReviewsProxy;
								Members.MovieReviews.Events.OnChange += onMovieReviewsProxy;
								onMovieReviewsIsRegistered = true;
							}
							onMovieReviews += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onMovieReviews -= value;
							if (onMovieReviews is null && onMovieReviewsIsRegistered)
							{
								Members.MovieReviews.Events.OnChange -= onMovieReviewsProxy;
								onMovieReviewsIsRegistered = false;
							}
						}
					}
				}
			
				private static void onMovieReviewsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onMovieReviews;
					if (handler is not null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnMovieRoles

				private static bool onMovieRolesIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onMovieRoles;
				public static event EventHandler<Person, PropertyEventArgs> OnMovieRoles
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onMovieRolesIsRegistered)
							{
								Members.MovieRoles.Events.OnChange -= onMovieRolesProxy;
								Members.MovieRoles.Events.OnChange += onMovieRolesProxy;
								onMovieRolesIsRegistered = true;
							}
							onMovieRoles += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onMovieRoles -= value;
							if (onMovieRoles is null && onMovieRolesIsRegistered)
							{
								Members.MovieRoles.Events.OnChange -= onMovieRolesProxy;
								onMovieRolesIsRegistered = false;
							}
						}
					}
				}
			
				private static void onMovieRolesProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onMovieRoles;
					if (handler is not null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnFollowedPersons

				private static bool onFollowedPersonsIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onFollowedPersons;
				public static event EventHandler<Person, PropertyEventArgs> OnFollowedPersons
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFollowedPersonsIsRegistered)
							{
								Members.FollowedPersons.Events.OnChange -= onFollowedPersonsProxy;
								Members.FollowedPersons.Events.OnChange += onFollowedPersonsProxy;
								onFollowedPersonsIsRegistered = true;
							}
							onFollowedPersons += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFollowedPersons -= value;
							if (onFollowedPersons is null && onFollowedPersonsIsRegistered)
							{
								Members.FollowedPersons.Events.OnChange -= onFollowedPersonsProxy;
								onFollowedPersonsIsRegistered = false;
							}
						}
					}
				}
			
				private static void onFollowedPersonsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onFollowedPersons;
					if (handler is not null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnFollowers

				private static bool onFollowersIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onFollowers;
				public static event EventHandler<Person, PropertyEventArgs> OnFollowers
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFollowersIsRegistered)
							{
								Members.Followers.Events.OnChange -= onFollowersProxy;
								Members.Followers.Events.OnChange += onFollowersProxy;
								onFollowersIsRegistered = true;
							}
							onFollowers += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFollowers -= value;
							if (onFollowers is null && onFollowersIsRegistered)
							{
								Members.Followers.Events.OnChange -= onFollowersProxy;
								onFollowersIsRegistered = false;
							}
						}
					}
				}
			
				private static void onFollowersProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Person, PropertyEventArgs> handler = onFollowers;
					if (handler is not null)
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

		string IPersonOriginalData.name { get { return OriginalData.name; } }
		int? IPersonOriginalData.born { get { return OriginalData.born; } }
		string IPersonOriginalData.Uid { get { return OriginalData.Uid; } }
		IEnumerable<Movie> IPersonOriginalData.ActedMovies { get { return OriginalData.ActedMovies.OriginalData; } }
		IEnumerable<Movie> IPersonOriginalData.DirectedMovies { get { return OriginalData.DirectedMovies.OriginalData; } }
		IEnumerable<Movie> IPersonOriginalData.ProducedMovies { get { return OriginalData.ProducedMovies.OriginalData; } }
		IEnumerable<Movie> IPersonOriginalData.WritedMovies { get { return OriginalData.WritedMovies.OriginalData; } }
		IEnumerable<MovieReview> IPersonOriginalData.MovieReviews { get { return OriginalData.MovieReviews.OriginalData; } }
		IEnumerable<MovieRole> IPersonOriginalData.MovieRoles { get { return OriginalData.MovieRoles.OriginalData; } }
		IEnumerable<Person> IPersonOriginalData.FollowedPersons { get { return OriginalData.FollowedPersons.OriginalData; } }
		IEnumerable<Person> IPersonOriginalData.Followers { get { return OriginalData.Followers.OriginalData; } }

		#endregion
		#endregion
	}
}