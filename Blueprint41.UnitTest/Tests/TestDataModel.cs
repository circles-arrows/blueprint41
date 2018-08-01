using Blueprint41.Dynamic;
using Blueprint41.UnitTest.Helper;
using Neo4j.Driver.V1;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Blueprint41.UnitTest.Tests
{
    [TestFixture]
    internal class TestDataModel : TestBase
    {
        #region DataModelWithRowVersion

        private class DataModelWithRowVersion : DatastoreModel<DataModelWithRowVersion>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("BaseEntity")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Person", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string));

                Entities.New("Address")
                        .AddProperty("Name", typeof(string))
                        .AddProperty("LastModifiedOn", typeof(DateTime));
            }
        }

        [Test]
        public void EnsureEntitiesHaveRowVersionPropertyOrInherited()
        {
            DatastoreModel model = new DataModelWithRowVersion();
            model.Execute(false);

            Assert.IsTrue(model.Entities["BaseEntity"].Properties["LastModifiedOn"].IsRowVersion);
            Assert.IsTrue(model.Entities["Person"].IsSubsclassOf(model.Entities["BaseEntity"]));
            Assert.IsFalse(model.Entities["Address"].Properties["LastModifiedOn"].IsRowVersion);
        }

        #endregion

        #region DataModelWithInvalidRowVersion
        private class DataModelWithInvalidRowVersion : DatastoreModel<DataModelWithInvalidRowVersion>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("BaseEntity")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Person", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string));

                Entities.New("Address")
                        .AddProperty("Name", typeof(string))
                        .AddProperty("LastModifiedOn", typeof(string))
                        .SetRowVersionField("LastModifiedOn");
            }
        }

        [Test]
        public void EnsureRowVersionPropertyIsDateTime()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DataModelWithInvalidRowVersion();
                model.Execute(false);
            });

            Assert.That(exception.Message, Contains.Substring("You cannot make a non-datetime field the row version."));
        }

        #endregion

        #region DataModelWithMultipleRowVersion
        private class DataModelWithMultipleRowVersion : DatastoreModel<DataModelWithMultipleRowVersion>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("BaseEntity")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .AddProperty("ModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn")
                       .SetRowVersionField("ModifiedOn");

                Entities.New("Person", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string));
            }
        }

        [Test]
        public void EnsureRowVersionIsOnlyOnePerEntity()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DataModelWithMultipleRowVersion();
                model.Execute(false);
            });

            Assert.That(exception.Message, Contains.Substring("Multiple row version fields are not allowed."));
        }

        #endregion

        #region DataModelEntityKey
        private class DataModelKeyNotUnique : DatastoreModel<DataModelKeyNotUnique>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("BaseEntity")
                       .AddProperty("Uid", typeof(string), false)
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");
            }
        }

        private class DataModelKeyIsUnique : DatastoreModel<DataModelKeyIsUnique>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("BaseEntity")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");
            }
        }

        private class DataModelMultipleKey : DatastoreModel<DataModelMultipleKey>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("BaseEntity")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .AddProperty("Uid2", typeof(string), false, IndexType.Unique)
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .SetKey("Uid2", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");
            }
        }

        [Test]
        public void EnsureEntityKeyIsUnique()
        {
            DatastoreModel model;

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                model = new DataModelKeyNotUnique();
                model.Execute(false);
            });

            Assert.That(exception.Message, Contains.Substring("You cannot make a non-unique field the key."));

            exception = Assert.Throws<InvalidOperationException>(() =>
            {
                model = new DataModelMultipleKey();
                model.Execute(false);
            });

            Assert.That(exception.Message, Contains.Substring("Multiple key not allowed."));

            model = new DataModelKeyIsUnique();
            model.Execute(false);

            Property uid = model.Entities["BaseEntity"].Properties["Uid"];
            Assert.IsTrue(uid.IsKey && uid.IndexType == IndexType.Unique);
        }

        #endregion        

        #region DataModelProperties

        private class DataModelEntityWithSameProperties : DatastoreModel<DataModelEntityWithSameProperties>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                Entities.New("Person")
                       .AddProperty("Name", typeof(string))
                       .AddProperty("Name", typeof(string));
            }
        }

        private class DataModelEntityWithSamePropertiesFromBase : DatastoreModel<DataModelEntityWithSamePropertiesFromBase>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                Entities.New("BaseEntity")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .AddProperty("Name", typeof(string))
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Person", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string));
            }
        }

        private class DataModelEntityWithSamePropertiesFromChangedInheritance : DatastoreModel<DataModelEntityWithSamePropertiesFromChangedInheritance>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                Entities.New("BaseEntity")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .AddProperty("Name", typeof(string))
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Person")
                       .AddProperty("Name", typeof(string));
            }

            [Version(0, 0, 1)]
            public void UpdateInheritance()
            {
                // TODO: Bug, Changing inheritance with same properties does not trigger an exception
                Entities["Person"].Refactor.ChangeInheritance(Entities["BaseEntity"]);
            }
        }

        [Test]
        public void EnsurePropertiesAreUnique()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DataModelEntityWithSameProperties();
                model.Execute(false);
            });

            Assert.That(exception.Message, Contains.Substring("Property with the name Name already exists on Entity Person"));

            exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DataModelEntityWithSamePropertiesFromBase();
                model.Execute(false);
            });

            Assert.That(exception.Message, Contains.Substring("Property with the name Name already exists on base class Entity BaseEntity"));

            exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DataModelEntityWithSamePropertiesFromChangedInheritance();
                model.Execute(false);
            });

            // TODO: Bug, Changing inheritance with same properties does not trigger an exception
            Assert.That(exception.Message, Contains.Substring("Property with the name Name already exists on base class Entity BaseEntity"));
        }

        #endregion

        #region IRefactorEntity CreateNode

        private class DataModelWithStaticData : DatastoreModel<DataModelWithStaticData>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("AccountType")
                   .Summary("The type of an Account")
                   .HasStaticData(true)
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string), false, IndexType.Unique)
                   .SetFullTextProperty("Name");

                Entities["AccountType"].Refactor.CreateNode(new { Uid = "6", Name = "Account" });
                Entities["AccountType"].Refactor.CreateNode(new { Uid = "81", Name = "MTMSAccount" });
                Entities["AccountType"].Refactor.CreateNode(new { Uid = "89", Name = "FinancialAccount" });
                Entities["AccountType"].Refactor.CreateNode(new { Uid = "90", Name = "BillingAccount" });
                Entities["AccountType"].Refactor.CreateNode(new { Uid = "91", Name = "AxaptaAccount" });
                Entities["AccountType"].Refactor.CreateNode(new { Uid = "349", Name = "Aircraft" });
                Entities["AccountType"].Refactor.CreateNode(new { Uid = "350", Name = "Installation" });
                Entities["AccountType"].Refactor.CreateNode(new { Uid = "351", Name = "SiteTrackingObject" });
                Entities["AccountType"].Refactor.CreateNode(new { Uid = "352", Name = "StaticSite" });
                Entities["AccountType"].Refactor.CreateNode(new { Uid = "353", Name = "Vessel" });

                Entities.New("ContactStatus")
                    .Summary("The ContactStatus is describing the status of a Contact")
                    .Example("Active, Inactive, Blacklisted")
                    //.HasStaticData(true)
                    .AddProperty("OrderBy", typeof(string))
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .SetKey("Uid", true)
                    .AddProperty("Name", typeof(string), false, IndexType.Unique);

                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "1", Name = "Active", OrderBy = "1" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "2", Name = "Inactive", OrderBy = "5" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "3", Name = "Blacklisted", OrderBy = "10" });

                Entities.New("ReferenceType")
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .SetKey("Uid", true)
                    .AddProperty("Name", typeof(string), false)
                    .AddProperty("Fields", typeof(List<string>));

                Entities["ReferenceType"].Refactor.CreateNode(new { Uid = "1", Name = "Opportunity", Fields = new List<string>() { "Field1" } });
            }
        }

        [Test]
        public void EnsureStaticDataAreCreated()
        {
            DatastoreModel model = new DataModelWithStaticData();
            model.Execute(false);

            Entity accountType = model.Entities["AccountType"];

            Assert.IsTrue(accountType.ContainsStaticData);
            Assert.IsNotNull(accountType.StaticData);
            Assert.IsTrue(accountType.StaticData.Count == 10);

            // TODO: Should it create a static data when HasStaticData == false ??
            Entity contactStatus = model.Entities["ContactStatus"];

            Assert.IsTrue(contactStatus.ContainsStaticData);
            Assert.IsNotNull(contactStatus.StaticData);
            Assert.IsTrue(contactStatus.StaticData.Count == 3);

            Entity referenceType = model.Entities["ReferenceType"];

            dynamic node = referenceType.Refactor.MatchNode("1");
            Assert.IsInstanceOf<DynamicEntity>(node);

            DynamicEntity nodeType = node as DynamicEntity;
            IReadOnlyDictionary<string, object> values = nodeType.GetDynamicEntityValues();

            Assert.AreEqual(values["Uid"], "1");
            Assert.AreEqual(values["Name"], "Opportunity");
            Assert.IsInstanceOf<List<string>>(values["Fields"]);
        }

        private class DataModelWithStaticDataKeyDuplicate : DatastoreModel<DataModelWithStaticDataKeyDuplicate>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                Entities.New("ContactStatus")
                    .HasStaticData(true)
                    .AddProperty("OrderBy", typeof(string))
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .SetKey("Uid", true)
                    .AddProperty("Name", typeof(string), false, IndexType.Unique);

                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "1", Name = "Active", OrderBy = "1" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "2", Name = "Inactive", OrderBy = "5" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "3", Name = "Blacklisted", OrderBy = "10" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "3", Name = "Unknown", OrderBy = "11" });
            }
        }

        [Test]
        public void EnsureStaticDataAreUnique()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DataModelWithStaticDataKeyDuplicate();
                model.Execute(false);
            });

            Assert.That(exception.Message, Contains.Substring("A static entity with the same key already exists."));
        }

        private class DataModelWithStaticDataWithoutKey : DatastoreModel<DataModelWithStaticDataWithoutKey>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                Entities.New("ContactStatus")
                    .HasStaticData(true)
                    .AddProperty("OrderBy", typeof(string))
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .AddProperty("Name", typeof(string), false, IndexType.Unique);

                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "1", Name = "Active", OrderBy = "1" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "2", Name = "Inactive", OrderBy = "5" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "3", Name = "Blacklisted", OrderBy = "10" });
            }
        }

        [Test]
        public void EnsureStaticDataHaveKeys()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DataModelWithStaticDataWithoutKey();
                model.Execute(false);
            });

            // Todo: Exception message should be more specific rather than a null reference error
            Assert.That(exception.Message, Contains.Substring("No key exists of entity 'ContactStatus'"));
        }

        private class DataModelWithStaticDataMissingProperty : DatastoreModel<DataModelWithStaticDataMissingProperty>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                Entities.New("ContactStatus")
                    .HasStaticData(true)
                    .AddProperty("OrderBy", typeof(string))
                    .AddProperty("Uid", typeof(string), false)
                    .SetKey("Uid", true)
                    .AddProperty("Label", typeof(string), false, IndexType.Unique);

                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "1", Name = "Active", OrderBy = "1" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "2", Name = "Inactive", OrderBy = "5" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "3", Name = "Blacklisted", OrderBy = "10" });
            }
        }

        [Test]
        public void EnsureStaticDataHaveCorrectProperties()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DataModelWithStaticDataMissingProperty();
                model.Execute(false);
            });

            Assert.That(exception.Message, Contains.Substring("The property 'Name' is not contained within entity 'ContactStatus'."));
        }

        #endregion

        #region IRefactorEntity DeleteNode

        private class DataModelStaticDataWithDeleteNode : DatastoreModel<DataModelStaticDataWithDeleteNode>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("ContactStatus")
                    .HasStaticData(true)
                    .AddProperty("OrderBy", typeof(string))
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .SetKey("Uid", true)
                    .AddProperty("Name", typeof(string), false, IndexType.Unique);

                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "1", Name = "Active", OrderBy = "1" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "2", Name = "Inactive", OrderBy = "5" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "3", Name = "Blacklisted", OrderBy = "10" });
            }

            [Version(0, 0, 1)]
            public void DeleteNode1()
            {
                Entities["ContactStatus"].Refactor.DeleteNode("1");
            }
        }

        [Test]
        public void EnsureStaticDataIsDeleted()
        {
            // TODO: Delete node has not yet been implemented
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DataModelStaticDataWithDeleteNode();
                model.Execute(true);
            });

            Assert.That(exception.Message, Contains.Substring("The method or operation is not implemented."));
        }

        #endregion

        #region IRefactorEntity Deprecate
        private class DataModelWithDeprecatedEntities : DatastoreModel<DataModelWithDeprecatedEntities>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("BaseEntity")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Person", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string));

                Entities.New("Address")
                        .AddProperty("Name", typeof(string))
                        .AddProperty("LastModifiedOn", typeof(DateTime));
            }

            [Version(0, 0, 1)]
            public void DeprecatePerson()
            {
                Entities["Person"].Refactor.Deprecate();
            }
        }

        [Test]
        public void IRefactorEntityDeprecate()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                DataModelWithDeprecatedEntities model = new DataModelWithDeprecatedEntities();
                model.Execute(true);
                Assert.Throws<ArgumentOutOfRangeException>(() => Assert.IsNotNull(model.Entities["Person"]));
                Assert.That(output.GetOuput(), Contains.Substring("MATCH (n:Person) WITH n LIMIT 10000 DETACH DELETE n"));
            }
        }

        #endregion

        #region IRefactorEntity MatchNode

        [Test]
        public void EnsureMatchNodeReturnsCorrectNode()
        {
            DatastoreModel model = new DataModelWithStaticData();
            model.Execute(true);

            Entity accountType = model.Entities["AccountType"];
            dynamic account = accountType.Refactor.MatchNode("6");

            Assert.IsInstanceOf<DynamicEntity>(account);

            DynamicEntity nodeType = account as DynamicEntity;
            IReadOnlyDictionary<string, object> values = nodeType.GetDynamicEntityValues();
            Assert.AreEqual(values["Uid"], "6");
            Assert.AreEqual(values["Name"], "Account");

            Assert.Throws<ArgumentNullException>(() => accountType.Refactor.MatchNode(null));
            Assert.Throws<InvalidCastException>(() => accountType.Refactor.MatchNode(6));
            Assert.Throws<ArgumentOutOfRangeException>(() => accountType.Refactor.MatchNode("7"));
        }
        #endregion

        #region IRefactorEntity Rename

        private class DatastoreEntityRefactor : DatastoreModel<DatastoreEntityRefactor>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("AccountType")
                   .Summary("The type of an Account")
                   .HasStaticData(true)
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string), false, IndexType.Unique)
                   .SetFullTextProperty("Name");

                Entities["AccountType"].Refactor.CreateNode(new { Uid = "6", Name = "Account" });
            }

            [Version(0, 0, 1)]
            public void Update()
            {
                Entities["AccountType"].Refactor.Rename("NewAccountType");

                Entities["NewAccountType"].AddProperty("Unique", typeof(string), IndexType.Unique);
                Entities["NewAccountType"].AddProperty("Indexed", typeof(string), IndexType.Indexed);

                Assert.Throws<ArgumentOutOfRangeException>(() => { Entity oldType = Entities["AccountType"]; });

                // TODO: Bug, was able to rename entity to an empty string
                Assert.Throws<Exception>(() => Entities["NewAccountType"].Refactor.Rename(""));
            }
        }

        [Test]
        public void IRefactorEntityRename()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                DatastoreModel model = new DatastoreEntityRefactor();
                model.Execute(true);
            }
        }
        #endregion

        #region IRefactorEntity ApplyConstraints

        private class DatastoreEntityRefactorConstraints : DatastoreModel<DatastoreEntityRefactorConstraints>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("AccountType")
                   .Summary("The type of an Account")
                   .HasStaticData(true)
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string), false, IndexType.Unique)
                   .SetFullTextProperty("Name");

                Entities["AccountType"].Refactor.CreateNode(new { Uid = "6", Name = "Account" });
            }

            [Version(0, 0, 1)]
            public void Update()
            {
                Entities["AccountType"].AddProperty("Unique", typeof(string), IndexType.Unique);
                Entities["AccountType"].AddProperty("Indexed", typeof(string), IndexType.Indexed);

                Entities["AccountType"].Refactor.ApplyConstraints();
            }
        }

        [Test]
        public void IRefactorEntityApplyConstraints()
        {
            // TODO: This does not call ApplyConstraints after adding properties. Had to call
            // Entities["AccountType"].Refactor.ApplyConstraints() to make it work.
            // Should this be automatic set by the updatescript after adding properties?

            using (ConsoleOutput output = new ConsoleOutput())
            {
                DatastoreEntityRefactorConstraints model = new DatastoreEntityRefactorConstraints();
                model.Execute(true);

                Assert.That(output.GetOuput(), Contains.Substring("CREATE INDEX ON :AccountType(Indexed)"));
                Assert.That(output.GetOuput(), Contains.Substring("CREATE CONSTRAINT ON (node:AccountType) ASSERT node.Unique IS UNIQUE"));
            }
        }
        #endregion

        #region IRefactorEntity ChangeInheritance()

        private class DatastoreEntityBaseWithoutParent : DatastoreModel<DatastoreEntityBaseWithoutParent>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("BaseOne")
                  .Abstract(true)
                  .Virtual(true)
                  .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                  .SetKey("Uid", true)
                  .AddProperty("LastModifiedOn", typeof(DateTime))
                  .SetRowVersionField("LastModifiedOn");

                Entities.New("BaseTwo")
                  .Abstract(true)
                  .Virtual(true)
                  .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                  .SetKey("Uid", true)
                  .AddProperty("LastModifiedOn", typeof(DateTime))
                  .SetRowVersionField("LastModifiedOn"); ;

                Entities.New("Child", Entities["BaseOne"])
                   .AddProperty("Name", typeof(string), false);
            }

            [Version(0, 0, 1)]
            public void Update()
            {
                Entities["Child"].Refactor.ChangeInheritance(Entities["BaseTwo"]);
            }
        }

        private class DatastoreEntityBaseWithParent : DatastoreModel<DatastoreEntityBaseWithParent>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("BaseOne")
                  .Abstract(true)
                  .Virtual(true)
                  .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                  .SetKey("Uid", true)
                  .AddProperty("LastModifiedOn", typeof(DateTime))
                  .SetRowVersionField("LastModifiedOn");

                Entities.New("BaseTwo", Entities["BaseOne"]);

                Entities.New("Child", Entities["BaseOne"])
                   .AddProperty("Name", typeof(string), false);
            }

            [Version(0, 0, 1)]
            public void Update()
            {
                Entities["Child"].Refactor.ChangeInheritance(Entities["BaseTwo"]);
            }
        }

        [Test]
        public void IRefactorChangeInheritance()
        {
            DatastoreModel model;

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                model = new DatastoreEntityBaseWithoutParent();
                model.Execute(true);

            });

            Assert.That(exception.Message, Contains.Substring("Specified method is not supported."));

            model = new DatastoreEntityBaseWithParent();
            model.Execute(true);
            Assert.AreEqual(model.Entities["Child"].Inherits.Name, "BaseTwo");
        }

        #endregion

        #region IRefactorEntity ResetFunctionalId
        private class DatastoreEntityResetFunctionalId : DatastoreModel<DatastoreEntityResetFunctionalId>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);
                FunctionalId accountId = FunctionalIds.New("Acct", "A_", IdFormat.Hash, 0);

                Entities.New("AccountType", accountId)
                   .Summary("The type of an Account")
                   .HasStaticData(true)
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string), false, IndexType.Unique)
                   .SetFullTextProperty("Name");

                Entities["AccountType"].Refactor.CreateNode(new { Uid = "6", Name = "Account" });

                Assert.IsNotNull(Entities["AccountType"].FunctionalId);
                Assert.AreEqual(Entities["AccountType"].FunctionalId, accountId);

            }

            [Version(0, 0, 1)]
            public void Update()
            {
                Entities["AccountType"].Refactor.ResetFunctionalId();
            }
        }

        [Test]
        public void IRefactorEntityResetFunctionalId()
        {
            DatastoreModel model = new DatastoreEntityResetFunctionalId();
            model.Execute(true);

            Assert.AreEqual(model.Entities["AccountType"].FunctionalId, model.FunctionalIds.Default);
        }
        #endregion

        #region IRefactorEntity CopyValue

        private class DatastoreEntityCopyValue : DatastoreModel<DatastoreEntityCopyValue>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("Account")                   
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string), false, IndexType.Unique)
                   .AddProperty("CopyName", typeof(string))
                   .SetFullTextProperty("Name");

                Entities.New("AccountType")
                   .Summary("The type of an Account")
                   .HasStaticData(true)
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string), false, IndexType.Unique)
                   .AddProperty("CopyName", typeof(string))
                   .SetFullTextProperty("Name");

                Entities["AccountType"].Refactor.CreateNode(new { Uid = "6", Name = "Account" });
            }

            [Version(0, 0, 1)]
            public void Update()
            {
                Entities["Account"].Refactor.CopyValue("Name", "CopyName");
                Assert.Throws<NotSupportedException>(() => Entities["Account"].Refactor.CopyValue("Name", "NonExistentProperty"));
                Assert.Throws<NotSupportedException>(() => Entities["Account"].Refactor.CopyValue("NonExistentProperty", "Name"));
                Assert.Throws<NotSupportedException>(() => Entities["AccountType"].Refactor.CopyValue("Name", "CopyName"));
            }
        }

        [Test]
        public void IRefactorCopyValue()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                DatastoreModel model = new DatastoreEntityCopyValue();
                model.Execute(true);
                Assert.IsTrue(Regex.IsMatch(output.GetOuput(), @"(MATCH \(node:Account\))[^a-zA-Z,0-9]*(WHERE NOT EXISTS\(node\.CopyName\) OR node.Name <> node.CopyName)[^a-zA-Z,0-9]*(WITH node limit 10000)[^a-zA-Z,0-9]*(SET node\.CopyName = node\.Name)"));
            }                
        }

        #endregion

        #region IRefactorEntity SetDefaultValue

        private class DatastoreEntitySetDefaultValue : DatastoreModel<DatastoreEntitySetDefaultValue>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("Account")
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string), false)
                   .SetFullTextProperty("Name");
            }

            [Version(0, 0, 1)]
            public void Update()
            {
                Entities["Account"].Refactor.SetDefaultValue((a) => a.Name = "First Account");

                ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                {
                    Entities["Account"].Refactor.SetDefaultValue((a) => a.Label = "First Account");
                });

                Assert.That(exception.Message, Contains.Substring("The field 'Label' was not present on entity 'Account'."));
            }
        }

        [Test]
        public void IRefactorEntitySetDefaultValue()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                DatastoreModel model = new DatastoreEntitySetDefaultValue();
                model.Execute(true);

                string query = @"MATCH (node:Account) WHERE NOT EXISTS(node.Name) WITH node LIMIT 10000 SET node.Name = ""First Account""";
                Assert.That(output.GetOuput(), Contains.Substring(query));
            }
        }

        #endregion

        #region IRefactorEntity SetFunctionalId
        private class DatastoreEntitySetFunctionalId : DatastoreModel<DatastoreEntitySetFunctionalId>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("Account")
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string), false)
                   .SetFullTextProperty("Name");

                Assert.AreEqual(Entities["Account"].FunctionalId, FunctionalIds.Default);
            }

            [Version(0, 0, 1)]
            public void UpdateFunctionalId()
            {
                FunctionalId account = FunctionalIds.New("Account", "A_", IdFormat.Hash);
                Entities["Account"].Refactor.SetFunctionalId(account);
                Assert.AreEqual(Entities["Account"].FunctionalId, account);
            }

            [Version(0, 0, 2)]
            public void ChnageFunctionalId()
            {
                FunctionalId toChangeFunctionalId = FunctionalIds.New("ChangeAccount", "CA_", IdFormat.Hash);

                Entities["Account"].Refactor.SetFunctionalId(toChangeFunctionalId, ApplyAlgorithm.ReapplyAll);
                Assert.AreEqual(Entities["Account"].FunctionalId, toChangeFunctionalId);
            }
        }

        private class DatastoreEntityInheritedFunctionalId : DatastoreModel<DatastoreEntitySetFunctionalId>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);
                FunctionalId account = FunctionalIds.New("Account", "A_", IdFormat.Hash);

                Entities.New("BaseAccount", account)
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid", true);

                Entities.New("Account", Entities["BaseAccount"])
                   .AddProperty("Name", typeof(string), false)
                   .SetFullTextProperty("Name");

                Assert.AreEqual(Entities["Account"].FunctionalId, account);
            }

            [Version(0, 0, 1)]
            public void UpdateFunctionalId()
            {
                FunctionalId toChangeFunctionalId = FunctionalIds.New("ChangeAccount", "CA_", IdFormat.Hash);
                Entities["Account"].Refactor.SetFunctionalId(toChangeFunctionalId);
            }
        }

        [Test]
        public void IRefactorEntitySetFunctionalId()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                DatastoreModel model = new DatastoreEntitySetFunctionalId();
                model.Execute(true);

                Assert.That(output.GetOuput(), Contains.Substring("MATCH (node:Account) where node.Uid STARTS WITH 'A_' AND Length(node.Uid) = 8 CALL blueprint41.hashing.decode(replace(node.Uid, 'A_', '')) YIELD value as decoded RETURN  case Max(decoded) WHEN NULL THEN 0 ELSE Max(decoded) END as MaxId"));
                Assert.That(output.GetOuput(), Contains.Substring("MATCH (node:Account) where node.Uid STARTS WITH 'CA_' AND Length(node.Uid) = 9 CALL blueprint41.hashing.decode(replace(node.Uid, 'CA_', '')) YIELD value as decoded RETURN  case Max(decoded) WHEN NULL THEN 0 ELSE Max(decoded) END as MaxId"));
            }

            TearDown();

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DatastoreEntityInheritedFunctionalId();
                model.Execute(true);
            });

            Assert.That(exception.Message, Contains.Substring("The entity 'Account' already inherited a functional id 'A_ (Account)', you cannot assign another one."));
        }
        #endregion
    }
}
