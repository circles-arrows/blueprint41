using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Blueprint41.Core;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;

using Datastore.Manipulation;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ClientException = Neo4j.Driver.ClientException;

namespace Blueprint41.UnitTest.Tests
{
    public partial class TestRelationships
    {
        [Test]
        public void RenameProperty()
        {
            SetupTestDataSet();
            
            Execute(RenameAddrLine1);

            // Assert...
        }

        [Test]
        public void MergePropertyPreferSource()
        {
            SetupTestDataSet();

            Execute(MergeAddrLine1And2IntoAddrLine1);

            // Assert...
        }

        [Test]
        public void MergePropertyPreferTarget()
        {
            SetupTestDataSet();

            Execute(MergeAddrLine1And2IntoAddrLine2);

            // Assert...
        }

        [Test]
        public void MergeAddrLine1And2AndFail()
        {
            SetupTestDataSet();

            Execute(MergeAddrLine1And2AndThrow);

            // Assert...
        }

        [Test]
        public void ToCompressedString()
        {
            SetupTestDataSet();

            Execute(CompressAddrLine1);

            // Assert...
        }

        [Test]
        public void Convert()
        {
            SetupTestDataSet();

            Execute(ConvertMinsWatchedToString);

            // Assert...
        }

        [Test]
        public void SetIndexTypeToIndex()
        {
            SetupTestDataSet();

            Execute(IndexAddrLine1);

            // Assert...
        }

        [Test]
        public void SetIndexTypeToUnique()
        {
            SetupTestDataSet();

            Execute(UniqueAddrLine1);

            // Assert...
        }

        [Test]
        public void Deprecate()
        {
            SetupTestDataSet();

            Execute(DeprecateAddrLine2And3);

            // Assert...
        }

        [Test]
        public void MakeNullable()
        {
            SetupTestDataSet();

            Execute(MakeMinutesWatchedNullable);

            // Assert...
        }

        [Test]
        public void MakeMandatoryWithoutDefaultNoThrow()
        {
            SetupTestDataSet();

            Execute(MakeAddrLine1Mandatory);

            // Assert...
        }

        [Test]
        public void MakeMandatoryWithDefaultNoThrow()
        {
            SetupTestDataSet();

            Execute(MakeAddrLine2MandatoryWithDefault);

            // Assert...
        }

        [Test]
        public void MakeMandatoryWithoutDefaultAndThrow()
        {
            SetupTestDataSet();

            Exception ex = Assert.Throws<InvalidOperationException>(() => Execute(MakeAddrLine3MandatoryAndThrow));
            Assert.That(ex.Message.Contains("Some nodes in the database contains null values for PERSON_LIVES_IN.AddressLine3."));
        }

        [Test]
        public void SetDefaultValue()
        {
            SetupTestDataSet();

            Execute(SetMinsWatchedFromNullToZero);

            // Assert...
        }
    }
}
