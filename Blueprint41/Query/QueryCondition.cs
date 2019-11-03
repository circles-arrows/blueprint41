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

        private object? Left;
        private Operator Operator;
        private object? Right;

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
            if (Operator != Operator.Not)
                CompileOperand(state, Left);
            else
                state.Text.Append("NOT(");

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

            if (Operator == Operator.Not)
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
            else
            {
                TypeMapping mapping = state.TypeMappings.FirstOrDefault(item => item.ReturnType == type);
                if (mapping == null)
                    return operand;

                return Parameter.Constant(operand, type);
            }
        }
        private static Type GetEnumeratedType(Type type)
        {
            return type.GetElementType() ?? (typeof(IEnumerable).IsAssignableFrom(type) ? type.GenericTypeArguments.FirstOrDefault() : null);
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
            else
            {
                throw new NotSupportedException("The expression is not supported for compilation.");
            }
        }

        private void CompileOperand(CompileState state, object? operand)
        {
            if (operand == null)
                return;

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
            else
            {
                state.Errors.Add($"The type {type!.Name} is not supported for compilation.");
                state.Text.Append(operand.ToString());
            }
        }

        private string GetConversionGroup(Type type, IEnumerable<TypeMapping> mappings)
        {
            TypeMapping mapping = mappings.FirstOrDefault(item => item.ReturnType == type);
            if (mapping == null)
                throw new InvalidOperationException($"An unexpected technical mapping failure while trying to find the conversion for type {type.Name}. Please contact the developer.");

            return mapping.ComparisonGroup;
        }
    }
}
