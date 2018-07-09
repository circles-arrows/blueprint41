using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
            Assert.IsTrue(model.Entities["Person"].IsSelfOrSubclassOf(model.Entities["BaseEntity"]));
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
        public void EnsureKeyIsUnique()
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

        #region DataModelWithDeprecatedEntities
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
        public void EnsureDeprecatedEntitiesDoesNotExists()
        {
            DataModelWithDeprecatedEntities model = new DataModelWithDeprecatedEntities();
            model.Execute(false);

            Assert.Throws<ArgumentOutOfRangeException>(() => Assert.IsNotNull(model.Entities["Person"]));
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
                // TODO: Bug, Changing inheritance with same prperties does not trigger an exception
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

            Assert.That(exception.Message, Contains.Substring("Property with the name Name already exists on base class Entity BaseEntity"));
        }

        #endregion
    }
}
