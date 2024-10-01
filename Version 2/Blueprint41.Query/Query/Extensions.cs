using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Core;
using Blueprint41.Persistence;
using Blueprint41.Query;
using q = Blueprint41.Query;

namespace Blueprint41.Query
{
    public static class QueryExtensions
    {
        #region Translator Compile

        private static readonly string[] EMPTY_COMPILED_ARGS = new string[0];

        internal static void Compile(this QueryTranslator self, FieldResult field, CompileState state)
        {
            string? functionText = field.FunctionText.Invoke(state.Translator);
            if (functionText is null)
            {
                if ((object?)field.Alias is not null)
                {
                    field.Alias.Compile(state);
                    if (!string.IsNullOrEmpty(field.FieldName))
                    {
                        state.Text.Append(".");
                        state.Text.Append(field.FieldName);
                    }
                }
                else if (!string.IsNullOrEmpty(field.FieldName))
                {
                    state.Text.Append(field.FieldName);
                }
            }
            else
            {
                string[]? compiledArgs = field.FunctionArgs?.Select(arg => state.Preview(self.GetCompile(arg), state)).ToArray();
                string compiledText = string.Format(functionText.Replace("{base}", "{{base}}"), compiledArgs ?? EMPTY_COMPILED_ARGS);

                if ((object?)field.Field is not null)
                {
                    string[] split = compiledText.Split(new string[] { "{base}" }, StringSplitOptions.None);
                    if (split.Length == 0)
                        throw new NotSupportedException("Functions have to include compilation of the base they are derived from.");

                    string baseText = state.Preview(field.Field.Compile, state);
                    state.Text.Append(string.Join(baseText, split));
                }
                else if ((object?)field.Alias is not null)
                {
                    string[] split = compiledText.Split(new string[] { "{base}" }, StringSplitOptions.None);
                    if (split.Length == 0)
                        throw new NotSupportedException("Functions have to include compilation of the base they are derived from.");

                    string baseText = state.Preview(field.Alias.Compile, state);
                    state.Text.Append(string.Join(baseText, split));
                }
                else
                {
                    state.Text.Append(compiledText);
                }
            }
        }
        internal static void Compile(this QueryTranslator self, AsResult result, CompileState state)
        {
            result.Result.Compile(state);
            state.Text.Append(" AS ");
            state.Text.Append(result.AliasName);
        }
        internal static void Compile(this QueryTranslator self, AliasResult alias, CompileState state)
        {
            alias.AliasName ??= string.Format("n{0}", state.patternSeq++);

            string? functionText = alias.FunctionText.Invoke(state.Translator);
            if (functionText is null)
            {
                state.Text.Append(alias.AliasName);
            }
            else
            {
                string[]? compiledArgs = alias.FunctionArgs?.Select(arg => state.Preview(self.GetCompile(arg), state)).ToArray();
                string compiledText = string.Format(functionText.Replace("{base}", "{{base}}"), compiledArgs ?? EMPTY_COMPILED_ARGS);

                if ((object?)alias.Alias is not null)
                {
                    string[] split = compiledText.Split(new string[] { "{base}" }, StringSplitOptions.None);
                    if (split.Length == 0)
                        throw new NotSupportedException("Functions have to include compilation of the base they are derived from.");

                    string baseText = state.Preview(alias.Alias.Compile, state);
                    state.Text.Append(string.Join(baseText, split));
                }
                else
                {
                    state.Text.Append(compiledText);
                }
            }
        }
        internal static void Compile(this QueryTranslator self, PathResult path, CompileState state)
        {
            self.Compile(path.Alias, state);
        }
        internal static void Compile(this QueryTranslator self, Literal litheral, CompileState state)
        {
            state.Text.Append(litheral.Text);
        }
        internal static void Compile(this QueryTranslator self, FunctionalId functionalId, CompileState state)
        {
            if (!self.PersistenceProvider.IsNeo4j  || self.PersistenceProvider.Major < 4)
                throw new NotSupportedException("Setting functional-id's on (batch) queries is not supported for Neo4j versions before v4.0.0");

            if (!self.HasBlueprint41FunctionalidFnNext.Value || !self.HasBlueprint41FunctionalidFnNextNumeric.Value)
                throw new NotSupportedException("Setting functional-id's on (batch) queries is not supported if the Blueprint41 plug-in is not installed or a lower version than 'blueprint41-4.0.2.jar'.");

            if (functionalId.Guid == Guid.Empty)
                state.Text.Append(self.FnApocCreateUuid);
            if (functionalId.Format == IdFormat.Hash)
                state.Text.AppendFormat(self.FnFunctionalIdNextHash, functionalId.Label);
            else
                state.Text.AppendFormat(self.FnFunctionalIdNextNumeric, functionalId.Label);
        }
        internal static void Compile(this QueryTranslator self, RelationFieldResult field, CompileState state)
        {
            if (field.Alias is not null)
            {
                field.Alias.Compile(state);
                state.Text.Append(".");
            }
            state.Text.Append(field.FieldName);
        }
        internal static void Compile(this QueryTranslator self, Parameter parameter, CompileState state)
        {
            if (!state.Parameters.Contains(parameter))
                state.Parameters.Add(parameter);

            if (parameter.HasValue && !state.Values.Contains(parameter))
                state.Values.Add(parameter);

            state.Text.Append("$");
            state.Text.Append(parameter.Name ?? $"param{state.paramSeq++}");
        }
        internal static void Compile(this QueryTranslator self, QueryCondition condition, CompileState state)
        {
            condition.Left = self.Substitute(state, condition.Left);
            condition.Right = self.Substitute(state, condition.Right);

            if (condition.Operator == Operator.Boolean)
            {
                state.Text.Append("(");
                switch (condition.Left)
                {
                    case BooleanResult boolean:
                        boolean.Compile(state);
                        break;
                    case Literal literal:
                        literal.Compile(state);
                        break;
                }
                state.Text.Append(")");
                return;
            }

            Type? leftType = self.GetOperandType(condition.Left);
            Type? rightType = self.GetOperandType(condition.Right);

            if (leftType is not null && rightType is not null)
            {
                if (leftType != rightType)
                {
                    if (condition.Operator == Operator.In)
                    {
                        if (rightType.GetInterface(nameof(IEnumerable)) is null)
                            state.Errors.Add($"The types of the fields {state.Preview(s => self.CompileOperand(s, condition.Right))} should be a collection.");

                        rightType = self.GetEnumeratedType(rightType);
                    }
                    if (self.GetConversionGroup(leftType, state.TypeMappings) != self.GetConversionGroup(rightType, state.TypeMappings))
                        state.Errors.Add($"The types of the fields {state.Preview(s => self.CompileOperand(s, condition.Left))} and {state.Preview(s => self.CompileOperand(s, condition.Right))} are not compatible.");
                }
            }

            state.Text.Append("(");
            if (condition.Operator == Operator.Not || condition.Operator == Operator.NotPattern)
                state.Text.Append("NOT(");
            else if (condition.Operator != Operator.Pattern)
                self.CompileOperand(state, condition.Left);

            if (condition.Right is Parameter rightParameter)
            {
                if (rightParameter.IsConstant && rightParameter.Value is null)
                {
                    condition.Operator.Compile(state, true);
                    self.CompileOperand(state, null);
                }
                else
                {
                    condition.Operator.Compile(state, false);
                    self.CompileOperand(state, condition.Right);
                }
            }
            else
            {
                condition.Operator.Compile(state, condition.Right is null);
                self.CompileOperand(state, condition.Right);
            }

            if (condition.Operator == Operator.Not || condition.Operator == Operator.NotPattern)
                state.Text.Append(")");

            state.Text.Append(")");
        }
        internal static void Compile(this QueryTranslator self, Node node, CompileState state, bool suppressAliases)
        {
            //find the root
            Node root = node;
            while (root.FromRelationship is not null)
                root = root.FromRelationship.FromNode;

            Node? current = root;
            do
            {
                GetDirection(current, state.Text);
                if (current.NodeAlias is not null)
                {
                    if (current.NodeAlias.AliasName is null)
                        current.NodeAlias.AliasName = string.Format("n{0}", state.patternSeq++);

                    if (current.IsReference || current.Neo4jLabel is null)
                    {
                        state.Text.Append("(");
                        state.Text.Append(current.NodeAlias.AliasName);
                        state.Text.Append(")");
                    }
                    else
                    {
                        state.Text.Append("(");
                        if (!suppressAliases)
                            state.Text.Append(current.NodeAlias.AliasName);
                        state.Text.Append(":");
                        state.Text.Append(current.Neo4jLabel);
                        InlineConditions(current, state);
                        state.Text.Append(")");
                    }
                }
                else
                {
                    if (current.Neo4jLabel is null)
                    {
                        state.Text.Append("()");
                    }
                    else
                    {
                        state.Text.Append("(");
                        state.Text.Append(":");
                        state.Text.Append(current.Neo4jLabel);
                        InlineConditions(current, state);
                        state.Text.Append(")");

                    }
                }

                if (current.ToRelationship is not null)
                {
                    current.ToRelationship.Compile(state);
                    current = current.ToRelationship.ToNode;
                    if (current is null)
                        break;
                }
                else
                    break;

            } while (true);

            void GetDirection(Node node, StringBuilder sb)
            {
                if (node.FromRelationship is null)
                    return;

                switch (node.Direction)
                {
                    case DirectionEnum.In:
                        sb.Append("-");
                        break;
                    case DirectionEnum.Out:
                        sb.Append("->");
                        break;
                    case DirectionEnum.None:
                        sb.Append("-");
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            void InlineConditions(Node current, CompileState state)
            {
                if (current.InlineConditions is not null && current.InlineConditions.Length != 0)
                {
                    state.Text.Append(" { ");

                    bool isFirst = true;
                    foreach (var condition in current.InlineConditions)
                    {
                        if (isFirst)
                            isFirst = false;
                        else
                            state.Text.Append(", ");

                        if (condition.Left is not FieldResult field)
                            continue;

                        object? value = self.Substitute(state, condition.Right);

                        state.Text.Append(field.FieldName);
                        state.Text.Append(": ");
                        self.CompileOperand(state, value);
                    }

                    state.Text.Append(" }");
                }
            }
        }
        internal static void Compile(this QueryTranslator self, PathNode path, CompileState state)
        {
            self.Compile(path.NodeAlias!, state);
            state.Text.Append(" = ");
            path.Node.Compile(state);
        }
        internal static void Compile(this QueryTranslator self, Assignment assignment, CompileState state, bool add)
        {
            assignment.Field.Compile(state);

            if (add)
                state.Text.Append(" += ");
            else
                state.Text.Append(" = ");

            self.CompileOperand(state, assignment.Value.GetValue());
        }
        internal static void Compile(this QueryTranslator self, q.Query query, CompileState state)
        {
            switch (query.Type)
            {
                case PartType.Search:
                    self.SearchTranslation(query, state);
                    break;
                case PartType.Match:
                    MatchTranslation(query, state);
                    break;
                case PartType.OptionalMatch:
                    state.Text.Append("OPTIONAL MATCH ");
                    query.ForEach(query.Patterns, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.UsingScan:
                    state.Text.Append("USING SCAN ");
                    query.ForEach(query.Aliases, state.Text, "\r\nUSING SCAN ", item =>
                    {
                        if (item.Node is null)
                            return;

                        item.Compile(state);
                        state.Text.Append(":" + item.Node.Neo4jLabel);
                    });
                    break;
                case PartType.UsingIndex:
                    state.Text.Append("USING INDEX ");
                    query.ForEach(query.Fields, state.Text, "\r\nUSING INDEX ", item =>
                    {
                        if (item.Alias is null || item.Alias.Node is null)
                            return;

                        state.Text.Append(string.Format("{0}:{1}({2})", item.Alias.AliasName, item.Alias.Node.Neo4jLabel, item.FieldName));
                    });
                    break;
                case PartType.OrderBy:
                    state.Text.Append("ORDER BY ");
                    query.ForEach(query.Fields, state.Text, ", ", delegate (FieldResult item)
                    {
                        item?.Compile(state);
                        if (!query.Ascending)
                            state.Text.Append(" DESC");
                    });
                    break;
                case PartType.Return:
                    if (query.Distinct)
                        state.Text.Append("RETURN DISTINCT ");
                    else
                        state.Text.Append("RETURN ");
                    query.ForEach(query.AsResults, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.Create:
                    state.Text.Append("CREATE ");
                    query.ForEach(query.Patterns, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.Merge:
                    state.Text.Append("MERGE ");
                    query.ForEach(query.Patterns, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.OnCreate:
                    state.Text.Append("ON CREATE SET ");
                    query.ForEach(query.Assignments, state.Text, ", ", item => item?.Compile(state, query.SetAdd));
                    break;
                case PartType.OnMatch:
                    state.Text.Append("ON MATCH SET ");
                    query.ForEach(query.Assignments, state.Text, ", ", item => item?.Compile(state, query.SetAdd));
                    break;
                case PartType.Set:
                    state.Text.Append("SET ");
                    query.ForEach(query.Assignments, state.Text, ", ", item => item?.Compile(state, query.SetAdd));
                    break;
                case PartType.Delete:
                    if (query.Detach)
                        state.Text.Append("DETACH DELETE ");
                    else
                        state.Text.Append("DELETE ");
                    query.ForEach(query.Results, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.Where:
                    state.Text.Append("WHERE ");
                    query.ForEach(query.Conditions, state.Text, " AND ", item => item?.Compile(state));
                    break;
                case PartType.Or:
                    state.Text.Append("OR ");
                    query.ForEach(query.Conditions, state.Text, " AND ", item => item?.Compile(state));
                    break;
                case PartType.Unwind:
                    state.Text.Append("UNWIND ");
                    query.ForEach(query.Results, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.With:
                    if (query.Distinct)
                        state.Text.Append("WITH DISTINCT ");
                    else
                        state.Text.Append("WITH ");
                    query.ForEach(query.Results, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.Skip:
                    state.Text.Append("SKIP ");
                    query.SkipValue.Compile(state);
                    break;
                case PartType.Limit:
                    state.Text.Append("LIMIT ");
                    query.LimitValue.Compile(state);
                    break;
                case PartType.UnionMatch:
                    UnionTranslation(query, state);
                    MatchTranslation(query, state);
                    break;
                case PartType.UnionSearch:
                    UnionTranslation(query, state);
                    self.SearchTranslation(query, state);
                    break;
                case PartType.CallSubquery:
                    state.Text.Append("CALL {");
                    state.Text.AppendLine();
                    query.SubQueryPart?.CompileParts(state);
                    state.Text.AppendLine();
                    state.Text.Append("}");
                    break;
                case PartType.None:
                case PartType.Compiled:
                    // Ignore
                    break;
                default:
                    throw new NotImplementedException($"Compilation for the {query.Type} clause is not supported yet.");
            }
            void MatchTranslation(q.Query query, CompileState state)
            {
                if (query.Patterns is null || query.Patterns.Length == 0)
                    return;

                state.Text.Append("MATCH ");
                query.ForEach(query.Patterns, state.Text, ", ", item => item?.Compile(state));
            }
            void UnionTranslation(q.Query query, CompileState state)
            {
                if (query.UnionWithDuplicates)
                    state.Text.Append("UNION ALL ");
                else
                    state.Text.Append("UNION ");
            }
        }

        internal static void OrderQueryParts(this QueryTranslator self, LinkedList<q.Query> parts)
        {
        }
        internal static void SearchTranslation(this QueryTranslator self, q.Query query, CompileState state)
        {
            string search = $"replace(trim(replace(replace(replace({state.Preview(query.SearchWords!.Compile, state)}, 'AND', '\"AND\"'), 'OR', '\"OR\"'), '  ', ' ')), ' ', ' {query.SearchOperator!.ToString()!.ToUpperInvariant()} ')";
            search = string.Format("replace({0}, '{1}', '{2}')", search, "]", @"\\]");
            search = string.Format("replace({0}, '{1}', '{2}')", search, "[", @"\\[");
            search = string.Format("replace({0}, '{1}', '{2}')", search, "-", $" {query.SearchOperator!.ToString()!.ToUpperInvariant()} ");
            Node node = query.Patterns!.First();
            AliasResult alias = node.NodeAlias!;
            AliasResult? weight = query.Aliases?.FirstOrDefault();
            FieldResult[] fields = query.Fields!;

            List<string> queries = new List<string>();
            foreach (var property in fields)
                queries.Add(string.Format("({0}:' + {1} + ')", property.FieldName, search));

            state.Text.Append(string.Format(self.FtiSearch, string.Join(" OR ", queries), state.Preview(alias.Compile, state)));
            if ((object?)weight is not null)
            {
                state.Text.Append(string.Format(self.FtiWeight, state.Preview(weight.Compile, state)));
            }
            state.Text.Append(" WHERE (");
            self.Compile(alias, state);
            state.Text.Append(":");
            state.Text.Append(node.Neo4jLabel);
            state.Text.Append(")");
        }

        #endregion

        #region Translator Helper Methods

        private static object? Substitute(this QueryTranslator self, CompileState state, object? operand)
        {
            if (operand is null)
                return null;

            switch (operand)
            {
                case Result:
                case QueryCondition:
                case Parameter:
                case Node:
                case FunctionalId:
                    return operand;
                default:
                    Type type = operand.GetType();
                    state.TypeMappings.TryGetValue(type, out TypeMapping? mapping);
                    if (mapping is null)
                        return operand;

                    return Parameter.Constant(operand, type);
            }
        }
        private static Type? GetOperandType(this QueryTranslator self, object? operand)
        {
            if (operand is null)
                return null;

            return operand switch
            {
                Result result => result.GetResultType(),
                QueryCondition => null,
                Parameter parameter => parameter.Type,
                Node => null,
                FunctionalId => typeof(string),
                _ => throw new NotSupportedException("The expression is not supported for compilation."),
            };
        }

        private static void CompileOperand(this QueryTranslator self, CompileState state, object? operand)
        {
            if (operand is null)
            {
                state.Text.Append("NULL");
                return;
            }

            switch (operand)
            {
                case Result result:
                    result.Compile(state);
                    break;
                case QueryCondition queryCondition:
                    queryCondition.Compile(state);
                    break;
                case Parameter parameter:
                    parameter.Compile(state);
                    break;
                case Node node:
                    node.Compile(state);
                    break;
                case FunctionalId functionalId:
                    self.Compile(functionalId, state);
                    break;
                default:
                    Type type = operand.GetType();
                    state.Errors.Add($"The type {type!.Name} is not supported for compilation.");
                    state.Text.Append(operand.ToString());
                    break;
            }
        }
        private static Action<CompileState> GetCompile(this QueryTranslator self, object? arg)
        {
            if (arg is null)
            {
                return delegate (CompileState state)
                {
                    state.Text.Append("NULL");
                };
            }
            return arg switch
            {
                Literal literal => literal.Compile,
                Parameter parameter => parameter.Compile,
                FieldResult fieldResult => fieldResult.Compile,
                QueryCondition queryCondition => queryCondition.Compile,
                AliasResult aliasResult => aliasResult.Compile,
                FunctionalId functionalId => delegate (CompileState state) { self.Compile(functionalId, state); }
                ,
                q.Query query => delegate (CompileState state) { query.SubQueryPart?.CompileParts(state); }
                ,
                _ => throw new NotSupportedException($"Function arguments of type '{arg.GetType().Name}' are not supported.")
            };
        }

        #endregion

        #region Entity

        internal static AliasResult? FindCommonBaseClass(List<AliasResult> aliases)
        {
            if (aliases.Count == 0)
                return null;

            if (aliases.Count == 1)
                return aliases.First();

            AliasResultInfo? e;
            List<AliasResultInfo>? shared = null;
            foreach (AliasResultInfo info in aliases.Select(item => new AliasResultInfo(item)))
            {
                List<AliasResultInfo> inheritChain = new List<AliasResultInfo>();
                e = info;
                while (e is not null)
                {
                    inheritChain.Add(e);
                    e = e.Inherits;
                }

                if (shared is null)
                {
                    shared = inheritChain;
                }
                else
                {
                    if (shared.Last().Distance > inheritChain.Last().Distance)
                        (shared, inheritChain) = (inheritChain, shared);

                    HashSet<string> inheritedEntities = new HashSet<string>(inheritChain.Select(item => item.Entity.Name));
                    for (int index = shared.Count - 1; index >= 0; index--)
                    {
                        if (!inheritedEntities.Contains(shared[index].Entity.Name))
                        {
                            shared.RemoveAt(index);
                        }
                    }
                }

                if (shared.Count == 0)
                    break;
            }

            if (shared is null || shared.Count == 0)
                return null;

            return shared.First().AliasResult;
        }
        private class AliasResultInfo
        {
            public AliasResultInfo(AliasResult aliasResult)
            {
                AliasResult = aliasResult;
                Entity = aliasResult.Entity!;
                Distance = 0;
                Inherits = (aliasResult.Entity?.Inherits is null) ? null : new AliasResultInfo(this);
            }
            private AliasResultInfo(AliasResultInfo info)
            {
                AliasResult = info.AliasResult;
                Entity = info.Entity.Inherits!;
                Distance = info.Distance + 1;
            }

            public AliasResult AliasResult { get; set; }
            public Entity Entity { get; set; }
            public int Distance { get; set; }

            public AliasResultInfo? Inherits { get; set; }
        }

        #endregion
    }
}
