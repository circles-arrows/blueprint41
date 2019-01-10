using Blueprint41.Dynamic;
using Blueprint41.UnitTest.Base;
using Blueprint41.UnitTest.Misc;
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
    internal class TestDataModel
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

                Entities.New("BaseEntityWithRowVersion")
                       .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                       .Abstract(true)
                       .Virtual(true)
                       .SetKey("Uid", true)
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("PersonInheritedBase", Entities["BaseEntityWithRowVersion"])
                       .AddProperty("Name", typeof(string));

                Entities.New("AddressWithNoParent")
                        .AddProperty("Name", typeof(string))
                        .AddProperty("LastModifiedOn", typeof(DateTime));

                Assert.IsTrue(Entities["BaseEntityWithRowVersion"].Properties["LastModifiedOn"].IsRowVersion);
                Assert.IsTrue(Entities["PersonInheritedBase"].IsSubsclassOf(Entities["BaseEntityWithRowVersion"]));
                Assert.IsFalse(Entities["AddressWithNoParent"].Properties["LastModifiedOn"].IsRowVersion);

                // Invalid RowVersion
                Entities.New("WithInvalidRowVersion")
                        .AddProperty("Name", typeof(string))
                        .AddProperty("LastModifiedOn", typeof(string));

                Helper.AssertThrows<NotSupportedException>(() =>
                {
                    Entities["WithInvalidRowVersion"].SetRowVersionField("LastModifiedOn");

                }, "You cannot make a non-datetime field the row version.");

                // Multiple RowVersion
                Entities.New("WithMultipleRowVersion")
                        .AddProperty("Name", typeof(string))
                        .AddProperty("LastModifiedOn", typeof(DateTime))
                        .AddProperty("ModifiedOn", typeof(DateTime));

                Helper.AssertThrows<NotSupportedException>(() =>
                {
                    Entities["WithMultipleRowVersion"].SetRowVersionField("LastModifiedOn");
                    Entities["WithMultipleRowVersion"].SetRowVersionField("ModifiedOn");

                }, "Multiple row version fields are not allowed.");
            }
        }

        [Test]
        public void EnsureEntitiesHaveRowVersionProperty()
        {
            DatastoreModel model = new DataModelWithRowVersion();
            model.Execute(false);
        }

        #endregion       

        #region DataModelEntityKey
        private class DataModelKey : DatastoreModel<DataModelKey>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                FunctionalIds.Default = FunctionalIds.New("Shared", "0", IdFormat.Numeric, 0);

                Entities.New("UniqueKey")
                     .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                     .SetKey("Uid", true);

                Assert.IsTrue(Entities["UniqueKey"].Properties["Uid"].IsKey);
                Assert.IsTrue(Entities["UniqueKey"].Properties["Uid"].IndexType == IndexType.Unique);

                Helper.AssertThrows<NotSupportedException>(() =>
                {
                    Entities.New("MultipleUniqueKey")
                      .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                      .AddProperty("OtherUid", typeof(string), false, IndexType.Unique)
                      .SetKey("Uid", true)
                      .SetKey("OtherUid", true);

                }, "Multiple key not allowed.");

                Helper.AssertThrows<NotSupportedException>(() =>
                {
                    Entities.New("NotUniqueKey")
                      .AddProperty("Uid", typeof(string), false)
                      .SetKey("Uid", true);

                }, "You cannot make a non-unique field the key.");
            }
        }

        [Test]
        public void EnsureEntityKeyIsUnique()
        {
            DatastoreModel model = new DataModelKey();
            model.Execute(false);
        }

        #endregion        

        #region DataModelProperties
        private class DataModelEntityProperties : DatastoreModel<DataModelEntityProperties>
        {
            protected override void SubscribeEventHandlers() { }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                Helper.AssertThrows<ArgumentException>(() =>
                {
                    Entities.New("SameProperty")
                       .AddProperty("Description", typeof(string))
                       .AddProperty("Description", typeof(string));
                }, "A Property with the name 'Description' already exists.\r\nParameter name: name");

                Helper.AssertThrows<NotSupportedException>(() =>
                {
                    Entities.New("BaseEntity")
                       .AddProperty("Name", typeof(string));

                    Entities.New("ChildEntity", Entities["BaseEntity"])
                           .AddProperty("Name", typeof(string));

                }, "Property with the name Name already exists on base class Entity BaseEntity");

                Entities.New("Property")
                    .AddProperty("Unique", typeof(string), IndexType.Unique)
                    .AddProperty("Indexed", typeof(string), IndexType.Indexed)
                    .AddProperty("None", typeof(string), IndexType.None)
                    .AddProperty("DefaultIndex", typeof(string))
                    .AddProperty("FullTextProperty", typeof(string))
                    .SetFullTextProperty("FullTextProperty");

                Assert.IsTrue(Entities["Property"].Properties["Unique"].IndexType == IndexType.Unique);
                Assert.IsTrue(Entities["Property"].Properties["Indexed"].IndexType == IndexType.Indexed);
                Assert.IsTrue(Entities["Property"].Properties["None"].IndexType == IndexType.None);
                Assert.IsTrue(Entities["Property"].Properties["DefaultIndex"].IndexType == IndexType.None);
                Assert.IsTrue(Entities["Property"].FullTextIndexProperties.Contains(Entities["Property"].Properties["FullTextProperty"]));

                Entities["Property"].RemoveFullTextProperty("FullTextProperty");

                Assert.IsFalse(Entities["Property"].FullTextIndexProperties.Contains(Entities["Property"].Properties["FullTextProperty"]));

                Entities["Property"].Abstract().Virtual();

                Assert.IsTrue(Entities["Property"].IsAbstract);
                Assert.IsTrue(Entities["Property"].IsVirtual);
            }
        }

        [Test]
        public void EnsurePropertiesAreValid()
        {
            DatastoreModel model = new DataModelEntityProperties();
            model.Execute(false);
        }
        #endregion

        #region DataModelRelationships()
        private class DataModelEntityRelationships : DatastoreModel<DataModelEntityRelationships>
        {
            protected override void SubscribeEventHandlers() { }

            [Version(0, 0, 0)]
            public void Initialize()
            {
                Entities.New("RelationshipOne")
                     .AddProperty("Name", typeof(string));

                Entities.New("RelationshipTwo")
                    .AddProperty("Name", typeof(string));

                Relations.New(Entities["RelationshipOne"], Entities["RelationshipTwo"], "HAS_RELATIONSHIP", "HAS")
                    .SetInProperty("InRels", PropertyType.Collection)
                    .SetOutProperty("OutRels", PropertyType.Lookup);

                Assert.IsTrue(Relations.Contains("HAS_RELATIONSHIP"));
                Assert.AreEqual(Relations["HAS_RELATIONSHIP"].Neo4JRelationshipType, "HAS");

                Assert.IsTrue(Entities["RelationshipOne"].HaveRelationship);
                Assert.IsTrue(Entities["RelationshipOne"].GetRelationships().Count() == 1);

                Assert.IsTrue(Entities["RelationshipTwo"].HaveRelationship);
                Assert.IsTrue(Entities["RelationshipTwo"].GetRelationships().Count() == 1);

                Assert.IsTrue(Relations["HAS_RELATIONSHIP"].InEntity == Entities["RelationshipTwo"]);
                Assert.IsTrue(Relations["HAS_RELATIONSHIP"].OutEntity == Entities["RelationshipOne"]);

                Assert.IsTrue(Relations["HAS_RELATIONSHIP"].InProperty.PropertyType == PropertyType.Collection);
                Assert.IsTrue(Relations["HAS_RELATIONSHIP"].OutProperty.PropertyType == PropertyType.Lookup);
            }
        }

        [Test]
        public void EnsureRelationshipsAreValid()
        {
            DatastoreModel model = new DataModelEntityProperties();
            model.Execute(false);
        }

        #endregion

    }
}
