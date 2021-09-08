using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Schema;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Neo4j.Refactoring;
using Force.Crc32;
using model = Blueprint41.Neo4j.Model;

namespace Blueprint41
{
    public abstract class DatastoreModel : IRefactorGlobal
    {
        protected DatastoreModel(PersistenceProvider persistence)
        {
            PersistenceProvider = persistence;

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

        public PersistenceProvider PersistenceProvider { get; private set; }

        public EntityCollection       Entities      { get; private set; }
        public RelationshipCollection Relations     { get; private set; }
        public InterfaceCollection    Interfaces    { get; private set; }
        public EnumerationCollection  Enumerations  { get; private set; }
        public FunctionalIdCollection FunctionalIds { get; private set; }
        public SubModelCollection     SubModels     { get; private set; }

        public static List<DatastoreModel> RegisteredModels { get; } = new List<DatastoreModel>();

        public bool IsUpgraded
        {
            get
            {
                return executed;
            }
        }
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

        private bool executed = false;
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
                if (!(prev is null) && prev.Equals(item)) // don't use prev == item !!!!
                    throw new NotSupportedException($"There are two methods ({prev.Name} & {item.Name}) with the same script version ({item.Major}.{item.Minor}.{item.Patch}).");

                prev = item;
            }


            if (!(unitTestScript is null))
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

        public void Execute(bool upgradeDatastore)
        {
            Execute(upgradeDatastore, null);
        }
        internal void Execute(bool upgradeDatastore, MethodInfo? unitTestScript)
        {
            if (isExecuting)
                throw new InvalidOperationException("It is not allowed to call the 'Execute' method from within an upgrade script.");

            try
            {
                isExecuting = true;

                Execute(upgradeDatastore, unitTestScript, item => true, true);

                lock (RegisteredModels)
                {
                    if (RegisteredModels.Any(item => item.GetType() == this.GetType()))
                        return;

                    RegisteredModels.Add(this);
                }
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
                if (upgradeDatastore && PersistenceProvider.IsNeo4j)
                {
                    using (Transaction.Begin(true))
                    {
                        if (!Parser.HasScript(script))
                        {
                            Debug.WriteLine("Running script: {0}.{1}.{2} ({3})", script.Major, script.Minor, script.Patch, script.Name);
                            Stopwatch sw = Stopwatch.StartNew();

                            Refactor.ApplyFunctionalIds();
                            RunScriptChecked(script);
                            Refactor.ApplyFunctionalIds();

                            Parser.CommitScript(script);
                            anyScriptRan = true;
                            scriptCommitted = true;
                            sw.Stop();
                            Debug.WriteLine("Finished script in {0} ms.", sw.ElapsedMilliseconds);
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
                    using (Transaction.Begin(true))
                    {
                        Parser.ForceScript(delegate ()
                        {
                            Refactor.ApplyConstraints();
                        });
                        Transaction.Commit();
                    }
                }
            }

            if (upgradeDatastore)
            {
                using (Transaction.Begin(true))
                {
                    Parser.ForceScript(delegate ()
                    {
                        Refactor.ApplyConstraints();
                    });
                    Transaction.Commit();
                }

                using (Transaction.Begin(true))
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

            executed = true;

#pragma warning restore CS0618 // Type or member is obsolete
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
                UpgradeScript? other = obj as UpgradeScript;
                if (other is null)
                    return false;

                return (Patch == other.Patch && Minor == other.Minor && Major == other.Major);
            }

            public override int GetHashCode()
            {
                return Patch.GetHashCode() ^ ROL(Minor.GetHashCode(), 10) ^ ROL(Major.GetHashCode(), 20) ^ Name.GetHashCode();

                int ROL(int value, int bits)
                {
                    uint val = (uint)value;
                    return (int)((val << bits) | (val >> (32 - bits)));
                }
            }
        }

        #endregion


        internal SchemaInfo GetSchema()
        {
            return PersistenceProvider.GetSchemaInfo(this);
        }

        protected IRefactorGlobal Refactor { get { return this; } }

        void IRefactorGlobal.ApplyFunctionalIds()
        {
            EnsureSchemaMigration();
            if (!Parser.ShouldExecute)
                return;

            GetSchema().UpdateFunctionalIds();
        }

        void IRefactorGlobal.ApplyConstraints()
        {
            EnsureSchemaMigration();
            if (!Parser.ShouldExecute)
                return;

            GetSchema().UpdateConstraints();
        }

        void IRefactorGlobal.SetCreationDate()
        {
            EnsureSchemaMigration();
            if (!Parser.ShouldExecute)
                return;

            Templates.SetCreationDate().RunBatched();
        }

        void IRefactorGlobal.ApplyFullTextSearchIndexes()
        {
            EnsureSchemaMigration();
            if (!Parser.ShouldExecute)
                return;

            PersistenceProvider.Translator.ApplyFullTextSearchIndexes(Entities);
        }

        protected DataMigrationScope DataMigration { get; private set; }
        public class DataMigrationScope
        {
            internal DataMigrationScope(DatastoreModel model)
            {
                Model = model;
            }
            public DatastoreModel Model { get; private set; }

            public void Run(Action script)
            {
                if (Model.datamigration)
                    throw new InvalidOperationException("Calling DataMigration.Run() from inside another data migration block is not allowed.");

                Model.datamigration = true;

                if (script is not null && Parser.ShouldExecute)
                {
                    using (Transaction.Begin(true))
                    {
                        script.Invoke();
                        Transaction.Commit();
                    }
                }

                Model.datamigration = false;
            }

            #region Global Migrations

            /// <summary>
            /// Executes a hard-coded Cypher query against the graph.
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
        private HashSet<Guid> knownGuids = new HashSet<Guid>();

        private AtomicDictionary<string, Entity> entityByLabel = new AtomicDictionary<string, Entity>();
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

        internal RefactorTemplates Templates => PersistenceProvider.Templates;
    }

    public abstract class DatastoreModel<TSelf> : DatastoreModel
        where TSelf : DatastoreModel<TSelf>, new()
    {
        protected DatastoreModel() : this(PersistenceProvider.CurrentPersistenceProvider) { }
        protected DatastoreModel(PersistenceProvider persistence) : base(persistence) { }

        private static TSelf? model = null;
        public static TSelf Model
        {
            get
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
                                TSelf m = new TSelf();
                                m.Execute(false);
                                model = m;
                            }
                        }
                    }
                }
                return model;
            }
        }
    }
}
