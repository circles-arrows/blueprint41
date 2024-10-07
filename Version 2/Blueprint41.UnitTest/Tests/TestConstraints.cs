using Blueprint41.UnitTest.Helper;
using NUnit.Framework;
using System;

namespace Blueprint41.UnitTest.Tests
{
    internal class TestConstraints : TestBase    
    {
        private class MovieDataStoreModel_01 : DatastoreModel<MovieDataStoreModel_01>
        {
#if NEO4J
            public override GDMS DatastoreTechnology => GDMS.Neo4j;
#elif MEMGRAPH
        public override GDMS DatastoreTechnology => GDMS.Memgraph;
#endif

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
                MovieDataStoreModel_01 model = new () { LogToConsole = true };               

                Assert.DoesNotThrow(() => model.Execute(true));
            }
        }

        private class MovieDataStoreModel_02 : DatastoreModel<MovieDataStoreModel_02>
        {
#if NEO4J
            public override GDMS DatastoreTechnology => GDMS.Neo4j;
#elif MEMGRAPH
        public override GDMS DatastoreTechnology => GDMS.Memgraph;
#endif

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
                MovieDataStoreModel_02 model = new() { LogToConsole = true };

                Exception ex = Assert.Throws<InvalidOperationException>(() => model.Execute(true));

                Assert.That(ex.InnerException is NotSupportedException);
                Assert.AreEqual("Multiple key not allowed.", ex.InnerException?.Message);
            }
        }
    }
}
