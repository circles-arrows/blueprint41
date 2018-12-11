using Blueprint41.Query;
using Blueprint41.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal abstract class NodePersistenceProvider
    {
        protected NodePersistenceProvider(PersistenceProvider factory)
        {
            this.PersistenceProviderFactory = factory;
        }

        public PersistenceProvider PersistenceProviderFactory { get; private set; }

        #region GetAll<T>
        public virtual List<T> GetAll<T>(Entity entity) where T : OGM
        {
            return LoadWhere<T>(entity, null, null, 0, 0);
        }

        public virtual List<T> GetAll<T>(Entity entity, int page = 0, int pageSize = 0, params Property[] orderBy) where T : OGM
        {
            return LoadWhere<T>(entity, null, null, page, pageSize, orderBy);
        }
        #endregion

        #region LoadWhere<T> and Load<T>
        public virtual List<T> LoadWhere<T>(Entity entity, string conditions, Parameter[] parameters, int page = 0, int pageSize = 0, params Property[] orderBy) where T : OGM
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

            IGraphResponse result = Transaction.Run(args.Cypher, args.Parameters);
            return Load<T>(entity, args, result, trans);
        }

        public virtual List<T> LoadWhere<T>(Entity entity, ICompiled query, params Parameter[] parameters) where T : OGM
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

            IGraphResponse result = Transaction.Run(args.Cypher, args.Parameters);
            return Load<T>(entity, args, result, trans);
        }

        protected abstract List<T> Load<T>(Entity entity, NodeEventArgs args, IGraphResponse response, Transaction trans) where T : OGM;
        #endregion

        #region Search
        internal virtual List<T> Search<T>(Entity entity, string text, Property[] properties, int page = 0, int pageSize = 0, params Property[] orderBy) where T : OGM
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
            foreach (var property in properties)
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

            var result = Transaction.Run(args.Cypher, args.Parameters);
            return Load<T>(entity, args, result, trans);
        }
        #endregion

        #region Load
        public virtual void Load(OGM item)
        {
            Transaction trans = Transaction.RunningTransaction;

            string returnStatement = " RETURN node";
            string match = string.Format("MATCH (node:{0}) WHERE node.{1} = {{key}}", item.GetEntity().Label.Name, item.GetEntity().Key.Name);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("key", item.GetKey());

            Dictionary<string, object> customState = null;
            var args = item.GetEntity().RaiseOnNodeLoading(trans, item, match + returnStatement, parameters, ref customState);

            IGraphResponse result = Transaction.Run(args.Cypher, args.Parameters);
            Load(item, result, args);
        }
        protected abstract void Load(OGM item, IGraphResponse response, NodeEventArgs args);
        #endregion

        #region Insert
        public virtual void Insert(OGM item)
        {
            Transaction trans = Transaction.RunningTransaction;
            Entity entity = item.GetEntity();

            string labels = string.Join(":", entity.GetBaseTypesAndSelf().Where(x => x.IsVirtual == false).Select(x => x.Label.Name));

            if (entity.RowVersion != null)
                item.SetRowVersion(trans.TransactionDate);

            IDictionary<string, object> node = item.GetData();

            string create = string.Format("CREATE (inserted:{0} {{node}}) Return inserted", labels);

            // TODO: Check for a better way to check it supports functional Id
            if (PersistenceProvider.TargetFeatures.FunctionalId)
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

            IGraphResponse response = Transaction.Run(args.Cypher, args.Parameters);
            Insert(item, entity, response, args);
        }

        protected abstract void Insert(OGM item, Entity entity, IGraphResponse response, NodeEventArgs args);
        #endregion

        #region Update
        public virtual void Update(OGM item)
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

            IGraphResponse response = Transaction.Run(args.Cypher, args.Parameters);

            Update(response, item, entity);

            entity.RaiseOnNodeUpdated(trans, args);
            item.PersistenceState = PersistenceState.Persisted;
        }

        protected abstract void Update(IGraphResponse response, OGM item, Entity entity);
        #endregion

        #region Delete
        public virtual void Delete(OGM item)
        {
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

            IGraphResponse response = Transaction.Run(args.Cypher, args.Parameters);

            Delete(response, item, entity);

            entity.RaiseOnNodeDeleted(trans, args);
        }

        protected abstract void Delete(IGraphResponse response, OGM item, Entity entity);
        #endregion

        #region ForceDelete
        public virtual void ForceDelete(OGM item)
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

            IGraphResponse response = Transaction.Run(args.Cypher, args.Parameters);
            ForceDelete(response, item, entity);
            entity.RaiseOnNodeDeleted(trans, args);
        }

        protected abstract void ForceDelete(IGraphResponse response, OGM item, Entity entity); 
        #endregion

        public abstract string NextFunctionID(FunctionalId functionalId);

        #region RelationsipExists
        public virtual bool RelationshipExists(Property foreignProperty, OGM item)
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

            IGraphResponse response = Transaction.Run(match, parameters);

            return RelationshipExists(response);
        }

        protected abstract bool RelationshipExists(IGraphResponse response); 
        #endregion
    }
}
