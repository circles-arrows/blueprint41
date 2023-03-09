#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Blueprint41.Query;

public static partial class Functions
{
    public static partial class Apoc
    {
        #region Conversion Methods

        private static Parameter PathOptions_To_StringListResult(PathOptions value)
        {
            #region PathOptions_To_StringListResult -> NoOverwrite

            return Parameter.Constant(value.ToString().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries));

            #endregion PathOptions_To_StringListResult -> Overwrite
        }
        private static Parameter string_To_StringResult(string value)
        {
            #region string_To_StringResult -> NoOverwrite

            return Parameter.Constant(value);

            #endregion string_To_StringResult -> Overwrite
        }

        #endregion

        /// <summary>
        /// apoc.case([condition, query, condition, query, …​], elseQuery:&apos;&apos;, params:{}) yield value - given a list of conditional / read-only query pairs, executes the query associated with the first conditional evaluating to true (or the else query if none are true) with the given parameters
        /// </summary>
        public static MiscResult Case(MiscListResult @conditionals, StringResult @elseQuery)
        {
            return new MiscResult(t => t.Call_Apoc_Case(2), new object[] { @conditionals, @elseQuery }, null);
        }
        /// <summary>
        /// apoc.case([condition, query, condition, query, …​], elseQuery:&apos;&apos;, params:{}) yield value - given a list of conditional / read-only query pairs, executes the query associated with the first conditional evaluating to true (or the else query if none are true) with the given parameters
        /// </summary>
        public static MiscResult Case(MiscListResult @conditionals, StringResult @elseQuery, MiscResult @params)
        {
            return new MiscResult(t => t.Call_Apoc_Case(3), new object[] { @conditionals, @elseQuery, @params }, null);
        }
        /// <summary>
        /// Provides descriptions of available procedures. To narrow the results, supply a search string. To also search in the description text, append + to the end of the search string.
        /// </summary>
        public static BooleanResult Help(StringResult @proc)
        {
            return new BooleanResult(t => t.Call_Apoc_Help, new object[] { @proc }, null);
        }
        /// <summary>
        /// RETURN apoc.version() | return the current APOC installed version
        /// </summary>
        public static StringResult Version()
        {
            return new StringResult(t => t.Fn_Apoc_Version, new object[0], null);
        }
        /// <summary>
        /// apoc.when(condition, ifQuery, elseQuery:&apos;&apos;, params:{}) yield value - based on the conditional, executes read-only ifQuery or elseQuery with the given parameters
        /// </summary>
        public static MiscResult When(BooleanResult @condition, StringResult @elseQuery, StringResult @ifQuery)
        {
            return new MiscResult(t => t.Call_Apoc_When(3), new object[] { @condition, @elseQuery, @ifQuery }, null);
        }
        /// <summary>
        /// apoc.when(condition, ifQuery, elseQuery:&apos;&apos;, params:{}) yield value - based on the conditional, executes read-only ifQuery or elseQuery with the given parameters
        /// </summary>
        public static MiscResult When(BooleanResult @condition, StringResult @elseQuery, StringResult @ifQuery, MiscResult @params)
        {
            return new MiscResult(t => t.Call_Apoc_When(4), new object[] { @condition, @elseQuery, @ifQuery, @params }, null);
        }

        public static partial class Agg
        {
            /// <summary>
            /// apoc.agg.first(value) - returns first value
            /// </summary>
            public static MiscResult First(MiscResult @value)
            {
                return new MiscResult(t => t.Fn_ApocAgg_First, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.agg.graph(path) - returns map of graph {nodes, relationships} of all distinct nodes and relationships
            /// </summary>
            public static MiscResult Graph(MiscResult @element)
            {
                return new MiscResult(t => t.Fn_ApocAgg_Graph, new object[] { @element }, null);
            }
            /// <summary>
            /// apoc.agg.last(value) - returns last value
            /// </summary>
            public static MiscResult Last(MiscResult @value)
            {
                return new MiscResult(t => t.Fn_ApocAgg_Last, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.agg.maxItems(item, value, groupLimit: -1) - returns a map {items:[], value:n} where value is the maximum value present, and items are all items with the same value. The number of items can be optionally limited.
            /// </summary>
            public static MiscResult MaxItems(NumericResult @groupLimit, MiscResult @item, MiscResult @value)
            {
                return new MiscResult(t => t.Fn_ApocAgg_MaxItems, new object[] { @groupLimit, @item, @value }, null);
            }
            /// <summary>
            /// apoc.agg.median(number) - returns median for non-null numeric values
            /// </summary>
            public static MiscResult Median(MiscResult @value)
            {
                return new MiscResult(t => t.Fn_ApocAgg_Median, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.agg.minItems(item, value, groupLimit: -1) - returns a map {items:[], value:n} where value is the minimum value present, and items are all items with the same value. The number of items can be optionally limited.
            /// </summary>
            public static MiscResult MinItems(NumericResult @groupLimit, MiscResult @item, MiscResult @value)
            {
                return new MiscResult(t => t.Fn_ApocAgg_MinItems, new object[] { @groupLimit, @item, @value }, null);
            }
            /// <summary>
            /// apoc.agg.nth(value,offset) - returns value of nth row (or -1 for last)
            /// </summary>
            public static MiscResult Nth(MiscResult @value)
            {
                return new MiscResult(t => t.Fn_ApocAgg_Nth, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.agg.percentiles(value,[percentiles = 0.5,0.75,0.9,0.95,0.99]) - returns given percentiles for values
            /// </summary>
            public static MiscListResult Percentiles(FloatListResult @percentiles, FloatResult @value)
            {
                return new MiscListResult(t => t.Fn_ApocAgg_Percentiles, new object[] { @percentiles, @value }, null);
            }
            /// <summary>
            /// apoc.agg.product(number) - returns given product for non-null values
            /// </summary>
            public static FloatResult Product(FloatResult @number)
            {
                return new FloatResult(t => t.Fn_ApocAgg_Product, new object[] { @number }, null);
            }
            /// <summary>
            /// apoc.agg.slice(value, start, length) - returns subset of non-null values, start is 0 based and length can be -1
            /// </summary>
            public static MiscListResult Slice(NumericResult @from, NumericResult @to, MiscResult @value)
            {
                return new MiscListResult(t => t.Fn_ApocAgg_Slice, new object[] { @from, @to, @value }, null);
            }
            /// <summary>
            /// apoc.agg.statistics(value,[percentiles = 0.5,0.75,0.9,0.95,0.99]) - returns numeric statistics (percentiles, min,minNonZero,max,total,mean,stdev) for values
            /// </summary>
            public static MiscResult Statistics(FloatListResult @percentiles, FloatResult @value)
            {
                return new MiscResult(t => t.Fn_ApocAgg_Statistics, new object[] { @percentiles, @value }, null);
            }
        }

        public static partial class Algo
        {
            /// <summary>
            /// apoc.algo.allSimplePaths(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, 5) YIELD path, weight - run allSimplePaths with relationships given and maxNodes
            /// </summary>
            public static PathResult AllSimplePaths(AliasResult @endNode, NumericResult @maxNodes, StringResult @relationshipTypesAndDirections, AliasResult @startNode)
            {
                return new PathResult(t => t.Call_ApocAlgo_AllSimplePaths, new object[] { @endNode, @maxNodes, @relationshipTypesAndDirections, @startNode }, null);
            }
            /// <summary>
            /// apoc.algo.aStar(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, &apos;distance&apos;,&apos;lat&apos;,&apos;lon&apos;) YIELD path, weight - run A* with relationship property name as cost function
            /// </summary>
            public static PathResult AStar(AliasResult @endNode, StringResult @latPropertyName, StringResult @lonPropertyName, StringResult @relationshipTypesAndDirections, AliasResult @startNode, StringResult @weightPropertyName)
            {
                return new PathResult(t => t.Call_ApocAlgo_AStar, new object[] { @endNode, @latPropertyName, @lonPropertyName, @relationshipTypesAndDirections, @startNode, @weightPropertyName }, null);
            }
            /// <summary>
            /// apoc.algo.aStar(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, {weight:&apos;dist&apos;,default:10,x:&apos;lon&apos;,y:&apos;lat&apos;}) YIELD path, weight - run A* with relationship property name as cost function
            /// </summary>
            public static PathResult AStarConfig(MiscResult @config, AliasResult @endNode, StringResult @relationshipTypesAndDirections, AliasResult @startNode)
            {
                return new PathResult(t => t.Call_ApocAlgo_AStarConfig, new object[] { @config, @endNode, @relationshipTypesAndDirections, @startNode }, null);
            }
            /// <summary>
            /// apoc.algo.cover(nodes) yield rel - returns all relationships between this set of nodes
            /// </summary>
            public static AliasResult Cover(MiscResult @nodes)
            {
                return new AliasResult(t => t.Call_ApocAlgo_Cover, new object[] { @nodes }, null);
            }
            /// <summary>
            /// apoc.algo.dijkstra(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, &apos;distance&apos;, defaultValue, numberOfWantedResults) YIELD path, weight - run dijkstra with relationship property name as cost function
            /// </summary>
            public static PathResult Dijkstra(FloatResult @defaultWeight, AliasResult @endNode, NumericResult @numberOfWantedPaths, StringResult @relationshipTypesAndDirections, AliasResult @startNode, StringResult @weightPropertyName)
            {
                return new PathResult(t => t.Call_ApocAlgo_Dijkstra, new object[] { @defaultWeight, @endNode, @numberOfWantedPaths, @relationshipTypesAndDirections, @startNode, @weightPropertyName }, null);
            }
            /// <summary>
            /// apoc.algo.dijkstraWithDefaultWeight(startNode, endNode, &apos;KNOWS|&lt;WORKS_WITH|IS_MANAGER_OF&gt;&apos;, &apos;distance&apos;, 10) YIELD path, weight - run dijkstra with relationship property name as cost function and a default weight if the property does not exist
            /// </summary>
            public static PathResult DijkstraWithDefaultWeight(FloatResult @defaultWeight, AliasResult @endNode, StringResult @relationshipTypesAndDirections, AliasResult @startNode, StringResult @weightPropertyName)
            {
                return new PathResult(t => t.Call_ApocAlgo_DijkstraWithDefaultWeight, new object[] { @defaultWeight, @endNode, @relationshipTypesAndDirections, @startNode, @weightPropertyName }, null);
            }
        }

        public static partial class Any
        {
            /// <summary>
            /// returns properties for virtual and real, nodes, rels and maps
            /// </summary>
            public static MiscResult Properties(StringListResult @keys, MiscResult @thing)
            {
                return new MiscResult(t => t.Fn_ApocAny_Properties, new object[] { @keys, @thing }, null);
            }
            /// <summary>
            /// returns property for virtual and real, nodes, rels and maps
            /// </summary>
            public static MiscResult Property(StringResult @key, MiscResult @thing)
            {
                return new MiscResult(t => t.Fn_ApocAny_Property, new object[] { @key, @thing }, null);
            }
        }

        public static partial class Atomic
        {
            /// <summary>
            /// apoc.atomic.add(node/relatonship,propertyName,number) Sums the property’s value with the &apos;number&apos; value
            /// </summary>
            public static MiscResult Add(MiscResult @container, FloatResult @number, StringResult @propertyName)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Add(3), new object[] { @container, @number, @propertyName }, null);
            }
            /// <summary>
            /// apoc.atomic.add(node/relatonship,propertyName,number) Sums the property’s value with the &apos;number&apos; value
            /// </summary>
            public static MiscResult Add(MiscResult @container, FloatResult @number, StringResult @propertyName, NumericResult @times)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Add(4), new object[] { @container, @number, @propertyName, @times }, null);
            }
            /// <summary>
            /// apoc.atomic.concat(node/relatonship,propertyName,string) Concats the property’s value with the &apos;string&apos; value
            /// </summary>
            public static MiscResult Concat(MiscResult @container, StringResult @propertyName, StringResult @string)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Concat(3), new object[] { @container, @propertyName, @string }, null);
            }
            /// <summary>
            /// apoc.atomic.concat(node/relatonship,propertyName,string) Concats the property’s value with the &apos;string&apos; value
            /// </summary>
            public static MiscResult Concat(MiscResult @container, StringResult @propertyName, StringResult @string, NumericResult @times)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Concat(4), new object[] { @container, @propertyName, @string, @times }, null);
            }
            /// <summary>
            /// apoc.atomic.insert(node/relatonship,propertyName,position,value) insert a value into the property’s array value at &apos;position&apos;
            /// </summary>
            public static MiscResult Insert(MiscResult @container, NumericResult @position, StringResult @propertyName, NumericResult @times, MiscResult @value)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Insert, new object[] { @container, @position, @propertyName, @times, @value }, null);
            }
            /// <summary>
            /// apoc.atomic.remove(node/relatonship,propertyName,position) remove the element at position &apos;position&apos;
            /// </summary>
            public static MiscResult Remove(MiscResult @container, NumericResult @position, StringResult @propertyName)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Remove(3), new object[] { @container, @position, @propertyName }, null);
            }
            /// <summary>
            /// apoc.atomic.remove(node/relatonship,propertyName,position) remove the element at position &apos;position&apos;
            /// </summary>
            public static MiscResult Remove(MiscResult @container, NumericResult @position, StringResult @propertyName, NumericResult @times)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Remove(4), new object[] { @container, @position, @propertyName, @times }, null);
            }
            /// <summary>
            /// apoc.atomic.subtract(node/relatonship,propertyName,number) Subtracts the &apos;number&apos; value to the property’s value
            /// </summary>
            public static MiscResult Subtract(MiscResult @container, FloatResult @number, StringResult @propertyName)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Subtract(3), new object[] { @container, @number, @propertyName }, null);
            }
            /// <summary>
            /// apoc.atomic.subtract(node/relatonship,propertyName,number) Subtracts the &apos;number&apos; value to the property’s value
            /// </summary>
            public static MiscResult Subtract(MiscResult @container, FloatResult @number, StringResult @propertyName, NumericResult @times)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Subtract(4), new object[] { @container, @number, @propertyName, @times }, null);
            }
            /// <summary>
            /// apoc.atomic.update(node/relatonship,propertyName,updateOperation) update a property’s value with a cypher operation (ex. &quot;n.prop1+n.prop2&quot;)
            /// </summary>
            public static MiscResult Update(MiscResult @container, StringResult @operation, StringResult @propertyName)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Update(3), new object[] { @container, @operation, @propertyName }, null);
            }
            /// <summary>
            /// apoc.atomic.update(node/relatonship,propertyName,updateOperation) update a property’s value with a cypher operation (ex. &quot;n.prop1+n.prop2&quot;)
            /// </summary>
            public static MiscResult Update(MiscResult @container, StringResult @operation, StringResult @propertyName, NumericResult @times)
            {
                return new MiscResult(t => t.Call_ApocAtomic_Update(4), new object[] { @container, @operation, @propertyName, @times }, null);
            }
        }

        public static partial class Bitwise
        {
            /// <summary>
            /// apoc.bitwise.op(60,&apos;|&apos;,13) bitwise operations a &amp; b, a | b, a ^ b, ~a, a &gt;&gt; b, a &gt;&gt;&gt; b, a &lt;&lt; b. returns the result of the bitwise operation
            /// </summary>
            public static NumericResult Op(NumericResult @a, NumericResult @b, StringResult @operator)
            {
                return new NumericResult(t => t.Fn_ApocBitwise_Op, new object[] { @a, @b, @operator }, null);
            }
        }

        public static partial class Bolt
        {
            /// <summary>
            /// apoc.bolt.execute(url-or-key, kernelTransaction, params, config) - access to other databases via bolt for reads and writes
            /// </summary>
            public static MiscResult Execute(MiscResult @config, StringResult @kernelTransaction, MiscResult @params, StringResult @url)
            {
                return new MiscResult(t => t.Call_ApocBolt_Execute, new object[] { @config, @kernelTransaction, @params, @url }, null);
            }
        }

        public static partial class Coll
        {
            /// <summary>
            /// apoc.coll.avg([0.5,1,2.3])
            /// </summary>
            public static FloatResult Avg(FloatListResult @numbers)
            {
                return new FloatResult(t => t.Fn_ApocColl_Avg, new object[] { @numbers }, null);
            }
            /// <summary>
            /// apoc.coll.combinations(coll, minSelect, maxSelect:minSelect) - Returns collection of all combinations of list elements of selection size between minSelect and maxSelect (default:minSelect), inclusive
            /// </summary>
            public static MiscListResult Combinations(MiscListResult @coll, NumericResult @maxSelect, NumericResult @minSelect)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Combinations, new object[] { @coll, @maxSelect, @minSelect }, null);
            }
            /// <summary>
            /// apoc.coll.contains(coll, value) optimized contains operation (using a HashSet) (returns single row or not)
            /// </summary>
            public static BooleanResult Contains(MiscListResult @coll, MiscResult @value)
            {
                return new BooleanResult(t => t.Fn_ApocColl_Contains, new object[] { @coll, @value }, null);
            }
            /// <summary>
            /// apoc.coll.containsAll(coll, values) optimized contains-all operation (using a HashSet) (returns single row or not)
            /// </summary>
            public static BooleanResult ContainsAll(MiscListResult @coll, MiscListResult @values)
            {
                return new BooleanResult(t => t.Fn_ApocColl_ContainsAll, new object[] { @coll, @values }, null);
            }
            /// <summary>
            /// apoc.coll.containsAllSorted(coll, value) optimized contains-all on a sorted list operation (Collections.binarySearch) (returns single row or not)
            /// </summary>
            public static BooleanResult ContainsAllSorted(MiscListResult @coll, MiscListResult @values)
            {
                return new BooleanResult(t => t.Fn_ApocColl_ContainsAllSorted, new object[] { @coll, @values }, null);
            }
            /// <summary>
            /// apoc.coll.containsDuplicates(coll) - returns true if a collection contains duplicate elements
            /// </summary>
            public static BooleanResult ContainsDuplicates(MiscListResult @coll)
            {
                return new BooleanResult(t => t.Fn_ApocColl_ContainsDuplicates, new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.containsSorted(coll, value) optimized contains on a sorted list operation (Collections.binarySearch) (returns single row or not)
            /// </summary>
            public static BooleanResult ContainsSorted(MiscListResult @coll, MiscResult @value)
            {
                return new BooleanResult(t => t.Fn_ApocColl_ContainsSorted, new object[] { @coll, @value }, null);
            }
            /// <summary>
            /// apoc.coll.different(values) - returns true if values are different
            /// </summary>
            public static BooleanResult Different(params MiscListResult[] @values)
            {
                return new BooleanResult(t => t.Fn_ApocColl_Different(@values.Length), @values, null);
            }
            /// <summary>
            /// apoc.coll.disjunction(first, second) - returns the disjunct set of the two lists
            /// </summary>
            public static MiscListResult Disjunction(MiscListResult @first, MiscListResult @second)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Disjunction, new object[] { @first, @second }, null);
            }
            /// <summary>
            /// apoc.coll.dropDuplicateNeighbors(list) - remove duplicate consecutive objects in a list
            /// </summary>
            public static MiscListResult DropDuplicateNeighbors(MiscListResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocColl_DropDuplicateNeighbors, new object[] { @list }, null);
            }
            /// <summary>
            /// apoc.coll.duplicates(coll) - returns a list of duplicate items in the collection
            /// </summary>
            public static MiscListResult Duplicates(MiscListResult @coll)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Duplicates, new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.duplicatesWithCount(coll) - returns a list of duplicate items in the collection and their count, keyed by item and count (e.g., [{item: xyz, count:2}, {item:zyx, count:5}])
            /// </summary>
            public static MiscListResult DuplicatesWithCount(MiscListResult @coll)
            {
                return new MiscListResult(t => t.Fn_ApocColl_DuplicatesWithCount, new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.elements(list,limit,offset) yield _1,_2,..,_10,_1s,_2i,_3f,_4m,_5l,_6n,_7r,_8p - deconstruct subset of mixed list into identifiers of the correct type
            /// </summary>
            public static MiscResult Elements(NumericResult @limit, NumericResult @offset, MiscListResult @values)
            {
                return new MiscResult(t => t.Call_ApocColl_Elements, new object[] { @limit, @offset, @values }, null);
            }
            /// <summary>
            /// apoc.coll.fill(item, count) - returns a list with the given count of items
            /// </summary>
            public static MiscListResult Fill(NumericResult @count, StringResult @item)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Fill, new object[] { @count, @item }, null);
            }
            /// <summary>
            /// apoc.coll.flatten(coll, [recursive]) - flattens list (nested if recursive is true)
            /// </summary>
            public static MiscListResult Flatten(MiscListResult @coll)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Flatten(1), new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.flatten(coll, [recursive]) - flattens list (nested if recursive is true)
            /// </summary>
            public static MiscListResult Flatten(MiscListResult @coll, BooleanResult @recursive)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Flatten(2), new object[] { @coll, @recursive }, null);
            }
            /// <summary>
            /// apoc.coll.frequencies(coll) - returns a list of frequencies of the items in the collection, keyed by item and count (e.g., [{item: xyz, count:2}, {item:zyx, count:5}, {item:abc, count:1}])
            /// </summary>
            public static MiscListResult Frequencies(MiscListResult @coll)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Frequencies, new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.frequenciesAsMap(coll) - return a map of frequencies of the items in the collection, key item, value count (e.g., {1:2, 2:1})
            /// </summary>
            public static MiscResult FrequenciesAsMap(MiscListResult @coll)
            {
                return new MiscResult(t => t.Fn_ApocColl_FrequenciesAsMap, new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.indexOf(coll, value) | position of value in the list
            /// </summary>
            public static NumericResult IndexOf(MiscListResult @coll, MiscResult @value)
            {
                return new NumericResult(t => t.Fn_ApocColl_IndexOf, new object[] { @coll, @value }, null);
            }
            /// <summary>
            /// apoc.coll.insertAll(coll, index, values) | insert values at index
            /// </summary>
            public static MiscListResult InsertAll(MiscListResult @coll, NumericResult @index, MiscListResult @values)
            {
                return new MiscListResult(t => t.Fn_ApocColl_InsertAll, new object[] { @coll, @index, @values }, null);
            }
            /// <summary>
            /// apoc.coll.intersection(first, second) - returns the unique intersection of the two lists
            /// </summary>
            public static MiscListResult Intersection(MiscListResult @first, MiscListResult @second)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Intersection, new object[] { @first, @second }, null);
            }
            /// <summary>
            /// apoc.coll.isEqualCollection(coll, values) return true if two collections contain the same elements with the same cardinality in any order (using a HashMap)
            /// </summary>
            public static BooleanResult IsEqualCollection(MiscListResult @coll, MiscListResult @values)
            {
                return new BooleanResult(t => t.Fn_ApocColl_IsEqualCollection, new object[] { @coll, @values }, null);
            }
            /// <summary>
            /// apoc.coll.max([0.5,1,2.3])
            /// </summary>
            public static MiscResult Max(params MiscListResult[] @values)
            {
                return new MiscResult(t => t.Fn_ApocColl_Max(@values.Length), @values, null);
            }
            /// <summary>
            /// apoc.coll.min([0.5,1,2.3])
            /// </summary>
            public static MiscResult Min(params MiscListResult[] @values)
            {
                return new MiscResult(t => t.Fn_ApocColl_Min(@values.Length), @values, null);
            }
            /// <summary>
            /// apoc.coll.occurrences(coll, item) - returns the count of the given item in the collection
            /// </summary>
            public static NumericResult Occurrences(MiscListResult @coll, MiscResult @item)
            {
                return new NumericResult(t => t.Fn_ApocColl_Occurrences, new object[] { @coll, @item }, null);
            }
            /// <summary>
            /// apoc.coll.pairs([1,2,3]) returns [1,2],[2,3],[3,null]
            /// </summary>
            public static MiscListResult Pairs(MiscListResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Pairs, new object[] { @list }, null);
            }
            /// <summary>
            /// apoc.coll.pairsMin([1,2,3]) returns [1,2],[2,3]
            /// </summary>
            public static MiscListResult PairsMin(MiscListResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocColl_PairsMin, new object[] { @list }, null);
            }
            /// <summary>
            /// apoc.coll.pairWithOffset(values, offset) - returns a list of pairs defined by the offset
            /// </summary>
            public static MiscResult PairWithOffset(NumericResult @offset, MiscListResult @values)
            {
                return new MiscResult(t => t.Call_ApocColl_PairWithOffset, new object[] { @offset, @values }, null);
            }
            /// <summary>
            /// apoc.coll.partition(list,batchSize)
            /// </summary>
            public static MiscResult Partition(NumericResult @batchSize, MiscListResult @values)
            {
                return new MiscResult(t => t.Call_ApocColl_Partition, new object[] { @batchSize, @values }, null);
            }
            /// <summary>
            /// apoc.coll.randomItem(coll)- returns a random item from the list, or null on an empty or null list
            /// </summary>
            public static MiscResult RandomItem(MiscListResult @coll)
            {
                return new MiscResult(t => t.Fn_ApocColl_RandomItem, new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.randomItems(coll, itemCount, allowRepick: false) - returns a list of itemCount random items from the original list, optionally allowing picked elements to be picked again
            /// </summary>
            public static MiscListResult RandomItems(BooleanResult @allowRepick, MiscListResult @coll, NumericResult @itemCount)
            {
                return new MiscListResult(t => t.Fn_ApocColl_RandomItems, new object[] { @allowRepick, @coll, @itemCount }, null);
            }
            /// <summary>
            /// apoc.coll.removeAll(first, second) - returns first list with all elements of second list removed
            /// </summary>
            public static MiscListResult RemoveAll(MiscListResult @first, MiscListResult @second)
            {
                return new MiscListResult(t => t.Fn_ApocColl_RemoveAll, new object[] { @first, @second }, null);
            }
            /// <summary>
            /// apoc.coll.reverse(coll) - returns reversed list
            /// </summary>
            public static MiscListResult Reverse(MiscListResult @coll)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Reverse, new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.runningTotal(list1) - returns an accumulative array. For example apoc.coll.runningTotal([1,2,3.5]) return [1,3,6.5]
            /// </summary>
            public static MiscListResult RunningTotal(FloatListResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocColl_RunningTotal, new object[] { @list }, null);
            }
            /// <summary>
            /// apoc.coll.set(coll, index, value) | set index to value
            /// </summary>
            public static MiscListResult Set(MiscListResult @coll, NumericResult @index, MiscResult @value)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Set, new object[] { @coll, @index, @value }, null);
            }
            /// <summary>
            /// apoc.coll.shuffle(coll) - returns the shuffled list
            /// </summary>
            public static MiscListResult Shuffle(MiscListResult @coll)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Shuffle, new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.sort(coll) sort on Collections
            /// </summary>
            public static MiscListResult Sort(MiscListResult @coll)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Sort, new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.sortMaps([maps], &apos;name&apos;) - sort maps by property
            /// </summary>
            public static MiscListResult SortMaps(MiscListResult @coll, StringResult @prop)
            {
                return new MiscListResult(t => t.Fn_ApocColl_SortMaps, new object[] { @coll, @prop }, null);
            }
            /// <summary>
            /// apoc.coll.sortMulti(coll, [&apos;^name&apos;,&apos;age&apos;],[limit],[skip]) - sort list of maps by several sort fields (ascending with ^ prefix) and optionally applies limit and skip
            /// </summary>
            public static MiscListResult SortMulti(MiscListResult @coll)
            {
                return new MiscListResult(t => t.Fn_ApocColl_SortMulti(1), new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.sortMulti(coll, [&apos;^name&apos;,&apos;age&apos;],[limit],[skip]) - sort list of maps by several sort fields (ascending with ^ prefix) and optionally applies limit and skip
            /// </summary>
            public static MiscListResult SortMulti(MiscListResult @coll, NumericResult @limit)
            {
                return new MiscListResult(t => t.Fn_ApocColl_SortMulti(2), new object[] { @coll, @limit }, null);
            }
            /// <summary>
            /// apoc.coll.sortMulti(coll, [&apos;^name&apos;,&apos;age&apos;],[limit],[skip]) - sort list of maps by several sort fields (ascending with ^ prefix) and optionally applies limit and skip
            /// </summary>
            public static MiscListResult SortMulti(MiscListResult @coll, NumericResult @limit, StringListResult @orderFields)
            {
                return new MiscListResult(t => t.Fn_ApocColl_SortMulti(3), new object[] { @coll, @limit, @orderFields }, null);
            }
            /// <summary>
            /// apoc.coll.sortMulti(coll, [&apos;^name&apos;,&apos;age&apos;],[limit],[skip]) - sort list of maps by several sort fields (ascending with ^ prefix) and optionally applies limit and skip
            /// </summary>
            public static MiscListResult SortMulti(MiscListResult @coll, NumericResult @limit, StringListResult @orderFields, NumericResult @skip)
            {
                return new MiscListResult(t => t.Fn_ApocColl_SortMulti(4), new object[] { @coll, @limit, @orderFields, @skip }, null);
            }
            /// <summary>
            /// apoc.coll.sortNodes([nodes], &apos;name&apos;) sort nodes by property
            /// </summary>
            public static MiscListResult SortNodes(AliasListResult @coll, StringResult @prop)
            {
                return new MiscListResult(t => t.Fn_ApocColl_SortNodes, new object[] { @coll, @prop }, null);
            }
            /// <summary>
            /// apoc.coll.sortText(coll) sort on string based collections
            /// </summary>
            public static MiscListResult SortText(StringListResult @coll)
            {
                return new MiscListResult(t => t.Fn_ApocColl_SortText(1), new object[] { @coll }, null);
            }
            /// <summary>
            /// apoc.coll.sortText(coll) sort on string based collections
            /// </summary>
            public static MiscListResult SortText(StringListResult @coll, MiscResult @conf)
            {
                return new MiscListResult(t => t.Fn_ApocColl_SortText(2), new object[] { @coll, @conf }, null);
            }
            /// <summary>
            /// apoc.coll.split(list,value) | splits collection on given values rows of lists, value itself will not be part of resulting lists
            /// </summary>
            public static MiscListResult Split(MiscResult @value, MiscListResult @values)
            {
                return new MiscListResult(t => t.Call_ApocColl_Split, new object[] { @value, @values }, null);
            }
            /// <summary>
            /// apoc.coll.stdev(list, isBiasCorrected) - returns the sample or population standard deviation with isBiasCorrected true or false respectively. For example apoc.coll.stdev([10, 12, 23]) return 7
            /// </summary>
            public static FloatResult Stdev(BooleanResult @isBiasCorrected, FloatListResult @list)
            {
                return new FloatResult(t => t.Fn_ApocColl_Stdev, new object[] { @isBiasCorrected, @list }, null);
            }
            /// <summary>
            /// apoc.coll.sum([0.5,1,2.3])
            /// </summary>
            public static FloatResult Sum(FloatListResult @numbers)
            {
                return new FloatResult(t => t.Fn_ApocColl_Sum, new object[] { @numbers }, null);
            }
            /// <summary>
            /// apoc.coll.sumLongs([1,3,3])
            /// </summary>
            public static NumericResult SumLongs(FloatListResult @numbers)
            {
                return new NumericResult(t => t.Fn_ApocColl_SumLongs, new object[] { @numbers }, null);
            }
            /// <summary>
            /// apoc.coll.toSet([list]) returns a unique list backed by a set
            /// </summary>
            public static MiscListResult ToSet(params MiscListResult[] @values)
            {
                return new MiscListResult(t => t.Fn_ApocColl_ToSet(@values.Length), @values, null);
            }
            /// <summary>
            /// apoc.coll.union(first, second) - creates the distinct union of the 2 lists
            /// </summary>
            public static MiscListResult Union(MiscListResult @first, MiscListResult @second)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Union, new object[] { @first, @second }, null);
            }
            /// <summary>
            /// apoc.coll.unionAll(first, second) - creates the full union with duplicates of the two lists
            /// </summary>
            public static MiscListResult UnionAll(MiscListResult @first, MiscListResult @second)
            {
                return new MiscListResult(t => t.Fn_ApocColl_UnionAll, new object[] { @first, @second }, null);
            }
            /// <summary>
            /// apoc.coll.zip([list1],[list2])
            /// </summary>
            public static MiscListResult Zip(MiscListResult @list1, MiscListResult @list2)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Zip, new object[] { @list1, @list2 }, null);
            }
            /// <summary>
            /// apoc.coll.zipToRows(list1,list2) - creates pairs like zip but emits one row per pair
            /// </summary>
            public static MiscListResult ZipToRows(MiscListResult @list1, MiscListResult @list2)
            {
                return new MiscListResult(t => t.Call_ApocColl_ZipToRows, new object[] { @list1, @list2 }, null);
            }
            /// <summary>
            /// apoc.coll.insert(coll, index, value) | insert value at index
            /// </summary>
            public static MiscListResult Insert(MiscListResult @coll, NumericResult @index, MiscResult @value)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Insert, new object[] { @coll, @index, @value }, null);
            }
            /// <summary>
            /// apoc.coll.remove(coll, index, [length=1]) | remove range of values from index to length
            /// </summary>
            public static MiscListResult Remove(MiscListResult @coll, NumericResult @index)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Remove(2), new object[] { @coll, @index }, null);
            }
            /// <summary>
            /// apoc.coll.remove(coll, index, [length=1]) | remove range of values from index to length
            /// </summary>
            public static MiscListResult Remove(MiscListResult @coll, NumericResult @index, NumericResult @length)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Remove(3), new object[] { @coll, @index, @length }, null);
            }
            /// <summary>
            /// apoc.coll.subtract(first, second) - returns unique set of first list with all elements of second list removed
            /// </summary>
            public static MiscListResult Subtract(MiscListResult @first, MiscListResult @second)
            {
                return new MiscListResult(t => t.Fn_ApocColl_Subtract, new object[] { @first, @second }, null);
            }
        }

        public static partial class Convert
        {
            /// <summary>
            /// apoc.convert.fromJsonList(&apos;[1,2,3]&apos;[,&apos;json-path&apos;, &apos;path-options&apos;])
            /// </summary>
            public static MiscListResult FromJsonList(StringResult @list, StringResult @path)
            {
                return new MiscListResult(t => t.Fn_ApocConvert_FromJsonList(2), new object[] { @list, @path }, null);
            }
            /// <summary>
            /// apoc.convert.fromJsonList(&apos;[1,2,3]&apos;[,&apos;json-path&apos;, &apos;path-options&apos;])
            /// </summary>
            public static MiscListResult FromJsonList(StringResult @list, StringResult @path, StringListResult @pathOptions)
            {
                return new MiscListResult(t => t.Fn_ApocConvert_FromJsonList(3), new object[] { @list, @path, @pathOptions }, null);
            }
            /// <summary>
            /// apoc.convert.fromJsonMap(&apos;{&quot;a&quot;:42,&quot;b&quot;:&quot;foo&quot;,&quot;c&quot;:[1,2,3]}&apos;[,&apos;json-path&apos;, &apos;path-options&apos;])
            /// </summary>
            public static MiscResult FromJsonMap(StringResult @map, StringResult @path)
            {
                return new MiscResult(t => t.Fn_ApocConvert_FromJsonMap(2), new object[] { @map, @path }, null);
            }
            /// <summary>
            /// apoc.convert.fromJsonMap(&apos;{&quot;a&quot;:42,&quot;b&quot;:&quot;foo&quot;,&quot;c&quot;:[1,2,3]}&apos;[,&apos;json-path&apos;, &apos;path-options&apos;])
            /// </summary>
            public static MiscResult FromJsonMap(StringResult @map, StringResult @path, StringListResult @pathOptions)
            {
                return new MiscResult(t => t.Fn_ApocConvert_FromJsonMap(3), new object[] { @map, @path, @pathOptions }, null);
            }
            /// <summary>
            /// apoc.convert.getJsonProperty(node,key[,&apos;json-path&apos;, &apos;path-options&apos;]) - converts serialized JSON in property back to original object
            /// </summary>
            public static MiscResult GetJsonProperty(StringResult @key, AliasResult @node, StringResult @path)
            {
                return new MiscResult(t => t.Fn_ApocConvert_GetJsonProperty(3), new object[] { @key, @node, @path }, null);
            }
            /// <summary>
            /// apoc.convert.getJsonProperty(node,key[,&apos;json-path&apos;, &apos;path-options&apos;]) - converts serialized JSON in property back to original object
            /// </summary>
            public static MiscResult GetJsonProperty(StringResult @key, AliasResult @node, StringResult @path, StringListResult @pathOptions)
            {
                return new MiscResult(t => t.Fn_ApocConvert_GetJsonProperty(4), new object[] { @key, @node, @path, @pathOptions }, null);
            }
            /// <summary>
            /// apoc.convert.getJsonPropertyMap(node,key[,&apos;json-path&apos;, &apos;path-options&apos;]) - converts serialized JSON in property back to map
            /// </summary>
            public static MiscResult GetJsonPropertyMap(StringResult @key, AliasResult @node, StringResult @path)
            {
                return new MiscResult(t => t.Fn_ApocConvert_GetJsonPropertyMap(3), new object[] { @key, @node, @path }, null);
            }
            /// <summary>
            /// apoc.convert.getJsonPropertyMap(node,key[,&apos;json-path&apos;, &apos;path-options&apos;]) - converts serialized JSON in property back to map
            /// </summary>
            public static MiscResult GetJsonPropertyMap(StringResult @key, AliasResult @node, StringResult @path, StringListResult @pathOptions)
            {
                return new MiscResult(t => t.Fn_ApocConvert_GetJsonPropertyMap(4), new object[] { @key, @node, @path, @pathOptions }, null);
            }
            /// <summary>
            /// apoc.convert.setJsonProperty(node,key,complexValue) - sets value serialized to JSON as property with the given name on the node
            /// </summary>
            public static MiscResult SetJsonProperty(StringResult @key, AliasResult @node, MiscResult @value)
            {
                return new MiscResult(t => t.Call_ApocConvert_SetJsonProperty, new object[] { @key, @node, @value }, null);
            }
            /// <summary>
            /// apoc.convert.toBoolean(value) | tries it’s best to convert the value to a boolean
            /// </summary>
            public static BooleanResult ToBoolean(MiscResult @bool)
            {
                return new BooleanResult(t => t.Fn_ApocConvert_ToBoolean, new object[] { @bool }, null);
            }
            /// <summary>
            /// apoc.convert.toBooleanList(value) | tries it’s best to convert the value to a list of booleans
            /// </summary>
            public static MiscListResult ToBooleanList(MiscResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocConvert_ToBooleanList, new object[] { @list }, null);
            }
            /// <summary>
            /// apoc.convert.toFloat(value) | tries it’s best to convert the value to a float
            /// </summary>
            public static FloatResult ToFloat(MiscResult @object)
            {
                return new FloatResult(t => t.Fn_ApocConvert_ToFloat, new object[] { @object }, null);
            }
            /// <summary>
            /// apoc.convert.toInteger(value) | tries it’s best to convert the value to an integer
            /// </summary>
            public static NumericResult ToInteger(MiscResult @object)
            {
                return new NumericResult(t => t.Fn_ApocConvert_ToInteger, new object[] { @object }, null);
            }
            /// <summary>
            /// apoc.convert.toIntList(value) | tries it’s best to convert the value to a list of integers
            /// </summary>
            public static MiscListResult ToIntList(MiscResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocConvert_ToIntList, new object[] { @list }, null);
            }
            /// <summary>
            /// apoc.convert.toJson([1,2,3]) or toJson({a:42,b:&quot;foo&quot;,c:[1,2,3]}) or toJson(NODE/REL/PATH)
            /// </summary>
            public static StringResult ToJson(MiscResult @value)
            {
                return new StringResult(t => t.Fn_ApocConvert_ToJson, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.convert.toList(value) | tries it’s best to convert the value to a list
            /// </summary>
            public static MiscListResult ToList(MiscResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocConvert_ToList, new object[] { @list }, null);
            }
            /// <summary>
            /// apoc.convert.toMap(value) | tries it’s best to convert the value to a map
            /// </summary>
            public static MiscResult ToMap(MiscResult @map)
            {
                return new MiscResult(t => t.Fn_ApocConvert_ToMap, new object[] { @map }, null);
            }
            /// <summary>
            /// apoc.convert.toNode(value) | tries it’s best to convert the value to a node
            /// </summary>
            public static AliasResult ToNode(MiscResult @node)
            {
                return new AliasResult(t => t.Fn_ApocConvert_ToNode, new object[] { @node }, null);
            }
            /// <summary>
            /// apoc.convert.toNodeList(value) | tries it’s best to convert the value to a list of nodes
            /// </summary>
            public static MiscListResult ToNodeList(MiscResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocConvert_ToNodeList, new object[] { @list }, null);
            }
            /// <summary>
            /// apoc.convert.toRelationship(value) | tries it’s best to convert the value to a relationship
            /// </summary>
            public static AliasResult ToRelationship(MiscResult @relationship)
            {
                return new AliasResult(t => t.Fn_ApocConvert_ToRelationship, new object[] { @relationship }, null);
            }
            /// <summary>
            /// apoc.convert.toRelationshipList(value) | tries it’s best to convert the value to a list of relationships
            /// </summary>
            public static MiscListResult ToRelationshipList(MiscResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocConvert_ToRelationshipList, new object[] { @list }, null);
            }
            /// <summary>
            /// apoc.convert.toSortedJsonMap(node|map, ignoreCase:true) - returns a JSON map with keys sorted alphabetically, with optional case sensitivity
            /// </summary>
            public static StringResult ToSortedJsonMap(BooleanResult @ignoreCase, MiscResult @value)
            {
                return new StringResult(t => t.Fn_ApocConvert_ToSortedJsonMap, new object[] { @ignoreCase, @value }, null);
            }
            /// <summary>
            /// apoc.convert.toString(value) | tries it’s best to convert the value to a string
            /// </summary>
            public static StringResult ToString(MiscResult @string)
            {
                return new StringResult(t => t.Fn_ApocConvert_ToString, new object[] { @string }, null);
            }
            /// <summary>
            /// apoc.convert.toStringList(value) | tries it’s best to convert the value to a list of strings
            /// </summary>
            public static MiscListResult ToStringList(MiscResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocConvert_ToStringList, new object[] { @list }, null);
            }
            /// <summary>
            /// apoc.convert.toTree([paths],[lowerCaseRels=true], [config]) creates a stream of nested documents representing the at least one root of these paths
            /// </summary>
            public static MiscResult ToTree(MiscResult @config, BooleanResult @lowerCaseRels, PathListResult @paths)
            {
                return new MiscResult(t => t.Call_ApocConvert_ToTree, new object[] { @config, @lowerCaseRels, @paths }, null);
            }
            /// <summary>
            /// apoc.convert.toSet(value) | tries it’s best to convert the value to a set
            /// </summary>
            public static MiscListResult ToSet(MiscResult @list)
            {
                return new MiscListResult(t => t.Fn_ApocConvert_ToSet, new object[] { @list }, null);
            }
        }

        public static partial class Create
        {
            /// <summary>
            /// apoc.create.addLabels( [node,id,ids,nodes], [&apos;Label&apos;,…​]) - adds the given labels to the node or nodes
            /// </summary>
            public static AliasResult AddLabels(StringListResult @label, MiscResult @nodes)
            {
                return new AliasResult(t => t.Call_ApocCreate_AddLabels, new object[] { @label, @nodes }, null);
            }
            /// <summary>
            /// apoc.create.clonePathsToVirtual
            /// </summary>
            public static PathResult ClonePathsToVirtual(PathListResult @paths)
            {
                return new PathResult(t => t.Call_ApocCreate_ClonePathsToVirtual, new object[] { @paths }, null);
            }
            /// <summary>
            /// apoc.create.clonePathToVirtual
            /// </summary>
            public static PathResult ClonePathToVirtual(PathResult @path)
            {
                return new PathResult(t => t.Call_ApocCreate_ClonePathToVirtual, new object[] { @path }, null);
            }
            /// <summary>
            /// apoc.create.node([&apos;Label&apos;], {key:value,…​}) - create node with dynamic labels
            /// </summary>
            public static AliasResult Node(StringListResult @label, MiscResult @props)
            {
                return new AliasResult(t => t.Call_ApocCreate_Node, new object[] { @label, @props }, null);
            }
            /// <summary>
            /// apoc.create.nodes([&apos;Label&apos;], [{key:value,…​}]) create multiple nodes with dynamic labels
            /// </summary>
            public static AliasResult Nodes(StringListResult @label, MiscListResult @props)
            {
                return new AliasResult(t => t.Call_ApocCreate_Nodes, new object[] { @label, @props }, null);
            }
            /// <summary>
            /// apoc.create.relationship(person1,&apos;KNOWS&apos;,{key:value,…​}, person2) create relationship with dynamic rel-type
            /// </summary>
            public static AliasResult Relationship(AliasResult @from, MiscResult @props, StringResult @relType, AliasResult @to)
            {
                return new AliasResult(t => t.Call_ApocCreate_Relationship, new object[] { @from, @props, @relType, @to }, null);
            }
            /// <summary>
            /// apoc.create.removeLabels( [node,id,ids,nodes], [&apos;Label&apos;,…​]) - removes the given labels from the node or nodes
            /// </summary>
            public static AliasResult RemoveLabels(StringListResult @label, MiscResult @nodes)
            {
                return new AliasResult(t => t.Call_ApocCreate_RemoveLabels, new object[] { @label, @nodes }, null);
            }
            /// <summary>
            /// apoc.create.removeProperties( [node,id,ids,nodes], [keys]) - removes the given properties from the nodes(s)
            /// </summary>
            public static AliasResult RemoveProperties(StringListResult @keys, MiscResult @nodes)
            {
                return new AliasResult(t => t.Call_ApocCreate_RemoveProperties, new object[] { @keys, @nodes }, null);
            }
            /// <summary>
            /// apoc.create.removeRelProperties( [rel,id,ids,rels], [keys]) - removes the given properties from the relationship(s)
            /// </summary>
            public static AliasResult RemoveRelProperties(StringListResult @keys, MiscResult @rels)
            {
                return new AliasResult(t => t.Call_ApocCreate_RemoveRelProperties, new object[] { @keys, @rels }, null);
            }
            /// <summary>
            /// apoc.create.setLabels( [node,id,ids,nodes], [&apos;Label&apos;,…​]) - sets the given labels, non matching labels are removed on the node or nodes
            /// </summary>
            public static AliasResult SetLabels(StringListResult @label, MiscResult @nodes)
            {
                return new AliasResult(t => t.Call_ApocCreate_SetLabels, new object[] { @label, @nodes }, null);
            }
            /// <summary>
            /// apoc.create.setProperties( [node,id,ids,nodes], [keys], [values]) - sets the given properties on the nodes(s)
            /// </summary>
            public static AliasResult SetProperties(StringListResult @keys, MiscResult @nodes, MiscListResult @values)
            {
                return new AliasResult(t => t.Call_ApocCreate_SetProperties, new object[] { @keys, @nodes, @values }, null);
            }
            /// <summary>
            /// apoc.create.setProperty( [node,id,ids,nodes], key, value) - sets the given property on the node(s)
            /// </summary>
            public static AliasResult SetProperty(StringResult @key, MiscResult @nodes, MiscResult @value)
            {
                return new AliasResult(t => t.Call_ApocCreate_SetProperty, new object[] { @key, @nodes, @value }, null);
            }
            /// <summary>
            /// apoc.create.setRelProperties( [rel,id,ids,rels], [keys], [values]) - sets the given properties on the relationship(s)
            /// </summary>
            public static AliasResult SetRelProperties(StringListResult @keys, MiscResult @rels, MiscListResult @values)
            {
                return new AliasResult(t => t.Call_ApocCreate_SetRelProperties, new object[] { @keys, @rels, @values }, null);
            }
            /// <summary>
            /// apoc.create.setRelProperty( [rel,id,ids,rels], key, value) - sets the given property on the relationship(s)
            /// </summary>
            public static AliasResult SetRelProperty(StringResult @key, MiscResult @relationships, MiscResult @value)
            {
                return new AliasResult(t => t.Call_ApocCreate_SetRelProperty, new object[] { @key, @relationships, @value }, null);
            }
            /// <summary>
            /// apoc.create.uuid() - creates an UUID
            /// </summary>
            public static StringResult Uuid()
            {
                return new StringResult(t => t.Fn_ApocCreate_Uuid, new object[0], null);
            }
            /// <summary>
            /// apoc.create.uuids(count) yield uuid - creates &apos;count&apos; UUIDs
            /// </summary>
            public static NumericResult Uuids(NumericResult @count)
            {
                return new NumericResult(t => t.Call_ApocCreate_Uuids, new object[] { @count }, null);
            }
            /// <summary>
            /// apoc.create.virtualPath([&apos;LabelA&apos;],{key:value},&apos;KNOWS&apos;,{key:value,…​},[&apos;LabelB&apos;],{key:value}) returns a virtual path of nodes joined by a relationship and the associated properties
            /// </summary>
            public static AliasResult VirtualPath(StringListResult @labelsM, StringListResult @labelsN, MiscResult @m, MiscResult @n, MiscResult @props, StringResult @relType)
            {
                return new AliasResult(t => t.Call_ApocCreate_VirtualPath, new object[] { @labelsM, @labelsN, @m, @n, @props, @relType }, null);
            }
            /// <summary>
            /// apoc.create.vNode([&apos;Label&apos;], {key:value,…​}) returns a virtual node
            /// </summary>
            public static MiscResult VNode(StringListResult @label)
            {
                return new MiscResult(t => t.Call_ApocCreate_VNode(1), new object[] { @label }, null);
            }
            /// <summary>
            /// apoc.create.vNode([&apos;Label&apos;], {key:value,…​}) returns a virtual node
            /// </summary>
            public static MiscResult VNode(StringListResult @label, MiscResult @props)
            {
                return new MiscResult(t => t.Call_ApocCreate_VNode(2), new object[] { @label, @props }, null);
            }
            /// <summary>
            /// apoc.create.vNodes([&apos;Label&apos;], [{key:value,…​}]) returns virtual nodes
            /// </summary>
            public static AliasResult VNodes(StringListResult @label, MiscListResult @props)
            {
                return new AliasResult(t => t.Call_ApocCreate_VNodes, new object[] { @label, @props }, null);
            }
            /// <summary>
            /// apoc.create.vPattern({_labels:[&apos;LabelA&apos;],key:value},&apos;KNOWS&apos;,{key:value,…​}, {_labels:[&apos;LabelB&apos;],key:value}) returns a virtual pattern
            /// </summary>
            public static AliasResult VPattern(MiscResult @from, MiscResult @props, StringResult @relType, MiscResult @to)
            {
                return new AliasResult(t => t.Call_ApocCreate_VPattern, new object[] { @from, @props, @relType, @to }, null);
            }
            /// <summary>
            /// apoc.create.vPatternFull([&apos;LabelA&apos;],{key:value},&apos;KNOWS&apos;,{key:value,…​},[&apos;LabelB&apos;],{key:value}) returns a virtual pattern
            /// </summary>
            public static AliasResult VPatternFull(StringListResult @labelsM, StringListResult @labelsN, MiscResult @m, MiscResult @n, MiscResult @props, StringResult @relType)
            {
                return new AliasResult(t => t.Call_ApocCreate_VPatternFull, new object[] { @labelsM, @labelsN, @m, @n, @props, @relType }, null);
            }
            /// <summary>
            /// apoc.create.vRelationship(nodeFrom,&apos;KNOWS&apos;,{key:value,…​}, nodeTo) returns a virtual relationship
            /// </summary>
            public static MiscResult VRelationship(AliasResult @from, MiscResult @props, StringResult @relType, AliasResult @to)
            {
                return new MiscResult(t => t.Call_ApocCreate_VRelationship, new object[] { @from, @props, @relType, @to }, null);
            }
        }

        public static partial class CreateVirtual
        {
            /// <summary>
            /// apoc.create.virtual.fromNode(node, [propertyNames]) returns a virtual node built from an existing node with only the requested properties
            /// </summary>
            public static AliasResult FromNode(AliasResult @node, StringListResult @propertyNames)
            {
                return new AliasResult(t => t.Fn_ApocCreateVirtual_FromNode, new object[] { @node, @propertyNames }, null);
            }
        }

        public static partial class Cypher
        {
            /// <summary>
            /// apoc.cypher.doIt(fragment, params) yield value - executes writing fragment with the given parameters
            /// </summary>
            public static MiscResult DoIt(StringResult @cypher, MiscResult @params)
            {
                return new MiscResult(t => t.Call_ApocCypher_DoIt, new object[] { @cypher, @params }, null);
            }
            /// <summary>
            /// apoc.cypher.run(fragment, params) yield value - executes reading fragment with the given parameters - currently no schema operations
            /// </summary>
            public static MiscResult Run(StringResult @cypher, MiscResult @params)
            {
                return new MiscResult(t => t.Call_ApocCypher_Run, new object[] { @cypher, @params }, null);
            }
            /// <summary>
            /// use either apoc.cypher.runFirstColumnMany for a list return or apoc.cypher.runFirstColumnSingle for returning the first row of the first column
            /// </summary>
            public static MiscResult RunFirstColumn(StringResult @cypher, BooleanResult @expectMultipleValues, MiscResult @params)
            {
                return new MiscResult(t => t.Fn_ApocCypher_RunFirstColumn, new object[] { @cypher, @expectMultipleValues, @params }, null);
            }
            /// <summary>
            /// apoc.cypher.runFirstColumnMany(statement, params) - executes statement with given parameters, returns first column only collected into a list, params are available as identifiers
            /// </summary>
            public static MiscListResult RunFirstColumnMany(StringResult @cypher, MiscResult @params)
            {
                return new MiscListResult(t => t.Fn_ApocCypher_RunFirstColumnMany, new object[] { @cypher, @params }, null);
            }
            /// <summary>
            /// apoc.cypher.runFirstColumnSingle(statement, params) - executes statement with given parameters, returns first element of the first column only, params are available as identifiers
            /// </summary>
            public static MiscResult RunFirstColumnSingle(StringResult @cypher, MiscResult @params)
            {
                return new MiscResult(t => t.Fn_ApocCypher_RunFirstColumnSingle, new object[] { @cypher, @params }, null);
            }
            /// <summary>
            /// apoc.cypher.runMany(&apos;cypher;\nstatements;&apos;, $params, [{statistics:true,timeout:10}]) - runs each semicolon separated statement and returns summary - currently no schema operations
            /// </summary>
            public static MiscResult RunMany(MiscResult @config, StringResult @cypher, MiscResult @params)
            {
                return new MiscResult(t => t.Call_ApocCypher_RunMany, new object[] { @config, @cypher, @params }, null);
            }
            /// <summary>
            /// apoc.cypher.runManyReadOnly(&apos;cypher;\nstatements;&apos;, $params, [{statistics:true,timeout:10}]) - runs each semicolon separated, read-only statement and returns summary - currently no schema operations
            /// </summary>
            public static MiscResult RunManyReadOnly(MiscResult @config, StringResult @cypher, MiscResult @params)
            {
                return new MiscResult(t => t.Call_ApocCypher_RunManyReadOnly, new object[] { @config, @cypher, @params }, null);
            }
            /// <summary>
            /// apoc.cypher.runSchema(statement, params) yield value - executes query schema statement with the given parameters
            /// </summary>
            public static MiscResult RunSchema(StringResult @cypher, MiscResult @params)
            {
                return new MiscResult(t => t.Call_ApocCypher_RunSchema, new object[] { @cypher, @params }, null);
            }
            /// <summary>
            /// apoc.cypher.runTimeboxed(&apos;cypherStatement&apos;,{params}, timeout) - abort kernelTransaction after timeout ms if not finished
            /// </summary>
            public static MiscResult RunTimeboxed(StringResult @cypher, MiscResult @params, NumericResult @timeout)
            {
                return new MiscResult(t => t.Call_ApocCypher_RunTimeboxed, new object[] { @cypher, @params, @timeout }, null);
            }
            /// <summary>
            /// apoc.cypher.runWrite(statement, params) yield value - alias for apoc.cypher.doIt
            /// </summary>
            public static MiscResult RunWrite(StringResult @cypher, MiscResult @params)
            {
                return new MiscResult(t => t.Call_ApocCypher_RunWrite, new object[] { @cypher, @params }, null);
            }
        }

        public static partial class Data
        {
            /// <summary>
            /// apoc.data.domain(&apos;url_or_email_address&apos;) YIELD domain - extract the domain name from a url or an email address. If nothing was found, yield null.
            /// </summary>
            public static StringResult Domain(StringResult @url_or_email_address)
            {
                return new StringResult(t => t.Fn_ApocData_Domain, new object[] { @url_or_email_address }, null);
            }
            /// <summary>
            /// apoc.data.url(&apos;url&apos;) as {protocol,host,port,path,query,file,anchor,user} | turn URL into map structure
            /// </summary>
            public static MiscResult Url(StringResult @url)
            {
                return new MiscResult(t => t.Fn_ApocData_Url, new object[] { @url }, null);
            }
        }

        public static partial class Date
        {
            /// <summary>
            /// apoc.date.convert(12345, &apos;ms&apos;, &apos;d&apos;) - convert a timestamp in one time unit into one of a different time unit
            /// </summary>
            public static NumericResult Convert(NumericResult @time, StringResult @toUnit, StringResult @unit)
            {
                return new NumericResult(t => t.Fn_ApocDate_Convert, new object[] { @time, @toUnit, @unit }, null);
            }
            /// <summary>
            /// apoc.date.convertFormat(&apos;Tue, 14 May 2019 14:52:06 -0400&apos;, &apos;rfc_1123_date_time&apos;, &apos;iso_date_time&apos;) - convert a String of one date format into a String of another date format.
            /// </summary>
            public static StringResult ConvertFormat(StringResult @convertTo, StringResult @currentFormat, StringResult @temporal)
            {
                return new StringResult(t => t.Fn_ApocDate_ConvertFormat, new object[] { @convertTo, @currentFormat, @temporal }, null);
            }
            /// <summary>
            /// apoc.date.currentTimestamp() - returns System.currentTimeMillis() at the time it was called. The value is current throughout transaction execution, and is different from Cypher’s timestamp() function, which does not update within a transaction.
            /// </summary>
            public static NumericResult CurrentTimestamp()
            {
                return new NumericResult(t => t.Fn_ApocDate_CurrentTimestamp, new object[0], null);
            }
            /// <summary>
            /// apoc.date.field(12345,(&apos;ms|s|m|h|d|month|year&apos;),(&apos;TZ&apos;)
            /// </summary>
            public static NumericResult Field(NumericResult @time)
            {
                return new NumericResult(t => t.Fn_ApocDate_Field(1), new object[] { @time }, null);
            }
            /// <summary>
            /// apoc.date.field(12345,(&apos;ms|s|m|h|d|month|year&apos;),(&apos;TZ&apos;)
            /// </summary>
            public static NumericResult Field(NumericResult @time, StringResult @timezone)
            {
                return new NumericResult(t => t.Fn_ApocDate_Field(2), new object[] { @time, @timezone }, null);
            }
            /// <summary>
            /// apoc.date.field(12345,(&apos;ms|s|m|h|d|month|year&apos;),(&apos;TZ&apos;)
            /// </summary>
            public static NumericResult Field(NumericResult @time, StringResult @timezone, StringResult @unit)
            {
                return new NumericResult(t => t.Fn_ApocDate_Field(3), new object[] { @time, @timezone, @unit }, null);
            }
            /// <summary>
            /// apoc.date.fields(&apos;2012-12-23&apos;,(&apos;yyyy-MM-dd&apos;)) - return columns and a map representation of date parsed with the given format with entries for years,months,weekdays,days,hours,minutes,seconds,zoneid
            /// </summary>
            public static MiscResult Fields(StringResult @date)
            {
                return new MiscResult(t => t.Fn_ApocDate_Fields(1), new object[] { @date }, null);
            }
            /// <summary>
            /// apoc.date.fields(&apos;2012-12-23&apos;,(&apos;yyyy-MM-dd&apos;)) - return columns and a map representation of date parsed with the given format with entries for years,months,weekdays,days,hours,minutes,seconds,zoneid
            /// </summary>
            public static MiscResult Fields(StringResult @date, StringResult @pattern)
            {
                return new MiscResult(t => t.Fn_ApocDate_Fields(2), new object[] { @date, @pattern }, null);
            }
            /// <summary>
            /// apoc.date.format(12345,(&apos;ms|s|m|h|d&apos;),(&apos;yyyy-MM-dd HH:mm:ss zzz&apos;),(&apos;TZ&apos;)) - get string representation of time value optionally using the specified unit (default ms) using specified format (default ISO) and specified time zone (default current TZ)
            /// </summary>
            public static StringResult Format(StringResult @format, NumericResult @time, StringResult @timezone)
            {
                return new StringResult(t => t.Fn_ApocDate_Format(3), new object[] { @format, @time, @timezone }, null);
            }
            /// <summary>
            /// apoc.date.format(12345,(&apos;ms|s|m|h|d&apos;),(&apos;yyyy-MM-dd HH:mm:ss zzz&apos;),(&apos;TZ&apos;)) - get string representation of time value optionally using the specified unit (default ms) using specified format (default ISO) and specified time zone (default current TZ)
            /// </summary>
            public static StringResult Format(StringResult @format, NumericResult @time, StringResult @timezone, StringResult @unit)
            {
                return new StringResult(t => t.Fn_ApocDate_Format(4), new object[] { @format, @time, @timezone, @unit }, null);
            }
            /// <summary>
            /// apoc.date.fromISO8601(&apos;yyyy-MM-ddTHH:mm:ss.SSSZ&apos;) - return number representation of time in EPOCH format
            /// </summary>
            public static NumericResult FromISO8601(StringResult @time)
            {
                return new NumericResult(t => t.Fn_ApocDate_FromISO8601, new object[] { @time }, null);
            }
            /// <summary>
            /// apoc.date.parse(&apos;2012-12-23&apos;,&apos;ms|s|m|h|d&apos;,&apos;yyyy-MM-dd&apos;) - parse date string using the specified format into the specified time unit
            /// </summary>
            public static NumericResult Parse(StringResult @format, StringResult @time, StringResult @timezone)
            {
                return new NumericResult(t => t.Fn_ApocDate_Parse(3), new object[] { @format, @time, @timezone }, null);
            }
            /// <summary>
            /// apoc.date.parse(&apos;2012-12-23&apos;,&apos;ms|s|m|h|d&apos;,&apos;yyyy-MM-dd&apos;) - parse date string using the specified format into the specified time unit
            /// </summary>
            public static NumericResult Parse(StringResult @format, StringResult @time, StringResult @timezone, StringResult @unit)
            {
                return new NumericResult(t => t.Fn_ApocDate_Parse(4), new object[] { @format, @time, @timezone, @unit }, null);
            }
            /// <summary>
            /// apoc.date.parseAsZonedDateTime(&apos;2012-12-23 23:59:59&apos;,&apos;yyyy-MM-dd HH:mm:ss&apos;, &apos;UTC-hour-offset&apos;) - parse date string using the specified format to specified timezone
            /// </summary>
            public static MiscResult ParseAsZonedDateTime(StringResult @format, StringResult @time)
            {
                return new MiscResult(t => t.Fn_ApocDate_ParseAsZonedDateTime(2), new object[] { @format, @time }, null);
            }
            /// <summary>
            /// apoc.date.parseAsZonedDateTime(&apos;2012-12-23 23:59:59&apos;,&apos;yyyy-MM-dd HH:mm:ss&apos;, &apos;UTC-hour-offset&apos;) - parse date string using the specified format to specified timezone
            /// </summary>
            public static MiscResult ParseAsZonedDateTime(StringResult @format, StringResult @time, StringResult @timezone)
            {
                return new MiscResult(t => t.Fn_ApocDate_ParseAsZonedDateTime(3), new object[] { @format, @time, @timezone }, null);
            }
            /// <summary>
            /// apoc.date.systemTimezone() - returns the system timezone display name
            /// </summary>
            public static StringResult SystemTimezone()
            {
                return new StringResult(t => t.Fn_ApocDate_SystemTimezone, new object[0], null);
            }
            /// <summary>
            /// apoc.date.toISO8601(12345,(&apos;ms|s|m|h|d&apos;) - return string representation of time in ISO8601 format
            /// </summary>
            public static StringResult ToISO8601(NumericResult @time)
            {
                return new StringResult(t => t.Fn_ApocDate_ToISO8601(1), new object[] { @time }, null);
            }
            /// <summary>
            /// apoc.date.toISO8601(12345,(&apos;ms|s|m|h|d&apos;) - return string representation of time in ISO8601 format
            /// </summary>
            public static StringResult ToISO8601(NumericResult @time, StringResult @unit)
            {
                return new StringResult(t => t.Fn_ApocDate_ToISO8601(2), new object[] { @time, @unit }, null);
            }
            /// <summary>
            /// toYears(timestamp) or toYears(date[,format]) - converts timestamp into floating point years
            /// </summary>
            public static FloatResult ToYears(StringResult @format, MiscResult @value)
            {
                return new FloatResult(t => t.Fn_ApocDate_ToYears, new object[] { @format, @value }, null);
            }
            /// <summary>
            /// apoc.date.add(12345, &apos;ms&apos;, -365, &apos;d&apos;) - given a timestamp in one time unit, adds a value of the specified time unit
            /// </summary>
            public static NumericResult Add(StringResult @addUnit, NumericResult @addValue, NumericResult @time, StringResult @unit)
            {
                return new NumericResult(t => t.Fn_ApocDate_Add, new object[] { @addUnit, @addValue, @time, @unit }, null);
            }
        }

        public static partial class Diff
        {
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static MiscResult Nodes(AliasResult @leftNode, AliasResult @rightNode)
            {
                return new MiscResult(t => t.Fn_ApocDiff_Nodes, new object[] { @leftNode, @rightNode }, null);
            }
        }

        public static partial class Do
        {
            /// <summary>
            /// apoc.do.case([condition, query, condition, query, …​], elseQuery:&apos;&apos;, params:{}) yield value - given a list of conditional / writing query pairs, executes the query associated with the first conditional evaluating to true (or the else query if none are true) with the given parameters
            /// </summary>
            public static MiscResult Case(MiscListResult @conditionals, StringResult @elseQuery)
            {
                return new MiscResult(t => t.Call_ApocDo_Case(2), new object[] { @conditionals, @elseQuery }, null);
            }
            /// <summary>
            /// apoc.do.case([condition, query, condition, query, …​], elseQuery:&apos;&apos;, params:{}) yield value - given a list of conditional / writing query pairs, executes the query associated with the first conditional evaluating to true (or the else query if none are true) with the given parameters
            /// </summary>
            public static MiscResult Case(MiscListResult @conditionals, StringResult @elseQuery, MiscResult @params)
            {
                return new MiscResult(t => t.Call_ApocDo_Case(3), new object[] { @conditionals, @elseQuery, @params }, null);
            }
            /// <summary>
            /// apoc.do.when(condition, ifQuery, elseQuery:&apos;&apos;, params:{}) yield value - based on the conditional, executes writing ifQuery or elseQuery with the given parameters
            /// </summary>
            public static MiscResult When(BooleanResult @condition, StringResult @elseQuery, StringResult @ifQuery)
            {
                return new MiscResult(t => t.Call_ApocDo_When(3), new object[] { @condition, @elseQuery, @ifQuery }, null);
            }
            /// <summary>
            /// apoc.do.when(condition, ifQuery, elseQuery:&apos;&apos;, params:{}) yield value - based on the conditional, executes writing ifQuery or elseQuery with the given parameters
            /// </summary>
            public static MiscResult When(BooleanResult @condition, StringResult @elseQuery, StringResult @ifQuery, MiscResult @params)
            {
                return new MiscResult(t => t.Call_ApocDo_When(4), new object[] { @condition, @elseQuery, @ifQuery, @params }, null);
            }
        }

        public static partial class Dv
        {
            /// <summary>
            /// Query a virtualized resource by name and return virtual nodes linked using virtual rels to the node passed as first param
            /// </summary>
            public static PathResult QueryAndLink(MiscResult @config, StringResult @name, AliasResult @node, MiscResult @params, StringResult @relName)
            {
                return new PathResult(t => t.Call_ApocDv_QueryAndLink, new object[] { @config, @name, @node, @params, @relName }, null);
            }
            /// <summary>
            /// Query a virtualized resource by name and return virtual nodes
            /// </summary>
            public static AliasResult Query(MiscResult @config, StringResult @name)
            {
                return new AliasResult(t => t.Call_ApocDv_Query(2), new object[] { @config, @name }, null);
            }
            /// <summary>
            /// Query a virtualized resource by name and return virtual nodes
            /// </summary>
            public static AliasResult Query(MiscResult @config, StringResult @name, MiscResult @params)
            {
                return new AliasResult(t => t.Call_ApocDv_Query(3), new object[] { @config, @name, @params }, null);
            }
        }

        public static partial class DvCatalog
        {
            /// <summary>
            /// Add a virtualized resource configuration
            /// </summary>
            public static StringResult Add(MiscResult @config, StringResult @name)
            {
                return new StringResult(t => t.Call_ApocDvCatalog_Add, new object[] { @config, @name }, null);
            }
            /// <summary>
            /// List all virtualized resource configs
            /// </summary>
            public static StringResult List()
            {
                return new StringResult(t => t.Call_ApocDvCatalog_List, new object[0], null);
            }
            /// <summary>
            /// Remove a virtualized resource config by name
            /// </summary>
            public static StringResult Remove(StringResult @name)
            {
                return new StringResult(t => t.Call_ApocDvCatalog_Remove, new object[] { @name }, null);
            }
        }

        public static partial class Example
        {
            /// <summary>
            /// apoc.example.movies() | Creates the sample movies graph
            /// </summary>
            public static NumericResult Movies()
            {
                return new NumericResult(t => t.Call_ApocExample_Movies, new object[0], null);
            }
        }

        public static partial class Export
        {
            /// <summary>
            /// apoc.export.cypherAll(file,config) - exports whole database incl. indexes as cypher statements to the provided file
            /// </summary>
            public static NumericResult CypherAll(MiscResult @config, StringResult @file)
            {
                return new NumericResult(t => t.Call_ApocExport_CypherAll, new object[] { @config, @file }, null);
            }
            /// <summary>
            /// apoc.export.cypherData(nodes,rels,file,config) - exports given nodes and relationships incl. indexes as cypher statements to the provided file
            /// </summary>
            public static NumericResult CypherData(MiscResult @config, StringResult @file, AliasListResult @nodes, AliasListResult @rels)
            {
                return new NumericResult(t => t.Call_ApocExport_CypherData, new object[] { @config, @file, @nodes, @rels }, null);
            }
            /// <summary>
            /// apoc.export.cypherGraph(graph,file,config) - exports given graph object incl. indexes as cypher statements to the provided file
            /// </summary>
            public static NumericResult CypherGraph(MiscResult @config, StringResult @file, MiscResult @graph)
            {
                return new NumericResult(t => t.Call_ApocExport_CypherGraph, new object[] { @config, @file, @graph }, null);
            }
            /// <summary>
            /// apoc.export.cypherQuery(query,file,config) - exports nodes and relationships from the cypher kernelTransaction incl. indexes as cypher statements to the provided file
            /// </summary>
            public static NumericResult CypherQuery(MiscResult @config, StringResult @file, StringResult @query)
            {
                return new NumericResult(t => t.Call_ApocExport_CypherQuery, new object[] { @config, @file, @query }, null);
            }
        }

        public static partial class ExportCsv
        {
            /// <summary>
            /// apoc.export.csv.all(file,config) - exports whole database as csv to the provided file
            /// </summary>
            public static NumericResult All(MiscResult @config, StringResult @file)
            {
                return new NumericResult(t => t.Call_ApocExportCsv_All, new object[] { @config, @file }, null);
            }
            /// <summary>
            /// apoc.export.csv.data(nodes,rels,file,config) - exports given nodes and relationships as csv to the provided file
            /// </summary>
            public static NumericResult Data(MiscResult @config, StringResult @file, AliasListResult @nodes, AliasListResult @rels)
            {
                return new NumericResult(t => t.Call_ApocExportCsv_Data, new object[] { @config, @file, @nodes, @rels }, null);
            }
            /// <summary>
            /// apoc.export.csv.graph(graph,file,config) - exports given graph object as csv to the provided file
            /// </summary>
            public static NumericResult Graph(MiscResult @config, StringResult @file, MiscResult @graph)
            {
                return new NumericResult(t => t.Call_ApocExportCsv_Graph, new object[] { @config, @file, @graph }, null);
            }
            /// <summary>
            /// apoc.export.csv.query(query,file,{config,…​,params:{params}}) - exports results from the cypher statement as csv to the provided file
            /// </summary>
            public static NumericResult Query(MiscResult @config, StringResult @file, StringResult @query)
            {
                return new NumericResult(t => t.Call_ApocExportCsv_Query, new object[] { @config, @file, @query }, null);
            }
        }

        public static partial class ExportCypher
        {
            /// <summary>
            /// apoc.export.cypher.schema(file,config) - exports all schema indexes and constraints to cypher
            /// </summary>
            public static NumericResult Schema(MiscResult @config, StringResult @file)
            {
                return new NumericResult(t => t.Call_ApocExportCypher_Schema, new object[] { @config, @file }, null);
            }
            /// <summary>
            /// apoc.export.cypher.all(file,config) - exports whole database incl. indexes as cypher statements to the provided file
            /// </summary>
            public static NumericResult All(MiscResult @config, StringResult @file)
            {
                return new NumericResult(t => t.Call_ApocExportCypher_All, new object[] { @config, @file }, null);
            }
            /// <summary>
            /// apoc.export.cypher.data(nodes,rels,file,config) - exports given nodes and relationships incl. indexes as cypher statements to the provided file
            /// </summary>
            public static NumericResult Data(MiscResult @config, StringResult @file, AliasListResult @nodes, AliasListResult @rels)
            {
                return new NumericResult(t => t.Call_ApocExportCypher_Data, new object[] { @config, @file, @nodes, @rels }, null);
            }
            /// <summary>
            /// apoc.export.cypher.graph(graph,file,config) - exports given graph object incl. indexes as cypher statements to the provided file
            /// </summary>
            public static NumericResult Graph(MiscResult @config, StringResult @file, MiscResult @graph)
            {
                return new NumericResult(t => t.Call_ApocExportCypher_Graph, new object[] { @config, @file, @graph }, null);
            }
            /// <summary>
            /// apoc.export.cypher.query(query,file,config) - exports nodes and relationships from the cypher statement incl. indexes as cypher statements to the provided file
            /// </summary>
            public static NumericResult Query(MiscResult @config, StringResult @file, StringResult @query)
            {
                return new NumericResult(t => t.Call_ApocExportCypher_Query, new object[] { @config, @file, @query }, null);
            }
        }

        public static partial class ExportGraphml
        {
            /// <summary>
            /// apoc.export.graphml.all(file,config) - exports whole database as graphml to the provided file
            /// </summary>
            public static NumericResult All(MiscResult @config, StringResult @file)
            {
                return new NumericResult(t => t.Call_ApocExportGraphml_All, new object[] { @config, @file }, null);
            }
            /// <summary>
            /// apoc.export.graphml.data(nodes,rels,file,config) - exports given nodes and relationships as graphml to the provided file
            /// </summary>
            public static NumericResult Data(MiscResult @config, StringResult @file, AliasListResult @nodes, AliasListResult @rels)
            {
                return new NumericResult(t => t.Call_ApocExportGraphml_Data, new object[] { @config, @file, @nodes, @rels }, null);
            }
            /// <summary>
            /// apoc.export.graphml.graph(graph,file,config) - exports given graph object as graphml to the provided file
            /// </summary>
            public static NumericResult Graph(MiscResult @config, StringResult @file, MiscResult @graph)
            {
                return new NumericResult(t => t.Call_ApocExportGraphml_Graph, new object[] { @config, @file, @graph }, null);
            }
            /// <summary>
            /// apoc.export.graphml.query(query,file,config) - exports nodes and relationships from the cypher statement as graphml to the provided file
            /// </summary>
            public static NumericResult Query(MiscResult @config, StringResult @file, StringResult @query)
            {
                return new NumericResult(t => t.Call_ApocExportGraphml_Query, new object[] { @config, @file, @query }, null);
            }
        }

        public static partial class ExportJson
        {
            /// <summary>
            /// apoc.export.json.all(file,config) - exports whole database as json to the provided file
            /// </summary>
            public static NumericResult All(MiscResult @config, StringResult @file)
            {
                return new NumericResult(t => t.Call_ApocExportJson_All, new object[] { @config, @file }, null);
            }
            /// <summary>
            /// apoc.export.json.data(nodes,rels,file,config) - exports given nodes and relationships as json to the provided file
            /// </summary>
            public static NumericResult Data(MiscResult @config, StringResult @file, AliasListResult @nodes, AliasListResult @rels)
            {
                return new NumericResult(t => t.Call_ApocExportJson_Data, new object[] { @config, @file, @nodes, @rels }, null);
            }
            /// <summary>
            /// apoc.export.json.graph(graph,file,config) - exports given graph object as json to the provided file
            /// </summary>
            public static NumericResult Graph(MiscResult @config, StringResult @file, MiscResult @graph)
            {
                return new NumericResult(t => t.Call_ApocExportJson_Graph, new object[] { @config, @file, @graph }, null);
            }
            /// <summary>
            /// apoc.export.json.query(query,file,{config,…​,params:{params}}) - exports results from the cypher statement as json to the provided file
            /// </summary>
            public static NumericResult Query(MiscResult @config, StringResult @file, StringResult @query)
            {
                return new NumericResult(t => t.Call_ApocExportJson_Query, new object[] { @config, @file, @query }, null);
            }
        }

        public static partial class Graph
        {
            /// <summary>
            /// apoc.graph.from(data,&apos;name&apos;,{properties}) | creates a virtual graph object for later processing it tries its best to extract the graph information from the data you pass in
            /// </summary>
            public static MiscResult From(MiscResult @data, StringResult @name, MiscResult @properties)
            {
                return new MiscResult(t => t.Call_ApocGraph_From, new object[] { @data, @name, @properties }, null);
            }
            /// <summary>
            /// apoc.graph.fromCypher(&apos;kernelTransaction&apos;,{params},&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscResult FromCypher(StringResult @kernelTransaction, StringResult @name, MiscResult @params, MiscResult @properties)
            {
                return new MiscResult(t => t.Call_ApocGraph_FromCypher, new object[] { @kernelTransaction, @name, @params, @properties }, null);
            }
            /// <summary>
            /// apoc.graph.fromData([nodes],[relationships],&apos;name&apos;,{properties}) | creates a virtual graph object for later processing
            /// </summary>
            public static MiscResult FromData(StringResult @name, AliasListResult @nodes, MiscResult @properties, AliasListResult @relationships)
            {
                return new MiscResult(t => t.Call_ApocGraph_FromData, new object[] { @name, @nodes, @properties, @relationships }, null);
            }
            /// <summary>
            /// apoc.graph.fromDB(&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscResult FromDB(StringResult @name, MiscResult @properties)
            {
                return new MiscResult(t => t.Call_ApocGraph_FromDB, new object[] { @name, @properties }, null);
            }
            /// <summary>
            /// apoc.graph.fromDocument({json}, {config}) yield graph - transform JSON documents into graph structures
            /// </summary>
            public static MiscResult FromDocument(MiscResult @config, MiscResult @json)
            {
                return new MiscResult(t => t.Call_ApocGraph_FromDocument, new object[] { @config, @json }, null);
            }
            /// <summary>
            /// apoc.graph.fromPath(path,&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscResult FromPath(StringResult @name, PathResult @path, MiscResult @properties)
            {
                return new MiscResult(t => t.Call_ApocGraph_FromPath, new object[] { @name, @path, @properties }, null);
            }
            /// <summary>
            /// apoc.graph.fromPaths([paths],&apos;name&apos;,{properties}) - creates a virtual graph object for later processing
            /// </summary>
            public static MiscResult FromPaths(StringResult @name, PathListResult @paths, MiscResult @properties)
            {
                return new MiscResult(t => t.Call_ApocGraph_FromPaths, new object[] { @name, @paths, @properties }, null);
            }
            /// <summary>
            /// apoc.graph.validateDocument({json}, {config}) yield row - validates the json, return the result of the validation
            /// </summary>
            public static MiscResult ValidateDocument(MiscResult @config, MiscResult @json)
            {
                return new MiscResult(t => t.Call_ApocGraph_ValidateDocument, new object[] { @config, @json }, null);
            }
        }

        public static partial class Hashing
        {
            /// <summary>
            /// calculate a checksum (md5) over a node or a relationship. This deals gracefully with array properties. Two identical entities do share the same hash. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static StringResult Fingerprint(StringListResult @propertyExcludes, MiscResult @someObject)
            {
                return new StringResult(t => t.Fn_ApocHashing_Fingerprint, new object[] { @propertyExcludes, @someObject }, null);
            }
            /// <summary>
            /// calculate a checksum (md5) over a the full graph. Be aware that this function does use in-memomry datastructures depending on the size of your graph. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static StringResult FingerprintGraph()
            {
                return new StringResult(t => t.Fn_ApocHashing_FingerprintGraph(0), new object[] {  }, null);
            }
            /// <summary>
            /// calculate a checksum (md5) over a the full graph. Be aware that this function does use in-memomry datastructures depending on the size of your graph. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static StringResult FingerprintGraph(StringListResult @propertyExcludes)
            {
                return new StringResult(t => t.Fn_ApocHashing_FingerprintGraph(1), new object[] { @propertyExcludes }, null);
            }
            /// <summary>
            /// calculate a checksum (md5) over a node or a relationship. This deals gracefully with array properties. Two identical entities do share the same hash. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static StringResult Fingerprinting(MiscResult @conf, MiscResult @someObject)
            {
                return new StringResult(t => t.Fn_ApocHashing_Fingerprinting, new object[] { @conf, @someObject }, null);
            }
        }

        public static partial class Import
        {
            /// <summary>
            /// apoc.import.csv(nodes, relationships, config) - imports nodes and relationships from the provided CSV files with given labels and types
            /// </summary>
            public static NumericResult Csv(MiscResult @config, MiscListResult @nodes, MiscListResult @relationships)
            {
                return new NumericResult(t => t.Call_ApocImport_Csv, new object[] { @config, @nodes, @relationships }, null);
            }
            /// <summary>
            /// apoc.import.graphml(urlOrBinaryFile,config) - imports graphml file
            /// </summary>
            public static NumericResult Graphml(MiscResult @config, MiscResult @urlOrBinaryFile)
            {
                return new NumericResult(t => t.Call_ApocImport_Graphml, new object[] { @config, @urlOrBinaryFile }, null);
            }
            /// <summary>
            /// apoc.import.json(urlOrBinaryFile,config) - imports the json list to the provided file
            /// </summary>
            public static NumericResult Json(MiscResult @config, MiscResult @urlOrBinaryFile)
            {
                return new NumericResult(t => t.Call_ApocImport_Json, new object[] { @config, @urlOrBinaryFile }, null);
            }
            /// <summary>
            /// apoc.import.xml(file,config) - imports graph from provided file
            /// </summary>
            public static AliasResult Xml(MiscResult @config, MiscResult @urlOrBinary)
            {
                return new AliasResult(t => t.Call_ApocImport_Xml, new object[] { @config, @urlOrBinary }, null);
            }
        }

        public static partial class Json
        {
            /// <summary>
            /// apoc.json.path(&apos;{json}&apos; [,&apos;json-path&apos; , &apos;path-options&apos;])
            /// </summary>
            public static MiscResult Path(StringResult @json)
            {
                return new MiscResult(t => t.Fn_ApocJson_Path(1), new object[] { @json }, null);
            }
            /// <summary>
            /// apoc.json.path(&apos;{json}&apos; [,&apos;json-path&apos; , &apos;path-options&apos;])
            /// </summary>
            public static MiscResult Path(StringResult @json, StringResult @path)
            {
                return new MiscResult(t => t.Fn_ApocJson_Path(2), new object[] { @json, @path }, null);
            }
            /// <summary>
            /// apoc.json.path(&apos;{json}&apos; [,&apos;json-path&apos; , &apos;path-options&apos;])
            /// </summary>
            public static MiscResult Path(StringResult @json, string @path)
            {
                return new MiscResult(t => t.Fn_ApocJson_Path(2), new object[] { @json, string_To_StringResult(@path) }, null);
            }
            /// <summary>
            /// apoc.json.path(&apos;{json}&apos; [,&apos;json-path&apos; , &apos;path-options&apos;])
            /// </summary>
            public static MiscResult Path(StringResult @json, StringResult @path, PathOptions @pathOptions)
            {
                return new MiscResult(t => t.Fn_ApocJson_Path(3), new object[] { @json, @path, PathOptions_To_StringListResult(@pathOptions) }, null);
            }
            /// <summary>
            /// apoc.json.path(&apos;{json}&apos; [,&apos;json-path&apos; , &apos;path-options&apos;])
            /// </summary>
            public static MiscResult Path(StringResult @json, string @path, PathOptions @pathOptions)
            {
                return new MiscResult(t => t.Fn_ApocJson_Path(3), new object[] { @json, string_To_StringResult(@path), PathOptions_To_StringListResult(@pathOptions) }, null);
            }
        }

        public static partial class Label
        {
            /// <summary>
            /// apoc.label.exists(element, label) - returns true or false related to label existance
            /// </summary>
            public static BooleanResult Exists(StringResult @label, MiscResult @node)
            {
                return new BooleanResult(t => t.Fn_ApocLabel_Exists, new object[] { @label, @node }, null);
            }
        }

        public static partial class Load
        {
            /// <summary>
            /// apoc.load.jsonArray(&apos;url&apos;) YIELD value - load array from JSON URL (e.g. web-api) to import JSON as stream of values
            /// </summary>
            public static MiscResult JsonArray(MiscResult @config, StringResult @path, StringResult @url)
            {
                return new MiscResult(t => t.Call_ApocLoad_JsonArray, new object[] { @config, @path, @url }, null);
            }
            /// <summary>
            /// apoc.load.jsonParams(&apos;urlOrKeyOrBinary&apos;,{header:value},payload, config) YIELD value - load from JSON URL (e.g. web-api) while sending headers / payload to import JSON as stream of values if the JSON was an array or a single value if it was a map
            /// </summary>
            public static MiscResult JsonParams(MiscResult @config, MiscResult @headers, StringResult @path, StringResult @payload, MiscResult @urlOrKeyOrBinary)
            {
                return new MiscResult(t => t.Call_ApocLoad_JsonParams, new object[] { @config, @headers, @path, @payload, @urlOrKeyOrBinary }, null);
            }
            /// <summary>
            /// apoc.load.json(&apos;urlOrKeyOrBinary&apos;,path, config) YIELD value - import JSON as stream of values if the JSON was an array or a single value if it was a map
            /// </summary>
            public static MiscResult Json(MiscResult @config, StringResult @path, MiscResult @urlOrKeyOrBinary)
            {
                return new MiscResult(t => t.Call_ApocLoad_Json, new object[] { @config, @path, @urlOrKeyOrBinary }, null);
            }
            /// <summary>
            /// apoc.load.xml(&apos;http://example.com/test.xml&apos;, &apos;xPath&apos;,config, false) YIELD value as doc CREATE (p:Person) SET p.name = doc.name - load from XML URL (e.g. web-api) to import XML as single nested map with attributes and _type, _text and _childrenx fields.
            /// </summary>
            public static MiscResult Xml(MiscResult @config, StringResult @path, BooleanResult @simple, MiscResult @urlOrBinary)
            {
                return new MiscResult(t => t.Call_ApocLoad_Xml, new object[] { @config, @path, @simple, @urlOrBinary }, null);
            }
        }

        public static partial class Lock
        {
            /// <summary>
            /// apoc.lock.all([nodes],[relationships]) acquires a write lock on the given nodes and relationships
            /// </summary>
            public static MiscResult All(AliasListResult @nodes, AliasListResult @rels)
            {
                return new MiscResult(t => t.Call_ApocLock_All, new object[] { @nodes, @rels }, null);
            }
            /// <summary>
            /// apoc.lock.nodes([nodes]) acquires a write lock on the given nodes
            /// </summary>
            public static MiscResult Nodes(AliasListResult @nodes)
            {
                return new MiscResult(t => t.Call_ApocLock_Nodes, new object[] { @nodes }, null);
            }
            /// <summary>
            /// apoc.lock.rels([relationships]) acquires a write lock on the given relationship
            /// </summary>
            public static MiscResult Rels(AliasListResult @rels)
            {
                return new MiscResult(t => t.Call_ApocLock_Rels, new object[] { @rels }, null);
            }
        }

        public static partial class LockRead
        {
            /// <summary>
            /// apoc.lock.read.nodes([nodes]) acquires a read lock on the given nodes
            /// </summary>
            public static MiscResult Nodes(AliasListResult @nodes)
            {
                return new MiscResult(t => t.Call_ApocLockRead_Nodes, new object[] { @nodes }, null);
            }
            /// <summary>
            /// apoc.lock.read.rels([relationships]) acquires a read lock on the given relationship
            /// </summary>
            public static MiscResult Rels(AliasListResult @rels)
            {
                return new MiscResult(t => t.Call_ApocLockRead_Rels, new object[] { @rels }, null);
            }
        }

        public static partial class Log
        {
            /// <summary>
            /// apoc.log.stream(&apos;neo4j.log&apos;, { last: n }) - retrieve log file contents, optionally return only the last n lines
            /// </summary>
            public static StringResult Stream(MiscResult @config, StringResult @path)
            {
                return new StringResult(t => t.Call_ApocLog_Stream, new object[] { @config, @path }, null);
            }
        }

        public static partial class Map
        {
            /// <summary>
            /// apoc.map.clean(map,[skip,keys],[skip,values]) yield map filters the keys and values contained in those lists, good for data cleaning from CSV/JSON
            /// </summary>
            public static MiscResult Clean(StringListResult @keys, MiscResult @map, MiscListResult @values)
            {
                return new MiscResult(t => t.Fn_ApocMap_Clean, new object[] { @keys, @map, @values }, null);
            }
            /// <summary>
            /// apoc.map.fromLists([keys],[values])
            /// </summary>
            public static MiscResult FromLists(StringListResult @keys, MiscListResult @values)
            {
                return new MiscResult(t => t.Fn_ApocMap_FromLists, new object[] { @keys, @values }, null);
            }
            /// <summary>
            /// apoc.map.fromNodes(label, property)
            /// </summary>
            public static MiscResult FromNodes(StringResult @label, StringResult @property)
            {
                return new MiscResult(t => t.Fn_ApocMap_FromNodes, new object[] { @label, @property }, null);
            }
            /// <summary>
            /// apoc.map.fromPairs([[key,value],[key2,value2],…​])
            /// </summary>
            public static MiscResult FromPairs(MiscJaggedListResult @pairs)
            {
                return new MiscResult(t => t.Fn_ApocMap_FromPairs, new object[] { @pairs }, null);
            }
            /// <summary>
            /// apoc.map.fromValues([key1,value1,key2,value2,…​])
            /// </summary>
            public static MiscResult FromValues(params MiscListResult[] @values)
            {
                return new MiscResult(t => t.Fn_ApocMap_FromValues(@values.Length), @values, null);
            }
            /// <summary>
            /// apoc.map.groupBy([maps/nodes/relationships],&apos;key&apos;) yield value - creates a map of the list keyed by the given property, with single values
            /// </summary>
            public static MiscResult GroupBy(StringResult @key, MiscListResult @values)
            {
                return new MiscResult(t => t.Fn_ApocMap_GroupBy, new object[] { @key, @values }, null);
            }
            /// <summary>
            /// apoc.map.groupByMulti([maps/nodes/relationships],&apos;key&apos;) yield value - creates a map of the list keyed by the given property, with list values
            /// </summary>
            public static MiscResult GroupByMulti(StringResult @key, MiscListResult @values)
            {
                return new MiscResult(t => t.Fn_ApocMap_GroupByMulti, new object[] { @key, @values }, null);
            }
            /// <summary>
            /// apoc.map.merge(first,second) - merges two maps
            /// </summary>
            public static MiscResult Merge(MiscResult @first, MiscResult @second)
            {
                return new MiscResult(t => t.Fn_ApocMap_Merge, new object[] { @first, @second }, null);
            }
            /// <summary>
            /// apoc.map.mergeList([{maps}]) yield value - merges all maps in the list into one
            /// </summary>
            public static MiscResult MergeList(MiscListResult @maps)
            {
                return new MiscResult(t => t.Fn_ApocMap_MergeList, new object[] { @maps }, null);
            }
            /// <summary>
            /// apoc.map.mget(map,key,[defaults],[fail=true])  - returns list of values for keys or throws exception if one of the key doesn’t exist and no default value given at that position
            /// </summary>
            public static MiscListResult Mget(BooleanResult @fail, StringListResult @keys, MiscResult @map)
            {
                return new MiscListResult(t => t.Fn_ApocMap_Mget(3), new object[] { @fail, @keys, @map }, null);
            }
            /// <summary>
            /// apoc.map.mget(map,key,[defaults],[fail=true])  - returns list of values for keys or throws exception if one of the key doesn’t exist and no default value given at that position
            /// </summary>
            public static MiscListResult Mget(BooleanResult @fail, StringListResult @keys, MiscResult @map, MiscListResult @values)
            {
                return new MiscListResult(t => t.Fn_ApocMap_Mget(4), new object[] { @fail, @keys, @map, @values }, null);
            }
            /// <summary>
            /// apoc.map.removeKey(map,key,{recursive:true/false}) - remove the key from the map (recursively if recursive is true)
            /// </summary>
            public static MiscResult RemoveKey(MiscResult @config, StringResult @key, MiscResult @map)
            {
                return new MiscResult(t => t.Fn_ApocMap_RemoveKey, new object[] { @config, @key, @map }, null);
            }
            /// <summary>
            /// apoc.map.removeKeys(map,[keys],{recursive:true/false}) - remove the keys from the map (recursively if recursive is true)
            /// </summary>
            public static MiscResult RemoveKeys(MiscResult @config, StringListResult @keys, MiscResult @map)
            {
                return new MiscResult(t => t.Fn_ApocMap_RemoveKeys, new object[] { @config, @keys, @map }, null);
            }
            /// <summary>
            /// apoc.map.setEntry(map,key,value)
            /// </summary>
            public static MiscResult SetEntry(StringResult @key, MiscResult @map, MiscResult @value)
            {
                return new MiscResult(t => t.Fn_ApocMap_SetEntry, new object[] { @key, @map, @value }, null);
            }
            /// <summary>
            /// apoc.map.setKey(map,key,value)
            /// </summary>
            public static MiscResult SetKey(StringResult @key, MiscResult @map, MiscResult @value)
            {
                return new MiscResult(t => t.Fn_ApocMap_SetKey, new object[] { @key, @map, @value }, null);
            }
            /// <summary>
            /// apoc.map.setLists(map,[keys],[values])
            /// </summary>
            public static MiscResult SetLists(StringListResult @keys, MiscResult @map, MiscListResult @values)
            {
                return new MiscResult(t => t.Fn_ApocMap_SetLists, new object[] { @keys, @map, @values }, null);
            }
            /// <summary>
            /// apoc.map.setPairs(map,[[key1,value1],[key2,value2])
            /// </summary>
            public static MiscResult SetPairs(MiscResult @map, MiscJaggedListResult @pairs)
            {
                return new MiscResult(t => t.Fn_ApocMap_SetPairs, new object[] { @map, @pairs }, null);
            }
            /// <summary>
            /// apoc.map.setValues(map,[key1,value1,key2,value2])
            /// </summary>
            public static MiscResult SetValues(MiscResult @map, MiscListResult @pairs)
            {
                return new MiscResult(t => t.Fn_ApocMap_SetValues, new object[] { @map, @pairs }, null);
            }
            /// <summary>
            /// apoc.map.sortedProperties(map, ignoreCase:true) - returns a list of key/value list pairs, with pairs sorted by keys alphabetically, with optional case sensitivity
            /// </summary>
            public static MiscListResult SortedProperties(BooleanResult @ignoreCase, MiscResult @map)
            {
                return new MiscListResult(t => t.Fn_ApocMap_SortedProperties, new object[] { @ignoreCase, @map }, null);
            }
            /// <summary>
            /// apoc.map.submap(map,keys,[defaults],[fail=true])  - returns submap for keys or throws exception if one of the key doesn’t exist and no default value given at that position
            /// </summary>
            public static MiscResult Submap(BooleanResult @fail, StringListResult @keys, MiscResult @map)
            {
                return new MiscResult(t => t.Fn_ApocMap_Submap(3), new object[] { @fail, @keys, @map }, null);
            }
            /// <summary>
            /// apoc.map.submap(map,keys,[defaults],[fail=true])  - returns submap for keys or throws exception if one of the key doesn’t exist and no default value given at that position
            /// </summary>
            public static MiscResult Submap(BooleanResult @fail, StringListResult @keys, MiscResult @map, MiscListResult @values)
            {
                return new MiscResult(t => t.Fn_ApocMap_Submap(4), new object[] { @fail, @keys, @map, @values }, null);
            }
            /// <summary>
            /// apoc.map.unflatten(map, delimiter:&apos;.&apos;) yield map - unflat from items separated by delimiter string to nested items (reverse of apoc.map.flatten function)
            /// </summary>
            public static MiscResult Unflatten(StringResult @delimiter, MiscResult @map)
            {
                return new MiscResult(t => t.Fn_ApocMap_Unflatten, new object[] { @delimiter, @map }, null);
            }
            /// <summary>
            /// apoc.map.updateTree(tree,key,) returns map - adds the {data} map on each level of the nested tree, where the key-value pairs match
            /// </summary>
            public static MiscResult UpdateTree(MiscJaggedListResult @data, StringResult @key, MiscResult @tree)
            {
                return new MiscResult(t => t.Fn_ApocMap_UpdateTree, new object[] { @data, @key, @tree }, null);
            }
            /// <summary>
            /// apoc.map.values(map, [key1,key2,key3,…​],[addNullsForMissing]) returns list of values indicated by the keys
            /// </summary>
            public static MiscListResult Values(BooleanResult @addNullsForMissing, StringListResult @keys, MiscResult @map)
            {
                return new MiscListResult(t => t.Fn_ApocMap_Values, new object[] { @addNullsForMissing, @keys, @map }, null);
            }
            /// <summary>
            /// apoc.map.flatten(map, delimiter:&apos;.&apos;) yield map - flattens nested items in map using dot notation
            /// </summary>
            public static MiscResult Flatten(StringResult @delimiter, MiscResult @map)
            {
                return new MiscResult(t => t.Fn_ApocMap_Flatten, new object[] { @delimiter, @map }, null);
            }
            /// <summary>
            /// apoc.map.get(map,key,[default],[fail=true]) - returns value for key or throws exception if key doesn’t exist and no default given
            /// </summary>
            public static MiscResult Get(BooleanResult @fail, StringResult @key, MiscResult @map, MiscResult @value)
            {
                return new MiscResult(t => t.Fn_ApocMap_Get, new object[] { @fail, @key, @map, @value }, null);
            }
        }

        public static partial class Math
        {
            /// <summary>
            /// apoc.math.cosh(val) | returns the hyperbolic cosin
            /// </summary>
            public static FloatResult Cosh(FloatResult @value)
            {
                return new FloatResult(t => t.Fn_ApocMath_Cosh, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.math.coth(val) | returns the hyperbolic cotangent
            /// </summary>
            public static FloatResult Coth(FloatResult @value)
            {
                return new FloatResult(t => t.Fn_ApocMath_Coth, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.math.csch(val) | returns the hyperbolic cosecant
            /// </summary>
            public static FloatResult Csch(FloatResult @value)
            {
                return new FloatResult(t => t.Fn_ApocMath_Csch, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.math.maxByte() | return the maximum value an byte can have
            /// </summary>
            public static NumericResult MaxByte()
            {
                return new NumericResult(t => t.Fn_ApocMath_MaxByte, new object[0], null);
            }
            /// <summary>
            /// apoc.math.maxDouble() | return the largest positive finite value of type double
            /// </summary>
            public static FloatResult MaxDouble()
            {
                return new FloatResult(t => t.Fn_ApocMath_MaxDouble, new object[0], null);
            }
            /// <summary>
            /// apoc.math.maxInt() | return the maximum value an int can have
            /// </summary>
            public static NumericResult MaxInt()
            {
                return new NumericResult(t => t.Fn_ApocMath_MaxInt, new object[0], null);
            }
            /// <summary>
            /// apoc.math.maxLong() | return the maximum value a long can have
            /// </summary>
            public static NumericResult MaxLong()
            {
                return new NumericResult(t => t.Fn_ApocMath_MaxLong, new object[0], null);
            }
            /// <summary>
            /// apoc.math.minByte() | return the minimum value an byte can have
            /// </summary>
            public static NumericResult MinByte()
            {
                return new NumericResult(t => t.Fn_ApocMath_MinByte, new object[0], null);
            }
            /// <summary>
            /// apoc.math.minDouble() | return the smallest positive nonzero value of type double
            /// </summary>
            public static FloatResult MinDouble()
            {
                return new FloatResult(t => t.Fn_ApocMath_MinDouble, new object[0], null);
            }
            /// <summary>
            /// apoc.math.minInt() | return the minimum value an int can have
            /// </summary>
            public static NumericResult MinInt()
            {
                return new NumericResult(t => t.Fn_ApocMath_MinInt, new object[0], null);
            }
            /// <summary>
            /// apoc.math.minLong() | return the minimum value a long can have
            /// </summary>
            public static NumericResult MinLong()
            {
                return new NumericResult(t => t.Fn_ApocMath_MinLong, new object[0], null);
            }
            /// <summary>
            /// apoc.math.regr(label, propertyY, propertyX) - It calculates the coefficient of determination (R-squared) for the values of propertyY and propertyX in the provided label
            /// </summary>
            public static FloatResult Regr(StringResult @label, StringResult @propertyX, StringResult @propertyY)
            {
                return new FloatResult(t => t.Call_ApocMath_Regr, new object[] { @label, @propertyX, @propertyY }, null);
            }
            /// <summary>
            /// apoc.math.round(value,[prec],mode=[CEILING,FLOOR,UP,DOWN,HALF_EVEN,HALF_DOWN,HALF_UP,DOWN,UNNECESSARY])
            /// </summary>
            public static FloatResult Round(StringResult @mode, NumericResult @precision, FloatResult @value)
            {
                return new FloatResult(t => t.Fn_ApocMath_Round, new object[] { @mode, @precision, @value }, null);
            }
            /// <summary>
            /// apoc.math.sech(val) | returns the hyperbolic secant
            /// </summary>
            public static FloatResult Sech(FloatResult @value)
            {
                return new FloatResult(t => t.Fn_ApocMath_Sech, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.math.sigmoid(val) | returns the sigmoid value
            /// </summary>
            public static FloatResult Sigmoid(FloatResult @value)
            {
                return new FloatResult(t => t.Fn_ApocMath_Sigmoid, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.math.sigmoidPrime(val) | returns the sigmoid prime [ sigmoid(val) * (1 - sigmoid(val)) ]
            /// </summary>
            public static FloatResult SigmoidPrime(FloatResult @value)
            {
                return new FloatResult(t => t.Fn_ApocMath_SigmoidPrime, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.math.sinh(val) | returns the hyperbolic sin
            /// </summary>
            public static FloatResult Sinh(FloatResult @value)
            {
                return new FloatResult(t => t.Fn_ApocMath_Sinh, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.math.tanh(val) | returns the hyperbolic tangent
            /// </summary>
            public static FloatResult Tanh(FloatResult @value)
            {
                return new FloatResult(t => t.Fn_ApocMath_Tanh, new object[] { @value }, null);
            }
        }

        public static partial class Merge
        {
            /// <summary>
            /// &quot;apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static AliasResult Node(MiscResult @identProps, StringListResult @label)
            {
                return new AliasResult(t => t.Call_ApocMerge_Node(2), new object[] { @identProps, @label }, null);
            }
            /// <summary>
            /// &quot;apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static AliasResult Node(MiscResult @identProps, StringListResult @label, MiscResult @onMatchProps)
            {
                return new AliasResult(t => t.Call_ApocMerge_Node(3), new object[] { @identProps, @label, @onMatchProps }, null);
            }
            /// <summary>
            /// &quot;apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static AliasResult Node(MiscResult @identProps, StringListResult @label, MiscResult @onMatchProps, MiscResult @props)
            {
                return new AliasResult(t => t.Call_ApocMerge_Node(4), new object[] { @identProps, @label, @onMatchProps, @props }, null);
            }
            /// <summary>
            /// apoc.merge.relationship(startNode, relType,  identProps:{key:value, …​}, onCreateProps:{key:value, …​}, endNode, onMatchProps:{key:value, …​}) - merge relationship with dynamic type, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static AliasResult Relationship(AliasResult @endNode, MiscResult @identProps, MiscResult @onMatchProps, MiscResult @props, StringResult @relationshipType, AliasResult @startNode)
            {
                return new AliasResult(t => t.Call_ApocMerge_Relationship, new object[] { @endNode, @identProps, @onMatchProps, @props, @relationshipType, @startNode }, null);
            }
        }

        public static partial class MergeNode
        {
            /// <summary>
            /// apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes eagerly, with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static AliasResult Eager(MiscResult @identProps, StringListResult @label)
            {
                return new AliasResult(t => t.Call_ApocMergeNode_Eager(2), new object[] { @identProps, @label }, null);
            }
            /// <summary>
            /// apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes eagerly, with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static AliasResult Eager(MiscResult @identProps, StringListResult @label, MiscResult @onMatchProps)
            {
                return new AliasResult(t => t.Call_ApocMergeNode_Eager(3), new object[] { @identProps, @label, @onMatchProps }, null);
            }
            /// <summary>
            /// apoc.merge.node.eager([&apos;Label&apos;], identProps:{key:value, …​}, onCreateProps:{key:value,…​}, onMatchProps:{key:value,…​}}) - merge nodes eagerly, with dynamic labels, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static AliasResult Eager(MiscResult @identProps, StringListResult @label, MiscResult @onMatchProps, MiscResult @props)
            {
                return new AliasResult(t => t.Call_ApocMergeNode_Eager(4), new object[] { @identProps, @label, @onMatchProps, @props }, null);
            }
        }

        public static partial class MergeRelationship
        {
            /// <summary>
            /// apoc.merge.relationship(startNode, relType,  identProps:{key:value, …​}, onCreateProps:{key:value, …​}, endNode, onMatchProps:{key:value, …​}) - merge relationship with dynamic type, with support for setting properties ON CREATE or ON MATCH
            /// </summary>
            public static AliasResult Eager(AliasResult @endNode, MiscResult @identProps, MiscResult @onMatchProps, MiscResult @props, StringResult @relationshipType, AliasResult @startNode)
            {
                return new AliasResult(t => t.Call_ApocMergeRelationship_Eager, new object[] { @endNode, @identProps, @onMatchProps, @props, @relationshipType, @startNode }, null);
            }
        }

        public static partial class Meta
        {
            /// <summary>
            /// apoc.meta.graphSample() - examines the database statistics to build the meta graph, very fast, might report extra relationships
            /// </summary>
            public static AliasListResult GraphSample()
            {
                return new AliasListResult(t => t.Call_ApocMeta_GraphSample(0), new object[] {  }, null);
            }
            /// <summary>
            /// apoc.meta.graphSample() - examines the database statistics to build the meta graph, very fast, might report extra relationships
            /// </summary>
            public static AliasListResult GraphSample(MiscResult @config)
            {
                return new AliasListResult(t => t.Call_ApocMeta_GraphSample(1), new object[] { @config }, null);
            }
            /// <summary>
            /// apoc.meta.nodeTypeProperties()
            /// </summary>
            public static BooleanResult NodeTypeProperties()
            {
                return new BooleanResult(t => t.Call_ApocMeta_NodeTypeProperties(0), new object[] {  }, null);
            }
            /// <summary>
            /// apoc.meta.nodeTypeProperties()
            /// </summary>
            public static BooleanResult NodeTypeProperties(MiscResult @config)
            {
                return new BooleanResult(t => t.Call_ApocMeta_NodeTypeProperties(1), new object[] { @config }, null);
            }
            /// <summary>
            /// apoc.meta.relTypeProperties()
            /// </summary>
            public static BooleanResult RelTypeProperties()
            {
                return new BooleanResult(t => t.Call_ApocMeta_RelTypeProperties(0), new object[] {  }, null);
            }
            /// <summary>
            /// apoc.meta.relTypeProperties()
            /// </summary>
            public static BooleanResult RelTypeProperties(MiscResult @config)
            {
                return new BooleanResult(t => t.Call_ApocMeta_RelTypeProperties(1), new object[] { @config }, null);
            }
            /// <summary>
            /// apoc.meta.subGraph({labels:[labels],rels:[rel-types], excludes:[labels,rel-types]}) - examines a sample sub graph to create the meta-graph
            /// </summary>
            public static AliasListResult SubGraph(MiscResult @config)
            {
                return new AliasListResult(t => t.Call_ApocMeta_SubGraph, new object[] { @config }, null);
            }
            /// <summary>
            /// apoc.meta.typeName(value) - type name of a value (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,UNKNOWN,MAP,LIST)
            /// </summary>
            public static StringResult TypeName(MiscResult @value)
            {
                return new StringResult(t => t.Fn_ApocMeta_TypeName, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.meta.data({config})  - examines a subset of the graph to provide a tabular meta information
            /// </summary>
            public static BooleanResult Data()
            {
                return new BooleanResult(t => t.Call_ApocMeta_Data(0), new object[] {  }, null);
            }
            /// <summary>
            /// apoc.meta.data({config})  - examines a subset of the graph to provide a tabular meta information
            /// </summary>
            public static BooleanResult Data(MiscResult @config)
            {
                return new BooleanResult(t => t.Call_ApocMeta_Data(1), new object[] { @config }, null);
            }
            /// <summary>
            /// apoc.meta.graph - examines the full graph to create the meta-graph
            /// </summary>
            public static AliasListResult Graph()
            {
                return new AliasListResult(t => t.Call_ApocMeta_Graph(0), new object[] {  }, null);
            }
            /// <summary>
            /// apoc.meta.graph - examines the full graph to create the meta-graph
            /// </summary>
            public static AliasListResult Graph(MiscResult @config)
            {
                return new AliasListResult(t => t.Call_ApocMeta_Graph(1), new object[] { @config }, null);
            }
            /// <summary>
            /// apoc.meta.isType(value,type) - returns a row if type name matches none if not (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,UNKNOWN,MAP,LIST)
            /// </summary>
            public static BooleanResult IsType(StringResult @type, MiscResult @value)
            {
                return new BooleanResult(t => t.Fn_ApocMeta_IsType, new object[] { @type, @value }, null);
            }
            /// <summary>
            /// apoc.meta.schema({config})  - examines a subset of the graph to provide a map-like meta information
            /// </summary>
            public static MiscResult Schema()
            {
                return new MiscResult(t => t.Call_ApocMeta_Schema(0), new object[] {  }, null);
            }
            /// <summary>
            /// apoc.meta.schema({config})  - examines a subset of the graph to provide a map-like meta information
            /// </summary>
            public static MiscResult Schema(MiscResult @config)
            {
                return new MiscResult(t => t.Call_ApocMeta_Schema(1), new object[] { @config }, null);
            }
            /// <summary>
            /// apoc.meta.stats yield labelCount, relTypeCount, propertyKeyCount, nodeCount, relCount, labels, relTypes, stats | returns the information stored in the transactional database statistics
            /// </summary>
            public static NumericResult Stats()
            {
                return new NumericResult(t => t.Call_ApocMeta_Stats, new object[0], null);
            }
            /// <summary>
            /// apoc.meta.type(value) - type name of a value (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,UNKNOWN,MAP,LIST)
            /// </summary>
            public static StringResult Type(MiscResult @value)
            {
                return new StringResult(t => t.Fn_ApocMeta_Type, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.meta.types(node-relationship-map)  - returns a map of keys to types
            /// </summary>
            public static MiscResult Types(MiscResult @properties)
            {
                return new MiscResult(t => t.Fn_ApocMeta_Types, new object[] { @properties }, null);
            }
        }

        public static partial class MetaCypher
        {
            /// <summary>
            /// apoc.meta.cypher.isType(value,type) - returns a row if type name matches none if not (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,MAP,LIST OF &lt;TYPE&gt;,POINT,DATE,DATE_TIME,LOCAL_TIME,LOCAL_DATE_TIME,TIME,DURATION)
            /// </summary>
            public static BooleanResult IsType(StringResult @type, MiscResult @value)
            {
                return new BooleanResult(t => t.Fn_ApocMetaCypher_IsType, new object[] { @type, @value }, null);
            }
            /// <summary>
            /// apoc.meta.cypher.type(value) - type name of a value (INTEGER,FLOAT,STRING,BOOLEAN,RELATIONSHIP,NODE,PATH,NULL,MAP,LIST OF &lt;TYPE&gt;,POINT,DATE,DATE_TIME,LOCAL_TIME,LOCAL_DATE_TIME,TIME,DURATION)
            /// </summary>
            public static StringResult Type(MiscResult @value)
            {
                return new StringResult(t => t.Fn_ApocMetaCypher_Type, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.meta.cypher.types(node-relationship-map)  - returns a map of keys to types
            /// </summary>
            public static MiscResult Types(MiscResult @properties)
            {
                return new MiscResult(t => t.Fn_ApocMetaCypher_Types, new object[] { @properties }, null);
            }
        }

        public static partial class MetaData
        {
            /// <summary>
            /// apoc.meta.data.of({graph}, {config})  - examines a subset of the graph to provide a tabular meta information
            /// </summary>
            public static BooleanResult Of(MiscResult @config, MiscResult @graph)
            {
                return new BooleanResult(t => t.Call_ApocMetaData_Of, new object[] { @config, @graph }, null);
            }
        }

        public static partial class MetaGraph
        {
            /// <summary>
            /// apoc.meta.graph.of({graph}, {config})  - examines a subset of the graph to provide a graph meta information
            /// </summary>
            public static AliasListResult Of()
            {
                return new AliasListResult(t => t.Call_ApocMetaGraph_Of(0), new object[] {  }, null);
            }
            /// <summary>
            /// apoc.meta.graph.of({graph}, {config})  - examines a subset of the graph to provide a graph meta information
            /// </summary>
            public static AliasListResult Of(MiscResult @config)
            {
                return new AliasListResult(t => t.Call_ApocMetaGraph_Of(1), new object[] { @config }, null);
            }
            /// <summary>
            /// apoc.meta.graph.of({graph}, {config})  - examines a subset of the graph to provide a graph meta information
            /// </summary>
            public static AliasListResult Of(MiscResult @config, MiscResult @graph)
            {
                return new AliasListResult(t => t.Call_ApocMetaGraph_Of(2), new object[] { @config, @graph }, null);
            }
        }

        public static partial class MetaNodes
        {
            /// <summary>
            /// apoc.meta.nodes.count([labels], $config) - Returns the sum of the nodes with a label present in the list.
            /// </summary>
            public static NumericResult Count()
            {
                return new NumericResult(t => t.Fn_ApocMetaNodes_Count(0), new object[] {  }, null);
            }
            /// <summary>
            /// apoc.meta.nodes.count([labels], $config) - Returns the sum of the nodes with a label present in the list.
            /// </summary>
            public static NumericResult Count(MiscResult @config)
            {
                return new NumericResult(t => t.Fn_ApocMetaNodes_Count(1), new object[] { @config }, null);
            }
            /// <summary>
            /// apoc.meta.nodes.count([labels], $config) - Returns the sum of the nodes with a label present in the list.
            /// </summary>
            public static NumericResult Count(MiscResult @config, StringListResult @nodes)
            {
                return new NumericResult(t => t.Fn_ApocMetaNodes_Count(2), new object[] { @config, @nodes }, null);
            }
        }

        public static partial class Neighbors
        {
            /// <summary>
            /// apoc.neighbors.athop(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at a distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static AliasResult Athop(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new AliasResult(t => t.Call_ApocNeighbors_Athop, new object[] { @distance, @node, @types }, null);
            }
            /// <summary>
            /// apoc.neighbors.byhop(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at each distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static AliasListResult Byhop(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new AliasListResult(t => t.Call_ApocNeighbors_Byhop, new object[] { @distance, @node, @types }, null);
            }
            /// <summary>
            /// apoc.neighbors.tohop(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern up to a certain distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static AliasResult Tohop(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new AliasResult(t => t.Call_ApocNeighbors_Tohop, new object[] { @distance, @node, @types }, null);
            }
        }

        public static partial class NeighborsAthop
        {
            /// <summary>
            /// apoc.neighbors.athop.count(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at a distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static NumericResult Count(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new NumericResult(t => t.Call_ApocNeighborsAthop_Count, new object[] { @distance, @node, @types }, null);
            }
        }

        public static partial class NeighborsByhop
        {
            /// <summary>
            /// apoc.neighbors.byhop.count(node, rel-direction-pattern, distance) - returns distinct nodes of the given relationships in the pattern at each distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static MiscListResult Count(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new MiscListResult(t => t.Call_ApocNeighborsByhop_Count, new object[] { @distance, @node, @types }, null);
            }
        }

        public static partial class NeighborsTohop
        {
            /// <summary>
            /// apoc.neighbors.tohop.count(node, rel-direction-pattern, distance) - returns distinct count of nodes of the given relationships in the pattern up to a certain distance, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static NumericResult Count(NumericResult @distance, AliasResult @node, StringResult @types)
            {
                return new NumericResult(t => t.Call_ApocNeighborsTohop_Count, new object[] { @distance, @node, @types }, null);
            }
        }

        public static partial class NlpAzureEntities
        {
            /// <summary>
            /// Creates a (virtual) entity graph for provided text
            /// </summary>
            public static MiscResult Graph(MiscResult @config, MiscResult @source)
            {
                return new MiscResult(t => t.Call_ApocNlpAzureEntities_Graph, new object[] { @config, @source }, null);
            }
            /// <summary>
            /// Provides a entity analysis for provided text
            /// </summary>
            public static MiscResult Stream(MiscResult @config, MiscResult @source)
            {
                return new MiscResult(t => t.Call_ApocNlpAzureEntities_Stream, new object[] { @config, @source }, null);
            }
        }

        public static partial class NlpAzureKeyPhrases
        {
            /// <summary>
            /// Creates a (virtual) key phrase graph for provided text
            /// </summary>
            public static MiscResult Graph(MiscResult @config, MiscResult @source)
            {
                return new MiscResult(t => t.Call_ApocNlpAzureKeyPhrases_Graph, new object[] { @config, @source }, null);
            }
            /// <summary>
            /// Provides a entity analysis for provided text
            /// </summary>
            public static MiscResult Stream(MiscResult @config, MiscResult @source)
            {
                return new MiscResult(t => t.Call_ApocNlpAzureKeyPhrases_Stream, new object[] { @config, @source }, null);
            }
        }

        public static partial class NlpAzureSentiment
        {
            /// <summary>
            /// Creates a (virtual) sentiment graph for provided text
            /// </summary>
            public static MiscResult Graph(MiscResult @config, MiscResult @source)
            {
                return new MiscResult(t => t.Call_ApocNlpAzureSentiment_Graph, new object[] { @config, @source }, null);
            }
            /// <summary>
            /// Provides a sentiment analysis for provided text
            /// </summary>
            public static MiscResult Stream(MiscResult @config, MiscResult @source)
            {
                return new MiscResult(t => t.Call_ApocNlpAzureSentiment_Stream, new object[] { @config, @source }, null);
            }
        }

        public static partial class Node
        {
            /// <summary>
            /// apoc.node.degree(node, rel-direction-pattern) - returns total degrees of the given relationships in the pattern, can use &apos;&gt;&apos; or &apos;&lt;&apos; for all outgoing or incoming relationships
            /// </summary>
            public static NumericResult Degree(AliasResult @node, StringResult @types)
            {
                return new NumericResult(t => t.Fn_ApocNode_Degree, new object[] { @node, @types }, null);
            }
            /// <summary>
            /// returns id for (virtual) nodes
            /// </summary>
            public static NumericResult Id(AliasResult @node)
            {
                return new NumericResult(t => t.Fn_ApocNode_Id, new object[] { @node }, null);
            }
            /// <summary>
            /// returns labels for (virtual) nodes
            /// </summary>
            public static MiscListResult Labels(AliasResult @node)
            {
                return new MiscListResult(t => t.Fn_ApocNode_Labels, new object[] { @node }, null);
            }
        }

        public static partial class NodeDegree
        {
            /// <summary>
            /// apoc.node.degree.in(node, relationshipName) - returns total number number of incoming relationships
            /// </summary>
            public static NumericResult In(AliasResult @node, StringResult @types)
            {
                return new NumericResult(t => t.Fn_ApocNodeDegree_In, new object[] { @node, @types }, null);
            }
            /// <summary>
            /// apoc.node.degree.out(node, relationshipName) - returns total number number of outgoing relationships
            /// </summary>
            public static NumericResult Out(AliasResult @node, StringResult @types)
            {
                return new NumericResult(t => t.Fn_ApocNodeDegree_Out, new object[] { @node, @types }, null);
            }
        }

        public static partial class NodeRelationship
        {
            /// <summary>
            /// apoc.node.relationship.exists(node, rel-direction-pattern) - returns true when the node has the relationships of the pattern
            /// </summary>
            public static BooleanResult Exists(AliasResult @node, StringResult @types)
            {
                return new BooleanResult(t => t.Fn_ApocNodeRelationship_Exists, new object[] { @node, @types }, null);
            }
            /// <summary>
            /// apoc.node.relationship.types(node, rel-direction-pattern) - returns a list of distinct relationship types
            /// </summary>
            public static MiscListResult Types(AliasResult @node, StringResult @types)
            {
                return new MiscListResult(t => t.Fn_ApocNodeRelationship_Types, new object[] { @node, @types }, null);
            }
        }

        public static partial class NodeRelationships
        {
            /// <summary>
            /// apoc.node.relationships.exist(node, rel-direction-pattern) - returns a map with rel-pattern, boolean for the given relationship patterns
            /// </summary>
            public static MiscResult Exist(AliasResult @node, StringResult @types)
            {
                return new MiscResult(t => t.Fn_ApocNodeRelationships_Exist, new object[] { @node, @types }, null);
            }
        }

        public static partial class Nodes
        {
            /// <summary>
            /// apoc.nodes.collapse([nodes…​],[{properties:&apos;overwrite&apos; or &apos;discard&apos; or &apos;combine&apos;}]) yield from, rel, to merge nodes onto first in list
            /// </summary>
            public static AliasResult Collapse(MiscResult @config, AliasListResult @nodes)
            {
                return new AliasResult(t => t.Call_ApocNodes_Collapse, new object[] { @config, @nodes }, null);
            }
            /// <summary>
            /// apoc.nodes.connected(start, end, rel-direction-pattern) - returns true when the node is connected to the other node, optimized for dense nodes
            /// </summary>
            public static BooleanResult Connected(AliasResult @start, StringResult @types)
            {
                return new BooleanResult(t => t.Fn_ApocNodes_Connected, new object[] { @start, @types }, null);
            }
            /// <summary>
            /// CALL apoc.nodes.cycles([nodes], $config) - Detect all path cycles from node list
            /// </summary>
            public static PathResult Cycles(MiscResult @config, AliasListResult @nodes)
            {
                return new PathResult(t => t.Call_ApocNodes_Cycles, new object[] { @config, @nodes }, null);
            }
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static AliasResult Group(MiscListResult @aggregations, MiscResult @config, StringListResult @groupByProperties, StringListResult @labels)
            {
                return new AliasResult(t => t.Call_ApocNodes_Group, new object[] { @aggregations, @config, @groupByProperties, @labels }, null);
            }
            /// <summary>
            /// apoc.nodes.isDense(node) - returns true if it is a dense node
            /// </summary>
            public static BooleanResult IsDense(AliasResult @node)
            {
                return new BooleanResult(t => t.Fn_ApocNodes_IsDense, new object[] { @node }, null);
            }
            /// <summary>
            /// apoc.nodes.link([nodes],&apos;REL_TYPE&apos;, conf) - creates a linked list of nodes from first to last
            /// </summary>
            public static MiscResult Link(MiscResult @config, AliasListResult @nodes, StringResult @type)
            {
                return new MiscResult(t => t.Call_ApocNodes_Link, new object[] { @config, @nodes, @type }, null);
            }
            /// <summary>
            /// apoc.nodes.delete(node|nodes|id|[ids]) - quickly delete all nodes with these ids
            /// </summary>
            public static NumericResult Delete(NumericResult @batchSize, MiscResult @nodes)
            {
                return new NumericResult(t => t.Call_ApocNodes_Delete, new object[] { @batchSize, @nodes }, null);
            }
            /// <summary>
            /// apoc.nodes.get(node|nodes|id|[ids]) - quickly returns all nodes with these ids
            /// </summary>
            public static AliasResult Get(MiscResult @nodes)
            {
                return new AliasResult(t => t.Call_ApocNodes_Get, new object[] { @nodes }, null);
            }
            /// <summary>
            /// apoc.get.rels(rel|id|[ids]) - quickly returns all relationships with these ids
            /// </summary>
            public static AliasResult Rels(MiscResult @relationships)
            {
                return new AliasResult(t => t.Call_ApocNodes_Rels, new object[] { @relationships }, null);
            }
        }

        public static partial class NodesRelationship
        {
            /// <summary>
            /// apoc.nodes.relationship.types(node|nodes|id|[ids], rel-direction-pattern) - returns a list of maps where each one has two fields: node which is the node subject of the analysis and types which is a list of distinct relationship types
            /// </summary>
            public static MiscListResult Types(MiscResult @ids, StringResult @types)
            {
                return new MiscListResult(t => t.Fn_ApocNodesRelationship_Types, new object[] { @ids, @types }, null);
            }
        }

        public static partial class NodesRelationships
        {
            /// <summary>
            /// apoc.nodes.relationships.exist(node|nodes|id|[ids], rel-direction-pattern) - returns a list of maps where each one has two fields: node which is the node subject of the analysis and exists which is a map with rel-pattern, boolean for the given relationship patterns
            /// </summary>
            public static MiscListResult Exist(MiscResult @ids, StringResult @types)
            {
                return new MiscListResult(t => t.Fn_ApocNodesRelationships_Exist, new object[] { @ids, @types }, null);
            }
        }

        public static partial class Number
        {
            /// <summary>
            /// apoc.number.arabicToRoman(number)  | convert arabic numbers to roman
            /// </summary>
            public static StringResult ArabicToRoman(MiscResult @number)
            {
                return new StringResult(t => t.Fn_ApocNumber_ArabicToRoman, new object[] { @number }, null);
            }
            /// <summary>
            /// apoc.number.parseFloat(text)  | parse a text using the default system pattern and language to produce a double
            /// </summary>
            public static FloatResult ParseFloat(StringResult @lang, StringResult @pattern, StringResult @text)
            {
                return new FloatResult(t => t.Fn_ApocNumber_ParseFloat, new object[] { @lang, @pattern, @text }, null);
            }
            /// <summary>
            /// apoc.number.parseInt(text)  | parse a text using the default system pattern and language to produce a long
            /// </summary>
            public static NumericResult ParseInt(StringResult @lang, StringResult @pattern, StringResult @text)
            {
                return new NumericResult(t => t.Fn_ApocNumber_ParseInt, new object[] { @lang, @pattern, @text }, null);
            }
            /// <summary>
            /// apoc.number.romanToArabic(romanNumber)  | convert roman numbers to arabic
            /// </summary>
            public static FloatResult RomanToArabic(StringResult @romanNumber)
            {
                return new FloatResult(t => t.Fn_ApocNumber_RomanToArabic, new object[] { @romanNumber }, null);
            }
            /// <summary>
            /// apoc.number.format(number)  | format a long or double using the default system pattern and language to produce a string
            /// </summary>
            public static StringResult Format(StringResult @lang, MiscResult @number, StringResult @pattern)
            {
                return new StringResult(t => t.Fn_ApocNumber_Format, new object[] { @lang, @number, @pattern }, null);
            }
        }

        public static partial class NumberExact
        {
            /// <summary>
            /// apoc.number.exact.div(stringA,stringB,[prec],[roundingModel]) - return the division’s result of two large numbers
            /// </summary>
            public static StringResult Div(NumericResult @precision, StringResult @roundingMode, StringResult @stringA, StringResult @stringB)
            {
                return new StringResult(t => t.Fn_ApocNumberExact_Div, new object[] { @precision, @roundingMode, @stringA, @stringB }, null);
            }
            /// <summary>
            /// apoc.number.exact.mul(stringA,stringB,[prec],[roundingModel]) - return the multiplication’s result of two large numbers
            /// </summary>
            public static StringResult Mul(NumericResult @precision, StringResult @roundingMode, StringResult @stringA, StringResult @stringB)
            {
                return new StringResult(t => t.Fn_ApocNumberExact_Mul, new object[] { @precision, @roundingMode, @stringA, @stringB }, null);
            }
            /// <summary>
            /// apoc.number.exact.sub(stringA,stringB) - return the substraction’s of two large numbers
            /// </summary>
            public static StringResult Sub(StringResult @stringA, StringResult @stringB)
            {
                return new StringResult(t => t.Fn_ApocNumberExact_Sub, new object[] { @stringA, @stringB }, null);
            }
            /// <summary>
            /// apoc.number.exact.toExact(number) - return the exact value
            /// </summary>
            public static NumericResult ToExact(NumericResult @number)
            {
                return new NumericResult(t => t.Fn_ApocNumberExact_ToExact, new object[] { @number }, null);
            }
            /// <summary>
            /// apoc.number.exact.add(stringA,stringB) - return the sum’s result of two large numbers
            /// </summary>
            public static StringResult Add(StringResult @stringA, StringResult @stringB)
            {
                return new StringResult(t => t.Fn_ApocNumberExact_Add, new object[] { @stringA, @stringB }, null);
            }
            /// <summary>
            /// apoc.number.exact.toFloat(string,[prec],[roundingMode]) - return the Float value of a large number
            /// </summary>
            public static FloatResult ToFloat(NumericResult @precision, StringResult @roundingMode, StringResult @stringA)
            {
                return new FloatResult(t => t.Fn_ApocNumberExact_ToFloat, new object[] { @precision, @roundingMode, @stringA }, null);
            }
            /// <summary>
            /// apoc.number.exact.toInteger(string,[prec],[roundingMode]) - return the Integer value of a large number
            /// </summary>
            public static NumericResult ToInteger(NumericResult @precision, StringResult @roundingMode, StringResult @stringA)
            {
                return new NumericResult(t => t.Fn_ApocNumberExact_ToInteger, new object[] { @precision, @roundingMode, @stringA }, null);
            }
        }

        public static partial class Path
        {
            /// <summary>
            /// apoc.path.combine(path1, path2) - combines the paths into one if the connecting node matches
            /// </summary>
            public static PathResult Combine(PathResult @first, PathResult @second)
            {
                return new PathResult(t => t.Fn_ApocPath_Combine, new object[] { @first, @second }, null);
            }
            /// <summary>
            /// apoc.path.create(startNode,[rels]) - creates a path instance of the given elements
            /// </summary>
            public static PathResult Create(AliasListResult @rels, AliasResult @startNode)
            {
                return new PathResult(t => t.Fn_ApocPath_Create, new object[] { @rels, @startNode }, null);
            }
            /// <summary>
            /// apoc.path.expand(startNode &lt;id&gt;|Node|list, &apos;TYPE|TYPE_OUT&gt;|&lt;TYPE_IN&apos;, &apos;+YesLabel|-NoLabel&apos;, minLevel, maxLevel ) yield path - expand from start node following the given relationships from min to max-level adhering to the label filters
            /// </summary>
            public static PathResult Expand(StringResult @labelFilter, NumericResult @maxLevel, NumericResult @minLevel, StringResult @relationshipFilter, MiscResult @start)
            {
                return new PathResult(t => t.Call_ApocPath_Expand, new object[] { @labelFilter, @maxLevel, @minLevel, @relationshipFilter, @start }, null);
            }
            /// <summary>
            /// apoc.path.expandConfig(startNode &lt;id&gt;|Node|list, {minLevel,maxLevel,uniqueness,relationshipFilter,labelFilter,uniqueness:&apos;RELATIONSHIP_PATH&apos;,bfs:true, filterStartNode:false, limit:-1, optional:false, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield path - expand from start node following the given relationships from min to max-level adhering to the label filters.
            /// </summary>
            public static PathResult ExpandConfig(MiscResult @config, MiscResult @start)
            {
                return new PathResult(t => t.Call_ApocPath_ExpandConfig, new object[] { @config, @start }, null);
            }
            /// <summary>
            /// apoc.path.spanningTree(startNode &lt;id&gt;|Node|list, {maxLevel,relationshipFilter,labelFilter,bfs:true, filterStartNode:false, limit:-1, optional:false, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield path - expand a spanning tree reachable from start node following relationships to max-level adhering to the label filters
            /// </summary>
            public static PathResult SpanningTree(MiscResult @config, MiscResult @start)
            {
                return new PathResult(t => t.Call_ApocPath_SpanningTree, new object[] { @config, @start }, null);
            }
            /// <summary>
            /// apoc.path.subgraphAll(startNode &lt;id&gt;|Node|list, {maxLevel,relationshipFilter,labelFilter,bfs:true, filterStartNode:false, limit:-1, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield nodes, relationships - expand the subgraph reachable from start node following relationships to max-level adhering to the label filters, and also return all relationships within the subgraph
            /// </summary>
            public static AliasListResult SubgraphAll(MiscResult @config, MiscResult @start)
            {
                return new AliasListResult(t => t.Call_ApocPath_SubgraphAll, new object[] { @config, @start }, null);
            }
            /// <summary>
            /// apoc.path.subgraphNodes(startNode &lt;id&gt;|Node|list, {maxLevel,relationshipFilter,labelFilter,bfs:true, filterStartNode:false, limit:-1, optional:false, endNodes:[], terminatorNodes:[], sequence, beginSequenceAtStart:true}) yield node - expand the subgraph nodes reachable from start node following relationships to max-level adhering to the label filters
            /// </summary>
            public static AliasResult SubgraphNodes(MiscResult @config, MiscResult @start)
            {
                return new AliasResult(t => t.Call_ApocPath_SubgraphNodes, new object[] { @config, @start }, null);
            }
            /// <summary>
            /// apoc.path.elements(path) - returns a list of node-relationship-node-…​
            /// </summary>
            public static MiscListResult Elements(PathResult @path)
            {
                return new MiscListResult(t => t.Fn_ApocPath_Elements, new object[] { @path }, null);
            }
            /// <summary>
            /// apoc.path.slice(path, [offset], [length]) - creates a sub-path with the given offset and length
            /// </summary>
            public static PathResult Slice(NumericResult @length, NumericResult @offset, PathResult @path)
            {
                return new PathResult(t => t.Fn_ApocPath_Slice, new object[] { @length, @offset, @path }, null);
            }
        }

        public static partial class Periodic
        {
            /// <summary>
            /// apoc.periodic.cancel(name) - cancel job with the given name
            /// </summary>
            public static BooleanResult Cancel(StringResult @name)
            {
                return new BooleanResult(t => t.Call_ApocPeriodic_Cancel, new object[] { @name }, null);
            }
            /// <summary>
            /// apoc.periodic.commit(statement,params) - runs the given statement in separate transactions until it returns 0
            /// </summary>
            public static MiscResult Commit(MiscResult @params, StringResult @statement)
            {
                return new MiscResult(t => t.Call_ApocPeriodic_Commit, new object[] { @params, @statement }, null);
            }
            /// <summary>
            /// apoc.periodic.countdown(&apos;name&apos;,statement,repeat-rate-in-seconds) creates a background job that will repeatedly execute the given Cypher statement until it returns 0.
            /// </summary>
            public static BooleanResult Countdown(StringResult @name, NumericResult @rate, StringResult @statement)
            {
                return new BooleanResult(t => t.Call_ApocPeriodic_Countdown, new object[] { @name, @rate, @statement }, null);
            }
            /// <summary>
            /// apoc.periodic.iterate(&apos;statement returning items&apos;, &apos;statement per item&apos;, {batchSize:1000,iterateList:true,parallel:false,params:{},concurrency:50,retries:0}) YIELD batches, total - run the second statement for each item returned by the first statement. Returns number of batches and total processed rows
            /// </summary>
            public static MiscResult Iterate(MiscResult @config, StringResult @cypherAction, StringResult @cypherIterate)
            {
                return new MiscResult(t => t.Call_ApocPeriodic_Iterate, new object[] { @config, @cypherAction, @cypherIterate }, null);
            }
            /// <summary>
            /// apoc.periodic.repeat(&apos;name&apos;,statement,repeat-rate-in-seconds, config) submit a repeatedly-called background query. The parameter &apos;config&apos; is optional and can contain a &apos;params&apos; entry usable in nested Cypher statement.
            /// </summary>
            public static BooleanResult Repeat(MiscResult @config, StringResult @name, NumericResult @rate, StringResult @statement)
            {
                return new BooleanResult(t => t.Call_ApocPeriodic_Repeat, new object[] { @config, @name, @rate, @statement }, null);
            }
            /// <summary>
            /// apoc.periodic.submit(&apos;name&apos;,statement,params) - creates a background job which executes a Cypher statement once. The parameter &apos;params&apos; is optional and can contain query parameters for the Cypher statement
            /// </summary>
            public static BooleanResult Submit(StringResult @name, MiscResult @params, StringResult @statement)
            {
                return new BooleanResult(t => t.Call_ApocPeriodic_Submit, new object[] { @name, @params, @statement }, null);
            }
            /// <summary>
            /// apoc.periodic.truncate({config}) - removes all entities (and optionally indexes and constraints) from db using the apoc.periodic.iterate under the hood
            /// </summary>
            public static MiscResult Truncate()
            {
                return new MiscResult(t => t.Call_ApocPeriodic_Truncate(0), new object[] {  }, null);
            }
            /// <summary>
            /// apoc.periodic.truncate({config}) - removes all entities (and optionally indexes and constraints) from db using the apoc.periodic.iterate under the hood
            /// </summary>
            public static MiscResult Truncate(MiscResult @config)
            {
                return new MiscResult(t => t.Call_ApocPeriodic_Truncate(1), new object[] { @config }, null);
            }
            /// <summary>
            /// apoc.periodic.list - list all jobs
            /// </summary>
            public static BooleanResult List()
            {
                return new BooleanResult(t => t.Call_ApocPeriodic_List, new object[0], null);
            }
        }

        public static partial class Refactor
        {
            /// <summary>
            /// apoc.refactor.categorize(sourceKey, type, outgoing, label, targetKey, copiedKeys, batchSize) turn each unique propertyKey into a category node and connect to it
            /// </summary>
            public static MiscResult Categorize(NumericResult @batchSize, StringListResult @copiedKeys, StringResult @label, BooleanResult @outgoing, StringResult @sourceKey, StringResult @targetKey, StringResult @type)
            {
                return new MiscResult(t => t.Call_ApocRefactor_Categorize, new object[] { @batchSize, @copiedKeys, @label, @outgoing, @sourceKey, @targetKey, @type }, null);
            }
            /// <summary>
            /// apoc.refactor.cloneNodes([node1,node2,…​]) clone nodes with their labels and properties
            /// </summary>
            public static StringResult CloneNodes(AliasListResult @nodes)
            {
                return new StringResult(t => t.Call_ApocRefactor_CloneNodes(1), new object[] { @nodes }, null);
            }
            /// <summary>
            /// apoc.refactor.cloneNodes([node1,node2,…​]) clone nodes with their labels and properties
            /// </summary>
            public static StringResult CloneNodes(AliasListResult @nodes, StringListResult @skipProperties)
            {
                return new StringResult(t => t.Call_ApocRefactor_CloneNodes(2), new object[] { @nodes, @skipProperties }, null);
            }
            /// <summary>
            /// apoc.refactor.cloneNodes([node1,node2,…​]) clone nodes with their labels and properties
            /// </summary>
            public static StringResult CloneNodes(AliasListResult @nodes, StringListResult @skipProperties, BooleanResult @withRelationships)
            {
                return new StringResult(t => t.Call_ApocRefactor_CloneNodes(3), new object[] { @nodes, @skipProperties, @withRelationships }, null);
            }
            /// <summary>
            /// apoc.refactor.cloneNodesWithRelationships([node1,node2,…​]) clone nodes with their labels, properties and relationships
            /// </summary>
            public static StringResult CloneNodesWithRelationships(AliasListResult @nodes)
            {
                return new StringResult(t => t.Call_ApocRefactor_CloneNodesWithRelationships, new object[] { @nodes }, null);
            }
            /// <summary>
            /// apoc.refactor.cloneSubgraph([node1,node2,…​], [rel1,rel2,…​]:[], {standinNodes:[], skipProperties:[]}) YIELD input, output, error | clone nodes with their labels and properties (optionally skipping any properties in the skipProperties list via the config map), and clone the given relationships (will exist between cloned nodes only). If no relationships are provided, all relationships between the given nodes will be cloned. Relationships can be optionally redirected according to standinNodes node pairings (this is a list of list-pairs of nodes), so given a node in the original subgraph (first of the pair), an existing node (second of the pair) can act as a standin for it within the cloned subgraph. Cloned relationships will be redirected to the standin.
            /// </summary>
            public static StringResult CloneSubgraph(MiscResult @config, AliasListResult @nodes)
            {
                return new StringResult(t => t.Call_ApocRefactor_CloneSubgraph(2), new object[] { @config, @nodes }, null);
            }
            /// <summary>
            /// apoc.refactor.cloneSubgraph([node1,node2,…​], [rel1,rel2,…​]:[], {standinNodes:[], skipProperties:[]}) YIELD input, output, error | clone nodes with their labels and properties (optionally skipping any properties in the skipProperties list via the config map), and clone the given relationships (will exist between cloned nodes only). If no relationships are provided, all relationships between the given nodes will be cloned. Relationships can be optionally redirected according to standinNodes node pairings (this is a list of list-pairs of nodes), so given a node in the original subgraph (first of the pair), an existing node (second of the pair) can act as a standin for it within the cloned subgraph. Cloned relationships will be redirected to the standin.
            /// </summary>
            public static StringResult CloneSubgraph(MiscResult @config, AliasListResult @nodes, AliasListResult @rels)
            {
                return new StringResult(t => t.Call_ApocRefactor_CloneSubgraph(3), new object[] { @config, @nodes, @rels }, null);
            }
            /// <summary>
            /// apoc.refactor.cloneSubgraphFromPaths([path1, path2, …​], {standinNodes:[], skipProperties:[]}) YIELD input, output, error | from the subgraph formed from the given paths, clone nodes with their labels and properties (optionally skipping any properties in the skipProperties list via the config map), and clone the relationships (will exist between cloned nodes only). Relationships can be optionally redirected according to standinNodes node pairings (this is a list of list-pairs of nodes), so given a node in the original subgraph (first of the pair), an existing node (second of the pair) can act as a standin for it within the cloned subgraph. Cloned relationships will be redirected to the standin.
            /// </summary>
            public static StringResult CloneSubgraphFromPaths(MiscResult @config, PathListResult @paths)
            {
                return new StringResult(t => t.Call_ApocRefactor_CloneSubgraphFromPaths, new object[] { @config, @paths }, null);
            }
            /// <summary>
            /// apoc.refactor.collapseNode([node1,node2],&apos;TYPE&apos;) collapse node to relationship, node with one rel becomes self-relationship
            /// </summary>
            public static StringResult CollapseNode(MiscResult @nodes, StringResult @type)
            {
                return new StringResult(t => t.Call_ApocRefactor_CollapseNode, new object[] { @nodes, @type }, null);
            }
            /// <summary>
            /// apoc.refactor.deleteAndReconnect([pathLinkedList], [nodesToRemove], {config}) - Removes some nodes from a linked list
            /// </summary>
            public static AliasListResult DeleteAndReconnect(MiscResult @config, AliasListResult @nodes, PathResult @path)
            {
                return new AliasListResult(t => t.Call_ApocRefactor_DeleteAndReconnect, new object[] { @config, @nodes, @path }, null);
            }
            /// <summary>
            /// apoc.refactor.extractNode([rel1,rel2,…​], [labels],&apos;OUT&apos;,&apos;IN&apos;) extract node from relationships
            /// </summary>
            public static StringResult ExtractNode(StringResult @inType, StringListResult @labels, StringResult @outType, MiscResult @relationships)
            {
                return new StringResult(t => t.Call_ApocRefactor_ExtractNode, new object[] { @inType, @labels, @outType, @relationships }, null);
            }
            /// <summary>
            /// apoc.refactor.invert(rel) inverts relationship direction
            /// </summary>
            public static StringResult Invert(AliasResult @relationship)
            {
                return new StringResult(t => t.Call_ApocRefactor_Invert, new object[] { @relationship }, null);
            }
            /// <summary>
            /// apoc.refactor.mergeNodes([node1,node2],[{properties:&apos;overwrite&apos; or &apos;discard&apos; or &apos;combine&apos;}]) merge nodes onto first in list
            /// </summary>
            public static AliasResult MergeNodes(MiscResult @config, AliasListResult @nodes)
            {
                return new AliasResult(t => t.Call_ApocRefactor_MergeNodes, new object[] { @config, @nodes }, null);
            }
            /// <summary>
            /// apoc.refactor.mergeRelationships([rel1,rel2]) merge relationships onto first in list
            /// </summary>
            public static AliasResult MergeRelationships(MiscResult @config, AliasListResult @rels)
            {
                return new AliasResult(t => t.Call_ApocRefactor_MergeRelationships, new object[] { @config, @rels }, null);
            }
            /// <summary>
            /// apoc.refactor.normalizeAsBoolean(entity, propertyKey, true_values, false_values) normalize/convert a property to be boolean
            /// </summary>
            public static MiscResult NormalizeAsBoolean(MiscResult @entity, MiscListResult @false_values, StringResult @propertyKey, MiscListResult @true_values)
            {
                return new MiscResult(t => t.Call_ApocRefactor_NormalizeAsBoolean, new object[] { @entity, @false_values, @propertyKey, @true_values }, null);
            }
            /// <summary>
            /// apoc.refactor.setType(rel, &apos;NEW-TYPE&apos;) change relationship-type
            /// </summary>
            public static StringResult SetType(StringResult @newType, AliasResult @relationship)
            {
                return new StringResult(t => t.Call_ApocRefactor_SetType, new object[] { @newType, @relationship }, null);
            }
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static StringResult To(AliasResult @newNode, AliasResult @relationship)
            {
                return new StringResult(t => t.Call_ApocRefactor_To, new object[] { @newNode, @relationship }, null);
            }
            /// <summary>
            /// apoc.refactor.from(rel, startNode) redirect relationship to use new start-node
            /// </summary>
            public static StringResult From(AliasResult @newNode, AliasResult @relationship)
            {
                return new StringResult(t => t.Call_ApocRefactor_From, new object[] { @newNode, @relationship }, null);
            }
        }

        public static partial class RefactorRename
        {
            /// <summary>
            /// apoc.refactor.rename.label(oldLabel, newLabel, [nodes]) | rename a label from &apos;oldLabel&apos; to &apos;newLabel&apos; for all nodes. If &apos;nodes&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscResult Label(StringResult @newLabel, AliasListResult @nodes, StringResult @oldLabel)
            {
                return new MiscResult(t => t.Call_ApocRefactorRename_Label, new object[] { @newLabel, @nodes, @oldLabel }, null);
            }
            /// <summary>
            /// apoc.refactor.rename.nodeProperty(oldName, newName, [nodes], {config}) | rename all node’s property from &apos;oldName&apos; to &apos;newName&apos;. If &apos;nodes&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscResult NodeProperty(MiscResult @config, StringResult @newName, AliasListResult @nodes, StringResult @oldName)
            {
                return new MiscResult(t => t.Call_ApocRefactorRename_NodeProperty, new object[] { @config, @newName, @nodes, @oldName }, null);
            }
            /// <summary>
            /// apoc.refactor.rename.typeProperty(oldName, newName, [rels], {config}) | rename all relationship’s property from &apos;oldName&apos; to &apos;newName&apos;. If &apos;rels&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscResult TypeProperty(MiscResult @config, StringResult @newName, StringResult @oldName)
            {
                return new MiscResult(t => t.Call_ApocRefactorRename_TypeProperty(3), new object[] { @config, @newName, @oldName }, null);
            }
            /// <summary>
            /// apoc.refactor.rename.typeProperty(oldName, newName, [rels], {config}) | rename all relationship’s property from &apos;oldName&apos; to &apos;newName&apos;. If &apos;rels&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscResult TypeProperty(MiscResult @config, StringResult @newName, StringResult @oldName, AliasListResult @rels)
            {
                return new MiscResult(t => t.Call_ApocRefactorRename_TypeProperty(4), new object[] { @config, @newName, @oldName, @rels }, null);
            }
            /// <summary>
            /// apoc.refactor.rename.type(oldType, newType, [rels], {config}) | rename all relationships with type &apos;oldType&apos; to &apos;newType&apos;. If &apos;rels&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscResult Type(MiscResult @config, StringResult @newType, StringResult @oldType)
            {
                return new MiscResult(t => t.Call_ApocRefactorRename_Type(3), new object[] { @config, @newType, @oldType }, null);
            }
            /// <summary>
            /// apoc.refactor.rename.type(oldType, newType, [rels], {config}) | rename all relationships with type &apos;oldType&apos; to &apos;newType&apos;. If &apos;rels&apos; is provided renaming is applied to this set only
            /// </summary>
            public static MiscResult Type(MiscResult @config, StringResult @newType, StringResult @oldType, AliasListResult @rels)
            {
                return new MiscResult(t => t.Call_ApocRefactorRename_Type(4), new object[] { @config, @newType, @oldType, @rels }, null);
            }
        }

        public static partial class Rel
        {
            /// <summary>
            /// returns endNode for (virtual) relationships
            /// </summary>
            public static AliasResult EndNode(AliasResult @rel)
            {
                return new AliasResult(t => t.Fn_ApocRel_EndNode, new object[] { @rel }, null);
            }
            /// <summary>
            /// returns startNode for (virtual) relationships
            /// </summary>
            public static AliasResult StartNode(AliasResult @rel)
            {
                return new AliasResult(t => t.Fn_ApocRel_StartNode, new object[] { @rel }, null);
            }
            /// <summary>
            /// returns id for (virtual) relationships
            /// </summary>
            public static NumericResult Id(AliasResult @rel)
            {
                return new NumericResult(t => t.Fn_ApocRel_Id, new object[] { @rel }, null);
            }
            /// <summary>
            /// returns type for (virtual) relationships
            /// </summary>
            public static StringResult Type(AliasResult @rel)
            {
                return new StringResult(t => t.Fn_ApocRel_Type, new object[] { @rel }, null);
            }
        }

        public static partial class Schema
        {
            /// <summary>
            /// apoc.schema.assert({indexLabel:, …​}, {constraintLabel:[constraintKeys], …​}, dropExisting : true) yield label, key, keys, unique, action - drops all other existing indexes and constraints when dropExisting is true (default is true), and asserts that at the end of the operation the given indexes and unique constraints are there, each label:key pair is considered one constraint/label. Non-constraint indexes can define compound indexes with label:[key1,key2…​] pairings.
            /// </summary>
            public static StringResult Assert(MiscResult @constraints, BooleanResult @dropExisting, MiscResult @indexes)
            {
                return new StringResult(t => t.Call_ApocSchema_Assert, new object[] { @constraints, @dropExisting, @indexes }, null);
            }
            /// <summary>
            /// CALL apoc.schema.relationships([config]) yield name, startLabel, type, endLabel, properties, status
            /// </summary>
            public static StringResult Relationships()
            {
                return new StringResult(t => t.Call_ApocSchema_Relationships(0), new object[] {  }, null);
            }
            /// <summary>
            /// CALL apoc.schema.relationships([config]) yield name, startLabel, type, endLabel, properties, status
            /// </summary>
            public static StringResult Relationships(MiscResult @config)
            {
                return new StringResult(t => t.Call_ApocSchema_Relationships(1), new object[] { @config }, null);
            }
            /// <summary>
            /// CALL apoc.schema.nodes([config]) yield name, label, properties, status, type
            /// </summary>
            public static StringResult Nodes()
            {
                return new StringResult(t => t.Call_ApocSchema_Nodes(0), new object[] {  }, null);
            }
            /// <summary>
            /// CALL apoc.schema.nodes([config]) yield name, label, properties, status, type
            /// </summary>
            public static StringResult Nodes(MiscResult @config)
            {
                return new StringResult(t => t.Call_ApocSchema_Nodes(1), new object[] { @config }, null);
            }
        }

        public static partial class SchemaNode
        {
            /// <summary>
            /// RETURN apoc.schema.node.constraintExists(labelName, propertyNames)
            /// </summary>
            public static BooleanResult ConstraintExists(StringResult @labelName, StringListResult @propertyName)
            {
                return new BooleanResult(t => t.Fn_ApocSchemaNode_ConstraintExists, new object[] { @labelName, @propertyName }, null);
            }
            /// <summary>
            /// RETURN apoc.schema.node.indexExists(labelName, propertyNames)
            /// </summary>
            public static BooleanResult IndexExists(StringResult @labelName, StringListResult @propertyName)
            {
                return new BooleanResult(t => t.Fn_ApocSchemaNode_IndexExists, new object[] { @labelName, @propertyName }, null);
            }
        }

        public static partial class SchemaProperties
        {
            /// <summary>
            /// apoc.schema.properties.distinct(label, key) - quickly returns all distinct values for a given key
            /// </summary>
            public static MiscListResult Distinct(StringResult @key, StringResult @label)
            {
                return new MiscListResult(t => t.Call_ApocSchemaProperties_Distinct, new object[] { @key, @label }, null);
            }
            /// <summary>
            /// apoc.schema.properties.distinctCount([label], [key]) YIELD label, key, value, count - quickly returns all distinct values and counts for a given key
            /// </summary>
            public static NumericResult DistinctCount(StringResult @key, StringResult @label)
            {
                return new NumericResult(t => t.Call_ApocSchemaProperties_DistinctCount, new object[] { @key, @label }, null);
            }
        }

        public static partial class SchemaRelationship
        {
            /// <summary>
            /// RETURN apoc.schema.relationship.constraintExists(type, propertyNames)
            /// </summary>
            public static BooleanResult ConstraintExists(StringListResult @propertyName, StringResult @type)
            {
                return new BooleanResult(t => t.Fn_ApocSchemaRelationship_ConstraintExists, new object[] { @propertyName, @type }, null);
            }
            /// <summary>
            /// RETURN apoc.schema.relationship.indexExists(relName, propertyNames)
            /// </summary>
            public static BooleanResult IndexExists(StringResult @labelName, StringListResult @propertyName)
            {
                return new BooleanResult(t => t.Fn_ApocSchemaRelationship_IndexExists, new object[] { @labelName, @propertyName }, null);
            }
        }

        public static partial class Scoring
        {
            /// <summary>
            /// apoc.scoring.existence(5, true) returns the provided score if true, 0 if false
            /// </summary>
            public static FloatResult Existence(BooleanResult @exists, NumericResult @score)
            {
                return new FloatResult(t => t.Fn_ApocScoring_Existence, new object[] { @exists, @score }, null);
            }
            /// <summary>
            /// apoc.scoring.pareto(10, 20, 100, 11) applies a Pareto scoring function over the inputs
            /// </summary>
            public static FloatResult Pareto(NumericResult @eightyPercentValue, NumericResult @maximumValue, NumericResult @minimumThreshold, NumericResult @score)
            {
                return new FloatResult(t => t.Fn_ApocScoring_Pareto, new object[] { @eightyPercentValue, @maximumValue, @minimumThreshold, @score }, null);
            }
        }

        public static partial class Search
        {
            /// <summary>
            /// Do a parallel search over multiple indexes returning a reduced representation of the nodes found: node id, labels and the searched properties. apoc.search.multiSearchReduced( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ). Multiple search results for the same node are merged into one record.
            /// </summary>
            public static NumericResult MultiSearchReduced(MiscResult @LabelPropertyMap, StringResult @operator, StringResult @value)
            {
                return new NumericResult(t => t.Call_ApocSearch_MultiSearchReduced, new object[] { @LabelPropertyMap, @operator, @value }, null);
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning nodes. usage apoc.search.nodeAll( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ) returns all the Nodes found in the different searches.
            /// </summary>
            public static AliasResult NodeAll(MiscResult @LabelPropertyMap, StringResult @operator, StringResult @value)
            {
                return new AliasResult(t => t.Call_ApocSearch_NodeAll, new object[] { @LabelPropertyMap, @operator, @value }, null);
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning a reduced representation of the nodes found: node id, labels and the searched property. apoc.search.nodeShortAll( map of label and properties which will be searched upon, operator: EXACT / CONTAINS / STARTS WITH | ENDS WITH / = / &lt;&gt; / &lt; / &gt; …​, value ). All &apos;hits&apos; are returned.
            /// </summary>
            public static NumericResult NodeAllReduced(MiscResult @LabelPropertyMap, StringResult @operator, MiscResult @value)
            {
                return new NumericResult(t => t.Call_ApocSearch_NodeAllReduced, new object[] { @LabelPropertyMap, @operator, @value }, null);
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning a reduced representation of the nodes found: node id, labels and the searched properties. apoc.search.nodeReduced( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ). Multiple search results for the same node are merged into one record.
            /// </summary>
            public static NumericResult NodeReduced(MiscResult @LabelPropertyMap, StringResult @operator, StringResult @value)
            {
                return new NumericResult(t => t.Call_ApocSearch_NodeReduced, new object[] { @LabelPropertyMap, @operator, @value }, null);
            }
            /// <summary>
            /// Do a parallel search over multiple indexes returning nodes. usage apoc.search.node( map of label and properties which will be searched upon, operator: EXACT | CONTAINS | STARTS WITH | ENDS WITH, searchValue ) returns all the DISTINCT Nodes found in the different searches.
            /// </summary>
            public static AliasResult Node(MiscResult @LabelPropertyMap, StringResult @operator, StringResult @value)
            {
                return new AliasResult(t => t.Call_ApocSearch_Node, new object[] { @LabelPropertyMap, @operator, @value }, null);
            }
        }

        public static partial class Spatial
        {
            /// <summary>
            /// apoc.spatial.geocode(&apos;address&apos;, maxResults, quotaException, $config) YIELD location, latitude, longitude, description, osmData - look up geographic location of address from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscResult Geocode(MiscResult @config, StringResult @location)
            {
                return new MiscResult(t => t.Call_ApocSpatial_Geocode(2), new object[] { @config, @location }, null);
            }
            /// <summary>
            /// apoc.spatial.geocode(&apos;address&apos;, maxResults, quotaException, $config) YIELD location, latitude, longitude, description, osmData - look up geographic location of address from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscResult Geocode(MiscResult @config, StringResult @location, NumericResult @maxResults)
            {
                return new MiscResult(t => t.Call_ApocSpatial_Geocode(3), new object[] { @config, @location, @maxResults }, null);
            }
            /// <summary>
            /// apoc.spatial.geocode(&apos;address&apos;, maxResults, quotaException, $config) YIELD location, latitude, longitude, description, osmData - look up geographic location of address from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscResult Geocode(MiscResult @config, StringResult @location, NumericResult @maxResults, BooleanResult @quotaException)
            {
                return new MiscResult(t => t.Call_ApocSpatial_Geocode(4), new object[] { @config, @location, @maxResults, @quotaException }, null);
            }
            /// <summary>
            /// apoc.spatial.geocodeOnce(&apos;address&apos;, $config) YIELD location, latitude, longitude, description, osmData - look up geographic location of address from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscResult GeocodeOnce(MiscResult @config, StringResult @location)
            {
                return new MiscResult(t => t.Call_ApocSpatial_GeocodeOnce, new object[] { @config, @location }, null);
            }
            /// <summary>
            /// apoc.spatial.reverseGeocode(latitude,longitude, quotaException, $config) YIELD location, latitude, longitude, description - look up address from latitude and longitude from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscResult ReverseGeocode(MiscResult @config, FloatResult @latitude, FloatResult @longitude)
            {
                return new MiscResult(t => t.Call_ApocSpatial_ReverseGeocode(3), new object[] { @config, @latitude, @longitude }, null);
            }
            /// <summary>
            /// apoc.spatial.reverseGeocode(latitude,longitude, quotaException, $config) YIELD location, latitude, longitude, description - look up address from latitude and longitude from a geocoding service (the default one is OpenStreetMap)
            /// </summary>
            public static MiscResult ReverseGeocode(MiscResult @config, FloatResult @latitude, FloatResult @longitude, BooleanResult @quotaException)
            {
                return new MiscResult(t => t.Call_ApocSpatial_ReverseGeocode(4), new object[] { @config, @latitude, @longitude, @quotaException }, null);
            }
            /// <summary>
            /// apoc.spatial.sortByDistance(List&lt;Path&gt;) sort the given paths based on the geo informations (lat/long) in ascending order
            /// </summary>
            public static FloatResult SortByDistance(PathListResult @paths)
            {
                return new FloatResult(t => t.Call_ApocSpatial_SortByDistance, new object[] { @paths }, null);
            }
        }

        public static partial class Stats
        {
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static StringResult Degrees(StringResult @types)
            {
                return new StringResult(t => t.Call_ApocStats_Degrees, new object[] { @types }, null);
            }
        }

        public static partial class SystemdbExport
        {
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static NumericResult Metadata()
            {
                return new NumericResult(t => t.Call_ApocSystemdbExport_Metadata(0), new object[] {  }, null);
            }
            /// <summary>
            /// No documentation available.
            /// </summary>
            public static NumericResult Metadata(MiscResult @config)
            {
                return new NumericResult(t => t.Call_ApocSystemdbExport_Metadata(1), new object[] { @config }, null);
            }
        }

        public static partial class Temporal
        {
            /// <summary>
            /// apoc.temporal.formatDuration(input, format) | Format a Duration
            /// </summary>
            public static StringResult FormatDuration(StringResult @format, MiscResult @input)
            {
                return new StringResult(t => t.Fn_ApocTemporal_FormatDuration, new object[] { @format, @input }, null);
            }
            /// <summary>
            /// apoc.temporal.toZonedTemporal(&apos;2012-12-23 23:59:59&apos;,&apos;yyyy-MM-dd HH:mm:ss&apos;, &apos;UTC-hour-offset&apos;) parse date string using the specified format to specified timezone
            /// </summary>
            public static MiscResult ToZonedTemporal(StringResult @format, StringResult @time)
            {
                return new MiscResult(t => t.Fn_ApocTemporal_ToZonedTemporal(2), new object[] { @format, @time }, null);
            }
            /// <summary>
            /// apoc.temporal.toZonedTemporal(&apos;2012-12-23 23:59:59&apos;,&apos;yyyy-MM-dd HH:mm:ss&apos;, &apos;UTC-hour-offset&apos;) parse date string using the specified format to specified timezone
            /// </summary>
            public static MiscResult ToZonedTemporal(StringResult @format, StringResult @time, StringResult @timezone)
            {
                return new MiscResult(t => t.Fn_ApocTemporal_ToZonedTemporal(3), new object[] { @format, @time, @timezone }, null);
            }
            /// <summary>
            /// apoc.temporal.format(input, format) | Format a temporal value
            /// </summary>
            public static StringResult Format(StringResult @format, MiscResult @temporal)
            {
                return new StringResult(t => t.Fn_ApocTemporal_Format, new object[] { @format, @temporal }, null);
            }
        }

        public static partial class Text
        {
            /// <summary>
            /// apoc.text.base64Decode(text) YIELD value - Decode Base64 encoded string
            /// </summary>
            public static StringResult Base64Decode(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Base64Decode, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.base64Encode(text) YIELD value - Encode a string with Base64
            /// </summary>
            public static StringResult Base64Encode(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Base64Encode, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.base64UrlDecode(url) YIELD value - Decode Base64 encoded url
            /// </summary>
            public static StringResult Base64UrlDecode(StringResult @url)
            {
                return new StringResult(t => t.Fn_ApocText_Base64UrlDecode, new object[] { @url }, null);
            }
            /// <summary>
            /// apoc.text.base64UrlEncode(text) YIELD value - Encode a url with Base64
            /// </summary>
            public static StringResult Base64UrlEncode(StringResult @url)
            {
                return new StringResult(t => t.Fn_ApocText_Base64UrlEncode, new object[] { @url }, null);
            }
            /// <summary>
            /// apoc.text.byteCount(text,[charset]) - return size of text in bytes
            /// </summary>
            public static NumericResult ByteCount(StringResult @charset, StringResult @text)
            {
                return new NumericResult(t => t.Fn_ApocText_ByteCount, new object[] { @charset, @text }, null);
            }
            /// <summary>
            /// apoc.text.bytes(text,[charset]) - return bytes of the text
            /// </summary>
            public static MiscListResult Bytes(StringResult @charset, StringResult @text)
            {
                return new MiscListResult(t => t.Fn_ApocText_Bytes, new object[] { @charset, @text }, null);
            }
            /// <summary>
            /// apoc.text.camelCase(text) YIELD value - Convert a string to camelCase
            /// </summary>
            public static StringResult CamelCase(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_CamelCase, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.capitalize(text) YIELD value - capitalise the first letter of the word
            /// </summary>
            public static StringResult Capitalize(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Capitalize, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.capitalizeAll(text) YIELD value - capitalise the first letter of every word in the text
            /// </summary>
            public static StringResult CapitalizeAll(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_CapitalizeAll, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.charAt(text, index) - the decimal value of the character at the given index
            /// </summary>
            public static NumericResult CharAt(NumericResult @index, StringResult @text)
            {
                return new NumericResult(t => t.Fn_ApocText_CharAt, new object[] { @index, @text }, null);
            }
            /// <summary>
            /// apoc.text.code(codepoint) - Returns the unicode character of the given codepoint
            /// </summary>
            public static StringResult Code(NumericResult @codepoint)
            {
                return new StringResult(t => t.Fn_ApocText_Code, new object[] { @codepoint }, null);
            }
            /// <summary>
            /// apoc.text.compareCleaned(text1, text2) - compare the given strings stripped of everything except alpha numeric characters converted to lower case.
            /// </summary>
            public static BooleanResult CompareCleaned(StringResult @text1, StringResult @text2)
            {
                return new BooleanResult(t => t.Fn_ApocText_CompareCleaned, new object[] { @text1, @text2 }, null);
            }
            /// <summary>
            /// apoc.text.decapitalize(text) YIELD value - decapitalize the first letter of the word
            /// </summary>
            public static StringResult Decapitalize(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Decapitalize, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.decapitalizeAll(text) YIELD value - decapitalize the first letter of all words
            /// </summary>
            public static StringResult DecapitalizeAll(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_DecapitalizeAll, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.distance(text1, text2) - compare the given strings with the Levenshtein distance algorithm.
            /// </summary>
            public static NumericResult Distance(StringResult @text1, StringResult @text2)
            {
                return new NumericResult(t => t.Fn_ApocText_Distance, new object[] { @text1, @text2 }, null);
            }
            /// <summary>
            /// apoc.text.doubleMetaphone(value) yield value - Compute the Double Metaphone phonetic encoding of all words of the text value which can be a single string or a list of strings
            /// </summary>
            public static MiscResult DoubleMetaphone(StringResult @value)
            {
                return new MiscResult(t => t.Call_ApocText_DoubleMetaphone, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.text.fuzzyMatch(text1, text2) - check if 2 words can be matched in a fuzzy way. Depending on the length of the String it will allow more characters that needs to be edited to match the second String.
            /// </summary>
            public static BooleanResult FuzzyMatch(StringResult @text1, StringResult @text2)
            {
                return new BooleanResult(t => t.Fn_ApocText_FuzzyMatch, new object[] { @text1, @text2 }, null);
            }
            /// <summary>
            /// apoc.text.hammingDistance(text1, text2) - compare the given strings with the Hamming distance algorithm.
            /// </summary>
            public static NumericResult HammingDistance(StringResult @text1, StringResult @text2)
            {
                return new NumericResult(t => t.Fn_ApocText_HammingDistance, new object[] { @text1, @text2 }, null);
            }
            /// <summary>
            /// apoc.text.hexCharAt(text, index) - the hex value string of the character at the given index
            /// </summary>
            public static StringResult HexCharAt(NumericResult @index, StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_HexCharAt, new object[] { @index, @text }, null);
            }
            /// <summary>
            /// apoc.text.hexValue(value) - the hex value string of the given number
            /// </summary>
            public static StringResult HexValue(NumericResult @value)
            {
                return new StringResult(t => t.Fn_ApocText_HexValue, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.text.indexesOf(text, lookup, from=0, to=-1==len) - finds all occurences of the lookup string in the text, return list, from inclusive, to exclusive, empty list if not found, null if text is null.
            /// </summary>
            public static MiscListResult IndexesOf(NumericResult @from, StringResult @lookup, StringResult @text)
            {
                return new MiscListResult(t => t.Fn_ApocText_IndexesOf(3), new object[] { @from, @lookup, @text }, null);
            }
            /// <summary>
            /// apoc.text.indexesOf(text, lookup, from=0, to=-1==len) - finds all occurences of the lookup string in the text, return list, from inclusive, to exclusive, empty list if not found, null if text is null.
            /// </summary>
            public static MiscListResult IndexesOf(NumericResult @from, StringResult @lookup, StringResult @text, NumericResult @to)
            {
                return new MiscListResult(t => t.Fn_ApocText_IndexesOf(4), new object[] { @from, @lookup, @text, @to }, null);
            }
            /// <summary>
            /// apoc.text.jaroWinklerDistance(text1, text2) - compare the given strings with the Jaro-Winkler distance algorithm.
            /// </summary>
            public static FloatResult JaroWinklerDistance(StringResult @text1, StringResult @text2)
            {
                return new FloatResult(t => t.Fn_ApocText_JaroWinklerDistance, new object[] { @text1, @text2 }, null);
            }
            /// <summary>
            /// apoc.text.join([&apos;text1&apos;,&apos;text2&apos;,…​], delimiter) - join the given strings with the given delimiter.
            /// </summary>
            public static StringResult Join(StringResult @delimiter, StringListResult @texts)
            {
                return new StringResult(t => t.Fn_ApocText_Join, new object[] { @delimiter, @texts }, null);
            }
            /// <summary>
            /// apoc.text.levenshteinDistance(text1, text2) - compare the given strings with the Levenshtein distance algorithm.
            /// </summary>
            public static NumericResult LevenshteinDistance(StringResult @text1, StringResult @text2)
            {
                return new NumericResult(t => t.Fn_ApocText_LevenshteinDistance, new object[] { @text1, @text2 }, null);
            }
            /// <summary>
            /// apoc.text.levenshteinSimilarity(text1, text2) - calculate the similarity (a value within 0 and 1) between two texts.
            /// </summary>
            public static FloatResult LevenshteinSimilarity(StringResult @text1, StringResult @text2)
            {
                return new FloatResult(t => t.Fn_ApocText_LevenshteinSimilarity, new object[] { @text1, @text2 }, null);
            }
            /// <summary>
            /// apoc.text.lpad(text,count,delim) YIELD value - left pad the string to the given width
            /// </summary>
            public static StringResult Lpad(NumericResult @count, StringResult @delim, StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Lpad, new object[] { @count, @delim, @text }, null);
            }
            /// <summary>
            /// apoc.text.phonetic(value) yield value - Compute the US_ENGLISH phonetic soundex encoding of all words of the text value which can be a single string or a list of strings
            /// </summary>
            public static MiscResult Phonetic(StringResult @value)
            {
                return new MiscResult(t => t.Call_ApocText_Phonetic, new object[] { @value }, null);
            }
            /// <summary>
            /// apoc.text.phoneticDelta(text1, text2) yield phonetic1, phonetic2, delta - Compute the US_ENGLISH soundex character difference between two given strings
            /// </summary>
            public static NumericResult PhoneticDelta(StringResult @text1, StringResult @text2)
            {
                return new NumericResult(t => t.Call_ApocText_PhoneticDelta, new object[] { @text1, @text2 }, null);
            }
            /// <summary>
            /// apoc.text.random(length, valid) YIELD value - generate a random string
            /// </summary>
            public static StringResult Random(NumericResult @length)
            {
                return new StringResult(t => t.Fn_ApocText_Random(1), new object[] { @length }, null);
            }
            /// <summary>
            /// apoc.text.random(length, valid) YIELD value - generate a random string
            /// </summary>
            public static StringResult Random(NumericResult @length, StringResult @valid)
            {
                return new StringResult(t => t.Fn_ApocText_Random(2), new object[] { @length, @valid }, null);
            }
            /// <summary>
            /// apoc.text.regexGroups(text, regex) - return all matching groups of the regex on the given text.
            /// </summary>
            public static MiscListResult RegexGroups(StringResult @regex, StringResult @text)
            {
                return new MiscListResult(t => t.Fn_ApocText_RegexGroups, new object[] { @regex, @text }, null);
            }
            /// <summary>
            /// apoc.text.regreplace(text, regex, replacement) - replace each substring of the given string that matches the given regular expression with the given replacement.
            /// </summary>
            public static StringResult Regreplace(StringResult @regex, StringResult @replacement, StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Regreplace, new object[] { @regex, @replacement, @text }, null);
            }
            /// <summary>
            /// apoc.text.rpad(text,count,delim) YIELD value - right pad the string to the given width
            /// </summary>
            public static StringResult Rpad(NumericResult @count, StringResult @delim, StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Rpad, new object[] { @count, @delim, @text }, null);
            }
            /// <summary>
            /// apoc.text.slug(text, delim) - slug the text with the given delimiter
            /// </summary>
            public static StringResult Slug(StringResult @delim, StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Slug, new object[] { @delim, @text }, null);
            }
            /// <summary>
            /// apoc.text.snakeCase(text) YIELD value - Convert a string to snake-case
            /// </summary>
            public static StringResult SnakeCase(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_SnakeCase, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.sorensenDiceSimilarityWithLanguage(text1, text2, languageTag) - compare the given strings with the Sørensen–Dice coefficient formula, with the provided IETF language tag
            /// </summary>
            public static FloatResult SorensenDiceSimilarity(StringResult @languageTag, StringResult @text1, StringResult @text2)
            {
                return new FloatResult(t => t.Fn_ApocText_SorensenDiceSimilarity, new object[] { @languageTag, @text1, @text2 }, null);
            }
            /// <summary>
            /// apoc.text.swapCase(text) YIELD value - Swap the case of a string
            /// </summary>
            public static StringResult SwapCase(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_SwapCase, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.toCypher(value, {skipKeys,keepKeys,skipValues,keepValues,skipNull,node,relationship,start,end}) | tries it’s best to convert the value to a cypher-property-string
            /// </summary>
            public static StringResult ToCypher(MiscResult @config, MiscResult @value)
            {
                return new StringResult(t => t.Fn_ApocText_ToCypher, new object[] { @config, @value }, null);
            }
            /// <summary>
            /// apoc.text.toUpperCase(text) YIELD value - Convert a string to UPPER_CASE
            /// </summary>
            public static StringResult ToUpperCase(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_ToUpperCase, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.upperCamelCase(text) YIELD value - Convert a string to camelCase
            /// </summary>
            public static StringResult UpperCamelCase(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_UpperCamelCase, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.urldecode(text) - return the urldecoded text
            /// </summary>
            public static StringResult Urldecode(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Urldecode, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.urlencode(text) - return the urlencoded text
            /// </summary>
            public static StringResult Urlencode(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Urlencode, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.clean(text) - strip the given string of everything except alpha numeric characters and convert it to lower case.
            /// </summary>
            public static StringResult Clean(StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Clean, new object[] { @text }, null);
            }
            /// <summary>
            /// apoc.text.format(text,[params],language) - sprintf format the string with the params given
            /// </summary>
            public static StringResult Format(StringResult @language, MiscListResult @params, StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Format, new object[] { @language, @params, @text }, null);
            }
            /// <summary>
            /// apoc.text.indexOf(text, lookup, from=0, to=-1==len) - find the first occurence of the lookup string in the text, from inclusive, to exclusive, -1 if not found, null if text is null.
            /// </summary>
            public static NumericResult IndexOf(NumericResult @from, StringResult @lookup, StringResult @text)
            {
                return new NumericResult(t => t.Fn_ApocText_IndexOf(3), new object[] { @from, @lookup, @text }, null);
            }
            /// <summary>
            /// apoc.text.indexOf(text, lookup, from=0, to=-1==len) - find the first occurence of the lookup string in the text, from inclusive, to exclusive, -1 if not found, null if text is null.
            /// </summary>
            public static NumericResult IndexOf(NumericResult @from, StringResult @lookup, StringResult @text, NumericResult @to)
            {
                return new NumericResult(t => t.Fn_ApocText_IndexOf(4), new object[] { @from, @lookup, @text, @to }, null);
            }
            /// <summary>
            /// apoc.text.repeat(item, count) - string multiplication
            /// </summary>
            public static StringResult Repeat(NumericResult @count, StringResult @item)
            {
                return new StringResult(t => t.Fn_ApocText_Repeat, new object[] { @count, @item }, null);
            }
            /// <summary>
            /// apoc.text.replace(text, regex, replacement) - replace each substring of the given string that matches the given regular expression with the given replacement.
            /// </summary>
            public static StringResult Replace(StringResult @regex, StringResult @replacement, StringResult @text)
            {
                return new StringResult(t => t.Fn_ApocText_Replace, new object[] { @regex, @replacement, @text }, null);
            }
            /// <summary>
            /// apoc.text.split(text, regex, limit) - splits the given text around matches of the given regex.
            /// </summary>
            public static MiscListResult Split(NumericResult @limit, StringResult @regex, StringResult @text)
            {
                return new MiscListResult(t => t.Fn_ApocText_Split, new object[] { @limit, @regex, @text }, null);
            }
        }

        public static partial class Trigger
        {
            /// <summary>
            /// CALL apoc.trigger.pause(name) | it pauses the trigger
            /// </summary>
            public static BooleanResult Pause(StringResult @name)
            {
                return new BooleanResult(t => t.Call_ApocTrigger_Pause, new object[] { @name }, null);
            }
            /// <summary>
            /// CALL apoc.trigger.resume(name) | it resumes the paused trigger
            /// </summary>
            public static BooleanResult Resume(StringResult @name)
            {
                return new BooleanResult(t => t.Call_ApocTrigger_Resume, new object[] { @name }, null);
            }
            /// <summary>
            /// add a trigger kernelTransaction under a name, in the kernelTransaction you can use {createdNodes}, {deletedNodes} etc., the selector is {phase:&apos;before/after/rollback&apos;} returns previous and new trigger information. Takes in an optional configuration.
            /// </summary>
            public static BooleanResult Add(MiscResult @config, StringResult @kernelTransaction, StringResult @name, MiscResult @selector)
            {
                return new BooleanResult(t => t.Call_ApocTrigger_Add, new object[] { @config, @kernelTransaction, @name, @selector }, null);
            }
            /// <summary>
            /// list all installed triggers
            /// </summary>
            public static BooleanResult List()
            {
                return new BooleanResult(t => t.Call_ApocTrigger_List, new object[0], null);
            }
            /// <summary>
            /// remove previously added trigger, returns trigger information
            /// </summary>
            public static BooleanResult Remove(StringResult @name)
            {
                return new BooleanResult(t => t.Call_ApocTrigger_Remove, new object[] { @name }, null);
            }
            /// <summary>
            /// removes all previously added trigger, returns trigger information
            /// </summary>
            public static BooleanResult RemoveAll()
            {
                return new BooleanResult(t => t.Call_ApocTrigger_RemoveAll, new object[0], null);
            }
        }

        public static partial class Util
        {
            /// <summary>
            /// apoc.util.compress(string, {config}) | return a compressed byte[] in various format from a string
            /// </summary>
            public static BinaryResult Compress(MiscResult @config, StringResult @data)
            {
                return new BinaryResult(t => t.Fn_ApocUtil_Compress, new object[] { @config, @data }, null);
            }
            /// <summary>
            /// apoc.util.decompress(compressed, {config}) | return a string from a compressed byte[] in various format
            /// </summary>
            public static StringResult Decompress(MiscResult @config, BinaryResult @data)
            {
                return new StringResult(t => t.Fn_ApocUtil_Decompress, new object[] { @config, @data }, null);
            }
            /// <summary>
            /// apoc.util.md5([values]) | computes the md5 of the concatenation of all string values of the list. Unsuitable for cryptographic use-cases.
            /// </summary>
            public static StringResult Md5(params MiscListResult[] @values)
            {
                return new StringResult(t => t.Fn_ApocUtil_Md5(@values.Length), @values, null);
            }
            /// <summary>
            /// apoc.util.sha1([values]) | computes the sha1 of the concatenation of all string values of the list
            /// </summary>
            public static StringResult Sha1(params MiscListResult[] @values)
            {
                return new StringResult(t => t.Fn_ApocUtil_Sha1(@values.Length), @values, null);
            }
            /// <summary>
            /// apoc.util.sha256([values]) | computes the sha256 of the concatenation of all string values of the list
            /// </summary>
            public static StringResult Sha256(params MiscListResult[] @values)
            {
                return new StringResult(t => t.Fn_ApocUtil_Sha256(@values.Length), @values, null);
            }
            /// <summary>
            /// apoc.util.sha384([values]) | computes the sha384 of the concatenation of all string values of the list
            /// </summary>
            public static StringResult Sha384(params MiscListResult[] @values)
            {
                return new StringResult(t => t.Fn_ApocUtil_Sha384(@values.Length), @values, null);
            }
            /// <summary>
            /// apoc.util.sha512([values]) | computes the sha512 of the concatenation of all string values of the list
            /// </summary>
            public static StringResult Sha512(params MiscListResult[] @values)
            {
                return new StringResult(t => t.Fn_ApocUtil_Sha512(@values.Length), @values, null);
            }
            /// <summary>
            /// apoc.util.sleep(&lt;duration&gt;) | sleeps for &lt;duration&gt; millis, transaction termination is honored
            /// </summary>
            public static MiscResult Sleep(NumericResult @duration)
            {
                return new MiscResult(t => t.Call_ApocUtil_Sleep, new object[] { @duration }, null);
            }
            /// <summary>
            /// apoc.util.validatePredicate(predicate, message, params) | if the predicate yields to true raise an exception else returns true, for use inside WHERE subclauses
            /// </summary>
            public static BooleanResult ValidatePredicate(StringResult @message, MiscListResult @params, BooleanResult @predicate)
            {
                return new BooleanResult(t => t.Fn_ApocUtil_ValidatePredicate, new object[] { @message, @params, @predicate }, null);
            }
            /// <summary>
            /// apoc.util.validate(predicate, message, params) | if the predicate yields to true raise an exception
            /// </summary>
            public static MiscResult Validate(StringResult @message, MiscListResult @params, BooleanResult @predicate)
            {
                return new MiscResult(t => t.Call_ApocUtil_Validate, new object[] { @message, @params, @predicate }, null);
            }
        }

        public static partial class Warmup
        {
            /// <summary>
            /// apoc.warmup.run(loadProperties=false,loadDynamicProperties=false,loadIndexes=false) - quickly loads all nodes and rels into memory by skipping one page at a time
            /// </summary>
            public static NumericResult Run()
            {
                return new NumericResult(t => t.Call_ApocWarmup_Run(0), new object[] {  }, null);
            }
            /// <summary>
            /// apoc.warmup.run(loadProperties=false,loadDynamicProperties=false,loadIndexes=false) - quickly loads all nodes and rels into memory by skipping one page at a time
            /// </summary>
            public static NumericResult Run(BooleanResult @loadDynamicProperties)
            {
                return new NumericResult(t => t.Call_ApocWarmup_Run(1), new object[] { @loadDynamicProperties }, null);
            }
            /// <summary>
            /// apoc.warmup.run(loadProperties=false,loadDynamicProperties=false,loadIndexes=false) - quickly loads all nodes and rels into memory by skipping one page at a time
            /// </summary>
            public static NumericResult Run(BooleanResult @loadDynamicProperties, BooleanResult @loadIndexes)
            {
                return new NumericResult(t => t.Call_ApocWarmup_Run(2), new object[] { @loadDynamicProperties, @loadIndexes }, null);
            }
            /// <summary>
            /// apoc.warmup.run(loadProperties=false,loadDynamicProperties=false,loadIndexes=false) - quickly loads all nodes and rels into memory by skipping one page at a time
            /// </summary>
            public static NumericResult Run(BooleanResult @loadDynamicProperties, BooleanResult @loadIndexes, BooleanResult @loadProperties)
            {
                return new NumericResult(t => t.Call_ApocWarmup_Run(3), new object[] { @loadDynamicProperties, @loadIndexes, @loadProperties }, null);
            }
        }

        public static partial class Xml
        {
            /// <summary>
            /// Deprecated by apoc.import.xml
            /// </summary>
            public static AliasResult Import(MiscResult @config, StringResult @url)
            {
                return new AliasResult(t => t.Call_ApocXml_Import, new object[] { @config, @url }, null);
            }
            /// <summary>
            /// RETURN apoc.xml.parse(&lt;xml string&gt;, &lt;xPath string&gt;, config, false) AS value
            /// </summary>
            public static MiscResult Parse(MiscResult @config, StringResult @data)
            {
                return new MiscResult(t => t.Fn_ApocXml_Parse(2), new object[] { @config, @data }, null);
            }
            /// <summary>
            /// RETURN apoc.xml.parse(&lt;xml string&gt;, &lt;xPath string&gt;, config, false) AS value
            /// </summary>
            public static MiscResult Parse(MiscResult @config, StringResult @data, StringResult @path)
            {
                return new MiscResult(t => t.Fn_ApocXml_Parse(3), new object[] { @config, @data, @path }, null);
            }
            /// <summary>
            /// RETURN apoc.xml.parse(&lt;xml string&gt;, &lt;xPath string&gt;, config, false) AS value
            /// </summary>
            public static MiscResult Parse(MiscResult @config, StringResult @data, StringResult @path, BooleanResult @simple)
            {
                return new MiscResult(t => t.Fn_ApocXml_Parse(4), new object[] { @config, @data, @path, @simple }, null);
            }
        }
    }
}
