#nullable enable

using System;
using System.Linq;

namespace Blueprint41.Neo4j.Model
{
    public abstract partial class QueryTranslator
    {
        #region apoc

        public virtual string Call_Apoc_Case(int count) => string.Format("apoc.case({0})", Args(2, 3, count));
        public virtual string Call_Apoc_Help            => "apoc.help({0})";
        public virtual string Call_Apoc_When(int count) => string.Format("apoc.when({0})", Args(3, 4, count));
        public virtual string Fn_Apoc_Version           => "apoc.version()";

        #endregion

        #region apoc.agg

        public virtual string Fn_ApocAgg_First       => "apoc.agg.first({0})";
        public virtual string Fn_ApocAgg_Graph       => "apoc.agg.graph({0})";
        public virtual string Fn_ApocAgg_Last        => "apoc.agg.last({0})";
        public virtual string Fn_ApocAgg_MaxItems    => "apoc.agg.maxItems({0}, {1}, {2})";
        public virtual string Fn_ApocAgg_Median      => "apoc.agg.median({0})";
        public virtual string Fn_ApocAgg_MinItems    => "apoc.agg.minItems({0}, {1}, {2})";
        public virtual string Fn_ApocAgg_Nth         => "apoc.agg.nth({0})";
        public virtual string Fn_ApocAgg_Percentiles => "apoc.agg.percentiles({0}, {1})";
        public virtual string Fn_ApocAgg_Product     => "apoc.agg.product({0})";
        public virtual string Fn_ApocAgg_Slice       => "apoc.agg.slice({0}, {1}, {2})";
        public virtual string Fn_ApocAgg_Statistics  => "apoc.agg.statistics({0}, {1})";

        #endregion

        #region apoc.algo

        public virtual string Call_ApocAlgo_AllSimplePaths            => "apoc.algo.allSimplePaths({0}, {1}, {2}, {3})";
        public virtual string Call_ApocAlgo_AStar                     => "apoc.algo.aStar({0}, {1}, {2}, {3}, {4}, {5})";
        public virtual string Call_ApocAlgo_AStarConfig               => "apoc.algo.aStarConfig({0}, {1}, {2}, {3})";
        public virtual string Call_ApocAlgo_Cover                     => "apoc.algo.cover({0})";
        public virtual string Call_ApocAlgo_Dijkstra                  => "apoc.algo.dijkstra({0}, {1}, {2}, {3}, {4}, {5})";
        public virtual string Call_ApocAlgo_DijkstraWithDefaultWeight => "apoc.algo.dijkstraWithDefaultWeight({0}, {1}, {2}, {3}, {4})";

        #endregion

        #region apoc.any

        public virtual string Fn_ApocAny_Properties => "apoc.any.properties({0}, {1})";
        public virtual string Fn_ApocAny_Property   => "apoc.any.property({0}, {1})";

        #endregion

        #region apoc.atomic

        public virtual string Call_ApocAtomic_Add(int count)      => string.Format("apoc.atomic.add({0})", Args(3, 4, count));
        public virtual string Call_ApocAtomic_Concat(int count)   => string.Format("apoc.atomic.concat({0})", Args(3, 4, count));
        public virtual string Call_ApocAtomic_Insert              => "apoc.atomic.insert({0}, {1}, {2}, {3}, {4})";
        public virtual string Call_ApocAtomic_Remove(int count)   => string.Format("apoc.atomic.remove({0})", Args(3, 4, count));
        public virtual string Call_ApocAtomic_Subtract(int count) => string.Format("apoc.atomic.subtract({0})", Args(3, 4, count));
        public virtual string Call_ApocAtomic_Update(int count)   => string.Format("apoc.atomic.update({0})", Args(3, 4, count));

        #endregion

        #region apoc.bitwise

        public virtual string Fn_ApocBitwise_Op => "apoc.bitwise.op({0}, {1}, {2})";

        #endregion

        #region apoc.bolt

        public virtual string Call_ApocBolt_Execute => "apoc.bolt.execute({0}, {1}, {2}, {3})";

        #endregion

        #region apoc.coll

        public virtual string Call_ApocColl_Elements             => "apoc.coll.elements({0}, {1}, {2})";
        public virtual string Call_ApocColl_PairWithOffset       => "apoc.coll.pairWithOffset({0}, {1})";
        public virtual string Call_ApocColl_Partition            => "apoc.coll.partition({0}, {1})";
        public virtual string Call_ApocColl_Split                => "apoc.coll.split({0}, {1})";
        public virtual string Call_ApocColl_ZipToRows            => "apoc.coll.zipToRows({0}, {1})";
        public virtual string Fn_ApocColl_Avg                    => "apoc.coll.avg({0})";
        public virtual string Fn_ApocColl_Combinations           => "apoc.coll.combinations({0}, {1}, {2})";
        public virtual string Fn_ApocColl_Contains               => "apoc.coll.contains({0}, {1})";
        public virtual string Fn_ApocColl_ContainsAll            => "apoc.coll.containsAll({0}, {1})";
        public virtual string Fn_ApocColl_ContainsAllSorted      => "apoc.coll.containsAllSorted({0}, {1})";
        public virtual string Fn_ApocColl_ContainsDuplicates     => "apoc.coll.containsDuplicates({0})";
        public virtual string Fn_ApocColl_ContainsSorted         => "apoc.coll.containsSorted({0}, {1})";
        public virtual string Fn_ApocColl_Different(int count)   => string.Format("apoc.coll.different([{0}])", Args(count));
        public virtual string Fn_ApocColl_Disjunction            => "apoc.coll.disjunction({0}, {1})";
        public virtual string Fn_ApocColl_DropDuplicateNeighbors => "apoc.coll.dropDuplicateNeighbors({0})";
        public virtual string Fn_ApocColl_Duplicates             => "apoc.coll.duplicates({0})";
        public virtual string Fn_ApocColl_DuplicatesWithCount    => "apoc.coll.duplicatesWithCount({0})";
        public virtual string Fn_ApocColl_Fill                   => "apoc.coll.fill({0}, {1})";
        public virtual string Fn_ApocColl_Flatten(int count)     => string.Format("apoc.coll.flatten({0})", Args(1, 2, count));
        public virtual string Fn_ApocColl_Frequencies            => "apoc.coll.frequencies({0})";
        public virtual string Fn_ApocColl_FrequenciesAsMap       => "apoc.coll.frequenciesAsMap({0})";
        public virtual string Fn_ApocColl_IndexOf                => "apoc.coll.indexOf({0}, {1})";
        public virtual string Fn_ApocColl_Insert                 => "apoc.coll.insert({0}, {1}, {2})";
        public virtual string Fn_ApocColl_InsertAll              => "apoc.coll.insertAll({0}, {1}, {2})";
        public virtual string Fn_ApocColl_Intersection           => "apoc.coll.intersection({0}, {1})";
        public virtual string Fn_ApocColl_IsEqualCollection      => "apoc.coll.isEqualCollection({0}, {1})";
        public virtual string Fn_ApocColl_Max(int count)         => string.Format("apoc.coll.max([{0}])", Args(count));
        public virtual string Fn_ApocColl_Min(int count)         => string.Format("apoc.coll.min([{0}])", Args(count));
        public virtual string Fn_ApocColl_Occurrences            => "apoc.coll.occurrences({0}, {1})";
        public virtual string Fn_ApocColl_Pairs                  => "apoc.coll.pairs({0})";
        public virtual string Fn_ApocColl_PairsMin               => "apoc.coll.pairsMin({0})";
        public virtual string Fn_ApocColl_RandomItem             => "apoc.coll.randomItem({0})";
        public virtual string Fn_ApocColl_RandomItems            => "apoc.coll.randomItems({0}, {1}, {2})";
        public virtual string Fn_ApocColl_Remove(int count)      => string.Format("apoc.coll.remove({0})", Args(2, 3, count));
        public virtual string Fn_ApocColl_RemoveAll              => "apoc.coll.removeAll({0}, {1})";
        public virtual string Fn_ApocColl_Reverse                => "apoc.coll.reverse({0})";
        public virtual string Fn_ApocColl_RunningTotal           => "apoc.coll.runningTotal({0})";
        public virtual string Fn_ApocColl_Set                    => "apoc.coll.set({0}, {1}, {2})";
        public virtual string Fn_ApocColl_Shuffle                => "apoc.coll.shuffle({0})";
        public virtual string Fn_ApocColl_Sort                   => "apoc.coll.sort({0})";
        public virtual string Fn_ApocColl_SortMaps               => "apoc.coll.sortMaps({0}, {1})";
        public virtual string Fn_ApocColl_SortMulti(int count)   => string.Format("apoc.coll.sortMulti({0})", Args(1, 4, count));
        public virtual string Fn_ApocColl_SortNodes              => "apoc.coll.sortNodes({0}, {1})";
        public virtual string Fn_ApocColl_SortText(int count)    => string.Format("apoc.coll.sortText({0})", Args(1, 2, count));
        public virtual string Fn_ApocColl_Stdev                  => "apoc.coll.stdev({0}, {1})";
        public virtual string Fn_ApocColl_Subtract               => "apoc.coll.subtract({0}, {1})";
        public virtual string Fn_ApocColl_Sum                    => "apoc.coll.sum({0})";
        public virtual string Fn_ApocColl_SumLongs               => "apoc.coll.sumLongs({0})";
        public virtual string Fn_ApocColl_ToSet(int count)       => string.Format("apoc.coll.toSet([{0}])", Args(count));
        public virtual string Fn_ApocColl_Union                  => "apoc.coll.union({0}, {1})";
        public virtual string Fn_ApocColl_UnionAll               => "apoc.coll.unionAll({0}, {1})";
        public virtual string Fn_ApocColl_Zip                    => "apoc.coll.zip({0}, {1})";

        #endregion

        #region apoc.convert

        public virtual string Call_ApocConvert_SetJsonProperty             => "apoc.convert.setJsonProperty({0}, {1}, {2})";
        public virtual string Call_ApocConvert_ToTree                      => "apoc.convert.toTree({0}, {1}, {2})";
        public virtual string Fn_ApocConvert_FromJsonList(int count)       => string.Format("apoc.convert.fromJsonList({0})", Args(2, 3, count));
        public virtual string Fn_ApocConvert_FromJsonMap(int count)        => string.Format("apoc.convert.fromJsonMap({0})", Args(2, 3, count));
        public virtual string Fn_ApocConvert_GetJsonProperty(int count)    => string.Format("apoc.convert.getJsonProperty({0})", Args(3, 4, count));
        public virtual string Fn_ApocConvert_GetJsonPropertyMap(int count) => string.Format("apoc.convert.getJsonPropertyMap({0})", Args(3, 4, count));
        public virtual string Fn_ApocConvert_ToBoolean                     => "apoc.convert.toBoolean({0})";
        public virtual string Fn_ApocConvert_ToBooleanList                 => "apoc.convert.toBooleanList({0})";
        public virtual string Fn_ApocConvert_ToFloat                       => "apoc.convert.toFloat({0})";
        public virtual string Fn_ApocConvert_ToInteger                     => "apoc.convert.toInteger({0})";
        public virtual string Fn_ApocConvert_ToIntList                     => "apoc.convert.toIntList({0})";
        public virtual string Fn_ApocConvert_ToJson                        => "apoc.convert.toJson({0})";
        public virtual string Fn_ApocConvert_ToList                        => "apoc.convert.toList({0})";
        public virtual string Fn_ApocConvert_ToMap                         => "apoc.convert.toMap({0})";
        public virtual string Fn_ApocConvert_ToNode                        => "apoc.convert.toNode({0})";
        public virtual string Fn_ApocConvert_ToNodeList                    => "apoc.convert.toNodeList({0})";
        public virtual string Fn_ApocConvert_ToRelationship                => "apoc.convert.toRelationship({0})";
        public virtual string Fn_ApocConvert_ToRelationshipList            => "apoc.convert.toRelationshipList({0})";
        public virtual string Fn_ApocConvert_ToSet                         => "apoc.convert.toSet({0})";
        public virtual string Fn_ApocConvert_ToSortedJsonMap               => "apoc.convert.toSortedJsonMap({0}, {1})";
        public virtual string Fn_ApocConvert_ToString                      => "apoc.convert.toString({0})";
        public virtual string Fn_ApocConvert_ToStringList                  => "apoc.convert.toStringList({0})";

        #endregion

        #region apoc.create

        public virtual string Call_ApocCreate_AddLabels           => "apoc.create.addLabels({0}, {1})";
        public virtual string Call_ApocCreate_ClonePathsToVirtual => "apoc.create.clonePathsToVirtual({0})";
        public virtual string Call_ApocCreate_ClonePathToVirtual  => "apoc.create.clonePathToVirtual({0})";
        public virtual string Call_ApocCreate_Node                => "apoc.create.node({0}, {1})";
        public virtual string Call_ApocCreate_Nodes               => "apoc.create.nodes({0}, {1})";
        public virtual string Call_ApocCreate_Relationship        => "apoc.create.relationship({0}, {1}, {2}, {3})";
        public virtual string Call_ApocCreate_RemoveLabels        => "apoc.create.removeLabels({0}, {1})";
        public virtual string Call_ApocCreate_RemoveProperties    => "apoc.create.removeProperties({0}, {1})";
        public virtual string Call_ApocCreate_RemoveRelProperties => "apoc.create.removeRelProperties({0}, {1})";
        public virtual string Call_ApocCreate_SetLabels           => "apoc.create.setLabels({0}, {1})";
        public virtual string Call_ApocCreate_SetProperties       => "apoc.create.setProperties({0}, {1}, {2})";
        public virtual string Call_ApocCreate_SetProperty         => "apoc.create.setProperty({0}, {1}, {2})";
        public virtual string Call_ApocCreate_SetRelProperties    => "apoc.create.setRelProperties({0}, {1}, {2})";
        public virtual string Call_ApocCreate_SetRelProperty      => "apoc.create.setRelProperty({0}, {1}, {2})";
        public virtual string Call_ApocCreate_Uuids               => "apoc.create.uuids({0})";
        public virtual string Call_ApocCreate_VirtualPath         => "apoc.create.virtualPath({0}, {1}, {2}, {3}, {4}, {5})";
        public virtual string Call_ApocCreate_VNode(int count)    => string.Format("apoc.create.vNode({0})", Args(1, 2, count));
        public virtual string Call_ApocCreate_VNodes              => "apoc.create.vNodes({0}, {1})";
        public virtual string Call_ApocCreate_VPattern            => "apoc.create.vPattern({0}, {1}, {2}, {3})";
        public virtual string Call_ApocCreate_VPatternFull        => "apoc.create.vPatternFull({0}, {1}, {2}, {3}, {4}, {5})";
        public virtual string Call_ApocCreate_VRelationship       => "apoc.create.vRelationship({0}, {1}, {2}, {3})";
        public virtual string Fn_ApocCreate_Uuid                  => "apoc.create.uuid()";

        #endregion

        #region apoc.create.virtual

        public virtual string Fn_ApocCreateVirtual_FromNode => "apoc.create.virtual.fromNode({0}, {1})";

        #endregion

        #region apoc.cypher

        public virtual string Call_ApocCypher_DoIt               => "apoc.cypher.doIt({0}, {1})";
        public virtual string Call_ApocCypher_Run                => "apoc.cypher.run({0}, {1})";
        public virtual string Call_ApocCypher_RunMany            => "apoc.cypher.runMany({0}, {1}, {2})";
        public virtual string Call_ApocCypher_RunManyReadOnly    => "apoc.cypher.runManyReadOnly({0}, {1}, {2})";
        public virtual string Call_ApocCypher_RunSchema          => "apoc.cypher.runSchema({0}, {1})";
        public virtual string Call_ApocCypher_RunTimeboxed       => "apoc.cypher.runTimeboxed({0}, {1}, {2})";
        public virtual string Call_ApocCypher_RunWrite           => "apoc.cypher.runWrite({0}, {1})";
        public virtual string Fn_ApocCypher_RunFirstColumn       => "apoc.cypher.runFirstColumn({0}, {1}, {2})";
        public virtual string Fn_ApocCypher_RunFirstColumnMany   => "apoc.cypher.runFirstColumnMany({0}, {1})";
        public virtual string Fn_ApocCypher_RunFirstColumnSingle => "apoc.cypher.runFirstColumnSingle({0}, {1})";

        #endregion

        #region apoc.data

        public virtual string Fn_ApocData_Domain => "apoc.data.domain({0})";
        public virtual string Fn_ApocData_Url    => "apoc.data.url({0})";

        #endregion

        #region apoc.date

        public virtual string Fn_ApocDate_Add                             => "apoc.date.add({0}, {1}, {2}, {3})";
        public virtual string Fn_ApocDate_Convert                         => "apoc.date.convert({0}, {1}, {2})";
        public virtual string Fn_ApocDate_ConvertFormat                   => "apoc.date.convertFormat({0}, {1}, {2})";
        public virtual string Fn_ApocDate_CurrentTimestamp                => "apoc.date.currentTimestamp()";
        public virtual string Fn_ApocDate_Field(int count)                => string.Format("apoc.date.field({0})", Args(1, 3, count));
        public virtual string Fn_ApocDate_Fields(int count)               => string.Format("apoc.date.fields({0})", Args(1, 2, count));
        public virtual string Fn_ApocDate_Format(int count)               => string.Format("apoc.date.format({0})", Args(3, 4, count));
        public virtual string Fn_ApocDate_FromISO8601                     => "apoc.date.fromISO8601({0})";
        public virtual string Fn_ApocDate_Parse(int count)                => string.Format("apoc.date.parse({0})", Args(3, 4, count));
        public virtual string Fn_ApocDate_ParseAsZonedDateTime(int count) => string.Format("apoc.date.parseAsZonedDateTime({0})", Args(2, 3, count));
        public virtual string Fn_ApocDate_SystemTimezone                  => "apoc.date.systemTimezone()";
        public virtual string Fn_ApocDate_ToISO8601(int count)            => string.Format("apoc.date.toISO8601({0})", Args(1, 2, count));
        public virtual string Fn_ApocDate_ToYears                         => "apoc.date.toYears({0}, {1})";

        #endregion

        #region apoc.diff

        public virtual string Fn_ApocDiff_Nodes => "apoc.diff.nodes({0}, {1})";

        #endregion

        #region apoc.do

        public virtual string Call_ApocDo_Case(int count) => string.Format("apoc.do.case({0})", Args(2, 3, count));
        public virtual string Call_ApocDo_When(int count) => string.Format("apoc.do.when({0})", Args(3, 4, count));

        #endregion

        #region apoc.dv

        public virtual string Call_ApocDv_Query(int count) => string.Format("apoc.dv.query({0})", Args(2, 3, count));
        public virtual string Call_ApocDv_QueryAndLink     => "apoc.dv.queryAndLink({0}, {1}, {2}, {3}, {4})";

        #endregion

        #region apoc.dv.catalog

        public virtual string Call_ApocDvCatalog_Add    => "apoc.dv.catalog.add({0}, {1})";
        public virtual string Call_ApocDvCatalog_List   => "apoc.dv.catalog.list()";
        public virtual string Call_ApocDvCatalog_Remove => "apoc.dv.catalog.remove({0})";

        #endregion

        #region apoc.example

        public virtual string Call_ApocExample_Movies => "apoc.example.movies()";

        #endregion

        #region apoc.export

        public virtual string Call_ApocExport_CypherAll   => "apoc.export.cypherAll({0}, {1})";
        public virtual string Call_ApocExport_CypherData  => "apoc.export.cypherData({0}, {1}, {2}, {3})";
        public virtual string Call_ApocExport_CypherGraph => "apoc.export.cypherGraph({0}, {1}, {2})";
        public virtual string Call_ApocExport_CypherQuery => "apoc.export.cypherQuery({0}, {1}, {2})";

        #endregion

        #region apoc.export.csv

        public virtual string Call_ApocExportCsv_All   => "apoc.export.csv.all({0}, {1})";
        public virtual string Call_ApocExportCsv_Data  => "apoc.export.csv.data({0}, {1}, {2}, {3})";
        public virtual string Call_ApocExportCsv_Graph => "apoc.export.csv.graph({0}, {1}, {2})";
        public virtual string Call_ApocExportCsv_Query => "apoc.export.csv.query({0}, {1}, {2})";

        #endregion

        #region apoc.export.cypher

        public virtual string Call_ApocExportCypher_All    => "apoc.export.cypher.all({0}, {1})";
        public virtual string Call_ApocExportCypher_Data   => "apoc.export.cypher.data({0}, {1}, {2}, {3})";
        public virtual string Call_ApocExportCypher_Graph  => "apoc.export.cypher.graph({0}, {1}, {2})";
        public virtual string Call_ApocExportCypher_Query  => "apoc.export.cypher.query({0}, {1}, {2})";
        public virtual string Call_ApocExportCypher_Schema => "apoc.export.cypher.schema({0}, {1})";

        #endregion

        #region apoc.export.graphml

        public virtual string Call_ApocExportGraphml_All   => "apoc.export.graphml.all({0}, {1})";
        public virtual string Call_ApocExportGraphml_Data  => "apoc.export.graphml.data({0}, {1}, {2}, {3})";
        public virtual string Call_ApocExportGraphml_Graph => "apoc.export.graphml.graph({0}, {1}, {2})";
        public virtual string Call_ApocExportGraphml_Query => "apoc.export.graphml.query({0}, {1}, {2})";

        #endregion

        #region apoc.export.json

        public virtual string Call_ApocExportJson_All   => "apoc.export.json.all({0}, {1})";
        public virtual string Call_ApocExportJson_Data  => "apoc.export.json.data({0}, {1}, {2}, {3})";
        public virtual string Call_ApocExportJson_Graph => "apoc.export.json.graph({0}, {1}, {2})";
        public virtual string Call_ApocExportJson_Query => "apoc.export.json.query({0}, {1}, {2})";

        #endregion

        #region apoc.graph

        public virtual string Call_ApocGraph_From             => "apoc.graph.from({0}, {1}, {2})";
        public virtual string Call_ApocGraph_FromCypher       => "apoc.graph.fromCypher({0}, {1}, {2}, {3})";
        public virtual string Call_ApocGraph_FromData         => "apoc.graph.fromData({0}, {1}, {2}, {3})";
        public virtual string Call_ApocGraph_FromDB           => "apoc.graph.fromDB({0}, {1})";
        public virtual string Call_ApocGraph_FromDocument     => "apoc.graph.fromDocument({0}, {1})";
        public virtual string Call_ApocGraph_FromPath         => "apoc.graph.fromPath({0}, {1}, {2})";
        public virtual string Call_ApocGraph_FromPaths        => "apoc.graph.fromPaths({0}, {1}, {2})";
        public virtual string Call_ApocGraph_ValidateDocument => "apoc.graph.validateDocument({0}, {1})";

        #endregion

        #region apoc.hashing

        public virtual string Fn_ApocHashing_Fingerprint                 => "apoc.hashing.fingerprint({0}, {1})";
        public virtual string Fn_ApocHashing_FingerprintGraph(int count) => string.Format("apoc.hashing.fingerprintGraph({0})", Args(0, 1, count));
        public virtual string Fn_ApocHashing_Fingerprinting              => "apoc.hashing.fingerprinting({0}, {1})";

        #endregion

        #region apoc.import

        public virtual string Call_ApocImport_Csv     => "apoc.import.csv({0}, {1}, {2})";
        public virtual string Call_ApocImport_Graphml => "apoc.import.graphml({0}, {1})";
        public virtual string Call_ApocImport_Json    => "apoc.import.json({0}, {1})";
        public virtual string Call_ApocImport_Xml     => "apoc.import.xml({0}, {1})";

        #endregion

        #region apoc.json

        public virtual string Fn_ApocJson_Path(int count) => string.Format("apoc.json.path({0})", Args(1, 3, count));

        #endregion

        #region apoc.label

        public virtual string Fn_ApocLabel_Exists => "apoc.label.exists({0}, {1})";

        #endregion

        #region apoc.load

        public virtual string Call_ApocLoad_Json       => "apoc.load.json({0}, {1}, {2})";
        public virtual string Call_ApocLoad_JsonArray  => "apoc.load.jsonArray({0}, {1}, {2})";
        public virtual string Call_ApocLoad_JsonParams => "apoc.load.jsonParams({0}, {1}, {2}, {3}, {4})";
        public virtual string Call_ApocLoad_Xml        => "apoc.load.xml({0}, {1}, {2}, {3})";

        #endregion

        #region apoc.lock

        public virtual string Call_ApocLock_All   => "apoc.lock.all({0}, {1})";
        public virtual string Call_ApocLock_Nodes => "apoc.lock.nodes({0})";
        public virtual string Call_ApocLock_Rels  => "apoc.lock.rels({0})";

        #endregion

        #region apoc.lock.read

        public virtual string Call_ApocLockRead_Nodes => "apoc.lock.read.nodes({0})";
        public virtual string Call_ApocLockRead_Rels  => "apoc.lock.read.rels({0})";

        #endregion

        #region apoc.log

        public virtual string Call_ApocLog_Stream => "apoc.log.stream({0}, {1})";

        #endregion

        #region apoc.map

        public virtual string Fn_ApocMap_Clean                 => "apoc.map.clean({0}, {1}, {2})";
        public virtual string Fn_ApocMap_Flatten               => "apoc.map.flatten({0}, {1})";
        public virtual string Fn_ApocMap_FromLists             => "apoc.map.fromLists({0}, {1})";
        public virtual string Fn_ApocMap_FromNodes             => "apoc.map.fromNodes({0}, {1})";
        public virtual string Fn_ApocMap_FromPairs             => "apoc.map.fromPairs({0})";
        public virtual string Fn_ApocMap_FromValues(int count) => string.Format("apoc.map.fromValues([{0}])", Args(count));
        public virtual string Fn_ApocMap_Get                   => "apoc.map.get({0}, {1}, {2}, {3})";
        public virtual string Fn_ApocMap_GroupBy               => "apoc.map.groupBy({0}, {1})";
        public virtual string Fn_ApocMap_GroupByMulti          => "apoc.map.groupByMulti({0}, {1})";
        public virtual string Fn_ApocMap_Merge                 => "apoc.map.merge({0}, {1})";
        public virtual string Fn_ApocMap_MergeList             => "apoc.map.mergeList({0})";
        public virtual string Fn_ApocMap_Mget(int count)       => string.Format("apoc.map.mget({0})", Args(3, 4, count));
        public virtual string Fn_ApocMap_RemoveKey             => "apoc.map.removeKey({0}, {1}, {2})";
        public virtual string Fn_ApocMap_RemoveKeys            => "apoc.map.removeKeys({0}, {1}, {2})";
        public virtual string Fn_ApocMap_SetEntry              => "apoc.map.setEntry({0}, {1}, {2})";
        public virtual string Fn_ApocMap_SetKey                => "apoc.map.setKey({0}, {1}, {2})";
        public virtual string Fn_ApocMap_SetLists              => "apoc.map.setLists({0}, {1}, {2})";
        public virtual string Fn_ApocMap_SetPairs              => "apoc.map.setPairs({0}, {1})";
        public virtual string Fn_ApocMap_SetValues             => "apoc.map.setValues({0}, {1})";
        public virtual string Fn_ApocMap_SortedProperties      => "apoc.map.sortedProperties({0}, {1})";
        public virtual string Fn_ApocMap_Submap(int count)     => string.Format("apoc.map.submap({0})", Args(3, 4, count));
        public virtual string Fn_ApocMap_Unflatten             => "apoc.map.unflatten({0}, {1})";
        public virtual string Fn_ApocMap_UpdateTree            => "apoc.map.updateTree({0}, {1}, {2})";
        public virtual string Fn_ApocMap_Values                => "apoc.map.values({0}, {1}, {2})";

        #endregion

        #region apoc.math

        public virtual string Call_ApocMath_Regr       => "apoc.math.regr({0}, {1}, {2})";
        public virtual string Fn_ApocMath_Cosh         => "apoc.math.cosh({0})";
        public virtual string Fn_ApocMath_Coth         => "apoc.math.coth({0})";
        public virtual string Fn_ApocMath_Csch         => "apoc.math.csch({0})";
        public virtual string Fn_ApocMath_MaxByte      => "apoc.math.maxByte()";
        public virtual string Fn_ApocMath_MaxDouble    => "apoc.math.maxDouble()";
        public virtual string Fn_ApocMath_MaxInt       => "apoc.math.maxInt()";
        public virtual string Fn_ApocMath_MaxLong      => "apoc.math.maxLong()";
        public virtual string Fn_ApocMath_MinByte      => "apoc.math.minByte()";
        public virtual string Fn_ApocMath_MinDouble    => "apoc.math.minDouble()";
        public virtual string Fn_ApocMath_MinInt       => "apoc.math.minInt()";
        public virtual string Fn_ApocMath_MinLong      => "apoc.math.minLong()";
        public virtual string Fn_ApocMath_Round        => "apoc.math.round({0}, {1}, {2})";
        public virtual string Fn_ApocMath_Sech         => "apoc.math.sech({0})";
        public virtual string Fn_ApocMath_Sigmoid      => "apoc.math.sigmoid({0})";
        public virtual string Fn_ApocMath_SigmoidPrime => "apoc.math.sigmoidPrime({0})";
        public virtual string Fn_ApocMath_Sinh         => "apoc.math.sinh({0})";
        public virtual string Fn_ApocMath_Tanh         => "apoc.math.tanh({0})";

        #endregion

        #region apoc.merge

        public virtual string Call_ApocMerge_Node(int count) => string.Format("apoc.merge.node({0})", Args(2, 4, count));
        public virtual string Call_ApocMerge_Relationship    => "apoc.merge.relationship({0}, {1}, {2}, {3}, {4}, {5})";

        #endregion

        #region apoc.merge.node

        public virtual string Call_ApocMergeNode_Eager(int count) => string.Format("apoc.merge.node.eager({0})", Args(2, 4, count));

        #endregion

        #region apoc.merge.relationship

        public virtual string Call_ApocMergeRelationship_Eager => "apoc.merge.relationship.eager({0}, {1}, {2}, {3}, {4}, {5})";

        #endregion

        #region apoc.meta

        public virtual string Call_ApocMeta_Data(int count)               => string.Format("apoc.meta.data({0})", Args(0, 1, count));
        public virtual string Call_ApocMeta_Graph(int count)              => string.Format("apoc.meta.graph({0})", Args(0, 1, count));
        public virtual string Call_ApocMeta_GraphSample(int count)        => string.Format("apoc.meta.graphSample({0})", Args(0, 1, count));
        public virtual string Call_ApocMeta_NodeTypeProperties(int count) => string.Format("apoc.meta.nodeTypeProperties({0})", Args(0, 1, count));
        public virtual string Call_ApocMeta_RelTypeProperties(int count)  => string.Format("apoc.meta.relTypeProperties({0})", Args(0, 1, count));
        public virtual string Call_ApocMeta_Schema(int count)             => string.Format("apoc.meta.schema({0})", Args(0, 1, count));
        public virtual string Call_ApocMeta_Stats                         => "apoc.meta.stats()";
        public virtual string Call_ApocMeta_SubGraph                      => "apoc.meta.subGraph({0})";
        public virtual string Fn_ApocMeta_IsType                          => "apoc.meta.isType({0}, {1})";
        public virtual string Fn_ApocMeta_Type                            => "apoc.meta.type({0})";
        public virtual string Fn_ApocMeta_TypeName                        => "apoc.meta.typeName({0})";
        public virtual string Fn_ApocMeta_Types                           => "apoc.meta.types({0})";

        #endregion

        #region apoc.meta.cypher

        public virtual string Fn_ApocMetaCypher_IsType => "apoc.meta.cypher.isType({0}, {1})";
        public virtual string Fn_ApocMetaCypher_Type   => "apoc.meta.cypher.type({0})";
        public virtual string Fn_ApocMetaCypher_Types  => "apoc.meta.cypher.types({0})";

        #endregion

        #region apoc.meta.data

        public virtual string Call_ApocMetaData_Of => "apoc.meta.data.of({0}, {1})";

        #endregion

        #region apoc.meta.graph

        public virtual string Call_ApocMetaGraph_Of(int count) => string.Format("apoc.meta.graph.of({0})", Args(0, 2, count));

        #endregion

        #region apoc.meta.nodes

        public virtual string Fn_ApocMetaNodes_Count(int count) => string.Format("apoc.meta.nodes.count({0})", Args(0, 2, count));

        #endregion

        #region apoc.neighbors

        public virtual string Call_ApocNeighbors_Athop => "apoc.neighbors.athop({0}, {1}, {2})";
        public virtual string Call_ApocNeighbors_Byhop => "apoc.neighbors.byhop({0}, {1}, {2})";
        public virtual string Call_ApocNeighbors_Tohop => "apoc.neighbors.tohop({0}, {1}, {2})";

        #endregion

        #region apoc.neighbors.athop

        public virtual string Call_ApocNeighborsAthop_Count => "apoc.neighbors.athop.count({0}, {1}, {2})";

        #endregion

        #region apoc.neighbors.byhop

        public virtual string Call_ApocNeighborsByhop_Count => "apoc.neighbors.byhop.count({0}, {1}, {2})";

        #endregion

        #region apoc.neighbors.tohop

        public virtual string Call_ApocNeighborsTohop_Count => "apoc.neighbors.tohop.count({0}, {1}, {2})";

        #endregion

        #region apoc.nlp.azure.entities

        public virtual string Call_ApocNlpAzureEntities_Graph  => "apoc.nlp.azure.entities.graph({0}, {1})";
        public virtual string Call_ApocNlpAzureEntities_Stream => "apoc.nlp.azure.entities.stream({0}, {1})";

        #endregion

        #region apoc.nlp.azure.keyPhrases

        public virtual string Call_ApocNlpAzureKeyPhrases_Graph  => "apoc.nlp.azure.keyPhrases.graph({0}, {1})";
        public virtual string Call_ApocNlpAzureKeyPhrases_Stream => "apoc.nlp.azure.keyPhrases.stream({0}, {1})";

        #endregion

        #region apoc.nlp.azure.sentiment

        public virtual string Call_ApocNlpAzureSentiment_Graph  => "apoc.nlp.azure.sentiment.graph({0}, {1})";
        public virtual string Call_ApocNlpAzureSentiment_Stream => "apoc.nlp.azure.sentiment.stream({0}, {1})";

        #endregion

        #region apoc.node

        public virtual string Fn_ApocNode_Degree => "apoc.node.degree({0}, {1})";
        public virtual string Fn_ApocNode_Id     => "apoc.node.id({0})";
        public virtual string Fn_ApocNode_Labels => "apoc.node.labels({0})";

        #endregion

        #region apoc.node.degree

        public virtual string Fn_ApocNodeDegree_In  => "apoc.node.degree.in({0}, {1})";
        public virtual string Fn_ApocNodeDegree_Out => "apoc.node.degree.out({0}, {1})";

        #endregion

        #region apoc.node.relationship

        public virtual string Fn_ApocNodeRelationship_Exists => "apoc.node.relationship.exists({0}, {1})";
        public virtual string Fn_ApocNodeRelationship_Types  => "apoc.node.relationship.types({0}, {1})";

        #endregion

        #region apoc.node.relationships

        public virtual string Fn_ApocNodeRelationships_Exist => "apoc.node.relationships.exist({0}, {1})";

        #endregion

        #region apoc.nodes

        public virtual string Call_ApocNodes_Collapse => "apoc.nodes.collapse({0}, {1})";
        public virtual string Call_ApocNodes_Cycles   => "apoc.nodes.cycles({0}, {1})";
        public virtual string Call_ApocNodes_Delete   => "apoc.nodes.delete({0}, {1})";
        public virtual string Call_ApocNodes_Get      => "apoc.nodes.get({0})";
        public virtual string Call_ApocNodes_Group    => "apoc.nodes.group({0}, {1}, {2}, {3})";
        public virtual string Call_ApocNodes_Link     => "apoc.nodes.link({0}, {1}, {2})";
        public virtual string Call_ApocNodes_Rels     => "apoc.nodes.rels({0})";
        public virtual string Fn_ApocNodes_Connected  => "apoc.nodes.connected({0}, {1})";
        public virtual string Fn_ApocNodes_IsDense    => "apoc.nodes.isDense({0})";

        #endregion

        #region apoc.nodes.relationship

        public virtual string Fn_ApocNodesRelationship_Types => "apoc.nodes.relationship.types({0}, {1})";

        #endregion

        #region apoc.nodes.relationships

        public virtual string Fn_ApocNodesRelationships_Exist => "apoc.nodes.relationships.exist({0}, {1})";

        #endregion

        #region apoc.number

        public virtual string Fn_ApocNumber_ArabicToRoman => "apoc.number.arabicToRoman({0})";
        public virtual string Fn_ApocNumber_Format        => "apoc.number.format({0}, {1}, {2})";
        public virtual string Fn_ApocNumber_ParseFloat    => "apoc.number.parseFloat({0}, {1}, {2})";
        public virtual string Fn_ApocNumber_ParseInt      => "apoc.number.parseInt({0}, {1}, {2})";
        public virtual string Fn_ApocNumber_RomanToArabic => "apoc.number.romanToArabic({0})";

        #endregion

        #region apoc.number.exact

        public virtual string Fn_ApocNumberExact_Add       => "apoc.number.exact.add({0}, {1})";
        public virtual string Fn_ApocNumberExact_Div       => "apoc.number.exact.div({0}, {1}, {2}, {3})";
        public virtual string Fn_ApocNumberExact_Mul       => "apoc.number.exact.mul({0}, {1}, {2}, {3})";
        public virtual string Fn_ApocNumberExact_Sub       => "apoc.number.exact.sub({0}, {1})";
        public virtual string Fn_ApocNumberExact_ToExact   => "apoc.number.exact.toExact({0})";
        public virtual string Fn_ApocNumberExact_ToFloat   => "apoc.number.exact.toFloat({0}, {1}, {2})";
        public virtual string Fn_ApocNumberExact_ToInteger => "apoc.number.exact.toInteger({0}, {1}, {2})";

        #endregion

        #region apoc.path

        public virtual string Call_ApocPath_Expand        => "apoc.path.expand({0}, {1}, {2}, {3}, {4})";
        public virtual string Call_ApocPath_ExpandConfig  => "apoc.path.expandConfig({0}, {1})";
        public virtual string Call_ApocPath_SpanningTree  => "apoc.path.spanningTree({0}, {1})";
        public virtual string Call_ApocPath_SubgraphAll   => "apoc.path.subgraphAll({0}, {1})";
        public virtual string Call_ApocPath_SubgraphNodes => "apoc.path.subgraphNodes({0}, {1})";
        public virtual string Fn_ApocPath_Combine         => "apoc.path.combine({0}, {1})";
        public virtual string Fn_ApocPath_Create          => "apoc.path.create({0}, {1})";
        public virtual string Fn_ApocPath_Elements        => "apoc.path.elements({0})";
        public virtual string Fn_ApocPath_Slice           => "apoc.path.slice({0}, {1}, {2})";

        #endregion

        #region apoc.periodic

        public virtual string Call_ApocPeriodic_Cancel              => "apoc.periodic.cancel({0})";
        public virtual string Call_ApocPeriodic_Commit              => "apoc.periodic.commit({0}, {1})";
        public virtual string Call_ApocPeriodic_Countdown           => "apoc.periodic.countdown({0}, {1}, {2})";
        public virtual string Call_ApocPeriodic_Iterate             => "apoc.periodic.iterate({0}, {1}, {2})";
        public virtual string Call_ApocPeriodic_List                => "apoc.periodic.list()";
        public virtual string Call_ApocPeriodic_Repeat              => "apoc.periodic.repeat({0}, {1}, {2}, {3})";
        public virtual string Call_ApocPeriodic_Submit              => "apoc.periodic.submit({0}, {1}, {2})";
        public virtual string Call_ApocPeriodic_Truncate(int count) => string.Format("apoc.periodic.truncate({0})", Args(0, 1, count));

        #endregion

        #region apoc.refactor

        public virtual string Call_ApocRefactor_Categorize                  => "apoc.refactor.categorize({0}, {1}, {2}, {3}, {4}, {5}, {6})";
        public virtual string Call_ApocRefactor_CloneNodes(int count)       => string.Format("apoc.refactor.cloneNodes({0})", Args(1, 3, count));
        public virtual string Call_ApocRefactor_CloneNodesWithRelationships => "apoc.refactor.cloneNodesWithRelationships({0})";
        public virtual string Call_ApocRefactor_CloneSubgraph(int count)    => string.Format("apoc.refactor.cloneSubgraph({0})", Args(2, 3, count));
        public virtual string Call_ApocRefactor_CloneSubgraphFromPaths      => "apoc.refactor.cloneSubgraphFromPaths({0}, {1})";
        public virtual string Call_ApocRefactor_CollapseNode                => "apoc.refactor.collapseNode({0}, {1})";
        public virtual string Call_ApocRefactor_DeleteAndReconnect          => "apoc.refactor.deleteAndReconnect({0}, {1}, {2})";
        public virtual string Call_ApocRefactor_ExtractNode                 => "apoc.refactor.extractNode({0}, {1}, {2}, {3})";
        public virtual string Call_ApocRefactor_From                        => "apoc.refactor.from({0}, {1})";
        public virtual string Call_ApocRefactor_Invert                      => "apoc.refactor.invert({0})";
        public virtual string Call_ApocRefactor_MergeNodes                  => "apoc.refactor.mergeNodes({0}, {1})";
        public virtual string Call_ApocRefactor_MergeRelationships          => "apoc.refactor.mergeRelationships({0}, {1})";
        public virtual string Call_ApocRefactor_NormalizeAsBoolean          => "apoc.refactor.normalizeAsBoolean({0}, {1}, {2}, {3})";
        public virtual string Call_ApocRefactor_SetType                     => "apoc.refactor.setType({0}, {1})";
        public virtual string Call_ApocRefactor_To                          => "apoc.refactor.to({0}, {1})";

        #endregion

        #region apoc.refactor.rename

        public virtual string Call_ApocRefactorRename_Label                   => "apoc.refactor.rename.label({0}, {1}, {2})";
        public virtual string Call_ApocRefactorRename_NodeProperty            => "apoc.refactor.rename.nodeProperty({0}, {1}, {2}, {3})";
        public virtual string Call_ApocRefactorRename_Type(int count)         => string.Format("apoc.refactor.rename.type({0})", Args(3, 4, count));
        public virtual string Call_ApocRefactorRename_TypeProperty(int count) => string.Format("apoc.refactor.rename.typeProperty({0})", Args(3, 4, count));

        #endregion

        #region apoc.rel

        public virtual string Fn_ApocRel_EndNode   => "apoc.rel.endNode({0})";
        public virtual string Fn_ApocRel_Id        => "apoc.rel.id({0})";
        public virtual string Fn_ApocRel_StartNode => "apoc.rel.startNode({0})";
        public virtual string Fn_ApocRel_Type      => "apoc.rel.type({0})";

        #endregion

        #region apoc.schema

        public virtual string Call_ApocSchema_Assert                   => "apoc.schema.assert({0}, {1}, {2})";
        public virtual string Call_ApocSchema_Nodes(int count)         => string.Format("apoc.schema.nodes({0})", Args(0, 1, count));
        public virtual string Call_ApocSchema_Relationships(int count) => string.Format("apoc.schema.relationships({0})", Args(0, 1, count));

        #endregion

        #region apoc.schema.node

        public virtual string Fn_ApocSchemaNode_ConstraintExists => "apoc.schema.node.constraintExists({0}, {1})";
        public virtual string Fn_ApocSchemaNode_IndexExists      => "apoc.schema.node.indexExists({0}, {1})";

        #endregion

        #region apoc.schema.properties

        public virtual string Call_ApocSchemaProperties_Distinct      => "apoc.schema.properties.distinct({0}, {1})";
        public virtual string Call_ApocSchemaProperties_DistinctCount => "apoc.schema.properties.distinctCount({0}, {1})";

        #endregion

        #region apoc.schema.relationship

        public virtual string Fn_ApocSchemaRelationship_ConstraintExists => "apoc.schema.relationship.constraintExists({0}, {1})";
        public virtual string Fn_ApocSchemaRelationship_IndexExists      => "apoc.schema.relationship.indexExists({0}, {1})";

        #endregion

        #region apoc.scoring

        public virtual string Fn_ApocScoring_Existence => "apoc.scoring.existence({0}, {1})";
        public virtual string Fn_ApocScoring_Pareto    => "apoc.scoring.pareto({0}, {1}, {2}, {3})";

        #endregion

        #region apoc.search

        public virtual string Call_ApocSearch_MultiSearchReduced => "apoc.search.multiSearchReduced({0}, {1}, {2})";
        public virtual string Call_ApocSearch_Node               => "apoc.search.node({0}, {1}, {2})";
        public virtual string Call_ApocSearch_NodeAll            => "apoc.search.nodeAll({0}, {1}, {2})";
        public virtual string Call_ApocSearch_NodeAllReduced     => "apoc.search.nodeAllReduced({0}, {1}, {2})";
        public virtual string Call_ApocSearch_NodeReduced        => "apoc.search.nodeReduced({0}, {1}, {2})";

        #endregion

        #region apoc.spatial

        public virtual string Call_ApocSpatial_Geocode(int count)        => string.Format("apoc.spatial.geocode({0})", Args(2, 4, count));
        public virtual string Call_ApocSpatial_GeocodeOnce               => "apoc.spatial.geocodeOnce({0}, {1})";
        public virtual string Call_ApocSpatial_ReverseGeocode(int count) => string.Format("apoc.spatial.reverseGeocode({0})", Args(3, 4, count));
        public virtual string Call_ApocSpatial_SortByDistance            => "apoc.spatial.sortByDistance({0})";

        #endregion

        #region apoc.stats

        public virtual string Call_ApocStats_Degrees => "apoc.stats.degrees({0})";

        #endregion

        #region apoc.systemdb.export

        public virtual string Call_ApocSystemdbExport_Metadata(int count) => string.Format("apoc.systemdb.export.metadata({0})", Args(0, 1, count));

        #endregion

        #region apoc.temporal

        public virtual string Fn_ApocTemporal_Format                     => "apoc.temporal.format({0}, {1})";
        public virtual string Fn_ApocTemporal_FormatDuration             => "apoc.temporal.formatDuration({0}, {1})";
        public virtual string Fn_ApocTemporal_ToZonedTemporal(int count) => string.Format("apoc.temporal.toZonedTemporal({0})", Args(2, 3, count));

        #endregion

        #region apoc.text

        public virtual string Call_ApocText_DoubleMetaphone      => "apoc.text.doubleMetaphone({0})";
        public virtual string Call_ApocText_Phonetic             => "apoc.text.phonetic({0})";
        public virtual string Call_ApocText_PhoneticDelta        => "apoc.text.phoneticDelta({0}, {1})";
        public virtual string Fn_ApocText_Base64Decode           => "apoc.text.base64Decode({0})";
        public virtual string Fn_ApocText_Base64Encode           => "apoc.text.base64Encode({0})";
        public virtual string Fn_ApocText_Base64UrlDecode        => "apoc.text.base64UrlDecode({0})";
        public virtual string Fn_ApocText_Base64UrlEncode        => "apoc.text.base64UrlEncode({0})";
        public virtual string Fn_ApocText_ByteCount              => "apoc.text.byteCount({0}, {1})";
        public virtual string Fn_ApocText_Bytes                  => "apoc.text.bytes({0}, {1})";
        public virtual string Fn_ApocText_CamelCase              => "apoc.text.camelCase({0})";
        public virtual string Fn_ApocText_Capitalize             => "apoc.text.capitalize({0})";
        public virtual string Fn_ApocText_CapitalizeAll          => "apoc.text.capitalizeAll({0})";
        public virtual string Fn_ApocText_CharAt                 => "apoc.text.charAt({0}, {1})";
        public virtual string Fn_ApocText_Clean                  => "apoc.text.clean({0})";
        public virtual string Fn_ApocText_Code                   => "apoc.text.code({0})";
        public virtual string Fn_ApocText_CompareCleaned         => "apoc.text.compareCleaned({0}, {1})";
        public virtual string Fn_ApocText_Decapitalize           => "apoc.text.decapitalize({0})";
        public virtual string Fn_ApocText_DecapitalizeAll        => "apoc.text.decapitalizeAll({0})";
        public virtual string Fn_ApocText_Distance               => "apoc.text.distance({0}, {1})";
        public virtual string Fn_ApocText_Format                 => "apoc.text.format({0}, {1}, {2})";
        public virtual string Fn_ApocText_FuzzyMatch             => "apoc.text.fuzzyMatch({0}, {1})";
        public virtual string Fn_ApocText_HammingDistance        => "apoc.text.hammingDistance({0}, {1})";
        public virtual string Fn_ApocText_HexCharAt              => "apoc.text.hexCharAt({0}, {1})";
        public virtual string Fn_ApocText_HexValue               => "apoc.text.hexValue({0})";
        public virtual string Fn_ApocText_IndexesOf(int count)   => string.Format("apoc.text.indexesOf({0})", Args(3, 4, count));
        public virtual string Fn_ApocText_IndexOf(int count)     => string.Format("apoc.text.indexOf({0})", Args(3, 4, count));
        public virtual string Fn_ApocText_JaroWinklerDistance    => "apoc.text.jaroWinklerDistance({0}, {1})";
        public virtual string Fn_ApocText_Join                   => "apoc.text.join({0}, {1})";
        public virtual string Fn_ApocText_LevenshteinDistance    => "apoc.text.levenshteinDistance({0}, {1})";
        public virtual string Fn_ApocText_LevenshteinSimilarity  => "apoc.text.levenshteinSimilarity({0}, {1})";
        public virtual string Fn_ApocText_Lpad                   => "apoc.text.lpad({0}, {1}, {2})";
        public virtual string Fn_ApocText_Random(int count)      => string.Format("apoc.text.random({0})", Args(1, 2, count));
        public virtual string Fn_ApocText_RegexGroups            => "apoc.text.regexGroups({0}, {1})";
        public virtual string Fn_ApocText_Regreplace             => "apoc.text.regreplace({0}, {1}, {2})";
        public virtual string Fn_ApocText_Repeat                 => "apoc.text.repeat({0}, {1})";
        public virtual string Fn_ApocText_Replace                => "apoc.text.replace({0}, {1}, {2})";
        public virtual string Fn_ApocText_Rpad                   => "apoc.text.rpad({0}, {1}, {2})";
        public virtual string Fn_ApocText_Slug                   => "apoc.text.slug({0}, {1})";
        public virtual string Fn_ApocText_SnakeCase              => "apoc.text.snakeCase({0})";
        public virtual string Fn_ApocText_SorensenDiceSimilarity => "apoc.text.sorensenDiceSimilarity({0}, {1}, {2})";
        public virtual string Fn_ApocText_Split                  => "apoc.text.split({0}, {1}, {2})";
        public virtual string Fn_ApocText_SwapCase               => "apoc.text.swapCase({0})";
        public virtual string Fn_ApocText_ToCypher               => "apoc.text.toCypher({0}, {1})";
        public virtual string Fn_ApocText_ToUpperCase            => "apoc.text.toUpperCase({0})";
        public virtual string Fn_ApocText_UpperCamelCase         => "apoc.text.upperCamelCase({0})";
        public virtual string Fn_ApocText_Urldecode              => "apoc.text.urldecode({0})";
        public virtual string Fn_ApocText_Urlencode              => "apoc.text.urlencode({0})";

        #endregion

        #region apoc.trigger

        public virtual string Call_ApocTrigger_Add       => "apoc.trigger.add({0}, {1}, {2}, {3})";
        public virtual string Call_ApocTrigger_List      => "apoc.trigger.list()";
        public virtual string Call_ApocTrigger_Pause     => "apoc.trigger.pause({0})";
        public virtual string Call_ApocTrigger_Remove    => "apoc.trigger.remove({0})";
        public virtual string Call_ApocTrigger_RemoveAll => "apoc.trigger.removeAll()";
        public virtual string Call_ApocTrigger_Resume    => "apoc.trigger.resume({0})";

        #endregion

        #region apoc.util

        public virtual string Call_ApocUtil_Sleep           => "apoc.util.sleep({0})";
        public virtual string Call_ApocUtil_Validate        => "apoc.util.validate({0}, {1}, {2})";
        public virtual string Fn_ApocUtil_Compress          => "apoc.util.compress({0}, {1})";
        public virtual string Fn_ApocUtil_Decompress        => "apoc.util.decompress({0}, {1})";
        public virtual string Fn_ApocUtil_Md5(int count)    => string.Format("apoc.util.md5([{0}])", Args(count));
        public virtual string Fn_ApocUtil_Sha1(int count)   => string.Format("apoc.util.sha1([{0}])", Args(count));
        public virtual string Fn_ApocUtil_Sha256(int count) => string.Format("apoc.util.sha256([{0}])", Args(count));
        public virtual string Fn_ApocUtil_Sha384(int count) => string.Format("apoc.util.sha384([{0}])", Args(count));
        public virtual string Fn_ApocUtil_Sha512(int count) => string.Format("apoc.util.sha512([{0}])", Args(count));
        public virtual string Fn_ApocUtil_ValidatePredicate => "apoc.util.validatePredicate({0}, {1}, {2})";

        #endregion

        #region apoc.warmup

        public virtual string Call_ApocWarmup_Run(int count) => string.Format("apoc.warmup.run({0})", Args(0, 3, count));

        #endregion

        #region apoc.xml

        public virtual string Call_ApocXml_Import         => "apoc.xml.import({0}, {1})";
        public virtual string Fn_ApocXml_Parse(int count) => string.Format("apoc.xml.parse({0})", Args(2, 4, count));

        #endregion

        #region Helper Methods

        private string Args(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", $"The minimum count is 0.");

            return string.Join(", ", Enumerable.Range(0, count).Select(item => string.Concat("{", item, "}")));
        }
        private string Args(int minimum, int maximum, int count)
        {
            if (count < minimum)
                throw new ArgumentOutOfRangeException("count", $"The minimum count is {minimum}.");

            if (count > maximum)
                throw new ArgumentOutOfRangeException("count", $"The maximum count is {maximum}.");

            return string.Join(", ", Enumerable.Range(0, count).Select(item => string.Concat("{", item, "}")));
        }

        #endregion
    }
}
