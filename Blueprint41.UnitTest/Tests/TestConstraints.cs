using Blueprint41.Neo4j.Persistence;
using Blueprint41.UnitTest.Helper;
using Neo4j.Driver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blueprint41.UnitTest.Tests
{
    internal class TestConstraints : TestBase    
    {
        private class MovieDataStoreModel_01 : DatastoreModel<MovieDataStoreModel_01>
        {
            protected override void SubscribeEventHandlers() { }

            [Version(0, 0, 0)]
            public void Script_0_0_0()
            {
                FunctionalIds.Default = FunctionalIds.UUID;

                Entities.New("Movie")
                        .AddProperty("title", typeof(string), IndexType.Unique)
                        .AddProperty("tagline", typeof(string))
                        .AddProperty("released", typeof(int))
                        .AddProperty("Uid", typeof(string), false, IndexType.Unique);
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                Entities["Movie"].SetKey("Uid", false);
            }
        }

        private class MovieDataStoreModel_02 : DatastoreModel<MovieDataStoreModel_02>
        {
            protected override void SubscribeEventHandlers() { }

            [Version(0, 0, 0)]
            public void Script_0_0_0()
            {
                FunctionalIds.Default = FunctionalIds.UUID;

                Entities.New("Movie")
                        .AddProperty("title", typeof(string), IndexType.Unique)
                        .AddProperty("tagline", typeof(string))
                        .AddProperty("released", typeof(int))
                        .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                        .SetKey("Uid", true);
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                Entities["Movie"].SetKey("title", false);
            }
        }

        [Test]
        public void SetNodeKeyFromUniqueConstraint()
        {
            using (ConsoleOutput output = new())
            {
                MovieDataStoreModel_01 model = new () { LogToConsole = true };               

                Assert.Throws<AggregateException>(() => model.Execute(true) );
            }
        }

        [Test]
        public void SetNewNodeKeyIfNodeKeyIsAlreadySet()
        {
            using (ConsoleOutput output = new())
            {
                MovieDataStoreModel_02 model = new() { LogToConsole = true };

                Assert.Throws<InvalidOperationException>(() => model.Execute(true));
            }
        }
    }
}
