using Blueprint41.UnitTest.Helper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Blueprint41.UnitTest.Tests
{
    [TestFixture]
    internal class TestRefactorProperty : TestBase
    {

        #region IRefactorPropertyRename
        private class DataModelPropertyRename : DatastoreModel<DataModelPropertyRename>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Script_0_0_0()
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

                Entities.New("Movie", Entities["BaseEntity"])
                       .AddProperty("Title", typeof(string), IndexType.Unique);

                Relations.New(Entities["Person"], Entities["Movie"], "PERSON_DIRECTED", "DIRECTED_BY")
                    .SetInProperty("DirectedMovies", PropertyType.Collection)
                    .SetOutProperty("Director", PropertyType.Lookup);

                Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTORS")
                    .SetInProperty("ActedInMovies", PropertyType.Collection)
                    .SetOutProperty("Actors", PropertyType.Collection);
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                Entities["Person"].Properties["Name"].Refactor.Rename("FullName");

                Assert.Throws<ArgumentNullException>(() => Entities["Person"].Properties["FullName"].Refactor.Rename(null));

                // TODO: Failed: Should not support empty string
                Assert.Throws<Exception>(() => Entities["Person"].Properties["FullName"].Refactor.Rename(""));                
            }
        }

        [Test]
        public void IRefactorPropertyRename()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                DataModelPropertyRename model = new DataModelPropertyRename();
                model.Execute(true);

                Assert.DoesNotThrow(() => { var a = model.Entities["Person"].Properties["FullName"]; });
                Assert.IsTrue(Regex.IsMatch(output.GetOuput(), @"(MATCH \(node:Person\) WHERE EXISTS\(node.Name\))[^a-zA-Z,0-9]*(WITH node LIMIT 10000)[^a-zA-Z,0-9]*(SET node\.FullName = node\.Name)[^a-zA-Z,0-9]*(SET node\.Name = NULL)"));
            }

        }
        #endregion

        #region IRefactorPropertyMove
        private class DataModelPropertyMove : DatastoreModel<DataModelPropertyMove>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Script_0_0_0()
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

                Entities.New("Movie", Entities["BaseEntity"])
                       .AddProperty("Title", typeof(string), IndexType.Unique)
                       .AddProperty("ToMoveProperty", typeof(string));

                Relations.New(Entities["Person"], Entities["Movie"], "PERSON_DIRECTED", "DIRECTED_BY")
                    .SetInProperty("DirectedMovies", PropertyType.Collection)
                    .SetOutProperty("Director", PropertyType.Lookup);

                Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTORS")
                    .SetInProperty("ActedInMovies", PropertyType.Collection)
                    .SetOutProperty("Actors", PropertyType.Collection);
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                Entities["Movie"].Properties["ToMoveProperty"].Refactor.Move(Entities["Person"]);
            }
        }

        private class DataModelPropertyMoveToBase : DatastoreModel<DataModelPropertyMove>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Script_0_0_0()
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

                Entities.New("Movie", Entities["BaseEntity"])
                       .AddProperty("Title", typeof(string), IndexType.Unique);

                Entities.New("Scene", Entities["Movie"])
                       .AddProperty("ToMoveProperty", typeof(string));

                Relations.New(Entities["Person"], Entities["Movie"], "PERSON_DIRECTED", "DIRECTED_BY")
                    .SetInProperty("DirectedMovies", PropertyType.Collection)
                    .SetOutProperty("Director", PropertyType.Lookup);

                Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTORS")
                    .SetInProperty("ActedInMovies", PropertyType.Collection)
                    .SetOutProperty("Actors", PropertyType.Collection);
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                Entities["Scene"].Properties["ToMoveProperty"].Refactor.Move(Entities["Movie"]);
            }
        }

        private class DataModelPropertyMoveFromBase : DatastoreModel<DataModelPropertyMove>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Script_0_0_0()
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

                Entities.New("Movie", Entities["BaseEntity"])
                       .AddProperty("Title", typeof(string), IndexType.Unique)
                       .AddProperty("MovePropertyToChild", typeof(string));

                Entities.New("Scene", Entities["Movie"])
                       .AddProperty("Name", typeof(string));

                Relations.New(Entities["Person"], Entities["Movie"], "PERSON_DIRECTED", "DIRECTED_BY")
                    .SetInProperty("DirectedMovies", PropertyType.Collection)
                    .SetOutProperty("Director", PropertyType.Lookup);

                Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTORS")
                    .SetInProperty("ActedInMovies", PropertyType.Collection)
                    .SetOutProperty("Actors", PropertyType.Collection);
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                Entities["Movie"].Properties["MovePropertyToChild"].Refactor.Move(Entities["Scene"]);
            }
        }

        [Test]
        public void IRefactorPropertyMove()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                DataModelPropertyMove model = new DataModelPropertyMove();
                model.Execute(true);

            });

            TearDown();

            DataModelPropertyMoveToBase modelToBase = new DataModelPropertyMoveToBase();
            modelToBase.Execute(true);

            Assert.DoesNotThrow(() => { var a = modelToBase.Entities["Movie"].Properties["ToMoveProperty"]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { var a = modelToBase.Entities["Scene"].Properties["ToMoveProperty"]; });

            TearDown();

            Assert.Throws<InvalidOperationException>(() =>
            {

                DataModelPropertyMoveFromBase modelFromBase = new DataModelPropertyMoveFromBase();
                modelFromBase.Execute(true);
            });
        }
        #endregion

        #region IRefactorPropertyMerge
        private class DataModelPropertyMerge : DatastoreModel<DataModelPropertyMerge>
        {
            protected override void SubscribeEventHandlers()
            {

            }

            [Version(0, 0, 0)]
            public void Script_0_0_0()
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

                Entities.New("Movie", Entities["BaseEntity"])
                       .AddProperty("Title", typeof(string), IndexType.Unique)
                       .AddProperty("ReleaseDate", typeof(DateTime));

                Entities.New("Scene", Entities["Movie"])
                       .AddProperty("Name", typeof(string), IndexType.Unique)
                       .AddProperty("Numbers", typeof(List<int>));

                Relations.New(Entities["Person"], Entities["Movie"], "PERSON_DIRECTED", "DIRECTED_BY")
                    .SetInProperty("DirectedMovies", PropertyType.Collection)
                    .SetOutProperty("Director", PropertyType.Lookup);

                Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTORS")
                    .SetInProperty("ActedInMovies", PropertyType.Collection)
                    .SetOutProperty("Actors", PropertyType.Collection);

                Relations.New(Entities["Movie"], Entities["Scene"], "MOVIES_HAS", "MOVIES_HAS")
                    .SetInProperty("MovieScenes", PropertyType.Collection)
                    .SetOutProperty("Scenes", PropertyType.Collection);
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                Entities["Scene"].Properties["Name"].Refactor.Merge(Entities["Movie"].Properties["Title"], MergeAlgorithm.PreferTarget);
                Assert.Throws<ArgumentOutOfRangeException>(() => { var a = Entities["Scene"].Properties["Name"]; });

                Entities["Scene"].AddProperty("Name", typeof(string), IndexType.Unique);

                Entities["Scene"].Properties["Name"].Refactor.Merge(Entities["Movie"].Properties["Title"], MergeAlgorithm.PreferSource);
                Assert.Throws<ArgumentOutOfRangeException>(() => { var a = Entities["Scene"].Properties["Name"]; });

                Entities["Scene"].AddProperty("Name", typeof(string), IndexType.Unique);

                Assert.Throws<ArgumentException>(() =>
                {
                    Entities["Movie"].Properties["Title"].Refactor.Merge(Entities["Scene"].Properties["Name"], MergeAlgorithm.PreferTarget);
                });

                // TODO: Should this be allowed to merge different types?
                Entities["Scene"].Properties["Numbers"].Refactor.Merge(Entities["Movie"].Properties["ReleaseDate"], MergeAlgorithm.PreferTarget);

                Entities["Scene"].AddProperty("Numbers", typeof(List<int>));
                Assert.Throws<NotImplementedException>(() => Relations["MOVIES_HAS"].OutProperty.Refactor.Merge(Entities["Scene"].Properties["Numbers"], MergeAlgorithm.PreferSource));
            }
        }

        [Test]
        public void IRefactorPropertyMerge()
        {
            DataModelPropertyMerge model = new DataModelPropertyMerge();
            model.Execute(true);
        }
        #endregion
    }
}
