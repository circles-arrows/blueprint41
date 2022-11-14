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
        public static MiscJaggedListResult Case(MiscListResult @conditionals, StringResult @elseQuery)
        {
            return new MiscJaggedListResult(t => t.CallApocCase(2), new object[] { @conditionals, @elseQuery });
        }
        /// <summary>
        /// apoc.case([condition, query, condition, query, …​], elseQuery:&apos;&apos;, params:{}) yield value - given a list of conditional / read-only query pairs, executes the query associated with the first conditional evaluating to true (or the else query if none are true) with the given parameters
        /// </summary>
        public static MiscJaggedListResult Case(MiscListResult @conditionals, StringResult @elseQuery, MiscResult @params)
        {
            return new MiscJaggedListResult(t => t.CallApocCase(3), new object[] { @conditionals, @elseQuery, @params });
        }
        /// <summary>
        /// Provides descriptions of available procedures. To narrow the results, supply a search string. To also search in the description text, append + to the end of the search string.
        /// </summary>
        public static MiscJaggedListResult Help(StringResult @proc)
        {
            return new MiscJaggedListResult(t => t.CallApocHelp, new object[] { @proc });
        }
        /// <summary>
        /// RETURN apoc.version() | return the current APOC installed version
        /// </summary>
        public static MiscJaggedListResult Version()
        {
            return new MiscJaggedListResult(t => t.FnApocVersion);
        }
        /// <summary>
        /// apoc.when(condition, ifQuery, elseQuery:&apos;&apos;, params:{}) yield value - based on the conditional, executes read-only ifQuery or elseQuery with the given parameters
        /// </summary>
        public static MiscJaggedListResult When(BooleanResult @condition, StringResult @elseQuery, StringResult @ifQuery)
        {
            return new MiscJaggedListResult(t => t.CallApocWhen(3), new object[] { @condition, @elseQuery, @ifQuery });
        }
        /// <summary>
        /// apoc.when(condition, ifQuery, elseQuery:&apos;&apos;, params:{}) yield value - based on the conditional, executes read-only ifQuery or elseQuery with the given parameters
        /// </summary>
        public static MiscJaggedListResult When(BooleanResult @condition, StringResult @elseQuery, StringResult @ifQuery, MiscResult @params)
        {
            return new MiscJaggedListResult(t => t.CallApocWhen(4), new object[] { @condition, @elseQuery, @ifQuery, @params });
        }

        public static partial class Agg
        {
            /// <summary>
            /// apoc.agg.first(value) - returns first value
            /// </summary>
            public static MiscJaggedListResult First(MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocAggFirst, new object[] { @value });
            }
            /// <summary>
            /// apoc.agg.graph(path) - returns map of graph {nodes, relationships} of all distinct nodes and relationships
            /// </summary>
            public static MiscJaggedListResult Graph(MiscResult @element)
            {
                return new MiscJaggedListResult(t => t.FnApocAggGraph, new object[] { @element });
            }
            /// <summary>
            /// apoc.agg.last(value) - returns last value
            /// </summary>
            public static MiscJaggedListResult Last(MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocAggLast, new object[] { @value });
            }
            /// <summary>
            /// apoc.agg.maxItems(item, value, groupLimit: -1) - returns a map {items:[], value:n} where value is the maximum value present, and items are all items with the same value. The number of items can be optionally limited.
            /// </summary>
            public static MiscJaggedListResult Maxitems(NumericResult @groupLimit, MiscResult @item, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocAggMaxitems, new object[] { @groupLimit, @item, @value });
            }
            /// <summary>
            /// apoc.agg.median(number) - returns median for non-null numeric values
            /// </summary>
            public static MiscJaggedListResult Median(MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocAggMedian, new object[] { @value });
            }
            /// <summary>
            /// apoc.agg.minItems(item, value, groupLimit: -1) - returns a map {items:[], value:n} where value is the minimum value present, and items are all items with the same value. The number of items can be optionally limited.
            /// </summary>
            public static MiscJaggedListResult Minitems(NumericResult @groupLimit, MiscResult @item, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocAggMinitems, new object[] { @groupLimit, @item, @value });
            }
            /// <summary>
            /// apoc.agg.nth(value,offset) - returns value of nth row (or -1 for last)
            /// </summary>
            public static MiscJaggedListResult Nth(MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocAggNth, new object[] { @value });
            }
            /// <summary>
            /// apoc.agg.percentiles(value,[percentiles = 0.5,0.75,0.9,0.95,0.99]) - returns given percentiles for values
            /// </summary>
            public static MiscJaggedListResult Percentiles(FloatListResult @percentiles, FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocAggPercentiles, new object[] { @percentiles, @value });
            }
            /// <summary>
            /// apoc.agg.product(number) - returns given product for non-null values
            /// </summary>
            public static MiscJaggedListResult Product(FloatResult @number)
            {
                return new MiscJaggedListResult(t => t.FnApocAggProduct, new object[] { @number });
            }
            /// <summary>
            /// apoc.agg.slice(value, start, length) - returns subset of non-null values, start is 0 based and length can be -1
            /// </summary>
            public static MiscJaggedListResult Slice(NumericResult @from, NumericResult @to, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocAggSlice, new object[] { @from, @to, @value });
            }
            /// <summary>
            /// apoc.agg.statistics(value,[percentiles = 0.5,0.75,0.9,0.95,0.99]) - returns numeric statistics (percentiles, min,minNonZero,max,total,mean,stdev) for values
            /// </summary>
            public static MiscJaggedListResult Statistics(FloatListResult @percentiles, FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocAggStatistics, new object[] { @percentiles, @value });
            }
        }

        public static partial class Algo
        {
            /// <summary>
            /// apoc.algo.allSimplePaths(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, 5) YIELD path, weight - run allSimplePaths with relationships given and maxNodes
            /// </summary>
            public static MiscJaggedListResult Allsimplepaths(AliasResult @endNode, NumericResult @maxNodes, StringResult @relationshipTypesAndDirections, AliasResult @startNode)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoAllsimplepaths, new object[] { @endNode, @maxNodes, @relationshipTypesAndDirections, @startNode });
            }
            /// <summary>
            /// apoc.algo.aStar(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, &apos;distance&apos;,&apos;lat&apos;,&apos;lon&apos;) YIELD path, weight - run A* with relationship property name as cost function
            /// </summary>
            public static MiscJaggedListResult Astar(AliasResult @endNode, StringResult @latPropertyName, StringResult @lonPropertyName, StringResult @relationshipTypesAndDirections, AliasResult @startNode, StringResult @weightPropertyName)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoAstar, new object[] { @endNode, @latPropertyName, @lonPropertyName, @relationshipTypesAndDirections, @startNode, @weightPropertyName });
            }
            /// <summary>
            /// apoc.algo.aStar(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, {weight:&apos;dist&apos;,default:10,x:&apos;lon&apos;,y:&apos;lat&apos;}) YIELD path, weight - run A* with relationship property name as cost function
            /// </summary>
            public static MiscJaggedListResult Astarconfig(MiscResult @config, AliasResult @endNode, StringResult @relationshipTypesAndDirections, AliasResult @startNode)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoAstarconfig, new object[] { @config, @endNode, @relationshipTypesAndDirections, @startNode });
            }
            /// <summary>
            /// apoc.algo.cover(nodes) yield rel - returns all relationships between this set of nodes
            /// </summary>
            public static MiscJaggedListResult Cover(MiscResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoCover, new object[] { @nodes });
            }
            /// <summary>
            /// apoc.algo.dijkstra(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, &apos;distance&apos;, defaultValue, numberOfWantedResults) YIELD path, weight - run dijkstra with relationship property name as cost function
            /// </summary>
            public static MiscJaggedListResult Dijkstra(FloatResult @defaultWeight, AliasResult @endNode, NumericResult @numberOfWantedPaths, StringResult @relationshipTypesAndDirections, AliasResult @startNode, StringResult @weightPropertyName)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoDijkstra, new object[] { @defaultWeight, @endNode, @numberOfWantedPaths, @relationshipTypesAndDirections, @startNode, @weightPropertyName });
            }
            /// <summary>
            /// apoc.algo.dijkstraWithDefaultWeight(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, &apos;distance&apos;, 10) YIELD path, weight - run dijkstra with relationship property name as cost function and a default weight if the property does not exist
            /// </summary>
            public static MiscJaggedListResult Dijkstrawithdefaultweight(FloatResult @defaultWeight, AliasResult @endNode, StringResult @relationshipTypesAndDirections, AliasResult @startNode, StringResult @weightPropertyName)
            {
                return new MiscJaggedListResult(t => t.CallApocAlgoDijkstrawithdefaultweight, new object[] { @defaultWeight, @endNode, @relationshipTypesAndDirections, @startNode, @weightPropertyName });
            }
        }

        public static partial class Any
        {
            /// <summary>
            /// returns properties for virtual and real, nodes, rels and maps
            /// </summary>
            public static MiscJaggedListResult Properties(StringListResult @keys, MiscResult @thing)
            {
                return new MiscJaggedListResult(t => t.FnApocAnyProperties, new object[] { @keys, @thing });
            }
            /// <summary>
            /// returns property for virtual and real, nodes, rels and maps
            /// </summary>
            public static MiscJaggedListResult Property(StringResult @key, MiscResult @thing)
            {
                return new MiscJaggedListResult(t => t.FnApocAnyProperty, new object[] { @key, @thing });
            }
        }

        public static partial class Atomic
        {
            /// <summary>
            /// apoc.atomic.add(node/relatonship,propertyName,number) Sums the property’s value with the &apos;number&apos; value
            /// </summary>
            public static MiscJaggedListResult Add(MiscResult @container, FloatResult @number, StringResult @propertyName)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicAdd(3), new object[] { @container, @number, @propertyName });
            }
            /// <summary>
            /// apoc.atomic.add(node/relatonship,propertyName,number) Sums the property’s value with the &apos;number&apos; value
            /// </summary>
            public static MiscJaggedListResult Add(MiscResult @container, FloatResult @number, StringResult @propertyName, NumericResult @times)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicAdd(4), new object[] { @container, @number, @propertyName, @times });
            }
            /// <summary>
            /// apoc.atomic.concat(node/relatonship,propertyName,string) Concats the property’s value with the &apos;string&apos; value
            /// </summary>
            public static MiscJaggedListResult Concat(MiscResult @container, StringResult @propertyName, StringResult @string)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicConcat(3), new object[] { @container, @propertyName, @string });
            }
            /// <summary>
            /// apoc.atomic.concat(node/relatonship,propertyName,string) Concats the property’s value with the &apos;string&apos; value
            /// </summary>
            public static MiscJaggedListResult Concat(MiscResult @container, StringResult @propertyName, StringResult @string, NumericResult @times)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicConcat(4), new object[] { @container, @propertyName, @string, @times });
            }
            /// <summary>
            /// apoc.atomic.insert(node/relatonship,propertyName,position,value) insert a value into the property’s array value at &apos;position&apos;
            /// </summary>
            public static MiscJaggedListResult Insert(MiscResult @container, NumericResult @position, StringResult @propertyName, NumericResult @times, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicInsert, new object[] { @container, @position, @propertyName, @times, @value });
            }
            /// <summary>
            /// apoc.atomic.remove(node/relatonship,propertyName,position) remove the element at position &apos;position&apos;
            /// </summary>
            public static MiscJaggedListResult Remove(MiscResult @container, NumericResult @position, StringResult @propertyName)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicRemove(3), new object[] { @container, @position, @propertyName });
            }
            /// <summary>
            /// apoc.atomic.remove(node/relatonship,propertyName,position) remove the element at position &apos;position&apos;
            /// </summary>
            public static MiscJaggedListResult Remove(MiscResult @container, NumericResult @position, StringResult @propertyName, NumericResult @times)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicRemove(4), new object[] { @container, @position, @propertyName, @times });
            }
            /// <summary>
            /// apoc.atomic.subtract(node/relatonship,propertyName,number) Subtracts the &apos;number&apos; value to the property’s value
            /// </summary>
            public static MiscJaggedListResult Subtract(MiscResult @container, FloatResult @number, StringResult @propertyName)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicSubtract(3), new object[] { @container, @number, @propertyName });
            }
            /// <summary>
            /// apoc.atomic.subtract(node/relatonship,propertyName,number) Subtracts the &apos;number&apos; value to the property’s value
            /// </summary>
            public static MiscJaggedListResult Subtract(MiscResult @container, FloatResult @number, StringResult @propertyName, NumericResult @times)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicSubtract(4), new object[] { @container, @number, @propertyName, @times });
            }
            /// <summary>
            /// apoc.atomic.update(node/relatonship,propertyName,updateOperation) update a property’s value with a cypher operation (ex. &quot;n.prop1+n.prop2&quot;)
            /// </summary>
            public static MiscJaggedListResult Update(MiscResult @container, StringResult @operation, StringResult @propertyName)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicUpdate(3), new object[] { @container, @operation, @propertyName });
            }
            /// <summary>
            /// apoc.atomic.update(node/relatonship,propertyName,updateOperation) update a property’s value with a cypher operation (ex. &quot;n.prop1+n.prop2&quot;)
            /// </summary>
            public static MiscJaggedListResult Update(MiscResult @container, StringResult @operation, StringResult @propertyName, NumericResult @times)
            {
                return new MiscJaggedListResult(t => t.CallApocAtomicUpdate(4), new object[] { @container, @operation, @propertyName, @times });
            }
        }

        public static partial class Bitwise
        {
            /// <summary>
            /// apoc.bitwise.op(60,&apos;|&apos;,13) bitwise operations a &amp; b, a | b, a ^ b, ~a, a &gt;&gt; b, a &gt;&gt;&gt; b, a &lt;&lt; b. returns the result of the bitwise operation
            /// </summary>
            public static MiscJaggedListResult Op(NumericResult @a, NumericResult @b, StringResult @operator)
            {
                return new MiscJaggedListResult(t => t.FnApocBitwiseOp, new object[] { @a, @b, @operator });
            }
        }

        public static partial class Bolt
        {
            /// <summary>
            /// apoc.bolt.execute(url-or-key, kernelTransaction, params, config) - access to other databases via bolt for reads and writes
            /// </summary>
            public static MiscJaggedListResult Execute(MiscResult @config, StringResult @kernelTransaction, MiscResult @params, StringResult @url)
            {
                return new MiscJaggedListResult(t => t.CallApocBoltExecute, new object[] { @config, @kernelTransaction, @params, @url });
            }
        }

        public static partial class Coll
        {
            /// <summary>
            /// apoc.coll.avg([0.5,1,2.3])
            /// </summary>
            public static MiscJaggedListResult Avg(FloatListResult @numbers)
            {
                return new MiscJaggedListResult(t => t.FnApocCollAvg, new object[] { @numbers });
            }
            /// <summary>
            /// apoc.coll.combinations(coll, minSelect, maxSelect:minSelect) - Returns collection of all combinations of list elements of selection size between minSelect and maxSelect (default:minSelect), inclusive
            /// </summary>
            public static MiscJaggedListResult Combinations(MiscListResult @coll, NumericResult @maxSelect, NumericResult @minSelect)
            {
                return new MiscJaggedListResult(t => t.FnApocCollCombinations, new object[] { @coll, @maxSelect, @minSelect });
            }
            /// <summary>
            /// apoc.coll.contains(coll, value) optimized contains operation (using a HashSet) (returns single row or not)
            /// </summary>
            public static MiscJaggedListResult Contains(MiscListResult @coll, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocCollContains, new object[] { @coll, @value });
            }
            /// <summary>
            /// apoc.coll.containsAll(coll, values) optimized contains-all operation (using a HashSet) (returns single row or not)
            /// </summary>
            public static MiscJaggedListResult Containsall(MiscListResult @coll, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocCollContainsall, new object[] { @coll, @values });
            }
            /// <summary>
            /// apoc.coll.containsAllSorted(coll, value) optimized contains-all on a sorted list operation (Collections.binarySearch) (returns single row or not)
            /// </summary>
            public static MiscJaggedListResult Containsallsorted(MiscListResult @coll, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocCollContainsallsorted, new object[] { @coll, @values });
            }
            /// <summary>
            /// apoc.coll.containsDuplicates(coll) - returns true if a collection contains duplicate elements
            /// </summary>
            public static MiscJaggedListResult Containsduplicates(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollContainsduplicates, new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.containsSorted(coll, value) optimized contains on a sorted list operation (Collections.binarySearch) (returns single row or not)
            /// </summary>
            public static MiscJaggedListResult Containssorted(MiscListResult @coll, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocCollContainssorted, new object[] { @coll, @value });
            }
            /// <summary>
            /// apoc.coll.different(values) - returns true if values are different
            /// </summary>
            public static MiscJaggedListResult Different(params MiscListResult[] @values)
            {
                return new MiscJaggedListResult(t => t.FnApocCollDifferent(@values.Length), @values);
            }
            /// <summary>
            /// apoc.coll.disjunction(first, second) - returns the disjunct set of the two lists
            /// </summary>
            public static MiscJaggedListResult Disjunction(MiscListResult @first, MiscListResult @second)
            {
                return new MiscJaggedListResult(t => t.FnApocCollDisjunction, new object[] { @first, @second });
            }
            /// <summary>
            /// apoc.coll.dropDuplicateNeighbors(list) - remove duplicate consecutive objects in a list
            /// </summary>
            public static MiscJaggedListResult Dropduplicateneighbors(MiscListResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollDropduplicateneighbors, new object[] { @list });
            }
            /// <summary>
            /// apoc.coll.duplicates(coll) - returns a list of duplicate items in the collection
            /// </summary>
            public static MiscJaggedListResult Duplicates(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollDuplicates, new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.duplicatesWithCount(coll) - returns a list of duplicate items in the collection and their count, keyed by item and count (e.g., [{item: xyz, count:2}, {item:zyx, count:5}])
            /// </summary>
            public static MiscJaggedListResult Duplicateswithcount(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollDuplicateswithcount, new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.elements(list,limit,offset) yield _1,_2,..,_10,_1s,_2i,_3f,_4m,_5l,_6n,_7r,_8p - deconstruct subset of mixed list into identifiers of the correct type
            /// </summary>
            public static MiscJaggedListResult Elements(NumericResult @limit, NumericResult @offset, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.CallApocCollElements, new object[] { @limit, @offset, @values });
            }
            /// <summary>
            /// apoc.coll.fill(item, count) - returns a list with the given count of items
            /// </summary>
            public static MiscJaggedListResult Fill(NumericResult @count, StringResult @item)
            {
                return new MiscJaggedListResult(t => t.FnApocCollFill, new object[] { @count, @item });
            }
            /// <summary>
            /// apoc.coll.flatten(coll, [recursive]) - flattens list (nested if recursive is true)
            /// </summary>
            public static MiscJaggedListResult Flatten(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollFlatten(1), new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.flatten(coll, [recursive]) - flattens list (nested if recursive is true)
            /// </summary>
            public static MiscJaggedListResult Flatten(MiscListResult @coll, BooleanResult @recursive)
            {
                return new MiscJaggedListResult(t => t.FnApocCollFlatten(2), new object[] { @coll, @recursive });
            }
            /// <summary>
            /// apoc.coll.frequencies(coll) - returns a list of frequencies of the items in the collection, keyed by item and count (e.g., [{item: xyz, count:2}, {item:zyx, count:5}, {item:abc, count:1}])
            /// </summary>
            public static MiscJaggedListResult Frequencies(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollFrequencies, new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.frequenciesAsMap(coll) - return a map of frequencies of the items in the collection, key item, value count (e.g., {1:2, 2:1})
            /// </summary>
            public static MiscJaggedListResult Frequenciesasmap(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollFrequenciesasmap, new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.indexOf(coll, value) | position of value in the list
            /// </summary>
            public static MiscJaggedListResult Indexof(MiscListResult @coll, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocCollIndexof, new object[] { @coll, @value });
            }
            /// <summary>
            /// apoc.coll.insertAll(coll, index, values) | insert values at index
            /// </summary>
            public static MiscJaggedListResult Insertall(MiscListResult @coll, NumericResult @index, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocCollInsertall, new object[] { @coll, @index, @values });
            }
            /// <summary>
            /// apoc.coll.intersection(first, second) - returns the unique intersection of the two lists
            /// </summary>
            public static MiscJaggedListResult Intersection(MiscListResult @first, MiscListResult @second)
            {
                return new MiscJaggedListResult(t => t.FnApocCollIntersection, new object[] { @first, @second });
            }
            /// <summary>
            /// apoc.coll.isEqualCollection(coll, values) return true if two collections contain the same elements with the same cardinality in any order (using a HashMap)
            /// </summary>
            public static MiscJaggedListResult Isequalcollection(MiscListResult @coll, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocCollIsequalcollection, new object[] { @coll, @values });
            }
            /// <summary>
            /// apoc.coll.max([0.5,1,2.3])
            /// </summary>
            public static MiscJaggedListResult Max(params MiscListResult[] @values)
            {
                return new MiscJaggedListResult(t => t.FnApocCollMax(@values.Length), @values);
            }
            /// <summary>
            /// apoc.coll.min([0.5,1,2.3])
            /// </summary>
            public static MiscJaggedListResult Min(params MiscListResult[] @values)
            {
                return new MiscJaggedListResult(t => t.FnApocCollMin(@values.Length), @values);
            }
            /// <summary>
            /// apoc.coll.occurrences(coll, item) - returns the count of the given item in the collection
            /// </summary>
            public static MiscJaggedListResult Occurrences(MiscListResult @coll, MiscResult @item)
            {
                return new MiscJaggedListResult(t => t.FnApocCollOccurrences, new object[] { @coll, @item });
            }
            /// <summary>
            /// apoc.coll.pairs([1,2,3]) returns [1,2],[2,3],[3,null]
            /// </summary>
            public static MiscJaggedListResult Pairs(MiscListResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollPairs, new object[] { @list });
            }
            /// <summary>
            /// apoc.coll.pairsMin([1,2,3]) returns [1,2],[2,3]
            /// </summary>
            public static MiscJaggedListResult Pairsmin(MiscListResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollPairsmin, new object[] { @list });
            }
            /// <summary>
            /// apoc.coll.pairWithOffset(values, offset) - returns a list of pairs defined by the offset
            /// </summary>
            public static MiscJaggedListResult Pairwithoffset(NumericResult @offset, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.CallApocCollPairwithoffset, new object[] { @offset, @values });
            }
            /// <summary>
            /// apoc.coll.partition(list,batchSize)
            /// </summary>
            public static MiscJaggedListResult Partition(NumericResult @batchSize, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.CallApocCollPartition, new object[] { @batchSize, @values });
            }
            /// <summary>
            /// apoc.coll.randomItem(coll)- returns a random item from the list, or null on an empty or null list
            /// </summary>
            public static MiscJaggedListResult Randomitem(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRandomitem, new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.randomItems(coll, itemCount, allowRepick: false) - returns a list of itemCount random items from the original list, optionally allowing picked elements to be picked again
            /// </summary>
            public static MiscJaggedListResult Randomitems(BooleanResult @allowRepick, MiscListResult @coll, NumericResult @itemCount)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRandomitems, new object[] { @allowRepick, @coll, @itemCount });
            }
            /// <summary>
            /// apoc.coll.removeAll(first, second) - returns first list with all elements of second list removed
            /// </summary>
            public static MiscJaggedListResult Removeall(MiscListResult @first, MiscListResult @second)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRemoveall, new object[] { @first, @second });
            }
            /// <summary>
            /// apoc.coll.reverse(coll) - returns reversed list
            /// </summary>
            public static MiscJaggedListResult Reverse(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollReverse, new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.runningTotal(list1) - returns an accumulative array. For example apoc.coll.runningTotal([1,2,3.5]) return [1,3,6.5]
            /// </summary>
            public static MiscJaggedListResult Runningtotal(FloatListResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRunningtotal, new object[] { @list });
            }
            /// <summary>
            /// apoc.coll.set(coll, index, value) | set index to value
            /// </summary>
            public static MiscJaggedListResult Set(MiscListResult @coll, NumericResult @index, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSet, new object[] { @coll, @index, @value });
            }
            /// <summary>
            /// apoc.coll.shuffle(coll) - returns the shuffled list
            /// </summary>
            public static MiscJaggedListResult Shuffle(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollShuffle, new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.sort(coll) sort on Collections
            /// </summary>
            public static MiscJaggedListResult Sort(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSort, new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.sortMaps([maps], &apos;name&apos;) - sort maps by property
            /// </summary>
            public static MiscJaggedListResult Sortmaps(MiscListResult @coll, StringResult @prop)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSortmaps, new object[] { @coll, @prop });
            }
            /// <summary>
            /// apoc.coll.sortMulti(coll, [&apos;^name&apos;,&apos;age&apos;],[limit],[skip]) - sort list of maps by several sort fields (ascending with ^ prefix) and optionally applies limit and skip
            /// </summary>
            public static MiscJaggedListResult Sortmulti(MiscListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSortmulti(1), new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.sortMulti(coll, [&apos;^name&apos;,&apos;age&apos;],[limit],[skip]) - sort list of maps by several sort fields (ascending with ^ prefix) and optionally applies limit and skip
            /// </summary>
            public static MiscJaggedListResult Sortmulti(MiscListResult @coll, NumericResult @limit)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSortmulti(2), new object[] { @coll, @limit });
            }
            /// <summary>
            /// apoc.coll.sortMulti(coll, [&apos;^name&apos;,&apos;age&apos;],[limit],[skip]) - sort list of maps by several sort fields (ascending with ^ prefix) and optionally applies limit and skip
            /// </summary>
            public static MiscJaggedListResult Sortmulti(MiscListResult @coll, NumericResult @limit, StringListResult @orderFields)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSortmulti(3), new object[] { @coll, @limit, @orderFields });
            }
            /// <summary>
            /// apoc.coll.sortMulti(coll, [&apos;^name&apos;,&apos;age&apos;],[limit],[skip]) - sort list of maps by several sort fields (ascending with ^ prefix) and optionally applies limit and skip
            /// </summary>
            public static MiscJaggedListResult Sortmulti(MiscListResult @coll, NumericResult @limit, StringListResult @orderFields, NumericResult @skip)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSortmulti(4), new object[] { @coll, @limit, @orderFields, @skip });
            }
            /// <summary>
            /// apoc.coll.sortNodes([nodes], &apos;name&apos;) sort nodes by property
            /// </summary>
            public static MiscJaggedListResult Sortnodes(AliasListResult @coll, StringResult @prop)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSortnodes, new object[] { @coll, @prop });
            }
            /// <summary>
            /// apoc.coll.sortText(coll) sort on string based collections
            /// </summary>
            public static MiscJaggedListResult Sorttext(StringListResult @coll)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSorttext(1), new object[] { @coll });
            }
            /// <summary>
            /// apoc.coll.sortText(coll) sort on string based collections
            /// </summary>
            public static MiscJaggedListResult Sorttext(StringListResult @coll, MiscResult @conf)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSorttext(2), new object[] { @coll, @conf });
            }
            /// <summary>
            /// apoc.coll.split(list,value) | splits collection on given values rows of lists, value itself will not be part of resulting lists
            /// </summary>
            public static MiscJaggedListResult Split(MiscResult @value, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.CallApocCollSplit, new object[] { @value, @values });
            }
            /// <summary>
            /// apoc.coll.stdev(list, isBiasCorrected) - returns the sample or population standard deviation with isBiasCorrected true or false respectively. For example apoc.coll.stdev([10, 12, 23]) return 7
            /// </summary>
            public static MiscJaggedListResult Stdev(BooleanResult @isBiasCorrected, FloatListResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocCollStdev, new object[] { @isBiasCorrected, @list });
            }
            /// <summary>
            /// apoc.coll.sum([0.5,1,2.3])
            /// </summary>
            public static MiscJaggedListResult Sum(FloatListResult @numbers)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSum, new object[] { @numbers });
            }
            /// <summary>
            /// apoc.coll.sumLongs([1,3,3])
            /// </summary>
            public static MiscJaggedListResult Sumlongs(FloatListResult @numbers)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSumlongs, new object[] { @numbers });
            }
            /// <summary>
            /// apoc.coll.toSet([list]) returns a unique list backed by a set
            /// </summary>
            public static MiscJaggedListResult Toset(params MiscListResult[] @values)
            {
                return new MiscJaggedListResult(t => t.FnApocCollToset(@values.Length), @values);
            }
            /// <summary>
            /// apoc.coll.union(first, second) - creates the distinct union of the 2 lists
            /// </summary>
            public static MiscJaggedListResult Union(MiscListResult @first, MiscListResult @second)
            {
                return new MiscJaggedListResult(t => t.FnApocCollUnion, new object[] { @first, @second });
            }
            /// <summary>
            /// apoc.coll.unionAll(first, second) - creates the full union with duplicates of the two lists
            /// </summary>
            public static MiscJaggedListResult Unionall(MiscListResult @first, MiscListResult @second)
            {
                return new MiscJaggedListResult(t => t.FnApocCollUnionall, new object[] { @first, @second });
            }
            /// <summary>
            /// apoc.coll.zip([list1],[list2])
            /// </summary>
            public static MiscJaggedListResult Zip(MiscListResult @list1, MiscListResult @list2)
            {
                return new MiscJaggedListResult(t => t.FnApocCollZip, new object[] { @list1, @list2 });
            }
            /// <summary>
            /// apoc.coll.zipToRows(list1,list2) - creates pairs like zip but emits one row per pair
            /// </summary>
            public static MiscJaggedListResult Ziptorows(MiscListResult @list1, MiscListResult @list2)
            {
                return new MiscJaggedListResult(t => t.CallApocCollZiptorows, new object[] { @list1, @list2 });
            }
            /// <summary>
            /// apoc.coll.insert(coll, index, value) | insert value at index
            /// </summary>
            public static MiscJaggedListResult Insert(MiscListResult @coll, NumericResult @index, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocCollInsert, new object[] { @coll, @index, @value });
            }
            /// <summary>
            /// apoc.coll.remove(coll, index, [length=1]) | remove range of values from index to length
            /// </summary>
            public static MiscJaggedListResult Remove(MiscListResult @coll, NumericResult @index)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRemove(2), new object[] { @coll, @index });
            }
            /// <summary>
            /// apoc.coll.remove(coll, index, [length=1]) | remove range of values from index to length
            /// </summary>
            public static MiscJaggedListResult Remove(MiscListResult @coll, NumericResult @index, NumericResult @length)
            {
                return new MiscJaggedListResult(t => t.FnApocCollRemove(3), new object[] { @coll, @index, @length });
            }
            /// <summary>
            /// apoc.coll.subtract(first, second) - returns unique set of first list with all elements of second list removed
            /// </summary>
            public static MiscJaggedListResult Subtract(MiscListResult @first, MiscListResult @second)
            {
                return new MiscJaggedListResult(t => t.FnApocCollSubtract, new object[] { @first, @second });
            }
        }

        public static partial class Convert
        {
            /// <summary>
            /// apoc.convert.fromJsonList(&apos;[1,2,3]&apos;[,&apos;json-path&apos;, &apos;path-options&apos;])
            /// </summary>
            public static MiscJaggedListResult Fromjsonlist(StringResult @list, StringResult @path)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertFromjsonlist(2), new object[] { @list, @path });
            }
            /// <summary>
            /// apoc.convert.fromJsonList(&apos;[1,2,3]&apos;[,&apos;json-path&apos;, &apos;path-options&apos;])
            /// </summary>
            public static MiscJaggedListResult Fromjsonlist(StringResult @list, StringResult @path, StringListResult @pathOptions)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertFromjsonlist(3), new object[] { @list, @path, @pathOptions });
            }
            /// <summary>
            /// apoc.convert.fromJsonMap(&apos;{&quot;a&quot;:42,&quot;b&quot;:&quot;foo&quot;,&quot;c&quot;:[1,2,3]}&apos;[,&apos;json-path&apos;, &apos;path-options&apos;])
            /// </summary>
            public static MiscJaggedListResult Fromjsonmap(StringResult @map, StringResult @path)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertFromjsonmap(2), new object[] { @map, @path });
            }
            /// <summary>
            /// apoc.convert.fromJsonMap(&apos;{&quot;a&quot;:42,&quot;b&quot;:&quot;foo&quot;,&quot;c&quot;:[1,2,3]}&apos;[,&apos;json-path&apos;, &apos;path-options&apos;])
            /// </summary>
            public static MiscJaggedListResult Fromjsonmap(StringResult @map, StringResult @path, StringListResult @pathOptions)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertFromjsonmap(3), new object[] { @map, @path, @pathOptions });
            }
            /// <summary>
            /// apoc.convert.getJsonProperty(node,key[,&apos;json-path&apos;, &apos;path-options&apos;]) - converts serialized JSON in property back to original object
            /// </summary>
            public static MiscJaggedListResult Getjsonproperty(StringResult @key, AliasResult @node, StringResult @path)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertGetjsonproperty(3), new object[] { @key, @node, @path });
            }
            /// <summary>
            /// apoc.convert.getJsonProperty(node,key[,&apos;json-path&apos;, &apos;path-options&apos;]) - converts serialized JSON in property back to original object
            /// </summary>
            public static MiscJaggedListResult Getjsonproperty(StringResult @key, AliasResult @node, StringResult @path, StringListResult @pathOptions)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertGetjsonproperty(4), new object[] { @key, @node, @path, @pathOptions });
            }
            /// <summary>
            /// apoc.convert.getJsonPropertyMap(node,key[,&apos;json-path&apos;, &apos;path-options&apos;]) - converts serialized JSON in property back to map
            /// </summary>
            public static MiscJaggedListResult Getjsonpropertymap(StringResult @key, AliasResult @node, StringResult @path)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertGetjsonpropertymap(3), new object[] { @key, @node, @path });
            }
            /// <summary>
            /// apoc.convert.getJsonPropertyMap(node,key[,&apos;json-path&apos;, &apos;path-options&apos;]) - converts serialized JSON in property back to map
            /// </summary>
            public static MiscJaggedListResult Getjsonpropertymap(StringResult @key, AliasResult @node, StringResult @path, StringListResult @pathOptions)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertGetjsonpropertymap(4), new object[] { @key, @node, @path, @pathOptions });
            }
            /// <summary>
            /// apoc.convert.setJsonProperty(node,key,complexValue) - sets value serialized to JSON as property with the given name on the node
            /// </summary>
            public static MiscJaggedListResult Setjsonproperty(StringResult @key, AliasResult @node, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocConvertSetjsonproperty, new object[] { @key, @node, @value });
            }
            /// <summary>
            /// apoc.convert.toBoolean(value) | tries it’s best to convert the value to a boolean
            /// </summary>
            public static MiscJaggedListResult Toboolean(MiscResult @bool)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertToboolean, new object[] { @bool });
            }
            /// <summary>
            /// apoc.convert.toBooleanList(value) | tries it’s best to convert the value to a list of booleans
            /// </summary>
            public static MiscJaggedListResult Tobooleanlist(MiscResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTobooleanlist, new object[] { @list });
            }
            /// <summary>
            /// apoc.convert.toFloat(value) | tries it’s best to convert the value to a float
            /// </summary>
            public static MiscJaggedListResult Tofloat(MiscResult @object)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTofloat, new object[] { @object });
            }
            /// <summary>
            /// apoc.convert.toInteger(value) | tries it’s best to convert the value to an integer
            /// </summary>
            public static MiscJaggedListResult Tointeger(MiscResult @object)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTointeger, new object[] { @object });
            }
            /// <summary>
            /// apoc.convert.toIntList(value) | tries it’s best to convert the value to a list of integers
            /// </summary>
            public static MiscJaggedListResult Tointlist(MiscResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTointlist, new object[] { @list });
            }
            /// <summary>
            /// apoc.convert.toJson([1,2,3]) or toJson({a:42,b:&quot;foo&quot;,c:[1,2,3]}) or toJson(NODE/REL/PATH)
            /// </summary>
            public static MiscJaggedListResult Tojson(MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTojson, new object[] { @value });
            }
            /// <summary>
            /// apoc.convert.toList(value) | tries it’s best to convert the value to a list
            /// </summary>
            public static MiscJaggedListResult Tolist(MiscResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTolist, new object[] { @list });
            }
            /// <summary>
            /// apoc.convert.toMap(value) | tries it’s best to convert the value to a map
            /// </summary>
            public static MiscJaggedListResult Tomap(MiscResult @map)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTomap, new object[] { @map });
            }
            /// <summary>
            /// apoc.convert.toNode(value) | tries it’s best to convert the value to a node
            /// </summary>
            public static MiscJaggedListResult Tonode(MiscResult @node)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTonode, new object[] { @node });
            }
            /// <summary>
            /// apoc.convert.toNodeList(value) | tries it’s best to convert the value to a list of nodes
            /// </summary>
            public static MiscJaggedListResult Tonodelist(MiscResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTonodelist, new object[] { @list });
            }
            /// <summary>
            /// apoc.convert.toRelationship(value) | tries it’s best to convert the value to a relationship
            /// </summary>
            public static MiscJaggedListResult Torelationship(MiscResult @relationship)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTorelationship, new object[] { @relationship });
            }
            /// <summary>
            /// apoc.convert.toRelationshipList(value) | tries it’s best to convert the value to a list of relationships
            /// </summary>
            public static MiscJaggedListResult Torelationshiplist(MiscResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTorelationshiplist, new object[] { @list });
            }
            /// <summary>
            /// apoc.convert.toSortedJsonMap(node|map, ignoreCase:true) - returns a JSON map with keys sorted alphabetically, with optional case sensitivity
            /// </summary>
            public static MiscJaggedListResult Tosortedjsonmap(BooleanResult @ignoreCase, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTosortedjsonmap, new object[] { @ignoreCase, @value });
            }
            /// <summary>
            /// apoc.convert.toString(value) | tries it’s best to convert the value to a string
            /// </summary>
            public static MiscJaggedListResult Tostring(MiscResult @string)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTostring, new object[] { @string });
            }
            /// <summary>
            /// apoc.convert.toStringList(value) | tries it’s best to convert the value to a list of strings
            /// </summary>
            public static MiscJaggedListResult Tostringlist(MiscResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertTostringlist, new object[] { @list });
            }
            /// <summary>
            /// apoc.convert.toTree([paths],[lowerCaseRels=true], [config]) creates a stream of nested documents representing the at least one root of these paths
            /// </summary>
            public static MiscJaggedListResult Totree(MiscResult @config, BooleanResult @lowerCaseRels, PathListResult @paths)
            {
                return new MiscJaggedListResult(t => t.CallApocConvertTotree, new object[] { @config, @lowerCaseRels, @paths });
            }
            /// <summary>
            /// apoc.convert.toSet(value) | tries it’s best to convert the value to a set
            /// </summary>
            public static MiscJaggedListResult Toset(MiscResult @list)
            {
                return new MiscJaggedListResult(t => t.FnApocConvertToset, new object[] { @list });
            }
        }

        public static partial class Create
        {
            /// <summary>
            /// apoc.create.addLabels( [node,id,ids,nodes], [&apos;Label&apos;,…​]) - adds the given labels to the node or nodes
            /// </summary>
            public static MiscJaggedListResult Addlabels(StringListResult @label, MiscResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateAddlabels, new object[] { @label, @nodes });
            }
            /// <summary>
            /// apoc.create.clonePathsToVirtual
            /// </summary>
            public static MiscJaggedListResult Clonepathstovirtual(PathListResult @paths)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateClonepathstovirtual, new object[] { @paths });
            }
            /// <summary>
            /// apoc.create.clonePathToVirtual
            /// </summary>
            public static MiscJaggedListResult Clonepathtovirtual(PathResult @path)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateClonepathtovirtual, new object[] { @path });
            }
            /// <summary>
            /// apoc.create.node([&apos;Label&apos;], {key:value,…​}) - create node with dynamic labels
            /// </summary>
            public static MiscJaggedListResult Node(StringListResult @label, MiscResult @props)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateNode, new object[] { @label, @props });
            }
            /// <summary>
            /// apoc.create.nodes([&apos;Label&apos;], [{key:value,…​}]) create multiple nodes with dynamic labels
            /// </summary>
            public static MiscJaggedListResult Nodes(StringListResult @label, MiscListResult @props)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateNodes, new object[] { @label, @props });
            }
            /// <summary>
            /// apoc.create.relationship(person1,&apos;KNOWS&apos;,{key:value,…​}, person2) create relationship with dynamic rel-type
            /// </summary>
            public static MiscJaggedListResult Relationship(AliasResult @from, MiscResult @props, StringResult @relType, AliasResult @to)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateRelationship, new object[] { @from, @props, @relType, @to });
            }
            /// <summary>
            /// apoc.create.removeLabels( [node,id,ids,nodes], [&apos;Label&apos;,…​]) - removes the given labels from the node or nodes
            /// </summary>
            public static MiscJaggedListResult Removelabels(StringListResult @label, MiscResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateRemovelabels, new object[] { @label, @nodes });
            }
            /// <summary>
            /// apoc.create.removeProperties( [node,id,ids,nodes], [keys]) - removes the given properties from the nodes(s)
            /// </summary>
            public static MiscJaggedListResult Removeproperties(StringListResult @keys, MiscResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateRemoveproperties, new object[] { @keys, @nodes });
            }
            /// <summary>
            /// apoc.create.removeRelProperties( [rel,id,ids,rels], [keys]) - removes the given properties from the relationship(s)
            /// </summary>
            public static MiscJaggedListResult Removerelproperties(StringListResult @keys, MiscResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateRemoverelproperties, new object[] { @keys, @rels });
            }
            /// <summary>
            /// apoc.create.setLabels( [node,id,ids,nodes], [&apos;Label&apos;,…​]) - sets the given labels, non matching labels are removed on the node or nodes
            /// </summary>
            public static MiscJaggedListResult Setlabels(StringListResult @label, MiscResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateSetlabels, new object[] { @label, @nodes });
            }
            /// <summary>
            /// apoc.create.setProperties( [node,id,ids,nodes], [keys], [values]) - sets the given properties on the nodes(s)
            /// </summary>
            public static MiscJaggedListResult Setproperties(StringListResult @keys, MiscResult @nodes, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateSetproperties, new object[] { @keys, @nodes, @values });
            }
            /// <summary>
            /// apoc.create.setProperty( [node,id,ids,nodes], key, value) - sets the given property on the node(s)
            /// </summary>
            public static MiscJaggedListResult Setproperty(StringResult @key, MiscResult @nodes, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateSetproperty, new object[] { @key, @nodes, @value });
            }
            /// <summary>
            /// apoc.create.setRelProperties( [rel,id,ids,rels], [keys], [values]) - sets the given properties on the relationship(s)
            /// </summary>
            public static MiscJaggedListResult Setrelproperties(StringListResult @keys, MiscResult @rels, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateSetrelproperties, new object[] { @keys, @rels, @values });
            }
            /// <summary>
            /// apoc.create.setRelProperty( [rel,id,ids,rels], key, value) - sets the given property on the relationship(s)
            /// </summary>
            public static MiscJaggedListResult Setrelproperty(StringResult @key, MiscResult @relationships, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateSetrelproperty, new object[] { @key, @relationships, @value });
            }
            /// <summary>
            /// apoc.create.uuid() - creates an UUID
            /// </summary>
            public static MiscJaggedListResult Uuid()
            {
                return new MiscJaggedListResult(t => t.FnApocCreateUuid);
            }
            /// <summary>
            /// apoc.create.uuids(count) yield uuid - creates &apos;count&apos; UUIDs
            /// </summary>
            public static MiscJaggedListResult Uuids(NumericResult @count)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateUuids, new object[] { @count });
            }
            /// <summary>
            /// apoc.create.virtualPath([&apos;LabelA&apos;],{key:value},&apos;KNOWS&apos;,{key:value,…​},[&apos;LabelB&apos;],{key:value}) returns a virtual path of nodes joined by a relationship and the associated properties
            /// </summary>
            public static MiscJaggedListResult Virtualpath(StringListResult @labelsM, StringListResult @labelsN, MiscResult @m, MiscResult @n, MiscResult @props, StringResult @relType)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVirtualpath, new object[] { @labelsM, @labelsN, @m, @n, @props, @relType });
            }
            /// <summary>
            /// apoc.create.vNode([&apos;Label&apos;], {key:value,…​}) returns a virtual node
            /// </summary>
            public static MiscJaggedListResult Vnode(StringListResult @label)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVnode(1), new object[] { @label });
            }
            /// <summary>
            /// apoc.create.vNode([&apos;Label&apos;], {key:value,…​}) returns a virtual node
            /// </summary>
            public static MiscJaggedListResult Vnode(StringListResult @label, MiscResult @props)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVnode(2), new object[] { @label, @props });
            }
            /// <summary>
            /// apoc.create.vNodes([&apos;Label&apos;], [{key:value,…​}]) returns virtual nodes
            /// </summary>
            public static MiscJaggedListResult Vnodes(StringListResult @label, MiscListResult @props)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVnodes, new object[] { @label, @props });
            }
            /// <summary>
            /// apoc.create.vPattern({_labels:[&apos;LabelA&apos;],key:value},&apos;KNOWS&apos;,{key:value,…​}, {_labels:[&apos;LabelB&apos;],key:value}) returns a virtual pattern
            /// </summary>
            public static MiscJaggedListResult Vpattern(MiscResult @from, MiscResult @props, StringResult @relType, MiscResult @to)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVpattern, new object[] { @from, @props, @relType, @to });
            }
            /// <summary>
            /// apoc.create.vPatternFull([&apos;LabelA&apos;],{key:value},&apos;KNOWS&apos;,{key:value,…​},[&apos;LabelB&apos;],{key:value}) returns a virtual pattern
            /// </summary>
            public static MiscJaggedListResult Vpatternfull(StringListResult @labelsM, StringListResult @labelsN, MiscResult @m, MiscResult @n, MiscResult @props, StringResult @relType)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVpatternfull, new object[] { @labelsM, @labelsN, @m, @n, @props, @relType });
            }
            /// <summary>
            /// apoc.create.vRelationship(nodeFrom,&apos;KNOWS&apos;,{key:value,…​}, nodeTo) returns a virtual relationship
            /// </summary>
            public static MiscJaggedListResult Vrelationship(AliasResult @from, MiscResult @props, StringResult @relType, AliasResult @to)
            {
                return new MiscJaggedListResult(t => t.CallApocCreateVrelationship, new object[] { @from, @props, @relType, @to });
            }
        }

        public static partial class CreateVirtual
        {
            /// <summary>
            /// apoc.create.virtual.fromNode(node, [propertyNames]) returns a virtual node built from an existing node with only the requested properties
            /// </summary>
            public static MiscJaggedListResult Fromnode(AliasResult @node, StringListResult @propertyNames)
            {
                return new MiscJaggedListResult(t => t.FnApocCreateVirtualFromnode, new object[] { @node, @propertyNames });
            }
        }

        public static partial class Cypher
        {
            /// <summary>
            /// apoc.cypher.doIt(fragment, params) yield value - executes writing fragment with the given parameters
            /// </summary>
            public static MiscJaggedListResult Doit(StringResult @cypher, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherDoit, new object[] { @cypher, @params });
            }
            /// <summary>
            /// apoc.cypher.run(fragment, params) yield value - executes reading fragment with the given parameters - currently no schema operations
            /// </summary>
            public static MiscJaggedListResult Run(StringResult @cypher, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRun, new object[] { @cypher, @params });
            }
            /// <summary>
            /// use either apoc.cypher.runFirstColumnMany for a list return or apoc.cypher.runFirstColumnSingle for returning the first row of the first column
            /// </summary>
            public static MiscJaggedListResult Runfirstcolumn(StringResult @cypher, BooleanResult @expectMultipleValues, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.FnApocCypherRunfirstcolumn, new object[] { @cypher, @expectMultipleValues, @params });
            }
            /// <summary>
            /// apoc.cypher.runFirstColumnMany(statement, params) - executes statement with given parameters, returns first column only collected into a list, params are available as identifiers
            /// </summary>
            public static MiscJaggedListResult Runfirstcolumnmany(StringResult @cypher, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.FnApocCypherRunfirstcolumnmany, new object[] { @cypher, @params });
            }
            /// <summary>
            /// apoc.cypher.runFirstColumnSingle(statement, params) - executes statement with given parameters, returns first element of the first column only, params are available as identifiers
            /// </summary>
            public static MiscJaggedListResult Runfirstcolumnsingle(StringResult @cypher, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.FnApocCypherRunfirstcolumnsingle, new object[] { @cypher, @params });
            }
            /// <summary>
            /// apoc.cypher.runMany(&apos;cypher;\nstatements;&apos;, $params, [{statistics:true,timeout:10}]) - runs each semicolon separated statement and returns summary - currently no schema operations
            /// </summary>
            public static MiscJaggedListResult Runmany(MiscResult @config, StringResult @cypher, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRunmany, new object[] { @config, @cypher, @params });
            }
            /// <summary>
            /// apoc.cypher.runManyReadOnly(&apos;cypher;\nstatements;&apos;, $params, [{statistics:true,timeout:10}]) - runs each semicolon separated, read-only statement and returns summary - currently no schema operations
            /// </summary>
            public static MiscJaggedListResult Runmanyreadonly(MiscResult @config, StringResult @cypher, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRunmanyreadonly, new object[] { @config, @cypher, @params });
            }
            /// <summary>
            /// apoc.cypher.runSchema(statement, params) yield value - executes query schema statement with the given parameters
            /// </summary>
            public static MiscJaggedListResult Runschema(StringResult @cypher, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRunschema, new object[] { @cypher, @params });
            }
            /// <summary>
            /// apoc.cypher.runTimeboxed(&apos;cypherStatement&apos;,{params}, timeout) - abort kernelTransaction after timeout ms if not finished
            /// </summary>
            public static MiscJaggedListResult Runtimeboxed(StringResult @cypher, MiscResult @params, NumericResult @timeout)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRuntimeboxed, new object[] { @cypher, @params, @timeout });
            }
            /// <summary>
            /// apoc.cypher.runWrite(statement, params) yield value - alias for apoc.cypher.doIt
            /// </summary>
            public static MiscJaggedListResult Runwrite(StringResult @cypher, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.CallApocCypherRunwrite, new object[] { @cypher, @params });
            }
        }

        public static partial class Data
        {
            /// <summary>
            /// apoc.data.domain(&apos;url_or_email_address&apos;) YIELD domain - extract the domain name from a url or an email address. If nothing was found, yield null.
            /// </summary>
            public static MiscJaggedListResult Domain(StringResult @url_or_email_address)
            {
                return new MiscJaggedListResult(t => t.FnApocDataDomain, new object[] { @url_or_email_address });
            }
            /// <summary>
            /// apoc.data.url(&apos;url&apos;) as {protocol,host,port,path,query,file,anchor,user} | turn URL into map structure
            /// </summary>
            public static MiscJaggedListResult Url(StringResult @url)
            {
                return new MiscJaggedListResult(t => t.FnApocDataUrl, new object[] { @url });
            }
        }

        public static partial class Date
        {
            /// <summary>
            /// apoc.date.convert(12345, &apos;ms&apos;, &apos;d&apos;) - convert a timestamp in one time unit into one of a different time unit
            /// </summary>
            public static MiscJaggedListResult Convert(NumericResult @time, StringResult @toUnit, StringResult @unit)
            {
                return new MiscJaggedListResult(t => t.FnApocDateConvert, new object[] { @time, @toUnit, @unit });
            }
            /// <summary>
            /// apoc.date.convertFormat(&apos;Tue, 14 May 2019 14:52:06 -0400&apos;, &apos;rfc_1123_date_time&apos;, &apos;iso_date_time&apos;) - convert a String of one date format into a String of another date format.
            /// </summary>
            public static MiscJaggedListResult Convertformat(StringResult @convertTo, StringResult @currentFormat, StringResult @temporal)
            {
                return new MiscJaggedListResult(t => t.FnApocDateConvertformat, new object[] { @convertTo, @currentFormat, @temporal });
            }
            /// <summary>
            /// apoc.date.currentTimestamp() - returns System.currentTimeMillis() at the time it was called. The value is current throughout transaction execution, and is different from Cypher’s timestamp() function, which does not update within a transaction.
            /// </summary>
            public static MiscJaggedListResult Currenttimestamp()
            {
                return new MiscJaggedListResult(t => t.FnApocDateCurrenttimestamp);
            }
            /// <summary>
            /// apoc.date.field(12345,(&apos;ms|s|m|h|d|month|year&apos;),(&apos;TZ&apos;)
            /// </summary>
            public static MiscJaggedListResult Field(NumericResult @time)
            {
                return new MiscJaggedListResult(t => t.FnApocDateField(1), new object[] { @time });
            }
            /// <summary>
            /// apoc.date.field(12345,(&apos;ms|s|m|h|d|month|year&apos;),(&apos;TZ&apos;)
            /// </summary>
            public static MiscJaggedListResult Field(NumericResult @time, StringResult @timezone)
            {
                return new MiscJaggedListResult(t => t.FnApocDateField(2), new object[] { @time, @timezone });
            }
            /// <summary>
            /// apoc.date.field(12345,(&apos;ms|s|m|h|d|month|year&apos;),(&apos;TZ&apos;)
            /// </summary>
            public static MiscJaggedListResult Field(NumericResult @time, StringResult @timezone, StringResult @unit)
            {
                return new MiscJaggedListResult(t => t.FnApocDateField(3), new object[] { @time, @timezone, @unit });
            }
            /// <summary>
            /// apoc.date.fields(&apos;2012-12-23&apos;,(&apos;yyyy-MM-dd&apos;)) - return columns and a map representation of date parsed with the given format with entries for years,months,weekdays,days,hours,minutes,seconds,zoneid
            /// </summary>
            public static MiscJaggedListResult Fields(StringResult @date)
            {
                return new MiscJaggedListResult(t => t.FnApocDateFields(1), new object[] { @date });
            }
            /// <summary>
            /// apoc.date.fields(&apos;2012-12-23&apos;,(&apos;yyyy-MM-dd&apos;)) - return columns and a map representation of date parsed with the given format with entries for years,months,weekdays,days,hours,minutes,seconds,zoneid
            /// </summary>
            public static MiscJaggedListResult Fields(StringResult @date, StringResult @pattern)
            {
                return new MiscJaggedListResult(t => t.FnApocDateFields(2), new object[] { @date, @pattern });
            }
            /// <summary>
            /// apoc.date.format(12345,(&apos;ms|s|m|h|d&apos;),(&apos;yyyy-MM-dd HH:mm:ss zzz&apos;),(&apos;TZ&apos;)) - get string representation of time value optionally using the specified unit (default ms) using specified format (default ISO) and specified time zone (default current TZ)
            /// </summary>
            public static MiscJaggedListResult Format(StringResult @format, NumericResult @time, StringResult @timezone)
            {
                return new MiscJaggedListResult(t => t.FnApocDateFormat(3), new object[] { @format, @time, @timezone });
            }
            /// <summary>
            /// apoc.date.format(12345,(&apos;ms|s|m|h|d&apos;),(&apos;yyyy-MM-dd HH:mm:ss zzz&apos;),(&apos;TZ&apos;)) - get string representation of time value optionally using the specified unit (default ms) using specified format (default ISO) and specified time zone (default current TZ)
            /// </summary>
            public static MiscJaggedListResult Format(StringResult @format, NumericResult @time, StringResult @timezone, StringResult @unit)
            {
                return new MiscJaggedListResult(t => t.FnApocDateFormat(4), new object[] { @format, @time, @timezone, @unit });
            }
            /// <summary>
            /// apoc.date.fromISO8601(&apos;yyyy-MM-ddTHH:mm:ss.SSSZ&apos;) - return number representation of time in EPOCH format
            /// </summary>
            public static MiscJaggedListResult Fromiso8601(StringResult @time)
            {
                return new MiscJaggedListResult(t => t.FnApocDateFromiso8601, new object[] { @time });
            }
            /// <summary>
            /// apoc.date.parse(&apos;2012-12-23&apos;,&apos;ms|s|m|h|d&apos;,&apos;yyyy-MM-dd&apos;) - parse date string using the specified format into the specified time unit
            /// </summary>
            public static MiscJaggedListResult Parse(StringResult @format, StringResult @time, StringResult @timezone)
            {
                return new MiscJaggedListResult(t => t.FnApocDateParse(3), new object[] { @format, @time, @timezone });
            }
            /// <summary>
            /// apoc.date.parse(&apos;2012-12-23&apos;,&apos;ms|s|m|h|d&apos;,&apos;yyyy-MM-dd&apos;) - parse date string using the specified format into the specified time unit
            /// </summary>
            public static MiscJaggedListResult Parse(StringResult @format, StringResult @time, StringResult @timezone, StringResult @unit)
            {
                return new MiscJaggedListResult(t => t.FnApocDateParse(4), new object[] { @format, @time, @timezone, @unit });
            }
            /// <summary>
            /// apoc.date.parseAsZonedDateTime(&apos;2012-12-23 23:59:59&apos;,&apos;yyyy-MM-dd HH:mm:ss&apos;, &apos;UTC-hour-offset&apos;) - parse date string using the specified format to specified timezone
            /// </summary>
            public static MiscJaggedListResult Parseaszoneddatetime(StringResult @format, StringResult @time)
            {
                return new MiscJaggedListResult(t => t.FnApocDateParseaszoneddatetime(2), new object[] { @format, @time });
            }
            /// <summary>
            /// apoc.date.parseAsZonedDateTime(&apos;2012-12-23 23:59:59&apos;,&apos;yyyy-MM-dd HH:mm:ss&apos;, &apos;UTC-hour-offset&apos;) - parse date string using the specified format to specified timezone
            /// </summary>
            public static MiscJaggedListResult Parseaszoneddatetime(StringResult @format, StringResult @time, StringResult @timezone)
            {
                return new MiscJaggedListResult(t => t.FnApocDateParseaszoneddatetime(3), new object[] { @format, @time, @timezone });
            }
            /// <summary>
            /// apoc.date.systemTimezone() - returns the system timezone display name
            /// </summary>
            public static MiscJaggedListResult Systemtimezone()
            {
                return new MiscJaggedListResult(t => t.FnApocDateSystemtimezone);
            }
            /// <summary>
            /// apoc.date.toISO8601(12345,(&apos;ms|s|m|h|d&apos;) - return string representation of time in ISO8601 format
            /// </summary>
            public static MiscJaggedListResult Toiso8601(NumericResult @time)
            {
                return new MiscJaggedListResult(t => t.FnApocDateToiso8601(1), new object[] { @time });
            }
            /// <summary>
            /// apoc.date.toISO8601(12345,(&apos;ms|s|m|h|d&apos;) - return string representation of time in ISO8601 format
            /// </summary>
            public static MiscJaggedListResult Toiso8601(NumericResult @time, StringResult @unit)
            {
                return new MiscJaggedListResult(t => t.FnApocDateToiso8601(2), new object[] { @time, @unit });
            }
            /// <summary>
            /// toYears(timestamp) or toYears(date[,format]) - converts timestamp into floating point years
            /// </summary>
            public static MiscJaggedListResult Toyears(StringResult @format, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocDateToyears, new object[] { @format, @value });
            }
            /// <summary>
            /// apoc.date.add(12345, &apos;ms&apos;, -365, &apos;d&apos;) - given a timestamp in one time unit, adds a value of the specified time unit
            /// </summary>
            public static MiscJaggedListResult Add(StringResult @addUnit, NumericResult @addValue, NumericResult @time, StringResult @unit)
            {
                return new MiscJaggedListResult(t => t.FnApocDateAdd, new object[] { @addUnit, @addValue, @time, @unit });
            }
        }

        public static partial class Diff
        {
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult Nodes(AliasResult @leftNode, AliasResult @rightNode)
            {
                return new MiscJaggedListResult(t => t.FnApocDiffNodes, new object[] { @leftNode, @rightNode });
            }
        }

        public static partial class Do
        {
            /// <summary>
            /// apoc.do.case([condition, query, condition, query, …​], elseQuery:&apos;&apos;, params:{}) yield value - given a list of conditional / writing query pairs, executes the query associated with the first conditional evaluating to true (or the else query if none are true) with the given parameters
            /// </summary>
            public static MiscJaggedListResult Case(MiscListResult @conditionals, StringResult @elseQuery)
            {
                return new MiscJaggedListResult(t => t.CallApocDoCase(2), new object[] { @conditionals, @elseQuery });
            }
            /// <summary>
            /// apoc.do.case([condition, query, condition, query, …​], elseQuery:&apos;&apos;, params:{}) yield value - given a list of conditional / writing query pairs, executes the query associated with the first conditional evaluating to true (or the else query if none are true) with the given parameters
            /// </summary>
            public static MiscJaggedListResult Case(MiscListResult @conditionals, StringResult @elseQuery, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.CallApocDoCase(3), new object[] { @conditionals, @elseQuery, @params });
            }
            /// <summary>
            /// apoc.do.when(condition, ifQuery, elseQuery:&apos;&apos;, params:{}) yield value - based on the conditional, executes writing ifQuery or elseQuery with the given parameters
            /// </summary>
            public static MiscJaggedListResult When(BooleanResult @condition, StringResult @elseQuery, StringResult @ifQuery)
            {
                return new MiscJaggedListResult(t => t.CallApocDoWhen(3), new object[] { @condition, @elseQuery, @ifQuery });
            }
            /// <summary>
            /// apoc.do.when(condition, ifQuery, elseQuery:&apos;&apos;, params:{}) yield value - based on the conditional, executes writing ifQuery or elseQuery with the given parameters
            /// </summary>
            public static MiscJaggedListResult When(BooleanResult @condition, StringResult @elseQuery, StringResult @ifQuery, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.CallApocDoWhen(4), new object[] { @condition, @elseQuery, @ifQuery, @params });
            }
        }

        public static partial class Dv
        {
            /// <summary>
            /// Query a virtualized resource by name and return virtual nodes linked using virtual rels to the node passed as first param
            /// </summary>
            public static MiscJaggedListResult Queryandlink(MiscResult @config, StringResult @name, AliasResult @node, MiscResult @params, StringResult @relName)
            {
                return new MiscJaggedListResult(t => t.CallApocDvQueryandlink, new object[] { @config, @name, @node, @params, @relName });
            }
            /// <summary>
            /// Query a virtualized resource by name and return virtual nodes
            /// </summary>
            public static MiscJaggedListResult Query(MiscResult @config, StringResult @name)
            {
                return new MiscJaggedListResult(t => t.CallApocDvQuery(2), new object[] { @config, @name });
            }
            /// <summary>
            /// Query a virtualized resource by name and return virtual nodes
            /// </summary>
            public static MiscJaggedListResult Query(MiscResult @config, StringResult @name, MiscResult @params)
            {
                return new MiscJaggedListResult(t => t.CallApocDvQuery(3), new object[] { @config, @name, @params });
            }
        }

        public static partial class DvCatalog
        {
            /// <summary>
            /// Add a virtualized resource configuration
            /// </summary>
            public static MiscJaggedListResult Add(MiscResult @config, StringResult @name)
            {
                return new MiscJaggedListResult(t => t.CallApocDvCatalogAdd, new object[] { @config, @name });
            }
            /// <summary>
            /// List all virtualized resource configs
            /// </summary>
            public static MiscJaggedListResult List()
            {
                return new MiscJaggedListResult(t => t.CallApocDvCatalogList);
            }
            /// <summary>
            /// Remove a virtualized resource config by name
            /// </summary>
            public static MiscJaggedListResult Remove(StringResult @name)
            {
                return new MiscJaggedListResult(t => t.CallApocDvCatalogRemove, new object[] { @name });
            }
        }

        public static partial class Example
        {
            /// <summary>
            /// apoc.example.movies() | Creates the sample movies graph
            /// </summary>
            public static MiscJaggedListResult Movies()
            {
                return new MiscJaggedListResult(t => t.CallApocExampleMovies);
            }
        }

        public static partial class Export
        {
            /// <summary>
            /// apoc.export.cypherAll(file,config) - exports whole database incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Cypherall(MiscResult @config, StringResult @file)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherall, new object[] { @config, @file });
            }
            /// <summary>
            /// apoc.export.cypherData(nodes,rels,file,config) - exports given nodes and relationships incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Cypherdata(MiscResult @config, StringResult @file, AliasListResult @nodes, AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherdata, new object[] { @config, @file, @nodes, @rels });
            }
            /// <summary>
            /// apoc.export.cypherGraph(graph,file,config) - exports given graph object incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Cyphergraph(MiscResult @config, StringResult @file, MiscResult @graph)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCyphergraph, new object[] { @config, @file, @graph });
            }
            /// <summary>
            /// apoc.export.cypherQuery(query,file,config) - exports nodes and relationships from the cypher kernelTransaction incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Cypherquery(MiscResult @config, StringResult @file, StringResult @query)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherquery, new object[] { @config, @file, @query });
            }
        }

        public static partial class ExportCsv
        {
            /// <summary>
            /// apoc.export.csv.all(file,config) - exports whole database as csv to the provided file
            /// </summary>
            public static MiscJaggedListResult All(MiscResult @config, StringResult @file)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCsvAll, new object[] { @config, @file });
            }
            /// <summary>
            /// apoc.export.csv.data(nodes,rels,file,config) - exports given nodes and relationships as csv to the provided file
            /// </summary>
            public static MiscJaggedListResult Data(MiscResult @config, StringResult @file, AliasListResult @nodes, AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCsvData, new object[] { @config, @file, @nodes, @rels });
            }
            /// <summary>
            /// apoc.export.csv.graph(graph,file,config) - exports given graph object as csv to the provided file
            /// </summary>
            public static MiscJaggedListResult Graph(MiscResult @config, StringResult @file, MiscResult @graph)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCsvGraph, new object[] { @config, @file, @graph });
            }
            /// <summary>
            /// apoc.export.csv.query(query,file,{config,…​,params:{params}}) - exports results from the cypher statement as csv to the provided file
            /// </summary>
            public static MiscJaggedListResult Query(MiscResult @config, StringResult @file, StringResult @query)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCsvQuery, new object[] { @config, @file, @query });
            }
        }

        public static partial class ExportCypher
        {
            /// <summary>
            /// apoc.export.cypher.schema(file,config) - exports all schema indexes and constraints to cypher
            /// </summary>
            public static MiscJaggedListResult Schema(MiscResult @config, StringResult @file)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherSchema, new object[] { @config, @file });
            }
            /// <summary>
            /// apoc.export.cypher.all(file,config) - exports whole database incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult All(MiscResult @config, StringResult @file)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherAll, new object[] { @config, @file });
            }
            /// <summary>
            /// apoc.export.cypher.data(nodes,rels,file,config) - exports given nodes and relationships incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Data(MiscResult @config, StringResult @file, AliasListResult @nodes, AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherData, new object[] { @config, @file, @nodes, @rels });
            }
            /// <summary>
            /// apoc.export.cypher.graph(graph,file,config) - exports given graph object incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Graph(MiscResult @config, StringResult @file, MiscResult @graph)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherGraph, new object[] { @config, @file, @graph });
            }
            /// <summary>
            /// apoc.export.cypher.query(query,file,config) - exports nodes and relationships from the cypher statement incl. indexes as cypher statements to the provided file
            /// </summary>
            public static MiscJaggedListResult Query(MiscResult @config, StringResult @file, StringResult @query)
            {
                return new MiscJaggedListResult(t => t.CallApocExportCypherQuery, new object[] { @config, @file, @query });
            }
        }

        public static partial class ExportGraphml
        {
            /// <summary>
            /// apoc.export.graphml.all(file,config) - exports whole database as graphml to the provided file
            /// </summary>
            public static MiscJaggedListResult All(MiscResult @config, StringResult @file)
            {
                return new MiscJaggedListResult(t => t.CallApocExportGraphmlAll, new object[] { @config, @file });
            }
            /// <summary>
            /// apoc.export.graphml.data(nodes,rels,file,config) - exports given nodes and relationships as graphml to the provided file
            /// </summary>
            public static MiscJaggedListResult Data(MiscResult @config, StringResult @file, AliasListResult @nodes, AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocExportGraphmlData, new object[] { @config, @file, @nodes, @rels });
            }
            /// <summary>
            /// apoc.export.graphml.graph(graph,file,config) - exports given graph object as graphml to the provided file
            /// </summary>
            public static MiscJaggedListResult Graph(MiscResult @config, StringResult @file, MiscResult @graph)
            {
                return new MiscJaggedListResult(t => t.CallApocExportGraphmlGraph, new object[] { @config, @file, @graph });
            }
            /// <summary>
            /// apoc.export.graphml.query(query,file,config) - exports nodes and relationships from the cypher statement as graphml to the provided file
            /// </summary>
            public static MiscJaggedListResult Query(MiscResult @config, StringResult @file, StringResult @query)
            {
                return new MiscJaggedListResult(t => t.CallApocExportGraphmlQuery, new object[] { @config, @file, @query });
            }
        }

        public static partial class ExportJson
        {
            /// <summary>
            /// apoc.export.json.all(file,config) - exports whole database as json to the provided file
            /// </summary>
            public static MiscJaggedListResult All(MiscResult @config, StringResult @file)
            {
                return new MiscJaggedListResult(t => t.CallApocExportJsonAll, new object[] { @config, @file });
            }
            /// <summary>
            /// apoc.export.json.data(nodes,rels,file,config) - exports given nodes and relationships as json to the provided file
            /// </summary>
            public static MiscJaggedListResult Data(MiscResult @config, StringResult @file, AliasListResult @nodes, AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocExportJsonData, new object[] { @config, @file, @nodes, @rels });
            }
            /// <summary>
            /// apoc.export.json.graph(graph,file,config) - exports given graph object as json to the provided file
            /// </summary>
            public static MiscJaggedListResult Graph(MiscResult @config, StringResult @file, MiscResult @graph)
            {
                return new MiscJaggedListResult(t => t.CallApocExportJsonGraph, new object[] { @config, @file, @graph });
            }
            /// <summary>
            /// apoc.export.json.query(query,file,{config,…​,params:{params}}) - exports results from the cypher statement as json to the provided file
            /// </summary>
            public static MiscJaggedListResult Query(MiscResult @config, StringResult @file, StringResult @query)
            {
                return new MiscJaggedListResult(t => t.CallApocExportJsonQuery, new object[] { @config, @file, @query });
            }
        }

        public static partial class Graph
        {
            /// <summary>
            /// apoc.graph.from(data,&apos;name&apos;,{properties}) | creates a virtual graph object for later processing it tries its best to extract the graph information from the data you pass in
            /// </summary>
            public static MiscJaggedListResult From(MiscResult @data, StringResult @name, MiscResult @properties)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFrom, new object[] { @data, @name, @properties });
            }
            /// <summary>
            /// apoc.graph.fromCypher(&apos;kernelTransaction&apos;,{params},&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscJaggedListResult Fromcypher(StringResult @kernelTransaction, StringResult @name, MiscResult @params, MiscResult @properties)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFromcypher, new object[] { @kernelTransaction, @name, @params, @properties });
            }
            /// <summary>
            /// apoc.graph.fromData([nodes],[relationships],&apos;name&apos;,{properties}) | creates a virtual graph object for later processing
            /// </summary>
            public static MiscJaggedListResult Fromdata(StringResult @name, AliasListResult @nodes, MiscResult @properties, AliasListResult @relationships)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFromdata, new object[] { @name, @nodes, @properties, @relationships });
            }
            /// <summary>
            /// apoc.graph.fromDB(&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscJaggedListResult Fromdb(StringResult @name, MiscResult @properties)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFromdb, new object[] { @name, @properties });
            }
            /// <summary>
            /// apoc.graph.fromDocument({json}, {config}) yield graph - transform JSON documents into graph structures
            /// </summary>
            public static MiscJaggedListResult Fromdocument(MiscResult @config, MiscResult @json)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFromdocument, new object[] { @config, @json });
            }
            /// <summary>
            /// apoc.graph.fromPath(path,&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscJaggedListResult Frompath(StringResult @name, PathResult @path, MiscResult @properties)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFrompath, new object[] { @name, @path, @properties });
            }
            /// <summary>
            /// apoc.graph.fromPaths([paths],&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscJaggedListResult Frompaths(StringResult @name, PathListResult @paths, MiscResult @properties)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphFrompaths, new object[] { @name, @paths, @properties });
            }
            /// <summary>
            /// apoc.graph.validateDocument({json}, {config}) yield row - validates the json, return the result of the validation
            /// </summary>
            public static MiscJaggedListResult Validatedocument(MiscResult @config, MiscResult @json)
            {
                return new MiscJaggedListResult(t => t.CallApocGraphValidatedocument, new object[] { @config, @json });
            }
        }

        public static partial class Hashing
        {
            /// <summary>
            /// calculate a checksum (md5) over a node or a relationship. This deals gracefully with array properties. Two identical entities do share the same hash. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static MiscJaggedListResult Fingerprint(StringListResult @propertyExcludes, MiscResult @someObject)
            {
                return new MiscJaggedListResult(t => t.FnApocHashingFingerprint, new object[] { @propertyExcludes, @someObject });
            }
            /// <summary>
            /// calculate a checksum (md5) over a the full graph. Be aware that this function does use in-memomry datastructures depending on the size of your graph. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static MiscJaggedListResult Fingerprintgraph()
            {
                return new MiscJaggedListResult(t => t.FnApocHashingFingerprintgraph(0), new object[] {  });
            }
            /// <summary>
            /// calculate a checksum (md5) over a the full graph. Be aware that this function does use in-memomry datastructures depending on the size of your graph. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static MiscJaggedListResult Fingerprintgraph(StringListResult @propertyExcludes)
            {
                return new MiscJaggedListResult(t => t.FnApocHashingFingerprintgraph(1), new object[] { @propertyExcludes });
            }
            /// <summary>
            /// calculate a checksum (md5) over a node or a relationship. This deals gracefully with array properties. Two identical entities do share the same hash. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static MiscJaggedListResult Fingerprinting(MiscResult @conf, MiscResult @someObject)
            {
                return new MiscJaggedListResult(t => t.FnApocHashingFingerprinting, new object[] { @conf, @someObject });
            }
        }

        public static partial class Import
        {
            /// <summary>
            /// apoc.import.csv(nodes, relationships, config) - imports nodes and relationships from the provided CSV files with given labels and types
            /// </summary>
            public static MiscJaggedListResult Csv(MiscResult @config, MiscListResult @nodes, MiscListResult @relationships)
            {
                return new MiscJaggedListResult(t => t.CallApocImportCsv, new object[] { @config, @nodes, @relationships });
            }
            /// <summary>
            /// apoc.import.graphml(urlOrBinaryFile,config) - imports graphml file
            /// </summary>
            public static MiscJaggedListResult Graphml(MiscResult @config, MiscResult @urlOrBinaryFile)
            {
                return new MiscJaggedListResult(t => t.CallApocImportGraphml, new object[] { @config, @urlOrBinaryFile });
            }
            /// <summary>
            /// apoc.import.json(urlOrBinaryFile,config) - imports the json list to the provided file
            /// </summary>
            public static MiscJaggedListResult Json(MiscResult @config, MiscResult @urlOrBinaryFile)
            {
                return new MiscJaggedListResult(t => t.CallApocImportJson, new object[] { @config, @urlOrBinaryFile });
            }
            /// <summary>
            /// apoc.import.xml(file,config) - imports graph from provided file
            /// </summary>
            public static MiscJaggedListResult Xml(MiscResult @config, MiscResult @urlOrBinary)
            {
                return new MiscJaggedListResult(t => t.CallApocImportXml, new object[] { @config, @urlOrBinary });
            }
        }

        public static partial class Json
        {
            /// <summary>
            /// apoc.json.path(&apos;{json}&apos; [,&apos;json-path&apos; , &apos;path-options&apos;])
            /// </summary>
            public static MiscJaggedListResult Path(StringResult @json)
            {
                return new MiscJaggedListResult(t => t.FnApocJsonPath(1), new object[] { @json });
            }
            // HAS ALTERNATE TYPE
            /// <summary>
            /// apoc.json.path(&apos;{json}&apos; [,&apos;json-path&apos; , &apos;path-options&apos;])
            /// </summary>
            public static MiscJaggedListResult Path(StringResult @json, StringResult @path)
            {
                return new MiscJaggedListResult(t => t.FnApocJsonPath(2), new object[] { @json, @path });
            }
            // HAS ALTERNATE TYPE
            /// <summary>
            /// apoc.json.path(&apos;{json}&apos; [,&apos;json-path&apos; , &apos;path-options&apos;])
            /// </summary>
            public static MiscJaggedListResult Path(StringResult @json, StringResult @path, PathOptions @pathOptions)
            {
                return new MiscJaggedListResult(t => t.FnApocJsonPath(3), new object[] { @json, @path, @pathOptions });
            }
        }

        public static partial class Label
        {
            /// <summary>
            /// apoc.label.exists(element, label) - returns true or false related to label existance
            /// </summary>
            public static MiscJaggedListResult Exists(StringResult @label, MiscResult @node)
            {
                return new MiscJaggedListResult(t => t.FnApocLabelExists, new object[] { @label, @node });
            }
        }

        public static partial class Load
        {
            /// <summary>
            /// apoc.load.jsonArray(&apos;url&apos;) YIELD value - load array from JSON URL (e.g. web-api) to import JSON as stream of values
            /// </summary>
            public static MiscJaggedListResult Jsonarray(MiscResult @config, StringResult @path, StringResult @url)
            {
                return new MiscJaggedListResult(t => t.CallApocLoadJsonarray, new object[] { @config, @path, @url });
            }
            /// <summary>
            /// apoc.load.jsonParams(&apos;urlOrKeyOrBinary&apos;,{header:value},payload, config) YIELD value - load from JSON URL (e.g. web-api) while sending headers / payload to import JSON as stream of values if the JSON was an array or a single value if it was a map
            /// </summary>
            public static MiscJaggedListResult Jsonparams(MiscResult @config, MiscResult @headers, StringResult @path, StringResult @payload, MiscResult @urlOrKeyOrBinary)
            {
                return new MiscJaggedListResult(t => t.CallApocLoadJsonparams, new object[] { @config, @headers, @path, @payload, @urlOrKeyOrBinary });
            }
            /// <summary>
            /// apoc.load.json(&apos;urlOrKeyOrBinary&apos;,path, config) YIELD value - import JSON as stream of values if the JSON was an array or a single value if it was a map
            /// </summary>
            public static MiscJaggedListResult Json(MiscResult @config, StringResult @path, MiscResult @urlOrKeyOrBinary)
            {
                return new MiscJaggedListResult(t => t.CallApocLoadJson, new object[] { @config, @path, @urlOrKeyOrBinary });
            }
            /// <summary>
            /// apoc.load.xml(&apos;http://example.com/test.xml&apos;, &apos;xPath&apos;,config, false) YIELD value as doc CREATE (p:Person) SET p.name = doc.name - load from XML URL (e.g. web-api) to import XML as single nested map with attributes and _type, _text and _childrenx fields.
            /// </summary>
            public static MiscJaggedListResult Xml(MiscResult @config, StringResult @path, BooleanResult @simple, MiscResult @urlOrBinary)
            {
                return new MiscJaggedListResult(t => t.CallApocLoadXml, new object[] { @config, @path, @simple, @urlOrBinary });
            }
        }

        public static partial class Lock
        {
            /// <summary>
            /// apoc.lock.all([nodes],[relationships]) acquires a write lock on the given nodes and relationships
            /// </summary>
            public static MiscJaggedListResult All(AliasListResult @nodes, AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocLockAll, new object[] { @nodes, @rels });
            }
            /// <summary>
            /// apoc.lock.nodes([nodes]) acquires a write lock on the given nodes
            /// </summary>
            public static MiscJaggedListResult Nodes(AliasListResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocLockNodes, new object[] { @nodes });
            }
            /// <summary>
            /// apoc.lock.rels([relationships]) acquires a write lock on the given relationship
            /// </summary>
            public static MiscJaggedListResult Rels(AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocLockRels, new object[] { @rels });
            }
        }

        public static partial class LockRead
        {
            /// <summary>
            /// apoc.lock.read.nodes([nodes]) acquires a read lock on the given nodes
            /// </summary>
            public static MiscJaggedListResult Nodes(AliasListResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocLockReadNodes, new object[] { @nodes });
            }
            /// <summary>
            /// apoc.lock.read.rels([relationships]) acquires a read lock on the given relationship
            /// </summary>
            public static MiscJaggedListResult Rels(AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocLockReadRels, new object[] { @rels });
            }
        }

        public static partial class Log
        {
            /// <summary>
            /// apoc.log.stream(&apos;neo4j.log&apos;, { last: n }) - retrieve log file contents, optionally return only the last n lines
            /// </summary>
            public static MiscJaggedListResult Stream(MiscResult @config, StringResult @path)
            {
                return new MiscJaggedListResult(t => t.CallApocLogStream, new object[] { @config, @path });
            }
        }

        public static partial class Map
        {
            /// <summary>
            /// apoc.map.clean(map,[skip,keys],[skip,values]) yield map filters the keys and values contained in those lists, good for data cleaning from CSV/JSON
            /// </summary>
            public static MiscJaggedListResult Clean(StringListResult @keys, MiscResult @map, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocMapClean, new object[] { @keys, @map, @values });
            }
            /// <summary>
            /// apoc.map.fromLists([keys],[values])
            /// </summary>
            public static MiscJaggedListResult Fromlists(StringListResult @keys, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocMapFromlists, new object[] { @keys, @values });
            }
            /// <summary>
            /// apoc.map.fromNodes(label, property)
            /// </summary>
            public static MiscJaggedListResult Fromnodes(StringResult @label, StringResult @property)
            {
                return new MiscJaggedListResult(t => t.FnApocMapFromnodes, new object[] { @label, @property });
            }
            /// <summary>
            /// apoc.map.fromPairs([[key,value],[key2,value2],…​])
            /// </summary>
            public static MiscJaggedListResult Frompairs(MiscJaggedListResult @pairs)
            {
                return new MiscJaggedListResult(t => t.FnApocMapFrompairs, new object[] { @pairs });
            }
            /// <summary>
            /// apoc.map.fromValues([key1,value1,key2,value2,…​])
            /// </summary>
            public static MiscJaggedListResult Fromvalues(params MiscListResult[] @values)
            {
                return new MiscJaggedListResult(t => t.FnApocMapFromvalues(@values.Length), @values);
            }
            /// <summary>
            /// apoc.map.groupBy([maps/nodes/relationships],&apos;key&apos;) yield value - creates a map of the list keyed by the given property, with single values
            /// </summary>
            public static MiscJaggedListResult Groupby(StringResult @key, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocMapGroupby, new object[] { @key, @values });
            }
            /// <summary>
            /// apoc.map.groupByMulti([maps/nodes/relationships],&apos;key&apos;) yield value - creates a map of the list keyed by the given property, with list values
            /// </summary>
            public static MiscJaggedListResult Groupbymulti(StringResult @key, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocMapGroupbymulti, new object[] { @key, @values });
            }
            /// <summary>
            /// apoc.map.merge(first,second) - merges two maps
            /// </summary>
            public static MiscJaggedListResult Merge(MiscResult @first, MiscResult @second)
            {
                return new MiscJaggedListResult(t => t.FnApocMapMerge, new object[] { @first, @second });
            }
            /// <summary>
            /// apoc.map.mergeList([{maps}]) yield value - merges all maps in the list into one
            /// </summary>
            public static MiscJaggedListResult Mergelist(MiscListResult @maps)
            {
                return new MiscJaggedListResult(t => t.FnApocMapMergelist, new object[] { @maps });
            }
            /// <summary>
            /// apoc.map.mget(map,key,[defaults],[fail=true])  - returns list of values for keys or throws exception if one of the key doesn’t exist and no default value given at that position
            /// </summary>
            public static MiscJaggedListResult Mget(BooleanResult @fail, StringListResult @keys, MiscResult @map)
            {
                return new MiscJaggedListResult(t => t.FnApocMapMget(3), new object[] { @fail, @keys, @map });
            }
            /// <summary>
            /// apoc.map.mget(map,key,[defaults],[fail=true])  - returns list of values for keys or throws exception if one of the key doesn’t exist and no default value given at that position
            /// </summary>
            public static MiscJaggedListResult Mget(BooleanResult @fail, StringListResult @keys, MiscResult @map, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocMapMget(4), new object[] { @fail, @keys, @map, @values });
            }
            /// <summary>
            /// apoc.map.removeKey(map,key,{recursive:true/false}) - remove the key from the map (recursively if recursive is true)
            /// </summary>
            public static MiscJaggedListResult Removekey(MiscResult @config, StringResult @key, MiscResult @map)
            {
                return new MiscJaggedListResult(t => t.FnApocMapRemovekey, new object[] { @config, @key, @map });
            }
            /// <summary>
            /// apoc.map.removeKeys(map,[keys],{recursive:true/false}) - remove the keys from the map (recursively if recursive is true)
            /// </summary>
            public static MiscJaggedListResult Removekeys(MiscResult @config, StringListResult @keys, MiscResult @map)
            {
                return new MiscJaggedListResult(t => t.FnApocMapRemovekeys, new object[] { @config, @keys, @map });
            }
            /// <summary>
            /// apoc.map.setEntry(map,key,value)
            /// </summary>
            public static MiscJaggedListResult Setentry(StringResult @key, MiscResult @map, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSetentry, new object[] { @key, @map, @value });
            }
            /// <summary>
            /// apoc.map.setKey(map,key,value)
            /// </summary>
            public static MiscJaggedListResult Setkey(StringResult @key, MiscResult @map, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSetkey, new object[] { @key, @map, @value });
            }
            /// <summary>
            /// apoc.map.setLists(map,[keys],[values])
            /// </summary>
            public static MiscJaggedListResult Setlists(StringListResult @keys, MiscResult @map, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSetlists, new object[] { @keys, @map, @values });
            }
            /// <summary>
            /// apoc.map.setPairs(map,[[key1,value1],[key2,value2])
            /// </summary>
            public static MiscJaggedListResult Setpairs(MiscResult @map, MiscJaggedListResult @pairs)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSetpairs, new object[] { @map, @pairs });
            }
            /// <summary>
            /// apoc.map.setValues(map,[key1,value1,key2,value2])
            /// </summary>
            public static MiscJaggedListResult Setvalues(MiscResult @map, MiscListResult @pairs)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSetvalues, new object[] { @map, @pairs });
            }
            /// <summary>
            /// apoc.map.sortedProperties(map, ignoreCase:true) - returns a list of key/value list pairs, with pairs sorted by keys alphabetically, with optional case sensitivity
            /// </summary>
            public static MiscJaggedListResult Sortedproperties(BooleanResult @ignoreCase, MiscResult @map)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSortedproperties, new object[] { @ignoreCase, @map });
            }
            /// <summary>
            /// apoc.map.submap(map,keys,[defaults],[fail=true])  - returns submap for keys or throws exception if one of the key doesn’t exist and no default value given at that position
            /// </summary>
            public static MiscJaggedListResult Submap(BooleanResult @fail, StringListResult @keys, MiscResult @map)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSubmap(3), new object[] { @fail, @keys, @map });
            }
            /// <summary>
            /// apoc.map.submap(map,keys,[defaults],[fail=true])  - returns submap for keys or throws exception if one of the key doesn’t exist and no default value given at that position
            /// </summary>
            public static MiscJaggedListResult Submap(BooleanResult @fail, StringListResult @keys, MiscResult @map, MiscListResult @values)
            {
                return new MiscJaggedListResult(t => t.FnApocMapSubmap(4), new object[] { @fail, @keys, @map, @values });
            }
            /// <summary>
            /// apoc.map.unflatten(map, delimiter:&apos;.&apos;) yield map - unflat from items separated by delimiter string to nested items (reverse of apoc.map.flatten function)
            /// </summary>
            public static MiscJaggedListResult Unflatten(StringResult @delimiter, MiscResult @map)
            {
                return new MiscJaggedListResult(t => t.FnApocMapUnflatten, new object[] { @delimiter, @map });
            }
            /// <summary>
            /// apoc.map.updateTree(tree,key,) returns map - adds the {data} map on each level of the nested tree, where the key-value pairs match
            /// </summary>
            public static MiscJaggedListResult Updatetree(MiscJaggedListResult @data, StringResult @key, MiscResult @tree)
            {
                return new MiscJaggedListResult(t => t.FnApocMapUpdatetree, new object[] { @data, @key, @tree });
            }
            /// <summary>
            /// apoc.map.values(map, [key1,key2,key3,…​],[addNullsForMissing]) returns list of values indicated by the keys
            /// </summary>
            public static MiscJaggedListResult Values(BooleanResult @addNullsForMissing, StringListResult @keys, MiscResult @map)
            {
                return new MiscJaggedListResult(t => t.FnApocMapValues, new object[] { @addNullsForMissing, @keys, @map });
            }
            /// <summary>
            /// apoc.map.flatten(map, delimiter:&apos;.&apos;) yield map - flattens nested items in map using dot notation
            /// </summary>
            public static MiscJaggedListResult Flatten(StringResult @delimiter, MiscResult @map)
            {
                return new MiscJaggedListResult(t => t.FnApocMapFlatten, new object[] { @delimiter, @map });
            }
            /// <summary>
            /// apoc.map.get(map,key,[default],[fail=true]) - returns value for key or throws exception if key doesn’t exist and no default given
            /// </summary>
            public static MiscJaggedListResult Get(BooleanResult @fail, StringResult @key, MiscResult @map, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMapGet, new object[] { @fail, @key, @map, @value });
            }
        }

        public static partial class Math
        {
            /// <summary>
            /// apoc.math.cosh(val) | returns the hyperbolic cosin
            /// </summary>
            public static MiscJaggedListResult Cosh(FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMathCosh, new object[] { @value });
            }
            /// <summary>
            /// apoc.math.coth(val) | returns the hyperbolic cotangent
            /// </summary>
            public static MiscJaggedListResult Coth(FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMathCoth, new object[] { @value });
            }
            /// <summary>
            /// apoc.math.csch(val) | returns the hyperbolic cosecant
            /// </summary>
            public static MiscJaggedListResult Csch(FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMathCsch, new object[] { @value });
            }
            /// <summary>
            /// apoc.math.maxByte() | return the maximum value an byte can have
            /// </summary>
            public static MiscJaggedListResult Maxbyte()
            {
                return new MiscJaggedListResult(t => t.FnApocMathMaxbyte);
            }
            /// <summary>
            /// apoc.math.maxDouble() | return the largest positive finite value of type double
            /// </summary>
            public static MiscJaggedListResult Maxdouble()
            {
                return new MiscJaggedListResult(t => t.FnApocMathMaxdouble);
            }
            /// <summary>
            /// apoc.math.maxInt() | return the maximum value an int can have
            /// </summary>
            public static MiscJaggedListResult Maxint()
            {
                return new MiscJaggedListResult(t => t.FnApocMathMaxint);
            }
            /// <summary>
            /// apoc.math.maxLong() | return the maximum value a long can have
            /// </summary>
            public static MiscJaggedListResult Maxlong()
            {
                return new MiscJaggedListResult(t => t.FnApocMathMaxlong);
            }
            /// <summary>
            /// apoc.math.minByte() | return the minimum value an byte can have
            /// </summary>
            public static MiscJaggedListResult Minbyte()
            {
                return new MiscJaggedListResult(t => t.FnApocMathMinbyte);
            }
            /// <summary>
            /// apoc.math.minDouble() | return the smallest positive nonzero value of type double
            /// </summary>
            public static MiscJaggedListResult Mindouble()
            {
                return new MiscJaggedListResult(t => t.FnApocMathMindouble);
            }
            /// <summary>
            /// apoc.math.minInt() | return the minimum value an int can have
            /// </summary>
            public static MiscJaggedListResult Minint()
            {
                return new MiscJaggedListResult(t => t.FnApocMathMinint);
            }
            /// <summary>
            /// apoc.math.minLong() | return the minimum value a long can have
            /// </summary>
            public static MiscJaggedListResult Minlong()
            {
                return new MiscJaggedListResult(t => t.FnApocMathMinlong);
            }
            /// <summary>
            /// apoc.math.regr(label, propertyY, propertyX) - It calculates the coefficient of determination (R-squared) for the values of propertyY and propertyX in the provided label
            /// </summary>
            public static MiscJaggedListResult Regr(StringResult @label, StringResult @propertyX, StringResult @propertyY)
            {
                return new MiscJaggedListResult(t => t.CallApocMathRegr, new object[] { @label, @propertyX, @propertyY });
            }
            /// <summary>
            /// apoc.math.round(value,[prec],mode=[CEILING,FLOOR,UP,DOWN,HALF_EVEN,HALF_DOWN,HALF_UP,DOWN,UNNECESSARY])
            /// </summary>
            public static MiscJaggedListResult Round(StringResult @mode, NumericResult @precision, FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMathRound, new object[] { @mode, @precision, @value });
            }
            /// <summary>
            /// apoc.math.sech(val) | returns the hyperbolic secant
            /// </summary>
            public static MiscJaggedListResult Sech(FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMathSech, new object[] { @value });
            }
            /// <summary>
            /// apoc.math.sigmoid(val) | returns the sigmoid value
            /// </summary>
            public static MiscJaggedListResult Sigmoid(FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMathSigmoid, new object[] { @value });
            }
            /// <summary>
            /// apoc.math.sigmoidPrime(val) | returns the sigmoid prime [ sigmoid(val) * (1 - sigmoid(val)) ]
            /// </summary>
            public static MiscJaggedListResult Sigmoidprime(FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMathSigmoidprime, new object[] { @value });
            }
            /// <summary>
            /// apoc.math.sinh(val) | returns the hyperbolic sin
            /// </summary>
            public static MiscJaggedListResult Sinh(FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMathSinh, new object[] { @value });
            }
            /// <summary>
            /// apoc.math.tanh(val) | returns the hyperbolic tangent
            /// </summary>
            public static MiscJaggedListResult Tanh(FloatResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMathTanh, new object[] { @value });
            }
        }

        public static partial class Merge
        {
            /// <summary>
            /// &quot;apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Node(MiscResult @identProps, StringListResult @label)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeNode(2), new object[] { @identProps, @label });
            }
            /// <summary>
            /// &quot;apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Node(MiscResult @identProps, StringListResult @label, MiscResult @onMatchProps)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeNode(3), new object[] { @identProps, @label, @onMatchProps });
            }
            /// <summary>
            /// &quot;apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Node(MiscResult @identProps, StringListResult @label, MiscResult @onMatchProps, MiscResult @props)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeNode(4), new object[] { @identProps, @label, @onMatchProps, @props });
            }
            /// <summary>
            /// apoc.merge.relationship(startNode, relType,  identProps:{key:value, …​}, onCreateProps:{key:value, …​}, endNode, onMatchProps:{key:value, …​}) - merge relationship with dynamic type, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Relationship(AliasResult @endNode, MiscResult @identProps, MiscResult @onMatchProps, MiscResult @props, StringResult @relationshipType, AliasResult @startNode)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeRelationship, new object[] { @endNode, @identProps, @onMatchProps, @props, @relationshipType, @startNode });
            }
        }

        public static partial class MergeNode
        {
            /// <summary>
            /// apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes eagerly, with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Eager(MiscResult @identProps, StringListResult @label)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeNodeEager(2), new object[] { @identProps, @label });
            }
            /// <summary>
            /// apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes eagerly, with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Eager(MiscResult @identProps, StringListResult @label, MiscResult @onMatchProps)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeNodeEager(3), new object[] { @identProps, @label, @onMatchProps });
            }
            /// <summary>
            /// apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes eagerly, with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Eager(MiscResult @identProps, StringListResult @label, MiscResult @onMatchProps, MiscResult @props)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeNodeEager(4), new object[] { @identProps, @label, @onMatchProps, @props });
            }
        }

        public static partial class MergeRelationship
        {
            /// <summary>
            /// apoc.merge.relationship(startNode, relType,  identProps:{key:value, …​}, onCreateProps:{key:value, …​}, endNode, onMatchProps:{key:value, …​}) - merge relationship with dynamic type, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static MiscJaggedListResult Eager(AliasResult @endNode, MiscResult @identProps, MiscResult @onMatchProps, MiscResult @props, StringResult @relationshipType, AliasResult @startNode)
            {
                return new MiscJaggedListResult(t => t.CallApocMergeRelationshipEager, new object[] { @endNode, @identProps, @onMatchProps, @props, @relationshipType, @startNode });
            }
        }

        public static partial class Meta
        {
            /// <summary>
            /// apoc.meta.graphSample() - examines the database statistics to build the meta graph, very fast, might report extra relationships
            /// </summary>
            public static MiscJaggedListResult Graphsample()
            {
                return new MiscJaggedListResult(t => t.CallApocMetaGraphsample(0), new object[] {  });
            }
            /// <summary>
            /// apoc.meta.graphSample() - examines the database statistics to build the meta graph, very fast, might report extra relationships
            /// </summary>
            public static MiscJaggedListResult Graphsample(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaGraphsample(1), new object[] { @config });
            }
            /// <summary>
            /// apoc.meta.nodeTypeProperties()
            /// </summary>
            public static MiscJaggedListResult Nodetypeproperties()
            {
                return new MiscJaggedListResult(t => t.CallApocMetaNodetypeproperties(0), new object[] {  });
            }
            /// <summary>
            /// apoc.meta.nodeTypeProperties()
            /// </summary>
            public static MiscJaggedListResult Nodetypeproperties(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaNodetypeproperties(1), new object[] { @config });
            }
            /// <summary>
            /// apoc.meta.relTypeProperties()
            /// </summary>
            public static MiscJaggedListResult Reltypeproperties()
            {
                return new MiscJaggedListResult(t => t.CallApocMetaReltypeproperties(0), new object[] {  });
            }
            /// <summary>
            /// apoc.meta.relTypeProperties()
            /// </summary>
            public static MiscJaggedListResult Reltypeproperties(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaReltypeproperties(1), new object[] { @config });
            }
            /// <summary>
            /// apoc.meta.subGraph({labels:[labels],rels:[rel-types], excludes:[labels,rel-types]}) - examines a sample sub graph to create the meta-graph
            /// </summary>
            public static MiscJaggedListResult Subgraph(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaSubgraph, new object[] { @config });
            }
            /// <summary>
            /// apoc.meta.typeName(value) - type name of a value (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,UNKNOWN,MAP,LIST)
            /// </summary>
            public static MiscJaggedListResult Typename(MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaTypename, new object[] { @value });
            }
            /// <summary>
            /// apoc.meta.data({config})  - examines a subset of the graph to provide a tabular meta information
            /// </summary>
            public static MiscJaggedListResult Data()
            {
                return new MiscJaggedListResult(t => t.CallApocMetaData(0), new object[] {  });
            }
            /// <summary>
            /// apoc.meta.data({config})  - examines a subset of the graph to provide a tabular meta information
            /// </summary>
            public static MiscJaggedListResult Data(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaData(1), new object[] { @config });
            }
            /// <summary>
            /// apoc.meta.graph - examines the full graph to create the meta-graph
            /// </summary>
            public static MiscJaggedListResult Graph()
            {
                return new MiscJaggedListResult(t => t.CallApocMetaGraph(0), new object[] {  });
            }
            /// <summary>
            /// apoc.meta.graph - examines the full graph to create the meta-graph
            /// </summary>
            public static MiscJaggedListResult Graph(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaGraph(1), new object[] { @config });
            }
            /// <summary>
            /// apoc.meta.isType(value,type) - returns a row if type name matches none if not (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,UNKNOWN,MAP,LIST)
            /// </summary>
            public static MiscJaggedListResult Istype(StringResult @type, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaIstype, new object[] { @type, @value });
            }
            /// <summary>
            /// apoc.meta.schema({config})  - examines a subset of the graph to provide a map-like meta information
            /// </summary>
            public static MiscJaggedListResult Schema()
            {
                return new MiscJaggedListResult(t => t.CallApocMetaSchema(0), new object[] {  });
            }
            /// <summary>
            /// apoc.meta.schema({config})  - examines a subset of the graph to provide a map-like meta information
            /// </summary>
            public static MiscJaggedListResult Schema(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaSchema(1), new object[] { @config });
            }
            /// <summary>
            /// apoc.meta.stats yield labelCount, relTypeCount, propertyKeyCount, nodeCount, relCount, labels, relTypes, stats | returns the information stored in the transactional database statistics
            /// </summary>
            public static MiscJaggedListResult Stats()
            {
                return new MiscJaggedListResult(t => t.CallApocMetaStats);
            }
            /// <summary>
            /// apoc.meta.type(value) - type name of a value (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,UNKNOWN,MAP,LIST)
            /// </summary>
            public static MiscJaggedListResult Type(MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaType, new object[] { @value });
            }
            /// <summary>
            /// apoc.meta.types(node-relationship-map)  - returns a map of keys to types
            /// </summary>
            public static MiscJaggedListResult Types(MiscResult @properties)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaTypes, new object[] { @properties });
            }
        }

        public static partial class MetaCypher
        {
            /// <summary>
            /// apoc.meta.cypher.isType(value,type) - returns a row if type name matches none if not (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,MAP,LIST OF &lt;TYPE&gt;,POINT,DATE,DATE_TIME,LOCAL_TIME,LOCAL_DATE_TIME,TIME,DURATION)
            /// </summary>
            public static MiscJaggedListResult Istype(StringResult @type, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaCypherIstype, new object[] { @type, @value });
            }
            /// <summary>
            /// apoc.meta.cypher.type(value) - type name of a value (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,MAP,LIST OF &lt;TYPE&gt;,POINT,DATE,DATE_TIME,LOCAL_TIME,LOCAL_DATE_TIME,TIME,DURATION)
            /// </summary>
            public static MiscJaggedListResult Type(MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaCypherType, new object[] { @value });
            }
            /// <summary>
            /// apoc.meta.cypher.types(node-relationship-map)  - returns a map of keys to types
            /// </summary>
            public static MiscJaggedListResult Types(MiscResult @properties)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaCypherTypes, new object[] { @properties });
            }
        }

        public static partial class MetaData
        {
            /// <summary>
            /// apoc.meta.data.of({graph}, {config})  - examines a subset of the graph to provide a tabular meta information
            /// </summary>
            public static MiscJaggedListResult Of(MiscResult @config, MiscResult @graph)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaDataOf, new object[] { @config, @graph });
            }
        }

        public static partial class MetaGraph
        {
            /// <summary>
            /// apoc.meta.graph.of({graph}, {config})  - examines a subset of the graph to provide a graph meta information
            /// </summary>
            public static MiscJaggedListResult Of()
            {
                return new MiscJaggedListResult(t => t.CallApocMetaGraphOf(0), new object[] {  });
            }
            /// <summary>
            /// apoc.meta.graph.of({graph}, {config})  - examines a subset of the graph to provide a graph meta information
            /// </summary>
            public static MiscJaggedListResult Of(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaGraphOf(1), new object[] { @config });
            }
            /// <summary>
            /// apoc.meta.graph.of({graph}, {config})  - examines a subset of the graph to provide a graph meta information
            /// </summary>
            public static MiscJaggedListResult Of(MiscResult @config, MiscResult @graph)
            {
                return new MiscJaggedListResult(t => t.CallApocMetaGraphOf(2), new object[] { @config, @graph });
            }
        }

        public static partial class MetaNodes
        {
            /// <summary>
            /// apoc.meta.nodes.count([labels], $config) - Returns the sum of the nodes with a label present in the list.
            /// </summary>
            public static MiscJaggedListResult Count()
            {
                return new MiscJaggedListResult(t => t.FnApocMetaNodesCount(0), new object[] {  });
            }
            /// <summary>
            /// apoc.meta.nodes.count([labels], $config) - Returns the sum of the nodes with a label present in the list.
            /// </summary>
            public static MiscJaggedListResult Count(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaNodesCount(1), new object[] { @config });
            }
            /// <summary>
            /// apoc.meta.nodes.count([labels], $config) - Returns the sum of the nodes with a label present in the list.
            /// </summary>
            public static MiscJaggedListResult Count(MiscResult @config, StringListResult @nodes)
            {
                return new MiscJaggedListResult(t => t.FnApocMetaNodesCount(2), new object[] { @config, @nodes });
            }
        }

        public static partial class Neighbors
        {
            /// <summary>
            /// apoc.neighbors.athop(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at a distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Athop(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsAthop, new object[] { @distance, @node, @types });
            }
            /// <summary>
            /// apoc.neighbors.byhop(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at each distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Byhop(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsByhop, new object[] { @distance, @node, @types });
            }
            /// <summary>
            /// apoc.neighbors.tohop(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern up to a certain distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Tohop(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsTohop, new object[] { @distance, @node, @types });
            }
        }

        public static partial class NeighborsAthop
        {
            /// <summary>
            /// apoc.neighbors.athop.count(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at a distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Count(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsAthopCount, new object[] { @distance, @node, @types });
            }
        }

        public static partial class NeighborsByhop
        {
            /// <summary>
            /// apoc.neighbors.byhop.count(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at each distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Count(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsByhopCount, new object[] { @distance, @node, @types });
            }
        }

        public static partial class NeighborsTohop
        {
            /// <summary>
            /// apoc.neighbors.tohop.count(node, rel-direction-pattern, distance) - returns distinct count of nodes of the given relationships in the pattern up to a certain distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Count(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.CallApocNeighborsTohopCount, new object[] { @distance, @node, @types });
            }
        }

        public static partial class NlpAzureEntities
        {
            /// <summary>
            /// Creates a (virtual) entity graph for provided text
            /// </summary>
            public static MiscJaggedListResult Graph(MiscResult @config, MiscResult @source)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureEntitiesGraph, new object[] { @config, @source });
            }
            /// <summary>
            /// Provides a entity analysis for provided text
            /// </summary>
            public static MiscJaggedListResult Stream(MiscResult @config, MiscResult @source)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureEntitiesStream, new object[] { @config, @source });
            }
        }

        public static partial class NlpAzureKeyphrases
        {
            /// <summary>
            /// Creates a (virtual) key phrase graph for provided text
            /// </summary>
            public static MiscJaggedListResult Graph(MiscResult @config, MiscResult @source)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureKeyphrasesGraph, new object[] { @config, @source });
            }
            /// <summary>
            /// Provides a entity analysis for provided text
            /// </summary>
            public static MiscJaggedListResult Stream(MiscResult @config, MiscResult @source)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureKeyphrasesStream, new object[] { @config, @source });
            }
        }

        public static partial class NlpAzureSentiment
        {
            /// <summary>
            /// Creates a (virtual) sentiment graph for provided text
            /// </summary>
            public static MiscJaggedListResult Graph(MiscResult @config, MiscResult @source)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureSentimentGraph, new object[] { @config, @source });
            }
            /// <summary>
            /// Provides a sentiment analysis for provided text
            /// </summary>
            public static MiscJaggedListResult Stream(MiscResult @config, MiscResult @source)
            {
                return new MiscJaggedListResult(t => t.CallApocNlpAzureSentimentStream, new object[] { @config, @source });
            }
        }

        public static partial class Node
        {
            /// <summary>
            /// apoc.node.degree(node, rel-direction-pattern) - returns total degrees of the given relationships in the pattern, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscJaggedListResult Degree(AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeDegree, new object[] { @node, @types });
            }
            /// <summary>
            /// returns id for (virtual) nodes
            /// </summary>
            public static MiscJaggedListResult Id(AliasResult @node)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeId, new object[] { @node });
            }
            /// <summary>
            /// returns labels for (virtual) nodes
            /// </summary>
            public static MiscJaggedListResult Labels(AliasResult @node)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeLabels, new object[] { @node });
            }
        }

        public static partial class NodeDegree
        {
            /// <summary>
            /// apoc.node.degree.in(node, relationshipName) - returns total number number of incoming relationships
            /// </summary>
            public static MiscJaggedListResult In(AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeDegreeIn, new object[] { @node, @types });
            }
            /// <summary>
            /// apoc.node.degree.out(node, relationshipName) - returns total number number of outgoing relationships
            /// </summary>
            public static MiscJaggedListResult Out(AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeDegreeOut, new object[] { @node, @types });
            }
        }

        public static partial class NodeRelationship
        {
            /// <summary>
            /// apoc.node.relationship.exists(node, rel-direction-pattern) - returns true when the node has the relationships of the pattern
            /// </summary>
            public static MiscJaggedListResult Exists(AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeRelationshipExists, new object[] { @node, @types });
            }
            /// <summary>
            /// apoc.node.relationship.types(node, rel-direction-pattern) - returns a list of distinct relationship types
            /// </summary>
            public static MiscJaggedListResult Types(AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeRelationshipTypes, new object[] { @node, @types });
            }
        }

        public static partial class NodeRelationships
        {
            /// <summary>
            /// apoc.node.relationships.exist(node, rel-direction-pattern) - returns a map with rel-pattern, boolean for the given relationship patterns
            /// </summary>
            public static MiscJaggedListResult Exist(AliasResult @node, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.FnApocNodeRelationshipsExist, new object[] { @node, @types });
            }
        }

        public static partial class Nodes
        {
            /// <summary>
            /// apoc.nodes.collapse([nodes…​],[{properties:&apos;overwrite&apos; or &apos;discard&apos; or &apos;combine&apos;}]) yield from, rel, to merge nodes onto first in list
            /// </summary>
            public static MiscJaggedListResult Collapse(MiscResult @config, AliasListResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesCollapse, new object[] { @config, @nodes });
            }
            /// <summary>
            /// apoc.nodes.connected(start, end, rel-direction-pattern) - returns true when the node is connected to the other node, optimized for dense nodes
            /// </summary>
            public static MiscJaggedListResult Connected(AliasResult @start, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.FnApocNodesConnected, new object[] { @start, @types });
            }
            /// <summary>
            /// CALL apoc.nodes.cycles([nodes], $config) - Detect all path cycles from node list
            /// </summary>
            public static MiscJaggedListResult Cycles(MiscResult @config, AliasListResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesCycles, new object[] { @config, @nodes });
            }
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult Group(MiscListResult @aggregations, MiscResult @config, StringListResult @groupByProperties, StringListResult @labels)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesGroup, new object[] { @aggregations, @config, @groupByProperties, @labels });
            }
            /// <summary>
            /// apoc.nodes.isDense(node) - returns true if it is a dense node
            /// </summary>
            public static MiscJaggedListResult Isdense(AliasResult @node)
            {
                return new MiscJaggedListResult(t => t.FnApocNodesIsdense, new object[] { @node });
            }
            /// <summary>
            /// apoc.nodes.link([nodes],&apos;REL_TYPE&apos;, conf) - creates a linked list of nodes from first to last
            /// </summary>
            public static MiscJaggedListResult Link(MiscResult @config, AliasListResult @nodes, StringResult @type)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesLink, new object[] { @config, @nodes, @type });
            }
            /// <summary>
            /// apoc.nodes.delete(node|nodes|id|[ids]) - quickly delete all nodes with these ids
            /// </summary>
            public static MiscJaggedListResult Delete(NumericResult @batchSize, MiscResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesDelete, new object[] { @batchSize, @nodes });
            }
            /// <summary>
            /// apoc.nodes.get(node|nodes|id|[ids]) - quickly returns all nodes with these ids
            /// </summary>
            public static MiscJaggedListResult Get(MiscResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesGet, new object[] { @nodes });
            }
            /// <summary>
            /// apoc.get.rels(rel|id|[ids]) - quickly returns all relationships with these ids
            /// </summary>
            public static MiscJaggedListResult Rels(MiscResult @relationships)
            {
                return new MiscJaggedListResult(t => t.CallApocNodesRels, new object[] { @relationships });
            }
        }

        public static partial class NodesRelationship
        {
            /// <summary>
            /// apoc.nodes.relationship.types(node|nodes|id|[ids], rel-direction-pattern) - returns a list of maps where each one has two fields: node which is the node subject of the analysis and types which is a list of distinct relationship types
            /// </summary>
            public static MiscJaggedListResult Types(MiscResult @ids, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.FnApocNodesRelationshipTypes, new object[] { @ids, @types });
            }
        }

        public static partial class NodesRelationships
        {
            /// <summary>
            /// apoc.nodes.relationships.exist(node|nodes|id|[ids], rel-direction-pattern) - returns a list of maps where each one has two fields: node which is the node subject of the analysis and exists which is a map with rel-pattern, boolean for the given relationship patterns
            /// </summary>
            public static MiscJaggedListResult Exist(MiscResult @ids, StringResult @types)
            {
                return new MiscJaggedListResult(t => t.FnApocNodesRelationshipsExist, new object[] { @ids, @types });
            }
        }

        public static partial class Number
        {
            /// <summary>
            /// apoc.number.arabicToRoman(number)  | convert arabic numbers to roman
            /// </summary>
            public static MiscJaggedListResult Arabictoroman(MiscResult @number)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberArabictoroman, new object[] { @number });
            }
            /// <summary>
            /// apoc.number.parseFloat(text)  | parse a text using the default system pattern and language to produce a double
            /// </summary>
            public static MiscJaggedListResult Parsefloat(StringResult @lang, StringResult @pattern, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberParsefloat, new object[] { @lang, @pattern, @text });
            }
            /// <summary>
            /// apoc.number.parseInt(text)  | parse a text using the default system pattern and language to produce a long
            /// </summary>
            public static MiscJaggedListResult Parseint(StringResult @lang, StringResult @pattern, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberParseint, new object[] { @lang, @pattern, @text });
            }
            /// <summary>
            /// apoc.number.romanToArabic(romanNumber)  | convert roman numbers to arabic
            /// </summary>
            public static MiscJaggedListResult Romantoarabic(StringResult @romanNumber)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberRomantoarabic, new object[] { @romanNumber });
            }
            /// <summary>
            /// apoc.number.format(number)  | format a long or double using the default system pattern and language to produce a string
            /// </summary>
            public static MiscJaggedListResult Format(StringResult @lang, MiscResult @number, StringResult @pattern)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberFormat, new object[] { @lang, @number, @pattern });
            }
        }

        public static partial class NumberExact
        {
            /// <summary>
            /// apoc.number.exact.div(stringA,stringB,[prec],[roundingModel]) - return the division’s result of two large numbers
            /// </summary>
            public static MiscJaggedListResult Div(NumericResult @precision, StringResult @roundingMode, StringResult @stringA, StringResult @stringB)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactDiv, new object[] { @precision, @roundingMode, @stringA, @stringB });
            }
            /// <summary>
            /// apoc.number.exact.mul(stringA,stringB,[prec],[roundingModel]) - return the multiplication’s result of two large numbers
            /// </summary>
            public static MiscJaggedListResult Mul(NumericResult @precision, StringResult @roundingMode, StringResult @stringA, StringResult @stringB)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactMul, new object[] { @precision, @roundingMode, @stringA, @stringB });
            }
            /// <summary>
            /// apoc.number.exact.sub(stringA,stringB) - return the substraction’s of two large numbers
            /// </summary>
            public static MiscJaggedListResult Sub(StringResult @stringA, StringResult @stringB)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactSub, new object[] { @stringA, @stringB });
            }
            /// <summary>
            /// apoc.number.exact.toExact(number) - return the exact value
            /// </summary>
            public static MiscJaggedListResult Toexact(NumericResult @number)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactToexact, new object[] { @number });
            }
            /// <summary>
            /// apoc.number.exact.add(stringA,stringB) - return the sum’s result of two large numbers
            /// </summary>
            public static MiscJaggedListResult Add(StringResult @stringA, StringResult @stringB)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactAdd, new object[] { @stringA, @stringB });
            }
            /// <summary>
            /// apoc.number.exact.toFloat(string,[prec],[roundingMode]) - return the Float value of a large number
            /// </summary>
            public static MiscJaggedListResult Tofloat(NumericResult @precision, StringResult @roundingMode, StringResult @stringA)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactTofloat, new object[] { @precision, @roundingMode, @stringA });
            }
            /// <summary>
            /// apoc.number.exact.toInteger(string,[prec],[roundingMode]) - return the Integer value of a large number
            /// </summary>
            public static MiscJaggedListResult Tointeger(NumericResult @precision, StringResult @roundingMode, StringResult @stringA)
            {
                return new MiscJaggedListResult(t => t.FnApocNumberExactTointeger, new object[] { @precision, @roundingMode, @stringA });
            }
        }

        public static partial class Path
        {
            /// <summary>
            /// apoc.path.combine(path1, path2) - combines the paths into one if the connecting node matches
            /// </summary>
            public static MiscJaggedListResult Combine(PathResult @first, PathResult @second)
            {
                return new MiscJaggedListResult(t => t.FnApocPathCombine, new object[] { @first, @second });
            }
            /// <summary>
            /// apoc.path.create(startNode,[rels]) - creates a path instance of the given elements
            /// </summary>
            public static MiscJaggedListResult Create(AliasListResult @rels, AliasResult @startNode)
            {
                return new MiscJaggedListResult(t => t.FnApocPathCreate, new object[] { @rels, @startNode });
            }
            /// <summary>
            /// apoc.path.expand(startNode &lt;id&gt;|Node|list, &apos;TYPE|TYPE_OUT&gt;|&lt;TYPE_IN&apos;, &apos;+YesLabel|-NoLabel&apos;, minLevel, maxLevel ) yield path - expand from start node following the given relationships from min to max-level adhering to the label filters
            /// </summary>
            public static MiscJaggedListResult Expand(StringResult @labelFilter, NumericResult @maxLevel, NumericResult @minLevel, StringResult @relationshipFilter, MiscResult @start)
            {
                return new MiscJaggedListResult(t => t.CallApocPathExpand, new object[] { @labelFilter, @maxLevel, @minLevel, @relationshipFilter, @start });
            }
            /// <summary>
            /// apoc.path.expandConfig(startNode &lt;id&gt;|Node|list, {minLevel,maxLevel,uniqueness,relationshipFilter,labelFilter,uniqueness:&apos;RELATIONSHIP_PATH&apos;,bfs:true, filterStartNode:false, limit:-1, optional:false, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield path - expand from start node following the given relationships from min to max-level adhering to the label filters.
            /// </summary>
            public static MiscJaggedListResult Expandconfig(MiscResult @config, MiscResult @start)
            {
                return new MiscJaggedListResult(t => t.CallApocPathExpandconfig, new object[] { @config, @start });
            }
            /// <summary>
            /// apoc.path.spanningTree(startNode &lt;id&gt;|Node|list, {maxLevel,relationshipFilter,labelFilter,bfs:true, filterStartNode:false, limit:-1, optional:false, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield path - expand a spanning tree reachable from start node following relationships to max-level adhering to the label filters
            /// </summary>
            public static MiscJaggedListResult Spanningtree(MiscResult @config, MiscResult @start)
            {
                return new MiscJaggedListResult(t => t.CallApocPathSpanningtree, new object[] { @config, @start });
            }
            /// <summary>
            /// apoc.path.subgraphAll(startNode &lt;id&gt;|Node|list, {maxLevel,relationshipFilter,labelFilter,bfs:true, filterStartNode:false, limit:-1, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield nodes, relationships - expand the subgraph reachable from start node following relationships to max-level adhering to the label filters, and also return all relationships within the subgraph
            /// </summary>
            public static MiscJaggedListResult Subgraphall(MiscResult @config, MiscResult @start)
            {
                return new MiscJaggedListResult(t => t.CallApocPathSubgraphall, new object[] { @config, @start });
            }
            /// <summary>
            /// apoc.path.subgraphNodes(startNode &lt;id&gt;|Node|list, {maxLevel,relationshipFilter,labelFilter,bfs:true, filterStartNode:false, limit:-1, optional:false, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield node - expand the subgraph nodes reachable from start node following relationships to max-level adhering to the label filters
            /// </summary>
            public static MiscJaggedListResult Subgraphnodes(MiscResult @config, MiscResult @start)
            {
                return new MiscJaggedListResult(t => t.CallApocPathSubgraphnodes, new object[] { @config, @start });
            }
            /// <summary>
            /// apoc.path.elements(path) - returns a list of node-relationship-node-…​
            /// </summary>
            public static MiscJaggedListResult Elements(PathResult @path)
            {
                return new MiscJaggedListResult(t => t.FnApocPathElements, new object[] { @path });
            }
            /// <summary>
            /// apoc.path.slice(path, [offset], [length]) - creates a sub-path with the given offset and length
            /// </summary>
            public static MiscJaggedListResult Slice(NumericResult @length, NumericResult @offset, PathResult @path)
            {
                return new MiscJaggedListResult(t => t.FnApocPathSlice, new object[] { @length, @offset, @path });
            }
        }

        public static partial class Periodic
        {
            /// <summary>
            /// apoc.periodic.cancel(name) - cancel job with the given name
            /// </summary>
            public static MiscJaggedListResult Cancel(StringResult @name)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicCancel, new object[] { @name });
            }
            /// <summary>
            /// apoc.periodic.commit(statement,params) - runs the given statement in separate transactions until it returns 0
            /// </summary>
            public static MiscJaggedListResult Commit(MiscResult @params, StringResult @statement)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicCommit, new object[] { @params, @statement });
            }
            /// <summary>
            /// apoc.periodic.countdown(&apos;name&apos;,statement,repeat-rate-in-seconds) creates a background job that will repeatedly execute the given Cypher statement until it returns 0.
            /// </summary>
            public static MiscJaggedListResult Countdown(StringResult @name, NumericResult @rate, StringResult @statement)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicCountdown, new object[] { @name, @rate, @statement });
            }
            /// <summary>
            /// apoc.periodic.iterate(&apos;statement returning items&apos;, &apos;statement per item&apos;, {batchSize:1000,iterateList:true,parallel:false,params:{},concurrency:50,retries:0}) YIELD batches, total - run the second statement for each item returned by the first statement. Returns number of batches and total processed rows
            /// </summary>
            public static MiscJaggedListResult Iterate(MiscResult @config, StringResult @cypherAction, StringResult @cypherIterate)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicIterate, new object[] { @config, @cypherAction, @cypherIterate });
            }
            /// <summary>
            /// apoc.periodic.repeat(&apos;name&apos;,statement,repeat-rate-in-seconds, config) submit a repeatedly-called background query. The parameter &apos;config&apos; is optional and can contain a &apos;params&apos; entry usable in nested Cypher statement.
            /// </summary>
            public static MiscJaggedListResult Repeat(MiscResult @config, StringResult @name, NumericResult @rate, StringResult @statement)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicRepeat, new object[] { @config, @name, @rate, @statement });
            }
            /// <summary>
            /// apoc.periodic.submit(&apos;name&apos;,statement,params) - creates a background job which executes a Cypher statement once. The parameter &apos;params&apos; is optional and can contain query parameters for the Cypher statement
            /// </summary>
            public static MiscJaggedListResult Submit(StringResult @name, MiscResult @params, StringResult @statement)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicSubmit, new object[] { @name, @params, @statement });
            }
            /// <summary>
            /// apoc.periodic.truncate({config}) - removes all entities (and optionally indexes and constraints) from db using the apoc.periodic.iterate under the hood
            /// </summary>
            public static MiscJaggedListResult Truncate()
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicTruncate(0), new object[] {  });
            }
            /// <summary>
            /// apoc.periodic.truncate({config}) - removes all entities (and optionally indexes and constraints) from db using the apoc.periodic.iterate under the hood
            /// </summary>
            public static MiscJaggedListResult Truncate(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicTruncate(1), new object[] { @config });
            }
            /// <summary>
            /// apoc.periodic.list - list all jobs
            /// </summary>
            public static MiscJaggedListResult List()
            {
                return new MiscJaggedListResult(t => t.CallApocPeriodicList);
            }
        }

        public static partial class Refactor
        {
            /// <summary>
            /// apoc.refactor.categorize(sourceKey, type, outgoing, label, targetKey, copiedKeys, batchSize) turn each unique propertyKey into a category node and connect to it
            /// </summary>
            public static MiscJaggedListResult Categorize(NumericResult @batchSize, StringListResult @copiedKeys, StringResult @label, BooleanResult @outgoing, StringResult @sourceKey, StringResult @targetKey, StringResult @type)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorCategorize, new object[] { @batchSize, @copiedKeys, @label, @outgoing, @sourceKey, @targetKey, @type });
            }
            /// <summary>
            /// apoc.refactor.cloneNodes([node1,node2,…​]) clone nodes with their labels and properties
            /// </summary>
            public static MiscJaggedListResult Clonenodes(AliasListResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonenodes(1), new object[] { @nodes });
            }
            /// <summary>
            /// apoc.refactor.cloneNodes([node1,node2,…​]) clone nodes with their labels and properties
            /// </summary>
            public static MiscJaggedListResult Clonenodes(AliasListResult @nodes, StringListResult @skipProperties)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonenodes(2), new object[] { @nodes, @skipProperties });
            }
            /// <summary>
            /// apoc.refactor.cloneNodes([node1,node2,…​]) clone nodes with their labels and properties
            /// </summary>
            public static MiscJaggedListResult Clonenodes(AliasListResult @nodes, StringListResult @skipProperties, BooleanResult @withRelationships)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonenodes(3), new object[] { @nodes, @skipProperties, @withRelationships });
            }
            /// <summary>
            /// apoc.refactor.cloneNodesWithRelationships([node1,node2,…​]) clone nodes with their labels, properties and relationships
            /// </summary>
            public static MiscJaggedListResult Clonenodeswithrelationships(AliasListResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonenodeswithrelationships, new object[] { @nodes });
            }
            /// <summary>
            /// apoc.refactor.cloneSubgraph([node1,node2,…​], [rel1,rel2,…​]:[], {standinNodes:[], skipProperties:[]}) YIELD input, output, error | clone nodes with their labels and properties (optionally skipping any properties in the skipProperties list via the config map), and clone the given relationships (will exist between cloned nodes only). If no relationships are provided, all relationships between the given nodes will be cloned. Relationships can be optionally redirected according to standinNodes node pairings (this is a list of list-pairs of nodes), so given a node in the original subgraph (first of the pair), an existing node (second of the pair) can act as a standin for it within the cloned subgraph. Cloned relationships will be redirected to the standin.
            /// </summary>
            public static MiscJaggedListResult Clonesubgraph(MiscResult @config, AliasListResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonesubgraph(2), new object[] { @config, @nodes });
            }
            /// <summary>
            /// apoc.refactor.cloneSubgraph([node1,node2,…​], [rel1,rel2,…​]:[], {standinNodes:[], skipProperties:[]}) YIELD input, output, error | clone nodes with their labels and properties (optionally skipping any properties in the skipProperties list via the config map), and clone the given relationships (will exist between cloned nodes only). If no relationships are provided, all relationships between the given nodes will be cloned. Relationships can be optionally redirected according to standinNodes node pairings (this is a list of list-pairs of nodes), so given a node in the original subgraph (first of the pair), an existing node (second of the pair) can act as a standin for it within the cloned subgraph. Cloned relationships will be redirected to the standin.
            /// </summary>
            public static MiscJaggedListResult Clonesubgraph(MiscResult @config, AliasListResult @nodes, AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonesubgraph(3), new object[] { @config, @nodes, @rels });
            }
            /// <summary>
            /// apoc.refactor.cloneSubgraphFromPaths([path1, path2, …​], {standinNodes:[], skipProperties:[]}) YIELD input, output, error | from the subgraph formed from the given paths, clone nodes with their labels and properties (optionally skipping any properties in the skipProperties list via the config map), and clone the relationships (will exist between cloned nodes only). Relationships can be optionally redirected according to standinNodes node pairings (this is a list of list-pairs of nodes), so given a node in the original subgraph (first of the pair), an existing node (second of the pair) can act as a standin for it within the cloned subgraph. Cloned relationships will be redirected to the standin.
            /// </summary>
            public static MiscJaggedListResult Clonesubgraphfrompaths(MiscResult @config, PathListResult @paths)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorClonesubgraphfrompaths, new object[] { @config, @paths });
            }
            /// <summary>
            /// apoc.refactor.collapseNode([node1,node2],&apos;TYPE&apos;) collapse node to relationship, node with one rel becomes self-relationship
            /// </summary>
            public static MiscJaggedListResult Collapsenode(MiscResult @nodes, StringResult @type)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorCollapsenode, new object[] { @nodes, @type });
            }
            /// <summary>
            /// apoc.refactor.deleteAndReconnect([pathLinkedList], [nodesToRemove], {config}) - Removes some nodes from a linked list
            /// </summary>
            public static MiscJaggedListResult Deleteandreconnect(MiscResult @config, AliasListResult @nodes, PathResult @path)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorDeleteandreconnect, new object[] { @config, @nodes, @path });
            }
            /// <summary>
            /// apoc.refactor.extractNode([rel1,rel2,…​], [labels],&apos;OUT&apos;,&apos;IN&apos;) extract node from relationships
            /// </summary>
            public static MiscJaggedListResult Extractnode(StringResult @inType, StringListResult @labels, StringResult @outType, MiscResult @relationships)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorExtractnode, new object[] { @inType, @labels, @outType, @relationships });
            }
            /// <summary>
            /// apoc.refactor.invert(rel) inverts relationship direction
            /// </summary>
            public static MiscJaggedListResult Invert(AliasResult @relationship)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorInvert, new object[] { @relationship });
            }
            /// <summary>
            /// apoc.refactor.mergeNodes([node1,node2],[{properties:&apos;overwrite&apos; or &apos;discard&apos; or &apos;combine&apos;}]) merge nodes onto first in list
            /// </summary>
            public static MiscJaggedListResult Mergenodes(MiscResult @config, AliasListResult @nodes)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorMergenodes, new object[] { @config, @nodes });
            }
            /// <summary>
            /// apoc.refactor.mergeRelationships([rel1,rel2]) merge relationships onto first in list
            /// </summary>
            public static MiscJaggedListResult Mergerelationships(MiscResult @config, AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorMergerelationships, new object[] { @config, @rels });
            }
            /// <summary>
            /// apoc.refactor.normalizeAsBoolean(entity, propertyKey, true_values, false_values) normalize/convert a property to be boolean
            /// </summary>
            public static MiscJaggedListResult Normalizeasboolean(MiscResult @entity, MiscListResult @false_values, StringResult @propertyKey, MiscListResult @true_values)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorNormalizeasboolean, new object[] { @entity, @false_values, @propertyKey, @true_values });
            }
            /// <summary>
            /// apoc.refactor.setType(rel, &apos;NEW-TYPE&apos;) change relationship-type
            /// </summary>
            public static MiscJaggedListResult Settype(StringResult @newType, AliasResult @relationship)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorSettype, new object[] { @newType, @relationship });
            }
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult To(AliasResult @newNode, AliasResult @relationship)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorTo, new object[] { @newNode, @relationship });
            }
            /// <summary>
            /// apoc.refactor.from(rel, startNode) redirect relationship to use new start-node
            /// </summary>
            public static MiscJaggedListResult From(AliasResult @newNode, AliasResult @relationship)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorFrom, new object[] { @newNode, @relationship });
            }
        }

        public static partial class RefactorRename
        {
            /// <summary>
            /// apoc.refactor.rename.label(oldLabel, newLabel, [nodes]) | rename a label from &apos;oldLabel&apos; to &apos;newLabel&apos; for all nodes. If &apos;nodes&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscJaggedListResult Label(StringResult @newLabel, AliasListResult @nodes, StringResult @oldLabel)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorRenameLabel, new object[] { @newLabel, @nodes, @oldLabel });
            }
            /// <summary>
            /// apoc.refactor.rename.nodeProperty(oldName, newName, [nodes], {config}) | rename all node’s property from &apos;oldName&apos; to &apos;newName&apos;. If &apos;nodes&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscJaggedListResult Nodeproperty(MiscResult @config, StringResult @newName, AliasListResult @nodes, StringResult @oldName)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorRenameNodeproperty, new object[] { @config, @newName, @nodes, @oldName });
            }
            /// <summary>
            /// apoc.refactor.rename.typeProperty(oldName, newName, [rels], {config}) | rename all relationship’s property from &apos;oldName&apos; to &apos;newName&apos;. If &apos;rels&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscJaggedListResult Typeproperty(MiscResult @config, StringResult @newName, StringResult @oldName)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorRenameTypeproperty(3), new object[] { @config, @newName, @oldName });
            }
            /// <summary>
            /// apoc.refactor.rename.typeProperty(oldName, newName, [rels], {config}) | rename all relationship’s property from &apos;oldName&apos; to &apos;newName&apos;. If &apos;rels&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscJaggedListResult Typeproperty(MiscResult @config, StringResult @newName, StringResult @oldName, AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorRenameTypeproperty(4), new object[] { @config, @newName, @oldName, @rels });
            }
            /// <summary>
            /// apoc.refactor.rename.type(oldType, newType, [rels], {config}) | rename all relationships with type &apos;oldType&apos; to &apos;newType&apos;. If &apos;rels&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscJaggedListResult Type(MiscResult @config, StringResult @newType, StringResult @oldType)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorRenameType(3), new object[] { @config, @newType, @oldType });
            }
            /// <summary>
            /// apoc.refactor.rename.type(oldType, newType, [rels], {config}) | rename all relationships with type &apos;oldType&apos; to &apos;newType&apos;. If &apos;rels&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscJaggedListResult Type(MiscResult @config, StringResult @newType, StringResult @oldType, AliasListResult @rels)
            {
                return new MiscJaggedListResult(t => t.CallApocRefactorRenameType(4), new object[] { @config, @newType, @oldType, @rels });
            }
        }

        public static partial class Rel
        {
            /// <summary>
            /// returns endNode for (virtual) relationships
            /// </summary>
            public static MiscJaggedListResult Endnode(AliasResult @rel)
            {
                return new MiscJaggedListResult(t => t.FnApocRelEndnode, new object[] { @rel });
            }
            /// <summary>
            /// returns startNode for (virtual) relationships
            /// </summary>
            public static MiscJaggedListResult Startnode(AliasResult @rel)
            {
                return new MiscJaggedListResult(t => t.FnApocRelStartnode, new object[] { @rel });
            }
            /// <summary>
            /// returns id for (virtual) relationships
            /// </summary>
            public static MiscJaggedListResult Id(AliasResult @rel)
            {
                return new MiscJaggedListResult(t => t.FnApocRelId, new object[] { @rel });
            }
            /// <summary>
            /// returns type for (virtual) relationships
            /// </summary>
            public static MiscJaggedListResult Type(AliasResult @rel)
            {
                return new MiscJaggedListResult(t => t.FnApocRelType, new object[] { @rel });
            }
        }

        public static partial class Schema
        {
            /// <summary>
            /// apoc.schema.assert({indexLabel:, …​}, {constraintLabel:[constraintKeys], …​}, dropExisting : true) yield label, key, keys, unique, action - drops all other existing indexes and constraints when dropExisting is true (default is true), and asserts that at the end of the operation the given indexes and unique constraints are there, each label:key pair is considered one constraint/label. Non-constraint indexes can define compound indexes with label:[key1,key2…​] pairings.
            /// </summary>
            public static MiscJaggedListResult Assert(MiscResult @constraints, BooleanResult @dropExisting, MiscResult @indexes)
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaAssert, new object[] { @constraints, @dropExisting, @indexes });
            }
            /// <summary>
            /// CALL apoc.schema.relationships([config]) yield name, startLabel, type, endLabel, properties, status
            /// </summary>
            public static MiscJaggedListResult Relationships()
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaRelationships(0), new object[] {  });
            }
            /// <summary>
            /// CALL apoc.schema.relationships([config]) yield name, startLabel, type, endLabel, properties, status
            /// </summary>
            public static MiscJaggedListResult Relationships(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaRelationships(1), new object[] { @config });
            }
            /// <summary>
            /// CALL apoc.schema.nodes([config]) yield name, label, properties, status, type
            /// </summary>
            public static MiscJaggedListResult Nodes()
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaNodes(0), new object[] {  });
            }
            /// <summary>
            /// CALL apoc.schema.nodes([config]) yield name, label, properties, status, type
            /// </summary>
            public static MiscJaggedListResult Nodes(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaNodes(1), new object[] { @config });
            }
        }

        public static partial class SchemaNode
        {
            /// <summary>
            /// RETURN apoc.schema.node.constraintExists(labelName, propertyNames)
            /// </summary>
            public static MiscJaggedListResult Constraintexists(StringResult @labelName, StringListResult @propertyName)
            {
                return new MiscJaggedListResult(t => t.FnApocSchemaNodeConstraintexists, new object[] { @labelName, @propertyName });
            }
            /// <summary>
            /// RETURN apoc.schema.node.indexExists(labelName, propertyNames)
            /// </summary>
            public static MiscJaggedListResult Indexexists(StringResult @labelName, StringListResult @propertyName)
            {
                return new MiscJaggedListResult(t => t.FnApocSchemaNodeIndexexists, new object[] { @labelName, @propertyName });
            }
        }

        public static partial class SchemaProperties
        {
            /// <summary>
            /// apoc.schema.properties.distinct(label, key) - quickly returns all distinct values for a given key
            /// </summary>
            public static MiscJaggedListResult Distinct(StringResult @key, StringResult @label)
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaPropertiesDistinct, new object[] { @key, @label });
            }
            /// <summary>
            /// apoc.schema.properties.distinctCount([label], [key]) YIELD label, key, value, count - quickly returns all distinct values and counts for a given key
            /// </summary>
            public static MiscJaggedListResult Distinctcount(StringResult @key, StringResult @label)
            {
                return new MiscJaggedListResult(t => t.CallApocSchemaPropertiesDistinctcount, new object[] { @key, @label });
            }
        }

        public static partial class SchemaRelationship
        {
            /// <summary>
            /// RETURN apoc.schema.relationship.constraintExists(type, propertyNames)
            /// </summary>
            public static MiscJaggedListResult Constraintexists(StringListResult @propertyName, StringResult @type)
            {
                return new MiscJaggedListResult(t => t.FnApocSchemaRelationshipConstraintexists, new object[] { @propertyName, @type });
            }
            /// <summary>
            /// RETURN apoc.schema.relationship.indexExists(relName, propertyNames)
            /// </summary>
            public static MiscJaggedListResult Indexexists(StringResult @labelName, StringListResult @propertyName)
            {
                return new MiscJaggedListResult(t => t.FnApocSchemaRelationshipIndexexists, new object[] { @labelName, @propertyName });
            }
        }

        public static partial class Scoring
        {
            /// <summary>
            /// apoc.scoring.existence(5, true) returns the provided score if true, 0 if false
            /// </summary>
            public static MiscJaggedListResult Existence(BooleanResult @exists, NumericResult @score)
            {
                return new MiscJaggedListResult(t => t.FnApocScoringExistence, new object[] { @exists, @score });
            }
            /// <summary>
            /// apoc.scoring.pareto(10, 20, 100, 11) applies a Pareto scoring function over the inputs
            /// </summary>
            public static MiscJaggedListResult Pareto(NumericResult @eightyPercentValue, NumericResult @maximumValue, NumericResult @minimumThreshold, NumericResult @score)
            {
                return new MiscJaggedListResult(t => t.FnApocScoringPareto, new object[] { @eightyPercentValue, @maximumValue, @minimumThreshold, @score });
            }
        }

        public static partial class Search
        {
            /// <summary>
            /// Do a parallel search over multiple indexes returning a reduced representation of the nodes found: node id, labels and the searched properties. apoc.search.multiSearchReduced( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ). Multiple search results for the same node are merged into one record.
            /// </summary>
            public static MiscJaggedListResult Multisearchreduced(MiscResult @LabelPropertyMap, StringResult @operator, StringResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocSearchMultisearchreduced, new object[] { @LabelPropertyMap, @operator, @value });
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning nodes. usage apoc.search.nodeAll( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ) returns all the Nodes found in the different searches.
            /// </summary>
            public static MiscJaggedListResult Nodeall(MiscResult @LabelPropertyMap, StringResult @operator, StringResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocSearchNodeall, new object[] { @LabelPropertyMap, @operator, @value });
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning a reduced representation of the nodes found: node id, labels and the searched property. apoc.search.nodeShortAll( map of label and properties which will be searched upon, operator: EXACT / CONTAINS / STARTS WITH | ENDS WITH / = / &lt;&gt; / &lt; / &gt; …​, value ). All &apos;hits&apos; are returned.
            /// </summary>
            public static MiscJaggedListResult Nodeallreduced(MiscResult @LabelPropertyMap, StringResult @operator, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocSearchNodeallreduced, new object[] { @LabelPropertyMap, @operator, @value });
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning a reduced representation of the nodes found: node id, labels and the searched properties. apoc.search.nodeReduced( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ). Multiple search results for the same node are merged into one record.
            /// </summary>
            public static MiscJaggedListResult Nodereduced(MiscResult @LabelPropertyMap, StringResult @operator, StringResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocSearchNodereduced, new object[] { @LabelPropertyMap, @operator, @value });
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning nodes. usage apoc.search.node( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ) returns all the DISTINCT Nodes found in the different searches.
            /// </summary>
            public static MiscJaggedListResult Node(MiscResult @LabelPropertyMap, StringResult @operator, StringResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocSearchNode, new object[] { @LabelPropertyMap, @operator, @value });
            }
        }

        public static partial class Spatial
        {
            /// <summary>
            /// apoc.spatial.geocode(&apos;address&apos;, maxResults, quotaException, $config) YIELD location, latitude, longitude, description, osmData - look up geographic location of address from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscJaggedListResult Geocode(MiscResult @config, StringResult @location)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialGeocode(2), new object[] { @config, @location });
            }
            /// <summary>
            /// apoc.spatial.geocode(&apos;address&apos;, maxResults, quotaException, $config) YIELD location, latitude, longitude, description, osmData - look up geographic location of address from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscJaggedListResult Geocode(MiscResult @config, StringResult @location, NumericResult @maxResults)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialGeocode(3), new object[] { @config, @location, @maxResults });
            }
            /// <summary>
            /// apoc.spatial.geocode(&apos;address&apos;, maxResults, quotaException, $config) YIELD location, latitude, longitude, description, osmData - look up geographic location of address from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscJaggedListResult Geocode(MiscResult @config, StringResult @location, NumericResult @maxResults, BooleanResult @quotaException)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialGeocode(4), new object[] { @config, @location, @maxResults, @quotaException });
            }
            /// <summary>
            /// apoc.spatial.geocodeOnce(&apos;address&apos;, $config) YIELD location, latitude, longitude, description, osmData - look up geographic location of address from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscJaggedListResult Geocodeonce(MiscResult @config, StringResult @location)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialGeocodeonce, new object[] { @config, @location });
            }
            /// <summary>
            /// apoc.spatial.reverseGeocode(latitude,longitude, quotaException, $config) YIELD location, latitude, longitude, description - look up address from latitude and longitude from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscJaggedListResult Reversegeocode(MiscResult @config, FloatResult @latitude, FloatResult @longitude)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialReversegeocode(3), new object[] { @config, @latitude, @longitude });
            }
            /// <summary>
            /// apoc.spatial.reverseGeocode(latitude,longitude, quotaException, $config) YIELD location, latitude, longitude, description - look up address from latitude and longitude from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscJaggedListResult Reversegeocode(MiscResult @config, FloatResult @latitude, FloatResult @longitude, BooleanResult @quotaException)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialReversegeocode(4), new object[] { @config, @latitude, @longitude, @quotaException });
            }
            /// <summary>
            /// apoc.spatial.sortByDistance(List&lt;Path&gt;) sort the given paths based on the geo informations (lat/long) in ascending order
            /// </summary>
            public static MiscJaggedListResult Sortbydistance(PathListResult @paths)
            {
                return new MiscJaggedListResult(t => t.CallApocSpatialSortbydistance, new object[] { @paths });
            }
        }

        public static partial class Stats
        {
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult Degrees(StringResult @types)
            {
                return new MiscJaggedListResult(t => t.CallApocStatsDegrees, new object[] { @types });
            }
        }

        public static partial class SystemdbExport
        {
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult Metadata()
            {
                return new MiscJaggedListResult(t => t.CallApocSystemdbExportMetadata(0), new object[] {  });
            }
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscJaggedListResult Metadata(MiscResult @config)
            {
                return new MiscJaggedListResult(t => t.CallApocSystemdbExportMetadata(1), new object[] { @config });
            }
        }

        public static partial class Temporal
        {
            /// <summary>
            /// apoc.temporal.formatDuration(input, format) | Format a Duration
            /// </summary>
            public static MiscJaggedListResult Formatduration(StringResult @format, MiscResult @input)
            {
                return new MiscJaggedListResult(t => t.FnApocTemporalFormatduration, new object[] { @format, @input });
            }
            /// <summary>
            /// apoc.temporal.toZonedTemporal(&apos;2012-12-23 23:59:59&apos;,&apos;yyyy-MM-dd HH:mm:ss&apos;, &apos;UTC-hour-offset&apos;) parse date string using the specified format to specified timezone
            /// </summary>
            public static MiscJaggedListResult Tozonedtemporal(StringResult @format, StringResult @time)
            {
                return new MiscJaggedListResult(t => t.FnApocTemporalTozonedtemporal(2), new object[] { @format, @time });
            }
            /// <summary>
            /// apoc.temporal.toZonedTemporal(&apos;2012-12-23 23:59:59&apos;,&apos;yyyy-MM-dd HH:mm:ss&apos;, &apos;UTC-hour-offset&apos;) parse date string using the specified format to specified timezone
            /// </summary>
            public static MiscJaggedListResult Tozonedtemporal(StringResult @format, StringResult @time, StringResult @timezone)
            {
                return new MiscJaggedListResult(t => t.FnApocTemporalTozonedtemporal(3), new object[] { @format, @time, @timezone });
            }
            /// <summary>
            /// apoc.temporal.format(input, format) | Format a temporal value
            /// </summary>
            public static MiscJaggedListResult Format(StringResult @format, MiscResult @temporal)
            {
                return new MiscJaggedListResult(t => t.FnApocTemporalFormat, new object[] { @format, @temporal });
            }
        }

        public static partial class Text
        {
            /// <summary>
            /// apoc.text.base64Decode(text) YIELD value - Decode Base64 encoded string
            /// </summary>
            public static MiscJaggedListResult Base64decode(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBase64decode, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.base64Encode(text) YIELD value - Encode a string with Base64
            /// </summary>
            public static MiscJaggedListResult Base64encode(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBase64encode, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.base64UrlDecode(url) YIELD value - Decode Base64 encoded url
            /// </summary>
            public static MiscJaggedListResult Base64urldecode(StringResult @url)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBase64urldecode, new object[] { @url });
            }
            /// <summary>
            /// apoc.text.base64UrlEncode(text) YIELD value - Encode a url with Base64
            /// </summary>
            public static MiscJaggedListResult Base64urlencode(StringResult @url)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBase64urlencode, new object[] { @url });
            }
            /// <summary>
            /// apoc.text.byteCount(text,[charset]) - return size of text in bytes
            /// </summary>
            public static MiscJaggedListResult Bytecount(StringResult @charset, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBytecount, new object[] { @charset, @text });
            }
            /// <summary>
            /// apoc.text.bytes(text,[charset]) - return bytes of the text
            /// </summary>
            public static MiscJaggedListResult Bytes(StringResult @charset, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextBytes, new object[] { @charset, @text });
            }
            /// <summary>
            /// apoc.text.camelCase(text) YIELD value - Convert a string to camelCase
            /// </summary>
            public static MiscJaggedListResult Camelcase(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextCamelcase, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.capitalize(text) YIELD value - capitalise the first letter of the word
            /// </summary>
            public static MiscJaggedListResult Capitalize(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextCapitalize, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.capitalizeAll(text) YIELD value - capitalise the first letter of every word in the text
            /// </summary>
            public static MiscJaggedListResult Capitalizeall(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextCapitalizeall, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.charAt(text, index) - the decimal value of the character at the given index
            /// </summary>
            public static MiscJaggedListResult Charat(NumericResult @index, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextCharat, new object[] { @index, @text });
            }
            /// <summary>
            /// apoc.text.code(codepoint) - Returns the unicode character of the given codepoint
            /// </summary>
            public static MiscJaggedListResult Code(NumericResult @codepoint)
            {
                return new MiscJaggedListResult(t => t.FnApocTextCode, new object[] { @codepoint });
            }
            /// <summary>
            /// apoc.text.compareCleaned(text1, text2) - compare the given strings stripped of everything except alpha numeric characters converted to lower case.
            /// </summary>
            public static MiscJaggedListResult Comparecleaned(StringResult @text1, StringResult @text2)
            {
                return new MiscJaggedListResult(t => t.FnApocTextComparecleaned, new object[] { @text1, @text2 });
            }
            /// <summary>
            /// apoc.text.decapitalize(text) YIELD value - decapitalize the first letter of the word
            /// </summary>
            public static MiscJaggedListResult Decapitalize(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextDecapitalize, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.decapitalizeAll(text) YIELD value - decapitalize the first letter of all words
            /// </summary>
            public static MiscJaggedListResult Decapitalizeall(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextDecapitalizeall, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.distance(text1, text2) - compare the given strings with the Levenshtein distance algorithm.
            /// </summary>
            public static MiscJaggedListResult Distance(StringResult @text1, StringResult @text2)
            {
                return new MiscJaggedListResult(t => t.FnApocTextDistance, new object[] { @text1, @text2 });
            }
            /// <summary>
            /// apoc.text.doubleMetaphone(value) yield value - Compute the Double Metaphone phonetic encoding of all words of the text value which can be a single string or a list of strings
            /// </summary>
            public static MiscJaggedListResult Doublemetaphone(StringResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocTextDoublemetaphone, new object[] { @value });
            }
            /// <summary>
            /// apoc.text.fuzzyMatch(text1, text2) - check if 2 words can be matched in a fuzzy way. Depending on the length of the String it will allow more characters that needs to be edited to match the second String.
            /// </summary>
            public static MiscJaggedListResult Fuzzymatch(StringResult @text1, StringResult @text2)
            {
                return new MiscJaggedListResult(t => t.FnApocTextFuzzymatch, new object[] { @text1, @text2 });
            }
            /// <summary>
            /// apoc.text.hammingDistance(text1, text2) - compare the given strings with the Hamming distance algorithm.
            /// </summary>
            public static MiscJaggedListResult Hammingdistance(StringResult @text1, StringResult @text2)
            {
                return new MiscJaggedListResult(t => t.FnApocTextHammingdistance, new object[] { @text1, @text2 });
            }
            /// <summary>
            /// apoc.text.hexCharAt(text, index) - the hex value string of the character at the given index
            /// </summary>
            public static MiscJaggedListResult Hexcharat(NumericResult @index, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextHexcharat, new object[] { @index, @text });
            }
            /// <summary>
            /// apoc.text.hexValue(value) - the hex value string of the given number
            /// </summary>
            public static MiscJaggedListResult Hexvalue(NumericResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocTextHexvalue, new object[] { @value });
            }
            /// <summary>
            /// apoc.text.indexesOf(text, lookup, from=0, to=-1==len) - finds all occurences of the lookup string in the text, return list, from inclusive, to exclusive, empty list if not found, null if text is null.
            /// </summary>
            public static MiscJaggedListResult Indexesof(NumericResult @from, StringResult @lookup, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextIndexesof(3), new object[] { @from, @lookup, @text });
            }
            /// <summary>
            /// apoc.text.indexesOf(text, lookup, from=0, to=-1==len) - finds all occurences of the lookup string in the text, return list, from inclusive, to exclusive, empty list if not found, null if text is null.
            /// </summary>
            public static MiscJaggedListResult Indexesof(NumericResult @from, StringResult @lookup, StringResult @text, NumericResult @to)
            {
                return new MiscJaggedListResult(t => t.FnApocTextIndexesof(4), new object[] { @from, @lookup, @text, @to });
            }
            /// <summary>
            /// apoc.text.jaroWinklerDistance(text1, text2) - compare the given strings with the Jaro-Winkler distance algorithm.
            /// </summary>
            public static MiscJaggedListResult Jarowinklerdistance(StringResult @text1, StringResult @text2)
            {
                return new MiscJaggedListResult(t => t.FnApocTextJarowinklerdistance, new object[] { @text1, @text2 });
            }
            /// <summary>
            /// apoc.text.join([&apos;text1&apos;,&apos;text2&apos;,…​], delimiter) - join the given strings with the given delimiter.
            /// </summary>
            public static MiscJaggedListResult Join(StringResult @delimiter, StringListResult @texts)
            {
                return new MiscJaggedListResult(t => t.FnApocTextJoin, new object[] { @delimiter, @texts });
            }
            /// <summary>
            /// apoc.text.levenshteinDistance(text1, text2) - compare the given strings with the Levenshtein distance algorithm.
            /// </summary>
            public static MiscJaggedListResult Levenshteindistance(StringResult @text1, StringResult @text2)
            {
                return new MiscJaggedListResult(t => t.FnApocTextLevenshteindistance, new object[] { @text1, @text2 });
            }
            /// <summary>
            /// apoc.text.levenshteinSimilarity(text1, text2) - calculate the similarity (a value within 0 and 1) between two texts.
            /// </summary>
            public static MiscJaggedListResult Levenshteinsimilarity(StringResult @text1, StringResult @text2)
            {
                return new MiscJaggedListResult(t => t.FnApocTextLevenshteinsimilarity, new object[] { @text1, @text2 });
            }
            /// <summary>
            /// apoc.text.lpad(text,count,delim) YIELD value - left pad the string to the given width
            /// </summary>
            public static MiscJaggedListResult Lpad(NumericResult @count, StringResult @delim, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextLpad, new object[] { @count, @delim, @text });
            }
            /// <summary>
            /// apoc.text.phonetic(value) yield value - Compute the US_ENGLISH phonetic soundex encoding of all words of the text value which can be a single string or a list of strings
            /// </summary>
            public static MiscJaggedListResult Phonetic(StringResult @value)
            {
                return new MiscJaggedListResult(t => t.CallApocTextPhonetic, new object[] { @value });
            }
            /// <summary>
            /// apoc.text.phoneticDelta(text1, text2) yield phonetic1, phonetic2, delta - Compute the US_ENGLISH soundex character difference between two given strings
            /// </summary>
            public static MiscJaggedListResult Phoneticdelta(StringResult @text1, StringResult @text2)
            {
                return new MiscJaggedListResult(t => t.CallApocTextPhoneticdelta, new object[] { @text1, @text2 });
            }
            /// <summary>
            /// apoc.text.random(length, valid) YIELD value - generate a random string
            /// </summary>
            public static MiscJaggedListResult Random(NumericResult @length)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRandom(1), new object[] { @length });
            }
            /// <summary>
            /// apoc.text.random(length, valid) YIELD value - generate a random string
            /// </summary>
            public static MiscJaggedListResult Random(NumericResult @length, StringResult @valid)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRandom(2), new object[] { @length, @valid });
            }
            /// <summary>
            /// apoc.text.regexGroups(text, regex) - return all matching groups of the regex on the given text.
            /// </summary>
            public static MiscJaggedListResult Regexgroups(StringResult @regex, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRegexgroups, new object[] { @regex, @text });
            }
            /// <summary>
            /// apoc.text.regreplace(text, regex, replacement) - replace each substring of the given string that matches the given regular expression with the given replacement.
            /// </summary>
            public static MiscJaggedListResult Regreplace(StringResult @regex, StringResult @replacement, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRegreplace, new object[] { @regex, @replacement, @text });
            }
            /// <summary>
            /// apoc.text.rpad(text,count,delim) YIELD value - right pad the string to the given width
            /// </summary>
            public static MiscJaggedListResult Rpad(NumericResult @count, StringResult @delim, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRpad, new object[] { @count, @delim, @text });
            }
            /// <summary>
            /// apoc.text.slug(text, delim) - slug the text with the given delimiter
            /// </summary>
            public static MiscJaggedListResult Slug(StringResult @delim, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextSlug, new object[] { @delim, @text });
            }
            /// <summary>
            /// apoc.text.snakeCase(text) YIELD value - Convert a string to snake-case
            /// </summary>
            public static MiscJaggedListResult Snakecase(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextSnakecase, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.sorensenDiceSimilarityWithLanguage(text1, text2, languageTag) - compare the given strings with the Sørensen–Dice coefficient formula, with the provided IETF language tag
            /// </summary>
            public static MiscJaggedListResult Sorensendicesimilarity(StringResult @languageTag, StringResult @text1, StringResult @text2)
            {
                return new MiscJaggedListResult(t => t.FnApocTextSorensendicesimilarity, new object[] { @languageTag, @text1, @text2 });
            }
            /// <summary>
            /// apoc.text.swapCase(text) YIELD value - Swap the case of a string
            /// </summary>
            public static MiscJaggedListResult Swapcase(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextSwapcase, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.toCypher(value, {skipKeys,keepKeys,skipValues,keepValues,skipNull,node,relationship,start,end}) | tries it’s best to convert the value to a cypher-property-string
            /// </summary>
            public static MiscJaggedListResult Tocypher(MiscResult @config, MiscResult @value)
            {
                return new MiscJaggedListResult(t => t.FnApocTextTocypher, new object[] { @config, @value });
            }
            /// <summary>
            /// apoc.text.toUpperCase(text) YIELD value - Convert a string to UPPER_CASE
            /// </summary>
            public static MiscJaggedListResult Touppercase(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextTouppercase, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.upperCamelCase(text) YIELD value - Convert a string to camelCase
            /// </summary>
            public static MiscJaggedListResult Uppercamelcase(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextUppercamelcase, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.urldecode(text) - return the urldecoded text
            /// </summary>
            public static MiscJaggedListResult Urldecode(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextUrldecode, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.urlencode(text) - return the urlencoded text
            /// </summary>
            public static MiscJaggedListResult Urlencode(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextUrlencode, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.clean(text) - strip the given string of everything except alpha numeric characters and convert it to lower case.
            /// </summary>
            public static MiscJaggedListResult Clean(StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextClean, new object[] { @text });
            }
            /// <summary>
            /// apoc.text.format(text,[params],language) - sprintf format the string with the params given
            /// </summary>
            public static MiscJaggedListResult Format(StringResult @language, MiscListResult @params, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextFormat, new object[] { @language, @params, @text });
            }
            /// <summary>
            /// apoc.text.indexOf(text, lookup, from=0, to=-1==len) - find the first occurence of the lookup string in the text, from inclusive, to exclusive, -1 if not found, null if text is null.
            /// </summary>
            public static MiscJaggedListResult Indexof(NumericResult @from, StringResult @lookup, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextIndexof(3), new object[] { @from, @lookup, @text });
            }
            /// <summary>
            /// apoc.text.indexOf(text, lookup, from=0, to=-1==len) - find the first occurence of the lookup string in the text, from inclusive, to exclusive, -1 if not found, null if text is null.
            /// </summary>
            public static MiscJaggedListResult Indexof(NumericResult @from, StringResult @lookup, StringResult @text, NumericResult @to)
            {
                return new MiscJaggedListResult(t => t.FnApocTextIndexof(4), new object[] { @from, @lookup, @text, @to });
            }
            /// <summary>
            /// apoc.text.repeat(item, count) - string multiplication
            /// </summary>
            public static MiscJaggedListResult Repeat(NumericResult @count, StringResult @item)
            {
                return new MiscJaggedListResult(t => t.FnApocTextRepeat, new object[] { @count, @item });
            }
            /// <summary>
            /// apoc.text.replace(text, regex, replacement) - replace each substring of the given string that matches the given regular expression with the given replacement.
            /// </summary>
            public static MiscJaggedListResult Replace(StringResult @regex, StringResult @replacement, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextReplace, new object[] { @regex, @replacement, @text });
            }
            /// <summary>
            /// apoc.text.split(text, regex, limit) - splits the given text around matches of the given regex.
            /// </summary>
            public static MiscJaggedListResult Split(NumericResult @limit, StringResult @regex, StringResult @text)
            {
                return new MiscJaggedListResult(t => t.FnApocTextSplit, new object[] { @limit, @regex, @text });
            }
        }

        public static partial class Trigger
        {
            /// <summary>
            /// CALL apoc.trigger.pause(name) | it pauses the trigger
            /// </summary>
            public static MiscJaggedListResult Pause(StringResult @name)
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerPause, new object[] { @name });
            }
            /// <summary>
            /// CALL apoc.trigger.resume(name) | it resumes the paused trigger
            /// </summary>
            public static MiscJaggedListResult Resume(StringResult @name)
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerResume, new object[] { @name });
            }
            /// <summary>
            /// add a trigger kernelTransaction under a name, in the kernelTransaction you can use {createdNodes}, {deletedNodes} etc., the selector is {phase:&apos;before/after/rollback&apos;} returns previous and new trigger information. Takes in an optional configuration.
            /// </summary>
            public static MiscJaggedListResult Add(MiscResult @config, StringResult @kernelTransaction, StringResult @name, MiscResult @selector)
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerAdd, new object[] { @config, @kernelTransaction, @name, @selector });
            }
            /// <summary>
            /// list all installed triggers
            /// </summary>
            public static MiscJaggedListResult List()
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerList);
            }
            /// <summary>
            /// remove previously added trigger, returns trigger information
            /// </summary>
            public static MiscJaggedListResult Remove(StringResult @name)
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerRemove, new object[] { @name });
            }
            /// <summary>
            /// removes all previously added trigger, returns trigger information
            /// </summary>
            public static MiscJaggedListResult Removeall()
            {
                return new MiscJaggedListResult(t => t.CallApocTriggerRemoveall);
            }
        }

        public static partial class Util
        {
            /// <summary>
            /// apoc.util.compress(string, {config}) | return a compressed byte[] in various format from a string
            /// </summary>
            public static MiscJaggedListResult Compress(MiscResult @config, StringResult @data)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilCompress, new object[] { @config, @data });
            }
            /// <summary>
            /// apoc.util.decompress(compressed, {config}) | return a string from a compressed byte[] in various format
            /// </summary>
            public static MiscJaggedListResult Decompress(MiscResult @config, BinaryResult @data)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilDecompress, new object[] { @config, @data });
            }
            /// <summary>
            /// apoc.util.md5([values]) | computes the md5 of the concatenation of all string values of the list. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static MiscJaggedListResult Md5(params MiscListResult[] @values)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilMd5(@values.Length), @values);
            }
            /// <summary>
            /// apoc.util.sha1([values]) | computes the sha1 of the concatenation of all string values of the list
            /// </summary>
            public static MiscJaggedListResult Sha1(params MiscListResult[] @values)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilSha1(@values.Length), @values);
            }
            /// <summary>
            /// apoc.util.sha256([values]) | computes the sha256 of the concatenation of all string values of the list
            /// </summary>
            public static MiscJaggedListResult Sha256(params MiscListResult[] @values)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilSha256(@values.Length), @values);
            }
            /// <summary>
            /// apoc.util.sha384([values]) | computes the sha384 of the concatenation of all string values of the list
            /// </summary>
            public static MiscJaggedListResult Sha384(params MiscListResult[] @values)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilSha384(@values.Length), @values);
            }
            /// <summary>
            /// apoc.util.sha512([values]) | computes the sha512 of the concatenation of all string values of the list
            /// </summary>
            public static MiscJaggedListResult Sha512(params MiscListResult[] @values)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilSha512(@values.Length), @values);
            }
            /// <summary>
            /// apoc.util.sleep(&lt;duration&gt;) | sleeps for &lt;duration&gt; millis, transaction termination is honored
            /// </summary>
            public static MiscJaggedListResult Sleep(NumericResult @duration)
            {
                return new MiscJaggedListResult(t => t.CallApocUtilSleep, new object[] { @duration });
            }
            /// <summary>
            /// apoc.util.validatePredicate(predicate, message, params) | if the predicate yields to true raise an exception else returns true, for use inside WHERE subclauses
            /// </summary>
            public static MiscJaggedListResult Validatepredicate(StringResult @message, MiscListResult @params, BooleanResult @predicate)
            {
                return new MiscJaggedListResult(t => t.FnApocUtilValidatepredicate, new object[] { @message, @params, @predicate });
            }
            /// <summary>
            /// apoc.util.validate(predicate, message, params) | if the predicate yields to true raise an exception
            /// </summary>
            public static MiscJaggedListResult Validate(StringResult @message, MiscListResult @params, BooleanResult @predicate)
            {
                return new MiscJaggedListResult(t => t.CallApocUtilValidate, new object[] { @message, @params, @predicate });
            }
        }

        public static partial class Warmup
        {
            /// <summary>
            /// apoc.warmup.run(loadProperties=false,loadDynamicProperties=false,loadIndexes=false) - quickly loads all nodes and rels into memory by skipping one page at a time
            /// </summary>
            public static MiscJaggedListResult Run()
            {
                return new MiscJaggedListResult(t => t.CallApocWarmupRun(0), new object[] {  });
            }
            /// <summary>
            /// apoc.warmup.run(loadProperties=false,loadDynamicProperties=false,loadIndexes=false) - quickly loads all nodes and rels into memory by skipping one page at a time
            /// </summary>
            public static MiscJaggedListResult Run(BooleanResult @loadDynamicProperties)
            {
                return new MiscJaggedListResult(t => t.CallApocWarmupRun(1), new object[] { @loadDynamicProperties });
            }
            /// <summary>
            /// apoc.warmup.run(loadProperties=false,loadDynamicProperties=false,loadIndexes=false) - quickly loads all nodes and rels into memory by skipping one page at a time
            /// </summary>
            public static MiscJaggedListResult Run(BooleanResult @loadDynamicProperties, BooleanResult @loadIndexes)
            {
                return new MiscJaggedListResult(t => t.CallApocWarmupRun(2), new object[] { @loadDynamicProperties, @loadIndexes });
            }
            /// <summary>
            /// apoc.warmup.run(loadProperties=false,loadDynamicProperties=false,loadIndexes=false) - quickly loads all nodes and rels into memory by skipping one page at a time
            /// </summary>
            public static MiscJaggedListResult Run(BooleanResult @loadDynamicProperties, BooleanResult @loadIndexes, BooleanResult @loadProperties)
            {
                return new MiscJaggedListResult(t => t.CallApocWarmupRun(3), new object[] { @loadDynamicProperties, @loadIndexes, @loadProperties });
            }
        }

        public static partial class Xml
        {
            /// <summary>
            /// Deprecated by apoc.import.xml
            /// </summary>
            public static MiscJaggedListResult Import(MiscResult @config, StringResult @url)
            {
                return new MiscJaggedListResult(t => t.CallApocXmlImport, new object[] { @config, @url });
            }
            /// <summary>
            /// RETURN apoc.xml.parse(&lt;xml string&gt;, &lt;xPath string&gt;, config, false) AS value
            /// </summary>
            public static MiscJaggedListResult Parse(MiscResult @config, StringResult @data)
            {
                return new MiscJaggedListResult(t => t.FnApocXmlParse(2), new object[] { @config, @data });
            }
            /// <summary>
            /// RETURN apoc.xml.parse(&lt;xml string&gt;, &lt;xPath string&gt;, config, false) AS value
            /// </summary>
            public static MiscJaggedListResult Parse(MiscResult @config, StringResult @data, StringResult @path)
            {
                return new MiscJaggedListResult(t => t.FnApocXmlParse(3), new object[] { @config, @data, @path });
            }
            /// <summary>
            /// RETURN apoc.xml.parse(&lt;xml string&gt;, &lt;xPath string&gt;, config, false) AS value
            /// </summary>
            public static MiscJaggedListResult Parse(MiscResult @config, StringResult @data, StringResult @path, BooleanResult @simple)
            {
                return new MiscJaggedListResult(t => t.FnApocXmlParse(4), new object[] { @config, @data, @path, @simple });
            }
        }
    }
}
