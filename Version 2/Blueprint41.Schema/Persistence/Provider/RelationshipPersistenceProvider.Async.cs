using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Core;
using Blueprint41.Dynamic;
using Blueprint41.Events;
using Blueprint41.Refactoring;
using driver = Blueprint41.Persistence;

namespace Blueprint41.Persistence
{
    internal partial class RelationshipPersistenceProvider
    {
        public async Task AddAsync(Relationship relationship, OGM inItem, OGM outItem, DateTime? moment, bool timedependent, Dictionary<string, object>? properties)
        {
            Transaction trans = Transaction.RunningTransaction;

            Checks(relationship, inItem, outItem);

            if (timedependent)
                await AddAsync(trans, relationship, inItem, outItem, properties, moment ?? Conversion.MinDateTime);
            else
                await AddAsync(trans, relationship, inItem, outItem, properties);
        }
        protected async Task AddAsync(Transaction trans, Relationship relationship, OGM inItem, OGM outItem, Dictionary<string, object>? properties)
        {
            Entity inEntity = inItem.GetEntity();
            Entity outEntity = outItem.GetEntity();

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            string match = string.Format("MATCH (in:{0}) WHERE in.{1} = $inKey \r\n MATCH (out:{2}) WHERE out.{3} = $outKey",
                inEntity.Label.Name,
                inEntity.Key.Name,
                outEntity.Label.Name,
                outEntity.Key.Name);

            string create = $"""
                MERGE (in)-[outr:{relationship.Neo4JRelationshipType}]->(out)
                ON CREATE SET outr = $map
                ON MATCH SET outr = $map, outr.CreationDate = $created
                """;

            long createdDate = Conversion<DateTime, long>.Convert(Transaction.RunningTransaction.TransactionDate);

            Dictionary<string, object> map = properties ?? new Dictionary<string, object>();
            map.AddOrSet(relationship.CreationDate, createdDate);

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("inKey", inItem.GetKey());
            parameters.Add("outKey", outItem.GetKey());
            parameters.Add("map", map);
            parameters.Add("created", createdDate);

            string query = match + "\r\n" + create;

            await ((IStatementRunnerAsync)trans).RunAsync(query, parameters);
        }
        protected async Task AddAsync(Transaction trans, Relationship relationship, OGM inItem, OGM outItem, Dictionary<string, object>? properties, DateTime moment)
        {
            long momentConv = Conversion<DateTime, long>.Convert(moment);
            if (momentConv >= Conversion.MaxDateTimeInMS)
                return; // Adding a relationship in the infinite future is equal to not adding it at all.

            Entity inEntity = inItem.GetEntity();
            Entity outEntity = outItem.GetEntity();

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            string delete = $$"""
                MATCH (in:{{inEntity.Label.Name}} { {{inEntity.Key.Name}}: $inKey })-[rel:{{relationship.Neo4JRelationshipType}}]->(out:{{outEntity.Label.Name}} { {{outEntity.Key.Name}}: $outKey })
                WHERE COALESCE(rel.StartDate, $min) >= $moment
                DELETE rel
                """;

            string update = $$"""
                MATCH (in:{{inEntity.Label.Name}} { {{inEntity.Key.Name}}: $inKey })-[rel:{{relationship.Neo4JRelationshipType}}]->(out:{{outEntity.Label.Name}} { {{outEntity.Key.Name}}: $outKey })
                WHERE COALESCE(rel.StartDate, $min) <= $moment AND COALESCE(rel.EndDate, $max) >= $moment
                WITH rel, properties(rel) AS map1, $map AS map2
                SET rel.EndDate = CASE WHEN {{MapRemoveKeys("map1", "$excl", relationship.ExcludedProperties())}} = {{MapRemoveKeys("map2", "$excl", relationship.ExcludedProperties())}} THEN $max ELSE $moment END
                """;

            string create = $$"""
                MATCH (in:{{inEntity.Label.Name}} { {{inEntity.Key.Name}}: $inKey }), (out:{{outEntity.Label.Name}} { {{outEntity.Key.Name}}: $outKey })
                OPTIONAL MATCH (in)-[rel:{{relationship.Neo4JRelationshipType}}]->(out)
                WHERE COALESCE(rel.StartDate, $min) <= $moment AND COALESCE(rel.EndDate, $max) > $moment
                WITH in, out, rel
                WHERE rel is null
                CREATE (in)-[outr:{{relationship.Neo4JRelationshipType}}]->(out) SET outr = $map
                """;

            Dictionary<string, object> map = properties ?? new Dictionary<string, object>();
            map.AddOrSet(relationship.StartDate, momentConv);
            map.AddOrSet(relationship.EndDate, Conversion.MaxDateTimeInMS);
            map.AddOrSet(relationship.CreationDate, Conversion<DateTime, long>.Convert(Transaction.RunningTransaction.TransactionDate));

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("inKey", inItem.GetKey());
            parameters.Add("outKey", outItem.GetKey());
            parameters.Add("min", Conversion.MinDateTimeInMS);
            parameters.Add("max", Conversion.MaxDateTimeInMS);
            parameters.Add("moment", momentConv);
            parameters.Add("map", map);
            parameters.Add("excl", relationship.ExcludedProperties());

            IStatementRunnerAsync runner = (IStatementRunnerAsync)trans;
            await runner.RunAsync(delete, parameters);
            await runner.RunAsync(update, parameters);
            await runner.RunAsync(create, parameters);
        }

        public async Task RemoveAsync(Relationship relationship, OGM? inItem, OGM? outItem, DateTime? moment, bool timedependent)
        {
            Transaction trans = Transaction.RunningTransaction;

            Checks(relationship, inItem, outItem);

            if (timedependent)
                await RemoveAsync(trans, relationship, inItem, outItem, moment ?? Conversion.MinDateTime);
            else
                await RemoveAsync(trans, relationship, inItem, outItem);
        }
        protected async Task RemoveAsync(Transaction trans, Relationship relationship, OGM? inItem, OGM? outItem)
        {
            string cypher;
            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            if (inItem is not null)
                parameters.Add("inKey", inItem!.GetKey());
            if (outItem is not null)
                parameters.Add("outKey", outItem!.GetKey());

            Entity inEntity = inItem?.GetEntity() ?? relationship.InEntity;
            Entity outEntity = outItem?.GetEntity() ?? relationship.OutEntity;

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            string inLabel = (inItem is null) ? $"in:{inEntity.Label.Name}" : $"in:{inEntity.Label.Name} {{ {inEntity.Key.Name}: $inKey }}";
            string outLabel = (outItem is null) ? $"out:{outEntity.Label.Name}" : $"out:{outEntity.Label.Name} {{ {outEntity.Key.Name}: $outKey }}";

            cypher = $"MATCH ({inLabel})-[r:{relationship.Neo4JRelationshipType}]->({outLabel}) DELETE r";

            await ((IStatementRunnerAsync)trans).RunAsync(cypher, parameters);
        }
        protected async Task RemoveAsync(Transaction trans, Relationship relationship, OGM? inItem, OGM? outItem, DateTime moment)
        {
            long momentConv = Conversion<DateTime, long>.Convert(moment);
            if (momentConv >= Conversion.MaxDateTimeInMS)
                return; // Removing a relationship in the infinite future is equal to not adding it at all.

            Entity inEntity = inItem?.GetEntity() ?? relationship.InEntity;
            Entity outEntity = outItem?.GetEntity() ?? relationship.OutEntity;

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            string inLabel = (inItem is null) ? $"in:{inEntity.Label.Name}" : $"in:{inEntity.Label.Name} {{ {inEntity.Key.Name}: $inKey }}";
            string outLabel = (outItem is null) ? $"out:{outEntity.Label.Name}" : $"out:{outEntity.Label.Name} {{ {outEntity.Key.Name}: $outKey }}";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"MATCH ({inLabel})-[rel:{relationship.Neo4JRelationshipType}]->({outLabel})");
            sb.AppendLine("WHERE COALESCE(rel.StartDate, $min) >= $moment");
            sb.AppendLine("DELETE rel");
            string delete = sb.ToString();

            sb.Clear();
            sb.AppendLine($"MATCH ({inLabel})-[rel:{relationship.Neo4JRelationshipType}]->({outLabel})");
            sb.AppendLine("WHERE COALESCE(rel.StartDate, $min) <= $moment AND COALESCE(rel.EndDate, $max) >= $moment");
            sb.AppendLine("SET rel.EndDate = $moment");
            string update = sb.ToString();

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            if (inItem is not null)
                parameters.Add("inKey", inItem!.GetKey());
            if (outItem is not null)
                parameters.Add("outKey", outItem!.GetKey());

            parameters.Add("min", Conversion.MinDateTimeInMS);
            parameters.Add("max", Conversion.MaxDateTimeInMS);
            parameters.Add("moment", momentConv);

            IStatementRunnerAsync runner = (IStatementRunnerAsync)trans;
            await runner.RunAsync(delete, parameters);
            await runner.RunAsync(update, parameters);
        }

        public async Task AddUnmanagedAsync(Relationship relationship, OGM inItem, OGM outItem, DateTime? startDate, DateTime? endDate, Dictionary<string, object>? properties, bool fullyUnmanaged = false)
        {
            Entity inEntity = inItem.GetEntity();
            Entity outEntity = outItem.GetEntity();

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            if (properties is not null && properties.Count > 0)
                throw new NotImplementedException("Support for setting properties via the unmanaged relationship interface is not implemented yet.");

            Transaction trans = Transaction.RunningTransaction;
            IStatementRunnerAsync runner = (IStatementRunnerAsync)trans;

            Checks(relationship, inItem, outItem);

            if (!fullyUnmanaged)
            {
                string find = string.Format(
                    "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = $inKey and out.{4} = $outKey and (r.{5} <= $endDate OR r.{5} IS NULL) AND (r.{6} > $startDate OR r.{6} IS NULL) RETURN min(COALESCE(r.{5}, $MinDateTime)) as MinStartDate, max(COALESCE(r.{6}, $MaxDateTime)) as MaxEndDate, count(r) as Count",
                    inEntity.Label.Name,
                    relationship.Neo4JRelationshipType,
                    outEntity.Label.Name,
                    inEntity.Key.Name,
                    outEntity.Key.Name,
                    relationship.StartDate,
                    relationship.EndDate);

                Dictionary<string, object?> parameters = new Dictionary<string, object?>();
                parameters.Add("inKey", inItem.GetKey());
                parameters.Add("outKey", outItem.GetKey());
                parameters.Add("startDate", Conversion<DateTime, long>.Convert(startDate ?? DateTime.MinValue));
                parameters.Add("endDate", Conversion<DateTime, long>.Convert(endDate ?? DateTime.MaxValue));
                parameters.Add("MinDateTime", Conversion<DateTime, long>.Convert(DateTime.MinValue));
                parameters.Add("MaxDateTime", Conversion<DateTime, long>.Convert(DateTime.MaxValue));

                driver.ResultCursor result = await runner.RunAsync(find, parameters);
                driver.Record? record = await result.FirstOrDefaultAsync();
                int count = record?["Count"].As<int>() ?? 0;
                if (count > 0)
                {
                    DateTime? minStartDate = Conversion<long?, DateTime?>.Convert(record?["MinStartDate"].As<long?>());
                    DateTime? maxEndDate = Conversion<long?, DateTime?>.Convert(record?["MaxEndDate"].As<long?>());
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
                    inEntity.Key.Name,
                    outEntity.Key.Name,
                    relationship.StartDate,
                    relationship.EndDate);

                await runner.RunAsync(delete, parameters);
            }

            string match = string.Format("MATCH (in:{0}) WHERE in.{1} = $inKey MATCH (out:{2}) WHERE out.{3} = $outKey",
                inEntity.Label.Name,
                inEntity.Key.Name,
                outEntity.Label.Name,
                outEntity.Key.Name);
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

            await runner.RunAsync(query, parameters2);
        }
        public async Task RemoveUnmanagedAsync(Relationship relationship, OGM inItem, OGM outItem, DateTime? startDate)
        {
            Entity inEntity = inItem.GetEntity();
            Entity outEntity = outItem.GetEntity();

            if (inEntity.Key is null || outEntity.Key is null)
                throw new InvalidOperationException("No key has been defined for this entity.");

            Transaction trans = Transaction.RunningTransaction;

            Checks(relationship, inItem, outItem);

            if (!relationship.IsTimeDependent)
                throw new NotSupportedException("EndCurrentRelationship method is only supported for time dependent relationship.");

            string match = string.Format(
                "MATCH (in:{0})-[r:{1}]->(out:{2}) WHERE in.{3} = $inKey and out.{4} = $outKey and COALESCE(r.{5}, $minDateTime) = $moment DELETE r",
                inEntity.Label.Name,
                relationship.Neo4JRelationshipType,
                outEntity.Label.Name,
                inEntity.Key.Name,
                outEntity.Key.Name,
                relationship.StartDate);

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("inKey", inItem.GetKey());
            parameters.Add("outKey", outItem.GetKey());
            parameters.Add("moment", Conversion<DateTime, long>.Convert(startDate ?? DateTime.MinValue));
            parameters.Add("minDateTime", Conversion<DateTime, long>.Convert(DateTime.MinValue));

            await ((IStatementRunnerAsync)trans).RunAsync(match, parameters);
        }
    }
}
