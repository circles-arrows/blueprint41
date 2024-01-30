using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Blueprint41.Neo4j.Model;

namespace Blueprint41.Query
{
	public partial class MiscResult : FieldResult<MiscResult, MiscListResult, object>, IPlainPrimitiveResult
	{
		internal MiscResult(FieldResult field) : base(field) { }
		public MiscResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : base(function, arguments, type) { }
		public MiscResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? overridenReturnType = null) : base(alias, fieldName, entity, property, overridenReturnType) { }
		public MiscResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public MiscResult(FieldResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

		public AsResult As(string aliasName, out MiscResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new MiscResult(aliasResult, null, null, null, this.GetResultType() ?? typeof(object));
			//alias = new MiscResult(aliasResult, null, null, null, typeof(object));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out MiscResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class StringResult : FieldResult<StringResult, StringListResult, string>, IPlainPrimitiveResult
	{
		internal StringResult(FieldResult field) : base(field) { }
		public StringResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : base(function, arguments, type) { }
		public StringResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? overridenReturnType = null) : base(alias, fieldName, entity, property, overridenReturnType) { }
		public StringResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public StringResult(FieldResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

		public AsResult As(string aliasName, out StringResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new StringResult(aliasResult, null, null, null, this.GetResultType() ?? typeof(string));
			//alias = new StringResult(aliasResult, null, null, null, typeof(string));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out StringResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class BooleanResult : FieldResult<BooleanResult, BooleanListResult, bool>, IPlainPrimitiveResult
	{
		internal BooleanResult(FieldResult field) : base(field) { }
		public BooleanResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : base(function, arguments, type) { }
		public BooleanResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? overridenReturnType = null) : base(alias, fieldName, entity, property, overridenReturnType) { }
		public BooleanResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public BooleanResult(FieldResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

		public AsResult As(string aliasName, out BooleanResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new BooleanResult(aliasResult, null, null, null, this.GetResultType() ?? typeof(bool));
			//alias = new BooleanResult(aliasResult, null, null, null, typeof(bool));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out BooleanResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class DateTimeResult : FieldResult<DateTimeResult, DateTimeListResult, DateTime>, IPlainPrimitiveResult
	{
		internal DateTimeResult(FieldResult field) : base(field) { }
		public DateTimeResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : base(function, arguments, type) { }
		public DateTimeResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? overridenReturnType = null) : base(alias, fieldName, entity, property, overridenReturnType) { }
		public DateTimeResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public DateTimeResult(FieldResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

		public AsResult As(string aliasName, out DateTimeResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new DateTimeResult(aliasResult, null, null, null, this.GetResultType() ?? typeof(DateTime));
			//alias = new DateTimeResult(aliasResult, null, null, null, typeof(DateTime));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out DateTimeResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class FloatResult : FieldResult<FloatResult, FloatListResult, double>, IPlainPrimitiveResult
	{
		internal FloatResult(FieldResult field) : base(field) { }
		public FloatResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : base(function, arguments, type) { }
		public FloatResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? overridenReturnType = null) : base(alias, fieldName, entity, property, overridenReturnType) { }
		public FloatResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public FloatResult(FieldResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

		public AsResult As(string aliasName, out FloatResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new FloatResult(aliasResult, null, null, null, this.GetResultType() ?? typeof(double));
			//alias = new FloatResult(aliasResult, null, null, null, typeof(double));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out FloatResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
	public partial class NumericResult : FieldResult<NumericResult, NumericListResult, long>, IPlainPrimitiveResult
	{
		internal NumericResult(FieldResult field) : base(field) { }
		public NumericResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : base(function, arguments, type) { }
		public NumericResult(AliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? overridenReturnType = null) : base(alias, fieldName, entity, property, overridenReturnType) { }
		public NumericResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
		public NumericResult(FieldResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

		public AsResult As(string aliasName, out NumericResult alias)
		{
			AliasResult aliasResult = new AliasResult(this, null)
			{
				AliasName = aliasName
			};

			alias = new NumericResult(aliasResult, null, null, null, this.GetResultType() ?? typeof(long));
			//alias = new NumericResult(aliasResult, null, null, null, typeof(long));
			return this.As(aliasName);
		}
		AsResult IResult.As<T>(string aliasName, out T alias)
		{
			AsResult retval = As(aliasName, out NumericResult genericAlias);
			alias = (T)(object)genericAlias;
			return retval;
		}
	}
}