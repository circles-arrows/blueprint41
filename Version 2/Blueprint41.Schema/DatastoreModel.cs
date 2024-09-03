using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Diagnostics;

using Force.Crc32;

using Blueprint41.Core;
using Blueprint41.Driver;
using Blueprint41.Refactoring;
using model = Blueprint41.Model;
using Blueprint41.Refactoring.Schema;
using Blueprint41.Persistence;

namespace Blueprint41
{
    public abstract partial class DatastoreModel : IRefactorGlobal, IDatastoreUnitTesting
    {
        protected DatastoreModel()
        {
            Entities = new EntityCollection(this);
            Relations = new RelationshipCollection(this);
            Interfaces = new InterfaceCollection(this);
            Enumerations = new EnumerationCollection(this);
            FunctionalIds = new FunctionalIdCollection(this);
            SubModels = new SubModelCollection(this);

            Labels = new model.LabelCollection();
            RelationshipTypes = new model.RelationshipTypeCollection();

            DataMigration = new DataMigrationScope(this);
        }

        /// <summary>
        /// The persistence provider registered with this data-store
        /// </summary>
        public PersistenceProvider PersistenceProvider
        {
            get
            {
                if (_persistenceProvider is null)
                    _persistenceProvider = new PersistenceProvider(this, null, null, null,null);

                return _persistenceProvider;
            }
        }
        internal protected PersistenceProvider? _persistenceProvider = null;

        public abstract GDMS DatastoreTechnology { get; }

        /// <summary>
        /// All entities in the data-store
        /// </summary>
        public EntityCollection       Entities      { get; private set; }

        /// <summary>
        /// All relationships in the data-store
        /// </summary>
        public RelationshipCollection Relations     { get; private set; }

        /// <summary>
        /// All interfaces in the data-store
        /// </summary>
        public InterfaceCollection    Interfaces    { get; private set; }

        /// <summary>
        /// All enumerations in the data-store
        /// </summary>
        public EnumerationCollection  Enumerations  { get; private set; }

        /// <summary>
        /// All functional ids in the data-store
        /// </summary>
        public FunctionalIdCollection FunctionalIds { get; private set; }

        /// <summary>
        /// All sub-models in the data-store
        /// </summary>
        public SubModelCollection     SubModels     { get; private set; }

        /// <summary>
        /// All registered models
        /// </summary>
        public static List<DatastoreModel> RegisteredModels { get; } = new List<DatastoreModel>();

        /// <summary>
        /// True when all scripts have been executed
        /// </summary>
        public bool HasExecuted
        {
            get
            {
                return executed;
            }
        }

        /// <summary>
        /// True when the data-store has been upgraded
        /// </summary>
        public bool IsUpgraded
        {
            get
            {
                return executed && didUpgradeDataStore;
            }
        }

        /// <summary>
        /// True when the data-store is in data-migration mode
        /// </summary>
        public bool IsDataMigration
        {
            get
            {
                return datamigration;
            }
        }

        internal model.LabelCollection Labels { get; private set; }

        internal model.RelationshipTypeCollection RelationshipTypes { get; private set; }

        #region Execute

        private bool executed = false; // true when all scripts finished executing
        private bool didUpgradeDataStore = false; // true when "Execute(true)"
        private bool datamigration = false;

        internal List<UpgradeScript> GetUpgradeScripts(MethodInfo? unitTestScript)
        {
            List<UpgradeScript> scripts = new List<UpgradeScript>();

            foreach (MethodInfo info in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                VersionAttribute? attr = info.GetCustomAttribute<VersionAttribute>();
                if (attr is not null)
                {
                    Action method = (Action)Delegate.CreateDelegate(typeof(Action), this, info);
                    scripts.Add(new UpgradeScript(method, attr.Major, attr.Minor, attr.Patch, info.Name));
                }
            }
            scripts.Sort();

            UpgradeScript? prev = null;
            foreach (UpgradeScript item in scripts)
            {
                if (prev is not null && prev.Equals(item)) // don't use prev == item !!!!
                    throw new NotSupportedException($"There are two methods ({prev.Name} & {item.Name}) with the same script version ({item.Major}.{item.Minor}.{item.Patch}).");

                prev = item;
            }


            if (unitTestScript is not null)
            {
                UpgradeScript prevScript = scripts.LastOrDefault();
                long major = prevScript?.Major ?? 0;
                long minor = prevScript?.Minor ?? 0;
                long patch = prevScript?.Patch ?? 0;

                Action<DatastoreModel> method = (Action<DatastoreModel>)Delegate.CreateDelegate(typeof(Action<DatastoreModel>), null, unitTestScript);
                UpgradeScript injectedScript = new UpgradeScript(new Action(() => method(this)), major, minor, patch + 1, "InjectedUnitTestMethod");
                scripts.Add(injectedScript);
            }

            return scripts;
        }

        /// <summary>
        /// Execute the data-store
        /// </summary>
        /// <param name="upgradeDatastore">Whether or not the data-store should be upgraded</param>
        public void Execute(bool upgradeDatastore)
        {
            if (PersistenceProvider is null)
                throw new InvalidOperationException($"Instead of using 'new {GetType().Name}();' to get an instance of the data-store, please use the method '{GetType().Name}.Connect(persistenceProvider);' instead.");

            Execute(upgradeDatastore, null);
        }

        /// <summary>
        /// Execute the data-store and subsequently execute a unit-test script if provided
        /// </summary>
        /// <param name="upgradeDatastore">Whether or not the data-store should be upgraded</param>
        /// <param name="unitTestScript">The unit-test script</param>
        void IDatastoreUnitTesting.Execute(bool upgradeDatastore, MethodInfo? unitTestScript) => Execute(upgradeDatastore, unitTestScript);
        internal void Execute(bool upgradeDatastore, MethodInfo? unitTestScript)
        {
            if (PersistenceProvider is null)
                throw new InvalidOperationException($"Instead of using 'new {GetType().Name}();' to get an instance of the data-store, please use the method '{GetType().Name}.Connect(persistenceProvider);' instead.");

            if (isExecuting)
                throw new InvalidOperationException("It is not allowed to call the 'Execute' method from within an upgrade script.");

            try
            {
                isExecuting = true;
                didUpgradeDataStore = upgradeDatastore;

                DatastoreModel? model = null;
                lock (RegisteredModels)
                {
                    model = RegisteredModels.FirstOrDefault(item => item.GetType() == this.GetType());
                    if (model is null)
                    {
                        model = this;
                        RegisteredModels.Add(model);
                    }
                }

                Execute(upgradeDatastore, unitTestScript, item => true, true);

                if (executed)
                    model.executed = true;

                if (didUpgradeDataStore)
                    model.didUpgradeDataStore = true;
            }
            finally
            {
                isExecuting = false;
            }
        }
        private bool isExecuting = false;

        internal void Execute(bool upgradeDatastore, MethodInfo? unitTestScript, Predicate<UpgradeScript> predicate, bool standAloneScript)
        {
#pragma warning disable CS0618 // Type or member is obsolete

            if (executed && standAloneScript)
                throw new InvalidOperationException();

            List<UpgradeScript> scripts = GetUpgradeScripts(unitTestScript);

            bool anyScriptRan = false;
            foreach (UpgradeScript script in scripts.Where(item => predicate.Invoke(item)))
            {
                bool scriptCommitted = false;
                if (upgradeDatastore && !PersistenceProvider.IsVoidProvider)
                {
                    if (PersistenceProvider.IsMemgraph)
                    {
                        // In Memgraph constraints cannot be manipulated during transactions.
                        // If any refactor action conflicts with a constraint (e.g. Rename property with NOT NULL constraint).
                        // Memgraph will hang on a lock taken during removal of the constraint and the lock taken during renaming the property.
                        using (PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
                        {
                            string clearSchema = "CALL schema.assert({},{}, {}, true) YIELD label, key RETURN *";
                            Session.RunningSession.Run(clearSchema);
                        }
                    }

                    using (PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
                    {
                        if (!Parser.HasScript(script))
                        {
                            Parser.Log("Running script: {0}.{1}.{2} ({3})", script.Major, script.Minor, script.Patch, script.Name);
                            Stopwatch sw = Stopwatch.StartNew();

                            Refactor.ApplyFunctionalIds();
                            RunScriptChecked(script);
                            Refactor.ApplyFunctionalIds();

                            Parser.CommitScript(script);
                            anyScriptRan = true;
                            scriptCommitted = true;
                            sw.Stop();
                            Parser.Log("Finished script in {0} ms.", sw.ElapsedMilliseconds);
                        }
                        else
                        {
                            RunScriptChecked(script);
                        }
                    }
                }
                else
                {
                    RunScriptChecked(script);
                }

                if (upgradeDatastore && scriptCommitted)
                {
                    using (PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
                    {
                        Parser.ForceScript(delegate ()
                        {
                            Refactor.ApplyConstraints();
                        });
                    }
                }
            }

            if (upgradeDatastore)
            {
                using (PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
                {
                    Parser.ForceScript(delegate ()
                    {
                        Refactor.ApplyConstraints();
                    });
                }

                using (PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
                {
                    if (!anyScriptRan && Parser.ShouldRefreshFunctionalIds())
                    {
                        Refactor.ApplyFunctionalIds();
                        Parser.SetLastRun();
                    }

                    Transaction.Commit();
                }
            }
            SubscribeEventHandlers();

#pragma warning restore CS0618 // Type or member is obsolete

            if (!PersistenceProvider.IsVoidProvider)
            {
                using (PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
                {
                    bool shouldRun = !Refactor.HasFullTextSearchIndexes() && Entities.Any(entity => entity.FullTextIndexProperties.Count > 0);
                    Refactor.ApplyFullTextSearchIndexes(shouldRun);
                }
            }

            executed = true;
        }

        private void RunScriptChecked(UpgradeScript script)
        {
            try
            {
                script.Method.Invoke();
            }
            catch (Exception e)
            {
                int line = 0;

                StackTrace stack = new StackTrace(e, true);
                for (int frameIndex = 0; frameIndex < stack.FrameCount; frameIndex++)
                {
                    StackFrame? frame = stack.GetFrame(frameIndex);
                    MethodBase? method = frame?.GetMethod();
                    if (method is null)
                        continue;

                    VersionAttribute? attr = method.GetCustomAttribute<VersionAttribute>();
                    if (attr is not null)
                    {
                        line = frame?.GetFileLineNumber() ?? 0;
                        break;
                    }
                }

                throw new InvalidOperationException($"Error in script version {script.Major}.{script.Minor}.{script.Patch}, line {line} -> {e.Message}", e);
            }
        }

        /// <summary>
        /// Subscribes to the event handlers in the overridden method
        /// </summary>
        protected abstract void SubscribeEventHandlers();

        [DebuggerDisplay("UpgradeScript: {Major}.{Minor}.{Patch} ({Name})")]
        internal class UpgradeScript : IComparable<UpgradeScript>
        {
            public UpgradeScript(Action method, long major, long minor, long patch, string name)
            {
                Method = method;
                Major = major;
                Minor = minor;
                Patch = patch;
                Name = name;
            }

            public Action Method { get; private set; }
            public long Major { get; private set; }
            public long Minor { get; private set; }
            public long Patch { get; private set; }
            public string Name { get; private set; }

            int IComparable<UpgradeScript>.CompareTo(UpgradeScript other)
            {
                int result = this.Major.CompareTo(other.Major);
                if (result != 0)
                    return result;

                result = this.Minor.CompareTo(other.Minor);
                if (result != 0)
                    return result;

                result = this.Patch.CompareTo(other.Patch);
                if (result != 0)
                    return result;

                return this.Name.CompareTo(other.Name);
            }

            public override bool Equals(object? obj)
            {
                if (obj is not UpgradeScript other)
                    return false;

                return (Patch == other.Patch && Minor == other.Minor && Major == other.Major);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Patch, Minor, Major, Name);
            }
        }

        #endregion

        /// <summary>
        /// Get the schema info for the data-store
        /// </summary>
        /// <returns>The schema info</returns>
        SchemaInfo IDatastoreUnitTesting.GetSchemaInfo() => PersistenceProvider.SchemaInfo;

        /// <summary>
        /// The refactor actions
        /// </summary>
        protected IRefactorGlobal Refactor { get { return this; } }

        /// <summary>
        /// (Re-)Create FunctionalId definitions in the database and set the initial seed to the max value found in the database.
        /// </summary>
        void IRefactorGlobal.ApplyFunctionalIds()
        {
            EnsureSchemaMigration();

            //This will cause that the code to refresh function id every 12 hours to not be triggered.
            //if (!Parser.ShouldExecute) 
            //    return;
            if (PersistenceProvider.Translator.HasBlueprint41FunctionalidFnNext.Value)
                PersistenceProvider.SchemaInfo.UpdateFunctionalIds();
        }

        /// <summary>
        /// Apply any missing constraint or index to the database
        /// </summary>
        void IRefactorGlobal.ApplyConstraints()
        {
            EnsureSchemaMigration();
            if (!Parser.ShouldExecute)
                return;

            PersistenceProvider.SchemaInfo.UpdateConstraints();
        }

        /// <summary>
        /// Apply any missing full-text-index to the database
        /// </summary>

        void IRefactorGlobal.ApplyFullTextSearchIndexes()
        {
            Refactor.ApplyFullTextSearchIndexes(false);
        }

        /// <summary>
        /// Apply any missing full-text-index to the database
        /// (force apply when shouldRun = true)
        /// </summary>
        void IRefactorGlobal.ApplyFullTextSearchIndexes(bool shouldRun)
        {
            EnsureSchemaMigration();
            if (!Parser.ShouldExecute || !shouldRun)
                return;

            PersistenceProvider.Translator.ApplyFullTextSearchIndexes(Entities);
        }

        /// <summary>
        /// Scans the data model if any full-text-indexes exist
        /// </summary>
        bool IRefactorGlobal.HasFullTextSearchIndexes()
        {
            return PersistenceProvider.Translator.HasFullTextSearchIndexes();
        }

        /// <summary>
        /// Define a data-migration
        /// </summary>
        protected DataMigrationScope DataMigration { get; private set; }
        public class DataMigrationScope
        {
            internal DataMigrationScope(DatastoreModel model)
            {
                Model = model;
            }
            public DatastoreModel Model { get; private set; }

            /// <summary>
            /// Run a data-migration script
            /// </summary>
            /// <param name="script">The script</param>
            public void Run(Action script)
            {
                if (Model.datamigration)
                    throw new InvalidOperationException("Calling DataMigration.Run() from inside another data migration block is not allowed.");

                Model.datamigration = true;

                if (script is not null && Parser.ShouldExecute)
                {
                    using (Model.PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
                    {
                        script.Invoke();
                        Transaction.Commit();
                    }
                }

                Model.datamigration = false;
            }

            #region Global Migrations

            /// <summary>
            /// Executes a hard-coded data-migration Cypher query against the graph
            /// <para>Be aware that the parameters will be automatically converted from Blueprint41 supported DOT NET types to Neo4j types. However, it returns a raw Neo4j response and no type conversions will be executed on the data it contains.</para>
            /// </summary>
            /// <param name="cypher">The query</param>
            /// <param name="parameters">Any parameters used in the query</param>
            /// <returns>An ExecuteResult object</returns>
            public RawResult ExecuteCypher(string cypher, Dictionary<string, object?>? parameters = null)
            {
                Dictionary<string, object?> convertedParams;

                if (parameters is null)
                    convertedParams = new Dictionary<string, object?>();
                else
                    convertedParams = parameters.ToDictionary(item => item.Key, item => (item.Value is null) ? null : Model.PersistenceProvider.ConvertToStoredType(item.Value.GetType(), item.Value));

                return Transaction.RunningTransaction.Run(cypher, convertedParams);
            }

            #endregion
        }

        internal void EnsureSchemaMigration([CallerMemberName] string callerMethodName = "")
        {
            if (IsDataMigration)
                throw new InvalidOperationException($"The method '{callerMethodName}' cannot be used inside a data migration block.");
        }
        internal void EnsureDataMigration([CallerMemberName] string callerMethodName = "")
        {
            if (!IsDataMigration)
                throw new InvalidOperationException($"The method '{callerMethodName}' cannot be used outside a data migration block. Consider adding the code:\r\nusing (DataMigration)\r\n{{\r\n\t{callerMethodName}...\r\n}}");
        }

        internal Guid GenerateGuid(string name)
        {
            byte[] b1 = Encoding.UTF8.GetBytes(name);
            byte[] b2 = new byte[b1.Length];
            for (int read = 0, write = b1.Length - 1; write >= 0; read++, write--)
                b2[write] = b1[read];

            Guid hash = GetHash(b1, b2);

            while (knownGuids.Contains(hash))
            {
                for (int index = 0; index < 8; index++)
                {
                    b1[index]++;
                    if (b1[index] != 0)
                        break;
                }
                hash = GetHash(b1, b2);
            }

            knownGuids.Add(hash);
            return hash;
        }

        private static Guid GetHash(byte[] b1, byte[] b2)
        {
            uint i1 = Crc32Algorithm.Compute(b1);
            uint i2 = Crc32CAlgorithm.Compute(b2);
            byte[] i3 = BitConverter.GetBytes(Crc32Algorithm.Compute(b1));
            byte[] i4 = BitConverter.GetBytes(Crc32CAlgorithm.Compute(b2));

            return new Guid(i1, (ushort)i2, (ushort)(i2 >> 16), i3[0], i3[1], i3[2], i3[3], i4[0], i4[1], i4[2], i4[3]);
        }
        private readonly HashSet<Guid> knownGuids = new HashSet<Guid>();

        private readonly AtomicDictionary<string, Entity> entityByLabel = new AtomicDictionary<string, Entity>();
        internal Entity? GetEntity(IEnumerable<string> labels)
        {
            Entity? entity = null;
            foreach (string label in labels)
            {
                entity = entityByLabel.TryGetOrAdd(label, key => Entities.FirstOrDefault(item => item.Label.Name == label));
                if (!entity.IsAbstract)
                    return entity;
            }
            return null;
        }

        /// <summary>
        /// True if runtime-types have been registered on the entities
        /// </summary>
        public bool TypesRegistered { get; internal set; } = false;
    }

    public abstract class DatastoreModel<TSelf> : DatastoreModel
        where TSelf : DatastoreModel<TSelf>, new()
    {
        public static TSelf Connect(Uri uri, AuthToken authToken, string? database = null, AdvancedConfig? advancedConfig = null)
        {
            TSelf instance = new TSelf();
            instance._persistenceProvider = new PersistenceProvider(instance, uri, authToken, database, advancedConfig);
            instance._persistenceProvider.Initialize();

            return instance;
        }

        private static TSelf? model = null;

        /// <summary>
        /// Get the main model instance
        /// <para>This method will not wait for the model to execute</para>
        /// </summary>
        /// <returns></returns>
        public static TSelf GetMainInstance()
        {
            if (model is null)
            {
                lock (typeof(TSelf))
                {
                    if (model is null)
                    {
                        model = (TSelf?)RegisteredModels.FirstOrDefault(item => item.GetType() == typeof(TSelf));
                        if (model is null)
                        {
                            model = new TSelf();
                            model.Execute(false);
                        }
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// The main model instance
        /// </summary>
        public static TSelf Model
        {
            get
            {
                while (!GetMainInstance().HasExecuted)
                    Thread.Sleep(100);

                return GetMainInstance();
            }
        }

        /// <summary>
        /// Whether or not queries should be logged to the console
        /// </summary>
        public bool LogToConsole { get => Parser.LogToConsole; set => Parser.LogToConsole = value; }

        /// <summary>
        /// Whether or not queries should be logged to the debugger
        /// </summary>
        public bool LogToDebugger { get => Parser.LogToDebugger; set => Parser.LogToDebugger= value; }
    }
}
