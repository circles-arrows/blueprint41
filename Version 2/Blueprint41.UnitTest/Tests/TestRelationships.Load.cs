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
        public void RelationDirectLoad()
        {
            SetupTestDataSet();

            using (MockModel.BeginTransaction())
            {
                var linus = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                Assert.IsNotNull(linus);

                List<PERSON_LIVES_IN> livesIn1 = PERSON_LIVES_IN.Where(alias => alias.Person(linus));
                List<PERSON_LIVES_IN> livesIn2 = PERSON_LIVES_IN.Where(InNode: linus);
                List<PERSON_LIVES_IN> livesIn3 = PERSON_LIVES_IN.Where(AddressLine1: "1630 Revello Drive");

                livesIn1.Assign(AddressLine1: "OTHER");

                List<PERSON_LIVES_IN> livesIn4 = PERSON_LIVES_IN.Where(AddressLine1: "OTHER");

                PERSON_LIVES_IN livesIn5 = linus!.GetCityIf(null, AddressLine1: "OTHER");
                List<PERSON_LIVES_IN> livesIn6 = linus.CityWhere(AddressLine1: "OTHER");
                List<PERSON_LIVES_IN> livesIn7 = linus.CityWhere(Moment: DateTime.UtcNow, AddressLine1: "OTHER");

                Transaction.Commit();
            }

            Execute(RenameAddrLine1);

            using (MockModel.BeginTransaction())
            {
                var linus = Person.Load(DatabaseUids.Persons.LinusTorvalds);
                Assert.IsNotNull(linus);

                var rels = ReadRelationsWithProperties(linus!, PERSON_LIVES_IN.Relationship, linus!.City);
                Assert.That(rels.All(r => r.properties.ContainsKey("NewName")));
            }
        }


    }
}
