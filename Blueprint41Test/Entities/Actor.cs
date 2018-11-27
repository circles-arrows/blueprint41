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
	public interface IActorOriginalData
    {
		string Uid { get; }
		string fullname { get; }
		IEnumerable<Film> ActedFilms { get; }
		IEnumerable<Film> DirectedFilms { get; }
		IEnumerable<Film> ProducedFilms { get; }
		IEnumerable<Film> FilmsWrited { get; }
    }

	public partial class Actor : OGM<Actor, Actor.ActorData, System.String>, IActorOriginalData
	{
        #region Initialize

        static Actor()
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
		public static Actor LoadByUid(string uid)
		{
			return FromQuery(nameof(LoadByUid), new Parameter(Param0, uid)).FirstOrDefault();
		}
        partial void AdditionalGeneratedStoredQueries();

        public static Dictionary<System.String, Actor> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.ActorAlias, IWhereQuery> query)
        {
            q.ActorAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Actor.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Actor => Uid : {this.Uid}, fullname : {this.fullname?.ToString() ?? "null"}";
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
                    OriginalData = new ActorData(InnerData);
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

		public class ActorData : Data<System.String>
		{
			public ActorData()
            {

            }

            public ActorData(ActorData data)
            {
				Uid = data.Uid;
				fullname = data.fullname;
				ActedFilms = data.ActedFilms;
				DirectedFilms = data.DirectedFilms;
				ProducedFilms = data.ProducedFilms;
				FilmsWrited = data.FilmsWrited;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Actor";

				ActedFilms = new EntityCollection<Film>(Wrapper, Members.ActedFilms, item => { if (Members.ActedFilms.Events.HasRegisteredChangeHandlers) { int loadHack = item.Actors.Count; } });
				DirectedFilms = new EntityCollection<Film>(Wrapper, Members.DirectedFilms, item => { if (Members.DirectedFilms.Events.HasRegisteredChangeHandlers) { int loadHack = item.Directors.Count; } });
				ProducedFilms = new EntityCollection<Film>(Wrapper, Members.ProducedFilms, item => { if (Members.ProducedFilms.Events.HasRegisteredChangeHandlers) { int loadHack = item.Producers.Count; } });
				FilmsWrited = new EntityCollection<Film>(Wrapper, Members.FilmsWrited, item => { if (Members.FilmsWrited.Events.HasRegisteredChangeHandlers) { int loadHack = item.Writers.Count; } });
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
				dictionary.Add("fullname",  fullname);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
				if (properties.TryGetValue("fullname", out value))
					fullname = (string)value;
			}

			#endregion

			#region Members for interface IActor

			public string Uid { get; set; }
			public string fullname { get; set; }
			public EntityCollection<Film> ActedFilms { get; private set; }
			public EntityCollection<Film> DirectedFilms { get; private set; }
			public EntityCollection<Film> ProducedFilms { get; private set; }
			public EntityCollection<Film> FilmsWrited { get; private set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IActor

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }
		public string fullname { get { LazyGet(); return InnerData.fullname; } set { if (LazySet(Members.fullname, InnerData.fullname, value)) InnerData.fullname = value; } }
		public EntityCollection<Film> ActedFilms { get { return InnerData.ActedFilms; } }
		private void ClearActedFilms(DateTime? moment)
		{
			((ILookupHelper<Film>)InnerData.ActedFilms).ClearLookup(moment);
		}
		public EntityCollection<Film> DirectedFilms { get { return InnerData.DirectedFilms; } }
		private void ClearDirectedFilms(DateTime? moment)
		{
			((ILookupHelper<Film>)InnerData.DirectedFilms).ClearLookup(moment);
		}
		public EntityCollection<Film> ProducedFilms { get { return InnerData.ProducedFilms; } }
		private void ClearProducedFilms(DateTime? moment)
		{
			((ILookupHelper<Film>)InnerData.ProducedFilms).ClearLookup(moment);
		}
		public EntityCollection<Film> FilmsWrited { get { return InnerData.FilmsWrited; } }
		private void ClearFilmsWrited(DateTime? moment)
		{
			((ILookupHelper<Film>)InnerData.FilmsWrited).ClearLookup(moment);
		}

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

        private static ActorMembers members = null;
        public static ActorMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Actor))
                    {
                        if (members == null)
                            members = new ActorMembers();
                    }
                }
                return members;
            }
        }
        public class ActorMembers
        {
            internal ActorMembers() { }

			#region Members for interface IActor

            public Property Uid { get; } = Blueprint41Test.MovieModel.Model.Entities["Actor"].Properties["Uid"];
            public Property fullname { get; } = Blueprint41Test.MovieModel.Model.Entities["Actor"].Properties["fullname"];
            public Property ActedFilms { get; } = Blueprint41Test.MovieModel.Model.Entities["Actor"].Properties["ActedFilms"];
            public Property DirectedFilms { get; } = Blueprint41Test.MovieModel.Model.Entities["Actor"].Properties["DirectedFilms"];
            public Property ProducedFilms { get; } = Blueprint41Test.MovieModel.Model.Entities["Actor"].Properties["ProducedFilms"];
            public Property FilmsWrited { get; } = Blueprint41Test.MovieModel.Model.Entities["Actor"].Properties["FilmsWrited"];
			#endregion

        }

        private static ActorFullTextMembers fullTextMembers = null;
        public static ActorFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Actor))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new ActorFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class ActorFullTextMembers
        {
            internal ActorFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Actor))
                {
                    if (entity == null)
                        entity = Blueprint41Test.MovieModel.Model.Entities["Actor"];
                }
            }
            return entity;
        }

		private static ActorEvents events = null;
        public static ActorEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Actor))
                    {
                        if (events == null)
                            events = new ActorEvents();
                    }
                }
                return events;
            }
        }
        public class ActorEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Actor, EntityEventArgs> onNew;
            public event EventHandler<Actor, EntityEventArgs> OnNew
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
                EventHandler<Actor, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Actor)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Actor, EntityEventArgs> onDelete;
            public event EventHandler<Actor, EntityEventArgs> OnDelete
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
                EventHandler<Actor, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Actor)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Actor, EntityEventArgs> onSave;
            public event EventHandler<Actor, EntityEventArgs> OnSave
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
                EventHandler<Actor, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Actor)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Actor, PropertyEventArgs> onUid;
				public static event EventHandler<Actor, PropertyEventArgs> OnUid
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
					EventHandler<Actor, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Actor)sender, args);
				}

				#endregion

				#region Onfullname

				private static bool onfullnameIsRegistered = false;

				private static EventHandler<Actor, PropertyEventArgs> onfullname;
				public static event EventHandler<Actor, PropertyEventArgs> Onfullname
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onfullnameIsRegistered)
							{
								Members.fullname.Events.OnChange -= onfullnameProxy;
								Members.fullname.Events.OnChange += onfullnameProxy;
								onfullnameIsRegistered = true;
							}
							onfullname += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onfullname -= value;
							if (onfullname == null && onfullnameIsRegistered)
							{
								Members.fullname.Events.OnChange -= onfullnameProxy;
								onfullnameIsRegistered = false;
							}
						}
					}
				}
            
				private static void onfullnameProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Actor, PropertyEventArgs> handler = onfullname;
					if ((object)handler != null)
						handler.Invoke((Actor)sender, args);
				}

				#endregion

				#region OnActedFilms

				private static bool onActedFilmsIsRegistered = false;

				private static EventHandler<Actor, PropertyEventArgs> onActedFilms;
				public static event EventHandler<Actor, PropertyEventArgs> OnActedFilms
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onActedFilmsIsRegistered)
							{
								Members.ActedFilms.Events.OnChange -= onActedFilmsProxy;
								Members.ActedFilms.Events.OnChange += onActedFilmsProxy;
								onActedFilmsIsRegistered = true;
							}
							onActedFilms += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onActedFilms -= value;
							if (onActedFilms == null && onActedFilmsIsRegistered)
							{
								Members.ActedFilms.Events.OnChange -= onActedFilmsProxy;
								onActedFilmsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onActedFilmsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Actor, PropertyEventArgs> handler = onActedFilms;
					if ((object)handler != null)
						handler.Invoke((Actor)sender, args);
				}

				#endregion

				#region OnDirectedFilms

				private static bool onDirectedFilmsIsRegistered = false;

				private static EventHandler<Actor, PropertyEventArgs> onDirectedFilms;
				public static event EventHandler<Actor, PropertyEventArgs> OnDirectedFilms
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDirectedFilmsIsRegistered)
							{
								Members.DirectedFilms.Events.OnChange -= onDirectedFilmsProxy;
								Members.DirectedFilms.Events.OnChange += onDirectedFilmsProxy;
								onDirectedFilmsIsRegistered = true;
							}
							onDirectedFilms += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDirectedFilms -= value;
							if (onDirectedFilms == null && onDirectedFilmsIsRegistered)
							{
								Members.DirectedFilms.Events.OnChange -= onDirectedFilmsProxy;
								onDirectedFilmsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDirectedFilmsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Actor, PropertyEventArgs> handler = onDirectedFilms;
					if ((object)handler != null)
						handler.Invoke((Actor)sender, args);
				}

				#endregion

				#region OnProducedFilms

				private static bool onProducedFilmsIsRegistered = false;

				private static EventHandler<Actor, PropertyEventArgs> onProducedFilms;
				public static event EventHandler<Actor, PropertyEventArgs> OnProducedFilms
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onProducedFilmsIsRegistered)
							{
								Members.ProducedFilms.Events.OnChange -= onProducedFilmsProxy;
								Members.ProducedFilms.Events.OnChange += onProducedFilmsProxy;
								onProducedFilmsIsRegistered = true;
							}
							onProducedFilms += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onProducedFilms -= value;
							if (onProducedFilms == null && onProducedFilmsIsRegistered)
							{
								Members.ProducedFilms.Events.OnChange -= onProducedFilmsProxy;
								onProducedFilmsIsRegistered = false;
							}
						}
					}
				}
            
				private static void onProducedFilmsProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Actor, PropertyEventArgs> handler = onProducedFilms;
					if ((object)handler != null)
						handler.Invoke((Actor)sender, args);
				}

				#endregion

				#region OnFilmsWrited

				private static bool onFilmsWritedIsRegistered = false;

				private static EventHandler<Actor, PropertyEventArgs> onFilmsWrited;
				public static event EventHandler<Actor, PropertyEventArgs> OnFilmsWrited
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onFilmsWritedIsRegistered)
							{
								Members.FilmsWrited.Events.OnChange -= onFilmsWritedProxy;
								Members.FilmsWrited.Events.OnChange += onFilmsWritedProxy;
								onFilmsWritedIsRegistered = true;
							}
							onFilmsWrited += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onFilmsWrited -= value;
							if (onFilmsWrited == null && onFilmsWritedIsRegistered)
							{
								Members.FilmsWrited.Events.OnChange -= onFilmsWritedProxy;
								onFilmsWritedIsRegistered = false;
							}
						}
					}
				}
            
				private static void onFilmsWritedProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Actor, PropertyEventArgs> handler = onFilmsWrited;
					if ((object)handler != null)
						handler.Invoke((Actor)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IActorOriginalData

		public IActorOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IActor

		string IActorOriginalData.Uid { get { return OriginalData.Uid; } }
		string IActorOriginalData.fullname { get { return OriginalData.fullname; } }
		IEnumerable<Film> IActorOriginalData.ActedFilms { get { return OriginalData.ActedFilms.OriginalData; } }
		IEnumerable<Film> IActorOriginalData.DirectedFilms { get { return OriginalData.DirectedFilms.OriginalData; } }
		IEnumerable<Film> IActorOriginalData.ProducedFilms { get { return OriginalData.ProducedFilms.OriginalData; } }
		IEnumerable<Film> IActorOriginalData.FilmsWrited { get { return OriginalData.FilmsWrited.OriginalData; } }

		#endregion
		#endregion
	}
}