using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Query
{
    public static class Functions
    {

        public static Parameter NULL { get { return null!; } }
        public static FloatResult Pi()
        {
            return new FloatResult(t => t.FnPi, null, typeof(Double));
        }
        public static FloatResult Rand()
        {
            return new FloatResult(t => t.FnRand, null, typeof(Double));
        }
        public static StringResult SHA1(params FieldResult[] fields)
        {
            return new StringResult(t => t.FnSHA1(fields.Length), fields, typeof(string));
        }
        public static StringResult MD5(params FieldResult[] fields)
        {
            return new StringResult(t => t.FnMD5(fields.Length), fields, typeof(string));
        }
        public static NumericListResult Range(int start, int end, int step)
        {
            return new NumericListResult(t => t.FnRange, new object[] { Parameter.Constant(start), Parameter.Constant(end), Parameter.Constant(step) }, typeof(int));
        }
        public static NumericListResult Range(int start, int end, NumericResult step)
        {
            return new NumericListResult(t => t.FnRange, new object[] { Parameter.Constant(start), Parameter.Constant(end), step }, typeof(int));
        }
        public static NumericListResult Range(int start, NumericResult end, int step)
        {
            return new NumericListResult(t => t.FnRange, new object[] { Parameter.Constant(start), end, Parameter.Constant(step) }, typeof(int));
        }
        public static NumericListResult Range(int start, NumericResult end, NumericResult step)
        {
            return new NumericListResult(t => t.FnRange, new object[] { Parameter.Constant(start), end, step }, typeof(int));
        }
        public static NumericListResult Range(NumericResult start, int end, int step)
        {
            return new NumericListResult(t => t.FnRange, new object[] { start, Parameter.Constant(end), Parameter.Constant(step) }, typeof(int));
        }
        public static NumericListResult Range(NumericResult start, int end, NumericResult step)
        {
            return new NumericListResult(t => t.FnRange, new object[] { start, Parameter.Constant(end), step }, typeof(int));
        }
        public static NumericListResult Range(NumericResult start, NumericResult end, int step)
        {
            return new NumericListResult(t => t.FnRange, new object[] { start, end, Parameter.Constant(step) }, typeof(int));
        }
        public static NumericListResult Range(NumericResult start, NumericResult end, NumericResult step)
        {
            return new NumericListResult(t => t.FnRange, new object[] { start, end, step }, typeof(int));
        }

        public static T CaseWhen<T>(QueryCondition condition, Parameter @then, Parameter @else)
             where T : IPlainPrimitiveResult
        {
            ResultHelper<T> info = ResultHelper.Of<T>();
            Type underlyingType = info.UnderlyingType!;

            return info.NewFunctionResult(t => t.FnCaseWhen, new object[] { condition, WrapIfNull(@then), WrapIfNull(@else) }, underlyingType);
        }
        public static T CaseWhen<T>(QueryCondition[] conditions, Parameter[] thens, Parameter @else)
             where T : IPlainPrimitiveResult
        {
            if (conditions.Length != thens.Length)
                throw new ArgumentException("The number of condition and then statement should match.");

            ResultHelper<T> info = ResultHelper.Of<T>();
            Type underlyingType = info.UnderlyingType!;

            object[] arguments = new object[(conditions.Length << 1) + 1];
            int argNum = 0;
            for (int index = 0; index < conditions.Length; index++)
            {
                arguments[argNum] = conditions[index];
                argNum++;

                arguments[argNum] = WrapIfNull(thens[index]);
                argNum++;
            }
            arguments[argNum] = WrapIfNull(@else);

            return info.NewFunctionResult(t => t.FnCaseWhenMultiple(conditions.Length), arguments, underlyingType);
        }
        public static T CaseWhen<T>(QueryCondition condition, T @then, T @else)
            where T : IResult
        {
            ResultHelper<T> info = ResultHelper.Of<T>();
            Type? underlyingType = info.UnderlyingType;

            return info.NewFunctionResult(t => t.FnCaseWhen, new object[] { condition, WrapIfNull(@then), WrapIfNull(@else) }, underlyingType);
        }
        public static T CaseWhen<T>(QueryCondition[] conditions, T[] thens, T @else)
             where T : IResult
        {
            if (conditions.Length != thens.Length)
                throw new ArgumentException("The number of condition and then statement should match.");

            ResultHelper<T> info = ResultHelper.Of<T>();
            Type? underlyingType = info.UnderlyingType;

            object[] arguments = new object[(conditions.Length << 1) + 1];
            int argNum = 0;
            for (int index = 0; index < conditions.Length; index++)
            {
                arguments[argNum] = conditions[index];
                argNum++;

                arguments[argNum] = WrapIfNull(thens[index]);
                argNum++;
            }
            arguments[argNum] = WrapIfNull(@else);

            return info.NewFunctionResult(t => t.FnCaseWhenMultiple(conditions.Length), arguments, underlyingType);
        }
        private static object WrapIfNull(object value)
        {
            if (value is null)
                return Parameter.Null;

            return value;
        }
    }
}
