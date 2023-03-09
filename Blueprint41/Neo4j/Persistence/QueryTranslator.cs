﻿using System;
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
        protected QueryTranslator(PersistenceProvider persistenceProvider)
        {
            PersistenceProvider = persistenceProvider;
        }

        #region Compile Query Parts

        internal virtual void Compile(FieldResult field, CompileState state)
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
                string[] compiledArgs = field.FunctionArgs.Select(arg => state.Preview(GetCompile(arg), state)).ToArray();
                string compiledText = string.Format(functionText.Replace("{base}", "{{base}}"), compiledArgs);

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
        internal virtual void Compile(AsResult result, CompileState state)
        {
            result.Result.Compile(state);
            state.Text.Append(" AS ");
            state.Text.Append(result.AliasName);
        }
        internal virtual void Compile(AliasResult alias, CompileState state)
        {
            if (alias.AliasName is null)
                alias.AliasName = string.Format("n{0}", state.patternSeq++);

            string? functionText = alias.FunctionText.Invoke(state.Translator);
            if (functionText is null)
            {
                state.Text.Append(alias.AliasName);
            }
            else
            {
                string[] compiledArgs = alias.FunctionArgs.Select(arg => state.Preview(GetCompile(arg), state)).ToArray();
                string compiledText = string.Format(functionText.Replace("{base}", "{{base}}"), compiledArgs);

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
        internal virtual void Compile(PathResult path, CompileState state)
        {
            Compile(path.Alias, state);
        }
        internal virtual void Compile(Literal litheral, CompileState state)
        {
            state.Text.Append(litheral.Text);
        }
        internal virtual void Compile(FunctionalId functionalId, CompileState state)
        {
            if (PersistenceProvider is Neo4jPersistenceProvider neo4j && neo4j.Major < 4)
                throw new NotSupportedException("Setting functional-id's on (batch) queries is not supported for Neo4j versions before v4.0.0");

            if (!HasBlueprint41FunctionalidFnNext.Value || !HasBlueprint41FunctionalidFnNextNumeric.Value)
                throw new NotSupportedException("Setting functional-id's on (batch) queries is not supported if the Blueprint41 plug-in is not installed or a lower version than 'blueprint41-4.0.2.jar'.");

            if (functionalId.Guid == Guid.Empty)
                state.Text.Append(Fn_ApocCreate_Uuid);
            if (functionalId.Format == IdFormat.Hash)
                state.Text.AppendFormat(FnFunctionalIdNextHash, functionalId.Label);
            else
                state.Text.AppendFormat(FnFunctionalIdNextNumeric, functionalId.Label);
        }
        internal virtual void Compile(RelationFieldResult field, CompileState state)
        {
            if (!(field.Alias is null))
            {
                field.Alias.Compile(state);
                state.Text.Append(".");
            }
            state.Text.Append(field.FieldName);
        }
        internal virtual void Compile(Parameter parameter, CompileState state)
        {
            if (!state.Parameters.Contains(parameter))
                state.Parameters.Add(parameter);

            if (parameter.HasValue && !state.Values.Contains(parameter))
                state.Values.Add(parameter);

            if (parameter.Name == Parameter.CONSTANT_NAME)
            {
                parameter.Name = $"param{state.paramSeq}";
                state.paramSeq++;
            }
            state.Text.Append("$");
            state.Text.Append(parameter.Name);
        }
        internal virtual void Compile(QueryCondition condition, CompileState state)
        {
            condition.Left = Substitute(state, condition.Left);
            condition.Right = Substitute(state, condition.Right);

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

            Type? leftType = GetOperandType(condition.Left);
            Type? rightType = GetOperandType(condition.Right);

            if (leftType is not null && rightType is not null)
            {
                if (leftType != rightType)
                {
                    if (condition.Operator == Operator.In)
                    {
                        if (rightType.GetInterface(nameof(IEnumerable)) is null)
                            state.Errors.Add($"The types of the fields {state.Preview(s => CompileOperand(s, condition.Right))} should be a collection.");

                        rightType = GetEnumeratedType(rightType);
                    }
                    if (GetConversionGroup(leftType, state.TypeMappings) != GetConversionGroup(rightType, state.TypeMappings))
                        state.Errors.Add($"The types of the fields {state.Preview(s => CompileOperand(s, condition.Left))} and {state.Preview(s => CompileOperand(s, condition.Right))} are not compatible.");
                }
            }

            state.Text.Append("(");
            if (condition.Operator == Operator.Not || condition.Operator == Operator.NotPattern)
                state.Text.Append("NOT(");
            else if (condition.Operator != Operator.Pattern)
                CompileOperand(state, condition.Left);

            if (condition.Right is Parameter)
            {
                Parameter rightParameter = (Parameter)condition.Right;
                if (rightParameter.IsConstant && rightParameter.Value is null)
                {
                    condition.Operator.Compile(state, true);
                    CompileOperand(state, null);
                }
                else
                {
                    condition.Operator.Compile(state, false);
                    CompileOperand(state, condition.Right);
                }
            }
            else
            {
                condition.Operator.Compile(state, condition.Right is null);
                CompileOperand(state, condition.Right);
            }

            if (condition.Operator == Operator.Not || condition.Operator == Operator.NotPattern)
                state.Text.Append(")");

            state.Text.Append(")");
        }
        internal virtual void Compile(Node node, CompileState state, bool suppressAliases)
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

                        FieldResult? field = condition.Left as FieldResult;
                        if (field is null)
                            continue;

                        object? value = Substitute(state, condition.Right);

                        state.Text.Append(field.FieldName);
                        state.Text.Append(": ");
                        CompileOperand(state, value);
                    }

                    state.Text.Append(" }");
                }
            }
        }
        internal virtual void Compile(PathNode path, CompileState state)
        {
            Compile(path.NodeAlias!, state);
            state.Text.Append(" = ");
            path.Node.Compile(state);
        }
        internal virtual void Compile(Assignment assignment, CompileState state, bool add)
        {
            assignment.Field.Compile(state);

            if (add)
                state.Text.Append(" += ");
            else
                state.Text.Append(" = ");

            CompileOperand(state, assignment.Value.GetValue());
        }
        internal virtual void Compile(q.Query query, CompileState state)
        {
            switch (query.Type)
            {
                case PartType.Search:
                    SearchTranslation(query, state);
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
                    SearchTranslation(query, state);
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
                    throw new NotImplementedException($"Compilation for the {query.Type.ToString()} clause is not supported yet.");
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

        internal protected virtual void SearchTranslation(q.Query query, CompileState state)
        {
            string search = $"replace(trim(replace(replace(replace({state.Preview(query.SearchWords!.Compile, state)}, 'AND', '\"AND\"'), 'OR', '\"OR\"'), '  ', ' ')), ' ', ' {query.SearchOperator!.ToString().ToUpperInvariant()} ')";
            search = string.Format("replace({0}, '{1}', '{2}')", search, "]", @"\\]");
            search = string.Format("replace({0}, '{1}', '{2}')", search, "[", @"\\[");
            search = string.Format("replace({0}, '{1}', '{2}')", search, "-", $" {query.SearchOperator!.ToString().ToUpperInvariant()} ");
            Node node = query.Patterns.First();
            AliasResult alias = node.NodeAlias!;
            AliasResult? weight = query.Aliases?.FirstOrDefault();
            FieldResult[] fields = query.Fields!;

            List<string> queries = new List<string>();
            foreach (var property in fields)
                queries.Add(string.Format("({0}:' + {1} + ')", property.FieldName, search));

            state.Text.Append(string.Format(FtiSearch, string.Join(" OR ", queries), state.Preview(alias.Compile, state)));
            if ((object?)weight is not null)
            {
                state.Text.Append(string.Format(FtiWeight, state.Preview(weight.Compile, state)));
            }
            state.Text.Append(" WHERE (");
            Compile(alias, state);
            state.Text.Append(":");
            state.Text.Append(node.Neo4jLabel);
            state.Text.Append(")");
        }

        #endregion

        #region Compile Functions

        public virtual string FnParam1                    => "{0}";
        public virtual string FnAsIs                      => "{base}";
        public virtual string FnToBoolean                 => "toBoolean({base})";
        public virtual string FnToInteger                 => "toInteger({base})";
        public virtual string FnToFloat                   => "toFloat({base})";
        public virtual string FnToString                  => "toString({base})";
        public virtual string FnExists                    => "exists({base})";
        public virtual string FnNot                       => "NOT ({base})";
        public virtual string FnCollect                   => "collect({base})";
        public virtual string FnCollectDistinct           => "collect(distinct {base})";
        public virtual string FnCoalesce                  => "coalesce({base}, {0})";
        public virtual string FnToUpper                   => "upper({base})";
        public virtual string FnToLower                   => "lower({base})";
        public virtual string FnReverse                   => "reverse({base})";
        public virtual string FnTrim                      => "trim({base})";
        public virtual string FnLeftTrim                  => "ltrim({base})";
        public virtual string FnRightTrim                 => "rtrim({base})";
        public virtual string FnCount                     => "count({base})";
        public virtual string FnCountDistinct             => "count(DISTINCT {base})";
        public virtual string FnSize                      => "size({base})";
        public virtual string FnListSize                  => "size({base})";
        public virtual string FnLength                    => "length({base})";
        public virtual string FnSplit                     => "split({base}, {0})";
        public virtual string FnLeft                      => "left({base}, {0})";
        public virtual string FnRight                     => "right({base}, {0})";
        public virtual string FnSubStringWOutLen          => "substring({base}, {0})";
        public virtual string FnSubString                 => "substring({base}, {0}, {1})";
        public virtual string FnReplace                   => "replace({base}, {0}, {1})";
        public virtual string FnMin                       => "min({base})";
        public virtual string FnMax                       => "max({base})";
        public virtual string FnProperties                => "properties({0})";
        public virtual string FnLabels                    => "LABELS({0})";
        public virtual string FnSign                      => "sign({base})";
        public virtual string FnAbs                       => "abs({base})";
        public virtual string FnSum                       => "sum({base})";
        public virtual string FnAvg                       => "avg({base})";
        public virtual string FnPercentileDisc            => "percentileDisc({base}, {0})";
        public virtual string FnPercentileCont            => "percentileCont({base}, {0})";
        public virtual string FnStDev                     => "stdev({base})";
        public virtual string FnStDevP                    => "stdevp({base})";
        public virtual string FnRound                     => "round({base})";
        public virtual string FnSqrt                      => "sqrt({base})";
        public virtual string FnSin                       => "sin({base})";
        public virtual string FnASin                      => "asin({base})";
        public virtual string FnCos                       => "cos({base})";
        public virtual string FnACos                      => "acos({base})";
        public virtual string FnTan                       => "tan({base})";
        public virtual string FnATan                      => "atan({base})";
        public virtual string FnATan2                     => "atan2({base})";
        public virtual string FnCot                       => "cot({base})";
        public virtual string FnHaversin                  => "haversin({base})";
        public virtual string FnDegrees                   => "degrees({base})";
        public virtual string FnRadians                   => "radians({base})";
        public virtual string FnLog10                     => "log10({base})";
        public virtual string FnLog                       => "log({base})";
        public virtual string FnExp                       => "exp({base})";
        public virtual string FnPi                        => "pi()";
        public virtual string FnRand                      => "rand()";
        public virtual string FnRange                     => "range({0}, {1}, {2})";
        public virtual string FnListGetItem               => "{base}[{0}]";
        public virtual string FnListHead                  => "HEAD({base})";
        public virtual string FnListLast                  => "LAST({base})";
        public virtual string FnFunctionalIdNextHash      => "blueprint41.functionalid.fnNext('{0}')";
        public virtual string FnFunctionalIdNextNumeric   => "blueprint41.functionalid.fnNextNumeric('{0}')";
        public virtual string CallFunctionalIdNextHash    => "CALL blueprint41.functionalid.next('{0}') YIELD value as key";
        public virtual string CallFunctionalIdNextNumeric => "CALL blueprint41.functionalid.nextNumeric('{0}') YIELD value as key";
        public virtual string CallApocCreateUuid          => "WITH apoc.create.uuid() as key";
        public virtual string FnListAll                   => "all(item IN {base} WHERE {0})";
        public virtual string FnListAny                   => "any(item IN {base} WHERE {0})";
        public virtual string FnListNone                  => "none(item IN {base} WHERE {0})";
        public virtual string FnListSingle                => "single(item IN {base} WHERE {0})";
        public virtual string FnListExtract               => "extract(item in {base} | {0})";
        public virtual string FnListReduce                => "reduce(value = {0}, item in {base} | {1})";
        public virtual string FnListSelect                => "[item in {base} | {0}]";
        public virtual string FnListSelectWhere           => "[item in {base} WHERE {0}]";
        public virtual string FnListSelectValueWhere      => "[item in {base} WHERE {0} | {1}]";
        public virtual string FnGetField                  => "{0}[{1}]";
        public virtual string FnGetFieldWithCoalesce      => "{0}[COALESCE({1}, '')]";
        public virtual string FnConvertToBoolean          => "CASE WHEN {0} IS NULL THEN NULL WHEN {0} THEN 1 ELSE 0 END";
        public virtual string FnConvertMinOrMaxToNull     => "CASE WHEN {base} = {0} THEN NULL ELSE {base} END";
        public virtual string FnConvertMinAndMaxToNull    => "CASE WHEN {base} = {0} OR {base} = {1} THEN NULL ELSE {base} END";
        public virtual string FnCaseWhen                  => "CASE WHEN {0} THEN {1} ELSE {2} END";
        public virtual string FnAdd                       => "({base} + {0})";
        public virtual string FnSubtract                  => "({base} - {0})";
        public virtual string FnMultiply                  => "({base} * {0})";
        public virtual string FnDivide                    => "({base} / {0})";
        public virtual string FnModulo                    => "({base} % {0})";
        public virtual string FnPower                     => "({base} ^ {0})";
        public virtual string FnCaseWhenMultiple(int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CASE ");
            int argNum = 0;
            for (int index = 0; index < count; index++)
            {
                sb.Append("WHEN {");
                sb.Append(argNum);
                sb.Append("} ");
                argNum++;

                sb.Append("THEN {");
                sb.Append(argNum);
                sb.Append("} ");
                argNum++;
            }
            sb.Append("ELSE {");
            sb.Append(argNum);
            sb.Append("} END");

            return sb.ToString();
        }
        public virtual string FnConcatMultiple(int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("({base}");
            for (int index = 0; index < count; index++)
            {
                sb.Append(" + {");
                sb.Append(index);
                sb.Append("}");
            }
            sb.Append(")");
            return sb.ToString();
        }

        #endregion

        #region Full Text Indexes

        public virtual string FtiSearch    => "CALL apoc.index.search('fts', '{0}') YIELD node AS {1}";
        public virtual string FtiWeight    => ", weight AS {0}";
        public virtual string FtiCreate    => "CALL apoc.index.addAllNodesExtended('fts', {{ {0} }}, {{ autoUpdate:true }})";
        public virtual string FtiEntity    => "{0}:[{1}]";
        public virtual string FtiProperty  => "'{0}'";
        public virtual string FtiSeparator => ", ";
        public virtual string FtiRemove    => "CALL apoc.index.remove('fts')";
        public virtual string FtiList      => "CALL apoc.index.list() YIELD type, name, config";
        #endregion

        #region Compile Operators

        public virtual string OpAnd                => " AND ";
        public virtual string OpOr                 => " OR ";
        public virtual string OpIs                 => " IS ";
        public virtual string OpIsNot              => " IS NOT ";
        public virtual string OpEqual              => " = ";
        public virtual string OpNotEqual           => " <> ";
        public virtual string OpLessThan           => " < ";
        public virtual string OpLessThanOrEqual    => " <= ";
        public virtual string OpGreaterThan        => " > ";
        public virtual string OpGreaterThanOrEqual => " >= ";
        public virtual string OpStartsWith         => " STARTS WITH ";
        public virtual string OpEndsWith           => " ENDS WITH ";
        public virtual string OpContains           => " CONTAINS ";
        public virtual string OpMatch              => " =~ ";
        public virtual string OpHasLabel           => ":";
        public virtual string OpIn                 => " IN ";

        #endregion

        #region PersistenceProvider

        internal PersistenceProvider PersistenceProvider { get; private set; }
        internal abstract NodePersistenceProvider GetNodePersistenceProvider();
        internal abstract RelationshipPersistenceProvider GetRelationshipPersistenceProvider();
        internal abstract SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel);
        internal abstract RefactorTemplates GetTemplates();

        #endregion

        #region Upgrade Script Parser

        internal virtual bool HasFullTextSearchIndexes()
        {
            using (Transaction.Begin(true))
            {
                var result = Transaction.RunningTransaction.Run(FtiList);
                return result.Count() > 0;
            }
        }
        internal virtual void ApplyFullTextSearchIndexes(IEnumerable<Entity> entities)
        {
            try
            {
                using (Transaction.Begin(true))
                {
                    Transaction.RunningTransaction.Run(FtiRemove);
                    Transaction.Commit();
                }
            }
            catch { }

            using (Transaction.Begin(true))
            {
                string indexes = string.Join(
                        FtiSeparator,
                        entities.Where(entity => entity.FullTextIndexProperties.Count > 0).Select(entity =>
                            string.Format(
                                FtiEntity,
                                entity.Label.Name,
                                string.Join(
                                    FtiSeparator,
                                    entity.FullTextIndexProperties.Select(item =>
                                        string.Format(
                                            FtiProperty,
                                            item.Name
                                        )
                                    )
                                )
                            )
                        )
                    );
                string query = string.Format(FtiCreate, indexes);

                Transaction.RunningTransaction.Run(query);
                Transaction.Commit();
            }
        }
        internal virtual bool HasScript(DatastoreModel.UpgradeScript script)
        {
            string query = "MATCH (version:RefactorVersion) RETURN version;";
            var result = Transaction.RunningTransaction.Run(query);

            RawRecord record = result.FirstOrDefault();
            if (record is null)
                return false;

            RawNode node = record["version"].As<RawNode>();
            (long major, long minor, long patch) databaseVersion = ((long)node.Properties["Major"]!, (long)node.Properties["Minor"]!, (long)node.Properties["Patch"]!);

            if (databaseVersion.major < script.Major)
                return false;

            if (databaseVersion.major > script.Major)
                return true;

            if (databaseVersion.minor < script.Minor)
                return false;

            if (databaseVersion.minor > script.Minor)
                return true;

            if (databaseVersion.patch < script.Patch)
                return false;

            return true;
        }
        internal virtual void CommitScript(DatastoreModel.UpgradeScript script)
        {
            // write version nr
            string create = "MERGE (n:RefactorVersion) ON CREATE SET n = $node ON MATCH SET n = $node";

            Dictionary<string, object> node = new Dictionary<string, object>();
            node.Add("Major", script.Major);
            node.Add("Minor", script.Minor);
            node.Add("Patch", script.Patch);
            node.Add("LastRun", Conversion<DateTime, long>.Convert(DateTime.UtcNow));

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("node", node);

            Transaction.RunningTransaction.Run(create, parameters);
            Transaction.Commit();
        }
        internal virtual bool ShouldRefreshFunctionalIds()
        {
            string query = "MATCH (version:RefactorVersion) RETURN version.LastRun as LastRun";
            var result = Transaction.RunningTransaction.Run(query);

            RawRecord record = result.FirstOrDefault();
            if (record is null)
                return true;

            DateTime? lastRun = Conversion<long?, DateTime?>.Convert(record["LastRun"].As<long?>());
            if (lastRun is null)
                return true;

            if (DateTime.UtcNow.Subtract(lastRun.Value).TotalHours >= 12)
                return true;

            return false;
        }
        internal virtual void SetLastRun()
        {
            // write version nr
            string query = "MATCH (n:RefactorVersion) SET n.LastRun = $LastRun";

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            parameters.Add("LastRun", Conversion<DateTime, long>.Convert(DateTime.UtcNow));

            Transaction.RunningTransaction.Run(query, parameters);
        }

        #endregion

        #region Helper Methods

        protected object? Substitute(CompileState state, object? operand)
        {
            if (operand is null)
                return null;

            Type type = operand.GetType();

            if (type.IsSubclassOfOrSelf(typeof(Result)))
            {
                return operand;
            }
            else if (type.IsSubclassOfOrSelf(typeof(QueryCondition)))
            {
                return operand;
            }
            else if (type.IsSubclassOfOrSelf(typeof(Parameter)))
            {
                return operand;
            }
            else if (type.IsSubclassOfOrSelf(typeof(Node)))
            {
                return operand;
            }
            else if (type.IsSubclassOfOrSelf(typeof(FunctionalId)))
            {
                return operand;
            }
            else
            {
                state.TypeMappings.TryGetValue(type, out TypeMapping mapping);
                if (mapping is null)
                    return operand;

                return Parameter.Constant(operand, type);
            }
        }
        protected Type? GetOperandType(object? operand)
        {
            if (operand is null)
                return null;

            Type type = operand.GetType();

            if (type.IsSubclassOfOrSelf(typeof(Result)))
            {
                return ((Result)operand).GetResultType();
            }
            else if (type.IsSubclassOfOrSelf(typeof(QueryCondition)))
            {
                return null;
            }
            else if (type.IsSubclassOfOrSelf(typeof(Parameter)))
            {
                return ((Parameter)operand).Type;
            }
            else if (type.IsSubclassOfOrSelf(typeof(Node)))
            {
                return null;
            }
            else if (type.IsSubclassOfOrSelf(typeof(FunctionalId)))
            {
                return typeof(string);
            }
            else
            {
                throw new NotSupportedException("The expression is not supported for compilation.");
            }
        }
        protected string GetConversionGroup(Type type, IReadOnlyDictionary<Type, TypeMapping> mappings)
        {
            mappings.TryGetValue(type, out TypeMapping mapping);
            if (mapping is null)
                throw new InvalidOperationException($"An unexpected technical mapping failure while trying to find the conversion for type {type.Name}. Please contact the developer.");

            return mapping.ComparisonGroup;
        }
        protected Type GetEnumeratedType(Type? type)
        {
            if (type is null)
                throw new InvalidOperationException("The type cannot be null");

            Type? result = type.GetElementType();
            if (result is null && typeof(IEnumerable).IsAssignableFrom(type))
                result = type.GenericTypeArguments.FirstOrDefault();

            return result ?? type;
        }

        protected void CompileOperand(CompileState state, object? operand)
        {
            if (operand is null)
            {
                state.Text.Append("NULL");
                return;
            }
            else
            {
                Type type = operand.GetType();

                if (type.IsSubclassOfOrSelf(typeof(Result)))
                {
                    ((Result)operand).Compile(state);
                }
                else if (type.IsSubclassOfOrSelf(typeof(QueryCondition)))
                {
                    ((QueryCondition)operand).Compile(state);
                }
                else if (type.IsSubclassOfOrSelf(typeof(Parameter)))
                {
                    ((Parameter)operand).Compile(state);
                }
                else if (type.IsSubclassOfOrSelf(typeof(Node)))
                {
                    ((Node)operand).Compile(state, false);
                }
                else if (type.IsSubclassOfOrSelf(typeof(FunctionalId)))
                {
                    Compile((FunctionalId)operand, state);
                }
                else
                {
                    state.Errors.Add($"The type {type!.Name} is not supported for compilation.");
                    state.Text.Append(operand.ToString());
                }
            }
        }
        protected Action<CompileState> GetCompile(object? arg)
        {
            if (arg is null)
            {
                return delegate (CompileState state)
                {
                    state.Text.Append("NULL");
                };
            }
            else if (arg is Literal)
            {
                Literal param = (Literal)arg;
                return param.Compile;
            }
            else if (arg is Parameter)
            {
                Parameter param = (Parameter)arg;
                return param.Compile;
            }
            else if (arg.GetType().IsSubclassOfOrSelf(typeof(FieldResult)))
            {
                FieldResult field = (FieldResult)arg;
                return field.Compile;
            }
            else if (arg is QueryCondition)
            {
                QueryCondition param = (QueryCondition)arg;
                return param.Compile;
            }
            else if (arg is AliasResult)
            {
                AliasResult param = (AliasResult)arg;
                return param.Compile;
            }
            else if (arg is FunctionalId)
            {
                return delegate (CompileState state)
                {
                    Compile((FunctionalId)arg, state);
                };
            }
            else
            {
                throw new NotSupportedException($"Function arguments of type '{arg.GetType().Name}' are not supported.");
            }
        }

        #endregion

        #region Support for Functions & Procedures

        internal Lazy<bool> HasBlueprint41Plugin => GetProcedure("blueprint41.functionalid.list");
        internal Lazy<bool> HasBlueprint41FunctionalidFnNext => GetFunction("blueprint41.functionalid.fnNext");
        internal Lazy<bool> HasBlueprint41FunctionalidFnNextNumeric => GetFunction("blueprint41.functionalid.fnNextNumeric");

        private Lazy<bool> GetFunction(string function)
        {
            return new Lazy<bool>(delegate ()
            {
                Neo4jPersistenceProvider? neo4j = PersistenceProvider as Neo4jPersistenceProvider;
                return neo4j?.HasFunction(function) ?? false;
            }, true);
        }
        private Lazy<bool> GetProcedure(string procedure)
        {
            return new Lazy<bool>(delegate ()
            {
                Neo4jPersistenceProvider? neo4j = PersistenceProvider as Neo4jPersistenceProvider;
                return neo4j?.HasProcedure(procedure) ?? false;
            }, true);
        }

        #endregion
    }
}
