// Ignore Spelling: Nullable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Refactoring;
using Blueprint41.UnitTest.DataStore;

namespace Blueprint41.UnitTest.Tests
{
    public partial class TestRelationships
    {
        #region RenameProperty          -> PERSON_LIVES_IN.AddressLine1

        public static void RenameAddrLine1(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"].Refactor.Rename("NewName");
        }

        #endregion

        #region MergeProperty()         -> PERSON_LIVES_IN.AddressLine2 -> PERSON_LIVES_IN.AddressLine1

        public static void MergeAddrLine1And2IntoAddrLine1(DatastoreModel model)
        {
            var addrLine1 = model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"];
            var addrLine2 = model.Relations["PERSON_LIVES_IN"].Properties["AddressLine2"];
            
            addrLine2.Refactor.Merge(addrLine1, MergeAlgorithm.PreferSource);
        }
        public static void MergeAddrLine1And2IntoAddrLine2(DatastoreModel model)
        {
            var addrLine1 = model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"];
            var addrLine2 = model.Relations["PERSON_LIVES_IN"].Properties["AddressLine2"];

            addrLine1.Refactor.Merge(addrLine2, MergeAlgorithm.PreferTarget);
        }
        public static void MergeAddrLine1And2AndThrow(DatastoreModel model)
        {
            var addrLine1 = model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"];
            var addrLine2 = model.Relations["PERSON_LIVES_IN"].Properties["AddressLine2"];

            addrLine1.Refactor.Merge(addrLine2, MergeAlgorithm.ThrowOnConflict);
        }

        #endregion

        #region ToCompressedString()    -> PERSON_LIVES_IN.AddressLine1

        public static void CompressAddrLine1(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"].Refactor.ToCompressedString();
        }

        #endregion

        #region Convert()               -> WATCHED_MOVIE.MinutesWatched

        public static void ConvertMinsWatchedToString(DatastoreModel model)
        {
            model.Relations["WATCHED_MOVIE"].Properties["MinutesWatched"].Refactor.Convert(typeof(string));
        }

        #endregion

        #region SetIndexType()          -> PERSON_LIVES_IN.AddressLine1
        
        public static void IndexAddrLine1(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"].Refactor.SetIndexType(IndexType.Indexed);
        }
        public static void UniqueAddrLine1(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"].Refactor.SetIndexType(IndexType.Unique);
        }

        #endregion

        #region Deprecate()             -> PERSON_LIVES_IN.AddressLine2 + PERSON_LIVES_IN.AddressLine3

        public static void DeprecateAddrLine2And3(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine2"].Refactor.Deprecate();
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine3"].Refactor.Deprecate();
        }

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

        public static void MakeAddrLine1Mandatory(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine1"].Refactor.MakeMandatory();
        }
        public static void MakeAddrLine2MandatoryWithDefault(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine2"].Refactor.MakeMandatory("DEFAULT");
        }
        public static void MakeAddrLine3MandatoryAndThrow(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine3"].Refactor.MakeMandatory();
        }

        #endregion

        #region SetDefaultValue()       -> WATCHED_MOVIE.MinutesWatched

        public static void SetAddrLine3FromNullToDEFAULT(DatastoreModel model)
        {
            model.Relations["PERSON_LIVES_IN"].Properties["AddressLine2"].Refactor.SetDefaultValue("DEFAULT");
        }

        #endregion
    }
}
