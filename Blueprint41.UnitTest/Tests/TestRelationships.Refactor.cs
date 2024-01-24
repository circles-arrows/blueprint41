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
            
            Execute(RenameAddressLine1);

            // Assert...
        }

        [Test]
        public void MergeProperty()
        {
            SetupTestDataSet();

            Execute(RenameAddressLine1);

            // Assert...
        }

        [Test]
        public void ToCompressedString()
        {
            SetupTestDataSet();

            Execute(RenameAddressLine1);

            // Assert...
        }

        [Test]
        public void Convert()
        {
            SetupTestDataSet();

            Execute(RenameAddressLine1);

            // Assert...
        }

        [Test]
        public void SetIndexType()
        {
            SetupTestDataSet();

            Execute(RenameAddressLine1);

            // Assert...
        }

        [Test]
        public void Deprecate()
        {
            SetupTestDataSet();

            Execute(RenameAddressLine1);

            // Assert...
        }

        [Test]
        public void MakeNullable()
        {
            SetupTestDataSet();

            Execute(RenameAddressLine1);

            // Assert...
        }

        [Test]
        public void MakeMandatory()
        {
            SetupTestDataSet();

            Execute(RenameAddressLine1);

            // Assert...
        }

        [Test]
        public void SetDefaultValue()
        {
            SetupTestDataSet();

            Execute(RenameAddressLine1);

            // Assert...
        }
    }
}
