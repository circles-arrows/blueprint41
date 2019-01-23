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
	public interface IPersonOriginalData : IBaseOriginalData
    {
		string Name { get; }
		IEnumerable<Film> ActedFilms { get; }
		IEnumerable<Film> DirectedFilms { get; }
		IEnumerable<Film> ProducedFilms { get; }
		IEnumerable<Film> FilmsWrited { get; }
    }

	public partial class Person : OGM<Person, Person.PersonData, System.String>, IBase, IPersonOriginalData
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
            return $"Person => Name : {this.Name}, Uid : {this.Uid}, LastModifiedOn : {this.LastModifiedOn}";
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
			if (InnerData.Name == null)
				throw new PersistenceException(string.Format("Cannot save Person with key '{0}' because the Name cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.Uid == null)
				throw new PersistenceException(string.Format("Cannot save Person with key '{0}' because the Uid cannot be null.", this.Uid?.ToString() ?? "<null>"));
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
				Name = data.Name;
				ActedFilms = data.ActedFilms;
				DirectedFilms = data.DirectedFilms;
				ProducedFilms = data.ProducedFilms;
				FilmsWrited = data.FilmsWrited;
				Uid = data.Uid;
				LastModifiedOn = data.LastModifiedOn;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Person";

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

			#region Members for interface IPerson

			public string Name { get; set; }
			public EntityCollection<Film> ActedFilms { get; private set; }
			public EntityCollection<Film> DirectedFilms { get; private set; }
			public EntityCollection<Film> ProducedFilms { get; private set; }
			public EntityCollection<Film> FilmsWrited { get; private set; }

			#endregion
			#region Members for interface IBase

			public string Uid { get; set; }
			public System.DateTime LastModifiedOn { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IPerson

		public string Name { get { LazyGet(); return InnerData.Name; } set { if (LazySet(Members.Name, InnerData.Name, value)) InnerData.Name = value; } }
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

            public Property Name { get; } = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"].Properties["Name"];
            public Property ActedFilms { get; } = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"].Properties["ActedFilms"];
            public Property DirectedFilms { get; } = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"].Properties["DirectedFilms"];
            public Property ProducedFilms { get; } = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"].Properties["ProducedFilms"];
            public Property FilmsWrited { get; } = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"].Properties["FilmsWrited"];
			#endregion

			#region Members for interface IBase

            public Property Uid { get; } = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"].Properties["Uid"];
            public Property LastModifiedOn { get; } = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Base"].Properties["LastModifiedOn"];
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
                        entity = Blueprint41.GremlinUnitTest.GremlinStoreCRUD.Model.Entities["Person"];
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

				#region OnName

				private static bool onNameIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onName;
				public static event EventHandler<Person, PropertyEventArgs> OnName
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
					EventHandler<Person, PropertyEventArgs> handler = onName;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnActedFilms

				private static bool onActedFilmsIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onActedFilms;
				public static event EventHandler<Person, PropertyEventArgs> OnActedFilms
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
					EventHandler<Person, PropertyEventArgs> handler = onActedFilms;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnDirectedFilms

				private static bool onDirectedFilmsIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onDirectedFilms;
				public static event EventHandler<Person, PropertyEventArgs> OnDirectedFilms
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
					EventHandler<Person, PropertyEventArgs> handler = onDirectedFilms;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnProducedFilms

				private static bool onProducedFilmsIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onProducedFilms;
				public static event EventHandler<Person, PropertyEventArgs> OnProducedFilms
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
					EventHandler<Person, PropertyEventArgs> handler = onProducedFilms;
					if ((object)handler != null)
						handler.Invoke((Person)sender, args);
				}

				#endregion

				#region OnFilmsWrited

				private static bool onFilmsWritedIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onFilmsWrited;
				public static event EventHandler<Person, PropertyEventArgs> OnFilmsWrited
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
					EventHandler<Person, PropertyEventArgs> handler = onFilmsWrited;
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

				#region OnLastModifiedOn

				private static bool onLastModifiedOnIsRegistered = false;

				private static EventHandler<Person, PropertyEventArgs> onLastModifiedOn;
				public static event EventHandler<Person, PropertyEventArgs> OnLastModifiedOn
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
					EventHandler<Person, PropertyEventArgs> handler = onLastModifiedOn;
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

		string IPersonOriginalData.Name { get { return OriginalData.Name; } }
		IEnumerable<Film> IPersonOriginalData.ActedFilms { get { return OriginalData.ActedFilms.OriginalData; } }
		IEnumerable<Film> IPersonOriginalData.DirectedFilms { get { return OriginalData.DirectedFilms.OriginalData; } }
		IEnumerable<Film> IPersonOriginalData.ProducedFilms { get { return OriginalData.ProducedFilms.OriginalData; } }
		IEnumerable<Film> IPersonOriginalData.FilmsWrited { get { return OriginalData.FilmsWrited.OriginalData; } }

		#endregion
		#region Members for interface IBase

		IBaseOriginalData IBase.OriginalVersion { get { return this; } }

		string IBaseOriginalData.Uid { get { return OriginalData.Uid; } }
		System.DateTime IBaseOriginalData.LastModifiedOn { get { return OriginalData.LastModifiedOn; } }

		#endregion
		#endregion
	}
}