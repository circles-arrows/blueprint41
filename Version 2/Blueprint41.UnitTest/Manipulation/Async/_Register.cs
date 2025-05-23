#nullable disable
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable CS8981 // Names should not be lower type only
#pragma warning disable IDE0130 // Namespace does not match folder structure
#pragma warning disable VSSpell001 // Spell Check

// ASYNC

using System;
using Blueprint41.Core;
using Blueprint41.Events;

namespace Datastore.Manipulation.Async
{
    internal class Register
    {
        [Obsolete]
        public static void Types()
        {
            if (Blueprint41.UnitTest.DataStore.MockModel.Model.TypesRegistered)
                return;

            lock (typeof(Register))
            {
                if (Blueprint41.UnitTest.DataStore.MockModel.Model.TypesRegistered)
                    return;

                ((ISetRuntimeType)Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["BaseEntity"]).SetRuntimeTypes(typeof(IBaseEntity), typeof(BaseEntity));
                ((ISetRuntimeType)Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["City"]).SetRuntimeTypes(typeof(City), typeof(City));
                ((ISetRuntimeType)Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Movie"]).SetRuntimeTypes(typeof(Movie), typeof(Movie));
                ((ISetRuntimeType)Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Person"]).SetRuntimeTypes(typeof(Person), typeof(Person));
                ((ISetRuntimeType)Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Rating"]).SetRuntimeTypes(typeof(Rating), typeof(Rating));
                ((ISetRuntimeType)Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"]).SetRuntimeTypes(typeof(Restaurant), typeof(Restaurant));
                ((ISetRuntimeType)Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["StreamingService"]).SetRuntimeTypes(typeof(StreamingService), typeof(StreamingService));
            }
        }
    }
}
