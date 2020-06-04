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
            return new AliasListResult(this, t => t.FnCollect);
        }
        public AliasListResult CollectDistinct()
        {
            return new AliasListResult(this, t => t.FnCollectDistinct);
        }
	}
	public partial class MiscResult
	{
        public MiscListResult Collect()
        {
            return new MiscListResult(this, t => t.FnCollect);
        }
        public MiscListResult CollectDistinct()
        {
            return new MiscListResult(this, t => t.FnCollectDistinct);
        }
	}
	public partial class StringResult
	{
        public StringListResult Collect()
        {
            return new StringListResult(this, t => t.FnCollect);
        }
        public StringListResult CollectDistinct()
        {
            return new StringListResult(this, t => t.FnCollectDistinct);
        }
	}
	public partial class BooleanResult
	{
        public BooleanListResult Collect()
        {
            return new BooleanListResult(this, t => t.FnCollect);
        }
        public BooleanListResult CollectDistinct()
        {
            return new BooleanListResult(this, t => t.FnCollectDistinct);
        }
	}
	public partial class DateTimeResult
	{
        public DateTimeListResult Collect()
        {
            return new DateTimeListResult(this, t => t.FnCollect);
        }
        public DateTimeListResult CollectDistinct()
        {
            return new DateTimeListResult(this, t => t.FnCollectDistinct);
        }
	}
	public partial class FloatResult
	{
        public FloatListResult Collect()
        {
            return new FloatListResult(this, t => t.FnCollect);
        }
        public FloatListResult CollectDistinct()
        {
            return new FloatListResult(this, t => t.FnCollectDistinct);
        }
	}
	public partial class NumericResult
	{
        public NumericListResult Collect()
        {
            return new NumericListResult(this, t => t.FnCollect);
        }
        public NumericListResult CollectDistinct()
        {
            return new NumericListResult(this, t => t.FnCollectDistinct);
        }
	}
}