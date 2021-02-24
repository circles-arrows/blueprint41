using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;

using Blueprint41.Core;
using Blueprint41.Query;

namespace Blueprint41
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class QueryExecutionContext
    {
        internal QueryExecutionContext(CompiledQuery query)
        {
            Transaction transaction = Transaction.RunningTransaction;

            CompiledQuery = query;
            Errors = query.Errors;
            QueryParameters = new Dictionary<string, (object?, bool)>();
            foreach (Parameter item in query.ConstantValues)
                QueryParameters.Add(item.Name, (transaction.PersistenceProviderFactory.ConvertToStoredType(item.Type!, item.Value), true));
            foreach (Parameter item in query.DefaultValues)
                QueryParameters.Add(item.Name, (transaction.PersistenceProviderFactory.ConvertToStoredType(item.Type!, item.Value), false));
        }

        public void SetParameter(string parameterName, object? value)
        {
            Transaction transaction = Transaction.RunningTransaction;
            if (value is null)
                AddOrSet(null);
            else
                AddOrSet(transaction.PersistenceProviderFactory.ConvertToStoredType(value.GetType(), value));

            void AddOrSet(object? value)
            {
                if (QueryParameters.ContainsKey(parameterName))
                {
                    if (QueryParameters[parameterName].isConstant)
                        throw new NotSupportedException("You cannot set the value of a constant.");

                    QueryParameters[parameterName] = (value, false);
                }
                else
                {
                    QueryParameters.Add(parameterName, (value, false));
                }
            }
        }
        public List<dynamic> Execute(NodeMapping nodeMapping = NodeMapping.AsReadOnlyEntity)
        {
            List<dynamic> items = new List<dynamic>();

            Transaction transaction = Transaction.RunningTransaction;
            Dictionary<string, object?> parameters = new Dictionary<string, object?>(QueryParameters.Count);
            foreach (KeyValuePair<string, (object? value, bool isConstant)> queryParameter in QueryParameters)
            {
                if (queryParameter.Value.value is null)
                    parameters.Add(queryParameter.Key, null);
                else
                    parameters.Add(queryParameter.Key, transaction.PersistenceProviderFactory.ConvertToStoredType(queryParameter.Value.GetType(), queryParameter.Value.value));
            }

            var result = transaction.Run(CompiledQuery.QueryText, parameters);
            foreach (var row in result)
            {
                IDictionary<string, object?> record = new ExpandoObject();
                foreach (var field in CompiledQuery.CompiledResultColumns)
                {
                    object? value;
                    if (row.Values.TryGetValue(field.FieldName, out value) && value is not null)
                    {
                        object? target = (field.ConvertMethod is null) ? value : field.ConvertMethod.Invoke(value);
                        if (target is not null)
                        {
                            if (field.Info.IsAlias)
                            {
                                if (!field.Info.IsList) // RETURNS INode
                                {
                                    RawNode node = target.As<RawNode>();
                                    target = (field.MapMethod is null || nodeMapping == NodeMapping.AsRawResult) ? (object?)node : field.MapMethod.Invoke(node, CompiledQuery.QueryText, parameters, nodeMapping);
                                }
                                else if (!field.Info.IsJaggedList) // RETURNS List<INode>
                                {
                                    List<object?>? nodeList = ((List<object?>?)target);
                                    IList? newList = null;
                                    if (nodeList is not null)
                                    {
                                        newList = (field.MapMethod is null || field.NewList is null || nodeMapping == NodeMapping.AsRawResult) ? new List<RawResult>(nodeList.Count) : field.NewList.Invoke(nodeList.Count);

                                        for (int index = 0; index < nodeList.Count; index++)
                                        {
                                            object? t = nodeList[index];
                                            if (t is null)
                                            {
                                                newList!.Add(null);
                                            }
                                            else
                                            {
                                                RawNode node = t.As<RawNode>();
                                                newList!.Add((field.MapMethod is null || field.NewList is null || nodeMapping == NodeMapping.AsRawResult) ? (object?)node : field.MapMethod.Invoke(node, CompiledQuery.QueryText, parameters, nodeMapping));
                                            }
                                        }
                                    }
                                    target = newList;
                                }
                                else // RETURNS List<List<INode>>
                                {
                                    List<List<object?>?>? jaggedNodeList = ((List<List<object?>?>?)target);
                                    IList? newJaggedList = null;
                                    if (jaggedNodeList is not null)
                                    {
                                        newJaggedList = (field.MapMethod is null || field.NewList is null || field.NewJaggedList is null || nodeMapping == NodeMapping.AsRawResult) ? new List<List<RawResult?>?>(jaggedNodeList.Count) : field.NewJaggedList.Invoke(jaggedNodeList.Count);

                                        for (int mainindex = 0; mainindex < jaggedNodeList.Count; mainindex++)
                                        {
                                            List<object?>? nodeList = jaggedNodeList[mainindex];
                                            if (nodeList is null)
                                            {
                                                newJaggedList!.Add(null);
                                            }
                                            else
                                            {
                                                IList newList = (field.MapMethod is null || field.NewList is null || nodeMapping == NodeMapping.AsRawResult) ? new List<RawResult>(nodeList.Count) : field.NewList.Invoke(nodeList.Count);

                                                for (int subindex = 0; subindex < nodeList.Count; subindex++)
                                                {
                                                    object? t = nodeList[subindex];
                                                    if (t is null)
                                                    {
                                                        newList.Add(null);
                                                    }
                                                    else
                                                    {
                                                        RawNode node = t.As<RawNode>();
                                                        newList.Add((field.MapMethod is null || field.NewList is null || field.NewJaggedList is null || nodeMapping == NodeMapping.AsRawResult) ? (object?)node : field.MapMethod.Invoke(node, CompiledQuery.QueryText, parameters, nodeMapping));
                                                    }
                                                }

                                                newJaggedList!.Add(newList);
                                            }
                                        }
                                    }
                                    target = newJaggedList;
                                }
                            }
                        }
                        record.Add(field.FieldName, target);
                    }
                    else
                    {
                        record.Add(field.FieldName, null);
                    }
                }
                items.Add(record);
            }
            return items;
        }

        public CompiledQuery CompiledQuery { get; set; }
        public IReadOnlyList<string> Errors { get; private set; }
        internal Dictionary<string, (object? value, bool isConstant)> QueryParameters { get; private set; }
        public override string ToString()
        {
            Transaction transaction = Transaction.RunningTransaction;
            string cypherQuery = CompiledQuery.QueryText;
            Dictionary<string, object?> parameterValues = new Dictionary<string, object?>();

            foreach (KeyValuePair<string, (object? value, bool isConstant)> queryParameter in QueryParameters)
            {
                if (queryParameter.Value.value is null)
                    parameterValues.Add(string.Format("{{{0}}}", queryParameter.Key), null);
                else
                    parameterValues.Add(string.Format("{{{0}}}", queryParameter.Key), transaction.PersistenceProviderFactory.ConvertToStoredType(queryParameter.Value.GetType(), queryParameter.Value.value));
            }

            foreach (KeyValuePair<string, object?> queryParam in parameterValues)
            {
                string? paramValue = queryParam.Value?.GetType() == typeof(string) ? string.Format("'{0}'", queryParam.Value.ToString()) : queryParam.Value?.ToString();
                cypherQuery = cypherQuery.Replace(queryParam.Key, paramValue);
            }
            return cypherQuery;
        }
        private string DebuggerDisplay { get => ToString(); }
    }
    public enum NodeMapping
    {
        AsRawResult,
        AsReadOnlyEntity,
        AsWritableEntity,
    }
}
