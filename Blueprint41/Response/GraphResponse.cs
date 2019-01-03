using Neo4j.Driver.V1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Response
{
    public interface IGraphResponse : IEnumerable<IRecord>, IEnumerable
    {
        object Result { get; }
    }

    public class GraphResponse : IGraphResponse
    {
        public object Result { get; private set; }

        public static GraphResponse GetGraphResponse<T>(object result)
        {
            GraphResponse graph = new GraphResponse();

            Type t = typeof(T);
            if (t == (typeof(IStatementResult)))
            {
                graph.Result = (T)result;
            }
            else if (t == typeof(GremlinResult))
            {
                graph.Result = (T)Activator.CreateInstance(t, result);
            }
            else
                throw new NotSupportedException($"The type {t} is not yet supported");

            return graph;
        }

        public IEnumerator<IRecord> GetEnumerator()
        {
            if (Result is IEnumerable<IRecord> res)
                return res.GetEnumerator();

            throw new NotSupportedException($"The type {Result.GetType()} is not supported.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
