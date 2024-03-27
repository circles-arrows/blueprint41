using System;
using Blueprint41.Core;

namespace Neo4j.Datastore.Manipulation
{
    internal class Register
    {
        private static bool isInitialized = false;

        public static void Types()
        {
            if (Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.TypesRegistered)
                return;

            lock (typeof(Register))
            {
                if (Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.TypesRegistered)
                    return;

                ((ISetRuntimeType)Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"]).SetRuntimeTypes(typeof(Movie), typeof(Movie));
                ((ISetRuntimeType)Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Person"]).SetRuntimeTypes(typeof(Person), typeof(Person));
            }
        }
    }
}
