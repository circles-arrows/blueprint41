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
        In
    }

    public static partial class ExtensionMethods
    {
        internal static void Compile(this Operator op, CompileState state, bool rightIsNull)
        {
            switch (op)
            {
                case Operator.And:
                    state.Text.Append(" AND ");
                    break;
                case Operator.Or:
                    state.Text.Append(" OR ");
                    break;
                case Operator.Equals:
                    if (rightIsNull)
                        state.Text.Append(" IS ");
                    else
                        state.Text.Append(" = ");
                    break;
                case Operator.NotEquals:
                    if (rightIsNull)
                        state.Text.Append(" IS NOT ");
                    else
                        state.Text.Append(" <> ");
                    break;
                case Operator.Less:
                    state.Text.Append(" < ");
                    break;
                case Operator.LessOrEqual:
                    state.Text.Append(" <= ");
                    break;
                case Operator.Greater:
                    state.Text.Append(" > ");
                    break;
                case Operator.GreaterOrEqual:
                    state.Text.Append(" >= ");
                    break;
                case Operator.StartsWith:
                    state.Text.Append(" STARTS WITH ");
                    break;
                case Operator.EndsWith:
                    state.Text.Append(" ENDS WITH ");
                    break;
                case Operator.Contains:
                    state.Text.Append(" CONTAINS ");
                    break;
                case Operator.Match:
                    state.Text.Append(" =~ ");
                    break;
                case Operator.HasLabel:
                    state.Text.Append(":");
                    break;
                case Operator.In:
                    state.Text.Append(" IN ");
                    break;
                default:
                    throw new NotSupportedException($"The operator {op.ToString()} is not supported yet.");
            }
        }
    }
}
