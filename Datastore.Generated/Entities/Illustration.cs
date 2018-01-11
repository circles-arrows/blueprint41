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
	public interface IIllustrationOriginalData
    {
		#region Outer Data

		#region Members for interface IIllustration

		string Diagram { get; }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime ModifiedDate { get; }

		#endregion
		#region Members for interface INeo4jBase

		string Uid { get; }

		#endregion
		#endregion
    }

	public partial class Illustration : OGM<Illustration, Illustration.IllustrationData, System.String>, ISchemaBase, INeo4jBase, IIllustrationOriginalData
	{
        #region Initialize

        static Illustration()
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

        public static Dictionary<System.String, Illustration> LoadByKeys(IEnumerable<System.String> uids)
        {
            return FromQuery(nameof(LoadByKeys), new Parameter(Param0, uids.ToArray(), typeof(System.String))).ToDictionary(item=> item.Uid, item => item);
        }

		protected static void RegisterQuery(string name, Func<IMatchQuery, q.IllustrationAlias, IWhereQuery> query)
        {
            q.IllustrationAlias alias;

            IMatchQuery matchQuery = Blueprint41.Transaction.CompiledQuery.Match(q.Node.Illustration.Alias(out alias));
            IWhereQuery partial = query.Invoke(matchQuery, alias);
            ICompiled compiled = partial.Return(alias).Compile();

			RegisterQuery(name, compiled);
        }

		public override string ToString()
        {
            return $"Illustration => Diagram : {this.Diagram}, ModifiedDate : {this.ModifiedDate}, Uid : {this.Uid}";
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
                    OriginalData = new IllustrationData(InnerData);
            }
        }


        #endregion

		#region Validations

		protected override void ValidateSave()
		{
            bool isUpdate = (PersistenceState != PersistenceState.New && PersistenceState != PersistenceState.NewAndChanged);

#pragma warning disable CS0472
			if (InnerData.Diagram == null)
				throw new PersistenceException(string.Format("Cannot save Illustration with key '{0}' because the Diagram cannot be null.", this.Uid?.ToString() ?? "<null>"));
			if (InnerData.ModifiedDate == null)
				throw new PersistenceException(string.Format("Cannot save Illustration with key '{0}' because the ModifiedDate cannot be null.", this.Uid?.ToString() ?? "<null>"));
#pragma warning restore CS0472
		}

		protected override void ValidateDelete()
		{
		}

		#endregion

		#region Inner Data

		public class IllustrationData : Data<System.String>
		{
			public IllustrationData()
            {

            }

            public IllustrationData(IllustrationData data)
            {
				Diagram = data.Diagram;
				ModifiedDate = data.ModifiedDate;
				Uid = data.Uid;
            }


            #region Initialize Collections

			protected override void InitializeCollections()
			{
				NodeType = "Illustration";

			}
			public string NodeType { get; private set; }
			sealed public override System.String GetKey() { return Blueprint41.Transaction.Current.ConvertFromStoredType<System.String>(Uid); }
			sealed protected override void SetKey(System.String key) { Uid = (string)Blueprint41.Transaction.Current.ConvertToStoredType<System.String>(key); base.SetKey(Uid); }

			#endregion
			#region Map Data

			sealed public override IDictionary<string, object> MapTo()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("Diagram",  Diagram);
				dictionary.Add("ModifiedDate",  Conversion<System.DateTime, long>.Convert(ModifiedDate));
				dictionary.Add("Uid",  Uid);
				return dictionary;
			}

			sealed public override void MapFrom(IReadOnlyDictionary<string, object> properties)
			{
				object value;
				if (properties.TryGetValue("Diagram", out value))
					Diagram = (string)value;
				if (properties.TryGetValue("ModifiedDate", out value))
					ModifiedDate = Conversion<long, System.DateTime>.Convert((long)value);
				if (properties.TryGetValue("Uid", out value))
					Uid = (string)value;
			}

			#endregion

			#region Members for interface IIllustration

			public string Diagram { get; set; }

			#endregion
			#region Members for interface ISchemaBase

			public System.DateTime ModifiedDate { get; set; }

			#endregion
			#region Members for interface INeo4jBase

			public string Uid { get; set; }

			#endregion
		}

		#endregion

		#region Outer Data

		#region Members for interface IIllustration

		public string Diagram { get { LazyGet(); return InnerData.Diagram; } set { if (LazySet(Members.Diagram, InnerData.Diagram, value)) InnerData.Diagram = value; } }

		#endregion
		#region Members for interface ISchemaBase

		public System.DateTime ModifiedDate { get { LazyGet(); return InnerData.ModifiedDate; } set { if (LazySet(Members.ModifiedDate, InnerData.ModifiedDate, value)) InnerData.ModifiedDate = value; } }

		#endregion
		#region Members for interface INeo4jBase

		public string Uid { get { return InnerData.Uid; } set { KeySet(() => InnerData.Uid = value); } }

		#endregion

		#region Virtual Node Type
		
		public string NodeType  { get { return InnerData.NodeType; } }
		
		#endregion

		#endregion

		#region Reflection

        private static IllustrationMembers members = null;
        public static IllustrationMembers Members
        {
            get
            {
                if (members == null)
                {
                    lock (typeof(Illustration))
                    {
                        if (members == null)
                            members = new IllustrationMembers();
                    }
                }
                return members;
            }
        }
        public class IllustrationMembers
        {
            internal IllustrationMembers() { }

			#region Members for interface IIllustration

            public Property Diagram { get; } = Datastore.AdventureWorks.Model.Entities["Illustration"].Properties["Diagram"];
			#endregion

			#region Members for interface ISchemaBase

            public Property ModifiedDate { get; } = Datastore.AdventureWorks.Model.Entities["SchemaBase"].Properties["ModifiedDate"];
			#endregion

			#region Members for interface INeo4jBase

            public Property Uid { get; } = Datastore.AdventureWorks.Model.Entities["Neo4jBase"].Properties["Uid"];
			#endregion

        }

        private static IllustrationFullTextMembers fullTextMembers = null;
        public static IllustrationFullTextMembers FullTextMembers
        {
            get
            {
                if (fullTextMembers == null)
                {
                    lock (typeof(Illustration))
                    {
                        if (fullTextMembers == null)
                            fullTextMembers = new IllustrationFullTextMembers();
                    }
                }
                return fullTextMembers;
            }
        }

        public class IllustrationFullTextMembers
        {
            internal IllustrationFullTextMembers() { }

        }

		sealed public override Entity GetEntity()
        {
            if (entity == null)
            {
                lock (typeof(Illustration))
                {
                    if (entity == null)
                        entity = Datastore.AdventureWorks.Model.Entities["Illustration"];
                }
            }
            return entity;
        }

		private static IllustrationEvents events = null;
        public static IllustrationEvents Events
        {
            get
            {
                if (events == null)
                {
                    lock (typeof(Illustration))
                    {
                        if (events == null)
                            events = new IllustrationEvents();
                    }
                }
                return events;
            }
        }
        public class IllustrationEvents
        {

            #region OnNew

            private bool onNewIsRegistered = false;

            private EventHandler<Illustration, EntityEventArgs> onNew;
            public event EventHandler<Illustration, EntityEventArgs> OnNew
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
                EventHandler<Illustration, EntityEventArgs> handler = onNew;
                if ((object)handler != null)
                    handler.Invoke((Illustration)sender, args);
            }

            #endregion

            #region OnDelete

            private bool onDeleteIsRegistered = false;

            private EventHandler<Illustration, EntityEventArgs> onDelete;
            public event EventHandler<Illustration, EntityEventArgs> OnDelete
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
                EventHandler<Illustration, EntityEventArgs> handler = onDelete;
                if ((object)handler != null)
                    handler.Invoke((Illustration)sender, args);
            }

            #endregion

            #region OnSave

            private bool onSaveIsRegistered = false;

            private EventHandler<Illustration, EntityEventArgs> onSave;
            public event EventHandler<Illustration, EntityEventArgs> OnSave
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
                EventHandler<Illustration, EntityEventArgs> handler = onSave;
                if ((object)handler != null)
                    handler.Invoke((Illustration)sender, args);
            }

            #endregion

            #region OnPropertyChange

            public static class OnPropertyChange
            {

				#region OnDiagram

				private static bool onDiagramIsRegistered = false;

				private static EventHandler<Illustration, PropertyEventArgs> onDiagram;
				public static event EventHandler<Illustration, PropertyEventArgs> OnDiagram
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onDiagramIsRegistered)
							{
								Members.Diagram.Events.OnChange -= onDiagramProxy;
								Members.Diagram.Events.OnChange += onDiagramProxy;
								onDiagramIsRegistered = true;
							}
							onDiagram += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onDiagram -= value;
							if (onDiagram == null && onDiagramIsRegistered)
							{
								Members.Diagram.Events.OnChange -= onDiagramProxy;
								onDiagramIsRegistered = false;
							}
						}
					}
				}
            
				private static void onDiagramProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Illustration, PropertyEventArgs> handler = onDiagram;
					if ((object)handler != null)
						handler.Invoke((Illustration)sender, args);
				}

				#endregion

				#region OnModifiedDate

				private static bool onModifiedDateIsRegistered = false;

				private static EventHandler<Illustration, PropertyEventArgs> onModifiedDate;
				public static event EventHandler<Illustration, PropertyEventArgs> OnModifiedDate
				{
					add
					{
						lock (typeof(OnPropertyChange))
						{
							if (!onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								Members.ModifiedDate.Events.OnChange += onModifiedDateProxy;
								onModifiedDateIsRegistered = true;
							}
							onModifiedDate += value;
						}
					}
					remove
					{
						lock (typeof(OnPropertyChange))
						{
							onModifiedDate -= value;
							if (onModifiedDate == null && onModifiedDateIsRegistered)
							{
								Members.ModifiedDate.Events.OnChange -= onModifiedDateProxy;
								onModifiedDateIsRegistered = false;
							}
						}
					}
				}
            
				private static void onModifiedDateProxy(object sender, PropertyEventArgs args)
				{
					EventHandler<Illustration, PropertyEventArgs> handler = onModifiedDate;
					if ((object)handler != null)
						handler.Invoke((Illustration)sender, args);
				}

				#endregion

				#region OnUid

				private static bool onUidIsRegistered = false;

				private static EventHandler<Illustration, PropertyEventArgs> onUid;
				public static event EventHandler<Illustration, PropertyEventArgs> OnUid
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
					EventHandler<Illustration, PropertyEventArgs> handler = onUid;
					if ((object)handler != null)
						handler.Invoke((Illustration)sender, args);
				}

				#endregion

			}

			#endregion
        }

        #endregion

		#region IIllustrationOriginalData

		public IIllustrationOriginalData OriginalVersion { get { return this; } }

		#region Members for interface IIllustration

		string IIllustrationOriginalData.Diagram { get { return OriginalData.Diagram; } }

		#endregion
		#region Members for interface ISchemaBase

		System.DateTime IIllustrationOriginalData.ModifiedDate { get { return OriginalData.ModifiedDate; } }

		#endregion
		#region Members for interface INeo4jBase

		string IIllustrationOriginalData.Uid { get { return OriginalData.Uid; } }

		#endregion
		#endregion
	}
}