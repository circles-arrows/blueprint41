using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace Blueprint41.Neo4j.Schema
{
    public class SchemaInfo
    {
        private SchemaInfo(DatastoreModel model)
        {
            Model = model;
        }

        internal static SchemaInfo FromDB(DatastoreModel model)
        {
            SchemaInfo info = new SchemaInfo(model);

            using (Transaction.Begin())
            {
                info.FunctionalIds = LoadData("CALL blueprint41.functionalid.list()", record => new FunctionalIdInfo(record));
                info.Constraints = LoadData("CALL db.constraints()", record => new ConstraintInfo(record));
                info.Indexes = LoadData("CALL db.indexes()", record => new IndexInfo(record));
                info.Labels = LoadSimpleData("CALL db.labels()", "label");
                info.PropertyKeys = LoadSimpleData("CALL db.propertyKeys()", "propertyKey");
                info.RelationshipTypes = LoadSimpleData("CALL db.relationshipTypes()", "relationshipType");
            }
            return info;
        }
        private static IReadOnlyList<string> LoadSimpleData(string procedure, string resultname)
        {
            return LoadData<string>(procedure, record => record.Values[resultname].As<string>());
        }
        private static IReadOnlyList<T> LoadData<T>(string procedure, Func<IRecord, T> processor)
        {
            bool retry;
            IReadOnlyList<T> data = null;
            do
            {
                try
                {
                    retry = false;
                    IStatementResult result = Persistence.Neo4jTransaction.Run(procedure);
                    data = result.Select(processor).ToArray();
                }
                catch (ClientException clientException)
                {
                    if (!clientException.Message.Contains("is still populating"))
                        throw;

                    retry = true;
                    Thread.Sleep(500);
                }
            } while (retry);

            return data;
        }

        public IReadOnlyList<FunctionalIdInfo> FunctionalIds { get; private set; }
        public IReadOnlyList<ConstraintInfo> Constraints { get; private set; }
        public IReadOnlyList<IndexInfo> Indexes { get; private set; }

        public IReadOnlyList<string> Labels { get; private set; }
        public IReadOnlyList<string> PropertyKeys { get; private set; }
        public IReadOnlyList<string> RelationshipTypes { get; private set; }
        private DatastoreModel Model;

        public ApplyConstraintEntity GetConstraintDifferences(Entity entity)
        {
            return new ApplyConstraintEntity(this, entity);
        }
        public IReadOnlyList<ApplyConstraintEntity> GetConstraintDifferences()
        {
            return Model.Entities.Where(entity => !entity.IsVirtual).Select(entity => GetConstraintDifferences(entity)).ToArray();
        }

        private int FindMaxId(FunctionalId functionalId)
        {
            bool first = true;
            string templateNumeric = "MATCH (node:{0}) WHERE toInt(node.Uid) IS NOT NULL WITH toInt(node.Uid) AS decoded RETURN case Max(decoded) WHEN NULL THEN 0 ELSE Max(decoded) END as MaxId";
            string templateHash = "MATCH (node:{0}) where node.Uid STARTS WITH '{1}' AND Length(node.Uid) = {2} CALL blueprint41.hashing.decode(replace(node.Uid, '{1}', '')) YIELD value as decoded RETURN  case Max(decoded) WHEN NULL THEN 0 ELSE Max(decoded) END as MaxId";
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
                var ids = LoadData(queryBuilder.ToString(), record => record.Values["MaxId"].As<int>());
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
                int maxNumber = FindMaxId(inMemory);
                int startFrom = maxNumber > inMemory.StartFrom ? maxNumber : inMemory.StartFrom;
                var inDb = FunctionalIds.FirstOrDefault(item => inMemory.Label == item.Label);
                if (inDb == null)
                {
                    actions.Add(new Schema.ApplyFunctionalId(this, inMemory.Label, inMemory.Prefix, startFrom, ApplyFunctionalIdAction.CreateFunctionalId));
                    continue;
                }

                if (inDb.Prefix != inMemory.Prefix || inDb.SequenceNumber < startFrom)
                {
                    startFrom = startFrom > inDb.SequenceNumber ? startFrom : inDb.SequenceNumber;
                    actions.Add(
                        new Schema.ApplyFunctionalId(
                            this, 
                            inDb.Label, 
                            inMemory.Prefix,
                            startFrom, 
                            ApplyFunctionalIdAction.UpdateFunctionalId));
                }
            }

            foreach (var inDb in FunctionalIds)
            {
                var inMemory = Model.FunctionalIds.FirstOrDefault(item => inDb.Label == item.Label);
                if(inMemory == null)
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
                    System.Diagnostics.Debug.WriteLine(diff.ToString());
                    foreach (var query in diff.ToCypher())
                    {
                        Persistence.Neo4jTransaction.Run(query);
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
                            //System.Diagnostics.Debug.WriteLine(diff.ToString());
                            Persistence.Neo4jTransaction.Run(cql);
                        }
                    }
                }
                Transaction.Commit();
            }
        }
    }
}