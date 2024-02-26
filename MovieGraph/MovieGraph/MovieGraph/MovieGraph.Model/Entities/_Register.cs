using System;
using Blueprint41.Core;

namespace Domain.Data.Manipulation
{
    internal class Register
    {
        private static bool isInitialized = false;

        public static void Types()
        {
            if (MovieGraph.Model.Datastore.Model.TypesRegistered)
                return;

            lock (typeof(Register))
            {
                if (MovieGraph.Model.Datastore.Model.TypesRegistered)
                    return;

                ((ISetRuntimeType)MovieGraph.Model.Datastore.Model.Entities["Genre"]).SetRuntimeTypes(typeof(Genre), typeof(Genre));
                ((ISetRuntimeType)MovieGraph.Model.Datastore.Model.Entities["Movie"]).SetRuntimeTypes(typeof(Movie), typeof(Movie));
                ((ISetRuntimeType)MovieGraph.Model.Datastore.Model.Entities["MovieReview"]).SetRuntimeTypes(typeof(MovieReview), typeof(MovieReview));
                ((ISetRuntimeType)MovieGraph.Model.Datastore.Model.Entities["MovieRole"]).SetRuntimeTypes(typeof(MovieRole), typeof(MovieRole));
                ((ISetRuntimeType)MovieGraph.Model.Datastore.Model.Entities["Person"]).SetRuntimeTypes(typeof(Person), typeof(Person));
            }
        }
    }
}
