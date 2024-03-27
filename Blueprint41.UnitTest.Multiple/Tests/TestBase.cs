namespace Blueprint41.UnitTest.Multiple.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        [SetUp]
        public void Setup()
        {
            TearDown();
        }

        [TearDown]
        public void TearDown()
        {
            using (Transaction.Begin())
            {
                string reset = "Match (n) detach delete n";
                Transaction.RunningTransaction.Run(reset);

                Transaction.Commit();
            }

            //Neo4jTearDown();

            //MemgraphTearDown();
        }

        internal static void Neo4jTearDown()
        {
            using (Transaction.Begin())
            {
                string clearSchema = "CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *";
                Transaction.RunningTransaction.Run(clearSchema);
                Transaction.Commit();
            }
        }

        internal static void MemgraphTearDown()
        {
            using (Session.Begin())
            {
                string clearSchema = "CALL schema.assert({},{}, {}, true) YIELD label, key RETURN *";
                Session.RunningSession.Run(clearSchema);
            }
        }
    }
}
