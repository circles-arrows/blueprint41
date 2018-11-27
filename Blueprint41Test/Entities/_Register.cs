
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

                ((ISetRuntimeType)Blueprint41Test.MovieModel.Model.Entities["Actor"]).SetRuntimeTypes(typeof(Actor), typeof(Actor));
                ((ISetRuntimeType)Blueprint41Test.MovieModel.Model.Entities["Film"]).SetRuntimeTypes(typeof(Film), typeof(Film));
            }
        }
    }
}
