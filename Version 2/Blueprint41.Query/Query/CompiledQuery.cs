using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41.Query
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class CompiledQueryInfo : ICompiledQueryInfo
    {
        internal CompiledQueryInfo(CompileState state, AsResult[] resultColumns)
        {
            QueryText = state.Text.ToString();
            Parameters = state.Parameters.ToList();
            DefaultValues = state.Values.Where(item => !item.IsConstant).ToList();
            ConstantValues = state.Values.Where(item => item.IsConstant).ToList();
            ResultColumns = resultColumns;
            ResultColumnTypeByName = resultColumns.ToDictionary(key => key.GetFieldName()!, item => item.GetResultType());
            CompiledResultColumns = resultColumns
                .Where(item => item.GetFieldName() is not null)
                .Select(item => new FieldInfo(Transaction.RunningTransaction, item))
                .ToList();
            Errors = new List<string>(state.Errors);
        }

        public string QueryText { get; private set; }

        public IReadOnlyList<Parameter> Parameters { get; private set; }
        IReadOnlyList<IParameter> ICompiledQueryInfo.Parameters => Parameters;

        public IReadOnlyList<Parameter> DefaultValues { get; private set; }
        IReadOnlyList<IParameter> ICompiledQueryInfo.DefaultValues => DefaultValues;

        public IReadOnlyList<Parameter> ConstantValues { get; private set; }
        IReadOnlyList<IParameter> ICompiledQueryInfo.ConstantValues => ConstantValues;
        public IReadOnlyList<AsResult> ResultColumns { get; private set; }
        internal IReadOnlyList<FieldInfo> CompiledResultColumns { get; private set; }
        public IReadOnlyDictionary<string, Type?> ResultColumnTypeByName { get; private set; }

        public IReadOnlyList<string> Errors { get; private set; }
        public override string ToString()
        {
            Transaction transaction = Transaction.RunningTransaction;
            string cypherQuery = this.QueryText;
            Dictionary<string, object?> parameterValues = new Dictionary<string, object?>();

            foreach (var queryParameter in this.ConstantValues)
            {
                if (queryParameter.Value is null)
                    parameterValues.Add(string.Format("{{{0}}}", queryParameter.Name), null);
                else
                    parameterValues.Add(string.Format("{{{0}}}", queryParameter.Name), transaction.PersistenceProvider.ConvertToStoredType(queryParameter.Value.GetType(), queryParameter.Value));
            }

            foreach (var queryParam in parameterValues)
            {
                object paramValue = queryParam.Value?.GetType() == typeof(string) ? string.Format("'{0}'", queryParam.Value.ToString()) : queryParam.Value?.ToString() ?? "NULL";
                cypherQuery = cypherQuery.Replace(queryParam.Key, paramValue.ToString());
            }
            return cypherQuery;
        }
        private string DebuggerDisplay { get => ToString(); }

        internal class FieldInfo
        {
            internal FieldInfo(Transaction transaction, AsResult field)
            {
                Field      = field;
                FieldName  = field.GetFieldName() ?? throw new InvalidOperationException("GetFieldName() cannot be null here.");
                TargetType = field.GetResultType();
                ResultType = field.Result.GetType();
                Info       = ResultHelper.Of(ResultType);

                Conversion? converter       = (TargetType is null) ? null : transaction.PersistenceProvider.GetConverterFromStoredType(TargetType);
                ConvertMethod               = (converter is null) ? null : (Func<object?, object?>)converter.Convert;
                Entity? entity              = null;
                MethodInfo? getEntityMethod = ResultType.GetProperty("Entity")?.GetGetMethod();
                if (getEntityMethod is not null)
                    entity                  = getEntityMethod.Invoke(field.Result, null) as Entity;

                MapMethod     = new RuntimeRegistered<Func<NodeResult, string, Dictionary<string, object?>?, NodeMapping, EntityFlavor, OGM?>?>();
                NewList       = new RuntimeRegistered<Func<int, IList>?>();
                NewJaggedList = new RuntimeRegistered<Func<int, IList>?>();

                MethodInfo? blockedMethod = (entity is null) ? null : entity!.RuntimeClassType.Blocking?.GetMethod("Map", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, new Type[] { typeof(NodeResult), typeof(string), typeof(Dictionary<string, object>), typeof(NodeMapping), typeof(EntityFlavor) }, null);
                MethodInfo? asyncMethod   = (entity is null) ? null : entity!.RuntimeClassType.Async?.   GetMethod("Map", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, new Type[] { typeof(NodeResult), typeof(string), typeof(Dictionary<string, object>), typeof(NodeMapping), typeof(EntityFlavor) }, null);
                MapMethod.Blocking        = (blockedMethod is null) ? null : (Func<NodeResult, string, Dictionary<string, object?>?, NodeMapping, EntityFlavor, OGM?>?)Delegate.CreateDelegate(typeof(Func<NodeResult, string, Dictionary<string, object?>?, NodeMapping, EntityFlavor, OGM?>), blockedMethod, true);
                MapMethod.Async           = (asyncMethod is null)   ? null : (Func<NodeResult, string, Dictionary<string, object?>?, NodeMapping, EntityFlavor, OGM?>?)Delegate.CreateDelegate(typeof(Func<NodeResult, string, Dictionary<string, object?>?, NodeMapping, EntityFlavor, OGM?>), asyncMethod,   true);

                if (entity is null)
                    return;

                (NewList.Blocking, NewJaggedList.Blocking) = GetListFunctions(entity.RuntimeReturnType.Blocking);
                (NewList.Async,    NewJaggedList.Async)    = GetListFunctions(entity.RuntimeReturnType.Async);

                return;

                (Func<int, IList>? newList, Func<int, IList>? newJaggedList) GetListFunctions(Type? runtimeType)
                {
                    if (runtimeType is null)
                        return (null, null);

                    Type listType = typeof(List<>).MakeGenericType(runtimeType);
                    Type jaggedListType = typeof(List<>).MakeGenericType(listType);
                    ConstructorInfo listCtor = listType.GetConstructor(new Type[] { typeof(int) })!;
                    ConstructorInfo jaggedListCtor = jaggedListType.GetConstructor(new Type[] { typeof(int) })!;

                    ParameterExpression capacity1 = Expression.Parameter(typeof(int), "capacity");
                    ParameterExpression capacity2 = Expression.Parameter(typeof(int), "capacity");

                    return (
                            Expression.Lambda<Func<int, IList>>(Expression.New(listCtor, capacity1), capacity1).Compile(),
                            Expression.Lambda<Func<int, IList>>(Expression.New(jaggedListCtor, capacity2), capacity2).Compile()
                        );
                }
            }

            public AsResult Field { get; private set; }
            public string FieldName { get; private set; }
            public Type? TargetType { get; private set; }
            public Type ResultType { get; private set; }
            public ResultHelper Info { get; private set; }
            public Func<object?, object?>? ConvertMethod { get; private set; }
            public readonly RuntimeRegistered<Func<NodeResult, string, Dictionary<string, object?>?, NodeMapping, EntityFlavor, OGM?>?> MapMethod;
            public readonly RuntimeRegistered<Func<int, IList>?> NewList;
            public readonly RuntimeRegistered<Func<int, IList>?> NewJaggedList;
        }
    }
}
