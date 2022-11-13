using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Void;
using Blueprint41.Neo4j.Schema;
using Blueprint41.Query;
using q = Blueprint41.Query;

namespace Blueprint41.Neo4j.Model
{
    public abstract partial class QueryTranslator
    {

        #region apoc

        public virtual string CallApocCase(int count) => string.Format("apoc.case({0})", Args(2, 3, count));
        public virtual string CallApocHelp            => "apoc.help({0})";
        public virtual string CallApocWhen(int count) => string.Format("apoc.when({0})", Args(3, 4, count));
        public virtual string FnApocVersion           => "apoc.version()";

        #endregion

        #region apoc.agg

        public virtual string FnApocAggFirst       => "apoc.agg.first({0})";
        public virtual string FnApocAggGraph       => "apoc.agg.graph({0})";
        public virtual string FnApocAggLast        => "apoc.agg.last({0})";
        public virtual string FnApocAggMaxitems    => "apoc.agg.maxItems({0}, {1}, {2})";
        public virtual string FnApocAggMedian      => "apoc.agg.median({0})";
        public virtual string FnApocAggMinitems    => "apoc.agg.minItems({0}, {1}, {2})";
        public virtual string FnApocAggNth         => "apoc.agg.nth({0})";
        public virtual string FnApocAggPercentiles => "apoc.agg.percentiles({0}, {1})";
        public virtual string FnApocAggProduct     => "apoc.agg.product({0})";
        public virtual string FnApocAggSlice       => "apoc.agg.slice({0}, {1}, {2})";
        public virtual string FnApocAggStatistics  => "apoc.agg.statistics({0}, {1})";

        #endregion

        #region apoc.algo

        public virtual string CallApocAlgoAllsimplepaths            => "apoc.algo.allSimplePaths({0}, {1}, {2}, {3})";
        public virtual string CallApocAlgoAstar                     => "apoc.algo.aStar({0}, {1}, {2}, {3}, {4}, {5})";
        public virtual string CallApocAlgoAstarconfig               => "apoc.algo.aStarConfig({0}, {1}, {2}, {3})";
        public virtual string CallApocAlgoCover                     => "apoc.algo.cover({0})";
        public virtual string CallApocAlgoDijkstra                  => "apoc.algo.dijkstra({0}, {1}, {2}, {3}, {4}, {5})";
        public virtual string CallApocAlgoDijkstrawithdefaultweight => "apoc.algo.dijkstraWithDefaultWeight({0}, {1}, {2}, {3}, {4})";

        #endregion

        #region apoc.any

        public virtual string FnApocAnyProperties => "apoc.any.properties({0}, {1})";
        public virtual string FnApocAnyProperty   => "apoc.any.property({0}, {1})";

        #endregion

        #region apoc.atomic

        public virtual string CallApocAtomicAdd(int count)      => string.Format("apoc.atomic.add({0})", Args(3, 4, count));
        public virtual string CallApocAtomicConcat(int count)   => string.Format("apoc.atomic.concat({0})", Args(3, 4, count));
        public virtual string CallApocAtomicInsert              => "apoc.atomic.insert({0}, {1}, {2}, {3}, {4})";
        public virtual string CallApocAtomicRemove(int count)   => string.Format("apoc.atomic.remove({0})", Args(3, 4, count));
        public virtual string CallApocAtomicSubtract(int count) => string.Format("apoc.atomic.subtract({0})", Args(3, 4, count));
        public virtual string CallApocAtomicUpdate(int count)   => string.Format("apoc.atomic.update({0})", Args(3, 4, count));

        #endregion

        #region apoc.bitwise

        public virtual string FnApocBitwiseOp => "apoc.bitwise.op({0}, {1}, {2})";

        #endregion

        #region apoc.bolt

        public virtual string CallApocBoltExecute => "apoc.bolt.execute({0}, {1}, {2}, {3})";

        #endregion

        #region apoc.coll

        public virtual string CallApocCollElements             => "apoc.coll.elements({0}, {1}, {2})";
        public virtual string CallApocCollPairwithoffset       => "apoc.coll.pairWithOffset({0}, {1})";
        public virtual string CallApocCollPartition            => "apoc.coll.partition({0}, {1})";
        public virtual string CallApocCollSplit                => "apoc.coll.split({0}, {1})";
        public virtual string CallApocCollZiptorows            => "apoc.coll.zipToRows({0}, {1})";
        public virtual string FnApocCollAvg                    => "apoc.coll.avg({0})";
        public virtual string FnApocCollCombinations           => "apoc.coll.combinations({0}, {1}, {2})";
        public virtual string FnApocCollContains               => "apoc.coll.contains({0}, {1})";
        public virtual string FnApocCollContainsall            => "apoc.coll.containsAll({0}, {1})";
        public virtual string FnApocCollContainsallsorted      => "apoc.coll.containsAllSorted({0}, {1})";
        public virtual string FnApocCollContainsduplicates     => "apoc.coll.containsDuplicates({0})";
        public virtual string FnApocCollContainssorted         => "apoc.coll.containsSorted({0}, {1})";
        public virtual string FnApocCollDifferent(int count)   => string.Format("apoc.coll.different([{0}])", Args(count));
        public virtual string FnApocCollDisjunction            => "apoc.coll.disjunction({0}, {1})";
        public virtual string FnApocCollDropduplicateneighbors => "apoc.coll.dropDuplicateNeighbors({0})";
        public virtual string FnApocCollDuplicates             => "apoc.coll.duplicates({0})";
        public virtual string FnApocCollDuplicateswithcount    => "apoc.coll.duplicatesWithCount({0})";
        public virtual string FnApocCollFill                   => "apoc.coll.fill({0}, {1})";
        public virtual string FnApocCollFlatten(int count)     => string.Format("apoc.coll.flatten({0})", Args(1, 2, count));
        public virtual string FnApocCollFrequencies            => "apoc.coll.frequencies({0})";
        public virtual string FnApocCollFrequenciesasmap       => "apoc.coll.frequenciesAsMap({0})";
        public virtual string FnApocCollIndexof                => "apoc.coll.indexOf({0}, {1})";
        public virtual string FnApocCollInsert                 => "apoc.coll.insert({0}, {1}, {2})";
        public virtual string FnApocCollInsertall              => "apoc.coll.insertAll({0}, {1}, {2})";
        public virtual string FnApocCollIntersection           => "apoc.coll.intersection({0}, {1})";
        public virtual string FnApocCollIsequalcollection      => "apoc.coll.isEqualCollection({0}, {1})";
        public virtual string FnApocCollMax(int count)         => string.Format("apoc.coll.max([{0}])", Args(count));
        public virtual string FnApocCollMin(int count)         => string.Format("apoc.coll.min([{0}])", Args(count));
        public virtual string FnApocCollOccurrences            => "apoc.coll.occurrences({0}, {1})";
        public virtual string FnApocCollPairs                  => "apoc.coll.pairs({0})";
        public virtual string FnApocCollPairsmin               => "apoc.coll.pairsMin({0})";
        public virtual string FnApocCollRandomitem             => "apoc.coll.randomItem({0})";
        public virtual string FnApocCollRandomitems            => "apoc.coll.randomItems({0}, {1}, {2})";
        public virtual string FnApocCollRemove(int count)      => string.Format("apoc.coll.remove({0})", Args(2, 3, count));
        public virtual string FnApocCollRemoveall              => "apoc.coll.removeAll({0}, {1})";
        public virtual string FnApocCollReverse                => "apoc.coll.reverse({0})";
        public virtual string FnApocCollRunningtotal           => "apoc.coll.runningTotal({0})";
        public virtual string FnApocCollSet                    => "apoc.coll.set({0}, {1}, {2})";
        public virtual string FnApocCollShuffle                => "apoc.coll.shuffle({0})";
        public virtual string FnApocCollSort                   => "apoc.coll.sort({0})";
        public virtual string FnApocCollSortmaps               => "apoc.coll.sortMaps({0}, {1})";
        public virtual string FnApocCollSortmulti(int count)   => string.Format("apoc.coll.sortMulti({0})", Args(1, 4, count));
        public virtual string FnApocCollSortnodes              => "apoc.coll.sortNodes({0}, {1})";
        public virtual string FnApocCollSorttext(int count)    => string.Format("apoc.coll.sortText({0})", Args(1, 2, count));
        public virtual string FnApocCollStdev                  => "apoc.coll.stdev({0}, {1})";
        public virtual string FnApocCollSubtract               => "apoc.coll.subtract({0}, {1})";
        public virtual string FnApocCollSum                    => "apoc.coll.sum({0})";
        public virtual string FnApocCollSumlongs               => "apoc.coll.sumLongs({0})";
        public virtual string FnApocCollToset(int count)       => string.Format("apoc.coll.toSet([{0}])", Args(count));
        public virtual string FnApocCollUnion                  => "apoc.coll.union({0}, {1})";
        public virtual string FnApocCollUnionall               => "apoc.coll.unionAll({0}, {1})";
        public virtual string FnApocCollZip                    => "apoc.coll.zip({0}, {1})";

        #endregion

        #region apoc.convert

        public virtual string CallApocConvertSetjsonproperty             => "apoc.convert.setJsonProperty({0}, {1}, {2})";
        public virtual string CallApocConvertTotree                      => "apoc.convert.toTree({0}, {1}, {2})";
        public virtual string FnApocConvertFromjsonlist(int count)       => string.Format("apoc.convert.fromJsonList({0})", Args(2, 3, count));
        public virtual string FnApocConvertFromjsonmap(int count)        => string.Format("apoc.convert.fromJsonMap({0})", Args(2, 3, count));
        public virtual string FnApocConvertGetjsonproperty(int count)    => string.Format("apoc.convert.getJsonProperty({0})", Args(3, 4, count));
        public virtual string FnApocConvertGetjsonpropertymap(int count) => string.Format("apoc.convert.getJsonPropertyMap({0})", Args(3, 4, count));
        public virtual string FnApocConvertToboolean                     => "apoc.convert.toBoolean({0})";
        public virtual string FnApocConvertTobooleanlist                 => "apoc.convert.toBooleanList({0})";
        public virtual string FnApocConvertTofloat                       => "apoc.convert.toFloat({0})";
        public virtual string FnApocConvertTointeger                     => "apoc.convert.toInteger({0})";
        public virtual string FnApocConvertTointlist                     => "apoc.convert.toIntList({0})";
        public virtual string FnApocConvertTojson                        => "apoc.convert.toJson({0})";
        public virtual string FnApocConvertTolist                        => "apoc.convert.toList({0})";
        public virtual string FnApocConvertTomap                         => "apoc.convert.toMap({0})";
        public virtual string FnApocConvertTonode                        => "apoc.convert.toNode({0})";
        public virtual string FnApocConvertTonodelist                    => "apoc.convert.toNodeList({0})";
        public virtual string FnApocConvertTorelationship                => "apoc.convert.toRelationship({0})";
        public virtual string FnApocConvertTorelationshiplist            => "apoc.convert.toRelationshipList({0})";
        public virtual string FnApocConvertToset                         => "apoc.convert.toSet({0})";
        public virtual string FnApocConvertTosortedjsonmap               => "apoc.convert.toSortedJsonMap({0}, {1})";
        public virtual string FnApocConvertTostring                      => "apoc.convert.toString({0})";
        public virtual string FnApocConvertTostringlist                  => "apoc.convert.toStringList({0})";

        #endregion

        #region apoc.create

        public virtual string CallApocCreateAddlabels           => "apoc.create.addLabels({0}, {1})";
        public virtual string CallApocCreateClonepathstovirtual => "apoc.create.clonePathsToVirtual({0})";
        public virtual string CallApocCreateClonepathtovirtual  => "apoc.create.clonePathToVirtual({0})";
        public virtual string CallApocCreateNode                => "apoc.create.node({0}, {1})";
        public virtual string CallApocCreateNodes               => "apoc.create.nodes({0}, {1})";
        public virtual string CallApocCreateRelationship        => "apoc.create.relationship({0}, {1}, {2}, {3})";
        public virtual string CallApocCreateRemovelabels        => "apoc.create.removeLabels({0}, {1})";
        public virtual string CallApocCreateRemoveproperties    => "apoc.create.removeProperties({0}, {1})";
        public virtual string CallApocCreateRemoverelproperties => "apoc.create.removeRelProperties({0}, {1})";
        public virtual string CallApocCreateSetlabels           => "apoc.create.setLabels({0}, {1})";
        public virtual string CallApocCreateSetproperties       => "apoc.create.setProperties({0}, {1}, {2})";
        public virtual string CallApocCreateSetproperty         => "apoc.create.setProperty({0}, {1}, {2})";
        public virtual string CallApocCreateSetrelproperties    => "apoc.create.setRelProperties({0}, {1}, {2})";
        public virtual string CallApocCreateSetrelproperty      => "apoc.create.setRelProperty({0}, {1}, {2})";
        public virtual string CallApocCreateUuids               => "apoc.create.uuids({0})";
        public virtual string CallApocCreateVirtualpath         => "apoc.create.virtualPath({0}, {1}, {2}, {3}, {4}, {5})";
        public virtual string CallApocCreateVnode(int count)    => string.Format("apoc.create.vNode({0})", Args(1, 2, count));
        public virtual string CallApocCreateVnodes              => "apoc.create.vNodes({0}, {1})";
        public virtual string CallApocCreateVpattern            => "apoc.create.vPattern({0}, {1}, {2}, {3})";
        public virtual string CallApocCreateVpatternfull        => "apoc.create.vPatternFull({0}, {1}, {2}, {3}, {4}, {5})";
        public virtual string CallApocCreateVrelationship       => "apoc.create.vRelationship({0}, {1}, {2}, {3})";
        public virtual string FnApocCreateUuid                  => "apoc.create.uuid()";

        #endregion

        #region apoc.create.virtual

        public virtual string FnApocCreateVirtualFromnode => "apoc.create.virtual.fromNode({0}, {1})";

        #endregion

        #region apoc.cypher

        public virtual string CallApocCypherDoit               => "apoc.cypher.doIt({0}, {1})";
        public virtual string CallApocCypherRun                => "apoc.cypher.run({0}, {1})";
        public virtual string CallApocCypherRunmany            => "apoc.cypher.runMany({0}, {1}, {2})";
        public virtual string CallApocCypherRunmanyreadonly    => "apoc.cypher.runManyReadOnly({0}, {1}, {2})";
        public virtual string CallApocCypherRunschema          => "apoc.cypher.runSchema({0}, {1})";
        public virtual string CallApocCypherRuntimeboxed       => "apoc.cypher.runTimeboxed({0}, {1}, {2})";
        public virtual string CallApocCypherRunwrite           => "apoc.cypher.runWrite({0}, {1})";
        public virtual string FnApocCypherRunfirstcolumn       => "apoc.cypher.runFirstColumn({0}, {1}, {2})";
        public virtual string FnApocCypherRunfirstcolumnmany   => "apoc.cypher.runFirstColumnMany({0}, {1})";
        public virtual string FnApocCypherRunfirstcolumnsingle => "apoc.cypher.runFirstColumnSingle({0}, {1})";

        #endregion

        #region apoc.data

        public virtual string FnApocDataDomain => "apoc.data.domain({0})";
        public virtual string FnApocDataUrl    => "apoc.data.url({0})";

        #endregion

        #region apoc.date

        public virtual string FnApocDateAdd                             => "apoc.date.add({0}, {1}, {2}, {3})";
        public virtual string FnApocDateConvert                         => "apoc.date.convert({0}, {1}, {2})";
        public virtual string FnApocDateConvertformat                   => "apoc.date.convertFormat({0}, {1}, {2})";
        public virtual string FnApocDateCurrenttimestamp                => "apoc.date.currentTimestamp()";
        public virtual string FnApocDateField(int count)                => string.Format("apoc.date.field({0})", Args(1, 3, count));
        public virtual string FnApocDateFields(int count)               => string.Format("apoc.date.fields({0})", Args(1, 2, count));
        public virtual string FnApocDateFormat(int count)               => string.Format("apoc.date.format({0})", Args(3, 4, count));
        public virtual string FnApocDateFromiso8601                     => "apoc.date.fromISO8601({0})";
        public virtual string FnApocDateParse(int count)                => string.Format("apoc.date.parse({0})", Args(3, 4, count));
        public virtual string FnApocDateParseaszoneddatetime(int count) => string.Format("apoc.date.parseAsZonedDateTime({0})", Args(2, 3, count));
        public virtual string FnApocDateSystemtimezone                  => "apoc.date.systemTimezone()";
        public virtual string FnApocDateToiso8601(int count)            => string.Format("apoc.date.toISO8601({0})", Args(1, 2, count));
        public virtual string FnApocDateToyears                         => "apoc.date.toYears({0}, {1})";

        #endregion

        #region apoc.diff

        public virtual string FnApocDiffNodes => "apoc.diff.nodes({0}, {1})";

        #endregion

        #region apoc.do

        public virtual string CallApocDoCase(int count) => string.Format("apoc.do.case({0})", Args(2, 3, count));
        public virtual string CallApocDoWhen(int count) => string.Format("apoc.do.when({0})", Args(3, 4, count));

        #endregion

        #region apoc.dv

        public virtual string CallApocDvQuery(int count) => string.Format("apoc.dv.query({0})", Args(2, 3, count));
        public virtual string CallApocDvQueryandlink     => "apoc.dv.queryAndLink({0}, {1}, {2}, {3}, {4})";

        #endregion

        #region apoc.dv.catalog

        public virtual string CallApocDvCatalogAdd    => "apoc.dv.catalog.add({0}, {1})";
        public virtual string CallApocDvCatalogList   => "apoc.dv.catalog.list()";
        public virtual string CallApocDvCatalogRemove => "apoc.dv.catalog.remove({0})";

        #endregion

        #region apoc.example

        public virtual string CallApocExampleMovies => "apoc.example.movies()";

        #endregion

        #region apoc.export

        public virtual string CallApocExportCypherall   => "apoc.export.cypherAll({0}, {1})";
        public virtual string CallApocExportCypherdata  => "apoc.export.cypherData({0}, {1}, {2}, {3})";
        public virtual string CallApocExportCyphergraph => "apoc.export.cypherGraph({0}, {1}, {2})";
        public virtual string CallApocExportCypherquery => "apoc.export.cypherQuery({0}, {1}, {2})";

        #endregion

        #region apoc.export.csv

        public virtual string CallApocExportCsvAll   => "apoc.export.csv.all({0}, {1})";
        public virtual string CallApocExportCsvData  => "apoc.export.csv.data({0}, {1}, {2}, {3})";
        public virtual string CallApocExportCsvGraph => "apoc.export.csv.graph({0}, {1}, {2})";
        public virtual string CallApocExportCsvQuery => "apoc.export.csv.query({0}, {1}, {2})";

        #endregion

        #region apoc.export.cypher

        public virtual string CallApocExportCypherAll    => "apoc.export.cypher.all({0}, {1})";
        public virtual string CallApocExportCypherData   => "apoc.export.cypher.data({0}, {1}, {2}, {3})";
        public virtual string CallApocExportCypherGraph  => "apoc.export.cypher.graph({0}, {1}, {2})";
        public virtual string CallApocExportCypherQuery  => "apoc.export.cypher.query({0}, {1}, {2})";
        public virtual string CallApocExportCypherSchema => "apoc.export.cypher.schema({0}, {1})";

        #endregion

        #region apoc.export.graphml

        public virtual string CallApocExportGraphmlAll   => "apoc.export.graphml.all({0}, {1})";
        public virtual string CallApocExportGraphmlData  => "apoc.export.graphml.data({0}, {1}, {2}, {3})";
        public virtual string CallApocExportGraphmlGraph => "apoc.export.graphml.graph({0}, {1}, {2})";
        public virtual string CallApocExportGraphmlQuery => "apoc.export.graphml.query({0}, {1}, {2})";

        #endregion

        #region apoc.export.json

        public virtual string CallApocExportJsonAll   => "apoc.export.json.all({0}, {1})";
        public virtual string CallApocExportJsonData  => "apoc.export.json.data({0}, {1}, {2}, {3})";
        public virtual string CallApocExportJsonGraph => "apoc.export.json.graph({0}, {1}, {2})";
        public virtual string CallApocExportJsonQuery => "apoc.export.json.query({0}, {1}, {2})";

        #endregion

        #region apoc.graph

        public virtual string CallApocGraphFrom             => "apoc.graph.from({0}, {1}, {2})";
        public virtual string CallApocGraphFromcypher       => "apoc.graph.fromCypher({0}, {1}, {2}, {3})";
        public virtual string CallApocGraphFromdata         => "apoc.graph.fromData({0}, {1}, {2}, {3})";
        public virtual string CallApocGraphFromdb           => "apoc.graph.fromDB({0}, {1})";
        public virtual string CallApocGraphFromdocument     => "apoc.graph.fromDocument({0}, {1})";
        public virtual string CallApocGraphFrompath         => "apoc.graph.fromPath({0}, {1}, {2})";
        public virtual string CallApocGraphFrompaths        => "apoc.graph.fromPaths({0}, {1}, {2})";
        public virtual string CallApocGraphValidatedocument => "apoc.graph.validateDocument({0}, {1})";

        #endregion

        #region apoc.hashing

        public virtual string FnApocHashingFingerprint                 => "apoc.hashing.fingerprint({0}, {1})";
        public virtual string FnApocHashingFingerprintgraph(int count) => string.Format("apoc.hashing.fingerprintGraph({0})", Args(0, 1, count));
        public virtual string FnApocHashingFingerprinting              => "apoc.hashing.fingerprinting({0}, {1})";

        #endregion

        #region apoc.import

        public virtual string CallApocImportCsv     => "apoc.import.csv({0}, {1}, {2})";
        public virtual string CallApocImportGraphml => "apoc.import.graphml({0}, {1})";
        public virtual string CallApocImportJson    => "apoc.import.json({0}, {1})";
        public virtual string CallApocImportXml     => "apoc.import.xml({0}, {1})";

        #endregion

        #region apoc.json

        public virtual string FnApocJsonPath(int count) => string.Format("apoc.json.path({0})", Args(1, 3, count));

        #endregion

        #region apoc.label

        public virtual string FnApocLabelExists => "apoc.label.exists({0}, {1})";

        #endregion

        #region apoc.load

        public virtual string CallApocLoadJson       => "apoc.load.json({0}, {1}, {2})";
        public virtual string CallApocLoadJsonarray  => "apoc.load.jsonArray({0}, {1}, {2})";
        public virtual string CallApocLoadJsonparams => "apoc.load.jsonParams({0}, {1}, {2}, {3}, {4})";
        public virtual string CallApocLoadXml        => "apoc.load.xml({0}, {1}, {2}, {3})";

        #endregion

        #region apoc.lock

        public virtual string CallApocLockAll   => "apoc.lock.all({0}, {1})";
        public virtual string CallApocLockNodes => "apoc.lock.nodes({0})";
        public virtual string CallApocLockRels  => "apoc.lock.rels({0})";

        #endregion

        #region apoc.lock.read

        public virtual string CallApocLockReadNodes => "apoc.lock.read.nodes({0})";
        public virtual string CallApocLockReadRels  => "apoc.lock.read.rels({0})";

        #endregion

        #region apoc.log

        public virtual string CallApocLogStream => "apoc.log.stream({0}, {1})";

        #endregion

        #region apoc.map

        public virtual string FnApocMapClean                 => "apoc.map.clean({0}, {1}, {2})";
        public virtual string FnApocMapFlatten               => "apoc.map.flatten({0}, {1})";
        public virtual string FnApocMapFromlists             => "apoc.map.fromLists({0}, {1})";
        public virtual string FnApocMapFromnodes             => "apoc.map.fromNodes({0}, {1})";
        public virtual string FnApocMapFrompairs             => "apoc.map.fromPairs({0})";
        public virtual string FnApocMapFromvalues(int count) => string.Format("apoc.map.fromValues([{0}])", Args(count));
        public virtual string FnApocMapGet                   => "apoc.map.get({0}, {1}, {2}, {3})";
        public virtual string FnApocMapGroupby               => "apoc.map.groupBy({0}, {1})";
        public virtual string FnApocMapGroupbymulti          => "apoc.map.groupByMulti({0}, {1})";
        public virtual string FnApocMapMerge                 => "apoc.map.merge({0}, {1})";
        public virtual string FnApocMapMergelist             => "apoc.map.mergeList({0})";
        public virtual string FnApocMapMget(int count)       => string.Format("apoc.map.mget({0})", Args(3, 4, count));
        public virtual string FnApocMapRemovekey             => "apoc.map.removeKey({0}, {1}, {2})";
        public virtual string FnApocMapRemovekeys            => "apoc.map.removeKeys({0}, {1}, {2})";
        public virtual string FnApocMapSetentry              => "apoc.map.setEntry({0}, {1}, {2})";
        public virtual string FnApocMapSetkey                => "apoc.map.setKey({0}, {1}, {2})";
        public virtual string FnApocMapSetlists              => "apoc.map.setLists({0}, {1}, {2})";
        public virtual string FnApocMapSetpairs              => "apoc.map.setPairs({0}, {1})";
        public virtual string FnApocMapSetvalues             => "apoc.map.setValues({0}, {1})";
        public virtual string FnApocMapSortedproperties      => "apoc.map.sortedProperties({0}, {1})";
        public virtual string FnApocMapSubmap(int count)     => string.Format("apoc.map.submap({0})", Args(3, 4, count));
        public virtual string FnApocMapUnflatten             => "apoc.map.unflatten({0}, {1})";
        public virtual string FnApocMapUpdatetree            => "apoc.map.updateTree({0}, {1}, {2})";
        public virtual string FnApocMapValues                => "apoc.map.values({0}, {1}, {2})";

        #endregion

        #region apoc.math

        public virtual string CallApocMathRegr       => "apoc.math.regr({0}, {1}, {2})";
        public virtual string FnApocMathCosh         => "apoc.math.cosh({0})";
        public virtual string FnApocMathCoth         => "apoc.math.coth({0})";
        public virtual string FnApocMathCsch         => "apoc.math.csch({0})";
        public virtual string FnApocMathMaxbyte      => "apoc.math.maxByte()";
        public virtual string FnApocMathMaxdouble    => "apoc.math.maxDouble()";
        public virtual string FnApocMathMaxint       => "apoc.math.maxInt()";
        public virtual string FnApocMathMaxlong      => "apoc.math.maxLong()";
        public virtual string FnApocMathMinbyte      => "apoc.math.minByte()";
        public virtual string FnApocMathMindouble    => "apoc.math.minDouble()";
        public virtual string FnApocMathMinint       => "apoc.math.minInt()";
        public virtual string FnApocMathMinlong      => "apoc.math.minLong()";
        public virtual string FnApocMathRound        => "apoc.math.round({0}, {1}, {2})";
        public virtual string FnApocMathSech         => "apoc.math.sech({0})";
        public virtual string FnApocMathSigmoid      => "apoc.math.sigmoid({0})";
        public virtual string FnApocMathSigmoidprime => "apoc.math.sigmoidPrime({0})";
        public virtual string FnApocMathSinh         => "apoc.math.sinh({0})";
        public virtual string FnApocMathTanh         => "apoc.math.tanh({0})";

        #endregion

        #region apoc.merge

        public virtual string CallApocMergeNode(int count) => string.Format("apoc.merge.node({0})", Args(2, 4, count));
        public virtual string CallApocMergeRelationship    => "apoc.merge.relationship({0}, {1}, {2}, {3}, {4}, {5})";

        #endregion

        #region apoc.merge.node

        public virtual string CallApocMergeNodeEager(int count) => string.Format("apoc.merge.node.eager({0})", Args(2, 4, count));

        #endregion

        #region apoc.merge.relationship

        public virtual string CallApocMergeRelationshipEager => "apoc.merge.relationship.eager({0}, {1}, {2}, {3}, {4}, {5})";

        #endregion

        #region apoc.meta

        public virtual string CallApocMetaData(int count)               => string.Format("apoc.meta.data({0})", Args(0, 1, count));
        public virtual string CallApocMetaGraph(int count)              => string.Format("apoc.meta.graph({0})", Args(0, 1, count));
        public virtual string CallApocMetaGraphsample(int count)        => string.Format("apoc.meta.graphSample({0})", Args(0, 1, count));
        public virtual string CallApocMetaNodetypeproperties(int count) => string.Format("apoc.meta.nodeTypeProperties({0})", Args(0, 1, count));
        public virtual string CallApocMetaReltypeproperties(int count)  => string.Format("apoc.meta.relTypeProperties({0})", Args(0, 1, count));
        public virtual string CallApocMetaSchema(int count)             => string.Format("apoc.meta.schema({0})", Args(0, 1, count));
        public virtual string CallApocMetaStats                         => "apoc.meta.stats()";
        public virtual string CallApocMetaSubgraph                      => "apoc.meta.subGraph({0})";
        public virtual string FnApocMetaIstype                          => "apoc.meta.isType({0}, {1})";
        public virtual string FnApocMetaType                            => "apoc.meta.type({0})";
        public virtual string FnApocMetaTypename                        => "apoc.meta.typeName({0})";
        public virtual string FnApocMetaTypes                           => "apoc.meta.types({0})";

        #endregion

        #region apoc.meta.cypher

        public virtual string FnApocMetaCypherIstype => "apoc.meta.cypher.isType({0}, {1})";
        public virtual string FnApocMetaCypherType   => "apoc.meta.cypher.type({0})";
        public virtual string FnApocMetaCypherTypes  => "apoc.meta.cypher.types({0})";

        #endregion

        #region apoc.meta.data

        public virtual string CallApocMetaDataOf => "apoc.meta.data.of({0}, {1})";

        #endregion

        #region apoc.meta.graph

        public virtual string CallApocMetaGraphOf(int count) => string.Format("apoc.meta.graph.of({0})", Args(0, 2, count));

        #endregion

        #region apoc.meta.nodes

        public virtual string FnApocMetaNodesCount(int count) => string.Format("apoc.meta.nodes.count({0})", Args(0, 2, count));

        #endregion

        #region apoc.neighbors

        public virtual string CallApocNeighborsAthop => "apoc.neighbors.athop({0}, {1}, {2})";
        public virtual string CallApocNeighborsByhop => "apoc.neighbors.byhop({0}, {1}, {2})";
        public virtual string CallApocNeighborsTohop => "apoc.neighbors.tohop({0}, {1}, {2})";

        #endregion

        #region apoc.neighbors.athop

        public virtual string CallApocNeighborsAthopCount => "apoc.neighbors.athop.count({0}, {1}, {2})";

        #endregion

        #region apoc.neighbors.byhop

        public virtual string CallApocNeighborsByhopCount => "apoc.neighbors.byhop.count({0}, {1}, {2})";

        #endregion

        #region apoc.neighbors.tohop

        public virtual string CallApocNeighborsTohopCount => "apoc.neighbors.tohop.count({0}, {1}, {2})";

        #endregion

        #region apoc.nlp.azure.entities

        public virtual string CallApocNlpAzureEntitiesGraph  => "apoc.nlp.azure.entities.graph({0}, {1})";
        public virtual string CallApocNlpAzureEntitiesStream => "apoc.nlp.azure.entities.stream({0}, {1})";

        #endregion

        #region apoc.nlp.azure.keyPhrases

        public virtual string CallApocNlpAzureKeyphrasesGraph  => "apoc.nlp.azure.keyPhrases.graph({0}, {1})";
        public virtual string CallApocNlpAzureKeyphrasesStream => "apoc.nlp.azure.keyPhrases.stream({0}, {1})";

        #endregion

        #region apoc.nlp.azure.sentiment

        public virtual string CallApocNlpAzureSentimentGraph  => "apoc.nlp.azure.sentiment.graph({0}, {1})";
        public virtual string CallApocNlpAzureSentimentStream => "apoc.nlp.azure.sentiment.stream({0}, {1})";

        #endregion

        #region apoc.node

        public virtual string FnApocNodeDegree => "apoc.node.degree({0}, {1})";
        public virtual string FnApocNodeId     => "apoc.node.id({0})";
        public virtual string FnApocNodeLabels => "apoc.node.labels({0})";

        #endregion

        #region apoc.node.degree

        public virtual string FnApocNodeDegreeIn  => "apoc.node.degree.in({0}, {1})";
        public virtual string FnApocNodeDegreeOut => "apoc.node.degree.out({0}, {1})";

        #endregion

        #region apoc.node.relationship

        public virtual string FnApocNodeRelationshipExists => "apoc.node.relationship.exists({0}, {1})";
        public virtual string FnApocNodeRelationshipTypes  => "apoc.node.relationship.types({0}, {1})";

        #endregion

        #region apoc.node.relationships

        public virtual string FnApocNodeRelationshipsExist => "apoc.node.relationships.exist({0}, {1})";

        #endregion

        #region apoc.nodes

        public virtual string CallApocNodesCollapse => "apoc.nodes.collapse({0}, {1})";
        public virtual string CallApocNodesCycles   => "apoc.nodes.cycles({0}, {1})";
        public virtual string CallApocNodesDelete   => "apoc.nodes.delete({0}, {1})";
        public virtual string CallApocNodesGet      => "apoc.nodes.get({0})";
        public virtual string CallApocNodesGroup    => "apoc.nodes.group({0}, {1}, {2}, {3})";
        public virtual string CallApocNodesLink     => "apoc.nodes.link({0}, {1}, {2})";
        public virtual string CallApocNodesRels     => "apoc.nodes.rels({0})";
        public virtual string FnApocNodesConnected  => "apoc.nodes.connected({0}, {1})";
        public virtual string FnApocNodesIsdense    => "apoc.nodes.isDense({0})";

        #endregion

        #region apoc.nodes.relationship

        public virtual string FnApocNodesRelationshipTypes => "apoc.nodes.relationship.types({0}, {1})";

        #endregion

        #region apoc.nodes.relationships

        public virtual string FnApocNodesRelationshipsExist => "apoc.nodes.relationships.exist({0}, {1})";

        #endregion

        #region apoc.number

        public virtual string FnApocNumberArabictoroman => "apoc.number.arabicToRoman({0})";
        public virtual string FnApocNumberFormat        => "apoc.number.format({0}, {1}, {2})";
        public virtual string FnApocNumberParsefloat    => "apoc.number.parseFloat({0}, {1}, {2})";
        public virtual string FnApocNumberParseint      => "apoc.number.parseInt({0}, {1}, {2})";
        public virtual string FnApocNumberRomantoarabic => "apoc.number.romanToArabic({0})";

        #endregion

        #region apoc.number.exact

        public virtual string FnApocNumberExactAdd       => "apoc.number.exact.add({0}, {1})";
        public virtual string FnApocNumberExactDiv       => "apoc.number.exact.div({0}, {1}, {2}, {3})";
        public virtual string FnApocNumberExactMul       => "apoc.number.exact.mul({0}, {1}, {2}, {3})";
        public virtual string FnApocNumberExactSub       => "apoc.number.exact.sub({0}, {1})";
        public virtual string FnApocNumberExactToexact   => "apoc.number.exact.toExact({0})";
        public virtual string FnApocNumberExactTofloat   => "apoc.number.exact.toFloat({0}, {1}, {2})";
        public virtual string FnApocNumberExactTointeger => "apoc.number.exact.toInteger({0}, {1}, {2})";

        #endregion

        #region apoc.path

        public virtual string CallApocPathExpand        => "apoc.path.expand({0}, {1}, {2}, {3}, {4})";
        public virtual string CallApocPathExpandconfig  => "apoc.path.expandConfig({0}, {1})";
        public virtual string CallApocPathSpanningtree  => "apoc.path.spanningTree({0}, {1})";
        public virtual string CallApocPathSubgraphall   => "apoc.path.subgraphAll({0}, {1})";
        public virtual string CallApocPathSubgraphnodes => "apoc.path.subgraphNodes({0}, {1})";
        public virtual string FnApocPathCombine         => "apoc.path.combine({0}, {1})";
        public virtual string FnApocPathCreate          => "apoc.path.create({0}, {1})";
        public virtual string FnApocPathElements        => "apoc.path.elements({0})";
        public virtual string FnApocPathSlice           => "apoc.path.slice({0}, {1}, {2})";

        #endregion

        #region apoc.periodic

        public virtual string CallApocPeriodicCancel              => "apoc.periodic.cancel({0})";
        public virtual string CallApocPeriodicCommit              => "apoc.periodic.commit({0}, {1})";
        public virtual string CallApocPeriodicCountdown           => "apoc.periodic.countdown({0}, {1}, {2})";
        public virtual string CallApocPeriodicIterate             => "apoc.periodic.iterate({0}, {1}, {2})";
        public virtual string CallApocPeriodicList                => "apoc.periodic.list()";
        public virtual string CallApocPeriodicRepeat              => "apoc.periodic.repeat({0}, {1}, {2}, {3})";
        public virtual string CallApocPeriodicSubmit              => "apoc.periodic.submit({0}, {1}, {2})";
        public virtual string CallApocPeriodicTruncate(int count) => string.Format("apoc.periodic.truncate({0})", Args(0, 1, count));

        #endregion

        #region apoc.refactor

        public virtual string CallApocRefactorCategorize                  => "apoc.refactor.categorize({0}, {1}, {2}, {3}, {4}, {5}, {6})";
        public virtual string CallApocRefactorClonenodes(int count)       => string.Format("apoc.refactor.cloneNodes({0})", Args(1, 3, count));
        public virtual string CallApocRefactorClonenodeswithrelationships => "apoc.refactor.cloneNodesWithRelationships({0})";
        public virtual string CallApocRefactorClonesubgraph(int count)    => string.Format("apoc.refactor.cloneSubgraph({0})", Args(2, 3, count));
        public virtual string CallApocRefactorClonesubgraphfrompaths      => "apoc.refactor.cloneSubgraphFromPaths({0}, {1})";
        public virtual string CallApocRefactorCollapsenode                => "apoc.refactor.collapseNode({0}, {1})";
        public virtual string CallApocRefactorDeleteandreconnect          => "apoc.refactor.deleteAndReconnect({0}, {1}, {2})";
        public virtual string CallApocRefactorExtractnode                 => "apoc.refactor.extractNode({0}, {1}, {2}, {3})";
        public virtual string CallApocRefactorFrom                        => "apoc.refactor.from({0}, {1})";
        public virtual string CallApocRefactorInvert                      => "apoc.refactor.invert({0})";
        public virtual string CallApocRefactorMergenodes                  => "apoc.refactor.mergeNodes({0}, {1})";
        public virtual string CallApocRefactorMergerelationships          => "apoc.refactor.mergeRelationships({0}, {1})";
        public virtual string CallApocRefactorNormalizeasboolean          => "apoc.refactor.normalizeAsBoolean({0}, {1}, {2}, {3})";
        public virtual string CallApocRefactorSettype                     => "apoc.refactor.setType({0}, {1})";
        public virtual string CallApocRefactorTo                          => "apoc.refactor.to({0}, {1})";

        #endregion

        #region apoc.refactor.rename

        public virtual string CallApocRefactorRenameLabel                   => "apoc.refactor.rename.label({0}, {1}, {2})";
        public virtual string CallApocRefactorRenameNodeproperty            => "apoc.refactor.rename.nodeProperty({0}, {1}, {2}, {3})";
        public virtual string CallApocRefactorRenameType(int count)         => string.Format("apoc.refactor.rename.type({0})", Args(3, 4, count));
        public virtual string CallApocRefactorRenameTypeproperty(int count) => string.Format("apoc.refactor.rename.typeProperty({0})", Args(3, 4, count));

        #endregion

        #region apoc.rel

        public virtual string FnApocRelEndnode   => "apoc.rel.endNode({0})";
        public virtual string FnApocRelId        => "apoc.rel.id({0})";
        public virtual string FnApocRelStartnode => "apoc.rel.startNode({0})";
        public virtual string FnApocRelType      => "apoc.rel.type({0})";

        #endregion

        #region apoc.schema

        public virtual string CallApocSchemaAssert                   => "apoc.schema.assert({0}, {1}, {2})";
        public virtual string CallApocSchemaNodes(int count)         => string.Format("apoc.schema.nodes({0})", Args(0, 1, count));
        public virtual string CallApocSchemaRelationships(int count) => string.Format("apoc.schema.relationships({0})", Args(0, 1, count));

        #endregion

        #region apoc.schema.node

        public virtual string FnApocSchemaNodeConstraintexists => "apoc.schema.node.constraintExists({0}, {1})";
        public virtual string FnApocSchemaNodeIndexexists      => "apoc.schema.node.indexExists({0}, {1})";

        #endregion

        #region apoc.schema.properties

        public virtual string CallApocSchemaPropertiesDistinct      => "apoc.schema.properties.distinct({0}, {1})";
        public virtual string CallApocSchemaPropertiesDistinctcount => "apoc.schema.properties.distinctCount({0}, {1})";

        #endregion

        #region apoc.schema.relationship

        public virtual string FnApocSchemaRelationshipConstraintexists => "apoc.schema.relationship.constraintExists({0}, {1})";
        public virtual string FnApocSchemaRelationshipIndexexists      => "apoc.schema.relationship.indexExists({0}, {1})";

        #endregion

        #region apoc.scoring

        public virtual string FnApocScoringExistence => "apoc.scoring.existence({0}, {1})";
        public virtual string FnApocScoringPareto    => "apoc.scoring.pareto({0}, {1}, {2}, {3})";

        #endregion

        #region apoc.search

        public virtual string CallApocSearchMultisearchreduced => "apoc.search.multiSearchReduced({0}, {1}, {2})";
        public virtual string CallApocSearchNode               => "apoc.search.node({0}, {1}, {2})";
        public virtual string CallApocSearchNodeall            => "apoc.search.nodeAll({0}, {1}, {2})";
        public virtual string CallApocSearchNodeallreduced     => "apoc.search.nodeAllReduced({0}, {1}, {2})";
        public virtual string CallApocSearchNodereduced        => "apoc.search.nodeReduced({0}, {1}, {2})";

        #endregion

        #region apoc.spatial

        public virtual string CallApocSpatialGeocode(int count)        => string.Format("apoc.spatial.geocode({0})", Args(2, 4, count));
        public virtual string CallApocSpatialGeocodeonce               => "apoc.spatial.geocodeOnce({0}, {1})";
        public virtual string CallApocSpatialReversegeocode(int count) => string.Format("apoc.spatial.reverseGeocode({0})", Args(3, 4, count));
        public virtual string CallApocSpatialSortbydistance            => "apoc.spatial.sortByDistance({0})";

        #endregion

        #region apoc.stats

        public virtual string CallApocStatsDegrees => "apoc.stats.degrees({0})";

        #endregion

        #region apoc.systemdb.export

        public virtual string CallApocSystemdbExportMetadata(int count) => string.Format("apoc.systemdb.export.metadata({0})", Args(0, 1, count));

        #endregion

        #region apoc.temporal

        public virtual string FnApocTemporalFormat                     => "apoc.temporal.format({0}, {1})";
        public virtual string FnApocTemporalFormatduration             => "apoc.temporal.formatDuration({0}, {1})";
        public virtual string FnApocTemporalTozonedtemporal(int count) => string.Format("apoc.temporal.toZonedTemporal({0})", Args(2, 3, count));

        #endregion

        #region apoc.text

        public virtual string CallApocTextDoublemetaphone      => "apoc.text.doubleMetaphone({0})";
        public virtual string CallApocTextPhonetic             => "apoc.text.phonetic({0})";
        public virtual string CallApocTextPhoneticdelta        => "apoc.text.phoneticDelta({0}, {1})";
        public virtual string FnApocTextBase64decode           => "apoc.text.base64Decode({0})";
        public virtual string FnApocTextBase64encode           => "apoc.text.base64Encode({0})";
        public virtual string FnApocTextBase64urldecode        => "apoc.text.base64UrlDecode({0})";
        public virtual string FnApocTextBase64urlencode        => "apoc.text.base64UrlEncode({0})";
        public virtual string FnApocTextBytecount              => "apoc.text.byteCount({0}, {1})";
        public virtual string FnApocTextBytes                  => "apoc.text.bytes({0}, {1})";
        public virtual string FnApocTextCamelcase              => "apoc.text.camelCase({0})";
        public virtual string FnApocTextCapitalize             => "apoc.text.capitalize({0})";
        public virtual string FnApocTextCapitalizeall          => "apoc.text.capitalizeAll({0})";
        public virtual string FnApocTextCharat                 => "apoc.text.charAt({0}, {1})";
        public virtual string FnApocTextClean                  => "apoc.text.clean({0})";
        public virtual string FnApocTextCode                   => "apoc.text.code({0})";
        public virtual string FnApocTextComparecleaned         => "apoc.text.compareCleaned({0}, {1})";
        public virtual string FnApocTextDecapitalize           => "apoc.text.decapitalize({0})";
        public virtual string FnApocTextDecapitalizeall        => "apoc.text.decapitalizeAll({0})";
        public virtual string FnApocTextDistance               => "apoc.text.distance({0}, {1})";
        public virtual string FnApocTextFormat                 => "apoc.text.format({0}, {1}, {2})";
        public virtual string FnApocTextFuzzymatch             => "apoc.text.fuzzyMatch({0}, {1})";
        public virtual string FnApocTextHammingdistance        => "apoc.text.hammingDistance({0}, {1})";
        public virtual string FnApocTextHexcharat              => "apoc.text.hexCharAt({0}, {1})";
        public virtual string FnApocTextHexvalue               => "apoc.text.hexValue({0})";
        public virtual string FnApocTextIndexesof(int count)   => string.Format("apoc.text.indexesOf({0})", Args(3, 4, count));
        public virtual string FnApocTextIndexof(int count)     => string.Format("apoc.text.indexOf({0})", Args(3, 4, count));
        public virtual string FnApocTextJarowinklerdistance    => "apoc.text.jaroWinklerDistance({0}, {1})";
        public virtual string FnApocTextJoin                   => "apoc.text.join({0}, {1})";
        public virtual string FnApocTextLevenshteindistance    => "apoc.text.levenshteinDistance({0}, {1})";
        public virtual string FnApocTextLevenshteinsimilarity  => "apoc.text.levenshteinSimilarity({0}, {1})";
        public virtual string FnApocTextLpad                   => "apoc.text.lpad({0}, {1}, {2})";
        public virtual string FnApocTextRandom(int count)      => string.Format("apoc.text.random({0})", Args(1, 2, count));
        public virtual string FnApocTextRegexgroups            => "apoc.text.regexGroups({0}, {1})";
        public virtual string FnApocTextRegreplace             => "apoc.text.regreplace({0}, {1}, {2})";
        public virtual string FnApocTextRepeat                 => "apoc.text.repeat({0}, {1})";
        public virtual string FnApocTextReplace                => "apoc.text.replace({0}, {1}, {2})";
        public virtual string FnApocTextRpad                   => "apoc.text.rpad({0}, {1}, {2})";
        public virtual string FnApocTextSlug                   => "apoc.text.slug({0}, {1})";
        public virtual string FnApocTextSnakecase              => "apoc.text.snakeCase({0})";
        public virtual string FnApocTextSorensendicesimilarity => "apoc.text.sorensenDiceSimilarity({0}, {1}, {2})";
        public virtual string FnApocTextSplit                  => "apoc.text.split({0}, {1}, {2})";
        public virtual string FnApocTextSwapcase               => "apoc.text.swapCase({0})";
        public virtual string FnApocTextTocypher               => "apoc.text.toCypher({0}, {1})";
        public virtual string FnApocTextTouppercase            => "apoc.text.toUpperCase({0})";
        public virtual string FnApocTextUppercamelcase         => "apoc.text.upperCamelCase({0})";
        public virtual string FnApocTextUrldecode              => "apoc.text.urldecode({0})";
        public virtual string FnApocTextUrlencode              => "apoc.text.urlencode({0})";

        #endregion

        #region apoc.trigger

        public virtual string CallApocTriggerAdd       => "apoc.trigger.add({0}, {1}, {2}, {3})";
        public virtual string CallApocTriggerList      => "apoc.trigger.list()";
        public virtual string CallApocTriggerPause     => "apoc.trigger.pause({0})";
        public virtual string CallApocTriggerRemove    => "apoc.trigger.remove({0})";
        public virtual string CallApocTriggerRemoveall => "apoc.trigger.removeAll()";
        public virtual string CallApocTriggerResume    => "apoc.trigger.resume({0})";

        #endregion

        #region apoc.util

        public virtual string CallApocUtilSleep           => "apoc.util.sleep({0})";
        public virtual string CallApocUtilValidate        => "apoc.util.validate({0}, {1}, {2})";
        public virtual string FnApocUtilCompress          => "apoc.util.compress({0}, {1})";
        public virtual string FnApocUtilDecompress        => "apoc.util.decompress({0}, {1})";
        public virtual string FnApocUtilMd5(int count)    => string.Format("apoc.util.md5([{0}])", Args(count));
        public virtual string FnApocUtilSha1(int count)   => string.Format("apoc.util.sha1([{0}])", Args(count));
        public virtual string FnApocUtilSha256(int count) => string.Format("apoc.util.sha256([{0}])", Args(count));
        public virtual string FnApocUtilSha384(int count) => string.Format("apoc.util.sha384([{0}])", Args(count));
        public virtual string FnApocUtilSha512(int count) => string.Format("apoc.util.sha512([{0}])", Args(count));
        public virtual string FnApocUtilValidatepredicate => "apoc.util.validatePredicate({0}, {1}, {2})";

        #endregion

        #region apoc.warmup

        public virtual string CallApocWarmupRun(int count) => string.Format("apoc.warmup.run({0})", Args(0, 3, count));

        #endregion

        #region apoc.xml

        public virtual string CallApocXmlImport         => "apoc.xml.import({0}, {1})";
        public virtual string FnApocXmlParse(int count) => string.Format("apoc.xml.parse({0})", Args(2, 4, count));

        #endregion

        //public virtual string FnApocCreateUuid          => "apoc.create.uuid()";
        //public virtual string FnApocCollFlatten         => "apoc.coll.flatten({0})";
        //public virtual string FnApocCollSort            => "apoc.coll.sort({0})";
        //public virtual string FnApocCollSortNodes       => "apoc.coll.sortNodes({0}, \"{1}\")";
        //public virtual string FnApocCollPairs           => "apoc.coll.pairs({0})";
        //public virtual string FnApocCollPairsMin        => "apoc.coll.pairsMin({0})";
        //public virtual string FnApocCollUnion           => "apoc.coll.union({0}, {1})";
        //public virtual string FnApocCollUnionAll        => "apoc.coll.unionAll({0}, {1})";
        //public virtual string FnApocMapSortedProperties => "apoc.map.sortedProperties({0})";
        //public virtual string FnApocJsonPath(int count)
        //{
        //    if (count > 2)
        //        throw new NotSupportedException("The count cannot be greater than 2.");
        //
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("apoc.json.path({0}");
        //
        //    if (count >= 1)
        //        sb.Append(", {1}");
        //
        //    if (count == 2)
        //        sb.Append(", {2}");
        //
        //    sb.Append(")");
        //
        //    return sb.ToString();
        //}
        //
        //public virtual string FnApocUtilSHA1(int count) => $"apoc.util.sha1([{string.Join(", ", Enumerable.Range(0, count).Select(item => string.Concat("{", item, "}")))}])";
        //public virtual string FnApocUtilMD5(int count)
        //{
        //    return $"apoc.util.md5([{string.Join(", ", Enumerable.Range(0, count).Select(item => string.Concat("{", item, "}")))}])";
        //}

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
    }
}
