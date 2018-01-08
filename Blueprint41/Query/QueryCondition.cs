using Blueprint41.Core;
using System;
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
            Left = left;
            Operator = op;
            Right = right;
        }

        private object Left;
        private Operator Operator;
        private object Right;

        internal void Compile(CompileState state)
        {
            Left = Substitute(state, Left);
            Right = Substitute(state, Right);

            Type leftType = GetOperandType(Left);
            Type rightType = GetOperandType(Right);

            if (leftType != null && rightType != null)
            {
                if (leftType != rightType)
                {
                    if (GetConversionGroup(leftType, state.TypeMappings) != GetConversionGroup(rightType, state.TypeMappings))
                        state.Errors.Add($"The types of the fields {state.Preview(s => CompileOperand(s, Left))} and {state.Preview(s => CompileOperand(s, Right))} are not compatible.");
                }
            }

            state.Text.Append("(");
            CompileOperand(state, Left);

            if (Right is Parameter)
            {
                Parameter rightParameter = Right as Parameter;
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
            
            state.Text.Append(")");
        }


        private object Substitute(CompileState state, object operand)
        {
            Type type = operand?.GetType();

            if (operand == null)
            {
                return null;
            }
            else if (type.IsSubclassOfOrSelf(typeof(Result)))
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

        private Type GetOperandType(object operand)
        {
            Type type = operand?.GetType();

            if (operand == null)
            {
                return null;
            }
            else if (type.IsSubclassOfOrSelf(typeof(Result)))
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

        private void CompileOperand(CompileState state, object operand)
        {
            Type type = operand?.GetType();

            if (operand == null)
            {
                state.Text.Append("NULL");
            }
            else if (type.IsSubclassOfOrSelf(typeof(Result)))
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
                state.Errors.Add($"The type {type.Name} is not supported for compilation.");
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
