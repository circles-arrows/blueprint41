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
		public MiscListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public MiscListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public MiscListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public MiscListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public StringListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public StringListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public StringListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public StringListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public BooleanListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public BooleanListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public BooleanListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public BooleanListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public DateTimeListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public DateTimeListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public DateTimeListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public DateTimeListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public FloatListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public FloatListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public FloatListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public FloatListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public NumericListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public NumericListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public NumericListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public NumericListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public MiscJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public MiscJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public MiscJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public MiscJaggedListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public StringJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public StringJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public StringJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public StringJaggedListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public BooleanJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public BooleanJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public BooleanJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public BooleanJaggedListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public DateTimeJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public DateTimeJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public DateTimeJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public DateTimeJaggedListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public FloatJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public FloatJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public FloatJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public FloatJaggedListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
		public NumericJaggedListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public NumericJaggedListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public NumericJaggedListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public NumericJaggedListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

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
