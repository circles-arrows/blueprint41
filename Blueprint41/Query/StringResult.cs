using Blueprint41.Neo4j.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public partial class StringResult
    {
        #region Operators

        public static QueryCondition operator ==(StringResult left, string right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(StringResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(StringResult left, string right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(StringResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }

        public static QueryCondition operator >(StringResult left, string right)
        {
            return new QueryCondition(left, Operator.Greater, Parameter.Constant(right));
        }
        public static QueryCondition operator >(StringResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Greater, right);
        }
        public static QueryCondition operator <(StringResult left, string right)
        {
            return new QueryCondition(left, Operator.Less, Parameter.Constant(right));
        }
        public static QueryCondition operator <(StringResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Less, right);
        }

        public static QueryCondition operator >=(StringResult left, string right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator >=(StringResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, right);
        }
        public static QueryCondition operator <=(StringResult left, string right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator <=(StringResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, right);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        public StringResult ToUpperCase()
        {
            return new StringResult(this, t => t.FnToUpper);
        }
        public StringResult ToLowerCase()
        {
            return new StringResult(this, t => t.FnToLower);
        }
        public StringResult Reverse()
        {
            return new StringResult(this, t => t.FnReverse);
        }
        public StringResult Trim()
        {
            return new StringResult(this, t => t.FnTrim);
        }
        public StringResult LTrim()
        {
            return new StringResult(this, t => t.FnLeftTrim);
        }
        public StringResult RTrim()
        {
            return new StringResult(this, t => t.FnRightTrim);
        }
        public NumericResult Length()
        {
            return new NumericResult(this, t => t.FnSize, null, typeof(long));
        }

        public StringListResult Split(string delimiter)
        {
            return new StringListResult(this, t => t.FnSplit, new object[] { Parameter.Constant(delimiter) });
        }
        public StringListResult Split(StringResult delimiter)
        {
            return new StringListResult(this, t => t.FnSplit, new object[] { delimiter });
        }
        public StringListResult Split(Parameter delimiter)
        {
            return new StringListResult(this, t => t.FnSplit, new object[] { delimiter });
        }

        public StringResult Left(int subLength)
        {
            return new StringResult(this, t => t.FnLeft, new object[] { Parameter.Constant(subLength) });
        }
        public StringResult Left(NumericResult subLength)
        {
            return new StringResult(this, t => t.FnLeft, new object[] { subLength });
        }
        public StringResult Left(Parameter subLength)
        {
            return new StringResult(this, t => t.FnLeft, new object[] { subLength });
        }

        public StringResult Right(int subLength)
        {
            return new StringResult(this, t => t.FnRight, new object[] { Parameter.Constant(subLength) });
        }
        public StringResult Right(NumericResult subLength)
        {
            return new StringResult(this, t => t.FnRight, new object[] { subLength });
        }
        public StringResult Right(Parameter subLength)
        {
            return new StringResult(this, t => t.FnRight, new object[] { subLength });
        }

        public StringResult Substring(int begin)
        {
            return new StringResult(this, t => t.FnSubStringWOutLen, new object[] { Parameter.Constant(begin) });
        }
        public StringResult Substring(NumericResult begin)
        {
            return new StringResult(this, t => t.FnSubStringWOutLen, new object[] { begin });
        }
        public StringResult Substring(Parameter begin)
        {
            return new StringResult(this, t => t.FnSubStringWOutLen, new object[] { begin });
        }

        public QueryCondition In(IEnumerable<string> enumerable)
        {
            return new QueryCondition(this, Operator.In, Parameter.Constant(enumerable.ToArray(), typeof(string)));
        }

        public QueryCondition NotIn(IEnumerable<string> enumerable)
        {
            return new QueryCondition(new BooleanResult(this, t => t.FnNot), Operator.In, Parameter.Constant(enumerable.ToArray(), typeof(string)));
        }

        public QueryCondition In(params string[] items)
        {
            return new QueryCondition(this, Operator.In, Parameter.Constant(items, typeof(string)));
        }

        public StringResult Substring(int begin, int subLength)
        {
            return new StringResult(this, t => t.FnSubString, new object[] { Parameter.Constant(begin), Parameter.Constant(subLength) });
        }
        public StringResult Substring(NumericResult begin, int subLength)
        {
            return new StringResult(this, t => t.FnSubString, new object[] { begin, Parameter.Constant(subLength) });
        }
        public StringResult Substring(Parameter begin, int subLength)
        {
            return new StringResult(this, t => t.FnSubString, new object[] { begin, Parameter.Constant(subLength) });
        }
        public StringResult Substring(int begin, NumericResult subLength)
        {
            return new StringResult(this, t => t.FnSubString, new object[] { Parameter.Constant(begin), subLength });
        }
        public StringResult Substring(NumericResult begin, NumericResult subLength)
        {
            return new StringResult(this, t => t.FnSubString, new object[] { begin, subLength });
        }
        public StringResult Substring(Parameter begin, NumericResult subLength)
        {
            return new StringResult(this, t => t.FnSubString, new object[] { begin, subLength });
        }
        public StringResult Substring(int begin, Parameter subLength)
        {
            return new StringResult(this, t => t.FnSubString, new object[] { Parameter.Constant(begin), subLength });
        }
        public StringResult Substring(NumericResult begin, Parameter subLength)
        {
            return new StringResult(this, t => t.FnSubString, new object[] { begin, subLength });
        }
        public StringResult Substring(Parameter begin, Parameter subLength)
        {
            return new StringResult(this, t => t.FnSubString, new object[] { begin, subLength });
        }

        public StringResult Replace(string search, string replacement)
        {
            return new StringResult(this, t => t.FnReplace, new object[] { Parameter.Constant(search), Parameter.Constant(replacement) });
        }
        public StringResult Replace(StringResult search, StringResult replacement)
        {
            return new StringResult(this, t => t.FnReplace, new object[] { search, replacement });
        }
        public StringResult Replace(Parameter search, Parameter replacement)
        {
            return new StringResult(this, t => t.FnReplace, new object[] { search, replacement });
        }
        public StringResult Replace(string search, StringResult replacement)
        {
            return new StringResult(this, t => t.FnReplace, new object[] { Parameter.Constant(search), replacement });
        }
        public StringResult Replace(string search, Parameter replacement)
        {
            return new StringResult(this, t => t.FnReplace, new object[] { Parameter.Constant(search), replacement });
        }
        public StringResult Replace(StringResult search, string replacement)
        {
            return new StringResult(this, t => t.FnReplace, new object[] { search, Parameter.Constant(replacement) });
        }
        public StringResult Replace(StringResult search, Parameter replacement)
        {
            return new StringResult(this, t => t.FnReplace, new object[] { search, replacement });
        }
        public StringResult Replace(Parameter search, string replacement)
        {
            return new StringResult(this, t => t.FnReplace, new object[] { search, Parameter.Constant(replacement) });
        }
        public StringResult Replace(Parameter search, StringResult replacement)
        {
            return new StringResult(this, t => t.FnReplace, new object[] { search, replacement });
        }

        public QueryCondition StartsWith(string text)
        {
            return new QueryCondition(this, Operator.StartsWith, text);
        }
        public QueryCondition StartsWith(Parameter param)
        {
            return new QueryCondition(this, Operator.StartsWith, param);
        }
        public QueryCondition NotStartsWith(string text)
        {
            return new QueryCondition(new BooleanResult(this, t => t.FnNot), Operator.StartsWith, text);
        }
        public QueryCondition NotStartsWith(Parameter param)
        {
            return new QueryCondition(new BooleanResult(this, t => t.FnNot), Operator.StartsWith, param);
        }
        public QueryCondition EndsWith(string text)
        {
            return new QueryCondition(this, Operator.EndsWith, text);
        }
        public QueryCondition EndsWith(Parameter param)
        {
            return new QueryCondition(this, Operator.EndsWith, param);
        }
        public QueryCondition Contains(string text)
        {
            return new QueryCondition(this, Operator.Contains, text);
        }
        public QueryCondition Contains(Parameter param)
        {
            return new QueryCondition(this, Operator.Contains, param);
        }
        public QueryCondition NotContains(string text)
        {
            return new QueryCondition(new BooleanResult(this, t => t.FnNot), Operator.Contains, text);
        }
        public QueryCondition NotContains(Parameter param)
        {
            return new QueryCondition(new BooleanResult(this, t => t.FnNot), Operator.Contains, param);
        }
        public QueryCondition MatchRegex(string text)
        {
            return new QueryCondition(this, Operator.Match, text);
        }

        public StringResult Concat(params object[] args)
        {
            if (args is null)
                throw new NullReferenceException("value cannot be null");

            int count = args.Length;
            object[] parameters = new object[count];
            for (int index = 0; index < count; index++)
            {
                object arg = args[index];
                if (arg is string)
                    parameters[index] = Parameter.Constant<string>((string)arg);
                else
                    parameters[index] = arg;
            }

            return new StringResult(this, t => t.FnConcatMultiple(count), parameters, typeof(string));
        }

        public StringResult Min()
        {
            return new StringResult(this, t => t.FnMin);
        }
        public StringResult Max()
        {
            return new StringResult(this, t => t.FnMax);
        }
    }
}
