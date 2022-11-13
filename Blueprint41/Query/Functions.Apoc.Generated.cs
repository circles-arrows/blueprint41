#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Blueprint41.Query;

public static partial class Functions
{
    public static partial class Apoc
    {
        /// <summary>
        /// apoc.case([condition, query, condition, query, …​], elseQuery:&apos;&apos;, params:{}) yield value - given a list of conditional / read-only query pairs, executes the query associated with the first conditional evaluating to true (or the else query if none are true) with the given parameters
        /// </summary>
        public static MiscJaggedListResult Case(MiscListResult list)
        {
            return new MiscJaggedListResult(t => t.CallApocCase(0));
        }
        /// <summary>
        /// Provides descriptions of available procedures. To narrow the results, supply a search string. To also search in the description text, append + to the end of the search string.
        /// </summary>
        public static MiscJaggedListResult Help(MiscListResult list)
        {
            return new MiscJaggedListResult(t => t.CallApocHelp);
        }
        /// <summary>
        /// RETURN apoc.version() | return the current APOC installed version
        /// </summary>
        public static MiscJaggedListResult Version(MiscListResult list)
        {
            return new MiscJaggedListResult(t => t.FnApocVersion);
        }
        /// <summary>
        /// apoc.when(condition, ifQuery, elseQuery:&apos;&apos;, params:{}) yield value - based on the conditional, executes read-only ifQuery or elseQuery with the given parameters
        /// </summary>
        public static MiscJaggedListResult When(MiscListResult list)
        {
            return new MiscJaggedListResult(t => t.CallApocWhen(0));
        }

        public static partial class Agg
        {
            /// <summary>
            /// apoc.agg.first(value) - returns first value
            /// </summary>
            public static MiscJaggedListResult First(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggFirst);
            }
            /// <summary>
            /// apoc.agg.graph(path) - returns map of graph {nodes, relationships} of all distinct nodes and relationships
            /// </summary>
            public static MiscJaggedListResult Graph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggGraph);
            }
            /// <summary>
            /// apoc.agg.last(value) - returns last value
            /// </summary>
            public static MiscJaggedListResult Last(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggLast);
            }
            /// <summary>
            /// apoc.agg.maxItems(item, value, groupLimit: -1) - returns a map {items:[], value:n} where value is the maximum value present, and items are all items with the same value. The number of items can be optionally limited.
            /// </summary>
            public static MiscJaggedListResult Maxitems(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggMaxitems);
            }
            /// <summary>
            /// apoc.agg.median(number) - returns median for non-null numeric values
            /// </summary>
            public static MiscJaggedListResult Median(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggMedian);
            }
            /// <summary>
            /// apoc.agg.minItems(item, value, groupLimit: -1) - returns a map {items:[], value:n} where value is the minimum value present, and items are all items with the same value. The number of items can be optionally limited.
            /// </summary>
            public static MiscJaggedListResult Minitems(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggMinitems);
            }
            /// <summary>
            /// apoc.agg.nth(value,offset) - returns value of nth row (or -1 for last)
            /// </summary>
            public static MiscJaggedListResult Nth(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggNth);
            }
            /// <summary>
            /// apoc.agg.percentiles(value,[percentiles = 0.5,0.75,0.9,0.95,0.99]) - returns given percentiles for values
            /// </summary>
            public static MiscJaggedListResult Percentiles(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggPercentiles);
            }
            /// <summary>
            /// apoc.agg.product(number) - returns given product for non-null values
            /// </summary>
            public static MiscJaggedListResult Product(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggProduct);
            }
            /// <summary>
            /// apoc.agg.slice(value, start, length) - returns subset of non-null values, start is 0 based and length can be -1
            /// </summary>
            public static MiscJaggedListResult Slice(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggSlice);
            }
            /// <summary>
            /// apoc.agg.statistics(value,[percentiles = 0.5,0.75,0.9,0.95,0.99]) - returns numeric statistics (percentiles, min,minNonZero,max,total,mean,stdev) for values
            /// </summary>
            public static MiscJaggedListResult Statistics(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAggStatistics);
            }
        }

        public static partial class Algo
        {
            /// <summary>
            /// apoc.algo.allSimplePaths(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, 5) YIELD path, weight - run allSimplePaths with relationships given and maxNodes
            /// </summary>
            public static MiscJaggedListResult Allsimplepaths(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoAllsimplepaths);
            }
            /// <summary>
            /// apoc.algo.aStar(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, &apos;distance&apos;,&apos;lat&apos;,&apos;lon&apos;) YIELD path, weight - run A* with relationship property name as cost function
            /// </summary>
            public static MiscJaggedListResult Astar(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoAstar);
            }
            /// <summary>
            /// apoc.algo.aStar(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, {weight:&apos;dist&apos;,default:10,x:&apos;lon&apos;,y:&apos;lat&apos;}) YIELD path, weight - run A* with relationship property name as cost function
            /// </summary>
            public static MiscJaggedListResult Astarconfig(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoAstarconfig);
            }
            /// <summary>
            /// apoc.algo.cover(nodes) yield rel - returns all relationships between this set of nodes
            /// </summary>
            public static MiscJaggedListResult Cover(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoCover);
            }
            /// <summary>
            /// apoc.algo.dijkstra(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, &apos;distance&apos;, defaultValue, numberOfWantedResults) YIELD path, weight - run dijkstra with relationship property name as cost function
            /// </summary>
            public static MiscJaggedListResult Dijkstra(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoDijkstra);
            }
            /// <summary>
            /// apoc.algo.dijkstraWithDefaultWeight(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, &apos;distance&apos;, 10) YIELD path, weight - run dijkstra with relationship property name as cost function and a default weight if the property does not exist
            /// </summary>
            public static MiscJaggedListResult Dijkstrawithdefaultweight(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoDijkstrawithdefaultweight);
            }
        }

        public static partial class Any
        {
            /// <summary>
            /// returns properties for virtual and real, nodes, rels and maps
            /// </summary>
            public static MiscJaggedListResult Properties(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAnyProperties);
            }
            /// <summary>
            /// returns property for virtual and real, nodes, rels and maps
            /// </summary>
            public static MiscJaggedListResult Property(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocAnyProperty);
            }
        }

        public static partial class Atomic
        {
            /// <summary>
            /// apoc.atomic.add(node/relatonship,propertyName,number) Sums the property’s value with the &apos;number&apos; value
            /// </summary>
            public static MiscJaggedListResult Add(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicAdd(0));
            }
            /// <summary>
            /// apoc.atomic.concat(node/relatonship,propertyName,string) Concats the property’s value with the &apos;string&apos; value
            /// </summary>
            public static MiscJaggedListResult Concat(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicConcat(0));
            }
            /// <summary>
            /// apoc.atomic.insert(node/relatonship,propertyName,position,value) insert a value into the property’s array value at &apos;position&apos;
            /// </summary>
            public static MiscJaggedListResult Insert(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicInsert);
            }
            /// <summary>
            /// apoc.atomic.remove(node/relatonship,propertyName,position) remove the element at position &apos;position&apos;
            /// </summary>
            public static MiscJaggedListResult Remove(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicRemove(0));
            }
            /// <summary>
            /// apoc.atomic.subtract(node/relatonship,propertyName,number) Subtracts the &apos;number&apos; value to the property’s value
            /// </summary>
            public static MiscJaggedListResult Subtract(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicSubtract(0));
            }
            /// <summary>
            /// apoc.atomic.update(node/relatonship,propertyName,updateOperation) update a property’s value with a cypher operation (ex. &quot;n.prop1+n.prop2&quot;)
            /// </summary>
            public static MiscJaggedListResult Update(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicUpdate(0));
            }
        }

        public static partial class Bitwise
        {
            /// <summary>
            /// apoc.bitwise.op(60,&apos;|&apos;,13) bitwise operations a &amp; b, a | b, a ^ b, ~a, a &gt;&gt; b, a &gt;&gt;&gt; b, a &lt;&lt; b. returns the result of the bitwise operation
            /// </summary>
            public static MiscJaggedListResult Op(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocBitwiseOp);
            }
        }

        public static partial class Bolt
        {
            /// <summary>
            /// apoc.bolt.execute(url-or-key, kernelTransaction, params, config) - access to other databases via bolt for reads and writes
            /// </summary>
            public static MiscJaggedListResult Execute(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocBoltExecute);
            }
        }

        public static partial class Coll
        {
            /// <summary>
            /// apoc.coll.avg([0.5,1,2.3])
            /// </summary>
            public static MiscJaggedListResult Avg(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollAvg);
            }
            /// <summary>
            /// apoc.coll.combinations(coll, minSelect, maxSelect:minSelect) - Returns collection of all combinations of list elements of selection size between minSelect and maxSelect (default:minSelect), inclusive
            /// </summary>
            public static MiscJaggedListResult Combinations(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollCombinations);
            }
            /// <summary>
            /// apoc.coll.contains(coll, value) optimized contains operation (using a HashSet) (returns single row or not)
            /// </summary>
            public static MiscJaggedListResult Contains(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollContains);
            }
            /// <summary>
            /// apoc.coll.containsAll(coll, values) optimized contains-all operation (using a HashSet) (returns single row or not)
            /// </summary>
            public static MiscJaggedListResult Containsall(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollContainsall);
            }
            /// <summary>
            /// apoc.coll.containsAllSorted(coll, value) optimized contains-all on a sorted list operation (Collections.binarySearch) (returns single row or not)
            /// </summary>
            public static MiscJaggedListResult Containsallsorted(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollContainsallsorted);
            }
            /// <summary>
            /// apoc.coll.containsDuplicates(coll) - returns true if a collection contains duplicate elements
            /// </summary>
            public static MiscJaggedListResult Containsduplicates(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollContainsduplicates);
            }
            /// <summary>
            /// apoc.coll.containsSorted(coll, value) optimized contains on a sorted list operation (Collections.binarySearch) (returns single row or not)
            /// </summary>
            public static MiscJaggedListResult Containssorted(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollContainssorted);
            }
            /// <summary>
            /// apoc.coll.different(values) - returns true if values are different
            /// </summary>
            public static MiscJaggedListResult Different(params Result[] results)
            {
                return new MiscJaggedListResult(t => t.FnApocCollDifferent(results.Length), results);
            }
            /// <summary>
            /// apoc.coll.disjunction(first, second) - returns the disjunct set of the two lists
            /// </summary>
            public static MiscJaggedListResult Disjunction(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollDisjunction);
            }
            /// <summary>
            /// apoc.coll.dropDuplicateNeighbors(list) - remove duplicate consecutive objects in a list
            /// </summary>
            public static MiscJaggedListResult Dropduplicateneighbors(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollDropduplicateneighbors);
            }
            /// <summary>
            /// apoc.coll.duplicates(coll) - returns a list of duplicate items in the collection
            /// </summary>
            public static MiscJaggedListResult Duplicates(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollDuplicates);
            }
            /// <summary>
            /// apoc.coll.duplicatesWithCount(coll) - returns a list of duplicate items in the collection and their count, keyed by item and count (e.g., [{item: xyz, count:2}, {item:zyx, count:5}])
            /// </summary>
            public static MiscJaggedListResult Duplicateswithcount(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollDuplicateswithcount);
            }
            /// <summary>
            /// apoc.coll.elements(list,limit,offset) yield _1,_2,..,_10,_1s,_2i,_3f,_4m,_5l,_6n,_7r,_8p - deconstruct subset of mixed list into identifiers of the correct type
            /// </summary>
            public static MiscJaggedListResult Elements(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCollElements);
            }
            /// <summary>
            /// apoc.coll.fill(item, count) - returns a list with the given count of items
            /// </summary>
            public static MiscJaggedListResult Fill(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollFill);
            }
            /// <summary>
            /// apoc.coll.flatten(coll, [recursive]) - flattens list (nested if recursive is true)
            /// </summary>
            public static MiscJaggedListResult Flatten(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollFlatten(0));
            }
            /// <summary>
            /// apoc.coll.frequencies(coll) - returns a list of frequencies of the items in the collection, keyed by item and count (e.g., [{item: xyz, count:2}, {item:zyx, count:5}, {item:abc, count:1}])
            /// </summary>
            public static MiscJaggedListResult Frequencies(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollFrequencies);
            }
            /// <summary>
            /// apoc.coll.frequenciesAsMap(coll) - return a map of frequencies of the items in the collection, key item, value count (e.g., {1:2, 2:1})
            /// </summary>
            public static MiscJaggedListResult Frequenciesasmap(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollFrequenciesasmap);
            }
            /// <summary>
            /// apoc.coll.indexOf(coll, value) | position of value in the list
            /// </summary>
            public static MiscJaggedListResult Indexof(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollIndexof);
            }
            /// <summary>
            /// apoc.coll.insertAll(coll, index, values) | insert values at index
            /// </summary>
            public static MiscJaggedListResult Insertall(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollInsertall);
            }
            /// <summary>
            /// apoc.coll.intersection(first, second) - returns the unique intersection of the two lists
            /// </summary>
            public static MiscJaggedListResult Intersection(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollIntersection);
            }
            /// <summary>
            /// apoc.coll.isEqualCollection(coll, values) return true if two collections contain the same elements with the same cardinality in any order (using a HashMap)
            /// </summary>
            public static MiscJaggedListResult Isequalcollection(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollIsequalcollection);
            }
            /// <summary>
            /// apoc.coll.max([0.5,1,2.3])
            /// </summary>
            public static MiscJaggedListResult Max(params Result[] results)
            {
                return new MiscJaggedListResult(t => t.FnApocCollMax(results.Length), results);
            }
            /// <summary>
            /// apoc.coll.min([0.5,1,2.3])
            /// </summary>
            public static MiscJaggedListResult Min(params Result[] results)
            {
                return new MiscJaggedListResult(t => t.FnApocCollMin(results.Length), results);
            }
            /// <summary>
            /// apoc.coll.occurrences(coll, item) - returns the count of the given item in the collection
            /// </summary>
            public static MiscJaggedListResult Occurrences(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollOccurrences);
            }
            /// <summary>
            /// apoc.coll.pairs([1,2,3]) returns [1,2],[2,3],[3,null]
            /// </summary>
            public static MiscJaggedListResult Pairs(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollPairs);
            }
            /// <summary>
            /// apoc.coll.pairsMin([1,2,3]) returns [1,2],[2,3]
            /// </summary>
            public static MiscJaggedListResult Pairsmin(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollPairsmin);
            }
            /// <summary>
            /// apoc.coll.pairWithOffset(values, offset) - returns a list of pairs defined by the offset
            /// </summary>
            public static MiscJaggedListResult Pairwithoffset(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCollPairwithoffset);
            }
            /// <summary>
            /// apoc.coll.partition(list,batchSize)
            /// </summary>
            public static MiscJaggedListResult Partition(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCollPartition);
            }
            /// <summary>
            /// apoc.coll.randomItem(coll)- returns a random item from the list, or null on an empty or null list
            /// </summary>
            public static MiscJaggedListResult Randomitem(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRandomitem);
            }
            /// <summary>
            /// apoc.coll.randomItems(coll, itemCount, allowRepick: false) - returns a list of itemCount random items from the original list, optionally allowing picked elements to be picked again
            /// </summary>
            public static MiscJaggedListResult Randomitems(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRandomitems);
            }
            /// <summary>
            /// apoc.coll.removeAll(first, second) - returns first list with all elements of second list removed
            /// </summary>
            public static MiscJaggedListResult Removeall(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRemoveall);
            }
            /// <summary>
            /// apoc.coll.reverse(coll) - returns reversed list
            /// </summary>
            public static MiscJaggedListResult Reverse(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollReverse);
            }
            /// <summary>
            /// apoc.coll.runningTotal(list1) - returns an accumulative array. For example apoc.coll.runningTotal([1,2,3.5]) return [1,3,6.5]
            /// </summary>
            public static MiscJaggedListResult Runningtotal(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRunningtotal);
            }
            /// <summary>
            /// apoc.coll.set(coll, index, value) | set index to value
            /// </summary>
            public static MiscJaggedListResult Set(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSet);
            }
            /// <summary>
            /// apoc.coll.shuffle(coll) - returns the shuffled list
            /// </summary>
            public static MiscJaggedListResult Shuffle(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollShuffle);
            }
            /// <summary>
            /// apoc.coll.sort(coll) sort on Collections
            /// </summary>
            public static MiscJaggedListResult Sort(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSort);
            }
            /// <summary>
            /// apoc.coll.sortMaps([maps], &apos;name&apos;) - sort maps by property
            /// </summary>
            public static MiscJaggedListResult Sortmaps(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSortmaps);
            }
            /// <summary>
            /// apoc.coll.sortMulti(coll, [&apos;^name&apos;,&apos;age&apos;],[limit],[skip]) - sort list of maps by several sort fields (ascending with ^ prefix) and optionally applies limit and skip
            /// </summary>
            public static MiscJaggedListResult Sortmulti(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSortmulti(0));
            }
            /// <summary>
            /// apoc.coll.sortNodes([nodes], &apos;name&apos;) sort nodes by property
            /// </summary>
            public static MiscJaggedListResult Sortnodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSortnodes);
            }
            /// <summary>
            /// apoc.coll.sortText(coll) sort on string based collections
            /// </summary>
            public static MiscJaggedListResult Sorttext(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSorttext(0));
            }
            /// <summary>
            /// apoc.coll.split(list,value) | splits collection on given values rows of lists, value itself will not be part of resulting lists
            /// </summary>
            public static MiscJaggedListResult Split(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCollSplit);
            }
            /// <summary>
            /// apoc.coll.stdev(list, isBiasCorrected) - returns the sample or population standard deviation with isBiasCorrected true or false respectively. For example apoc.coll.stdev([10, 12, 23]) return 7
            /// </summary>
            public static MiscJaggedListResult Stdev(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollStdev);
            }
            /// <summary>
            /// apoc.coll.sum([0.5,1,2.3])
            /// </summary>
            public static MiscJaggedListResult Sum(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSum);
            }
            /// <summary>
            /// apoc.coll.sumLongs([1,3,3])
            /// </summary>
            public static MiscJaggedListResult Sumlongs(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSumlongs);
            }
            /// <summary>
            /// apoc.coll.toSet([list]) returns a unique list backed by a set
            /// </summary>
            public static MiscJaggedListResult Toset(params Result[] results)
            {
                return new MiscJaggedListResult(t => t.FnApocCollToset(results.Length), results);
            }
            /// <summary>
            /// apoc.coll.union(first, second) - creates the distinct union of the 2 lists
            /// </summary>
            public static MiscJaggedListResult Union(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollUnion);
            }
            /// <summary>
            /// apoc.coll.unionAll(first, second) - creates the full union with duplicates of the two lists
            /// </summary>
            public static MiscJaggedListResult Unionall(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollUnionall);
            }
            /// <summary>
            /// apoc.coll.zip([list1],[list2])
            /// </summary>
            public static MiscJaggedListResult Zip(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollZip);
            }
            /// <summary>
            /// apoc.coll.zipToRows(list1,list2) - creates pairs like zip but emits one row per pair
            /// </summary>
            public static MiscJaggedListResult Ziptorows(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCollZiptorows);
            }
            /// <summary>
            /// apoc.coll.insert(coll, index, value) | insert value at index
            /// </summary>
            public static MiscJaggedListResult Insert(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollInsert);
            }
            /// <summary>
            /// apoc.coll.remove(coll, index, [length=1]) | remove range of values from index to length
            /// </summary>
            public static MiscJaggedListResult Remove(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRemove(0));
            }
            /// <summary>
            /// apoc.coll.subtract(first, second) - returns unique set of first list with all elements of second list removed
            /// </summary>
            public static MiscJaggedListResult Subtract(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSubtract);
            }
        }

        public static partial class Convert
        {
            /// <summary>
            /// apoc.convert.fromJsonList(&apos;[1,2,3]&apos;[,&apos;json-path&apos;, &apos;path-options&apos;])
            /// </summary>
            public static MiscJaggedListResult Fromjsonlist(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertFromjsonlist(0));
            }
            /// <summary>
            /// apoc.convert.fromJsonMap(&apos;{&quot;a&quot;:42,&quot;b&quot;:&quot;foo&quot;,&quot;c&quot;:[1,2,3]}&apos;[,&apos;json-path&apos;, &apos;path-options&apos;])
            /// </summary>
            public static MiscJaggedListResult Fromjsonmap(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertFromjsonmap(0));
            }
            /// <summary>
            /// apoc.convert.getJsonProperty(node,key[,&apos;json-path&apos;, &apos;path-options&apos;]) - converts serialized JSON in property back to original object
            /// </summary>
            public static MiscJaggedListResult Getjsonproperty(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertGetjsonproperty(0));
            }
            /// <summary>
            /// apoc.convert.getJsonPropertyMap(node,key[,&apos;json-path&apos;, &apos;path-options&apos;]) - converts serialized JSON in property back to map
            /// </summary>
            public static MiscJaggedListResult Getjsonpropertymap(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertGetjsonpropertymap(0));
            }
            /// <summary>
            /// apoc.convert.setJsonProperty(node,key,complexValue) - sets value serialized to JSON as property with the given name on the node
            /// </summary>
            public static MiscJaggedListResult Setjsonproperty(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocConvertSetjsonproperty);
            }
            /// <summary>
            /// apoc.convert.toBoolean(value) | tries it’s best to convert the value to a boolean
            /// </summary>
            public static MiscJaggedListResult Toboolean(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertToboolean);
            }
            /// <summary>
            /// apoc.convert.toBooleanList(value) | tries it’s best to convert the value to a list of booleans
            /// </summary>
            public static MiscJaggedListResult Tobooleanlist(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTobooleanlist);
            }
            /// <summary>
            /// apoc.convert.toFloat(value) | tries it’s best to convert the value to a float
            /// </summary>
            public static MiscJaggedListResult Tofloat(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTofloat);
            }
            /// <summary>
            /// apoc.convert.toInteger(value) | tries it’s best to convert the value to an integer
            /// </summary>
            public static MiscJaggedListResult Tointeger(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTointeger);
            }
            /// <summary>
            /// apoc.convert.toIntList(value) | tries it’s best to convert the value to a list of integers
            /// </summary>
            public static MiscJaggedListResult Tointlist(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTointlist);
            }
            /// <summary>
            /// apoc.convert.toJson([1,2,3]) or toJson({a:42,b:&quot;foo&quot;,c:[1,2,3]}) or toJson(NODE/REL/PATH)
            /// </summary>
            public static MiscJaggedListResult Tojson(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTojson);
            }
            /// <summary>
            /// apoc.convert.toList(value) | tries it’s best to convert the value to a list
            /// </summary>
            public static MiscJaggedListResult Tolist(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTolist);
            }
            /// <summary>
            /// apoc.convert.toMap(value) | tries it’s best to convert the value to a map
            /// </summary>
            public static MiscJaggedListResult Tomap(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTomap);
            }
            /// <summary>
            /// apoc.convert.toNode(value) | tries it’s best to convert the value to a node
            /// </summary>
            public static MiscJaggedListResult Tonode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTonode);
            }
            /// <summary>
            /// apoc.convert.toNodeList(value) | tries it’s best to convert the value to a list of nodes
            /// </summary>
            public static MiscJaggedListResult Tonodelist(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTonodelist);
            }
            /// <summary>
            /// apoc.convert.toRelationship(value) | tries it’s best to convert the value to a relationship
            /// </summary>
            public static MiscJaggedListResult Torelationship(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTorelationship);
            }
            /// <summary>
            /// apoc.convert.toRelationshipList(value) | tries it’s best to convert the value to a list of relationships
            /// </summary>
            public static MiscJaggedListResult Torelationshiplist(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTorelationshiplist);
            }
            /// <summary>
            /// apoc.convert.toSortedJsonMap(node|map, ignoreCase:true) - returns a JSON map with keys sorted alphabetically, with optional case sensitivity
            /// </summary>
            public static MiscJaggedListResult Tosortedjsonmap(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTosortedjsonmap);
            }
            /// <summary>
            /// apoc.convert.toString(value) | tries it’s best to convert the value to a string
            /// </summary>
            public static MiscJaggedListResult Tostring(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTostring);
            }
            /// <summary>
            /// apoc.convert.toStringList(value) | tries it’s best to convert the value to a list of strings
            /// </summary>
            public static MiscJaggedListResult Tostringlist(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTostringlist);
            }
            /// <summary>
            /// apoc.convert.toTree([paths],[lowerCaseRels=true], [config]) creates a stream of nested documents representing the at least one root of these paths
            /// </summary>
            public static MiscJaggedListResult Totree(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocConvertTotree);
            }
            /// <summary>
            /// apoc.convert.toSet(value) | tries it’s best to convert the value to a set
            /// </summary>
            public static MiscJaggedListResult Toset(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertToset);
            }
        }

        public static partial class Create
        {
            /// <summary>
            /// apoc.create.addLabels( [node,id,ids,nodes], [&apos;Label&apos;,…​]) - adds the given labels to the node or nodes
            /// </summary>
            public static MiscJaggedListResult Addlabels(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateAddlabels);
            }
            /// <summary>
            /// apoc.create.clonePathsToVirtual
            /// </summary>
            public static MiscJaggedListResult Clonepathstovirtual(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateClonepathstovirtual);
            }
            /// <summary>
            /// apoc.create.clonePathToVirtual
            /// </summary>
            public static MiscJaggedListResult Clonepathtovirtual(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateClonepathtovirtual);
            }
            /// <summary>
            /// apoc.create.node([&apos;Label&apos;], {key:value,…​}) - create node with dynamic labels
            /// </summary>
            public static MiscJaggedListResult Node(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateNode);
            }
            /// <summary>
            /// apoc.create.nodes([&apos;Label&apos;], [{key:value,…​}]) create multiple nodes with dynamic labels
            /// </summary>
            public static MiscJaggedListResult Nodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateNodes);
            }
            /// <summary>
            /// apoc.create.relationship(person1,&apos;KNOWS&apos;,{key:value,…​}, person2) create relationship with dynamic rel-type
            /// </summary>
            public static MiscJaggedListResult Relationship(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateRelationship);
            }
            /// <summary>
            /// apoc.create.removeLabels( [node,id,ids,nodes], [&apos;Label&apos;,…​]) - removes the given labels from the node or nodes
            /// </summary>
            public static MiscJaggedListResult Removelabels(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateRemovelabels);
            }
            /// <summary>
            /// apoc.create.removeProperties( [node,id,ids,nodes], [keys]) - removes the given properties from the nodes(s)
            /// </summary>
            public static MiscJaggedListResult Removeproperties(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateRemoveproperties);
            }
            /// <summary>
            /// apoc.create.removeRelProperties( [rel,id,ids,rels], [keys]) - removes the given properties from the relationship(s)
            /// </summary>
            public static MiscJaggedListResult Removerelproperties(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateRemoverelproperties);
            }
            /// <summary>
            /// apoc.create.setLabels( [node,id,ids,nodes], [&apos;Label&apos;,…​]) - sets the given labels, non matching labels are removed on the node or nodes
            /// </summary>
            public static MiscJaggedListResult Setlabels(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateSetlabels);
            }
            /// <summary>
            /// apoc.create.setProperties( [node,id,ids,nodes], [keys], [values]) - sets the given properties on the nodes(s)
            /// </summary>
            public static MiscJaggedListResult Setproperties(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateSetproperties);
            }
            /// <summary>
            /// apoc.create.setProperty( [node,id,ids,nodes], key, value) - sets the given property on the node(s)
            /// </summary>
            public static MiscJaggedListResult Setproperty(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateSetproperty);
            }
            /// <summary>
            /// apoc.create.setRelProperties( [rel,id,ids,rels], [keys], [values]) - sets the given properties on the relationship(s)
            /// </summary>
            public static MiscJaggedListResult Setrelproperties(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateSetrelproperties);
            }
            /// <summary>
            /// apoc.create.setRelProperty( [rel,id,ids,rels], key, value) - sets the given property on the relationship(s)
            /// </summary>
            public static MiscJaggedListResult Setrelproperty(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateSetrelproperty);
            }
            /// <summary>
            /// apoc.create.uuid() - creates an UUID
            /// </summary>
            public static MiscJaggedListResult Uuid(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCreateUuid);
            }
            /// <summary>
            /// apoc.create.uuids(count) yield uuid - creates &apos;count&apos; UUIDs
            /// </summary>
            public static MiscJaggedListResult Uuids(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateUuids);
            }
            /// <summary>
            /// apoc.create.virtualPath([&apos;LabelA&apos;],{key:value},&apos;KNOWS&apos;,{key:value,…​},[&apos;LabelB&apos;],{key:value}) returns a virtual path of nodes joined by a relationship and the associated properties
            /// </summary>
            public static MiscJaggedListResult Virtualpath(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVirtualpath);
            }
            /// <summary>
            /// apoc.create.vNode([&apos;Label&apos;], {key:value,…​}) returns a virtual node
            /// </summary>
            public static MiscJaggedListResult Vnode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVnode(0));
            }
            /// <summary>
            /// apoc.create.vNodes([&apos;Label&apos;], [{key:value,…​}]) returns virtual nodes
            /// </summary>
            public static MiscJaggedListResult Vnodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVnodes);
            }
            /// <summary>
            /// apoc.create.vPattern({_labels:[&apos;LabelA&apos;],key:value},&apos;KNOWS&apos;,{key:value,…​}, {_labels:[&apos;LabelB&apos;],key:value}) returns a virtual pattern
            /// </summary>
            public static MiscJaggedListResult Vpattern(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVpattern);
            }
            /// <summary>
            /// apoc.create.vPatternFull([&apos;LabelA&apos;],{key:value},&apos;KNOWS&apos;,{key:value,…​},[&apos;LabelB&apos;],{key:value}) returns a virtual pattern
            /// </summary>
            public static MiscJaggedListResult Vpatternfull(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVpatternfull);
            }
            /// <summary>
            /// apoc.create.vRelationship(nodeFrom,&apos;KNOWS&apos;,{key:value,…​}, nodeTo) returns a virtual relationship
            /// </summary>
            public static MiscJaggedListResult Vrelationship(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVrelationship);
            }
        }

        public static partial class CreateVirtual
        {
            /// <summary>
            /// apoc.create.virtual.fromNode(node, [propertyNames]) returns a virtual node built from an existing node with only the requested properties
            /// </summary>
            public static MiscJaggedListResult Fromnode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCreateVirtualFromnode);
            }
        }

        public static partial class Cypher
        {
            /// <summary>
            /// apoc.cypher.doIt(fragment, params) yield value - executes writing fragment with the given parameters
            /// </summary>
            public static MiscJaggedListResult Doit(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherDoit);
            }
            /// <summary>
            /// apoc.cypher.run(fragment, params) yield value - executes reading fragment with the given parameters - currently no schema operations
            /// </summary>
            public static MiscJaggedListResult Run(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRun);
            }
            /// <summary>
            /// use either apoc.cypher.runFirstColumnMany for a list return or apoc.cypher.runFirstColumnSingle for returning the first row of the first column
            /// </summary>
            public static MiscJaggedListResult Runfirstcolumn(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCypherRunfirstcolumn);
            }
            /// <summary>
            /// apoc.cypher.runFirstColumnMany(statement, params) - executes statement with given parameters, returns first column only collected into a list, params are available as identifiers
            /// </summary>
            public static MiscJaggedListResult Runfirstcolumnmany(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCypherRunfirstcolumnmany);
            }
            /// <summary>
            /// apoc.cypher.runFirstColumnSingle(statement, params) - executes statement with given parameters, returns first element of the first column only, params are available as identifiers
            /// </summary>
            public static MiscJaggedListResult Runfirstcolumnsingle(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocCypherRunfirstcolumnsingle);
            }
            /// <summary>
            /// apoc.cypher.runMany(&apos;cypher;\nstatements;&apos;, $params, [{statistics:true,timeout:10}]) - runs each semicolon separated statement and returns summary - currently no schema operations
            /// </summary>
            public static MiscJaggedListResult Runmany(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRunmany);
            }
            /// <summary>
            /// apoc.cypher.runManyReadOnly(&apos;cypher;\nstatements;&apos;, $params, [{statistics:true,timeout:10}]) - runs each semicolon separated, read-only statement and returns summary - currently no schema operations
            /// </summary>
            public static MiscJaggedListResult Runmanyreadonly(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRunmanyreadonly);
            }
            /// <summary>
            /// apoc.cypher.runSchema(statement, params) yield value - executes query schema statement with the given parameters
            /// </summary>
            public static MiscJaggedListResult Runschema(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRunschema);
            }
            /// <summary>
            /// apoc.cypher.runTimeboxed(&apos;cypherStatement&apos;,{params}, timeout) - abort kernelTransaction after timeout ms if not finished
            /// </summary>
            public static MiscJaggedListResult Runtimeboxed(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRuntimeboxed);
            }
            /// <summary>
            /// apoc.cypher.runWrite(statement, params) yield value - alias for apoc.cypher.doIt
            /// </summary>
            public static MiscJaggedListResult Runwrite(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRunwrite);
            }
        }

        public static partial class Data
        {
            /// <summary>
            /// apoc.data.domain(&apos;url_or_email_address&apos;) YIELD domain - extract the domain name from a url or an email address. If nothing was found, yield null.
            /// </summary>
            public static MiscJaggedListResult Domain(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDataDomain);
            }
            /// <summary>
            /// apoc.data.url(&apos;url&apos;) as {protocol,host,port,path,query,file,anchor,user} | turn URL into map structure
            /// </summary>
            public static MiscJaggedListResult Url(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDataUrl);
            }
        }

        public static partial class Date
        {
            /// <summary>
            /// apoc.date.convert(12345, &apos;ms&apos;, &apos;d&apos;) - convert a timestamp in one time unit into one of a different time unit
            /// </summary>
            public static MiscJaggedListResult Convert(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateConvert);
            }
            /// <summary>
            /// apoc.date.convertFormat(&apos;Tue, 14 May 2019 14:52:06 -0400&apos;, &apos;rfc_1123_date_time&apos;, &apos;iso_date_time&apos;) - convert a String of one date format into a String of another date format.
            /// </summary>
            public static MiscJaggedListResult Convertformat(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateConvertformat);
            }
            /// <summary>
            /// apoc.date.currentTimestamp() - returns System.currentTimeMillis() at the time it was called. The value is current throughout transaction execution, and is different from Cypher’s timestamp() function, which does not update within a transaction.
            /// </summary>
            public static MiscJaggedListResult Currenttimestamp(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateCurrenttimestamp);
            }
            /// <summary>
            /// apoc.date.field(12345,(&apos;ms|s|m|h|d|month|year&apos;),(&apos;TZ&apos;)
            /// </summary>
            public static MiscJaggedListResult Field(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateField(0));
            }
            /// <summary>
            /// apoc.date.fields(&apos;2012-12-23&apos;,(&apos;yyyy-MM-dd&apos;)) - return columns and a map representation of date parsed with the given format with entries for years,months,weekdays,days,hours,minutes,seconds,zoneid
            /// </summary>
            public static MiscJaggedListResult Fields(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateFields(0));
            }
            /// <summary>
            /// apoc.date.format(12345,(&apos;ms|s|m|h|d&apos;),(&apos;yyyy-MM-dd HH:mm:ss zzz&apos;),(&apos;TZ&apos;)) - get string representation of time value optionally using the specified unit (default ms) using specified format (default ISO) and specified time zone (default current TZ)
            /// </summary>
            public static MiscJaggedListResult Format(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateFormat(0));
            }
            /// <summary>
            /// apoc.date.fromISO8601(&apos;yyyy-MM-ddTHH:mm:ss.SSSZ&apos;) - return number representation of time in EPOCH format
            /// </summary>
            public static MiscJaggedListResult Fromiso8601(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateFromiso8601);
            }
            /// <summary>
            /// apoc.date.parse(&apos;2012-12-23&apos;,&apos;ms|s|m|h|d&apos;,&apos;yyyy-MM-dd&apos;) - parse date string using the specified format into the specified time unit
            /// </summary>
            public static MiscJaggedListResult Parse(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateParse(0));
            }
            /// <summary>
            /// apoc.date.parseAsZonedDateTime(&apos;2012-12-23 23:59:59&apos;,&apos;yyyy-MM-dd HH:mm:ss&apos;, &apos;UTC-hour-offset&apos;) - parse date string using the specified format to specified timezone
            /// </summary>
            public static MiscJaggedListResult Parseaszoneddatetime(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateParseaszoneddatetime(0));
            }
            /// <summary>
            /// apoc.date.systemTimezone() - returns the system timezone display name
            /// </summary>
            public static MiscJaggedListResult Systemtimezone(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateSystemtimezone);
            }
            /// <summary>
            /// apoc.date.toISO8601(12345,(&apos;ms|s|m|h|d&apos;) - return string representation of time in ISO8601 format
            /// </summary>
            public static MiscJaggedListResult Toiso8601(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateToiso8601(0));
            }
            /// <summary>
            /// toYears(timestamp) or toYears(date[,format]) - converts timestamp into floating point years
            /// </summary>
            public static MiscJaggedListResult Toyears(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateToyears);
            }
            /// <summary>
            /// apoc.date.add(12345, &apos;ms&apos;, -365, &apos;d&apos;) - given a timestamp in one time unit, adds a value of the specified time unit
            /// </summary>
            public static MiscJaggedListResult Add(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDateAdd);
            }
        }

        public static partial class Diff
        {
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult Nodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocDiffNodes);
            }
        }

        public static partial class Do
        {
            /// <summary>
            /// apoc.do.case([condition, query, condition, query, …​], elseQuery:&apos;&apos;, params:{}) yield value - given a list of conditional / writing query pairs, executes the query associated with the first conditional evaluating to true (or the else query if none are true) with the given parameters
            /// </summary>
            public static MiscJaggedListResult Case(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocDoCase(0));
            }
            /// <summary>
            /// apoc.do.when(condition, ifQuery, elseQuery:&apos;&apos;, params:{}) yield value - based on the conditional, executes writing ifQuery or elseQuery with the given parameters
            /// </summary>
            public static MiscJaggedListResult When(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocDoWhen(0));
            }
        }

        public static partial class Dv
        {
            /// <summary>
            /// Query a virtualized resource by name and return virtual nodes linked using virtual rels to the node passed as first param
            /// </summary>
            public static MiscJaggedListResult Queryandlink(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocDvQueryandlink);
            }
            /// <summary>
            /// Query a virtualized resource by name and return virtual nodes
            /// </summary>
            public static MiscJaggedListResult Query(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocDvQuery(0));
            }
        }

        public static partial class DvCatalog
        {
            /// <summary>
            /// Add a virtualized resource configuration
            /// </summary>
            public static MiscJaggedListResult Add(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocDvCatalogAdd);
            }
            /// <summary>
            /// List all virtualized resource configs
            /// </summary>
            public static MiscJaggedListResult List(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocDvCatalogList);
            }
            /// <summary>
            /// Remove a virtualized resource config by name
            /// </summary>
            public static MiscJaggedListResult Remove(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocDvCatalogRemove);
            }
        }

        public static partial class Example
        {
            /// <summary>
            /// apoc.example.movies() | Creates the sample movies graph
            /// </summary>
            public static MiscJaggedListResult Movies(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExampleMovies);
            }
        }

        public static partial class Export
        {
            /// <summary>
            /// apoc.export.cypherAll(file,config) - exports whole database incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Cypherall(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherall);
            }
            /// <summary>
            /// apoc.export.cypherData(nodes,rels,file,config) - exports given nodes and relationships incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Cypherdata(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherdata);
            }
            /// <summary>
            /// apoc.export.cypherGraph(graph,file,config) - exports given graph object incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Cyphergraph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCyphergraph);
            }
            /// <summary>
            /// apoc.export.cypherQuery(query,file,config) - exports nodes and relationships from the cypher kernelTransaction incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Cypherquery(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherquery);
            }
        }

        public static partial class ExportCsv
        {
            /// <summary>
            /// apoc.export.csv.all(file,config) - exports whole database as csv to the provided file
            /// </summary>
            public static MiscJaggedListResult All(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCsvAll);
            }
            /// <summary>
            /// apoc.export.csv.data(nodes,rels,file,config) - exports given nodes and relationships as csv to the provided file
            /// </summary>
            public static MiscJaggedListResult Data(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCsvData);
            }
            /// <summary>
            /// apoc.export.csv.graph(graph,file,config) - exports given graph object as csv to the provided file
            /// </summary>
            public static MiscJaggedListResult Graph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCsvGraph);
            }
            /// <summary>
            /// apoc.export.csv.query(query,file,{config,…​,params:{params}}) - exports results from the cypher statement as csv to the provided file
            /// </summary>
            public static MiscJaggedListResult Query(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCsvQuery);
            }
        }

        public static partial class ExportCypher
        {
            /// <summary>
            /// apoc.export.cypher.schema(file,config) - exports all schema indexes and constraints to cypher
            /// </summary>
            public static MiscJaggedListResult Schema(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherSchema);
            }
            /// <summary>
            /// apoc.export.cypher.all(file,config) - exports whole database incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult All(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherAll);
            }
            /// <summary>
            /// apoc.export.cypher.data(nodes,rels,file,config) - exports given nodes and relationships incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Data(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherData);
            }
            /// <summary>
            /// apoc.export.cypher.graph(graph,file,config) - exports given graph object incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Graph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherGraph);
            }
            /// <summary>
            /// apoc.export.cypher.query(query,file,config) - exports nodes and relationships from the cypher statement incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Query(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherQuery);
            }
        }

        public static partial class ExportGraphml
        {
            /// <summary>
            /// apoc.export.graphml.all(file,config) - exports whole database as graphml to the provided file
            /// </summary>
            public static MiscJaggedListResult All(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportGraphmlAll);
            }
            /// <summary>
            /// apoc.export.graphml.data(nodes,rels,file,config) - exports given nodes and relationships as graphml to the provided file
            /// </summary>
            public static MiscJaggedListResult Data(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportGraphmlData);
            }
            /// <summary>
            /// apoc.export.graphml.graph(graph,file,config) - exports given graph object as graphml to the provided file
            /// </summary>
            public static MiscJaggedListResult Graph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportGraphmlGraph);
            }
            /// <summary>
            /// apoc.export.graphml.query(query,file,config) - exports nodes and relationships from the cypher statement as graphml to the provided file
            /// </summary>
            public static MiscJaggedListResult Query(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportGraphmlQuery);
            }
        }

        public static partial class ExportJson
        {
            /// <summary>
            /// apoc.export.json.all(file,config) - exports whole database as json to the provided file
            /// </summary>
            public static MiscJaggedListResult All(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportJsonAll);
            }
            /// <summary>
            /// apoc.export.json.data(nodes,rels,file,config) - exports given nodes and relationships as json to the provided file
            /// </summary>
            public static MiscJaggedListResult Data(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportJsonData);
            }
            /// <summary>
            /// apoc.export.json.graph(graph,file,config) - exports given graph object as json to the provided file
            /// </summary>
            public static MiscJaggedListResult Graph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportJsonGraph);
            }
            /// <summary>
            /// apoc.export.json.query(query,file,{config,…​,params:{params}}) - exports results from the cypher statement as json to the provided file
            /// </summary>
            public static MiscJaggedListResult Query(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocExportJsonQuery);
            }
        }

        public static partial class Graph
        {
            /// <summary>
            /// apoc.graph.from(data,&apos;name&apos;,{properties}) | creates a virtual graph object for later processing it tries its best to extract the graph information from the data you pass in
            /// </summary>
            public static MiscJaggedListResult From(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFrom);
            }
            /// <summary>
            /// apoc.graph.fromCypher(&apos;kernelTransaction&apos;,{params},&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscJaggedListResult Fromcypher(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFromcypher);
            }
            /// <summary>
            /// apoc.graph.fromData([nodes],[relationships],&apos;name&apos;,{properties}) | creates a virtual graph object for later processing
            /// </summary>
            public static MiscJaggedListResult Fromdata(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFromdata);
            }
            /// <summary>
            /// apoc.graph.fromDB(&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscJaggedListResult Fromdb(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFromdb);
            }
            /// <summary>
            /// apoc.graph.fromDocument({json}, {config}) yield graph - transform JSON documents into graph structures
            /// </summary>
            public static MiscJaggedListResult Fromdocument(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFromdocument);
            }
            /// <summary>
            /// apoc.graph.fromPath(path,&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscJaggedListResult Frompath(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFrompath);
            }
            /// <summary>
            /// apoc.graph.fromPaths([paths],&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscJaggedListResult Frompaths(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFrompaths);
            }
            /// <summary>
            /// apoc.graph.validateDocument({json}, {config}) yield row - validates the json, return the result of the validation
            /// </summary>
            public static MiscJaggedListResult Validatedocument(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphValidatedocument);
            }
        }

        public static partial class Hashing
        {
            /// <summary>
            /// calculate a checksum (md5) over a node or a relationship. This deals gracefully with array properties. Two identical entities do share the same hash. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static MiscJaggedListResult Fingerprint(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocHashingFingerprint);
            }
            /// <summary>
            /// calculate a checksum (md5) over a the full graph. Be aware that this function does use in-memomry datastructures depending on the size of your graph. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static MiscJaggedListResult Fingerprintgraph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocHashingFingerprintgraph(0));
            }
            /// <summary>
            /// calculate a checksum (md5) over a node or a relationship. This deals gracefully with array properties. Two identical entities do share the same hash. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static MiscJaggedListResult Fingerprinting(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocHashingFingerprinting);
            }
        }

        public static partial class Import
        {
            /// <summary>
            /// apoc.import.csv(nodes, relationships, config) - imports nodes and relationships from the provided CSV files with given labels and types
            /// </summary>
            public static MiscJaggedListResult Csv(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocImportCsv);
            }
            /// <summary>
            /// apoc.import.graphml(urlOrBinaryFile,config) - imports graphml file
            /// </summary>
            public static MiscJaggedListResult Graphml(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocImportGraphml);
            }
            /// <summary>
            /// apoc.import.json(urlOrBinaryFile,config) - imports the json list to the provided file
            /// </summary>
            public static MiscJaggedListResult Json(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocImportJson);
            }
            /// <summary>
            /// apoc.import.xml(file,config) - imports graph from provided file
            /// </summary>
            public static MiscJaggedListResult Xml(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocImportXml);
            }
        }

        public static partial class Json
        {
            /// <summary>
            /// apoc.json.path(&apos;{json}&apos; [,&apos;json-path&apos; , &apos;path-options&apos;])
            /// </summary>
            public static MiscJaggedListResult Path(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocJsonPath(0));
            }
        }

        public static partial class Label
        {
            /// <summary>
            /// apoc.label.exists(element, label) - returns true or false related to label existance
            /// </summary>
            public static MiscJaggedListResult Exists(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocLabelExists);
            }
        }

        public static partial class Load
        {
            /// <summary>
            /// apoc.load.jsonArray(&apos;url&apos;) YIELD value - load array from JSON URL (e.g. web-api) to import JSON as stream of values
            /// </summary>
            public static MiscJaggedListResult Jsonarray(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocLoadJsonarray);
            }
            /// <summary>
            /// apoc.load.jsonParams(&apos;urlOrKeyOrBinary&apos;,{header:value},payload, config) YIELD value - load from JSON URL (e.g. web-api) while sending headers / payload to import JSON as stream of values if the JSON was an array or a single value if it was a map
            /// </summary>
            public static MiscJaggedListResult Jsonparams(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocLoadJsonparams);
            }
            /// <summary>
            /// apoc.load.json(&apos;urlOrKeyOrBinary&apos;,path, config) YIELD value - import JSON as stream of values if the JSON was an array or a single value if it was a map
            /// </summary>
            public static MiscJaggedListResult Json(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocLoadJson);
            }
            /// <summary>
            /// apoc.load.xml(&apos;http://example.com/test.xml&apos;, &apos;xPath&apos;,config, false) YIELD value as doc CREATE (p:Person) SET p.name = doc.name - load from XML URL (e.g. web-api) to import XML as single nested map with attributes and _type, _text and _childrenx fields.
            /// </summary>
            public static MiscJaggedListResult Xml(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocLoadXml);
            }
        }

        public static partial class Lock
        {
            /// <summary>
            /// apoc.lock.all([nodes],[relationships]) acquires a write lock on the given nodes and relationships
            /// </summary>
            public static MiscJaggedListResult All(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocLockAll);
            }
            /// <summary>
            /// apoc.lock.nodes([nodes]) acquires a write lock on the given nodes
            /// </summary>
            public static MiscJaggedListResult Nodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocLockNodes);
            }
            /// <summary>
            /// apoc.lock.rels([relationships]) acquires a write lock on the given relationship
            /// </summary>
            public static MiscJaggedListResult Rels(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocLockRels);
            }
        }

        public static partial class LockRead
        {
            /// <summary>
            /// apoc.lock.read.nodes([nodes]) acquires a read lock on the given nodes
            /// </summary>
            public static MiscJaggedListResult Nodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocLockReadNodes);
            }
            /// <summary>
            /// apoc.lock.read.rels([relationships]) acquires a read lock on the given relationship
            /// </summary>
            public static MiscJaggedListResult Rels(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocLockReadRels);
            }
        }

        public static partial class Log
        {
            /// <summary>
            /// apoc.log.stream(&apos;neo4j.log&apos;, { last: n }) - retrieve log file contents, optionally return only the last n lines
            /// </summary>
            public static MiscJaggedListResult Stream(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocLogStream);
            }
        }

        public static partial class Map
        {
            /// <summary>
            /// apoc.map.clean(map,[skip,keys],[skip,values]) yield map filters the keys and values contained in those lists, good for data cleaning from CSV/JSON
            /// </summary>
            public static MiscJaggedListResult Clean(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapClean);
            }
            /// <summary>
            /// apoc.map.fromLists([keys],[values])
            /// </summary>
            public static MiscJaggedListResult Fromlists(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapFromlists);
            }
            /// <summary>
            /// apoc.map.fromNodes(label, property)
            /// </summary>
            public static MiscJaggedListResult Fromnodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapFromnodes);
            }
            /// <summary>
            /// apoc.map.fromPairs([[key,value],[key2,value2],…​])
            /// </summary>
            public static MiscJaggedListResult Frompairs(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapFrompairs);
            }
            /// <summary>
            /// apoc.map.fromValues([key1,value1,key2,value2,…​])
            /// </summary>
            public static MiscJaggedListResult Fromvalues(params Result[] results)
            {
                return new MiscJaggedListResult(t => t.FnApocMapFromvalues(results.Length), results);
            }
            /// <summary>
            /// apoc.map.groupBy([maps/nodes/relationships],&apos;key&apos;) yield value - creates a map of the list keyed by the given property, with single values
            /// </summary>
            public static MiscJaggedListResult Groupby(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapGroupby);
            }
            /// <summary>
            /// apoc.map.groupByMulti([maps/nodes/relationships],&apos;key&apos;) yield value - creates a map of the list keyed by the given property, with list values
            /// </summary>
            public static MiscJaggedListResult Groupbymulti(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapGroupbymulti);
            }
            /// <summary>
            /// apoc.map.merge(first,second) - merges two maps
            /// </summary>
            public static MiscJaggedListResult Merge(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapMerge);
            }
            /// <summary>
            /// apoc.map.mergeList([{maps}]) yield value - merges all maps in the list into one
            /// </summary>
            public static MiscJaggedListResult Mergelist(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapMergelist);
            }
            /// <summary>
            /// apoc.map.mget(map,key,[defaults],[fail=true])  - returns list of values for keys or throws exception if one of the key doesn’t exist and no default value given at that position
            /// </summary>
            public static MiscJaggedListResult Mget(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapMget(0));
            }
            /// <summary>
            /// apoc.map.removeKey(map,key,{recursive:true/false}) - remove the key from the map (recursively if recursive is true)
            /// </summary>
            public static MiscJaggedListResult Removekey(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapRemovekey);
            }
            /// <summary>
            /// apoc.map.removeKeys(map,[keys],{recursive:true/false}) - remove the keys from the map (recursively if recursive is true)
            /// </summary>
            public static MiscJaggedListResult Removekeys(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapRemovekeys);
            }
            /// <summary>
            /// apoc.map.setEntry(map,key,value)
            /// </summary>
            public static MiscJaggedListResult Setentry(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSetentry);
            }
            /// <summary>
            /// apoc.map.setKey(map,key,value)
            /// </summary>
            public static MiscJaggedListResult Setkey(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSetkey);
            }
            /// <summary>
            /// apoc.map.setLists(map,[keys],[values])
            /// </summary>
            public static MiscJaggedListResult Setlists(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSetlists);
            }
            /// <summary>
            /// apoc.map.setPairs(map,[[key1,value1],[key2,value2])
            /// </summary>
            public static MiscJaggedListResult Setpairs(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSetpairs);
            }
            /// <summary>
            /// apoc.map.setValues(map,[key1,value1,key2,value2])
            /// </summary>
            public static MiscJaggedListResult Setvalues(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSetvalues);
            }
            /// <summary>
            /// apoc.map.sortedProperties(map, ignoreCase:true) - returns a list of key/value list pairs, with pairs sorted by keys alphabetically, with optional case sensitivity
            /// </summary>
            public static MiscJaggedListResult Sortedproperties(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSortedproperties);
            }
            /// <summary>
            /// apoc.map.submap(map,keys,[defaults],[fail=true])  - returns submap for keys or throws exception if one of the key doesn’t exist and no default value given at that position
            /// </summary>
            public static MiscJaggedListResult Submap(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSubmap(0));
            }
            /// <summary>
            /// apoc.map.unflatten(map, delimiter:&apos;.&apos;) yield map - unflat from items separated by delimiter string to nested items (reverse of apoc.map.flatten function)
            /// </summary>
            public static MiscJaggedListResult Unflatten(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapUnflatten);
            }
            /// <summary>
            /// apoc.map.updateTree(tree,key,) returns map - adds the {data} map on each level of the nested tree, where the key-value pairs match
            /// </summary>
            public static MiscJaggedListResult Updatetree(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapUpdatetree);
            }
            /// <summary>
            /// apoc.map.values(map, [key1,key2,key3,…​],[addNullsForMissing]) returns list of values indicated by the keys
            /// </summary>
            public static MiscJaggedListResult Values(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapValues);
            }
            /// <summary>
            /// apoc.map.flatten(map, delimiter:&apos;.&apos;) yield map - flattens nested items in map using dot notation
            /// </summary>
            public static MiscJaggedListResult Flatten(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapFlatten);
            }
            /// <summary>
            /// apoc.map.get(map,key,[default],[fail=true]) - returns value for key or throws exception if key doesn’t exist and no default given
            /// </summary>
            public static MiscJaggedListResult Get(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMapGet);
            }
        }

        public static partial class Math
        {
            /// <summary>
            /// apoc.math.cosh(val) | returns the hyperbolic cosin
            /// </summary>
            public static MiscJaggedListResult Cosh(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathCosh);
            }
            /// <summary>
            /// apoc.math.coth(val) | returns the hyperbolic cotangent
            /// </summary>
            public static MiscJaggedListResult Coth(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathCoth);
            }
            /// <summary>
            /// apoc.math.csch(val) | returns the hyperbolic cosecant
            /// </summary>
            public static MiscJaggedListResult Csch(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathCsch);
            }
            /// <summary>
            /// apoc.math.maxByte() | return the maximum value an byte can have
            /// </summary>
            public static MiscJaggedListResult Maxbyte(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathMaxbyte);
            }
            /// <summary>
            /// apoc.math.maxDouble() | return the largest positive finite value of type double
            /// </summary>
            public static MiscJaggedListResult Maxdouble(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathMaxdouble);
            }
            /// <summary>
            /// apoc.math.maxInt() | return the maximum value an int can have
            /// </summary>
            public static MiscJaggedListResult Maxint(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathMaxint);
            }
            /// <summary>
            /// apoc.math.maxLong() | return the maximum value a long can have
            /// </summary>
            public static MiscJaggedListResult Maxlong(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathMaxlong);
            }
            /// <summary>
            /// apoc.math.minByte() | return the minimum value an byte can have
            /// </summary>
            public static MiscJaggedListResult Minbyte(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathMinbyte);
            }
            /// <summary>
            /// apoc.math.minDouble() | return the smallest positive nonzero value of type double
            /// </summary>
            public static MiscJaggedListResult Mindouble(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathMindouble);
            }
            /// <summary>
            /// apoc.math.minInt() | return the minimum value an int can have
            /// </summary>
            public static MiscJaggedListResult Minint(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathMinint);
            }
            /// <summary>
            /// apoc.math.minLong() | return the minimum value a long can have
            /// </summary>
            public static MiscJaggedListResult Minlong(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathMinlong);
            }
            /// <summary>
            /// apoc.math.regr(label, propertyY, propertyX) - It calculates the coefficient of determination (R-squared) for the values of propertyY and propertyX in the provided label
            /// </summary>
            public static MiscJaggedListResult Regr(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMathRegr);
            }
            /// <summary>
            /// apoc.math.round(value,[prec],mode=[CEILING,FLOOR,UP,DOWN,HALF_EVEN,HALF_DOWN,HALF_UP,DOWN,UNNECESSARY])
            /// </summary>
            public static MiscJaggedListResult Round(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathRound);
            }
            /// <summary>
            /// apoc.math.sech(val) | returns the hyperbolic secant
            /// </summary>
            public static MiscJaggedListResult Sech(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathSech);
            }
            /// <summary>
            /// apoc.math.sigmoid(val) | returns the sigmoid value
            /// </summary>
            public static MiscJaggedListResult Sigmoid(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathSigmoid);
            }
            /// <summary>
            /// apoc.math.sigmoidPrime(val) | returns the sigmoid prime [ sigmoid(val) * (1 - sigmoid(val)) ]
            /// </summary>
            public static MiscJaggedListResult Sigmoidprime(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathSigmoidprime);
            }
            /// <summary>
            /// apoc.math.sinh(val) | returns the hyperbolic sin
            /// </summary>
            public static MiscJaggedListResult Sinh(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathSinh);
            }
            /// <summary>
            /// apoc.math.tanh(val) | returns the hyperbolic tangent
            /// </summary>
            public static MiscJaggedListResult Tanh(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMathTanh);
            }
        }

        public static partial class Merge
        {
            /// <summary>
            /// &quot;apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Node(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeNode(0));
            }
            /// <summary>
            /// apoc.merge.relationship(startNode, relType,  identProps:{key:value, …​}, onCreateProps:{key:value, …​}, endNode, onMatchProps:{key:value, …​}) - merge relationship with dynamic type, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Relationship(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeRelationship);
            }
        }

        public static partial class MergeNode
        {
            /// <summary>
            /// apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes eagerly, with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Eager(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeNodeEager(0));
            }
        }

        public static partial class MergeRelationship
        {
            /// <summary>
            /// apoc.merge.relationship(startNode, relType,  identProps:{key:value, …​}, onCreateProps:{key:value, …​}, endNode, onMatchProps:{key:value, …​}) - merge relationship with dynamic type, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Eager(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeRelationshipEager);
            }
        }

        public static partial class Meta
        {
            /// <summary>
            /// apoc.meta.graphSample() - examines the database statistics to build the meta graph, very fast, might report extra relationships
            /// </summary>
            public static MiscJaggedListResult Graphsample(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaGraphsample(0));
            }
            /// <summary>
            /// apoc.meta.nodeTypeProperties()
            /// </summary>
            public static MiscJaggedListResult Nodetypeproperties(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaNodetypeproperties(0));
            }
            /// <summary>
            /// apoc.meta.relTypeProperties()
            /// </summary>
            public static MiscJaggedListResult Reltypeproperties(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaReltypeproperties(0));
            }
            /// <summary>
            /// apoc.meta.subGraph({labels:[labels],rels:[rel-types], excludes:[labels,rel-types]}) - examines a sample sub graph to create the meta-graph
            /// </summary>
            public static MiscJaggedListResult Subgraph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaSubgraph);
            }
            /// <summary>
            /// apoc.meta.typeName(value) - type name of a value (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,UNKNOWN,MAP,LIST)
            /// </summary>
            public static MiscJaggedListResult Typename(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaTypename);
            }
            /// <summary>
            /// apoc.meta.data({config})  - examines a subset of the graph to provide a tabular meta information
            /// </summary>
            public static MiscJaggedListResult Data(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaData(0));
            }
            /// <summary>
            /// apoc.meta.graph - examines the full graph to create the meta-graph
            /// </summary>
            public static MiscJaggedListResult Graph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaGraph(0));
            }
            /// <summary>
            /// apoc.meta.isType(value,type) - returns a row if type name matches none if not (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,UNKNOWN,MAP,LIST)
            /// </summary>
            public static MiscJaggedListResult Istype(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaIstype);
            }
            /// <summary>
            /// apoc.meta.schema({config})  - examines a subset of the graph to provide a map-like meta information
            /// </summary>
            public static MiscJaggedListResult Schema(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaSchema(0));
            }
            /// <summary>
            /// apoc.meta.stats yield labelCount, relTypeCount, propertyKeyCount, nodeCount, relCount, labels, relTypes, stats | returns the information stored in the transactional database statistics
            /// </summary>
            public static MiscJaggedListResult Stats(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaStats);
            }
            /// <summary>
            /// apoc.meta.type(value) - type name of a value (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,UNKNOWN,MAP,LIST)
            /// </summary>
            public static MiscJaggedListResult Type(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaType);
            }
            /// <summary>
            /// apoc.meta.types(node-relationship-map)  - returns a map of keys to types
            /// </summary>
            public static MiscJaggedListResult Types(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaTypes);
            }
        }

        public static partial class MetaCypher
        {
            /// <summary>
            /// apoc.meta.cypher.isType(value,type) - returns a row if type name matches none if not (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,MAP,LIST OF &lt;TYPE&gt;,POINT,DATE,DATE_TIME,LOCAL_TIME,LOCAL_DATE_TIME,TIME,DURATION)
            /// </summary>
            public static MiscJaggedListResult Istype(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaCypherIstype);
            }
            /// <summary>
            /// apoc.meta.cypher.type(value) - type name of a value (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,MAP,LIST OF &lt;TYPE&gt;,POINT,DATE,DATE_TIME,LOCAL_TIME,LOCAL_DATE_TIME,TIME,DURATION)
            /// </summary>
            public static MiscJaggedListResult Type(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaCypherType);
            }
            /// <summary>
            /// apoc.meta.cypher.types(node-relationship-map)  - returns a map of keys to types
            /// </summary>
            public static MiscJaggedListResult Types(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaCypherTypes);
            }
        }

        public static partial class MetaData
        {
            /// <summary>
            /// apoc.meta.data.of({graph}, {config})  - examines a subset of the graph to provide a tabular meta information
            /// </summary>
            public static MiscJaggedListResult Of(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaDataOf);
            }
        }

        public static partial class MetaGraph
        {
            /// <summary>
            /// apoc.meta.graph.of({graph}, {config})  - examines a subset of the graph to provide a graph meta information
            /// </summary>
            public static MiscJaggedListResult Of(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaGraphOf(0));
            }
        }

        public static partial class MetaNodes
        {
            /// <summary>
            /// apoc.meta.nodes.count([labels], $config) - Returns the sum of the nodes with a label present in the list.
            /// </summary>
            public static MiscJaggedListResult Count(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaNodesCount(0));
            }
        }

        public static partial class Neighbors
        {
            /// <summary>
            /// apoc.neighbors.athop(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at a distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Athop(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsAthop);
            }
            /// <summary>
            /// apoc.neighbors.byhop(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at each distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Byhop(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsByhop);
            }
            /// <summary>
            /// apoc.neighbors.tohop(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern up to a certain distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Tohop(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsTohop);
            }
        }

        public static partial class NeighborsAthop
        {
            /// <summary>
            /// apoc.neighbors.athop.count(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at a distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Count(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsAthopCount);
            }
        }

        public static partial class NeighborsByhop
        {
            /// <summary>
            /// apoc.neighbors.byhop.count(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at each distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Count(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsByhopCount);
            }
        }

        public static partial class NeighborsTohop
        {
            /// <summary>
            /// apoc.neighbors.tohop.count(node, rel-direction-pattern, distance) - returns distinct count of nodes of the given relationships in the pattern up to a certain distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Count(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsTohopCount);
            }
        }

        public static partial class NlpAzureEntities
        {
            /// <summary>
            /// Creates a (virtual) entity graph for provided text
            /// </summary>
            public static MiscJaggedListResult Graph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureEntitiesGraph);
            }
            /// <summary>
            /// Provides a entity analysis for provided text
            /// </summary>
            public static MiscJaggedListResult Stream(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureEntitiesStream);
            }
        }

        public static partial class NlpAzureKeyphrases
        {
            /// <summary>
            /// Creates a (virtual) key phrase graph for provided text
            /// </summary>
            public static MiscJaggedListResult Graph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureKeyphrasesGraph);
            }
            /// <summary>
            /// Provides a entity analysis for provided text
            /// </summary>
            public static MiscJaggedListResult Stream(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureKeyphrasesStream);
            }
        }

        public static partial class NlpAzureSentiment
        {
            /// <summary>
            /// Creates a (virtual) sentiment graph for provided text
            /// </summary>
            public static MiscJaggedListResult Graph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureSentimentGraph);
            }
            /// <summary>
            /// Provides a sentiment analysis for provided text
            /// </summary>
            public static MiscJaggedListResult Stream(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureSentimentStream);
            }
        }

        public static partial class Node
        {
            /// <summary>
            /// apoc.node.degree(node, rel-direction-pattern) - returns total degrees of the given relationships in the pattern, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Degree(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeDegree);
            }
            /// <summary>
            /// returns id for (virtual) nodes
            /// </summary>
            public static MiscJaggedListResult Id(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeId);
            }
            /// <summary>
            /// returns labels for (virtual) nodes
            /// </summary>
            public static MiscJaggedListResult Labels(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeLabels);
            }
        }

        public static partial class NodeDegree
        {
            /// <summary>
            /// apoc.node.degree.in(node, relationshipName) - returns total number number of incoming relationships
            /// </summary>
            public static MiscJaggedListResult In(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeDegreeIn);
            }
            /// <summary>
            /// apoc.node.degree.out(node, relationshipName) - returns total number number of outgoing relationships
            /// </summary>
            public static MiscJaggedListResult Out(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeDegreeOut);
            }
        }

        public static partial class NodeRelationship
        {
            /// <summary>
            /// apoc.node.relationship.exists(node, rel-direction-pattern) - returns true when the node has the relationships of the pattern
            /// </summary>
            public static MiscJaggedListResult Exists(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeRelationshipExists);
            }
            /// <summary>
            /// apoc.node.relationship.types(node, rel-direction-pattern) - returns a list of distinct relationship types
            /// </summary>
            public static MiscJaggedListResult Types(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeRelationshipTypes);
            }
        }

        public static partial class NodeRelationships
        {
            /// <summary>
            /// apoc.node.relationships.exist(node, rel-direction-pattern) - returns a map with rel-pattern, boolean for the given relationship patterns
            /// </summary>
            public static MiscJaggedListResult Exist(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeRelationshipsExist);
            }
        }

        public static partial class Nodes
        {
            /// <summary>
            /// apoc.nodes.collapse([nodes…​],[{properties:&apos;overwrite&apos; or &apos;discard&apos; or &apos;combine&apos;}]) yield from, rel, to merge nodes onto first in list
            /// </summary>
            public static MiscJaggedListResult Collapse(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesCollapse);
            }
            /// <summary>
            /// apoc.nodes.connected(start, end, rel-direction-pattern) - returns true when the node is connected to the other node, optimized for dense nodes
            /// </summary>
            public static MiscJaggedListResult Connected(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodesConnected);
            }
            /// <summary>
            /// CALL apoc.nodes.cycles([nodes], $config) - Detect all path cycles from node list
            /// </summary>
            public static MiscJaggedListResult Cycles(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesCycles);
            }
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult Group(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesGroup);
            }
            /// <summary>
            /// apoc.nodes.isDense(node) - returns true if it is a dense node
            /// </summary>
            public static MiscJaggedListResult Isdense(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodesIsdense);
            }
            /// <summary>
            /// apoc.nodes.link([nodes],&apos;REL_TYPE&apos;, conf) - creates a linked list of nodes from first to last
            /// </summary>
            public static MiscJaggedListResult Link(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesLink);
            }
            /// <summary>
            /// apoc.nodes.delete(node|nodes|id|[ids]) - quickly delete all nodes with these ids
            /// </summary>
            public static MiscJaggedListResult Delete(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesDelete);
            }
            /// <summary>
            /// apoc.nodes.get(node|nodes|id|[ids]) - quickly returns all nodes with these ids
            /// </summary>
            public static MiscJaggedListResult Get(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesGet);
            }
            /// <summary>
            /// apoc.get.rels(rel|id|[ids]) - quickly returns all relationships with these ids
            /// </summary>
            public static MiscJaggedListResult Rels(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesRels);
            }
        }

        public static partial class NodesRelationship
        {
            /// <summary>
            /// apoc.nodes.relationship.types(node|nodes|id|[ids], rel-direction-pattern) - returns a list of maps where each one has two fields: node which is the node subject of the analysis and types which is a list of distinct relationship types
            /// </summary>
            public static MiscJaggedListResult Types(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodesRelationshipTypes);
            }
        }

        public static partial class NodesRelationships
        {
            /// <summary>
            /// apoc.nodes.relationships.exist(node|nodes|id|[ids], rel-direction-pattern) - returns a list of maps where each one has two fields: node which is the node subject of the analysis and exists which is a map with rel-pattern, boolean for the given relationship patterns
            /// </summary>
            public static MiscJaggedListResult Exist(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNodesRelationshipsExist);
            }
        }

        public static partial class Number
        {
            /// <summary>
            /// apoc.number.arabicToRoman(number)  | convert arabic numbers to roman
            /// </summary>
            public static MiscJaggedListResult Arabictoroman(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberArabictoroman);
            }
            /// <summary>
            /// apoc.number.parseFloat(text)  | parse a text using the default system pattern and language to produce a double
            /// </summary>
            public static MiscJaggedListResult Parsefloat(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberParsefloat);
            }
            /// <summary>
            /// apoc.number.parseInt(text)  | parse a text using the default system pattern and language to produce a long
            /// </summary>
            public static MiscJaggedListResult Parseint(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberParseint);
            }
            /// <summary>
            /// apoc.number.romanToArabic(romanNumber)  | convert roman numbers to arabic
            /// </summary>
            public static MiscJaggedListResult Romantoarabic(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberRomantoarabic);
            }
            /// <summary>
            /// apoc.number.format(number)  | format a long or double using the default system pattern and language to produce a string
            /// </summary>
            public static MiscJaggedListResult Format(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberFormat);
            }
        }

        public static partial class NumberExact
        {
            /// <summary>
            /// apoc.number.exact.div(stringA,stringB,[prec],[roundingModel]) - return the division’s result of two large numbers
            /// </summary>
            public static MiscJaggedListResult Div(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactDiv);
            }
            /// <summary>
            /// apoc.number.exact.mul(stringA,stringB,[prec],[roundingModel]) - return the multiplication’s result of two large numbers
            /// </summary>
            public static MiscJaggedListResult Mul(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactMul);
            }
            /// <summary>
            /// apoc.number.exact.sub(stringA,stringB) - return the substraction’s of two large numbers
            /// </summary>
            public static MiscJaggedListResult Sub(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactSub);
            }
            /// <summary>
            /// apoc.number.exact.toExact(number) - return the exact value
            /// </summary>
            public static MiscJaggedListResult Toexact(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactToexact);
            }
            /// <summary>
            /// apoc.number.exact.add(stringA,stringB) - return the sum’s result of two large numbers
            /// </summary>
            public static MiscJaggedListResult Add(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactAdd);
            }
            /// <summary>
            /// apoc.number.exact.toFloat(string,[prec],[roundingMode]) - return the Float value of a large number
            /// </summary>
            public static MiscJaggedListResult Tofloat(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactTofloat);
            }
            /// <summary>
            /// apoc.number.exact.toInteger(string,[prec],[roundingMode]) - return the Integer value of a large number
            /// </summary>
            public static MiscJaggedListResult Tointeger(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactTointeger);
            }
        }

        public static partial class Path
        {
            /// <summary>
            /// apoc.path.combine(path1, path2) - combines the paths into one if the connecting node matches
            /// </summary>
            public static MiscJaggedListResult Combine(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocPathCombine);
            }
            /// <summary>
            /// apoc.path.create(startNode,[rels]) - creates a path instance of the given elements
            /// </summary>
            public static MiscJaggedListResult Create(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocPathCreate);
            }
            /// <summary>
            /// apoc.path.expand(startNode &lt;id&gt;|Node|list, &apos;TYPE|TYPE_OUT&gt;|&lt;TYPE_IN&apos;, &apos;+YesLabel|-NoLabel&apos;, minLevel, maxLevel ) yield path - expand from start node following the given relationships from min to max-level adhering to the label filters
            /// </summary>
            public static MiscJaggedListResult Expand(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPathExpand);
            }
            /// <summary>
            /// apoc.path.expandConfig(startNode &lt;id&gt;|Node|list, {minLevel,maxLevel,uniqueness,relationshipFilter,labelFilter,uniqueness:&apos;RELATIONSHIP_PATH&apos;,bfs:true, filterStartNode:false, limit:-1, optional:false, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield path - expand from start node following the given relationships from min to max-level adhering to the label filters.
            /// </summary>
            public static MiscJaggedListResult Expandconfig(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPathExpandconfig);
            }
            /// <summary>
            /// apoc.path.spanningTree(startNode &lt;id&gt;|Node|list, {maxLevel,relationshipFilter,labelFilter,bfs:true, filterStartNode:false, limit:-1, optional:false, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield path - expand a spanning tree reachable from start node following relationships to max-level adhering to the label filters
            /// </summary>
            public static MiscJaggedListResult Spanningtree(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPathSpanningtree);
            }
            /// <summary>
            /// apoc.path.subgraphAll(startNode &lt;id&gt;|Node|list, {maxLevel,relationshipFilter,labelFilter,bfs:true, filterStartNode:false, limit:-1, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield nodes, relationships - expand the subgraph reachable from start node following relationships to max-level adhering to the label filters, and also return all relationships within the subgraph
            /// </summary>
            public static MiscJaggedListResult Subgraphall(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPathSubgraphall);
            }
            /// <summary>
            /// apoc.path.subgraphNodes(startNode &lt;id&gt;|Node|list, {maxLevel,relationshipFilter,labelFilter,bfs:true, filterStartNode:false, limit:-1, optional:false, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield node - expand the subgraph nodes reachable from start node following relationships to max-level adhering to the label filters
            /// </summary>
            public static MiscJaggedListResult Subgraphnodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPathSubgraphnodes);
            }
            /// <summary>
            /// apoc.path.elements(path) - returns a list of node-relationship-node-…​
            /// </summary>
            public static MiscJaggedListResult Elements(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocPathElements);
            }
            /// <summary>
            /// apoc.path.slice(path, [offset], [length]) - creates a sub-path with the given offset and length
            /// </summary>
            public static MiscJaggedListResult Slice(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocPathSlice);
            }
        }

        public static partial class Periodic
        {
            /// <summary>
            /// apoc.periodic.cancel(name) - cancel job with the given name
            /// </summary>
            public static MiscJaggedListResult Cancel(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicCancel);
            }
            /// <summary>
            /// apoc.periodic.commit(statement,params) - runs the given statement in separate transactions until it returns 0
            /// </summary>
            public static MiscJaggedListResult Commit(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicCommit);
            }
            /// <summary>
            /// apoc.periodic.countdown(&apos;name&apos;,statement,repeat-rate-in-seconds) creates a background job that will repeatedly execute the given Cypher statement until it returns 0.
            /// </summary>
            public static MiscJaggedListResult Countdown(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicCountdown);
            }
            /// <summary>
            /// apoc.periodic.iterate(&apos;statement returning items&apos;, &apos;statement per item&apos;, {batchSize:1000,iterateList:true,parallel:false,params:{},concurrency:50,retries:0}) YIELD batches, total - run the second statement for each item returned by the first statement. Returns number of batches and total processed rows
            /// </summary>
            public static MiscJaggedListResult Iterate(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicIterate);
            }
            /// <summary>
            /// apoc.periodic.repeat(&apos;name&apos;,statement,repeat-rate-in-seconds, config) submit a repeatedly-called background query. The parameter &apos;config&apos; is optional and can contain a &apos;params&apos; entry usable in nested Cypher statement.
            /// </summary>
            public static MiscJaggedListResult Repeat(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicRepeat);
            }
            /// <summary>
            /// apoc.periodic.submit(&apos;name&apos;,statement,params) - creates a background job which executes a Cypher statement once. The parameter &apos;params&apos; is optional and can contain query parameters for the Cypher statement
            /// </summary>
            public static MiscJaggedListResult Submit(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicSubmit);
            }
            /// <summary>
            /// apoc.periodic.truncate({config}) - removes all entities (and optionally indexes and constraints) from db using the apoc.periodic.iterate under the hood
            /// </summary>
            public static MiscJaggedListResult Truncate(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicTruncate(0));
            }
            /// <summary>
            /// apoc.periodic.list - list all jobs
            /// </summary>
            public static MiscJaggedListResult List(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicList);
            }
        }

        public static partial class Refactor
        {
            /// <summary>
            /// apoc.refactor.categorize(sourceKey, type, outgoing, label, targetKey, copiedKeys, batchSize) turn each unique propertyKey into a category node and connect to it
            /// </summary>
            public static MiscJaggedListResult Categorize(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorCategorize);
            }
            /// <summary>
            /// apoc.refactor.cloneNodes([node1,node2,…​]) clone nodes with their labels and properties
            /// </summary>
            public static MiscJaggedListResult Clonenodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonenodes(0));
            }
            /// <summary>
            /// apoc.refactor.cloneNodesWithRelationships([node1,node2,…​]) clone nodes with their labels, properties and relationships
            /// </summary>
            public static MiscJaggedListResult Clonenodeswithrelationships(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonenodeswithrelationships);
            }
            /// <summary>
            /// apoc.refactor.cloneSubgraph([node1,node2,…​], [rel1,rel2,…​]:[], {standinNodes:[], skipProperties:[]}) YIELD input, output, error | clone nodes with their labels and properties (optionally skipping any properties in the skipProperties list via the config map), and clone the given relationships (will exist between cloned nodes only). If no relationships are provided, all relationships between the given nodes will be cloned. Relationships can be optionally redirected according to standinNodes node pairings (this is a list of list-pairs of nodes), so given a node in the original subgraph (first of the pair), an existing node (second of the pair) can act as a standin for it within the cloned subgraph. Cloned relationships will be redirected to the standin.
            /// </summary>
            public static MiscJaggedListResult Clonesubgraph(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonesubgraph(0));
            }
            /// <summary>
            /// apoc.refactor.cloneSubgraphFromPaths([path1, path2, …​], {standinNodes:[], skipProperties:[]}) YIELD input, output, error | from the subgraph formed from the given paths, clone nodes with their labels and properties (optionally skipping any properties in the skipProperties list via the config map), and clone the relationships (will exist between cloned nodes only). Relationships can be optionally redirected according to standinNodes node pairings (this is a list of list-pairs of nodes), so given a node in the original subgraph (first of the pair), an existing node (second of the pair) can act as a standin for it within the cloned subgraph. Cloned relationships will be redirected to the standin.
            /// </summary>
            public static MiscJaggedListResult Clonesubgraphfrompaths(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonesubgraphfrompaths);
            }
            /// <summary>
            /// apoc.refactor.collapseNode([node1,node2],&apos;TYPE&apos;) collapse node to relationship, node with one rel becomes self-relationship
            /// </summary>
            public static MiscJaggedListResult Collapsenode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorCollapsenode);
            }
            /// <summary>
            /// apoc.refactor.deleteAndReconnect([pathLinkedList], [nodesToRemove], {config}) - Removes some nodes from a linked list
            /// </summary>
            public static MiscJaggedListResult Deleteandreconnect(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorDeleteandreconnect);
            }
            /// <summary>
            /// apoc.refactor.extractNode([rel1,rel2,…​], [labels],&apos;OUT&apos;,&apos;IN&apos;) extract node from relationships
            /// </summary>
            public static MiscJaggedListResult Extractnode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorExtractnode);
            }
            /// <summary>
            /// apoc.refactor.invert(rel) inverts relationship direction
            /// </summary>
            public static MiscJaggedListResult Invert(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorInvert);
            }
            /// <summary>
            /// apoc.refactor.mergeNodes([node1,node2],[{properties:&apos;overwrite&apos; or &apos;discard&apos; or &apos;combine&apos;}]) merge nodes onto first in list
            /// </summary>
            public static MiscJaggedListResult Mergenodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorMergenodes);
            }
            /// <summary>
            /// apoc.refactor.mergeRelationships([rel1,rel2]) merge relationships onto first in list
            /// </summary>
            public static MiscJaggedListResult Mergerelationships(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorMergerelationships);
            }
            /// <summary>
            /// apoc.refactor.normalizeAsBoolean(entity, propertyKey, true_values, false_values) normalize/convert a property to be boolean
            /// </summary>
            public static MiscJaggedListResult Normalizeasboolean(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorNormalizeasboolean);
            }
            /// <summary>
            /// apoc.refactor.setType(rel, &apos;NEW-TYPE&apos;) change relationship-type
            /// </summary>
            public static MiscJaggedListResult Settype(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorSettype);
            }
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult To(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorTo);
            }
            /// <summary>
            /// apoc.refactor.from(rel, startNode) redirect relationship to use new start-node
            /// </summary>
            public static MiscJaggedListResult From(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorFrom);
            }
        }

        public static partial class RefactorRename
        {
            /// <summary>
            /// apoc.refactor.rename.label(oldLabel, newLabel, [nodes]) | rename a label from &apos;oldLabel&apos; to &apos;newLabel&apos; for all nodes. If &apos;nodes&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscJaggedListResult Label(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorRenameLabel);
            }
            /// <summary>
            /// apoc.refactor.rename.nodeProperty(oldName, newName, [nodes], {config}) | rename all node’s property from &apos;oldName&apos; to &apos;newName&apos;. If &apos;nodes&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscJaggedListResult Nodeproperty(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorRenameNodeproperty);
            }
            /// <summary>
            /// apoc.refactor.rename.typeProperty(oldName, newName, [rels], {config}) | rename all relationship’s property from &apos;oldName&apos; to &apos;newName&apos;. If &apos;rels&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscJaggedListResult Typeproperty(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorRenameTypeproperty(0));
            }
            /// <summary>
            /// apoc.refactor.rename.type(oldType, newType, [rels], {config}) | rename all relationships with type &apos;oldType&apos; to &apos;newType&apos;. If &apos;rels&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscJaggedListResult Type(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorRenameType(0));
            }
        }

        public static partial class Rel
        {
            /// <summary>
            /// returns endNode for (virtual) relationships
            /// </summary>
            public static MiscJaggedListResult Endnode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocRelEndnode);
            }
            /// <summary>
            /// returns startNode for (virtual) relationships
            /// </summary>
            public static MiscJaggedListResult Startnode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocRelStartnode);
            }
            /// <summary>
            /// returns id for (virtual) relationships
            /// </summary>
            public static MiscJaggedListResult Id(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocRelId);
            }
            /// <summary>
            /// returns type for (virtual) relationships
            /// </summary>
            public static MiscJaggedListResult Type(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocRelType);
            }
        }

        public static partial class Schema
        {
            /// <summary>
            /// apoc.schema.assert({indexLabel:, …​}, {constraintLabel:[constraintKeys], …​}, dropExisting : true) yield label, key, keys, unique, action - drops all other existing indexes and constraints when dropExisting is true (default is true), and asserts that at the end of the operation the given indexes and unique constraints are there, each label:key pair is considered one constraint/label. Non-constraint indexes can define compound indexes with label:[key1,key2…​] pairings.
            /// </summary>
            public static MiscJaggedListResult Assert(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaAssert);
            }
            /// <summary>
            /// CALL apoc.schema.relationships([config]) yield name, startLabel, type, endLabel, properties, status
            /// </summary>
            public static MiscJaggedListResult Relationships(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaRelationships(0));
            }
            /// <summary>
            /// CALL apoc.schema.nodes([config]) yield name, label, properties, status, type
            /// </summary>
            public static MiscJaggedListResult Nodes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaNodes(0));
            }
        }

        public static partial class SchemaNode
        {
            /// <summary>
            /// RETURN apoc.schema.node.constraintExists(labelName, propertyNames)
            /// </summary>
            public static MiscJaggedListResult Constraintexists(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocSchemaNodeConstraintexists);
            }
            /// <summary>
            /// RETURN apoc.schema.node.indexExists(labelName, propertyNames)
            /// </summary>
            public static MiscJaggedListResult Indexexists(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocSchemaNodeIndexexists);
            }
        }

        public static partial class SchemaProperties
        {
            /// <summary>
            /// apoc.schema.properties.distinct(label, key) - quickly returns all distinct values for a given key
            /// </summary>
            public static MiscJaggedListResult Distinct(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaPropertiesDistinct);
            }
            /// <summary>
            /// apoc.schema.properties.distinctCount([label], [key]) YIELD label, key, value, count - quickly returns all distinct values and counts for a given key
            /// </summary>
            public static MiscJaggedListResult Distinctcount(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaPropertiesDistinctcount);
            }
        }

        public static partial class SchemaRelationship
        {
            /// <summary>
            /// RETURN apoc.schema.relationship.constraintExists(type, propertyNames)
            /// </summary>
            public static MiscJaggedListResult Constraintexists(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocSchemaRelationshipConstraintexists);
            }
            /// <summary>
            /// RETURN apoc.schema.relationship.indexExists(relName, propertyNames)
            /// </summary>
            public static MiscJaggedListResult Indexexists(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocSchemaRelationshipIndexexists);
            }
        }

        public static partial class Scoring
        {
            /// <summary>
            /// apoc.scoring.existence(5, true) returns the provided score if true, 0 if false
            /// </summary>
            public static MiscJaggedListResult Existence(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocScoringExistence);
            }
            /// <summary>
            /// apoc.scoring.pareto(10, 20, 100, 11) applies a Pareto scoring function over the inputs
            /// </summary>
            public static MiscJaggedListResult Pareto(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocScoringPareto);
            }
        }

        public static partial class Search
        {
            /// <summary>
            /// Do a parallel search over multiple indexes returning a reduced representation of the nodes found: node id, labels and the searched properties. apoc.search.multiSearchReduced( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ). Multiple search results for the same node are merged into one record.
            /// </summary>
            public static MiscJaggedListResult Multisearchreduced(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSearchMultisearchreduced);
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning nodes. usage apoc.search.nodeAll( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ) returns all the Nodes found in the different searches.
            /// </summary>
            public static MiscJaggedListResult Nodeall(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSearchNodeall);
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning a reduced representation of the nodes found: node id, labels and the searched property. apoc.search.nodeShortAll( map of label and properties which will be searched upon, operator: EXACT / CONTAINS / STARTS WITH | ENDS WITH / = / &lt;&gt; / &lt; / &gt; …​, value ). All &apos;hits&apos; are returned.
            /// </summary>
            public static MiscJaggedListResult Nodeallreduced(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSearchNodeallreduced);
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning a reduced representation of the nodes found: node id, labels and the searched properties. apoc.search.nodeReduced( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ). Multiple search results for the same node are merged into one record.
            /// </summary>
            public static MiscJaggedListResult Nodereduced(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSearchNodereduced);
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning nodes. usage apoc.search.node( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ) returns all the DISTINCT Nodes found in the different searches.
            /// </summary>
            public static MiscJaggedListResult Node(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSearchNode);
            }
        }

        public static partial class Spatial
        {
            /// <summary>
            /// apoc.spatial.geocode(&apos;address&apos;, maxResults, quotaException, $config) YIELD location, latitude, longitude, description, osmData - look up geographic location of address from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscJaggedListResult Geocode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialGeocode(0));
            }
            /// <summary>
            /// apoc.spatial.geocodeOnce(&apos;address&apos;, $config) YIELD location, latitude, longitude, description, osmData - look up geographic location of address from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscJaggedListResult Geocodeonce(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialGeocodeonce);
            }
            /// <summary>
            /// apoc.spatial.reverseGeocode(latitude,longitude, quotaException, $config) YIELD location, latitude, longitude, description - look up address from latitude and longitude from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscJaggedListResult Reversegeocode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialReversegeocode(0));
            }
            /// <summary>
            /// apoc.spatial.sortByDistance(List&lt;Path&gt;) sort the given paths based on the geo informations (lat/long) in ascending order
            /// </summary>
            public static MiscJaggedListResult Sortbydistance(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialSortbydistance);
            }
        }

        public static partial class Stats
        {
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult Degrees(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocStatsDegrees);
            }
        }

        public static partial class SystemdbExport
        {
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult Metadata(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocSystemdbExportMetadata(0));
            }
        }

        public static partial class Temporal
        {
            /// <summary>
            /// apoc.temporal.formatDuration(input, format) | Format a Duration
            /// </summary>
            public static MiscJaggedListResult Formatduration(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTemporalFormatduration);
            }
            /// <summary>
            /// apoc.temporal.toZonedTemporal(&apos;2012-12-23 23:59:59&apos;,&apos;yyyy-MM-dd HH:mm:ss&apos;, &apos;UTC-hour-offset&apos;) parse date string using the specified format to specified timezone
            /// </summary>
            public static MiscJaggedListResult Tozonedtemporal(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTemporalTozonedtemporal(0));
            }
            /// <summary>
            /// apoc.temporal.format(input, format) | Format a temporal value
            /// </summary>
            public static MiscJaggedListResult Format(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTemporalFormat);
            }
        }

        public static partial class Text
        {
            /// <summary>
            /// apoc.text.base64Decode(text) YIELD value - Decode Base64 encoded string
            /// </summary>
            public static MiscJaggedListResult Base64decode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBase64decode);
            }
            /// <summary>
            /// apoc.text.base64Encode(text) YIELD value - Encode a string with Base64
            /// </summary>
            public static MiscJaggedListResult Base64encode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBase64encode);
            }
            /// <summary>
            /// apoc.text.base64UrlDecode(url) YIELD value - Decode Base64 encoded url
            /// </summary>
            public static MiscJaggedListResult Base64urldecode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBase64urldecode);
            }
            /// <summary>
            /// apoc.text.base64UrlEncode(text) YIELD value - Encode a url with Base64
            /// </summary>
            public static MiscJaggedListResult Base64urlencode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBase64urlencode);
            }
            /// <summary>
            /// apoc.text.byteCount(text,[charset]) - return size of text in bytes
            /// </summary>
            public static MiscJaggedListResult Bytecount(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBytecount);
            }
            /// <summary>
            /// apoc.text.bytes(text,[charset]) - return bytes of the text
            /// </summary>
            public static MiscJaggedListResult Bytes(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBytes);
            }
            /// <summary>
            /// apoc.text.camelCase(text) YIELD value - Convert a string to camelCase
            /// </summary>
            public static MiscJaggedListResult Camelcase(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextCamelcase);
            }
            /// <summary>
            /// apoc.text.capitalize(text) YIELD value - capitalise the first letter of the word
            /// </summary>
            public static MiscJaggedListResult Capitalize(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextCapitalize);
            }
            /// <summary>
            /// apoc.text.capitalizeAll(text) YIELD value - capitalise the first letter of every word in the text
            /// </summary>
            public static MiscJaggedListResult Capitalizeall(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextCapitalizeall);
            }
            /// <summary>
            /// apoc.text.charAt(text, index) - the decimal value of the character at the given index
            /// </summary>
            public static MiscJaggedListResult Charat(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextCharat);
            }
            /// <summary>
            /// apoc.text.code(codepoint) - Returns the unicode character of the given codepoint
            /// </summary>
            public static MiscJaggedListResult Code(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextCode);
            }
            /// <summary>
            /// apoc.text.compareCleaned(text1, text2) - compare the given strings stripped of everything except alpha numeric characters converted to lower case.
            /// </summary>
            public static MiscJaggedListResult Comparecleaned(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextComparecleaned);
            }
            /// <summary>
            /// apoc.text.decapitalize(text) YIELD value - decapitalize the first letter of the word
            /// </summary>
            public static MiscJaggedListResult Decapitalize(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextDecapitalize);
            }
            /// <summary>
            /// apoc.text.decapitalizeAll(text) YIELD value - decapitalize the first letter of all words
            /// </summary>
            public static MiscJaggedListResult Decapitalizeall(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextDecapitalizeall);
            }
            /// <summary>
            /// apoc.text.distance(text1, text2) - compare the given strings with the Levenshtein distance algorithm.
            /// </summary>
            public static MiscJaggedListResult Distance(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextDistance);
            }
            /// <summary>
            /// apoc.text.doubleMetaphone(value) yield value - Compute the Double Metaphone phonetic encoding of all words of the text value which can be a single string or a list of strings
            /// </summary>
            public static MiscJaggedListResult Doublemetaphone(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocTextDoublemetaphone);
            }
            /// <summary>
            /// apoc.text.fuzzyMatch(text1, text2) - check if 2 words can be matched in a fuzzy way. Depending on the length of the String it will allow more characters that needs to be edited to match the second String.
            /// </summary>
            public static MiscJaggedListResult Fuzzymatch(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextFuzzymatch);
            }
            /// <summary>
            /// apoc.text.hammingDistance(text1, text2) - compare the given strings with the Hamming distance algorithm.
            /// </summary>
            public static MiscJaggedListResult Hammingdistance(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextHammingdistance);
            }
            /// <summary>
            /// apoc.text.hexCharAt(text, index) - the hex value string of the character at the given index
            /// </summary>
            public static MiscJaggedListResult Hexcharat(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextHexcharat);
            }
            /// <summary>
            /// apoc.text.hexValue(value) - the hex value string of the given number
            /// </summary>
            public static MiscJaggedListResult Hexvalue(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextHexvalue);
            }
            /// <summary>
            /// apoc.text.indexesOf(text, lookup, from=0, to=-1==len) - finds all occurences of the lookup string in the text, return list, from inclusive, to exclusive, empty list if not found, null if text is null.
            /// </summary>
            public static MiscJaggedListResult Indexesof(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextIndexesof(0));
            }
            /// <summary>
            /// apoc.text.jaroWinklerDistance(text1, text2) - compare the given strings with the Jaro-Winkler distance algorithm.
            /// </summary>
            public static MiscJaggedListResult Jarowinklerdistance(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextJarowinklerdistance);
            }
            /// <summary>
            /// apoc.text.join([&apos;text1&apos;,&apos;text2&apos;,…​], delimiter) - join the given strings with the given delimiter.
            /// </summary>
            public static MiscJaggedListResult Join(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextJoin);
            }
            /// <summary>
            /// apoc.text.levenshteinDistance(text1, text2) - compare the given strings with the Levenshtein distance algorithm.
            /// </summary>
            public static MiscJaggedListResult Levenshteindistance(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextLevenshteindistance);
            }
            /// <summary>
            /// apoc.text.levenshteinSimilarity(text1, text2) - calculate the similarity (a value within 0 and 1) between two texts.
            /// </summary>
            public static MiscJaggedListResult Levenshteinsimilarity(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextLevenshteinsimilarity);
            }
            /// <summary>
            /// apoc.text.lpad(text,count,delim) YIELD value - left pad the string to the given width
            /// </summary>
            public static MiscJaggedListResult Lpad(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextLpad);
            }
            /// <summary>
            /// apoc.text.phonetic(value) yield value - Compute the US_ENGLISH phonetic soundex encoding of all words of the text value which can be a single string or a list of strings
            /// </summary>
            public static MiscJaggedListResult Phonetic(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocTextPhonetic);
            }
            /// <summary>
            /// apoc.text.phoneticDelta(text1, text2) yield phonetic1, phonetic2, delta - Compute the US_ENGLISH soundex character difference between two given strings
            /// </summary>
            public static MiscJaggedListResult Phoneticdelta(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocTextPhoneticdelta);
            }
            /// <summary>
            /// apoc.text.random(length, valid) YIELD value - generate a random string
            /// </summary>
            public static MiscJaggedListResult Random(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRandom(0));
            }
            /// <summary>
            /// apoc.text.regexGroups(text, regex) - return all matching groups of the regex on the given text.
            /// </summary>
            public static MiscJaggedListResult Regexgroups(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRegexgroups);
            }
            /// <summary>
            /// apoc.text.regreplace(text, regex, replacement) - replace each substring of the given string that matches the given regular expression with the given replacement.
            /// </summary>
            public static MiscJaggedListResult Regreplace(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRegreplace);
            }
            /// <summary>
            /// apoc.text.rpad(text,count,delim) YIELD value - right pad the string to the given width
            /// </summary>
            public static MiscJaggedListResult Rpad(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRpad);
            }
            /// <summary>
            /// apoc.text.slug(text, delim) - slug the text with the given delimiter
            /// </summary>
            public static MiscJaggedListResult Slug(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextSlug);
            }
            /// <summary>
            /// apoc.text.snakeCase(text) YIELD value - Convert a string to snake-case
            /// </summary>
            public static MiscJaggedListResult Snakecase(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextSnakecase);
            }
            /// <summary>
            /// apoc.text.sorensenDiceSimilarityWithLanguage(text1, text2, languageTag) - compare the given strings with the Sørensen–Dice coefficient formula, with the provided IETF language tag
            /// </summary>
            public static MiscJaggedListResult Sorensendicesimilarity(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextSorensendicesimilarity);
            }
            /// <summary>
            /// apoc.text.swapCase(text) YIELD value - Swap the case of a string
            /// </summary>
            public static MiscJaggedListResult Swapcase(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextSwapcase);
            }
            /// <summary>
            /// apoc.text.toCypher(value, {skipKeys,keepKeys,skipValues,keepValues,skipNull,node,relationship,start,end}) | tries it’s best to convert the value to a cypher-property-string
            /// </summary>
            public static MiscJaggedListResult Tocypher(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextTocypher);
            }
            /// <summary>
            /// apoc.text.toUpperCase(text) YIELD value - Convert a string to UPPER_CASE
            /// </summary>
            public static MiscJaggedListResult Touppercase(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextTouppercase);
            }
            /// <summary>
            /// apoc.text.upperCamelCase(text) YIELD value - Convert a string to camelCase
            /// </summary>
            public static MiscJaggedListResult Uppercamelcase(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextUppercamelcase);
            }
            /// <summary>
            /// apoc.text.urldecode(text) - return the urldecoded text
            /// </summary>
            public static MiscJaggedListResult Urldecode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextUrldecode);
            }
            /// <summary>
            /// apoc.text.urlencode(text) - return the urlencoded text
            /// </summary>
            public static MiscJaggedListResult Urlencode(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextUrlencode);
            }
            /// <summary>
            /// apoc.text.clean(text) - strip the given string of everything except alpha numeric characters and convert it to lower case.
            /// </summary>
            public static MiscJaggedListResult Clean(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextClean);
            }
            /// <summary>
            /// apoc.text.format(text,[params],language) - sprintf format the string with the params given
            /// </summary>
            public static MiscJaggedListResult Format(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextFormat);
            }
            /// <summary>
            /// apoc.text.indexOf(text, lookup, from=0, to=-1==len) - find the first occurence of the lookup string in the text, from inclusive, to exclusive, -1 if not found, null if text is null.
            /// </summary>
            public static MiscJaggedListResult Indexof(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextIndexof(0));
            }
            /// <summary>
            /// apoc.text.repeat(item, count) - string multiplication
            /// </summary>
            public static MiscJaggedListResult Repeat(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRepeat);
            }
            /// <summary>
            /// apoc.text.replace(text, regex, replacement) - replace each substring of the given string that matches the given regular expression with the given replacement.
            /// </summary>
            public static MiscJaggedListResult Replace(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextReplace);
            }
            /// <summary>
            /// apoc.text.split(text, regex, limit) - splits the given text around matches of the given regex.
            /// </summary>
            public static MiscJaggedListResult Split(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocTextSplit);
            }
        }

        public static partial class Trigger
        {
            /// <summary>
            /// CALL apoc.trigger.pause(name) | it pauses the trigger
            /// </summary>
            public static MiscJaggedListResult Pause(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerPause);
            }
            /// <summary>
            /// CALL apoc.trigger.resume(name) | it resumes the paused trigger
            /// </summary>
            public static MiscJaggedListResult Resume(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerResume);
            }
            /// <summary>
            /// add a trigger kernelTransaction under a name, in the kernelTransaction you can use {createdNodes}, {deletedNodes} etc., the selector is {phase:&apos;before/after/rollback&apos;} returns previous and new trigger information. Takes in an optional configuration.
            /// </summary>
            public static MiscJaggedListResult Add(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerAdd);
            }
            /// <summary>
            /// list all installed triggers
            /// </summary>
            public static MiscJaggedListResult List(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerList);
            }
            /// <summary>
            /// remove previously added trigger, returns trigger information
            /// </summary>
            public static MiscJaggedListResult Remove(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerRemove);
            }
            /// <summary>
            /// removes all previously added trigger, returns trigger information
            /// </summary>
            public static MiscJaggedListResult Removeall(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerRemoveall);
            }
        }

        public static partial class Util
        {
            /// <summary>
            /// apoc.util.compress(string, {config}) | return a compressed byte[] in various format from a string
            /// </summary>
            public static MiscJaggedListResult Compress(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilCompress);
            }
            /// <summary>
            /// apoc.util.decompress(compressed, {config}) | return a string from a compressed byte[] in various format
            /// </summary>
            public static MiscJaggedListResult Decompress(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilDecompress);
            }
            /// <summary>
            /// apoc.util.md5([values]) | computes the md5 of the concatenation of all string values of the list. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static MiscJaggedListResult Md5(params Result[] results)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilMd5(results.Length), results);
            }
            /// <summary>
            /// apoc.util.sha1([values]) | computes the sha1 of the concatenation of all string values of the list
            /// </summary>
            public static MiscJaggedListResult Sha1(params Result[] results)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilSha1(results.Length), results);
            }
            /// <summary>
            /// apoc.util.sha256([values]) | computes the sha256 of the concatenation of all string values of the list
            /// </summary>
            public static MiscJaggedListResult Sha256(params Result[] results)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilSha256(results.Length), results);
            }
            /// <summary>
            /// apoc.util.sha384([values]) | computes the sha384 of the concatenation of all string values of the list
            /// </summary>
            public static MiscJaggedListResult Sha384(params Result[] results)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilSha384(results.Length), results);
            }
            /// <summary>
            /// apoc.util.sha512([values]) | computes the sha512 of the concatenation of all string values of the list
            /// </summary>
            public static MiscJaggedListResult Sha512(params Result[] results)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilSha512(results.Length), results);
            }
            /// <summary>
            /// apoc.util.sleep(&lt;duration&gt;) | sleeps for &lt;duration&gt; millis, transaction termination is honored
            /// </summary>
            public static MiscJaggedListResult Sleep(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocUtilSleep);
            }
            /// <summary>
            /// apoc.util.validatePredicate(predicate, message, params) | if the predicate yields to true raise an exception else returns true, for use inside WHERE subclauses
            /// </summary>
            public static MiscJaggedListResult Validatepredicate(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilValidatepredicate);
            }
            /// <summary>
            /// apoc.util.validate(predicate, message, params) | if the predicate yields to true raise an exception
            /// </summary>
            public static MiscJaggedListResult Validate(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocUtilValidate);
            }
        }

        public static partial class Warmup
        {
            /// <summary>
            /// apoc.warmup.run(loadProperties=false,loadDynamicProperties=false,loadIndexes=false) - quickly loads all nodes and rels into memory by skipping one page at a time
            /// </summary>
            public static MiscJaggedListResult Run(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocWarmupRun(0));
            }
        }

        public static partial class Xml
        {
            /// <summary>
            /// Deprecated by apoc.import.xml
            /// </summary>
            public static MiscJaggedListResult Import(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.CallApocXmlImport);
            }
            /// <summary>
            /// RETURN apoc.xml.parse(&lt;xml string&gt;, &lt;xPath string&gt;, config, false) AS value
            /// </summary>
            public static MiscJaggedListResult Parse(MiscListResult list)
            {
                return new MiscJaggedListResult(t => t.FnApocXmlParse(0));
            }
        }
    }
}
