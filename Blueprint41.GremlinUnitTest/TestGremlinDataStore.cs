using Blueprint41.DatastoreTemplates;
using Blueprint41.GremlinUnitTest.Misc;
using NUnit.Framework;
using System;
using System.IO;

namespace Blueprint41.GremlinUnitTest
{
    

    [TestFixture]
    internal class TestGremlinDataStore : GremlinBase
    {
        public override Type Datastoremodel => typeof(GremlinStore);

        [SetUp]
        [Order(2)]
        public void GenerateGremlinEntityFiles()
        {
            string projectFolder = AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\";
            GeneratorSettings settings = new GeneratorSettings(projectFolder);

            string entitiesFolder = Path.Combine(projectFolder, settings.EntitiesFolder);
            string nodesFolder = Path.Combine(projectFolder, settings.NodesFolder);
            string relationshipFolder = Path.Combine(projectFolder, settings.RelationshipsFolder);

            if (Directory.Exists(entitiesFolder) && Directory.Exists(nodesFolder) && Directory.Exists(relationshipFolder))
                return;

            GeneratorResult result = Generator.Execute<GremlinStore>(settings);

            // Entities
            Assert.True(File.Exists(Path.Combine(entitiesFolder, "Film.cs")));
            Assert.True(File.Exists(Path.Combine(entitiesFolder, "Person.cs")));

            // Nodes
            Assert.True(File.Exists(Path.Combine(nodesFolder, "FilmNode.cs")));
            Assert.True(File.Exists(Path.Combine(nodesFolder, "PersonNode.cs")));

            //Relationship
            Assert.True(File.Exists(Path.Combine(relationshipFolder, "PERSON_ACTED_IN_FILM.cs")));
            Assert.True(File.Exists(Path.Combine(relationshipFolder, "PERSON_DIRECTED_FILM.cs")));
            Assert.True(File.Exists(Path.Combine(relationshipFolder, "PERSON_PRODUCED_FILM.cs")));
            Assert.True(File.Exists(Path.Combine(relationshipFolder, "PERSON_WROTE_FILM.cs")));
        }

        [Test]
        public void CheckGremlinGraphFeatures()
        {
            GremlinStore store = new GremlinStore();
            store.Execute(true);
        }
    }
}
