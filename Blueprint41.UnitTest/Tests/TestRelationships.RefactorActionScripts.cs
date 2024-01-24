// Ignore Spelling: Nullable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.UnitTest.DataStore;

namespace Blueprint41.UnitTest.Tests
{
    public partial class TestRelationships
    {
        public static void Execute(Action<DatastoreModel> script)
        {
            string name = script.Method.Name;

            MockModel model = new MockModel()
            {
                LogToConsole = true
            };
            ((IDatastoreUnitTesting)model).Execute(true, typeof(TestRelationships).GetMethod(name));
        }

        #region RenameProperty          -> PERSON_LIVES_IN.AddressLine1

        public static void RenameAddressLine1(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"].Refactor.Rename("NewName");
        }

        #endregion

        #region MergeProperty()         -> PERSON_LIVES_IN.AddressLine2 -> PERSON_LIVES_IN.AddressLine1

        #endregion

        #region ToCompressedString()    -> PERSON_LIVES_IN.AddressLine1

        #endregion

        #region Convert()               -> WATCHED_MOVIE.MinutesWatched

        #endregion

        #region SetIndexType()          -> PERSON_LIVES_IN.AddressLine1

        #endregion

        #region Deprecate()             -> PERSON_LIVES_IN.AddressLine2 + PERSON_LIVES_IN.AddressLine3

        #endregion

        #region MakeNullable            -> WATCHED_MOVIE.MinutesWatched & SUBSCRIBED_TO_STREAMING_SERVICE.MonthlyFee

        public static void MakeMinutesWatchedNullable(DatastoreModel model)
        {
            model.Relations["WATCHED_MOVIE"].Properties["MinutesWatched"].Refactor.MakeNullable();
        }
        public static void MakeMonthlyFeeNullable(DatastoreModel model)
        {
            model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"].Properties["MonthlyFee"].Refactor.MakeNullable();
        }

        #endregion

        #region MakeMandatory()         -> PERSON_LIVES_IN.AddressLine1

        #endregion

        #region SetDefaultValue()       -> WATCHED_MOVIE.MinutesWatched

        #endregion
    }
}
