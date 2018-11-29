using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Response
{
    public interface IGraphResponse
    {
        object Data { get; }
    }

    public class GraphResponse : IGraphResponse
    {
        public object Data { get; private set; }

        public static GraphResponse GetGraphResponse<T>(object result)
        {
            GraphResponse graph = new GraphResponse();

            Type t = typeof(T);
            if (t == (typeof(IStatementResult)))
            {
                graph.Data = (T)result;
            }
            else if (t == typeof(GremlinResult))
            {
                graph.Data = (T)Activator.CreateInstance(t, result);
            }
            else
                throw new NotSupportedException($"The type {t} is not yet supported");

            return graph;
        }
    }

}
