using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Blueprint41.Query
{
	public partial class AliasResult
	{
        public AliasListResult Collect()
        {
            return new AliasListResult(this, "collect({base})");
        }
        public AliasListResult CollectDistinct()
        {
            return new AliasListResult(this, "collect(distinct {base})");
        }
	}
	public partial class MiscResult
	{
        public MiscListResult Collect()
        {
            return new MiscListResult(this, "collect({base})");
        }
        public MiscListResult CollectDistinct()
        {
            return new MiscListResult(this, "collect(distinct {base})");
        }
	}
	public partial class StringResult
	{
        public StringListResult Collect()
        {
            return new StringListResult(this, "collect({base})");
        }
        public StringListResult CollectDistinct()
        {
            return new StringListResult(this, "collect(distinct {base})");
        }
	}
	public partial class BooleanResult
	{
        public BooleanListResult Collect()
        {
            return new BooleanListResult(this, "collect({base})");
        }
        public BooleanListResult CollectDistinct()
        {
            return new BooleanListResult(this, "collect(distinct {base})");
        }
	}
	public partial class DateTimeResult
	{
        public DateTimeListResult Collect()
        {
            return new DateTimeListResult(this, "collect({base})");
        }
        public DateTimeListResult CollectDistinct()
        {
            return new DateTimeListResult(this, "collect(distinct {base})");
        }
	}
	public partial class FloatResult
	{
        public FloatListResult Collect()
        {
            return new FloatListResult(this, "collect({base})");
        }
        public FloatListResult CollectDistinct()
        {
            return new FloatListResult(this, "collect(distinct {base})");
        }
	}
	public partial class NumericResult
	{
        public NumericListResult Collect()
        {
            return new NumericListResult(this, "collect({base})");
        }
        public NumericListResult CollectDistinct()
        {
            return new NumericListResult(this, "collect(distinct {base})");
        }
	}
}