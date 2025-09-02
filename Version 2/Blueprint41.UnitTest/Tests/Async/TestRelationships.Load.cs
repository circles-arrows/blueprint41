using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Blueprint41.Core;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;

using Datastore.Manipulation.Async;

using NUnit.Framework;
using NUnit.Framework.Internal;

using ClientException = Neo4j.Driver.ClientException;

namespace Blueprint41.UnitTest.Tests.Async
{
    public partial class TestRelationships : TestBase
    {
        [Test]
        public async Task RelationDirectLoad()
        {
            await SetupTestDataSetAsync();

            await using (MockModel.BeginTransactionAsync())
            {
                Person? linus = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);

                Assert.IsNotNull(linus);

                List<PERSON_LIVES_IN> livesIn1 = await PERSON_LIVES_IN.WhereAsync(alias => alias.Person(linus));
                List<PERSON_LIVES_IN> livesIn2 = await PERSON_LIVES_IN.WhereAsync(InNode: linus);
                List<PERSON_LIVES_IN> livesIn3 = await PERSON_LIVES_IN.WhereAsync(AddressLine1: "1630 Revello Drive");

                await livesIn1.AssignAsync(AddressLine1: "OTHER");

                List<PERSON_LIVES_IN> livesIn4 = await PERSON_LIVES_IN.WhereAsync(AddressLine1: "OTHER");

                PERSON_LIVES_IN livesIn5 = await linus!.GetCityIfAsync(null, AddressLine1: "OTHER");
                List<PERSON_LIVES_IN> livesIn6 = await linus.CityWhereAsync(AddressLine1: "OTHER");
                List<PERSON_LIVES_IN> livesIn7 = await linus.CityWhereAsync(Moment: DateTime.UtcNow, AddressLine1: "OTHER");

                await Transaction.CommitAsync();
            }

            await ExecuteAsync(Blocking.TestRelationships.RenameAddrLine1);

            await using (MockModel.BeginTransactionAsync())
            {
                var linus = await Person.LoadAsync(DatabaseUids.Persons.LinusTorvalds);
                Assert.IsNotNull(linus);

                var rels = await ReadRelationsWithPropertiesAsync(linus!, PERSON_LIVES_IN.Relationship, await linus!.GetCityAsync());
                Assert.That(rels.All(r => r.properties.ContainsKey("NewName")));
            }
        }
    }
}
