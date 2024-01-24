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

        #region RenameProperty

        public static void RenameAddressLine1(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"].Refactor.Rename("NewName");
        }

        #endregion

        #region MergeProperty()

        #endregion

        #region ToCompressedString()

        #endregion

        #region Convert()

        #endregion

        #region SetIndexType()

        #endregion

        #region Deprecate()

        #endregion

        #region MakeNullable

        public static void MakeMinutesWatchedNullable(DatastoreModel model)
        {
            model.Relations["WATCHED_MOVIE"].Properties["MinutesWatched"].Refactor.MakeNullable();
        }
        public static void MakeMonthlyFeeNullable(DatastoreModel model)
        {
            model.Relations["SUBSCRIBED_TO_STREAMING_SERVICE"].Properties["MonthlyFee"].Refactor.MakeNullable();
        }

        #endregion

        #region MakeMandatory()

        #endregion

        #region SetDefaultValue()

        #endregion
    }
}
