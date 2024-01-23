using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.UnitTest.DataStore;

namespace Blueprint41.UnitTest.Tests
{
    public static class RefactorActionScripts
    {
        public static void Execute(string name)
        {
            MockModel model = new MockModel()
            {
                LogToConsole = true
            };
            ((IDatastoreUnitTesting)model).Execute(true, typeof(RefactorActionScripts).GetMethod(name));
        }


        public static void RemoveMinutesWatchedConstraint(DatastoreModel @this)
        {
            @this.Relations["WATCHED_MOVIE"].Properties["MinutesWatched"].Refactor.MakeNullable();
        }
        public static void RemoveMonthlyFeeConstraint(DatastoreModel @this)
        {
            @this.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"].Properties["MonthlyFee"].Refactor.MakeNullable();
        }

        public static void RenameAddressLine1(DatastoreModel @this)
        {
            @this.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"].Refactor.Rename("NewName");
        }
    }
}
