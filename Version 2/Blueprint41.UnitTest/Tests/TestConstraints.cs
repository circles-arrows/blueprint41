using Blueprint41.UnitTest.Helper;
using NUnit.Framework;
using System;

namespace Blueprint41.UnitTest.Tests
{
    internal class TestConstraints : TestBase
    {
        private class MovieDataStoreModel_01 : DatastoreModel<MovieDataStoreModel_01>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                Entities["Movie"].SetKey("Uid");
            }
        }

        [Test]
        public void SetNodeKeyFromUniqueConstraint()
        {
            using (ConsoleOutput output = new())
            {
                Assert.DoesNotThrow(() => Connect<MovieDataStoreModel_01>(true, true));
            }
        }

        private class MovieDataStoreModel_02 : DatastoreModel<MovieDataStoreModel_02>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                        .SetKey("Uid");
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                Entities["Movie"].SetKey("title");
            }
        }

        [Test]
        public void SetNewNodeKeyIfNodeKeyIsAlreadySet()
        {
            using (ConsoleOutput output = new())
            {
                Exception ex = Assert.Throws<InvalidOperationException>(() => Connect<MovieDataStoreModel_02>(true, true));

                Assert.That(ex.InnerException is NotSupportedException);
                Assert.AreEqual("Multiple key not allowed.", ex.InnerException?.Message);
            }
        }
    }
}
