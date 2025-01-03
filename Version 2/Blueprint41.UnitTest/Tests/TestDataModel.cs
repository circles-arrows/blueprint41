﻿using Blueprint41.Dynamic;
using Blueprint41.Refactoring;
using Blueprint41.UnitTest.DataStore;
using Blueprint41.UnitTest.Helper;
using Neo4j.Driver;
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
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("BaseEntityWithRowVersion")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid")
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("PersonInheritedBase", Entities["BaseEntityWithRowVersion"])
                       .AddProperty("Name", typeof(string));

                Entities.New("AddressWithNoParent")
                        .AddProperty("Name", typeof(string))
                        .AddProperty("LastModifiedOn", typeof(DateTime));
            }
        }

        [Test]
        public void EnsureEntitiesHaveRowVersionPropertyOrInherited()
        {
            DatastoreModel model = Connect<DataModelWithRowVersion>(false);

            Assert.IsTrue(model.Entities["BaseEntityWithRowVersion"].Properties["LastModifiedOn"].IsRowVersion);
            Assert.IsTrue(model.Entities["PersonInheritedBase"].IsSubsclassOf(model.Entities["BaseEntityWithRowVersion"]));
            Assert.IsFalse(model.Entities["AddressWithNoParent"].Properties["LastModifiedOn"].IsRowVersion);
        }

        #endregion

        #region DataModelWithInvalidRowVersion
        private class DataModelWithInvalidRowVersion : DatastoreModel<DataModelWithInvalidRowVersion>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                       .SetKey("Uid")
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
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Connect<DataModelWithInvalidRowVersion>(false));

            Assert.That(exception.Message, Contains.Substring("You cannot make a non-datetime field the row version."));
        }

        #endregion

        #region DataModelWithMultipleRowVersion
        private class DataModelWithMultipleRowVersion : DatastoreModel<DataModelWithMultipleRowVersion>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                       .SetKey("Uid")
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
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Connect<DataModelWithMultipleRowVersion>(false));

            Assert.That(exception.Message, Contains.Substring("Multiple row version fields are not allowed."));
        }

        #endregion

        #region DataModelEntityKey
        private class DataModelKeyNotUnique : DatastoreModel<DataModelKeyNotUnique>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                       .SetKey("Uid")
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");
            }
        }

        private class DataModelKeyIsUnique : DatastoreModel<DataModelKeyIsUnique>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                       .SetKey("Uid")
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Assert.IsTrue(Entities["BaseEntity"].Properties["Uid"].IsKey);
                Assert.IsTrue(Entities["BaseEntity"].Properties["Uid"].IndexType == IndexType.Unique);
            }
        }

        private class DataModelMultipleKey : DatastoreModel<DataModelMultipleKey>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                       .SetKey("Uid")
                       .SetKey("Uid2")
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");
            }
        }

        [Test]
        public void EnsureEntityKeyIsUnique()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Connect<DataModelKeyNotUnique>(false));

            Assert.That(exception.Message, Contains.Substring("You cannot make a non-unique field the key."));

            exception = Assert.Throws<InvalidOperationException>(() => Connect<DataModelMultipleKey>(false));

            Assert.That(exception.Message, Contains.Substring("Multiple key not allowed."));

            var model = Connect<DataModelKeyIsUnique>(false);
            model.Execute(false);
        }

        #endregion        

        #region DataModelProperties
        private class DataModelEntityWithSameProperties : DatastoreModel<DataModelEntityWithSameProperties>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                       .SetKey("Uid")
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Person", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string));
            }
        }

        private class DataModelEntityWithSamePropertiesFromChangedInheritance : DatastoreModel<DataModelEntityWithSamePropertiesFromChangedInheritance>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                    .SetKey("Uid")
                    .AddProperty("LastModifiedOn", typeof(DateTime))
                    .SetRowVersionField("LastModifiedOn");

                Entities.New("Person")
                    .AddProperty("Name", typeof(string));

                Entities.New("Continent", Entities["BaseEntity"])
                    .AddProperty("Location", typeof(string));

                Assert.AreEqual(Entities["Continent"].Inherits, Entities["BaseEntity"]);
            }

            [Version(0, 0, 1)]
            public void UpdateInheritance()
            {
                // TODO: Bug, Changing inheritance with same properties does not trigger an exception
                Entities["Person"].Refactor.ChangeInheritance(Entities["BaseEntity"]);
                Assert.AreEqual(Entities["Person"].Inherits, Entities["BaseEntity"]);
            }
        }

        [Test]
        public void EnsurePropertiesAreUnique()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Connect<DataModelEntityWithSameProperties>(false));
            Assert.That(exception.Message, Contains.Substring("Property with the name Name already exists on Entity Person"));

            exception = Assert.Throws<InvalidOperationException>(() => Connect<DataModelEntityWithSamePropertiesFromBase>(false));
            Assert.That(exception.Message, Contains.Substring("Property with the name Name already exists on base class Entity BaseEntity"));

            exception = Assert.Throws<InvalidOperationException>(() => Connect<DataModelEntityWithSamePropertiesFromChangedInheritance>(false));
            Assert.That(exception.Message, Contains.Substring("Property with the name Name already exists on base class Entity BaseEntity"));
        }
        #endregion

        #region IRefactorEntity CreateNode

        private class DataModelWithStaticData : DatastoreModel<DataModelWithStaticData>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                   .SetKey("Uid")
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
                    .SetKey("Uid")
                    .AddProperty("Name", typeof(string), false, IndexType.Unique);

                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "1", Name = "Active", OrderBy = "1" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "2", Name = "Inactive", OrderBy = "5" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "3", Name = "Blacklisted", OrderBy = "10" });

                Entities.New("ReferenceType")
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .SetKey("Uid")
                    .AddProperty("Name", typeof(string), false)
                    .AddProperty("Fields", typeof(List<string>));

                Entities["ReferenceType"].Refactor.CreateNode(new { Uid = "1", Name = "Opportunity", Fields = new List<string>() { "Field1" } });
            }
        }

        [Test]
        public void EnsureStaticDataAreCreated()
        {
            var model = Connect<DataModelWithStaticData>(false);

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

            dynamic? node = referenceType.Refactor.MatchNode("1");
            Assert.IsInstanceOf<DynamicEntity>(node);

            DynamicEntity? nodeType = node as DynamicEntity;
            IReadOnlyDictionary<string, object?>? values = nodeType?.GetDynamicEntityValues();

            Assert.IsNotNull(values);
            Assert.AreEqual(values!["Uid"], "1");
            Assert.AreEqual(values["Name"], "Opportunity");
            Assert.IsInstanceOf<List<string>>(values["Fields"]);
        }

        private class DataModelWithStaticDataKeyDuplicate : DatastoreModel<DataModelWithStaticDataKeyDuplicate>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                    .SetKey("Uid")
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
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Connect<DataModelWithStaticDataKeyDuplicate>(false));

            Assert.That(exception.Message, Contains.Substring("A static entity with the same key already exists."));
        }

        private class DataModelWithStaticDataWithoutKey : DatastoreModel<DataModelWithStaticDataWithoutKey>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Connect<DataModelWithStaticDataWithoutKey>(false));

            Assert.That(exception.Message, Contains.Substring("No key exists of entity 'ContactStatus'"));
        }

        private class DataModelWithStaticDataMissingProperty : DatastoreModel<DataModelWithStaticDataMissingProperty>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                    .SetKey("Uid")
                    .AddProperty("Label", typeof(string), false, IndexType.Unique);

                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "1", Name = "Active", OrderBy = "1" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "2", Name = "Inactive", OrderBy = "5" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "3", Name = "Blacklisted", OrderBy = "10" });
            }
        }

        [Test]
        public void EnsureStaticDataHaveCorrectProperties()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Connect<DataModelWithStaticDataMissingProperty>(false));

            Assert.That(exception.Message, Contains.Substring("The property 'Name' is not contained within entity 'ContactStatus'."));
        }

        #endregion

        #region IRefactorEntity DeleteNode

        private class DataModelStaticDataWithDeleteNode : DatastoreModel<DataModelStaticDataWithDeleteNode>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                    .SetKey("Uid")
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
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                DatastoreModel model = Connect<DataModelStaticDataWithDeleteNode>(true);
                dynamic? contactStatus = model.Entities["ContactStatus"].Refactor.MatchNode("1");
            });
            
            Assert.That(exception.Message, Contains.Substring("Specified argument was out of the range of valid values."));
        }

        #endregion

        #region IRefactorEntity Deprecate
        private class DataModelWithDeprecatedEntities : DatastoreModel<DataModelWithDeprecatedEntities>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                       .SetKey("Uid")
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
                var model = Connect<DataModelWithDeprecatedEntities>(true, true);
                Assert.Throws<ArgumentOutOfRangeException>(() => Assert.IsNotNull(model.Entities["Person"]));
                Assert.That(output.GetOutput(), Contains.Substring("Deprecate entity from Person"));
            }
        }

        #endregion

        #region IRefactorEntity MatchNode

        [Test]
        public void EnsureMatchNodeReturnsCorrectNode()
        {
            DatastoreModel model = Connect<DataModelWithStaticData>(true);

            Entity accountType = model.Entities["AccountType"];
            dynamic? account = accountType.Refactor.MatchNode("6");

            Assert.IsInstanceOf<DynamicEntity>(account);

            DynamicEntity? nodeType = account as DynamicEntity;
            IReadOnlyDictionary<string, object?>? values = nodeType?.GetDynamicEntityValues();
            Assert.IsNotNull(values);
            Assert.AreEqual(values!["Uid"], "6");
            Assert.AreEqual(values["Name"], "Account");

            Assert.Throws<ArgumentNullException>(() => accountType.Refactor.MatchNode(null!));
            Assert.Throws<InvalidCastException>(() => accountType.Refactor.MatchNode(6));
            Assert.Throws<ArgumentOutOfRangeException>(() => accountType.Refactor.MatchNode("7"));
        }
        #endregion

        #region IRefactorEntity Rename

        private class DatastoreEntityRefactor : DatastoreModel<DatastoreEntityRefactor>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                   .SetKey("Uid")
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

                Assert.Throws<ArgumentNullException>(() => Entities["NewAccountType"].Refactor.Rename(""));
                Assert.Throws<ArgumentNullException>(() => Entities["NewAccountType"].Refactor.Rename(null!));
            }
        }

        [Test]
        public void IRefactorEntityRename()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                Connect<DatastoreEntityRefactor>(true, true);
            }
        }
        #endregion

        #region IRefactorEntity ApplyConstraints

        private class DatastoreEntityRefactorConstraints : DatastoreModel<DatastoreEntityRefactorConstraints>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                   .SetKey("Uid")
                   .AddProperty("Name", typeof(string), false, IndexType.Unique)
                   .SetFullTextProperty("Name");

                Entities["AccountType"].Refactor.CreateNode(new { Uid = "6", Name = "Account" });
            }

            [Version(0, 0, 1)]
            public void Update()
            {
                Entities["AccountType"].AddProperty("Unique", typeof(string), IndexType.Unique);
                Entities["AccountType"].AddProperty("Indexed", typeof(string), IndexType.Indexed);
            }
        }

        [Test]
        public void IRefactorEntityApplyConstraints()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                Connect<DatastoreEntityRefactorConstraints>(true, true);

#if NEO4J
                Assert.That(output.GetOutput(), Contains.Substring("CREATE INDEX AccountType_Indexed_RangeIndex FOR (node:AccountType) ON (node.Indexed)"));
                Assert.That(output.GetOutput(), Contains.Substring("CREATE CONSTRAINT AccountType_Unique_UniqueConstraint FOR (node:AccountType) REQUIRE node.Unique IS UNIQUE"));
#elif MEMGRAPH
                Assert.That(output.GetOutput(), Contains.Substring("CREATE INDEX ON :AccountType(Indexed)"));
                Assert.That(output.GetOutput(), Contains.Substring("CREATE CONSTRAINT ON (node:AccountType) ASSERT node.Unique IS UNIQUE"));
#endif
            }
        }

#if !MEMGRAPH
        private class DatastoreEntityRefactorConstraintsHackedWithCompositeConstraint : DatastoreModel<DatastoreEntityRefactorConstraintsHackedWithCompositeConstraint>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                   .SetKey("Uid")
                   .AddProperty("Name", typeof(string), false, IndexType.Unique)
                   .SetFullTextProperty("Name");

                Entities["AccountType"].Refactor.CreateNode(new { Uid = "6", Name = "Account" });
            }

            [Version(0, 0, 1)]
            public void HackCompositeConstraint()
            {
                DataMigration.Run(delegate ()
                {
                    DataMigration.ExecuteCypher("CREATE CONSTRAINT AccountType_Uid_And_Name_UniqueConstraint FOR (node:AccountType) REQUIRE (node.Uid, node.Name) IS UNIQUE");
                });
            }

            [Version(0, 0, 2)]
            public void Update()
            {
                Entities["AccountType"].AddProperty("Unique", typeof(string), IndexType.Unique);
                Entities["AccountType"].AddProperty("Indexed", typeof(string), IndexType.Indexed);
            }
        }

        [Test]
        public void IRefactorEntityDontTouchCompositeConstraint()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                Connect<DatastoreEntityRefactorConstraintsHackedWithCompositeConstraint>(true, true);
                Assert.That(output.GetOutput(), Contains.Substring("CREATE INDEX AccountType_Indexed_RangeIndex FOR (node:AccountType) ON (node.Indexed)"));
                Assert.That(output.GetOutput(), Contains.Substring("CREATE CONSTRAINT AccountType_Unique_UniqueConstraint FOR (node:AccountType) REQUIRE node.Unique IS UNIQUE"));

                using (MockModel.BeginTransaction())
                {
                    Assert.That(Transaction.Run("show constraints").ToList().Select(item => item["name"]?.ToString()).Any(item => item == "AccountType_Uid_And_Name_UniqueConstraint"));
                }
            }
        }
#endif

        #endregion

        #region IRefactorEntity ChangeInheritance()

        private class DatastoreEntityBaseWithoutParent : DatastoreModel<DatastoreEntityBaseWithoutParent>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                  .SetKey("Uid")
                  .AddProperty("LastModifiedOn", typeof(DateTime))
                  .SetRowVersionField("LastModifiedOn");

                Entities.New("BaseTwo")
                  .Abstract(true)
                  .Virtual(true)
                  .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                  .SetKey("Uid")
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
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                  .SetKey("Uid")
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
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Connect<DatastoreEntityBaseWithoutParent>(true));
            Assert.That(exception.Message, Contains.Substring("Specified method is not supported."));

            var model = Connect<DatastoreEntityBaseWithParent>(true);
            Assert.AreEqual(model.Entities["Child"].Inherits?.Name, "BaseTwo");
        }

        #endregion

        #region IRefactorEntity ResetFunctionalId
        private class DatastoreEntityResetFunctionalId : DatastoreModel<DatastoreEntityResetFunctionalId>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                   .SetKey("Uid")
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
            var model = Connect<DatastoreEntityResetFunctionalId>(true);
            Assert.AreEqual(model.Entities["AccountType"].FunctionalId, model.FunctionalIds.Default);
        }
        #endregion

        #region IRefactorEntity CopyValue

        private class DatastoreEntityCopyValue : DatastoreModel<DatastoreEntityCopyValue>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("Account")
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid")
                   .AddProperty("Name", typeof(string), false, IndexType.Unique)
                   .AddProperty("CopyName", typeof(string))
                   .SetFullTextProperty("Name");

                Entities.New("AccountType")
                   .Summary("The type of an Account")
                   .HasStaticData(true)
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid")
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
                Connect<DatastoreEntityCopyValue>(true, true);
                Assert.IsTrue(Regex.IsMatch(output.GetOutput(), "Copy properties from Name to CopyName for entity Account"));
            }
        }

        #endregion

        #region IRefactorEntity SetDefaultValue

        private class DatastoreEntitySetDefaultValue : DatastoreModel<DatastoreEntitySetDefaultValue>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("Account")
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid")
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
                Connect<DatastoreEntitySetDefaultValue>(true, true);

                string query = "SetDefaultConstantValue -> Account.Name = 'First Account'";
                Assert.That(output.GetOutput(), Contains.Substring(query));
            }
        }

        #endregion

        #region IRefactorEntity SetFunctionalId
        private class DatastoreEntitySetFunctionalId : DatastoreModel<DatastoreEntitySetFunctionalId>
        {
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("Account")
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid")
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
            public override GDMS DatastoreTechnology => DatabaseConnectionSettings.DatastoreTechnology;

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
                   .SetKey("Uid");

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

#if NEO4J
        [Test]
        public void IRefactorEntitySetFunctionalId()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                Connect<DatastoreEntitySetFunctionalId>(true, true);

                Assert.That(output.GetOutput(), Contains.Substring("Differences for Account (\"A_\" : 1) -> CreateFunctionalId"));
                Assert.That(output.GetOutput(), Contains.Substring("Differences for ChangeAccount (\"CA_\" : 1) -> CreateFunctionalId"));
            }

            TearDown();

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DatastoreModel model = new DatastoreEntityInheritedFunctionalId();
                model.Execute(true);

                //Connect<DatastoreEntityInheritedFunctionalId>(true);
            });

            Assert.That(exception.Message, Contains.Substring("The entity 'Account' already inherited a functional id 'A_ (Account)', you cannot assign another one."));
        }
#endif

#endregion
    }
}
