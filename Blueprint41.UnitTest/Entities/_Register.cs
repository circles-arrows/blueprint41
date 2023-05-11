using System;
using Blueprint41.Core;

namespace Datastore.Manipulation
{
	internal class Register
	{
		private static bool isInitialized = false;

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
				((ISetRuntimeType)Blueprint41.UnitTest.DataStore.MockModel.Model.Entities["Restaurant"]).SetRuntimeTypes(typeof(Restaurant), typeof(Restaurant));
			}
		}
	}
}
