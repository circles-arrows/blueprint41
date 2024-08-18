using Blueprint41.Async.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using query = Blueprint41.Async.Query;

namespace Blueprint41.Async.Query
{
	public partial class Query : IUnwindQuery<MiscResult>
	{
		public IUnwindQuery<MiscResult> Unwind(MiscListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out MiscResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new MiscResult(aliasResult, null, null, null, typeof(object));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<MiscResult> Unwind(MiscListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<MiscResult> Unwind(MiscListResult list);
	}
	public partial class Query : IUnwindQuery<StringResult>
	{
		public IUnwindQuery<StringResult> Unwind(StringListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out StringResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new StringResult(aliasResult, null, null, null, typeof(string));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<StringResult> Unwind(StringListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<StringResult> Unwind(StringListResult list);
	}
	public partial class Query : IUnwindQuery<BooleanResult>
	{
		public IUnwindQuery<BooleanResult> Unwind(BooleanListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out BooleanResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new BooleanResult(aliasResult, null, null, null, typeof(bool));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<BooleanResult> Unwind(BooleanListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<BooleanResult> Unwind(BooleanListResult list);
	}
	public partial class Query : IUnwindQuery<DateTimeResult>
	{
		public IUnwindQuery<DateTimeResult> Unwind(DateTimeListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out DateTimeResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new DateTimeResult(aliasResult, null, null, null, typeof(DateTime));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<DateTimeResult> Unwind(DateTimeListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<DateTimeResult> Unwind(DateTimeListResult list);
	}
	public partial class Query : IUnwindQuery<FloatResult>
	{
		public IUnwindQuery<FloatResult> Unwind(FloatListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out FloatResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new FloatResult(aliasResult, null, null, null, typeof(double));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<FloatResult> Unwind(FloatListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<FloatResult> Unwind(FloatListResult list);
	}
	public partial class Query : IUnwindQuery<NumericResult>
	{
		public IUnwindQuery<NumericResult> Unwind(NumericListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out NumericResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new NumericResult(aliasResult, null, null, null, typeof(long));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<NumericResult> Unwind(NumericListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<NumericResult> Unwind(NumericListResult list);
	}
	public partial class Query : IUnwindQuery<MiscListResult>
	{
		public IUnwindQuery<MiscListResult> Unwind(MiscJaggedListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out MiscListResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new MiscListResult(aliasResult, null, null, null, typeof(object[]));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<MiscListResult> Unwind(MiscJaggedListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<MiscListResult> Unwind(MiscJaggedListResult list);
	}
	public partial class Query : IUnwindQuery<StringListResult>
	{
		public IUnwindQuery<StringListResult> Unwind(StringJaggedListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out StringListResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new StringListResult(aliasResult, null, null, null, typeof(string[]));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<StringListResult> Unwind(StringJaggedListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<StringListResult> Unwind(StringJaggedListResult list);
	}
	public partial class Query : IUnwindQuery<BooleanListResult>
	{
		public IUnwindQuery<BooleanListResult> Unwind(BooleanJaggedListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out BooleanListResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new BooleanListResult(aliasResult, null, null, null, typeof(bool[]));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<BooleanListResult> Unwind(BooleanJaggedListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<BooleanListResult> Unwind(BooleanJaggedListResult list);
	}
	public partial class Query : IUnwindQuery<DateTimeListResult>
	{
		public IUnwindQuery<DateTimeListResult> Unwind(DateTimeJaggedListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out DateTimeListResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new DateTimeListResult(aliasResult, null, null, null, typeof(DateTime[]));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<DateTimeListResult> Unwind(DateTimeJaggedListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<DateTimeListResult> Unwind(DateTimeJaggedListResult list);
	}
	public partial class Query : IUnwindQuery<FloatListResult>
	{
		public IUnwindQuery<FloatListResult> Unwind(FloatJaggedListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out FloatListResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new FloatListResult(aliasResult, null, null, null, typeof(double[]));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<FloatListResult> Unwind(FloatJaggedListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<FloatListResult> Unwind(FloatJaggedListResult list);
	}
	public partial class Query : IUnwindQuery<NumericListResult>
	{
		public IUnwindQuery<NumericListResult> Unwind(NumericJaggedListResult list)
		{
			SetType(PartType.Unwind);
			Fields = new[] { list };

			return this;
		}
		public IMatchQuery As(string aliasName, out NumericListResult alias)
		{
			this.Results = new[] { Fields.First().As(aliasName) };

			AliasResult aliasResult = new AliasResult()
			{
				AliasName = aliasName
			};
			alias = new NumericListResult(aliasResult, null, null, null, typeof(long[]));

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IUnwindQuery<NumericListResult> Unwind(NumericJaggedListResult list);
	}
	public partial interface IWhereQuery
	{
		IUnwindQuery<NumericListResult> Unwind(NumericJaggedListResult list);
	}
}
