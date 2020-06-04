using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public enum Operator
    {
        And,
        Or,
        Equals,
        NotEquals,
        Less,
        LessOrEqual,
        Greater,
        GreaterOrEqual,
        StartsWith,
        EndsWith,
        Contains,
        Match,
        HasLabel,
        In,
        Not,
        Boolean
    }

    public static partial class ExtensionMethods
    {
        internal static void Compile(this Operator op, CompileState state, bool rightIsNull)
        {
            switch (op)
            {
                case Operator.And:
                    state.Text.Append(state.Translator.OpAnd);
                    break;
                case Operator.Or:
                    state.Text.Append(state.Translator.OpOr);
                    break;
                case Operator.Equals:
                    if (rightIsNull)
                        state.Text.Append(state.Translator.OpIs);
                    else
                        state.Text.Append(state.Translator.OpEqual);
                    break;
                case Operator.NotEquals:
                    if (rightIsNull)
                        state.Text.Append(state.Translator.OpIsNot);
                    else
                        state.Text.Append(state.Translator.OpNotEqual);
                    break;
                case Operator.Less:
                    state.Text.Append(state.Translator.OpLessThan);
                    break;
                case Operator.LessOrEqual:
                    state.Text.Append(state.Translator.OpLessThanOrEqual);
                    break;
                case Operator.Greater:
                    state.Text.Append(state.Translator.OpGreaterThan);
                    break;
                case Operator.GreaterOrEqual:
                    state.Text.Append(state.Translator.OpGreaterThanOrEqual);
                    break;
                case Operator.StartsWith:
                    state.Text.Append(state.Translator.OpStartsWith);
                    break;
                case Operator.EndsWith:
                    state.Text.Append(state.Translator.OpEndsWith);
                    break;
                case Operator.Contains:
                    state.Text.Append(state.Translator.OpContains);
                    break;
                case Operator.Match:
                    state.Text.Append(state.Translator.OpMatch);
                    break;
                case Operator.HasLabel:
                    state.Text.Append(state.Translator.OpHasLabel);
                    break;
                case Operator.In:
                    state.Text.Append(state.Translator.OpIn);
                    break;
                case Operator.Not:
                    break;
                default:
                    throw new NotSupportedException($"The operator {op.ToString()} is not supported yet.");
            }
        }
    }
}
