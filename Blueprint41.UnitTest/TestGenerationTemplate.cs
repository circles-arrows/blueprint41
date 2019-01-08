using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;
using Blueprint41.UnitTest.Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blueprint41.UnitTest
{
    [TestFixture]
    internal class TestGenerationTemplate : Neo4jBase
    {
        class GenerationStoreModel : DatastoreModel<GenerationStoreModel>
        {
            protected override void SubscribeEventHandlers() { }

            [Version(0, 0, 0)]
            public void Script_0_0_0()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("GeneratorBase")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("GeneratorEntity", Entities["GeneratorBase"])
                       .AddProperty("Name", typeof(string));

                Entities.New("GeneratorEntityTwo", Entities["GeneratorBase"])
                       .AddProperty("Name", typeof(string));

                Relations.New(Entities["GeneratorEntity"], Entities["GeneratorEntityTwo"], "GENERATOR_REL", "REL")
                    .SetInProperty("Generators", PropertyType.Lookup);
            }
        }

        public override Type Datastoremodel => typeof(GenerationStoreModel);

        [Test]
        public void EnsureFilesAreGenerated()
        {
            string projectFolder = Environment.CurrentDirectory + "\\..\\..\\..\\";
            GeneratorSettings settings = new GeneratorSettings(projectFolder);
            settings.EntitiesFolder = "GeneratorEntities";
            settings.NodesFolder = "GeneratorNodes";
            settings.RelationshipsFolder = "GeneratorRelationships";

            GeneratorResult result = Generator.Execute<GenerationStoreModel>(settings);

            string entitiesFolder = Path.Combine(projectFolder, settings.EntitiesFolder);
            string nodesFolder = Path.Combine(projectFolder, settings.NodesFolder);
            string relationshipFolder = Path.Combine(projectFolder, settings.RelationshipsFolder);

            // Entities
            Assert.True(File.Exists(Path.Combine(entitiesFolder, "GeneratorBase.cs")));
            Assert.True(File.Exists(Path.Combine(entitiesFolder, "GeneratorEntity.cs")));
            Assert.True(File.Exists(Path.Combine(entitiesFolder, "GeneratorEntityTwo.cs")));

            // Nodes
            Assert.True(File.Exists(Path.Combine(nodesFolder, "GeneratorBaseNode.cs")));
            Assert.True(File.Exists(Path.Combine(nodesFolder, "GeneratorEntityNode.cs")));
            Assert.True(File.Exists(Path.Combine(nodesFolder, "GeneratorEntityTwoNode.cs")));

            //Relationship
            Assert.True(File.Exists(Path.Combine(relationshipFolder, "GENERATOR_REL.cs")));

            Directory.Delete(entitiesFolder, true);
            Directory.Delete(nodesFolder, true);
            Directory.Delete(relationshipFolder, true);
        }
    }
}
