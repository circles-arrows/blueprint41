﻿using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.Core;
using System.Reflection;
using System.Collections.ObjectModel;
using Blueprint41;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using Blueprint41.Response;

namespace System.Linq
{
    public static partial class ExtensionMethods
    {
        public static string ToCSharp(this Type type, bool nullable = false)
        {
            CodeDomProvider csharpProvider = CodeDomProvider.CreateProvider("C#");
            CodeTypeReference typeReference = new CodeTypeReference(type);
            CodeVariableDeclarationStatement variableDeclaration = new CodeVariableDeclarationStatement(typeReference, "dummy");
            StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                csharpProvider.GenerateCodeFromStatement(variableDeclaration, writer, new CodeGeneratorOptions());
            }
            sb.Replace(" dummy;\r\n", null);
            if (nullable && type.IsValueType)
                return sb.ToString() + "?";
            return sb.ToString();
        }

        public static bool IsSubclassOfOrSelf(this Type self, Type type)
        {
            if (self == type)
                return true;

            return self.IsSubclassOf(type);
        }

        #region Hashset Extensions

        private static class HashSetDelegateHolder<T>
        {
            private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;
            public static MethodInfo InitializeMethod { get; } = typeof(HashSet<T>).GetMethod("Initialize", Flags);
        }

        public static void SetCapacity<T>(this HashSet<T> hs, int capacity)
        {
            HashSetDelegateHolder<T>.InitializeMethod.Invoke(hs, new object[] { capacity });
        }

        public static HashSet<T> GetHashSet<T>(int capacity)
        {
            var hashSet = new HashSet<T>();
            hashSet.SetCapacity(capacity);
            return hashSet;
        }

        #endregion

        public static IEnumerable<ReadOnlyCollection<T>> Chunks<T>(this IEnumerable<T> source, int pageSize)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var currentPage = new List<T>(pageSize)
            {
                enumerator.Current
            };

                    while (currentPage.Count < pageSize && enumerator.MoveNext())
                    {
                        currentPage.Add(enumerator.Current);
                    }
                    yield return new ReadOnlyCollection<T>(currentPage);
                }
            }
        }

        internal static bool IsCompatible(this RelationshipType self, RelationshipType other)
        {
            if (self == RelationshipType.None || other == RelationshipType.None)
                return true;

            switch (self)
            {
                case RelationshipType.Collection:
                    {
                        switch (other)
                        {
                            case RelationshipType.Collection:
                            case RelationshipType.Collection_Collection:
                            case RelationshipType.Collection_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Collection_Collection:
                    {
                        switch (other)
                        {
                            case RelationshipType.Collection:
                            case RelationshipType.Collection_Collection:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Collection_Lookup:
                    {
                        switch (other)
                        {
                            case RelationshipType.Collection:
                            case RelationshipType.Collection_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Lookup:
                    {
                        switch (other)
                        {
                            case RelationshipType.Lookup:
                            case RelationshipType.Lookup_Collection:
                            case RelationshipType.Lookup_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Lookup_Collection:
                    {
                        switch (other)
                        {
                            case RelationshipType.Lookup:
                            case RelationshipType.Lookup_Collection:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Lookup_Lookup:
                    {
                        switch (other)
                        {
                            case RelationshipType.Lookup:
                            case RelationshipType.Lookup_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                default:
                    throw new NotImplementedException();
            }
        }
        internal static bool IsImplicitLookup(this RelationshipType self)
        {
            switch (self)
            {
                case RelationshipType.None:
                case RelationshipType.Collection:
                case RelationshipType.Collection_Collection:
                    return false;
                case RelationshipType.Collection_Lookup:
                case RelationshipType.Lookup:
                case RelationshipType.Lookup_Collection:
                case RelationshipType.Lookup_Lookup:
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }
        internal static bool IsImplicitCollection(this RelationshipType self)
        {
            switch (self)
            {
                case RelationshipType.None:
                case RelationshipType.Collection:
                case RelationshipType.Collection_Collection:
                case RelationshipType.Collection_Lookup:
                case RelationshipType.Lookup:
                case RelationshipType.Lookup_Collection:
                    return true;
                case RelationshipType.Lookup_Lookup:
                    return false;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
namespace System
{
    public static partial class ExtensionMethods
    {
        public static string ToJson<T>(this T self)
        {
            using (MemoryStream writer = new MemoryStream())
            {
                Cache<T>.JsonSerializer.WriteObject(writer, self);
                return Encoding.UTF8.GetString(writer.ToArray());
            }
        }

        public static string ToCypherString(this string cypher, Dictionary<string, object> parameters)
        {
            if (parameters != null)
            {
                foreach (var par in parameters)
                {
                    string jsonValue;

                    // it won't properly serialize a dictionary if not passed by the type itself.
                    if (par.Value is Dictionary<string, object> dictionaryValue)
                        jsonValue = ToJson(dictionaryValue);
                    else
                        jsonValue = ToJson(par.Value);

                    jsonValue = Regex.Replace(jsonValue, "(\"*[\\w\\d]*\":)", (match) => { return match.Value.Replace("\"", string.Empty); });
                    jsonValue = jsonValue.Replace('"', '\'');
                    cypher = cypher.Replace("{" + par.Key + "}", jsonValue);
                }
            }

            return cypher;
        }

        public static T FromJson<T>(this string self)
        {
            using (MemoryStream reader = new MemoryStream(Encoding.UTF8.GetBytes(self)))
            {
                return (T)Cache<T>.JsonSerializer.ReadObject(reader);
            }
        }

        private class Cache<T>
        {
            public static DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings()
            {
                UseSimpleDictionaryFormat = true,
            });
        }
    }
}

namespace Neo4j.Driver.V1
{
    public static partial class ExtensionMethods
    {
        public static IGraphResponse RunCypher(this IStatementRunner runner, string statement)
        {
            IStatementResult result = runner.Run(statement);
            return GraphResponse.GetGraphResponse<IStatementResult>(result);
        }

        public static IGraphResponse RunCypher(this IStatementRunner runner, string statement, Dictionary<string, object> parameters)
        {
            IStatementResult result = runner.Run(statement, parameters);
            return GraphResponse.GetGraphResponse<IStatementResult>(result);
        }
    }
}
