using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Void;
using Blueprint41.Neo4j.Refactoring;

namespace Blueprint41.Neo4j.Schema
{
    public class SchemaInfo
    {
        internal SchemaInfo(DatastoreModel model)
        {
            Model = model;
            Initialize();
        }
        protected virtual void Initialize()
        {
            using (Transaction.Begin())
            {
                bool hasPlugin = Model.PersistenceProvider.Translator.HasBlueprint41Plugin.Value;

                FunctionalIds     = hasPlugin ? LoadData("CALL blueprint41.functionalid.list()", record => NewFunctionalIdInfo(record)) : new List<FunctionalIdInfo>(0);
                Constraints       = LoadData("CALL db.constraints()", record => NewConstraintInfo(record));
                Indexes           = LoadData("CALL db.indexes()", record => NewIndexInfo(record));
                Labels            = LoadSimpleData("CALL db.labels()", "label");
                PropertyKeys      = LoadSimpleData("CALL db.propertyKeys()", "propertyKey");
                RelationshipTypes = LoadSimpleData("CALL db.relationshipTypes()", "relationshipType");
            }
        }
        protected IReadOnlyList<string> LoadSimpleData(string procedure, string resultname)
        {
            return LoadData<string>(procedure, record => record.Values[resultname].As<string>());
        }
        protected IReadOnlyList<T> LoadData<T>(string procedure, Func<RawRecord, T> processor)
        {
            bool retry;
            IReadOnlyList<T>? data = null;
            do
            {
                try
                {
                    retry = false;
                    RawResult result = Transaction.RunningTransaction.Run(procedure);
                    data = result.Select(processor).ToArray();
                }
                catch (Exception clientException)
                {
                    if (!clientException.Message.Contains("is still populating"))
                        throw;

                    retry = true;
                    Thread.Sleep(500);
                }
            } while (retry);

            return data ?? throw new NotSupportedException("Could not load the data.");
        }

        public IReadOnlyList<FunctionalIdInfo> FunctionalIds { get; protected set; } = null!;
        public IReadOnlyList<ConstraintInfo>   Constraints   { get; protected set; } = null!;
        public IReadOnlyList<IndexInfo>        Indexes       { get; protected set; } = null!;

        public IReadOnlyList<string> Labels            { get; protected set; } = null!;
        public IReadOnlyList<string> PropertyKeys      { get; protected set; } = null!;
        public IReadOnlyList<string> RelationshipTypes { get; protected set; } = null!;
        protected DatastoreModel Model;

        public IReadOnlyList<ApplyConstraintEntity> GetConstraintDifferences()
        {
            return Model.Entities.Where(entity => !entity.IsVirtual).Select(entity => GetConstraintDifferences(entity)).ToArray();
        }
        public ApplyConstraintEntity GetConstraintDifferences(Entity entity)
        {
            return NewApplyConstraintEntity(entity);
        }

        protected virtual long FindMaxId(FunctionalId functionalId)
        {
            bool first = true;
            string templateNumeric = "MATCH (node:{0}) WHERE toInt(node.Uid) IS NOT NULL WITH toInt(node.Uid) AS decoded RETURN case Max(decoded) WHEN NULL THEN 0 ELSE Max(decoded) END as MaxId";
            string templateHash = "MATCH (node:{0}) where node.Uid STARTS WITH '{1}' AND Length(node.Uid) >= {2} CALL blueprint41.hashing.decode(replace(node.Uid, '{1}', '')) YIELD value as decoded RETURN  case Max(decoded) WHEN NULL THEN 0 ELSE Max(decoded) END as MaxId";
            string actualFidValue = "CALL blueprint41.functionalid.current('{0}') YIELD Sequence as sequence RETURN sequence";
            StringBuilder queryBuilder = new StringBuilder();
            foreach (var entity in Model.Entities.Where(entity => entity.FunctionalId?.Label == functionalId.Label))
            {
                if (first)
                    first = false;
                else
                    queryBuilder.AppendLine("UNION");

                if (functionalId.Format == IdFormat.Hash)
                    queryBuilder.AppendFormat(templateHash, entity.Label.Name, functionalId.Prefix, functionalId.Prefix.Length + 6);
                else
                    queryBuilder.AppendFormat(templateNumeric, entity.Label.Name);
                queryBuilder.AppendLine();
            }

            if (queryBuilder.Length != 0)
            {
                var ids = LoadData(queryBuilder.ToString(), record => record.Values["MaxId"].As<long>());
                if (ids.Count == 0)
                    return 0;
                else
                    return ids.Max() + 1;
            }
            else
            {
                return LoadData(string.Format(actualFidValue, functionalId.Label), record => record.Values["sequence"].As<int?>()).FirstOrDefault()??0;
            }
        }

        internal IReadOnlyList<ApplyFunctionalId> GetFunctionalIdDifferences()
        {
            List<ApplyFunctionalId> actions = new List<Schema.ApplyFunctionalId>();
            foreach (var inMemory in Model.FunctionalIds)
            {
                long maxNumber = FindMaxId(inMemory);
                long startFrom = maxNumber > inMemory.StartFrom ? maxNumber : inMemory.StartFrom;
                var inDb = FunctionalIds.FirstOrDefault(item => inMemory.Label == item.Label);
                if (inDb is null)
                {
                    actions.Add(NewApplyFunctionalId(inMemory.Label, inMemory.Prefix, startFrom, ApplyFunctionalIdAction.CreateFunctionalId));
                    continue;
                }

                if (inDb.Prefix != inMemory.Prefix || inDb.SequenceNumber < startFrom)
                {
                    startFrom = startFrom > inDb.SequenceNumber ? startFrom : inDb.SequenceNumber;
                    actions.Add(
                        NewApplyFunctionalId(
                            inDb.Label, 
                            inMemory.Prefix,
                            startFrom, 
                            ApplyFunctionalIdAction.UpdateFunctionalId));
                }
            }

            foreach (var inDb in FunctionalIds)
            {
                var inMemory = Model.FunctionalIds.FirstOrDefault(item => inDb.Label == item.Label);
                if(inMemory is null)
                    actions.Add(new Schema.ApplyFunctionalId(this, inDb.Label, inDb.Prefix, inDb.SequenceNumber, ApplyFunctionalIdAction.DeleteFunctionalId));
            }

            return actions;
        }
        internal void UpdateFunctionalIds()
        {
            using (Transaction.Begin())
            {
                foreach (var diff in GetFunctionalIdDifferences())
                {
                    Parser.Log(diff.ToString());
                    foreach (var query in diff.ToCypher())
                    {
                        Transaction.RunningTransaction.Run(query);
                    }
                }
                Transaction.Commit();
            }
        }
        internal void UpdateConstraints()
        {
            using (Transaction.Begin())
            {
                foreach (var diff in GetConstraintDifferences())
                {
                    foreach (var action in diff.Actions)
                    {
                        foreach (var cql in action.ToCypher())
                        {
                            Parser.Log(cql);
                            Transaction.RunningTransaction.Run(cql);
                        }
                    }
                }
                Transaction.Commit();
            }
        }

        protected virtual FunctionalIdInfo NewFunctionalIdInfo(RawRecord rawRecord) => new FunctionalIdInfo(rawRecord);
        protected virtual ConstraintInfo   NewConstraintInfo(RawRecord rawRecord)   => new ConstraintInfo(rawRecord);
        protected virtual IndexInfo        NewIndexInfo(RawRecord rawRecord)        => new IndexInfo(rawRecord);

        protected virtual ApplyConstraintEntity   NewApplyConstraintEntity(Entity entity)                                                                              => new ApplyConstraintEntity(this, entity);
        protected virtual ApplyFunctionalId       NewApplyFunctionalId(string label, string prefix, long startFrom, ApplyFunctionalIdAction action)                     => new ApplyFunctionalId(this, label, prefix, startFrom, action);
        internal virtual ApplyConstraintProperty  NewApplyConstraintProperty(ApplyConstraintEntity parent, Property property, params ApplyConstraintAction[] commands) => new ApplyConstraintProperty(parent, property, commands);
        internal virtual ApplyConstraintProperty  NewApplyConstraintProperty(ApplyConstraintEntity parent, string property, params ApplyConstraintAction[] commands)   => new ApplyConstraintProperty(parent, property, commands);
    }
}