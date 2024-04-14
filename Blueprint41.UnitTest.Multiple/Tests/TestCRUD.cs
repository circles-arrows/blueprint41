using Blueprint41;
using Blueprint41.Core;
using Blueprint41.UnitTest.Multiple.Memgraph.DataStore;
using Blueprint41.UnitTest.Multiple.Neo4j.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neo4jDriver = Blueprint41.Neo4j.Persistence.Driver.v5;
using MemgraphDriver = Blueprint41.Neo4j.Persistence.Driver.Memgraph;

using Neo4jData = Neo4j.Datastore.Manipulation;
using MemgraphData = Memgraph.Datastore.Manipulation;

namespace Blueprint41.UnitTest.Multiple.Tests
{
    public class TestCRUD : TestBase
    {
        [SetUp]
        public void Neo4jDriverSetUp()
        {
            Neo4jModel.CurrentPersistenceProvider = new Neo4jDriver.Neo4jPersistenceProvider("bolt://localhost:7687", "neo4j", "passionite?01", "unittest");

            var neo4jModel = new Neo4jModel();
            neo4jModel.Execute(true);

            Neo4jTearDown();
        }

        [SetUp]
        public void MemgraphDriverSetUp()
        {
            MemgraphModel.CurrentPersistenceProvider = new MemgraphDriver.Neo4jPersistenceProvider("bolt://localhost:7689", string.Empty, string.Empty);

            var memgraphModel = new MemgraphModel();
            memgraphModel.Execute(true);

            MemgraphTearDown();
        }

        [Test]
        public void CreateNode()
        {
            using (Transaction.Begin())
            {
                Neo4jData.Movie matrix = new Neo4jData.Movie()
                {
                    title = "The Matrix",
                    tagline = "Welcome to the Real World",
                    released = 1999
                };

                Transaction.Commit();
            }

            using (Transaction.Begin())
            {
                var movie = Neo4jData.Movie.LoadBytitle("The Matrix");

                Assert.IsNotNull(movie);
                Assert.AreEqual("The Matrix", movie.title);
                Assert.AreEqual("Welcome to the Real World", movie.tagline);
                Assert.AreEqual(1999, movie.released);
            }
        }

        [Test]
        public void CreateNodeBoth()
        {
            CreateNodeOnNeo4j();
            CreateNodeOnMemgraph();
        }

        private void CreateNodeOnMemgraph()
        {
            MemgraphDriverSetUp();

            using (Transaction.Begin())
            {
                MemgraphData.Movie matrix = new MemgraphData.Movie()
                {
                    title = "The Matrix",
                    tagline = "Welcome to the Real World",
                    released = 1999
                };

                Transaction.Commit();
            }

            using (Transaction.Begin())
            {
                var movie = MemgraphData.Movie.LoadBytitle("The Matrix");

                Assert.IsNotNull(movie);
                Assert.AreEqual("The Matrix", movie.title);
                Assert.AreEqual("Welcome to the Real World", movie.tagline);
                Assert.AreEqual(1999, movie.released);
            }

            MemgraphTearDown();
        }

        private void CreateNodeOnNeo4j()
        {
            Neo4jDriverSetUp();

            using (Transaction.Begin())
            {
                Neo4jData.Movie matrix = new Neo4jData.Movie()
                {
                    title = "The Matrix",
                    tagline = "Welcome to the Real World",
                    released = 1999
                };

                Transaction.Commit();
            }

            using (Transaction.Begin())
            {
                var movie = Neo4jData.Movie.LoadBytitle("The Matrix");

                Assert.IsNotNull(movie);
                Assert.AreEqual("The Matrix", movie.title);
                Assert.AreEqual("Welcome to the Real World", movie.tagline);
                Assert.AreEqual(1999, movie.released);
            }

            Neo4jTearDown();
        }
    }
}
