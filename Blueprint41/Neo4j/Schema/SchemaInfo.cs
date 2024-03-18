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
        internal SchemaInfo(DatastoreModel model, Neo4jPersistenceProvider persistenceProvider)
        {
            Model = model;
            PersistenceProvider = persistenceProvider;
            Initialize();
        }
        public Neo4jPersistenceProvider PersistenceProvider { get; protected set; }

        protected virtual void Initialize()
        {
            using (Transaction.Begin())
            {
                bool hasPlugin = Model.PersistenceProvider.Translator.HasBlueprint41Plugin.Value;

                FunctionalIds     = hasPlugin ? LoadData("CALL blueprint41.functionalid.list()", record => NewFunctionalIdInfo(record)) : new List<FunctionalIdInfo>(0);
                Constraints       = LoadData("CALL db.constraints()", record => NewConstraintInfo(record, PersistenceProvider));
                Indexes           = LoadData("CALL db.indexes()", record => NewIndexInfo(record, PersistenceProvider));
                Labels            = LoadSimpleData("CALL db.labels()", "label");
                RelationshipTypes = LoadSimpleData("CALL db.relationshipTypes()", "relationshipType");
            }
        }
        protected IReadOnlyList<string> LoadSimpleData(string procedure, string resultname)
        {
            return LoadData<string>(procedure, record => record.Values[resultname].As<string>());
        }
        protected IReadOnlyList<T> LoadData<T>(string procedure, Func<RawRecord, T> processor)
        {
            IStatementRunner runner = Session.Current as IStatementRunner ?? Transaction.Current ?? throw new InvalidOperationException("Either a Session or an Transaction should be started.");

            bool retry;
            IReadOnlyList<T>? data = null;
            do
            {
                try
                {
                    retry = false;
                    RawResult result = runner.Run(procedure);
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
        public IReadOnlyList<string> RelationshipTypes { get; protected set; } = null!;
        protected DatastoreModel Model;

        public IReadOnlyList<ApplyConstraintEntity> GetConstraintDifferences()
        {
            return Model.Entities.Where(entity => !entity.IsVirtual).Select(GetConstraintDifferences).Union(Model.Relations.Select(GetConstraintDifferences)).ToArray();
        }
        public ApplyConstraintEntity GetConstraintDifferences(IEntity entity)
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
                return ids.Count == 0 ? 0 : ids.Max() + 1;
            }
            else
            {
                return LoadData(string.Format(actualFidValue, functionalId.Label), record => record.Values["sequence"].As<int?>()).FirstOrDefault()??0;
            }
        }

        internal virtual IReadOnlyList<ApplyFunctionalId> GetFunctionalIdDifferences()
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
        internal virtual void UpdateFunctionalIds()
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
        internal virtual void UpdateConstraints()
        {
            using (Session.Begin())
            {
                foreach (var diff in GetConstraintDifferences())
                {
                    foreach (var action in diff.Actions)
                    {
                        foreach (var cql in action.ToCypher())
                        {
                            using (Transaction.Begin())
                            {
                                Parser.Log(cql);
                                Transaction.RunningTransaction.Run(cql);
                                Transaction.Commit();
                            }
                        }
                    }
                }
            }
        }

        internal virtual void RemoveIndexesAndContraints(Property property)
        {
            if (!property.Nullable || property.IndexType != IndexType.None)
            {
                property.Nullable = true;
                property.IndexType = IndexType.None;

                foreach (var entity in property.Parent.GetSubclassesOrSelf())
                {
                    ApplyConstraintEntity applyConstraint = NewApplyConstraintEntity(entity);
                    foreach (var action in applyConstraint.Actions.Where(c => c.Property == property.Name))
                    {
                        foreach (string query in action.ToCypher())
                        {
                            Parser.Execute(query, null, !PersistenceProvider.IsMemgraph);
                        }
                    }
                }
            }
        }

        protected virtual FunctionalIdInfo NewFunctionalIdInfo(RawRecord rawRecord)                                               => new FunctionalIdInfo(rawRecord);
        protected virtual ConstraintInfo   NewConstraintInfo(RawRecord rawRecord, Neo4jPersistenceProvider persistenceProvider)   => new ConstraintInfo(rawRecord, persistenceProvider);
        protected virtual IndexInfo        NewIndexInfo(RawRecord rawRecord, Neo4jPersistenceProvider persistenceProvider)        => new IndexInfo(rawRecord, persistenceProvider);

        internal virtual ApplyConstraintEntity    NewApplyConstraintEntity(IEntity entity)                                                                              => new ApplyConstraintEntity(this, entity);
        internal virtual ApplyFunctionalId        NewApplyFunctionalId(string label, string prefix, long startFrom, ApplyFunctionalIdAction action)                     => new ApplyFunctionalId(this, label, prefix, startFrom, action);
        internal virtual ApplyConstraintProperty  NewApplyConstraintProperty(ApplyConstraintEntity parent, Property property, List<(ApplyConstraintAction, string?)> commands) => new ApplyConstraintProperty(parent, property, commands);
        internal virtual ApplyConstraintProperty  NewApplyConstraintProperty(ApplyConstraintEntity parent, string property, List<(ApplyConstraintAction, string?)> commands)   => new ApplyConstraintProperty(parent, property, commands);

        internal virtual List<(ApplyConstraintAction, string?)> ComputeCommands(IEntity entity, IndexType indexType, bool nullable, bool isKey, IEnumerable<ConstraintInfo> constraints, IEnumerable<IndexInfo> indexes)
        {
            bool isUnique = entity.IsVirtual ? false : constraints.Any(item => item.IsUnique);
            bool isIndexed = entity.IsVirtual ? false : indexes.Any(item => item.IsIndexed);
            bool isMandatory = entity.IsVirtual ? false : constraints.Any(item => item.IsMandatory);

            string? uniqueConstraintName = constraints.FirstOrDefault(item => item.IsUnique)?.Name;
            string? existsConstraintName = constraints.FirstOrDefault(item => item.IsMandatory)?.Name;
            string? indexName = indexes.FirstOrDefault(item => item.IsIndexed)?.Name;

            List<(ApplyConstraintAction, string?)> commands = new List<(ApplyConstraintAction, string?)>();

            if (entity.IsAbstract && indexType == IndexType.Unique)
                indexType = IndexType.Indexed;

            switch (indexType)
            {
                case IndexType.None:
                    if (isUnique)
                    {
                        // Database has an unique index, but we want no index at all
                        commands.Add((ApplyConstraintAction.DeleteUniqueConstraint, uniqueConstraintName));
                    }
                    else if (isIndexed)
                    {
                        // Database has an index, but we want no index at all
                        commands.Add((ApplyConstraintAction.DeleteIndex, indexName));
                    }
                    break;
                case IndexType.Indexed:
                    if (isUnique)
                    {
                        // Database has an unique index, but we want a normal index instead
                        commands.Add((ApplyConstraintAction.DeleteUniqueConstraint, uniqueConstraintName));
                        commands.Add((ApplyConstraintAction.CreateIndex, null));
                    }
                    else if (!isIndexed)
                    {
                        // Database has no index, but we want a normal index instead
                        commands.Add((ApplyConstraintAction.CreateIndex, null));
                    }
                    break;
                case IndexType.Unique:
                    if (!isUnique)
                    {
                        if (isIndexed)
                        {
                            // Database has a normal index, but we want a unique index instead
                            commands.Add((ApplyConstraintAction.DeleteIndex, indexName));
                            commands.Add((ApplyConstraintAction.CreateUniqueConstraint, null));
                        }
                        else
                        {
                            // Database has no index, but we want a unique index instead
                            commands.Add((ApplyConstraintAction.CreateUniqueConstraint, null));
                        }
                    }
                    break;
            }

            if (isMandatory && nullable)
            {
                // Database has a exists constraint, but we want a nullable field instead
                commands.Add((ApplyConstraintAction.DeleteExistsConstraint, existsConstraintName));
            }
            else if (!isMandatory && !nullable)
            {
                // Database has has no exists constraint, but we want a non-nullable field instead
                commands.Add((ApplyConstraintAction.CreateExistsConstraint, null));
            }

            return commands;
        }

    }
}