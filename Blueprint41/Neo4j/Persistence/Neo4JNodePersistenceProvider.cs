using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neo4j.Driver.V1;

using Blueprint41.Core;
using System.Diagnostics;
using Blueprint41.Query;
using System.Data;
using Blueprint41.Response;

namespace Blueprint41.Neo4j.Persistence
{
    internal class Neo4JNodePersistenceProvider : NodePersistenceProvider
    {
        public Neo4JNodePersistenceProvider(PersistenceProvider factory) : base(factory) { }

        public override List<T> GetAll<T>(Entity entity)
        {
            return LoadWhere<T>(entity, null, null, 0, 0);
        }
        public override List<T> GetAll<T>(Entity entity, int page, int pageSize, params Property[] orderBy)
        {
            return LoadWhere<T>(entity, null, null, page, pageSize, orderBy);
        }

        public override void Load(OGM item)
        {
            Transaction trans = Transaction.RunningTransaction;

            string returnStatement = " RETURN node";
            string match = string.Format("MATCH (node:{0}) WHERE node.{1} = {{key}}", item.GetEntity().Label.Name, item.GetEntity().Key.Name);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("key", item.GetKey());

            Dictionary<string, object> customState = null;
            var args = item.GetEntity().RaiseOnNodeLoading(trans, item, match + returnStatement, parameters, ref customState);

            var result = Neo4jTransaction.Run(args.Cypher, args.Parameters);

            IRecord record = result.FirstOrDefault();
            if (record == null)
            {
                item.PersistenceState = PersistenceState.DoesntExist;
                return;
            }

            INode loaded = record["node"].As<INode>();

            args.Id = loaded.Id;
            args.Labels = loaded.Labels;
            // HACK: To make it faster we do not copy/replicate the Dictionary here, but it means someone
            //       could be changing the INode content from within an event. Possibly dangerous, but
            //       turns out the Neo4j driver can deal with it ... for now ... 
            args = item.GetEntity().RaiseOnNodeLoaded(trans, args, loaded.Id, loaded.Labels, (Dictionary<string, object>)loaded.Properties);

            if (item.PersistenceState == PersistenceState.HasUid || item.PersistenceState == PersistenceState.Loaded)
            {
                item.SetData(args.Properties);
                item.PersistenceState = PersistenceState.Loaded;
            }
        }

        public override void Delete(OGM item)
        {
            if (PersistenceProvider.TargetFeatures.Delete == false)
                throw new NotSupportedException();

            Transaction trans = Transaction.RunningTransaction;
            Entity entity = item.GetEntity();

            string match;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("key", item.GetKey());

            if (entity.RowVersion == null)
            {
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = {{key}} DELETE node", entity.Label.Name, entity.Key.Name);
            }
            else
            {
                parameters.Add("lockId", Conversion<DateTime, long>.Convert(item.GetRowVersion()));
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = {{key}} AND node.{2} = {{lockId}} DELETE node", entity.Label.Name, entity.Key.Name, entity.RowVersion.Name);
            }

            Dictionary<string, object> customState = null;
            var args = entity.RaiseOnNodeDelete(trans, item, match, parameters, ref customState);

            IGraphResponse result = Transaction.Run(args.Cypher, args.Parameters);

            // TODO: Implement Delete Gremlin result counters
            if (result.Result is GremlinResult grem)
                if (grem.StatusCode != 200)
                    throw new DBConcurrencyException(grem.ErrorMessage);

            if (result.Result is IStatementResult statementResult)
                if (statementResult.Summary.Counters.NodesDeleted == 0)
                    throw new DBConcurrencyException($"The {entity.Name} with {entity.Key.Name} '{item.GetKey()?.ToString() ?? "<NULL>"}' was changed or deleted by another process or thread.");

            entity.RaiseOnNodeDeleted(trans, args);
        }

        public override void ForceDelete(OGM item)
        {
            Transaction trans = Transaction.RunningTransaction;
            Entity entity = item.GetEntity();

            string match;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("key", item.GetKey());

            if (entity.RowVersion == null)
            {
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = {{key}} DETACH DELETE node", entity.Label.Name, entity.Key.Name);
            }
            else
            {
                parameters.Add("lockId", Conversion<DateTime, long>.Convert(item.GetRowVersion()));
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = {{key}} AND node.{2} = {{lockId}} DETACH DELETE node", entity.Label.Name, entity.Key.Name, entity.RowVersion.Name);
            }

            Dictionary<string, object> customState = null;
            var args = entity.RaiseOnNodeDelete(trans, item, match, parameters, ref customState);

            IGraphResponse result = Transaction.Run(args.Cypher, args.Parameters);

            // TODO: Implement Force Delete Gremlin result
            if (result.Result is GremlinResult grem)
                if (grem.StatusCode != 200)
                    throw new DBConcurrencyException(grem.ErrorMessage);

            if (result.Result is IStatementResult statementResult)
                if (statementResult.Summary.Counters.NodesDeleted == 0)
                    throw new DBConcurrencyException($"The {entity.Name} with {entity.Key.Name} '{item.GetKey()?.ToString() ?? "<NULL>"}' was changed or deleted by another process or thread.");

            entity.RaiseOnNodeDeleted(trans, args);
        }

        public override void Insert(OGM item)
        {
            Transaction trans = Transaction.RunningTransaction;
            Entity entity = item.GetEntity();

            string labels = string.Join(":", entity.GetBaseTypesAndSelf().Where(x => x.IsVirtual == false).Select(x => x.Label.Name));

            if (entity.RowVersion != null)
                item.SetRowVersion(trans.TransactionDate);

            IDictionary<string, object> node = item.GetData();

            string create = string.Format("CREATE (inserted:{0} {{node}}) Return inserted", labels);

            if (PersistenceProvider.TargetFeatures.Cypher)
            {
                if (item.GetKey() == null && entity.FunctionalId != null)
                {
                    string nextKey = string.Format("CALL blueprint41.functionalid.next('{0}') YIELD value as key", entity.FunctionalId.Label);
                    if (entity.FunctionalId.Format == IdFormat.Numeric)
                        nextKey = string.Format("CALL blueprint41.functionalid.nextNumeric('{0}') YIELD value as key", entity.FunctionalId.Label);

                    create = nextKey + "\r\n" + string.Format("CREATE (inserted:{0} {{node}}) SET inserted.{1} = key Return inserted", labels, entity.Key.Name);

                    node.Remove(entity.Key.Name);
                }
                else if (entity.FunctionalId != null)
                {
                    entity.FunctionalId.SeenUid(item.GetKey().ToString());
                }
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("node", node);

            Dictionary<string, object> customState = null;
            var args = entity.RaiseOnNodeCreate(trans, item, create, parameters, ref customState);

            var result = Transaction.Run(args.Cypher, args.Parameters);
            IRecord record = result.FirstOrDefault();
            if (record == null)
                throw new InvalidOperationException($"Due to an unexpected state of the neo4j transaction, it seems impossible to insert the {entity.Name} at this time.");

            INode inserted = record["inserted"].As<INode>();

            // TODO: Support for Gremlin Id
            args.Id = inserted.Id;
            args.Labels = inserted.Labels;
            // HACK: To make it faster we do not copy/replicate the Dictionary here, but it means someone
            //       could be changing the INode content from within an event. Possibly dangerous, but
            //       turns out the Neo4j driver can deal with it ... for now ... 
            args.Properties = (Dictionary<string, object>)inserted.Properties;
            args = entity.RaiseOnNodeCreated(trans, args, inserted.Id, inserted.Labels, (Dictionary<string, object>)inserted.Properties);

            item.SetData(args.Properties);
            item.PersistenceState = PersistenceState.Persisted;
        }

        public override void Update(OGM item)
        {
            Transaction trans = Transaction.RunningTransaction;
            Entity entity = item.GetEntity();

            string match;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("key", item.GetKey());

            if (entity.RowVersion == null)
            {
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = {{key}} SET node = {{newValues}}", entity.Label.Name, entity.Key.Name);
            }
            else
            {
                parameters.Add("lockId", Conversion<DateTime, long>.Convert(item.GetRowVersion()));
                match = string.Format("MATCH (node:{0}) WHERE node.{1} = {{key}} AND node.{2} = {{lockId}} SET node = {{newValues}}", entity.Label.Name, entity.Key.Name, entity.RowVersion.Name);
                item.SetRowVersion(trans.TransactionDate);
            }

            IDictionary<string, object> node = item.GetData();
            parameters.Add("newValues", node);

            Dictionary<string, object> customState = null;
            var args = entity.RaiseOnNodeUpdate(trans, item, match, parameters, ref customState);

            IGraphResponse result = Neo4jTransaction.Run(args.Cypher, args.Parameters);

            // TODO: Implement Force Delete Gremlin result

            if (result.Result is IStatementResult statementResult)
                if (!statementResult.Summary.Counters.ContainsUpdates)
                    throw new DBConcurrencyException($"The {entity.Name} with {entity.Key.Name} '{item.GetKey()?.ToString() ?? "<NULL>"}' was changed or deleted by another process or thread.");

            entity.RaiseOnNodeUpdated(trans, args);
            item.PersistenceState = PersistenceState.Persisted;
        }

        public override string NextFunctionID(FunctionalId functionalId)
        {
            if (functionalId == null)
                throw new ArgumentNullException("functionalId");

            string nextKey = string.Format("CALL blueprint41.functionalid.next('{0}') YIELD value as key", functionalId.Label);
            if (functionalId.Format == IdFormat.Numeric)
                nextKey = string.Format("CALL blueprint41.functionalid.nextNumeric('{0}') YIELD value as key", functionalId.Label);

            var result = Neo4jTransaction.Run(nextKey).First();
            return result["key"].ToString();
        }

        public override List<T> LoadWhere<T>(Entity entity, string conditions, Parameter[] parameters, int page, int pageSize, params Property[] orderBy)
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

            if (orderBy != null && orderBy.Length != 0)
            {
                Property odd = orderBy.FirstOrDefault(item => !entity.IsSelfOrSubclassOf(item.Parent));
                if (odd != null)
                    throw new InvalidOperationException(string.Format("Order property '{0}' belongs to the entity '{1}' while the query only contains entities of type '{2)'.", odd.Name, odd.Parent.Name, entity.Name));

                sb.Append(" ORDER BY ");
                sb.Append(string.Join(", ", orderBy.Select(item => string.Concat("node.", item.Name))));
            }

            if (pageSize > 0)
            {
                sb.Append(" SKIP ");
                sb.Append(page * pageSize);
                sb.Append(" LIMIT ");
                sb.Append(pageSize);
            }

            Dictionary<string, object> customState = null;
            Dictionary<string, object> arguments = new Dictionary<string, object>();
            if (parameters != null)
                foreach (Parameter parameter in parameters)
                    arguments.Add(parameter.Name, parameter.Value);

            var args = entity.RaiseOnNodeLoading(trans, null, sb.ToString(), arguments, ref customState);

            var result = Neo4jTransaction.Run(args.Cypher, args.Parameters);
            return Load<T>(entity, args, result, trans);
        }
        public override List<T> LoadWhere<T>(Entity entity, ICompiled query, params Parameter[] parameters)
        {
            Transaction trans = Transaction.RunningTransaction;

            QueryExecutionContext context = query.GetExecutionContext();
            foreach (Parameter queryParameter in parameters)
            {
                if ((object)queryParameter.Value == null)
                    context.SetParameter(queryParameter.Name, null);
                else
                    context.SetParameter(queryParameter.Name, trans.ConvertToStoredType(queryParameter.Value.GetType(), queryParameter.Value));
            }

            Dictionary<string, object> customState = null;
            var args = entity.RaiseOnNodeLoading(trans, null, context.CompiledQuery.QueryText, context.QueryParameters, ref customState);

            var result = Neo4jTransaction.Run(args.Cypher, args.Parameters);

            return Load<T>(entity, args, result, trans);
        }

        private List<T> Load<T>(Entity entity, NodeEventArgs args, IGraphResponse response, Transaction trans) where T : OGM
        {
            List<Entity> concretes = entity.GetConcreteClasses();

            List<T> items = new List<T>();
            foreach (var record in response)
            {
                var node = record[0].As<INode>();
                if (node == null)
                    continue;

                var key = node.Properties[entity.Key.Name];

                Entity concrete = null;
                if (entity.IsAbstract)
                {
                    if (entity.NodeType != null)
                    {
                        string label = node.Properties[entity.NodeTypeName].As<string>();
                        concrete = concretes.FirstOrDefault(item => item.Label.Name == label);
                    }
                    if (concrete == null)
                        concrete = GetEntity(entity.Parent, node.Labels);
                    if (concrete == null)
                        throw new KeyNotFoundException($"Unable to find the concrete class for entity {entity.Name}, labels in DB are: {string.Join(", ", node.Labels)}.");
                }
                else
                {
                    concrete = entity;
                }

                T wrapper = (T)Transaction.RunningTransaction.GetEntityByKey(concrete.Name, key);
                if (wrapper == null)
                {
                    wrapper = Activator<T>(concrete);
                    wrapper.SetKey(key);
                    args.Sender = wrapper;
                    args = entity.RaiseOnNodeLoaded(trans, args, node.Id, node.Labels, (Dictionary<string, object>)node.Properties);
                    wrapper.SetData(args.Properties);
                    wrapper.PersistenceState = PersistenceState.Loaded;
                }
                else
                {
                    args.Sender = wrapper;
                    args = entity.RaiseOnNodeLoaded(trans, args, node.Id, node.Labels, (Dictionary<string, object>)node.Properties);
                    if (wrapper.PersistenceState == PersistenceState.HasUid || wrapper.PersistenceState == PersistenceState.Loaded || wrapper.PersistenceState == PersistenceState.Persisted)
                    {
                        wrapper.SetData(args.Properties);
                        wrapper.PersistenceState = PersistenceState.Loaded;
                    }
                }
                items.Add(wrapper);
            }
            entity.RaiseOnBatchFinished(trans, args);
            return items;
        }

        internal override List<T> Search<T>(Entity entity, string text, Property[] fullTextProperties, int page = 0, int pageSize = 0, params Property[] orderBy)
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

            string search = text.Trim(' ', '(', ')').Replace("  ", " ").Replace(" ", " AND ");


            List<string> queries = new List<string>();
            foreach (var property in fullTextProperties)
            {
                if (entity.FullTextIndexProperties.Contains(property) == false)
                    throw new ArgumentException("Property {0} is not included in the full text index.");

                queries.Add(string.Format("({0}.{1}:{2})", entity.Label.Name, property.Name, search));
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("CALL apoc.index.search('fts', '");
            sb.Append(string.Join(" OR ", queries));
            sb.Append("') YIELD node WHERE (node:");
            sb.Append(entity.Label.Name);
            sb.Append(") RETURN DISTINCT node");

            if (orderBy != null && orderBy.Length != 0)
            {
                Property odd = orderBy.FirstOrDefault(item => !entity.IsSelfOrSubclassOf(item.Parent));
                if (odd != null)
                    throw new InvalidOperationException(string.Format("Order property '{0}' belongs to the entity '{1}' while the query only contains entities of type '{2)'.", odd.Name, odd.Parent.Name, entity.Name));

                sb.Append(" ORDER BY ");
                sb.Append(string.Join(", ", orderBy.Select(item => string.Concat("node.", item.Name))));
            }

            if (pageSize > 0)
            {
                sb.Append(" SKIP ");
                sb.Append(page * pageSize);
                sb.Append(" LIMIT ");
                sb.Append(pageSize);
            }

            Dictionary<string, object> customState = null;
            var args = entity.RaiseOnNodeLoading(trans, null, sb.ToString(), null, ref customState);

            var result = Neo4jTransaction.Run(args.Cypher, args.Parameters);
            return Load<T>(entity, args, result, trans);
        }

        public override bool RelationshipExists(Property foreignProperty, OGM item)
        {
            string pattern;
            if (foreignProperty.Direction == DirectionEnum.In)
                pattern = "MATCH (node:{0})<-[:{2}]-(:{3}) WHERE node.{1} = {{key}} RETURN node LIMIT 1";
            else
                pattern = "MATCH (node:{0})-[:{2}]->(:{3}) WHERE node.{1} = {{key}} RETURN node LIMIT 1";

            string match = string.Format(
                pattern,
                item.GetEntity().Label.Name,
                item.GetEntity().Key.Name,
                foreignProperty.Relationship.Neo4JRelationshipType,
                foreignProperty.Parent.Label.Name);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("key", item.GetKey());

            var result = Neo4jTransaction.Run(match, parameters);
            return result.Any();
        }

        static private T Activator<T>(Entity entity)
        {
            if (entity.IsAbstract)
                throw new NotSupportedException($"You cannot instantiate the abstract entity {entity.Name}.");

            Func<OGM> activator;
            if (!activators.TryGetValue(entity.Name, out activator))
            {
                lock (typeof(Neo4JNodePersistenceProvider))
                {
                    if (!activators.TryGetValue(entity.Name, out activator))
                    {
                        foreach (Type type in typeof(T).Assembly.GetTypes())
                        {
                            if (type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(OGM<,,>))
                            {
                                OGM instance = (OGM)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(type);
                                Entity entityInstance = instance.GetEntity();
                                if (entityInstance.IsAbstract)
                                    continue;

                                activators.Add(entityInstance.Name, delegate ()
                                {
                                    return System.Activator.CreateInstance(type) as OGM;
                                });
                            }
                        }
                        activator = activators[entity.Name];
                    }
                }
            }
            return (T)Transaction.Execute(() => activator.Invoke(), EventOptions.GraphEvents);
        }
        static private Dictionary<string, Func<OGM>> activators = new Dictionary<string, Func<OGM>>();

        static private Dictionary<string, Entity> entityByLabel = new Dictionary<string, Entity>();
        static private Entity GetEntity(DatastoreModel datastore, IEnumerable<string> labels)
        {
            Entity entity = null;
            foreach (string label in labels)
            {
                if (!entityByLabel.TryGetValue(label, out entity))
                {
                    lock (entityByLabel)
                    {
                        if (!entityByLabel.TryGetValue(label, out entity))
                        {
                            entity = datastore.Entities.FirstOrDefault(item => item.Label.Name == label);
                            entityByLabel.Add(label, entity);
                        }
                    }
                }

                if (!entity.IsAbstract)
                    return entity;
            }
            return null;
        }
    }
}