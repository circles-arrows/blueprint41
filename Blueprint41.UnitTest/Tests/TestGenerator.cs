using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Blueprint41.UnitTest.Tests
{
    [TestFixture]
    internal class TestGenerator : TestBase
    {
        [Test]
        public void EnsureGeneratorSettingsIsRequired()
        {
            Assert.Throws<ArgumentNullException>(() => Generator.Execute<MockModel>());
        }

        [Test]
        public void EnsureFilesAreGenerated()
        {
            string projectFolder = Environment.CurrentDirectory + "\\..\\..\\..\\";
            GeneratorSettings settings = new GeneratorSettings(projectFolder);
            GeneratorResult result = GenerateModel<MockModel>(settings);

            FileExists(result.EntityResult, Path.Combine(projectFolder, settings.EntitiesFolder));
            FileExists(result.RelationshipResult, Path.Combine(projectFolder, settings.RelationshipsFolder));
            FileExists(result.NodeResult, Path.Combine(projectFolder, settings.NodesFolder));

            DeleteGeneratedFiles(settings, projectFolder);
        }

        [Test]
        public void EnsureFilesAreNotGeneratedWhenDeprecated()
        {
            string projectFolder = Environment.CurrentDirectory + "\\..\\..\\..\\";
            GeneratorSettings settings = new GeneratorSettings(projectFolder);
            GeneratorResult result = GenerateModel<MockModelWithDeprecate>(settings);

            // The person entity is deprecated so it should be included in the entity result
            bool exist = result.EntityResult.Select(x => x.Key).SingleOrDefault(x => x == "Person") != null;
            Assert.IsFalse(exist);

            string entityPath = Path.Combine(Path.Combine(projectFolder, settings.EntitiesFolder), "Person.cs");
            Assert.IsFalse(File.Exists(entityPath));

            DeleteGeneratedFiles(settings, projectFolder);
        }

        private GeneratorResult GenerateModel<T>(GeneratorSettings settings) where T : DatastoreModel<T>, new()
        {
            return Generator.Execute<T>(settings);
        }

        private void DeleteDirAndFiles(string dirPath)
        {
            foreach (string file in Directory.EnumerateFiles(dirPath))
                File.Delete(file);

            if (Directory.GetFiles(dirPath).Length == 0)
                Directory.Delete(dirPath);
        }

        private void DeleteGeneratedFiles(GeneratorSettings settings, string projectFolder)
        {
            DeleteDirAndFiles(Path.Combine(projectFolder, settings.EntitiesFolder));
            DeleteDirAndFiles(Path.Combine(projectFolder, settings.NodesFolder));
            DeleteDirAndFiles(Path.Combine(projectFolder, settings.RelationshipsFolder));
        }

        private void FileExists(Dictionary<string, string> dictionary, string path)
        {
            foreach (KeyValuePair<string, string> item in dictionary)
            {
                string entityPath = Path.Combine(path, item.Key + ".cs");
                Assert.IsTrue(File.Exists(entityPath));
            }
        }
    }
}
