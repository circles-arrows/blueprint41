using Blueprint41.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class QueryCondition
    {
        public static QueryCondition operator &(QueryCondition a, QueryCondition b)
        {
            return new QueryCondition(a, Operator.And, b);
        }
        public static QueryCondition operator |(QueryCondition a, QueryCondition b)
        {
            return new QueryCondition(a, Operator.Or, b);
        }

        internal QueryCondition(object left, Operator op, object right)
        {
            if (op == Operator.Boolean && (left as BooleanResult) is null)
                throw new InvalidOperationException("The left side of an boolean operator must be of type BooleanResult");
                    
            Left = left;
            Operator = op;
            Right = right;
        }
        public QueryCondition(BooleanResult booleanResult)
        {
            Left = booleanResult;
            Operator = Operator.Boolean;
            Right = null;
        }
        public QueryCondition(Blueprint41.Query.Node node, bool not = false)
        {
            Left = string.Empty;
            Operator = (not) ? Operator.NotPattern : Operator.Pattern;
            Right = node;
        }

        internal object? Left;
        internal Operator Operator;
        internal object? Right;

        internal void Compile(CompileState state)
        {
            Left = Substitute(state, Left);
            Right = Substitute(state, Right);

            if (Operator == Operator.Boolean)
            {
                state.Text.Append("(");
                ((BooleanResult)Left!).Compile(state);
                state.Text.Append(")");
                return;
            }

            Type? leftType = GetOperandType(Left);
            Type? rightType = GetOperandType(Right);

            if (leftType != null && rightType != null)
            {
                if (leftType != rightType)
                {
                    if (Operator == Operator.In)
                    {
                        if(rightType.GetInterface(nameof(IEnumerable)) == null)
                            state.Errors.Add($"The types of the fields {state.Preview(s => CompileOperand(s, Right))} should be a collection.");

                        rightType = GetEnumeratedType(rightType);
                    }
                    if (GetConversionGroup(leftType, state.TypeMappings) != GetConversionGroup(rightType, state.TypeMappings))
                        state.Errors.Add($"The types of the fields {state.Preview(s => CompileOperand(s, Left))} and {state.Preview(s => CompileOperand(s, Right))} are not compatible.");
                }
            }

            state.Text.Append("(");
            if (Operator == Operator.Not || Operator == Operator.NotPattern)
                state.Text.Append("NOT(");
            else if (Operator != Operator.Pattern)
                CompileOperand(state, Left);

            if (Right is Parameter)
            {
                Parameter rightParameter = (Parameter)Right; 
                if (rightParameter.IsConstant && rightParameter.Value == null)
                {
                    Operator.Compile(state, true);
                    CompileOperand(state, null);
                }
                else
                {
                    Operator.Compile(state, false);
                    CompileOperand(state, Right);
                }
            }
            else
            {
                Operator.Compile(state, Right == null);
                CompileOperand(state, Right);
            }

            if (Operator == Operator.Not || Operator == Operator.NotPattern)
                state.Text.Append(")");

            state.Text.Append(")");
        }


        private object? Substitute(CompileState state, object? operand)
        {
            if (operand == null)
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
            else
            {
                state.TypeMappings.TryGetValue(type, out TypeMapping mapping);
                if (mapping == null)
                    return operand;

                return Parameter.Constant(operand, type);
            }
        }
        private static Type GetEnumeratedType(Type? type)
        {
            if (type is null)
                throw new InvalidOperationException("The type cannot be null");

            Type? result = type.GetElementType();
            if (result is null && typeof(IEnumerable).IsAssignableFrom(type))
                result = type.GenericTypeArguments.FirstOrDefault();

            return result ?? type;
        }

        private Type? GetOperandType(object? operand)
        {
            if (operand == null)
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
            else
            {
                throw new NotSupportedException("The expression is not supported for compilation.");
            }
        }

        private void CompileOperand(CompileState state, object? operand)
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
                else
                {
                    state.Errors.Add($"The type {type!.Name} is not supported for compilation.");
                    state.Text.Append(operand.ToString());
                }
            }
        }

        private string GetConversionGroup(Type type, IReadOnlyDictionary<Type, TypeMapping> mappings)
        {
            mappings.TryGetValue(type, out TypeMapping mapping);
            if (mapping == null)
                throw new InvalidOperationException($"An unexpected technical mapping failure while trying to find the conversion for type {type.Name}. Please contact the developer.");

            return mapping.ComparisonGroup;
        }
    }
}
