using Blueprint41.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using query = Blueprint41.Query;

namespace Blueprint41.Query
{
	public partial class Query
	{
		public IMatchQuery Unwind<TList, TResult, TType>(ListResult<TList, TResult, TType> list, string aliasName, out TResult alias)
			where TList : ListResult<TList, TResult, TType>, IPrimitiveListResult
			where TResult : FieldResult, IPrimitiveResult
		{
			SetType(PartType.Unwind);

			Results = new[] { list.As(aliasName) };
			list.NewResult(t => aliasName).As(aliasName, out alias);

			return New;
		}
		public IMatchQuery Unwind<TList, TResult>(ListResult<TList, TResult> list, string aliasName, out TResult alias)
			where TList : ListResult<TList, TResult>, IAliasListResult
			where TResult : AliasResult, IAliasResult
		{
			SetType(PartType.Unwind);

			Results = new[] { list.As(aliasName) };
			list.NewResult(t => aliasName).As(aliasName, out alias);

			return New;
		}
	}
	public partial interface IOptionalMatchQuery
	{
		IMatchQuery Unwind<TList, TResult, TType>(ListResult<TList, TResult, TType> list, string aliasName, out TResult alias)
			where TList : ListResult<TList, TResult, TType>, IPrimitiveListResult
			where TResult : FieldResult, IPrimitiveResult;
		IMatchQuery Unwind<TList, TResult>(ListResult<TList, TResult> list, string aliasName, out TResult alias)
		 where TList : ListResult<TList, TResult>, IAliasListResult
			where TResult : AliasResult, IAliasResult;
	}
	public partial interface IWhereQuery
	{
		IMatchQuery Unwind<TList, TResult, TType>(ListResult<TList, TResult, TType> list, string aliasName, out TResult alias)
			where TList : ListResult<TList, TResult, TType>, IPrimitiveListResult
			where TResult : FieldResult, IPrimitiveResult;

		IMatchQuery Unwind<TList, TResult>(ListResult<TList, TResult> list, string aliasName, out TResult alias)
		 where TList : ListResult<TList, TResult>, IAliasListResult
			where TResult : AliasResult, IAliasResult;
	}
	public partial interface ICallSubqueryMatch
    {
		IMatchQuery Unwind<TList, TResult, TType>(ListResult<TList, TResult, TType> list, string aliasName, out TResult alias)
			where TList : ListResult<TList, TResult, TType>, IPrimitiveListResult
			where TResult : FieldResult, IPrimitiveResult;

		IMatchQuery Unwind<TList, TResult>(ListResult<TList, TResult> list, string aliasName, out TResult alias)
		 where TList : ListResult<TList, TResult>, IAliasListResult
			where TResult : AliasResult, IAliasResult;
	}
}
