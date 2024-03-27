using System;
using Blueprint41.Core;

namespace Memgraph.Datastore.Manipulation
{
    internal class Register
    {
        private static bool isInitialized = false;

        public static void Types()
        {
            if (Blueprint41.UnitTest.Multiple.Memgraph.DataStore.MemgraphModel.Model.TypesRegistered)
                return;

            lock (typeof(Register))
            {
                if (Blueprint41.UnitTest.Multiple.Memgraph.DataStore.MemgraphModel.Model.TypesRegistered)
                    return;

                ((ISetRuntimeType)Blueprint41.UnitTest.Multiple.Memgraph.DataStore.MemgraphModel.Model.Entities["Movie"]).SetRuntimeTypes(typeof(Movie), typeof(Movie));
                ((ISetRuntimeType)Blueprint41.UnitTest.Multiple.Memgraph.DataStore.MemgraphModel.Model.Entities["Person"]).SetRuntimeTypes(typeof(Person), typeof(Person));
            }
        }
    }
}
