using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.Core;
using Neo4j.Driver.V1;
using Blueprint41.Dynamic;
using Blueprint41.Neo4j.Refactoring;

namespace Blueprint41.Neo4j.Persistence
{
    internal class Neo4JRelationshipPersistenceProvider : RelationshipPersistenceProvider
    {
        public Neo4JRelationshipPersistenceProvider(PersistenceProvider factory) : base(factory) { }

        private void Checks(Relationship relationship, OGM inItem, OGM outItem)
        {
            if (inItem.GetKey() == null || outItem.GetKey() == null)
                throw new NotImplementedException("Entity should have key to participate in relationships.");

            if (inItem.PersistenceState == PersistenceState.New || outItem.PersistenceState == PersistenceState.New)
                throw new NotImplementedException("New entities should be saved first before it can participate in relationships.");
        }

        public override IEnumerable<CollectionItem> Load(OGM parent, Core.EntityCollectionBase target)
        {
            string pattern = string.Empty;
            if (target.Direction == DirectionEnum.In)
                pattern = "MATCH ({0})-[rel:{2}]->({3}) WHERE node.{1} = {{key}} RETURN out, rel";
            else if(target.Direction == DirectionEnum.Out)
                pattern = "MATCH ({0})<-[rel:{2}]-({3}) WHERE node.{1} = {{key}} RETURN out, rel";

            Entity targetEntity = target.ForeignEntity;
            string match = string.Format(pattern,
               target.Parent.GetEntity().GetDbName("node"),
               target.ParentEntity.Key.Name,
               target.Relationship.Neo4JRelationshipType,
               targetEntity.GetDbName("out"));

            Dictionary<string, object> parameters2 = new Dictionary<string, object>();
            parameters2.Add("key", parent.GetKey());

            List<CollectionItem> items = new List<CollectionItem>();
            var result = Neo4jTransaction.Run(match, parameters2);

            foreach (var record in result)
            {
                OGM item = ReadNode(parent, targetEntity, record.Values["out"].As<INode>());
                IRelationship rel = record.Values["rel"].As<IRelationship>();

                DateTime? startDate = null;
                DateTime? endDate = null;

                if (target.Relationship.IsTimeDependent)
                {
                    object value;
                    if (rel.Properties.TryGetValue(target.Relationship.StartDate, out value))
                        startDate = Conversion<long, DateTime>.Convert((long)value);
                    if (rel.Properties.TryGetValue(target.Relationship.EndDate, out value))
                        endDate = Conversion<long, DateTime>.Convert((long)value);
                }

                items.Add(target.NewCollectionItem(parent, item, startDate, endDate));
            }

            return items;
        }

        public override Dictionary<OGM, CollectionItemList> Load(IEnumerable<OGM> parents, Core.EntityCollectionBase target)
        {
            if (parents.Count() == 0)
                return new Dictionary<OGM, CollectionItemList>();

            Dictionary<object, OGM> parentDict = new Dictionary<object, OGM>();
            foreach (OGM parent in parents)
            {
                if(!parentDict.ContainsKey(parent.GetKey()))
                    parentDict.Add(parent.GetKey(), parent);
            }

            string pattern = string.Empty;
            if (target.Direction == DirectionEnum.In)
                pattern = "MATCH ({0})-[rel:{2}]->({3}) WHERE node.{1} in ({{keys}}) RETURN out, rel, node.{1} as ParentKey";
            else if (target.Direction == DirectionEnum.Out)
                pattern = "MATCH ({0})<-[rel:{2}]-({3}) WHERE node.{1} in ({{keys}}) RETURN out, rel, node.{1} as ParentKey";

            Entity targetEntity = target.ForeignEntity;
            string match = string.Format(pattern,
               target.Parent.GetEntity().GetDbName("node"),
               target.ParentEntity.Key.Name,
               target.Relationship.Neo4JRelationshipType,
               targetEntity.GetDbName("out"));

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("keys", parents.Select(item => item.GetKey()).ToArray());

            if (parents.Any(parent => parent.GetEntity() != target.Parent.GetEntity()))
                throw new InvalidOperationException("This code should only load collections of the same concrete parent class.");

            List<CollectionItem> items = new List<CollectionItem>();
            var result = Neo4jTransaction.Run(match, parameters);

            foreach (var record in result)
            {
                OGM parent = parentDict[record.Values["ParentKey"].As<object>()];
                OGM item = ReadNode(parent, targetEntity, record.Values["out"].As<INode>());
                IRelationship rel = record.Values["rel"].As<IRelationship>();

                DateTime? startDate = null; ;
                DateTime? endDate = null;

                if (target.Relationship.IsTimeDependent)
                {
                    object value;
                    if (rel.Properties.TryGetValue(target.Relationship.StartDate, out value))
                        startDate = Conversion<long, DateTime>.Convert((long)value);
                    if (rel.Properties.TryGetValue(target.Relationship.EndDate, out value))
                        endDate = Conversion<long, DateTime>.Convert((long)value);
                }

                items.Add(target.NewCollectionItem(parent, item, startDate, endDate));
            }

            return CollectionItemList.Get(items);
        }

        private OGM ReadNode(OGM parent, Entity targetEntity, INode node)
        {
            object keyObject;
            node.Properties.TryGetValue(targetEntity.Key?.Name, out keyObject);

            string typeName = null;
            if (targetEntity.NodeType != null)
            {
                object nodeType;
                if (node.Properties.TryGetValue(targetEntity.NodeType.Name, out nodeType))
                    typeName = nodeType as string;
            }

            if (typeName == null)
            {
                if (!targetEntity.IsAbstract)
                    typeName = targetEntity.Name;
                else
                    typeName = targetEntity.GetConcreteClasses().Where(e => node.Labels.Contains(e.Label.Name)).Select(e => e.Name).FirstOrDefault();
            }

            if (typeName == null)
                throw new NotSupportedException("The concrete type of the node could not be determined.");

            Type type;
            if (!typeCache.TryGetValue(typeName, out type))
            {
                lock (typeCache)
                {
                    if (!typeCache.TryGetValue(typeName, out type))
                    {
                        type = parent.GetType().Assembly.GetTypes().FirstOrDefault(x => x.Name == typeName);
                        if (type == null)
                            throw new NotSupportedException();

                        typeCache.Add(typeName, type);
                    }
                }
            }

            OGM item = null;
            if (keyObject != null)
            {
                item = Transaction.Current.GetEntityByKey(typeName, keyObject);
                if (item != null && 
                    (item.PersistenceState == PersistenceState.HasUid 
                        || 
                    item.PersistenceState == PersistenceState.Loaded))
                {
                    item.SetData(node.Properties);
                    item.PersistenceState = PersistenceState.Loaded;
                }
            }

            if (item == null)
            {
                if(targetEntity.Parent.IsUpgraded)
                    item = (OGM)Activator.CreateInstance(type);
                else
                    item = new DynamicEntity(targetEntity, Parser.ShouldExecute);

                item.SetData(node.Properties);
                item.SetKey(item.GetKey());
                item.PersistenceState = PersistenceState.Loaded;
            }

            return item;
        }
        private static Dictionary<string, Type> typeCache = new Dictionary<string, Type>();

        public override void Add(Relationship relationship, OGM inItem, OGM outItem, DateTime? moment, bool timedependent)
        {
            Checks(relationship, inItem, outItem);

            string match = string.Format("MATCH (in:{0} {{{1}:{{inKey}} }}) \r\n MATCH (out:{2} {{{3}:{{outKey}} }})",
                inItem.GetEntity().Label.Name,
                inItem.GetEntity().Key.Name,
                outItem.GetEntity().Label.Name,
                outItem.GetEntity().Key.Name);
            string create = string.Format("MERGE (in)-[outr:{0}]->(out) ON CREATE SET outr += {{node}}", relationship.Neo4JRelationshipType);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("inKey", inItem.GetKey());
            parameters.Add("outKey", outItem.GetKey());

            Dictionary<string, object> node = new Dictionary<string, object>();
            node.Add(relationship.CreationDate, Conversion<DateTime, long>.Convert(Transaction.Current.TransactionDate));
            if (timedependent)
            {
                DateTime startDate = moment.HasValue ? moment.Value : DateTime.MinValue;
                node.Add(relationship.StartDate, Conversion<DateTime, long>.Convert(startDate));
                node.Add(relationship.EndDate, Conversion<DateTime, long>.Convert(DateTime.MaxValue));
            }

            parameters.Add("node", node);

            string query = match + "\r\n" + create;
            Neo4jTransaction.Run(query, parameters);
        }
        public override void Remove(Relationship relationship, OGM inItem, OGM outItem, DateTime? moment, bool timedependent)
        {
            Checks(relationship, inItem, outItem);

            string cypher;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("inKey", inItem.GetKey());
            parameters.Add("outKey", outItem.GetKey());

            if (timedependent)
            {
                parameters.Add("moment", Conversion<DateTime, long>.Convert(moment ?? DateTime.MinValue));

                // End Current
                cypher = string.Format(
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = {{inKey}} and out.{4} = {{outKey}} and (r.{6} > {{moment}} OR r.{6} IS NULL) AND (r.{5} <={{moment}} OR r.{5} IS NULL) SET r.EndDate = {{moment}}",
                    inItem.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    outItem.GetEntity().Label.Name,
                    inItem.GetEntity().Key.Name,
                    outItem.GetEntity().Key.Name,
                    relationship.StartDate,
                    relationship.EndDate);

                Neo4jTransaction.Run(cypher, parameters);

                // Remove Future
                cypher = string.Format(
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = {{inKey}} and out.{4} = {{outKey}} and r.{5} > {{moment}} DELETE r",
                    inItem.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    outItem.GetEntity().Label.Name,
                    inItem.GetEntity().Key.Name,
                    outItem.GetEntity().Key.Name,
                    relationship.StartDate);

                Neo4jTransaction.Run(cypher, parameters);
            }
            else
            {
                cypher = string.Format(
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = {{inKey}} and out.{4} = {{outKey}} DELETE r",
                    inItem.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    outItem.GetEntity().Label.Name,
                    inItem.GetEntity().Key.Name,
                    outItem.GetEntity().Key.Name);

                Neo4jTransaction.Run(cypher, parameters);
            }
        }
        public override void RemoveAll(Relationship relationship, DirectionEnum direction, OGM item, DateTime? moment, bool timedependent)
        {
            string cypher;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("key", item.GetKey());

            string match = (direction == DirectionEnum.Out) ? "MATCH (item:{0})<-[r:{1}]-(useless)" : "MATCH (item:{0})-[r:{1}]->(useless)";

            if (timedependent)
            {
                parameters.Add("moment", Conversion<DateTime, long>.Convert(moment ?? DateTime.MinValue));
                
                // End Current
                cypher = string.Format(
                    match + " WHERE item.{2} = {{key}} and (r.{4} > {{moment}} OR r.{4} IS NULL) AND (r.{3} <={{moment}} OR r.{3} IS NULL) SET r.EndDate = {{moment}}",
                    item.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    item.GetEntity().Key.Name,
                    relationship.StartDate,
                    relationship.EndDate);

                Neo4jTransaction.Run(cypher, parameters);

                // Remove Future
                cypher = string.Format(
                    match + " WHERE item.{2} = {{key}} and r.{3} > {{moment}} DELETE r",
                    item.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    item.GetEntity().Key.Name,
                    relationship.StartDate);

                Neo4jTransaction.Run(cypher, parameters);
            }
            else
            {
                cypher = string.Format(
                    match + " WHERE item.{2} = {{key}} DELETE r",
                    item.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    item.GetEntity().Key.Name);

                Neo4jTransaction.Run(cypher, parameters);
            }
        }

        public override void AddUnmanaged(Relationship relationship, OGM inItem, OGM outItem, DateTime? startDate, DateTime? endDate, bool fullyUnmanaged = false)
        {
            Checks(relationship, inItem, outItem);

            if (!fullyUnmanaged)
            {
                string find = string.Format(
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = {{inKey}} and out.{4} = {{outKey}} and (r.{5} <= {{endDate}} OR r.{5} IS NULL) AND (r.{6} > {{startDate}} OR r.{6} IS NULL) RETURN min(COALESCE(r.{5}, {{MinDateTime}})) as MinStartDate, max(COALESCE(r.{6}, {{MaxDateTime}})) as MaxEndDate, count(r) as Count",
                    inItem.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    outItem.GetEntity().Label.Name,
                    inItem.GetEntity().Key.Name,
                    outItem.GetEntity().Key.Name,
                    relationship.StartDate,
                    relationship.EndDate);

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("inKey", inItem.GetKey());
                parameters.Add("outKey", outItem.GetKey());
                parameters.Add("startDate", Conversion<DateTime, long>.Convert(startDate ?? DateTime.MinValue));
                parameters.Add("endDate", Conversion<DateTime, long>.Convert(endDate ?? DateTime.MaxValue));
                parameters.Add("MinDateTime", Conversion<DateTime, long>.Convert(DateTime.MinValue));
                parameters.Add("MaxDateTime", Conversion<DateTime, long>.Convert(DateTime.MaxValue));

                IStatementResult result = Neo4jTransaction.Run(find, parameters);
                IRecord record = result.FirstOrDefault();
                int count = record["Count"].As<int>();
                if (count > 0)
                {
                    DateTime? minStartDate = Conversion<long?, DateTime?>.Convert(record["MinStartDate"].As<long?>());
                    DateTime? maxEndDate = Conversion<long?, DateTime?>.Convert(record["MaxEndDate"].As<long?>());
                    if (startDate > minStartDate)
                        startDate = minStartDate ?? DateTime.MinValue;

                    if (endDate < maxEndDate)
                        endDate = maxEndDate ?? DateTime.MaxValue;
                }

                string delete = string.Format(
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = {{inKey}} and out.{4} = {{outKey}} and (r.{5} <= {{endDate}}) AND (r.{6} > {{startDate}}) DELETE r",
                    inItem.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    outItem.GetEntity().Label.Name,
                    inItem.GetEntity().Key.Name,
                    outItem.GetEntity().Key.Name,
                    relationship.StartDate,
                    relationship.EndDate);

                Neo4jTransaction.Run(delete, parameters);
            }

            string match = string.Format("MATCH (in:{0} {{{1}:{{inKey}} }}) \r\n MATCH (out:{2} {{{3}:{{outKey}} }})",
                inItem.GetEntity().Label.Name,
                inItem.GetEntity().Key.Name,
                outItem.GetEntity().Label.Name,
                outItem.GetEntity().Key.Name);
            string create = string.Format("MERGE (in)-[outr:{0} {{node}}]->(out) ON CREATE SET outr += {{node}}", relationship.Neo4JRelationshipType);

            Dictionary<string, object> node = new Dictionary<string, object>();
            node.Add(relationship.CreationDate, Conversion<DateTime, long>.Convert(Transaction.Current.TransactionDate));
            if (relationship.IsTimeDependent)
            {
                node.Add(relationship.StartDate, Conversion<DateTime, long>.Convert(startDate ?? DateTime.MinValue));
                node.Add(relationship.EndDate, Conversion<DateTime, long>.Convert(endDate ?? DateTime.MaxValue));
            }

            Dictionary<string, object> parameters2 = new Dictionary<string, object>();
            parameters2.Add("inKey", inItem.GetKey());
            parameters2.Add("outKey", outItem.GetKey());
            parameters2.Add("node", node);

            string query = match + "\r\n" + create;
            Neo4jTransaction.Run(query, parameters2);
        }
        public override void RemoveUnmanaged(Relationship relationship, OGM inItem, OGM outItem, DateTime? moment)
        {
            Checks(relationship, inItem, outItem);

            if (relationship.IsTimeDependent == false)
                throw new NotSupportedException("EndCurrentRelationship method is only supported for time dependent relationship.");

            string match = string.Format(
                "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = {{inKey}} and out.{4} = {{outKey}} and COALESCE(r.{5}, {{minDateTime}}) = {{moment}} DELETE r",
                inItem.GetEntity().Label.Name,
                relationship.Neo4JRelationshipType,
                outItem.GetEntity().Label.Name,
                inItem.GetEntity().Key.Name,
                outItem.GetEntity().Key.Name,
                relationship.StartDate);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("inKey", inItem.GetKey());
            parameters.Add("outKey", outItem.GetKey());
            parameters.Add("moment", Conversion<DateTime, long>.Convert(moment ?? DateTime.MinValue));
            parameters.Add("minDateTime", Conversion<DateTime, long>.Convert(DateTime.MinValue));

            Neo4jTransaction.Run(match, parameters);
        }

    }
}
