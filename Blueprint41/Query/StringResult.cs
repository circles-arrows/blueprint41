using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public partial class StringResult : FieldResult
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

        internal StringResult(FieldResult field) : base(field) { }
        public StringResult(string function, object[]? arguments, Type? type) : base(function, arguments, type) { }
        public StringResult(AliasResult alias, string fieldName, Entity entity, Property property, Type? overridenReturnType = null) : base(alias, fieldName, entity, property, overridenReturnType) { }
        public StringResult(FieldResult field, string function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

        public StringResult ToUpperCase()
        {
            return new StringResult(this, "upper({base})");
        }
        public StringResult ToLowerCase()
        {
            return new StringResult(this, "lower({base})");
        }
        public StringResult Reverse()
        {
            return new StringResult(this, "reverse({base})");
        }
        public StringResult Trim()
        {
            return new StringResult(this, "trim({base})");
        }
        public StringResult LTrim()
        {
            return new StringResult(this, "ltrim({base})");
        }
        public StringResult RTrim()
        {
            return new StringResult(this, "rtrim({base})");
        }
        public NumericResult Length()
        {
            return new NumericResult(this, "length({base})", null, typeof(long));
        }

        public StringResult Split(string delimiter)
        {
            return new StringResult(this, "split({base}, {0})", new object[] { Parameter.Constant(delimiter) });
        }
        public StringResult Split(StringResult delimiter)
        {
            return new StringResult(this, "split({base}, {0})", new object[] { delimiter });
        }
        public StringResult Split(Parameter delimiter)
        {
            return new StringResult(this, "split({base}, {0})", new object[] { delimiter });
        }

        public StringResult Left(int subLength)
        {
            return new StringResult(this, "left({base}, {0})", new object[] { Parameter.Constant(subLength) });
        }
        public StringResult Left(NumericResult subLength)
        {
            return new StringResult(this, "left({base}, {0})", new object[] { subLength });
        }
        public StringResult Left(Parameter subLength)
        {
            return new StringResult(this, "left({base}, {0})", new object[] { subLength });
        }

        public StringResult Right(int subLength)
        {
            return new StringResult(this, "right({base}, {0})", new object[] { Parameter.Constant(subLength) });
        }
        public StringResult Right(NumericResult subLength)
        {
            return new StringResult(this, "right({base}, {0})", new object[] { subLength });
        }
        public StringResult Right(Parameter subLength)
        {
            return new StringResult(this, "right({base}, {0})", new object[] { subLength });
        }

        public StringResult Substring(int begin)
        {
            return new StringResult(this, "substring({base}, {0})", new object[] { Parameter.Constant(begin) });
        }
        public StringResult Substring(NumericResult begin)
        {
            return new StringResult(this, "substring({base}, {0})", new object[] { begin });
        }
        public StringResult Substring(Parameter begin)
        {
            return new StringResult(this, "substring({base}, {0})", new object[] { begin });
        }

        public QueryCondition In(IEnumerable<string> enumerable)
        {
            return new QueryCondition(this, Operator.In, Parameter.Constant(enumerable.ToArray(), typeof(string)));
        }

        public QueryCondition NotIn(IEnumerable<string> enumerable)
        {
            return new QueryCondition(new BooleanResult(this, "NOT ({base})"), Operator.In, Parameter.Constant(enumerable.ToArray(), typeof(string)));
        }

        public QueryCondition In(params string[] items)
        {
            return new QueryCondition(this, Operator.In, Parameter.Constant(items, typeof(string)));
        }

        public StringResult Substring(int begin, int subLength)
        {
            return new StringResult(this, "substring({base}, {0}, {1})", new object[] { Parameter.Constant(begin), Parameter.Constant(subLength) });
        }
        public StringResult Substring(NumericResult begin, int subLength)
        {
            return new StringResult(this, "substring({base}, {0}, {1})", new object[] { begin, Parameter.Constant(subLength) });
        }
        public StringResult Substring(Parameter begin, int subLength)
        {
            return new StringResult(this, "substring({base}, {0}, {1})", new object[] { begin, Parameter.Constant(subLength) });
        }
        public StringResult Substring(int begin, NumericResult subLength)
        {
            return new StringResult(this, "substring({base}, {0}, {1})", new object[] { Parameter.Constant(begin), subLength });
        }
        public StringResult Substring(NumericResult begin, NumericResult subLength)
        {
            return new StringResult(this, "substring({base}, {0}, {1})", new object[] { begin, subLength });
        }
        public StringResult Substring(Parameter begin, NumericResult subLength)
        {
            return new StringResult(this, "substring({base}, {0}, {1})", new object[] { begin, subLength });
        }
        public StringResult Substring(int begin, Parameter subLength)
        {
            return new StringResult(this, "substring({base}, {0}, {1})", new object[] { Parameter.Constant(begin), subLength });
        }
        public StringResult Substring(NumericResult begin, Parameter subLength)
        {
            return new StringResult(this, "substring({base}, {0}, {1})", new object[] { begin, subLength });
        }
        public StringResult Substring(Parameter begin, Parameter subLength)
        {
            return new StringResult(this, "substring({base}, {0}, {1})", new object[] { begin, subLength });
        }

        public StringResult Replace(string search, string replacement)
        {
            return new StringResult(this, "replace({base}, {0}, {1})", new object[] { Parameter.Constant(search), Parameter.Constant(replacement) });
        }
        public StringResult Replace(StringResult search, StringResult replacement)
        {
            return new StringResult(this, "replace({base}, {0}, {1})", new object[] { search, replacement });
        }
        public StringResult Replace(Parameter search, Parameter replacement)
        {
            return new StringResult(this, "replace({base}, {0}, {1})", new object[] { search, replacement });
        }
        public StringResult Replace(string search, StringResult replacement)
        {
            return new StringResult(this, "replace({base}, {0}, {1})", new object[] { Parameter.Constant(search), replacement });
        }
        public StringResult Replace(string search, Parameter replacement)
        {
            return new StringResult(this, "replace({base}, {0}, {1})", new object[] { Parameter.Constant(search), replacement });
        }
        public StringResult Replace(StringResult search, string replacement)
        {
            return new StringResult(this, "replace({base}, {0}, {1})", new object[] { search, Parameter.Constant(replacement) });
        }
        public StringResult Replace(StringResult search, Parameter replacement)
        {
            return new StringResult(this, "replace({base}, {0}, {1})", new object[] { search, replacement });
        }
        public StringResult Replace(Parameter search, string replacement)
        {
            return new StringResult(this, "replace({base}, {0}, {1})", new object[] { search, Parameter.Constant(replacement) });
        }
        public StringResult Replace(Parameter search, StringResult replacement)
        {
            return new StringResult(this, "replace({base}, {0}, {1})", new object[] { search, replacement });
        }

        public QueryCondition StartsWith(string text)
        {
            return new QueryCondition(this, Operator.StartsWith, text);
        }
        public QueryCondition StartsWith(Parameter param)
        {
            return new QueryCondition(this, Operator.StartsWith, param);
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
        public QueryCondition MatchRegex(string text)
        {
            return new QueryCondition(this, Operator.Match, text);
        }

        public StringResult Coalesce(string value)
        {
            if (value == null)
                throw new NullReferenceException("value cannot be null");

            return new StringResult(this, "coalesce({base}, {0})", new object[] { Parameter.Constant(value) });
        }
        public StringResult Coalesce(MiscResult value)
        {
            return new StringResult(this, "coalesce({base}, {0})", new object[] { value });
        }
        public StringResult Coalesce(Parameter value)
        {
            return new StringResult(this, "coalesce({base}, {0})", new object[] { value });
        }

        public StringResult Concat(params object[] args)
        {
            if (args == null)
                throw new NullReferenceException("value cannot be null");

            StringBuilder sb = new StringBuilder();
            sb.Append("{base}");

            int index = 0;
            object[] parameters = new object[args.Length];
            foreach (object arg in args)
            {
                if (arg is string)
                    parameters[index] = Parameter.Constant<string>((string)arg);
                else
                    parameters[index] = arg;

                sb.Append(" + {");
                sb.Append(index);
                sb.Append("}");
                index++;
            }

            return new StringResult(this, $"({sb.ToString()})", parameters, typeof(string));
        }

        public StringResult Min()
        {
            return new StringResult(this, "min({base})");
        }
        public StringResult Max()
        {
            return new StringResult(this, "max({base})");
        }
    }
}
