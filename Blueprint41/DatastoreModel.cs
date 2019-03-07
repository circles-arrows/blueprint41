using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.Neo4j.Refactoring;
using Blueprint41.Neo4j.Refactoring.Templates;
using Blueprint41.Neo4j.Schema;
using Force.Crc32;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using model = Blueprint41.Neo4j.Model;

namespace Blueprint41
{
    public abstract class DatastoreModel : IRefactorGlobal
    {
        protected DatastoreModel(Type targetDatabase)
        {
            PersistenceProvider = (PersistenceProvider)Activator.CreateInstance(targetDatabase, true);

            Entities = new EntityCollection(this);
            Relations = new RelationshipCollection(this);
            FunctionalIds = new FunctionalIdCollection(this);

            Labels = new model.LabelCollection();
            RelationshipTypes = new model.RelationshipTypeCollection();

            DataMigration = new DataMigrationScope(this);
        }

        public PersistenceProvider PersistenceProvider { get; private set; }

        public EntityCollection Entities { get; private set; }
        public RelationshipCollection Relations { get; private set; }
        public FunctionalIdCollection FunctionalIds { get; private set; }
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

        internal List<UpgradeScript> GetUpgradeScripts(MethodInfo unitTestScript)
        {
            List<UpgradeScript> scripts = new List<UpgradeScript>();

            foreach (MethodInfo info in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                VersionAttribute attr = info.GetCustomAttribute<VersionAttribute>();
                if (attr != null)
                {
                    Action method = (Action)Delegate.CreateDelegate(typeof(Action), this, info);
                    scripts.Add(new UpgradeScript(method, attr.Major, attr.Minor, attr.Patch, info.Name));
                }
            }
            scripts.Sort();

            UpgradeScript prev = null;
            foreach (UpgradeScript item in scripts)
            {
                if (prev != null && prev.Equals(item)) // don't use prev == item !!!!
                    throw new NotSupportedException($"There are two methods ({prev.Name} & {item.Name}) with the same script version ({item.Major}.{item.Minor}.{item.Patch}).");

                prev = item;
            }


            if ((object)unitTestScript != null)
            {
                UpgradeScript prevScript = scripts.LastOrDefault();
                long major = prevScript?.Major ?? 0;
                long minor = prevScript?.Minor ?? 0;
                long patch = prevScript?.Patch ?? 0;

                Action method = (Action)Delegate.CreateDelegate(typeof(Action), this, unitTestScript);
                UpgradeScript injectedScript = new UpgradeScript(method, major, minor, patch + 1, "InjectedUnitTestMethod");
                scripts.Add(injectedScript);
            }

            return scripts;
        }

        public void Execute(bool upgradeDatastore)
        {
            Execute(upgradeDatastore, null);
        }
        internal void Execute(bool upgradeDatastore, MethodInfo unitTestScript)
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

        internal void Execute(bool upgradeDatastore, MethodInfo unitTestScript, Predicate<UpgradeScript> predicate, bool standAloneScript)
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
                    if (!anyScriptRan && Parser.ShouldRefreshFunctionalIds())
                    {
                        Refactor.ApplyConstraints();
                        Refactor.ApplyFunctionalIds();
                        Parser.SetLastRun();
                    }
                    if (anyScriptRan)
                    {
                        Parser.ForceScript(delegate ()
                        {
                            Refactor.ApplyConstraints();
                        });
                    }

                    Transaction.Commit();
                }

                SubscribeEventHandlers();
            }

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
                    StackFrame frame = stack.GetFrame(frameIndex);
                    MethodBase method = frame.GetMethod();
                    if (method == null)
                        continue;

                    VersionAttribute attr = method.GetCustomAttribute<VersionAttribute>();
                    if (attr != null)
                    {
                        line = frame.GetFileLineNumber();
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

            internal UpgradeScript(INode node)
            {
                Major = (long)node.Properties["Major"];
                Minor = (long)node.Properties["Minor"];
                Patch = (long)node.Properties["Patch"];
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

            public override bool Equals(object obj)
            {
                UpgradeScript other = obj as UpgradeScript;
                if ((object)other == null)
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
            return SchemaInfo.FromDB(this);
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

            string cypher = "MATCH ()-[r]->() WHERE NOT EXISTS(r.CreationDate)  WITH r LIMIT 10000 SET r.CreationDate = ID(r)";
            Parser.ExecuteBatched(cypher, new Dictionary<string, object>(), true);
        }

        void IRefactorGlobal.ApplyFullTextSearchIndexes()
        {
            EnsureSchemaMigration();
            if (!Parser.ShouldExecute)
                return;

            ApplyFullTextSearchIndexes();
        }

        public void ApplyFullTextSearchIndexes()
        {
            using (Transaction.Begin(true))
            {
                Neo4jTransaction.Run("CALL apoc.index.remove('fts')");
                Transaction.Commit();
            }

            using (Transaction.Begin(true))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("CALL apoc.index.addAllNodesExtended('fts',");
                builder.AppendLine("\t{");

                bool first = true;
                foreach (var entity in Entities)
                {
                    if (entity.FullTextIndexProperties.Count == 0)
                        continue;

                    if (first)
                        first = false;
                    else
                        builder.AppendLine(",");

                    builder.AppendFormat("\t\t{0}:\t\t\t['", entity.Label.Name);
                    builder.Append(string.Join("', '", entity.FullTextIndexProperties.Select(item => item.Name)));
                    builder.Append("']");
                }

                builder.AppendLine();
                builder.AppendLine("\t},");
                builder.AppendLine("\t{");
                builder.AppendLine("\t\tautoUpdate:true");
                builder.AppendLine("\t}");
                builder.AppendLine(")");

                Neo4jTransaction.Run(builder.ToString());

                Transaction.Commit();
            }
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

                if (script != null && Parser.ShouldExecute)
                {
                    Transaction.Flush();
                    script.Invoke();
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
            /// <returns>An IStatementResult object</returns>
            public IStatementResult ExecuteCypher(string cypher, Dictionary<string, object> parameters = null)
            {
                if (parameters == null)
                    parameters = new Dictionary<string, object>();

                Dictionary<string, object> convertedParams = parameters.ToDictionary(item => item.Key, item => ((object)item.Value == null) ? null : Model.PersistenceProvider.ConvertToStoredType(item.Value.GetType(), item.Value));

                return Neo4jTransaction.Run(cypher, convertedParams);
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
    }

    public abstract class DatastoreModel<TSelf> : DatastoreModel
        where TSelf : DatastoreModel<TSelf>, new()
    {
        protected DatastoreModel() : this(typeof(Neo4JPersistenceProvider)) { }
        protected DatastoreModel(Type targetDatabase) : base(targetDatabase) { }

        private static DatastoreModel model = null;
        public static DatastoreModel Model
        {
            get
            {
                if (model == null)
                {
                    lock (typeof(TSelf))
                    {
                        if (model == null)
                        {
                            model = RegisteredModels.FirstOrDefault(item => item.GetType() == typeof(TSelf));
                            if (model == null)
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
