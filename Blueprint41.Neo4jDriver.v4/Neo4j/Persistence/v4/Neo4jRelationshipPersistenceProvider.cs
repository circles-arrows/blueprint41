using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;
using Blueprint41.Dynamic;
using Blueprint41.Neo4j.Refactoring;

namespace Blueprint41.Neo4j.Persistence.Driver.v4
{
    internal class Neo4jRelationshipPersistenceProvider : Void.Neo4jRelationshipPersistenceProvider
    {
        public Neo4jRelationshipPersistenceProvider(PersistenceProvider factory) : base(factory) { }

        private void Checks(Relationship relationship, OGM inItem, OGM outItem)
        {
            if (inItem.GetKey() == null || outItem.GetKey() is null)
                throw new NotImplementedException("Entity should have key to participate in relationships.");

            if (inItem.PersistenceState == PersistenceState.New || outItem.PersistenceState == PersistenceState.New)
                throw new NotImplementedException("New entities should be saved first before it can participate in relationships.");
        }

        public override IEnumerable<CollectionItem> Load(OGM parent, Core.EntityCollectionBase target)
        {
            Entity targetEntity = target.ForeignEntity;
            string[] nodeNames = target.Parent.GetEntity().GetDbNames("node");
            string[] outNames = targetEntity.GetDbNames("out");

            if (nodeNames.Length > 1 && outNames.Length > 1)
                throw new InvalidOperationException("Both ends are virtual entities, this is too expensive to query...");

            List<string> fullMatch = new List<string>();
            for (int nodeIndex = 0; nodeIndex < nodeNames.Length; nodeIndex++)
            {
                for (int outIndex = 0; outIndex < outNames.Length; outIndex++)
                {
                    string pattern = string.Empty;
                    if (target.Direction == DirectionEnum.In)
                        pattern = "MATCH ({0})-[rel:{2}]->({3}) WHERE node.{1} = $key RETURN out, rel";
                    else if (target.Direction == DirectionEnum.Out)
                        pattern = "MATCH ({0})<-[rel:{2}]-({3}) WHERE node.{1} = $key RETURN out, rel";

                    string match = string.Format(pattern,
                       nodeNames[nodeIndex],
                       target.ParentEntity.Key.Name,
                       target.Relationship.Neo4JRelationshipType,
                       outNames[outIndex]);

                    fullMatch.Add(match);
                }
            }

            Dictionary<string, object?> parameters2 = new Dictionary<string, object?>();
            parameters2.Add("key", parent.GetKey());

            List<CollectionItem> items = new List<CollectionItem>();
            var result = Transaction.RunningTransaction.Run(string.Join(" UNION ", fullMatch), parameters2);

            foreach (var record in result)
            {
                RawNode node = record.Values["out"].As<RawNode>();
                if (node is null)
                    continue;

                OGM item = ReadNode(parent, targetEntity, node);
                RawRelationship rel = record.Values["rel"].As<RawRelationship>();

                DateTime? startDate = null;
                DateTime? endDate = null;

                if (target.Relationship.IsTimeDependent)
                {
                    object? value;
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

            HashSet<OGM> parentHashset = new HashSet<OGM>(parents);

            string matchClause = string.Empty;
            if (target.Direction == DirectionEnum.In)
                matchClause = "MATCH ({0})-[rel:{2}]->({3})";
            else if (target.Direction == DirectionEnum.Out)
                matchClause = "MATCH ({0})<-[rel:{2}]-({3})";

            string whereClause = " WHERE node.{1} in ($keys) ";
            string returnClause = " RETURN node as Parent, out as Item ";
            if (target.Relationship.IsTimeDependent)
                returnClause = $" RETURN node as Parent, out as Item, rel.{target.Relationship.StartDate} as StartDate, rel.{target.Relationship.EndDate} as EndDate";

            Entity targetEntity = target.ForeignEntity;

            string[] nodeNames = target.Parent.GetEntity().GetDbNames("node");
            string[] outNames = targetEntity.GetDbNames("out");

            if (nodeNames.Length > 1 && outNames.Length > 1)
                throw new InvalidOperationException("Both ends are virtual entities, this is too expensive to query...");

            List<string> fullMatch = new List<string>();
            for (int nodeIndex = 0; nodeIndex < nodeNames.Length; nodeIndex++)
            {
                for (int outIndex = 0; outIndex < outNames.Length; outIndex++)
                {
                    string match = string.Format(string.Concat(matchClause, whereClause, returnClause),
                    nodeNames[nodeIndex],
                    target.ParentEntity.Key.Name,
                    target.Relationship.Neo4JRelationshipType,
                        outNames[outIndex]);

                    fullMatch.Add(match);
                }
            }

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("keys", parents.Select(item => item.GetKey()).ToArray());

            if (parents.Any(parent => parent.GetEntity() != target.Parent.GetEntity()))
                throw new InvalidOperationException("This code should only load collections of the same concrete parent class.");

            string cypher = string.Join(" UNION ", fullMatch);
            var result = Transaction.RunningTransaction.Run(cypher, parameters);
            List<CollectionItem> items = new List<CollectionItem>();
            foreach (var record in result)
            {
                DateTime? startDate = null;
                DateTime? endDate = null;

                if (target.Relationship.IsTimeDependent)
                {
                    startDate = (record.Values["StartDate"] is not null) ? Conversion<long, DateTime>.Convert((long)record.Values["StartDate"].As<long>()) : (DateTime?)null;
                    endDate = (record.Values["EndDate"] is not null) ? Conversion<long, DateTime>.Convert((long)record.Values["EndDate"].As<long>()) : (DateTime?)null;
                }
                OGM? parent = target.Parent.GetEntity().Map(record.Values["Parent"].As<RawNode>(), NodeMapping.AsWritableEntity);
                OGM? item = targetEntity.Map(record.Values["Item"].As<RawNode>(), NodeMapping.AsWritableEntity);

                if (parent is null || item is null)
                    throw new NotSupportedException("The cypher query expected to have a parent node and a child node.");

                if (parentHashset.Contains(parent))
                    items.Add(target.NewCollectionItem(parent, item, startDate, endDate));
            }

            return CollectionItemList.Get(items);
        }

        private Dictionary<object, List<RawNode>> Load(Entity targetEntity, IEnumerable<object> keys)
        {
            string[] nodeNames = targetEntity.GetDbNames("node");

            List<string> fullMatch = new List<string>();
            for (int nodeIndex = 0; nodeIndex < nodeNames.Length; nodeIndex++)
            {
                string match = string.Format(
                    "MATCH ({0}) WHERE node.{1} in ($keys) RETURN DISTINCT node, node.{1} as key",
                    nodeNames[nodeIndex],
                    targetEntity.Key.Name
                );
                fullMatch.Add(match);
            }

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("keys", keys.Distinct().ToList());
            var result = Transaction.RunningTransaction.Run(string.Join(" UNION ", fullMatch), parameters);

            Dictionary<object, List<RawNode>> retval = new Dictionary<object, List<RawNode>>();
            foreach (var record in result)
            {
                List<RawNode>? items;
                if (!retval.TryGetValue(record.Values["key"].As<object>(), out items))
                {
                    items = new List<RawNode>();
                    retval.Add(record.Values["key"].As<object>(), items);
                }
                items.Add(record.Values["node"].As<RawNode>());
            }

            return retval;
        }
        private OGM ReadNode(OGM parent, Entity targetEntity, RawNode node)
        {
            object? keyObject = null;
            if (targetEntity.Key is not null)
                node.Properties.TryGetValue(targetEntity.Key.Name, out keyObject);

            string? typeName = null;
            if (targetEntity.NodeType is not null)
            {
                object? nodeType;
                if (node.Properties.TryGetValue(targetEntity.NodeType.Name, out nodeType))
                    typeName = nodeType as string;
            }

            if (typeName is null)
            {
                if (!targetEntity.IsAbstract)
                    typeName = targetEntity.Name;
                else
                    typeName = targetEntity.GetConcreteClasses().Where(e => node.Labels.Contains(e.Label.Name)).Select(e => e.Name).FirstOrDefault();
            }

            if (typeName is null)
                throw new NotSupportedException("The concrete type of the node could not be determined.");

            OGM? item = null;
            if (keyObject is not null)
            {
                item = Transaction.RunningTransaction.GetEntityByKey(typeName, keyObject);
                if (item is not null &&
                    (item.PersistenceState == PersistenceState.HasUid
                        ||
                    item.PersistenceState == PersistenceState.Loaded))
                {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                    item.SetData(node.Properties);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                    item.PersistenceState = PersistenceState.Loaded;
                }

                if (item is null)
                {
                    if (targetEntity.Parent.IsUpgraded)
                    {
                        Type type = typeCache.TryGetOrAdd(typeName, key =>
                        {
                            type = parent.GetType().Assembly.GetTypes().FirstOrDefault(x => x.Name == typeName);
                            if (type is null)
                                throw new NotSupportedException();

                            return type;
                        });
                        item = (OGM)Activator.CreateInstance(type)!;
                    }
                    else
                        item = new DynamicEntity(targetEntity, Parser.ShouldExecute);

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                    item.SetData(node.Properties);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                    item.SetKey(keyObject);
                    item.PersistenceState = PersistenceState.Loaded;
                }
            }

            if (item is null)
                throw new InvalidOperationException("Could not load object from node properties.");

            return item;
        }
        private static AtomicDictionary<string, Type> typeCache = new AtomicDictionary<string, Type>();

        public override void Add(Relationship relationship, OGM inItem, OGM outItem, DateTime? moment, bool timedependent)
        {
            Transaction trans = Transaction.RunningTransaction;

            Checks(relationship, inItem, outItem);

            string match = string.Format("MATCH (in:{0}) WHERE in.{1} = $inKey \r\n MATCH (out:{2}) WHERE out.{3} = $outKey",
                inItem.GetEntity().Label.Name,
                inItem.GetEntity().Key.Name,
                outItem.GetEntity().Label.Name,
                outItem.GetEntity().Key.Name);
            string create = string.Format("MERGE (in)-[outr:{0}]->(out) ON CREATE SET outr.CreationDate = ${1} SET outr += $node", relationship.Neo4JRelationshipType, relationship.CreationDate);

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("inKey", inItem.GetKey());
            parameters.Add("outKey", outItem.GetKey());

            Dictionary<string, object> node = new Dictionary<string, object>();
            parameters.Add(relationship.CreationDate, Conversion<DateTime, long>.Convert(Transaction.RunningTransaction.TransactionDate));
            if (timedependent)
            {
                DateTime startDate = moment.HasValue ? moment.Value : DateTime.MinValue;
                node.Add(relationship.StartDate, Conversion<DateTime, long>.Convert(startDate));
                node.Add(relationship.EndDate, Conversion<DateTime, long>.Convert(DateTime.MaxValue));
            }

            parameters.Add("node", node);

            string query = match + "\r\n" + create;
            relationship.RaiseOnRelationCreate(trans);

            RawResult result = trans.Run(query, parameters);

            relationship.RaiseOnRelationCreated(trans);

            //if (result.Summary.Counters.RelationshipsCreated == 0)
            //    throw new ApplicationException($"Unable to create relationship '{relationship.Neo4JRelationshipType}' between {inItem.GetEntity().Label.Name}({inItem.GetKey()}) and {outItem.GetEntity().Label.Name}({outItem.GetKey()})");
        }
        public override void Remove(Relationship relationship, OGM inItem, OGM outItem, DateTime? moment, bool timedependent)
        {
            Transaction trans = Transaction.RunningTransaction;

            Checks(relationship, inItem, outItem);

            string cypher;
            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("inKey", inItem.GetKey());
            parameters.Add("outKey", outItem.GetKey());

            if (timedependent)
            {
                parameters.Add("moment", Conversion<DateTime, long>.Convert(moment ?? DateTime.MinValue));

                // End Current
                cypher = string.Format(
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = $inKey and out.{4} = $outKey and (r.{6} > $moment OR r.{6} IS NULL) AND (r.{5} <= $moment OR r.{5} IS NULL) SET r.EndDate = $moment",
                    inItem.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    outItem.GetEntity().Label.Name,
                    inItem.GetEntity().Key.Name,
                    outItem.GetEntity().Key.Name,
                    relationship.StartDate,
                    relationship.EndDate);

                trans.Run(cypher, parameters);

                // Remove Future
                cypher = string.Format(
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = $inKey and out.{4} = $outKey and r.{5} > $moment DELETE r",
                    inItem.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    outItem.GetEntity().Label.Name,
                    inItem.GetEntity().Key.Name,
                    outItem.GetEntity().Key.Name,
                    relationship.StartDate);

                relationship.RaiseOnRelationDelete(trans);

                RawResult result = trans.Run(cypher, parameters);

                relationship.RaiseOnRelationDeleted(trans);

                if (result.Statistics().RelationshipsDeleted == 0)
                    throw new ApplicationException($"Unable to delete time dependent future relationship '{relationship.Neo4JRelationshipType}' between {inItem.GetEntity().Label.Name}({inItem.GetKey()}) and {outItem.GetEntity().Label.Name}({outItem.GetKey()})");
            }
            else
            {
                cypher = string.Format(
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = $inKey and out.{4} = $outKey DELETE r",
                    inItem.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    outItem.GetEntity().Label.Name,
                    inItem.GetEntity().Key.Name,
                    outItem.GetEntity().Key.Name);

                relationship.RaiseOnRelationDelete(trans);

                RawResult result = trans.Run(cypher, parameters);

                relationship.RaiseOnRelationDeleted(trans);

                if (result.Statistics().RelationshipsDeleted == 0)
                    throw new ApplicationException($"Unable to delete relationship '{relationship.Neo4JRelationshipType}' between {inItem.GetEntity().Label.Name}({inItem.GetKey()}) and {outItem.GetEntity().Label.Name}({outItem.GetKey()})");
            }
        }
        public override void RemoveAll(Relationship relationship, OGM item, DateTime? moment, bool timedependent)
        {
            Transaction trans = Transaction.RunningTransaction;

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("key", item.GetKey());

            DirectionEnum direction = relationship.ComputeDirection(item.GetEntity());
            string match = (direction == DirectionEnum.Out) ? "MATCH (item:{0})<-[r:{1}]-(out)" : "MATCH (item:{0})-[r:{1}]->(out)";
            Entity outEntity = (direction == DirectionEnum.Out) ? relationship.InEntity : relationship.OutEntity;

            string condition = string.Join(" OR ", outEntity.GetDbNames("out"));

            if (timedependent)
            {
                parameters.Add("moment", Conversion<DateTime, long>.Convert(moment ?? DateTime.MinValue));

                // End Current
                string cypher = string.Format(
                    match + " WHERE ({2}) and item.{3} = $key and (r.{5} > $moment OR r.{5} IS NULL) AND (r.{4} <= $moment OR r.{4} IS NULL) SET r.EndDate = $moment",
                    item.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    condition,
                    item.GetEntity().Key.Name,
                    relationship.StartDate,
                    relationship.EndDate);

                trans.Run(cypher, parameters);

                // Remove Future
                cypher = string.Format(
                    match + " WHERE ({2}) and item.{3} = $key and r.{4} > $moment DELETE r",
                    item.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    condition,
                    item.GetEntity().Key.Name,
                    relationship.StartDate);

                trans.Run(cypher, parameters);
                //IResult result = trans.Run(cypher, parameters);
                //if (result.Summary.Counters.RelationshipsDeleted == 0)
                //    throw new ApplicationException($"Unable to delete all time dependent future relationships '{relationship.Neo4JRelationshipType}' related to {item.GetEntity().Label.Name}({item.GetKey()}).");

            }
            else
            {
                string cypher = string.Format(
                    match + " WHERE ({2}) and item.{3} = $key DELETE r",
                    item.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    condition,
                    item.GetEntity().Key.Name);

                trans.Run(cypher, parameters);

                //IResult result = trans.Run(cypher, parameters);
                //if (result.Summary.Counters.RelationshipsDeleted == 0)
                //    throw new ApplicationException($"Unable to remove all relationships '{relationship.Neo4JRelationshipType}' related to {item.GetEntity().Label.Name}({item.GetKey()}).");
            }
        }

        public override void AddUnmanaged(Relationship relationship, OGM inItem, OGM outItem, DateTime? startDate, DateTime? endDate, bool fullyUnmanaged = false)
        {
            Transaction trans = Transaction.RunningTransaction;

            Checks(relationship, inItem, outItem);

            if (!fullyUnmanaged)
            {
                string find = string.Format(
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = $inKey and out.{4} = $outKey and (r.{5} <= $endDate OR r.{5} IS NULL) AND (r.{6} > $startDate OR r.{6} IS NULL) RETURN min(COALESCE(r.{5}, $MinDateTime)) as MinStartDate, max(COALESCE(r.{6}, $MaxDateTime)) as MaxEndDate, count(r) as Count",
                    inItem.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    outItem.GetEntity().Label.Name,
                    inItem.GetEntity().Key.Name,
                    outItem.GetEntity().Key.Name,
                    relationship.StartDate,
                    relationship.EndDate);

                Dictionary<string, object?> parameters = new Dictionary<string, object?>();
                parameters.Add("inKey", inItem.GetKey());
                parameters.Add("outKey", outItem.GetKey());
                parameters.Add("startDate", Conversion<DateTime, long>.Convert(startDate ?? DateTime.MinValue));
                parameters.Add("endDate", Conversion<DateTime, long>.Convert(endDate ?? DateTime.MaxValue));
                parameters.Add("MinDateTime", Conversion<DateTime, long>.Convert(DateTime.MinValue));
                parameters.Add("MaxDateTime", Conversion<DateTime, long>.Convert(DateTime.MaxValue));

                RawResult result = trans.Run(find, parameters);
                RawRecord record = result.FirstOrDefault();
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
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = $inKey and out.{4} = $outKey and (r.{5} <= $endDate) AND (r.{6} > $startDate) DELETE r",
                    inItem.GetEntity().Label.Name,
                    relationship.Neo4JRelationshipType,
                    outItem.GetEntity().Label.Name,
                    inItem.GetEntity().Key.Name,
                    outItem.GetEntity().Key.Name,
                    relationship.StartDate,
                    relationship.EndDate);

                trans.Run(delete, parameters);
            }

            string match = string.Format("MATCH (in:{0}) WHERE in.{1} = $inKey MATCH (out:{2}) WHERE out.{3} = $outKey",
                inItem.GetEntity().Label.Name,
                inItem.GetEntity().Key.Name,
                outItem.GetEntity().Label.Name,
                outItem.GetEntity().Key.Name);
            string create = string.Format("CREATE (in)-[outr:{0} $node]->(out)", relationship.Neo4JRelationshipType);

            Dictionary<string, object> node = new Dictionary<string, object>();
            node.Add(relationship.CreationDate, Conversion<DateTime, long>.Convert(Transaction.RunningTransaction.TransactionDate));
            if (relationship.IsTimeDependent)
            {
                node.Add(relationship.StartDate, Conversion<DateTime, long>.Convert(startDate ?? DateTime.MinValue));
                node.Add(relationship.EndDate, Conversion<DateTime, long>.Convert(endDate ?? DateTime.MaxValue));
            }

            Dictionary<string, object?> parameters2 = new Dictionary<string, object?>();
            parameters2.Add("inKey", inItem.GetKey());
            parameters2.Add("outKey", outItem.GetKey());
            parameters2.Add("node", node);

            string query = match + "\r\n" + create;

            relationship.RaiseOnRelationCreate(trans);

            trans.Run(query, parameters2);

            relationship.RaiseOnRelationCreated(trans);
        }
        public override void RemoveUnmanaged(Relationship relationship, OGM inItem, OGM outItem, DateTime? moment)
        {
            Transaction trans = Transaction.RunningTransaction;

            Checks(relationship, inItem, outItem);

            if (relationship.IsTimeDependent == false)
                throw new NotSupportedException("EndCurrentRelationship method is only supported for time dependent relationship.");

            string match = string.Format(
                "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = $inKey and out.{4} = $outKey and COALESCE(r.{5}, $minDateTime) = $moment DELETE r",
                inItem.GetEntity().Label.Name,
                relationship.Neo4JRelationshipType,
                outItem.GetEntity().Label.Name,
                inItem.GetEntity().Key.Name,
                outItem.GetEntity().Key.Name,
                relationship.StartDate);

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("inKey", inItem.GetKey());
            parameters.Add("outKey", outItem.GetKey());
            parameters.Add("moment", Conversion<DateTime, long>.Convert(moment ?? DateTime.MinValue));
            parameters.Add("minDateTime", Conversion<DateTime, long>.Convert(DateTime.MinValue));

            relationship.RaiseOnRelationDelete(trans);

            trans.Run(match, parameters);

            relationship.RaiseOnRelationDeleted(trans);
        }
    }
}
