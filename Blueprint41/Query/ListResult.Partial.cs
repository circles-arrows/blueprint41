using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Blueprint41.Query
{
	public abstract partial class ListResult<TList, TResult>
	{
		//public MiscListResult Extract(Func<TResult, MiscResult> logic)
        //{
        //    TResult itemField = NewResult("item", new object[0], typeof(object));
        //    MiscResult result = logic.Invoke(itemField);
		//
        //    return new MiscListResult(this, "extract(item in {base} | {0})", new object[] { result }, typeof(object));
        //}
		//public StringListResult Extract(Func<TResult, StringResult> logic)
        //{
        //    TResult itemField = NewResult("item", new object[0], typeof(string));
        //    StringResult result = logic.Invoke(itemField);
		//
        //    return new StringListResult(this, "extract(item in {base} | {0})", new object[] { result }, typeof(string));
        //}
		//public BooleanListResult Extract(Func<TResult, BooleanResult> logic)
        //{
        //    TResult itemField = NewResult("item", new object[0], typeof(bool));
        //    BooleanResult result = logic.Invoke(itemField);
		//
        //    return new BooleanListResult(this, "extract(item in {base} | {0})", new object[] { result }, typeof(bool));
        //}
		//public DateTimeListResult Extract(Func<TResult, DateTimeResult> logic)
        //{
        //    TResult itemField = NewResult("item", new object[0], typeof(DateTime));
        //    DateTimeResult result = logic.Invoke(itemField);
		//
        //    return new DateTimeListResult(this, "extract(item in {base} | {0})", new object[] { result }, typeof(DateTime));
        //}
		//public FloatListResult Extract(Func<TResult, FloatResult> logic)
        //{
        //    TResult itemField = NewResult("item", new object[0], typeof(double));
        //    FloatResult result = logic.Invoke(itemField);
		//
        //    return new FloatListResult(this, "extract(item in {base} | {0})", new object[] { result }, typeof(double));
        //}
		//public NumericListResult Extract(Func<TResult, NumericResult> logic)
        //{
        //    TResult itemField = NewResult("item", new object[0], typeof(long));
        //    NumericResult result = logic.Invoke(itemField);
		//
        //    return new NumericListResult(this, "extract(item in {base} | {0})", new object[] { result }, typeof(long));
        //}
	}
	public abstract partial class ListResult<TList, TResult, TType>
	{
		public MiscListResult Extract(Func<TResult, MiscResult> logic)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            MiscResult result = logic.Invoke(itemField);

            return new MiscListResult(this, "extract(item in {base} | {0})", new object[] { result });
        }
		public StringListResult Extract(Func<TResult, StringResult> logic)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            StringResult result = logic.Invoke(itemField);

            return new StringListResult(this, "extract(item in {base} | {0})", new object[] { result });
        }
		public BooleanListResult Extract(Func<TResult, BooleanResult> logic)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            BooleanResult result = logic.Invoke(itemField);

            return new BooleanListResult(this, "extract(item in {base} | {0})", new object[] { result });
        }
		public DateTimeListResult Extract(Func<TResult, DateTimeResult> logic)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            DateTimeResult result = logic.Invoke(itemField);

            return new DateTimeListResult(this, "extract(item in {base} | {0})", new object[] { result });
        }
		public FloatListResult Extract(Func<TResult, FloatResult> logic)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            FloatResult result = logic.Invoke(itemField);

            return new FloatListResult(this, "extract(item in {base} | {0})", new object[] { result });
        }
		public NumericListResult Extract(Func<TResult, NumericResult> logic)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            NumericResult result = logic.Invoke(itemField);

            return new NumericListResult(this, "extract(item in {base} | {0})", new object[] { result });
        }
	}
}