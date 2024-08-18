using Blueprint41.Neo4j.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
	public partial class MiscListResult : ListResult<MiscListResult, MiscResult, object>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(MiscListResult left, MiscListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(MiscListResult left, object[] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(MiscListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(MiscListResult left, MiscListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(MiscListResult left, object[] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(MiscListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public MiscListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public MiscListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public MiscListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public MiscListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out MiscListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new MiscListResult(aliasResult, null, null, null, typeof(object));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out MiscListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
		public MiscJaggedListResult PairsMin()
		{
			return new MiscJaggedListResult(this, t => t.FnApocCollPairsMin);
		}
	}
	public partial class StringListResult : ListResult<StringListResult, StringResult, string>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(StringListResult left, StringListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(StringListResult left, string[] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(StringListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(StringListResult left, StringListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(StringListResult left, string[] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(StringListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public StringListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public StringListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public StringListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public StringListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out StringListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new StringListResult(aliasResult, null, null, null, typeof(string));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out StringListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
		public StringJaggedListResult PairsMin()
		{
			return new StringJaggedListResult(this, t => t.FnApocCollPairsMin);
		}
	}
	public partial class BooleanListResult : ListResult<BooleanListResult, BooleanResult, bool>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(BooleanListResult left, BooleanListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(BooleanListResult left, bool[] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(BooleanListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(BooleanListResult left, BooleanListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(BooleanListResult left, bool[] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(BooleanListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public BooleanListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public BooleanListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public BooleanListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public BooleanListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out BooleanListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new BooleanListResult(aliasResult, null, null, null, typeof(bool));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out BooleanListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
		public BooleanJaggedListResult PairsMin()
		{
			return new BooleanJaggedListResult(this, t => t.FnApocCollPairsMin);
		}
	}
	public partial class DateTimeListResult : ListResult<DateTimeListResult, DateTimeResult, DateTime>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(DateTimeListResult left, DateTimeListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(DateTimeListResult left, DateTime[] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(DateTimeListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(DateTimeListResult left, DateTimeListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(DateTimeListResult left, DateTime[] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(DateTimeListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public DateTimeListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public DateTimeListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public DateTimeListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public DateTimeListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out DateTimeListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new DateTimeListResult(aliasResult, null, null, null, typeof(DateTime));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out DateTimeListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
		public DateTimeJaggedListResult PairsMin()
		{
			return new DateTimeJaggedListResult(this, t => t.FnApocCollPairsMin);
		}
	}
	public partial class FloatListResult : ListResult<FloatListResult, FloatResult, double>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(FloatListResult left, FloatListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(FloatListResult left, double[] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(FloatListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(FloatListResult left, FloatListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(FloatListResult left, double[] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(FloatListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public FloatListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public FloatListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public FloatListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public FloatListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out FloatListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new FloatListResult(aliasResult, null, null, null, typeof(double));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out FloatListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
		public FloatJaggedListResult PairsMin()
		{
			return new FloatJaggedListResult(this, t => t.FnApocCollPairsMin);
		}
	}
	public partial class NumericListResult : ListResult<NumericListResult, NumericResult, long>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(NumericListResult left, NumericListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(NumericListResult left, long[] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(NumericListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(NumericListResult left, NumericListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(NumericListResult left, long[] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(NumericListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public NumericListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public NumericListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public NumericListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public NumericListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out NumericListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new NumericListResult(aliasResult, null, null, null, typeof(long));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out NumericListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
		public NumericJaggedListResult PairsMin()
		{
			return new NumericJaggedListResult(this, t => t.FnApocCollPairsMin);
		}
	}
	public partial class MiscJaggedListResult : ListResult<MiscJaggedListResult, MiscListResult, object[]>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(MiscJaggedListResult left, MiscJaggedListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(MiscJaggedListResult left, object[][] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(MiscJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(MiscJaggedListResult left, MiscJaggedListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(MiscJaggedListResult left, object[][] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(MiscJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public MiscJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public MiscJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public MiscJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public MiscJaggedListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out MiscJaggedListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new MiscJaggedListResult(aliasResult, null, null, null, typeof(object[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out MiscJaggedListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class StringJaggedListResult : ListResult<StringJaggedListResult, StringListResult, string[]>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(StringJaggedListResult left, StringJaggedListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(StringJaggedListResult left, string[][] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(StringJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(StringJaggedListResult left, StringJaggedListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(StringJaggedListResult left, string[][] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(StringJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public StringJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public StringJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public StringJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public StringJaggedListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out StringJaggedListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new StringJaggedListResult(aliasResult, null, null, null, typeof(string[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out StringJaggedListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class BooleanJaggedListResult : ListResult<BooleanJaggedListResult, BooleanListResult, bool[]>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(BooleanJaggedListResult left, BooleanJaggedListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(BooleanJaggedListResult left, bool[][] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(BooleanJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(BooleanJaggedListResult left, BooleanJaggedListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(BooleanJaggedListResult left, bool[][] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(BooleanJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public BooleanJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public BooleanJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public BooleanJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public BooleanJaggedListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out BooleanJaggedListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new BooleanJaggedListResult(aliasResult, null, null, null, typeof(bool[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out BooleanJaggedListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class DateTimeJaggedListResult : ListResult<DateTimeJaggedListResult, DateTimeListResult, DateTime[]>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(DateTimeJaggedListResult left, DateTimeJaggedListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(DateTimeJaggedListResult left, DateTime[][] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(DateTimeJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(DateTimeJaggedListResult left, DateTimeJaggedListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(DateTimeJaggedListResult left, DateTime[][] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(DateTimeJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public DateTimeJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public DateTimeJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public DateTimeJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public DateTimeJaggedListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out DateTimeJaggedListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new DateTimeJaggedListResult(aliasResult, null, null, null, typeof(DateTime[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out DateTimeJaggedListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class FloatJaggedListResult : ListResult<FloatJaggedListResult, FloatListResult, double[]>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(FloatJaggedListResult left, FloatJaggedListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(FloatJaggedListResult left, double[][] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(FloatJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(FloatJaggedListResult left, FloatJaggedListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(FloatJaggedListResult left, double[][] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(FloatJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public FloatJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public FloatJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public FloatJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public FloatJaggedListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out FloatJaggedListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new FloatJaggedListResult(aliasResult, null, null, null, typeof(double[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out FloatJaggedListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class NumericJaggedListResult : ListResult<NumericJaggedListResult, NumericListResult, long[]>, IPrimitiveListResult
	{
		public static QueryCondition operator ==(NumericJaggedListResult left, NumericJaggedListResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
		public static QueryCondition operator ==(NumericJaggedListResult left, long[][] right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(NumericJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(NumericJaggedListResult left, NumericJaggedListResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(NumericJaggedListResult left, long[][] right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(NumericJaggedListResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }


		public NumericJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public NumericJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public NumericJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public NumericJaggedListResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out NumericJaggedListResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new NumericJaggedListResult(aliasResult, null, null, null, typeof(long[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out NumericJaggedListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
}
