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
			AliasResult aliasResult = new AliasResult()
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
		public ListOfMiscListResult PairsMin()
		{
			return new ListOfMiscListResult(this, t => t.FnPairsMin);
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
			AliasResult aliasResult = new AliasResult()
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
		public ListOfStringListResult PairsMin()
		{
			return new ListOfStringListResult(this, t => t.FnPairsMin);
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
			AliasResult aliasResult = new AliasResult()
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
		public ListOfBooleanListResult PairsMin()
		{
			return new ListOfBooleanListResult(this, t => t.FnPairsMin);
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
			AliasResult aliasResult = new AliasResult()
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
		public ListOfDateTimeListResult PairsMin()
		{
			return new ListOfDateTimeListResult(this, t => t.FnPairsMin);
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
			AliasResult aliasResult = new AliasResult()
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
		public ListOfFloatListResult PairsMin()
		{
			return new ListOfFloatListResult(this, t => t.FnPairsMin);
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
			AliasResult aliasResult = new AliasResult()
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
		public ListOfNumericListResult PairsMin()
		{
			return new ListOfNumericListResult(this, t => t.FnPairsMin);
		}
	}
	public partial class ListOfMiscListResult : ListResult<ListOfMiscListResult, MiscListResult, object[]>, IPrimitiveJaggedListResult
	{
		public ListOfMiscListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public ListOfMiscListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public ListOfMiscListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public ListOfMiscListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out ListOfMiscListResult alias)
		{
			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};

			alias = new ListOfMiscListResult(aliasResult, null, null, null, typeof(object[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out ListOfMiscListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class ListOfStringListResult : ListResult<ListOfStringListResult, StringListResult, string[]>, IPrimitiveJaggedListResult
	{
		public ListOfStringListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public ListOfStringListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public ListOfStringListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public ListOfStringListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out ListOfStringListResult alias)
		{
			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};

			alias = new ListOfStringListResult(aliasResult, null, null, null, typeof(string[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out ListOfStringListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class ListOfBooleanListResult : ListResult<ListOfBooleanListResult, BooleanListResult, bool[]>, IPrimitiveJaggedListResult
	{
		public ListOfBooleanListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public ListOfBooleanListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public ListOfBooleanListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public ListOfBooleanListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out ListOfBooleanListResult alias)
		{
			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};

			alias = new ListOfBooleanListResult(aliasResult, null, null, null, typeof(bool[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out ListOfBooleanListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class ListOfDateTimeListResult : ListResult<ListOfDateTimeListResult, DateTimeListResult, DateTime[]>, IPrimitiveJaggedListResult
	{
		public ListOfDateTimeListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public ListOfDateTimeListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public ListOfDateTimeListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public ListOfDateTimeListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out ListOfDateTimeListResult alias)
		{
			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};

			alias = new ListOfDateTimeListResult(aliasResult, null, null, null, typeof(DateTime[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out ListOfDateTimeListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class ListOfFloatListResult : ListResult<ListOfFloatListResult, FloatListResult, double[]>, IPrimitiveJaggedListResult
	{
		public ListOfFloatListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public ListOfFloatListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public ListOfFloatListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public ListOfFloatListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out ListOfFloatListResult alias)
		{
			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};

			alias = new ListOfFloatListResult(aliasResult, null, null, null, typeof(double[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out ListOfFloatListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class ListOfNumericListResult : ListResult<ListOfNumericListResult, NumericListResult, long[]>, IPrimitiveJaggedListResult
	{
		public ListOfNumericListResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }
		public ListOfNumericListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
		public ListOfNumericListResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public ListOfNumericListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? type = null) : base(alias, fieldName, entity, property, type) { }

		public AsResult As(string aliasName, out ListOfNumericListResult alias)
		{
			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};

			alias = new ListOfNumericListResult(aliasResult, null, null, null, typeof(long[]));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out ListOfNumericListResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
}
