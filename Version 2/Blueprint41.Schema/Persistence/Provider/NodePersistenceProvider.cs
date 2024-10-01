using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.Core;
using Blueprint41.Events;
using driver = Blueprint41.Persistence;

namespace Blueprint41.Persistence
{
    internal class NodePersistenceProvider
    {
        internal NodePersistenceProvider(DatastoreModel datastoreModel)
        {
            PersistenceProvider = datastoreModel.PersistenceProvider;
        }
        public PersistenceProvider PersistenceProvider { get; private set; }

        public List<T> GetAll<T>(Entity entity)
            where T : class, OGM
        {
            return LoadWhere<T>(entity, string.Empty, null, 0, 0);
        }
        public List<T> GetAll<T>(Entity entity, int page, int pageSize, bool ascending = true, params Property[] orderBy)
            where T : class, OGM
        {
            return LoadWhere<T>(entity, string.Empty, null, page, pageSize, ascending, orderBy);
        }

        public void Load(OGM item, bool locked = false)
        {
            Transaction trans = Transaction.RunningTransaction;

            string returnStatement = (locked) ? " WITH COLLECT(node) AS nodes CALL apoc.lock.nodes(nodes) RETURN HEAD(nodes) AS node" : " RETURN node";
            string match = string.Format("MATCH (node:{0}) WHERE node.{1} = $key", item.GetEntity().Label.Name, item.GetEntity().Key.Name);
            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("key", item.GetKey());

            Dictionary<string, object?>? customState = null;
            var args = item.GetEntity().RaiseOnNodeLoading(trans, item, match + returnStatement, parameters, ref customState);

            var result = ((IStatementRunner)trans).Run(args.Cypher, args.Parameters);

            driver.Record? record = result.FirstOrDefault();
            if (record is null || record["node"] is null)
            {
                item.PersistenceState = PersistenceState.DoesntExist;
                return;
            }

            driver.NodeResult loaded = record["node"]!.As<driver.NodeResult>();

            args.ElementId = loaded.ElementId;
            args.Labels = loaded.Labels;
            // HACK: To make it faster we do not copy/replicate the Dictionary here, but it means someone
            //       could be changing the INode content from within an event. Possibly dangerous, but
            //       turns out the Neo4j driver can deal with it ... for now ... 
            args = item.GetEntity().RaiseOnNodeLoaded(trans, args, loaded.ElementId, loaded.Labels, (Dictionary<string, object?>)loaded.Properties);

            if (item.PersistenceState == PersistenceState.HasUid || item.PersistenceState == PersistenceState.Loaded)
            {
                item.SetData(args.Properties!);
                item.PersistenceState = PersistenceState.Loaded;
                item.OriginalPersistenceState = PersistenceState.Loaded;
            }
        }

        public void Delete(OGM item)
        {
            Transaction trans = Transaction.RunningTransaction;
            Entity entity = item.GetEntity();

            string match;
            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("key", item.GetKey());

            if (entity.RowVersion is null)
            {
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = $key DELETE node", entity.Label.Name, entity.Key.Name);
            }
            else
            {
                parameters.Add("lockId", Conversion<DateTime, long>.Convert(item.GetRowVersion()));
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = $key AND node.{2} = $lockId DELETE node", entity.Label.Name, entity.Key.Name, entity.RowVersion.Name);
            }

            Dictionary<string, object?>? customState = null;
            var args = entity.RaiseOnNodeDelete(trans, item, match, parameters, ref customState);

            var result = ((IStatementRunner)trans).Run(args.Cypher, args.Parameters);
            var counters = result.Statistics();
            if (counters.NodesDeleted == 0)
                throw new DBConcurrencyException($"The {entity.Name} with {entity.Key.Name} '{item.GetKey()?.ToString() ?? "<NULL>"}' was changed or deleted by another process or thread.");

            entity.RaiseOnNodeDeleted(trans, args);
            item.PersistenceState = PersistenceState.Deleted;
        }

        public void ForceDelete(OGM item)
        {
            Transaction trans = Transaction.RunningTransaction;
            Entity entity = item.GetEntity();

            string match;
            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("key", item.GetKey());

            if (entity.RowVersion is null)
            {
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = $key DETACH DELETE node", entity.Label.Name, entity.Key.Name);
            }
            else
            {
                parameters.Add("lockId", Conversion<DateTime, long>.Convert(item.GetRowVersion()));
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = $key AND node.{2} = $lockId DETACH DELETE node", entity.Label.Name, entity.Key.Name, entity.RowVersion.Name);
            }

            Dictionary<string, object?>? customState = null;
            var args = entity.RaiseOnNodeDelete(trans, item, match, parameters, ref customState);

            var result = ((IStatementRunner)trans).Run(args.Cypher, args.Parameters);
            var counters =  result.Statistics();
            if (counters.NodesDeleted == 0)
                throw new DBConcurrencyException($"The {entity.Name} with {entity.Key.Name} '{item.GetKey()?.ToString() ?? "<NULL>"}' was changed or deleted by another process or thread.");

            entity.RaiseOnNodeDeleted(trans, args);
            item.PersistenceState = PersistenceState.Deleted;
        }

        public void Insert(OGM item)
        {
            Transaction trans = Transaction.RunningTransaction;
            Entity entity = item.GetEntity();

            string labels = string.Join(":", entity.GetBaseTypesAndSelf().Where(x => !x.IsVirtual).Select(x => x.Label.Name));

            if (entity.RowVersion is not null)
                item.SetRowVersion(trans.TransactionDate);

            IDictionary<string, object?> node = item.GetData();

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("node", node);

            string? create = null;
            object? key = item.GetKey();
            if (entity.FunctionalId is not null)
            {
                if (key is null)
                {
                    create = $"{NextFunctionalIdQuery(entity.FunctionalId)} MERGE (inserted:{labels} {{ {entity.Key.Name}: key}}) ON CREATE SET inserted += $node ON MATCH SET inserted = $node, inserted.{entity.Key.Name} = key RETURN inserted";
                    node.Remove(entity.Key.Name);
                }
                else
                {
                    if (entity.FunctionalId.Label != "Guid")
                        entity.FunctionalId.SeenUid(key.ToString()!);
                }
            }

            if (create is null)
            {
                if (key is null)
                    throw new NotSupportedException("You have to specify the key value if no functional Id was defined.");

                parameters.Add("key", key);

                create = $"MERGE (inserted:{labels} {{ {entity.Key.Name}: $key}}) ON CREATE SET inserted = $node ON MATCH SET inserted = $node RETURN inserted";
            }

            Dictionary<string, object?>? customState = null;
            var args = entity.RaiseOnNodeCreate(trans, item, create, parameters, ref customState);

            var result = ((IStatementRunner)trans).Run(args.Cypher, args.Parameters);
            driver.Record? record = result.FirstOrDefault();
            if (record is null)
                throw new InvalidOperationException($"Due to an unexpected state of the neo4j transaction, it seems impossible to insert the {entity.Name} at this time.");

            driver.NodeResult inserted = record["inserted"].As<driver.NodeResult>();

            args.ElementId = inserted.ElementId;
            args.Labels = inserted.Labels;
            // HACK: To make it faster we do not copy/replicate the Dictionary here, but it means someone
            //       could be changing the INode content from within an event. Possibly dangerous, but
            //       turns out the Neo4j driver can deal with it ... for now ... 
            args.Properties = (Dictionary<string, object?>)inserted.Properties;
            args = entity.RaiseOnNodeCreated(trans, args, inserted.ElementId, inserted.Labels, (Dictionary<string, object?>)inserted.Properties);

            item.SetData(args.Properties!);
            item.PersistenceState = PersistenceState.Persisted;
            Transaction.RunningTransaction.Register(entity.Name, item, true);
        }

        public void Update(OGM item)
        {
            Transaction trans = Transaction.RunningTransaction;
            Entity entity = item.GetEntity();

            string match;
            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("key", item.GetKey());

            if (entity.RowVersion is null)
            {
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = $key SET node = $newValues", entity.Label.Name, entity.Key.Name);
            }
            else
            {
                parameters.Add("lockId", Conversion<DateTime, long>.Convert(item.GetRowVersion()));
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = $key AND node.{2} = $lockId SET node = $newValues", entity.Label.Name, entity.Key.Name, entity.RowVersion.Name);
                item.SetRowVersion(trans.TransactionDate);
            }

            IDictionary<string, object?> node = item.GetData();
            parameters.Add("newValues", node);

            Dictionary<string, object?>? customState = null;
            var args = entity.RaiseOnNodeUpdate(trans, item, match, parameters, ref customState);

            driver.ResultCursor result = ((IStatementRunner)trans).Run(args.Cypher, args.Parameters);
            if (!GetStatistics(result).ContainsUpdates)
                throw new DBConcurrencyException($"The {entity.Name} with {entity.Key.Name} '{item.GetKey()?.ToString() ?? "<NULL>"}' was changed or deleted by another process or thread.");

            entity.RaiseOnNodeUpdated(trans, args);
            item.PersistenceState = PersistenceState.Persisted;
        }

        public string NextFunctionID(FunctionalId functionalId)
        {
            if (functionalId is null)
                throw new ArgumentNullException("functionalId");

            driver.Record? result = Transaction.Run(NextFunctionalIdQuery(functionalId)).FirstOrDefault();
            return result?["key"]?.ToString()!;
        }
        private string NextFunctionalIdQuery(FunctionalId functionalId)
        {
            QueryTranslator t = PersistenceProvider.Translator;

            if (functionalId.Guid == Guid.Empty)
                return t.CallApocCreateUuid;
            if (functionalId.Format == IdFormat.Hash)
                return string.Format(t.CallFunctionalIdNextHash, functionalId.Label);
            else
                return string.Format(t.CallFunctionalIdNextNumeric, functionalId.Label);
        }

        public List<T> LoadWhere<T>(Entity entity, string conditions, IParameter[]? parameters, int page, int pageSize, bool ascending = true, params Property[] orderBy)
            where T : class, OGM
        {
            Transaction trans = Transaction.RunningTransaction;

            StringBuilder sb = new StringBuilder();
            sb.Append("MATCH (node:");
            sb.Append(entity.Label.Name);
            sb.Append(")");

            if (!string.IsNullOrEmpty(conditions))
            {
                sb.Append(" WHERE ");
                sb.AppendFormat(conditions, "node");
            }

            sb.Append(" RETURN node");

            if (orderBy is not null && orderBy.Length != 0)
            {
                Property? odd = orderBy.FirstOrDefault(item => !entity.IsSelfOrSubclassOf(item.Parent));
                if (odd is not null)
                    throw new InvalidOperationException(string.Format("Order property '{0}' belongs to the entity '{1}' while the query only contains entities of type '{2)'.", odd.Name, odd.Parent.Name, entity.Name));

                sb.Append(" ORDER BY ");
                sb.Append(string.Join(", ", orderBy.Select(item => string.Concat("node.", item.Name))));
                if (!ascending)
                    sb.Append(" DESC ");
            }

            if (pageSize > 0)
            {
                sb.Append(" SKIP ");
                sb.Append(page * pageSize);
                sb.Append(" LIMIT ");
                sb.Append(pageSize);
            }

            Dictionary<string, object?>? customState = null;
            Dictionary<string, object?> arguments = new Dictionary<string, object?>();
            if (parameters is not null)
                foreach (IParameter parameter in parameters)
                    arguments.Add(parameter.Name, parameter.Value);

            var args = entity.RaiseOnNodeLoading(trans, null, sb.ToString(), arguments, ref customState);

            var result = ((IStatementRunner)trans).Run(args.Cypher, args.Parameters);
            return Load<T>(entity, args, result, trans);
        }
        public List<T> LoadWhere<T>(Entity entity, ICompiledQuery query, IParameter[] parameters, int page = 0, int pageSize = 0, bool ascending = true, params Property[] orderBy)
            where T : class, OGM
        {
            Transaction trans = Transaction.RunningTransaction;

            IQueryExecutionContext context = query.GetExecutionContext();
            foreach (IParameter queryParameter in parameters)
            {
                if (queryParameter.Value is null)
                    context.SetParameter(queryParameter.Name, null);
                else
                    context.SetParameter(queryParameter.Name, entity.Parent.PersistenceProvider.ConvertToStoredType(queryParameter.Value.GetType(), queryParameter.Value));
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(context.CompiledQuery.QueryText);
            if (orderBy is not null && orderBy.Length != 0)
            {
                Property? odd = orderBy.FirstOrDefault(item => !entity.IsSelfOrSubclassOf(item.Parent));
                if (odd is not null)
                    throw new InvalidOperationException(string.Format("Order property '{0}' belongs to the entity '{1}' while the query only contains entities of type '{2}'.", odd.Name, odd.Parent.Name, entity.Name));

                sb.Append(" ORDER BY ");
                sb.Append(string.Join(", ", orderBy.Select(item => string.Concat("node.", item.Name))));
                if (!ascending)
                    sb.Append(" DESC ");
            }

            if (pageSize > 0)
            {
                sb.Append(" SKIP ");
                sb.Append(page * pageSize);
                sb.Append(" LIMIT ");
                sb.Append(pageSize);
            }
            Dictionary<string, object?>? customState = null;
            var args = entity.RaiseOnNodeLoading(trans, null, sb.ToString(), context.GetParameters(), ref customState);

            var result = Transaction.Run(args.Cypher, args.Parameters);

            return Load<T>(entity, args, result, trans);
        }

        internal List<T> Load<T>(Entity entity, NodeEventArgs args, Persistence.ResultCursor result, Transaction trans)
            where T : class, OGM
        {
            IReadOnlyList<Entity> concretes = entity.GetConcreteClasses();

            List<T> items = new List<T>();
            foreach (var record in result.ToList())
            {
                var node = record["node"]?.As<driver.NodeResult>();
                if (node is null)
                    continue;

                var key = node.Properties[entity.Key.Name];
                if (key is null)
                    continue;

                Entity? concrete = null;
                if (entity.IsAbstract)
                {
                    if (entity.NodeType is not null)
                    {
                        string label = node.Properties[entity.NodeTypeName]!.As<string>();
                        concrete = concretes.FirstOrDefault(item => item.Label.Name == label);
                    }
                    if (concrete is null)
                        concrete = entity.Parent.GetEntity(node.Labels);
                    if (concrete is null)
                        throw new KeyNotFoundException($"Unable to find the concrete class for entity {entity.Name}, labels in DB are: {string.Join(", ", node.Labels)}.");
                }
                else
                {
                    concrete = entity;
                }

                T? wrapper = (T?)Transaction.RunningTransaction.GetEntityByKey(concrete.Name, key);
                if (wrapper is null)
                {
                    wrapper = (T)concrete.Activator();
                    wrapper.SetKey(key);
                    args.Sender = wrapper;
                    args = entity.RaiseOnNodeLoaded(trans, args, node.ElementId, node.Labels, (Dictionary<string, object?>)node.Properties);
                    wrapper.SetData(args.Properties!);
                    wrapper.PersistenceState = PersistenceState.Loaded;
                    wrapper.OriginalPersistenceState = PersistenceState.Loaded;
                }
                else
                {
                    args.Sender = wrapper;
                    args = entity.RaiseOnNodeLoaded(trans, args, node.ElementId, node.Labels, (Dictionary<string, object?>)node.Properties);

                    PersistenceState tmp = wrapper.PersistenceState;
                    if (tmp == PersistenceState.HasUid || tmp == PersistenceState.Loaded || tmp == PersistenceState.Persisted)
                    {
                        wrapper.SetData(args.Properties!);
                        wrapper.PersistenceState = PersistenceState.Loaded;
                        wrapper.OriginalPersistenceState = PersistenceState.Loaded;
                    }
                }
                items.Add(wrapper);
            }
            entity.RaiseOnBatchFinished(trans, args);
            return items;
        }

        internal List<T> Search<T>(Entity entity, string text, Property[] fullTextProperties, int page = 0, int pageSize = 0, bool ascending = true, params Property[] orderBy)
            where T : class, OGM
        {
            Transaction trans = Transaction.RunningTransaction;
            HashSet<string> keys = new HashSet<string>()
            {
               "AND", "OR"
            };

            foreach (string k in keys)
            {
                text = text.Replace(k, string.Concat("\"", k, "\""));
            }

            text = text.Replace("[", @"\\[")
                    .Replace("]", @"\\]")
                    .Replace("(", @"\\(")
                    .Replace(")", @"\\)");

            string search = text.Trim(' ').Replace("  ", " ").Replace(" ", " AND ");


            List<string> queries = new List<string>();
            foreach (var property in fullTextProperties)
            {
                if (!entity.FullTextIndexProperties.Contains(property))
                    throw new ArgumentException("Property {0} is not included in the full text index.");

                queries.Add(string.Format("({0}.{1}:{2})", entity.Label.Name, property.Name, search));
            }

            StringBuilder sb = new StringBuilder();

            if (PersistenceProvider.Major < 4)
            {
                sb.Append("CALL apoc.index.search('fts', '");
                sb.Append(string.Join(" OR ", queries));
                sb.Append("') YIELD node WHERE (node:");
                sb.Append(entity.Label.Name);
                sb.Append(") RETURN DISTINCT node");
            }
            else
            {
                sb.Append("CALL db.index.fulltext.queryNodes('fts', '\"");
                sb.Append(search);
                sb.Append("\"') YIELD node WHERE (node:");
                sb.Append(entity.Label.Name);
                sb.Append(") RETURN DISTINCT node");
            }

            if (orderBy is not null && orderBy.Length != 0)
            {
                Property? odd = orderBy.FirstOrDefault(item => !entity.IsSelfOrSubclassOf(item.Parent));
                if (odd is not null)
                    throw new InvalidOperationException(string.Format("Order property '{0}' belongs to the entity '{1}' while the query only contains entities of type '{2)'.", odd.Name, odd.Parent.Name, entity.Name));

                sb.Append(" ORDER BY ");
                sb.Append(string.Join(", ", orderBy.Select(item => string.Concat("node.", item.Name))));
                if (!ascending)
                    sb.Append(" DESC ");
            }

            if (pageSize > 0)
            {
                sb.Append(" SKIP ");
                sb.Append(page * pageSize);
                sb.Append(" LIMIT ");
                sb.Append(pageSize);
            }

            Dictionary<string, object?>? customState = null;
            var args = entity.RaiseOnNodeLoading(trans, null, sb.ToString(), null, ref customState);

            var result = ((IStatementRunner)trans).Run(args.Cypher, args.Parameters);
            return Load<T>(entity, args, result, trans);
        }

        public bool RelationshipExists(EntityProperty foreignProperty, OGM item)
        {
            string pattern;
            if (foreignProperty.Direction == DirectionEnum.In)
                pattern = "MATCH (node:{0})<-[:{2}]-(:{3}) WHERE node.{1} = $key RETURN node LIMIT 1";
            else
                pattern = "MATCH (node:{0})-[:{2}]->(:{3}) WHERE node.{1} = $key RETURN node LIMIT 1";

            string match = string.Format(
                pattern,
                item.GetEntity().Label.Name,
                item.GetEntity().Key.Name,
                foreignProperty.Relationship?.Neo4JRelationshipType,
                foreignProperty.Parent.Label.Name);

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("key", item.GetKey());

            var result = Transaction.Run(match, parameters);
            return result.FirstOrDefault() is not null;
        }

        private driver.Counters GetStatistics(driver.ResultCursor result)
        {
            return result.Statistics();
        }
    }
}
