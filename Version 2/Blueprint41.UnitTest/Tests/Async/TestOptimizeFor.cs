using System;
using System.Threading.Tasks;

using NUnit.Framework;

using Blueprint41.Core;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Blueprint41.UnitTest.Mocks;

using Datastore.Manipulation.Async;

namespace Blueprint41.UnitTest.Tests.Async
{
    [TestFixture]
    internal class TestOptimizeFor : TestBase
    {
        [Test]
        public async Task TestOptimize()
        {
            Connect<MockModel>(true).Execute(true);

            using (ConsoleOutput output = new ConsoleOutput())
            {
                string? key = null;

                string outputConsole;
                await using (MockModel.BeginTransactionAsync(ReadWriteMode.ReadWrite))
                {
                    Person p1 = new Person
                    {
                        Name = "Martin Sheen",
                    };

                    Person p2 = new Person
                    {
                        Name = "Michael Douglas",
                    };

                    Person p3 = new Person
                    {
                        Name = "Oliver Stone",
                    };

                    Person p4 = new Person
                    {
                        Name = "Rob Reiner",
                    };

                    Movie wallstreet = new Movie
                    {
                        Title = "Wall Street"
                    };

                    Movie tap = new Movie
                    {
                        Title = "The American President"
                    };

                    Movie st = new Movie
                    {
                        Title = "Starwars"
                    };

                    (await p1.ActedInMoviesAsync()).Add(tap);
                    (await p1.ActedInMoviesAsync()).Add(wallstreet);

                    (await p2.ActedInMoviesAsync()).Add(tap);
                    (await p2.ActedInMoviesAsync()).Add(wallstreet);

                    (await p3.DirectedMoviesAsync()).Add(wallstreet);
                    (await p4.DirectedMoviesAsync()).Add(tap);

                    await Transaction.CommitAsync();

                    key = p2.Uid;
                }

                await using (MockModel.BeginTransactionAsync(OptimizeFor.RecursiveSubGraphAccess))
                {
                    Person? p = await Person.LoadAsync(key);
                    Assert.IsNotNull(p);
                    Assert.Zero((await p!.DirectedMoviesAsync()).Count);
                    Assert.Greater((await p.ActedInMoviesAsync()).Count, 0);
                    Assert.IsNotNull((await p.ActedInMoviesAsync())[0]);
                    Assert.Greater((await (await p.ActedInMoviesAsync())[0]!.ActorsAsync()).Count, 0);

                    outputConsole = output.GetOutput();

                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person) WHERE node.Uid = $key RETURN node"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person)-[rel:DIRECTED_BY]->(out:Movie) WHERE node.Uid in ($keys)  RETURN node as Parent, out as Item"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person)-[rel:ACTORS]->(out:Movie) WHERE node.Uid in ($keys)  RETURN node as Parent, out as Item"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Movie)<-[rel:ACTORS]-(out:Person) WHERE node.Uid in ($keys)  RETURN node as Parent, out as Item"));
                }

                await using (MockModel.BeginTransactionAsync(OptimizeFor.PartialSubGraphAccess))
                {
                    Person? p = await Person.LoadAsync(key);
                    Assert.IsNotNull(p);
                    Assert.Zero((await p!.DirectedMoviesAsync()).Count);
                    Assert.Greater((await p.ActedInMoviesAsync()).Count, 0);
                    Assert.IsNotNull((await p.ActedInMoviesAsync())[0]);
                    Assert.Greater((await (await p.ActedInMoviesAsync())[0]!.ActorsAsync()).Count, 0);

                    outputConsole = output.GetOutput();

                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person) WHERE node.Uid = $key RETURN node"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person)-[rel:DIRECTED_BY]->(out:Movie) WHERE node.Uid = $key RETURN out, rel"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Person)-[rel:ACTORS]->(out:Movie) WHERE node.Uid = $key RETURN out, rel"));
                    Assert.IsTrue(outputConsole.Contains(@"MATCH (node:Movie)<-[rel:ACTORS]-(out:Person) WHERE node.Uid = $key RETURN out, rel"));
                }
            }
        }
    }
}
