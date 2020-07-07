using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Query
{
    public interface IResult
    {
        AsResult As(string aliasName);
        AsResult As<T>(string aliasName, out T alias) where T : IResult;
    }

    public interface IPrimitiveResult : IResult
    {
    }
    public interface IAliasResult : IResult
    {
    }
    public interface IListResult : IResult
    {
    }
    public interface IPlainPrimitiveResult : IPrimitiveResult
    {
    }
    public interface IPlainAliasResult : IAliasResult
    {
    }
    public interface IPlainListResult : IListResult
    {
    }

    public interface IPrimitiveListResult : IPrimitiveResult, IListResult
    {
    }
    public interface IAliasListResult : IAliasResult, IListResult
    {
    }

    public interface IJaggedListResult : IListResult
    {
    }
    public interface IPrimitiveJaggedListResult : IPrimitiveListResult, IJaggedListResult
    {
    }
    public interface IAliasJaggedListResult : IAliasListResult, IJaggedListResult
    {
    }
}
