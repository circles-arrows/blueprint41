
using System;
using Blueprint41.Core;

namespace Datastore.Manipulation
{
  internal class Register
    {
        private static bool isInitialized = false;

        public static void Types()
        {
            if (isInitialized)
                return;

            lock (typeof(Register))
            {
                if (isInitialized)
                    return;

				isInitialized = true;

                ((ISetRuntimeType)Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Base"]).SetRuntimeTypes(typeof(IBase), typeof(Base));
                ((ISetRuntimeType)Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Film"]).SetRuntimeTypes(typeof(Film), typeof(Film));
                ((ISetRuntimeType)Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Genre"]).SetRuntimeTypes(typeof(Genre), typeof(Genre));
                ((ISetRuntimeType)Blueprint41.GremlinUnitTest.GremlinStore.Model.Entities["Person"]).SetRuntimeTypes(typeof(Person), typeof(Person));
            }
        }
    }
}
