using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Blueprint41.Neo4j.Persistence;
using Blueprint41.UnitTest.Helper;
using Neo4j.Driver;
using NUnit.Framework;
using NUnit.Framework.Constraints;

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
                       .SetKey("Uid")
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

                //TODO: This should fail when there is an existing inherited property
                Assert.Throws<InvalidOperationException>(() => Entities["Person"].Properties["FullName"].Refactor.Rename("LastModifiedOn"));

                Assert.Throws<ArgumentNullException>(() => Entities["Person"].Properties["FullName"].Refactor.Rename(null));                
                Assert.Throws<ArgumentNullException>(() => Entities["Person"].Properties["FullName"].Refactor.Rename(""));
            }
        }

        [Test]
        public void IRefactorPropertyRename()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                DataModelPropertyRename model = new DataModelPropertyRename() { LogToConsole = true };
                model.Execute(true);

                Assert.DoesNotThrow(() => { var a = model.Entities["Person"].Properties["FullName"]; });
                Assert.That(output.GetOutput(), Contains.Substring("executing RenameProperty -> Rename property from Name to FullName"));
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
                       .SetKey("Uid")
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
                       .SetKey("Uid")
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
                       .SetKey("Uid")
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

            Assert.DoesNotThrow(() =>
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
                       .SetKey("Uid")
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

        #region IRefactorPropertyConvert
        private class DataModelPropertyConvert : DatastoreModel<DataModelPropertyConvert>
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
                       .SetKey("Uid")
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Scene", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string), IndexType.Unique)
                       .AddProperty("BoolToLong", typeof(bool))
                       .AddProperty("BoolToDouble", typeof(bool))
                       .AddProperty("BoolToString", typeof(bool))
                       .AddProperty("LongToBool", typeof(long))
                       .AddProperty("LongToDouble", typeof(long))
                       .AddProperty("LongToString", typeof(long))
                       .AddProperty("DoubleToBool", typeof(double))
                       .AddProperty("DoubleToLong", typeof(double))
                       .AddProperty("DoubleToString", typeof(double))
                       .AddProperty("StringToBool", typeof(string))
                       .AddProperty("StringToLong", typeof(string))
                       .AddProperty("StringToDouble", typeof(string));
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                // TODO: Not able to convert types specified by genericConvertTabel (Property.cs)

                Assert.IsTrue(typeof(bool) == Entities["Scene"].Properties["BoolToLong"].SystemReturnType);
                Assert.IsTrue(typeof(bool) == Entities["Scene"].Properties["BoolToDouble"].SystemReturnType);
                Assert.IsTrue(typeof(bool) == Entities["Scene"].Properties["BoolToString"].SystemReturnType);

                Assert.IsTrue(typeof(long) == Entities["Scene"].Properties["LongToBool"].SystemReturnType);
                Assert.IsTrue(typeof(long) == Entities["Scene"].Properties["LongToDouble"].SystemReturnType);
                Assert.IsTrue(typeof(long) == Entities["Scene"].Properties["LongToString"].SystemReturnType);

                Assert.IsTrue(typeof(double) == Entities["Scene"].Properties["DoubleToBool"].SystemReturnType);
                Assert.IsTrue(typeof(double) == Entities["Scene"].Properties["DoubleToLong"].SystemReturnType);
                Assert.IsTrue(typeof(double) == Entities["Scene"].Properties["DoubleToString"].SystemReturnType);

                Assert.IsTrue(typeof(string) == Entities["Scene"].Properties["StringToBool"].SystemReturnType);
                Assert.IsTrue(typeof(string) == Entities["Scene"].Properties["StringToLong"].SystemReturnType);
                Assert.IsTrue(typeof(string) == Entities["Scene"].Properties["StringToDouble"].SystemReturnType);

                Assert.DoesNotThrow(() => Entities["Scene"].Properties["BoolToLong"].Refactor.Convert(typeof(long)));
                Assert.DoesNotThrow(() => Entities["Scene"].Properties["BoolToDouble"].Refactor.Convert(typeof(double)));
                Assert.DoesNotThrow(() => Entities["Scene"].Properties["BoolToString"].Refactor.Convert(typeof(string)));

                Assert.DoesNotThrow(() => Entities["Scene"].Properties["LongToBool"].Refactor.Convert(typeof(bool)));
                Assert.DoesNotThrow(() => Entities["Scene"].Properties["LongToDouble"].Refactor.Convert(typeof(double)));
                Assert.DoesNotThrow(() => Entities["Scene"].Properties["LongToString"].Refactor.Convert(typeof(string)));

                Assert.DoesNotThrow(() => Entities["Scene"].Properties["DoubleToBool"].Refactor.Convert(typeof(bool)));
                Assert.DoesNotThrow(() => Entities["Scene"].Properties["DoubleToLong"].Refactor.Convert(typeof(long)));
                Assert.DoesNotThrow(() => Entities["Scene"].Properties["DoubleToString"].Refactor.Convert(typeof(string)));

                Assert.Throws<NotSupportedException>(() => Entities["Scene"].Properties["StringToBool"].Refactor.Convert(typeof(bool)));
                Assert.DoesNotThrow(() => Entities["Scene"].Properties["StringToLong"].Refactor.Convert(typeof(long)));
                Assert.DoesNotThrow(() => Entities["Scene"].Properties["StringToDouble"].Refactor.Convert(typeof(double)));
            }
        }

        [Test]
        public void IRefactorPropertyConvert()
        {
            DataModelPropertyConvert model = new DataModelPropertyConvert();
            model.Execute(true);
        }
        #endregion

        #region IRefactorPropertySetIndexTypeAndDeprecate
        private class DataModelPropertySetIndexTypeAndDeprecate : DatastoreModel<DataModelPropertySetIndexTypeAndDeprecate>
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
                       .SetKey("Uid")
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Person", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string));

                Entities.New("Movie", Entities["BaseEntity"])
                       .AddProperty("Title", typeof(string), IndexType.Unique)
                       .AddProperty("ReleaseDate", typeof(DateTime));

                Entities.New("Scene", Entities["Movie"])
                       .AddProperty("Name", typeof(string), IndexType.Unique)
                       .AddProperty("Number", typeof(bool));

                Entities.New("Genre", Entities["BaseEntity"])
                    .AddProperty("Name", typeof(string), IndexType.Unique)
                    .HasStaticData(true)
                    .SetFullTextProperty("Name");

                Entities["Genre"].Refactor.CreateNode(new { Uid = "7", Name = "Action" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "8", Name = "Adventure" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "9", Name = "Comedy" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "10", Name = "Drama" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "11", Name = "Horror" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "12", Name = "Musical" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "13", Name = "Science Fiction" });

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
                Assert.IsTrue(IndexType.Unique == Entities["Scene"].Properties["Name"].IndexType);
                Assert.IsTrue(IndexType.None == Entities["Scene"].Properties["Number"].IndexType);
                Assert.IsTrue(IndexType.Unique == Entities["Genre"].Properties["Name"].IndexType);

                Entities["Scene"].Properties["Name"].Refactor.SetIndexType(IndexType.None);
                Entities["Scene"].Properties["Number"].Refactor.SetIndexType(IndexType.Unique);
                Entities["Genre"].Properties["Name"].Refactor.SetIndexType(IndexType.Indexed);

                Assert.IsTrue(IndexType.None == Entities["Scene"].Properties["Name"].IndexType);
                Assert.IsTrue(IndexType.Unique == Entities["Scene"].Properties["Number"].IndexType);
                Assert.IsTrue(IndexType.Indexed == Entities["Genre"].Properties["Name"].IndexType);
            }

            //[Version(0, 0, 2)]
            //public void Script_0_0_2()
            //{
            //    Entities["Scene"].Properties["Name"].Refactor.Deprecate();
            //    Entities["Scene"].Properties["Number"].Refactor.Deprecate();
            //    Entities["Genre"].Properties["Name"].Refactor.Deprecate();

            //    Assert.Throws<ArgumentOutOfRangeException>(() => { var name = Entities["Scene"].Properties["Name"]; });
            //    Assert.Throws<ArgumentOutOfRangeException>(() => { var num = Entities["Scene"].Properties["Number"]; });
            //    Assert.Throws<ArgumentOutOfRangeException>(() => { var name = Entities["Genre"].Properties["Name"]; });
            //}
        }

        [Test]
        public void IRefactorPropertySetIndexTypeAndDeprecate()
        {
            DataModelPropertySetIndexTypeAndDeprecate model = new DataModelPropertySetIndexTypeAndDeprecate();
            model.Execute(true);
                        
            IndexAssert.None("Scene", "Name");
            IndexAssert.Unique("Scene", "Number");
            IndexAssert.Index("Genre", "Name");
        } 

        #endregion

        #region IRefactorReroute

        private class DataModelPropertyReroute : DatastoreModel<DataModelPropertyReroute>
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
                       .SetKey("Uid")
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Person", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string));

                Entities.New("Movie", Entities["BaseEntity"])
                       .AddProperty("Title", typeof(string), IndexType.Unique)
                       .AddProperty("ReleaseDate", typeof(DateTime));

                Entities.New("Scene", Entities["Movie"])
                       .AddProperty("Name", typeof(string), IndexType.Unique)
                       .AddProperty("Number", typeof(int));

                Entities.New("Genre", Entities["BaseEntity"])
                    .AddProperty("Name", typeof(string), IndexType.Unique)
                    .HasStaticData(true)
                    .SetFullTextProperty("Name");

                Entities["Genre"].Refactor.CreateNode(new { Uid = "7", Name = "Action" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "8", Name = "Adventure" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "9", Name = "Comedy" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "10", Name = "Drama" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "11", Name = "Horror" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "12", Name = "Musical" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "13", Name = "Science Fiction" });

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
                // TODO: Reroute tests
            }
        }

        [Test]
        public void IRefactorPropertyReroute()
        {
            DataModelPropertyReroute model = new DataModelPropertyReroute();
            model.Execute(true);
        }
        #endregion

        #region IRefactorConvert, IRefactorMakeMandatory(), IRefactorMakeNullable
        private class DataModelPropertyConvertRel : DatastoreModel<DataModelPropertyConvertRel>
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
                       .SetKey("Uid")
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Person", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string))
                       .AddProperty("NotNullableProperty", typeof(int), false);

                Entities.New("Movie", Entities["BaseEntity"])
                       .AddProperty("Title", typeof(string), IndexType.Unique)
                       .AddProperty("ReleaseDate", typeof(DateTime));

                Relations.New(Entities["Person"], Entities["Movie"], "DIRECTED_BY", "DIRECTED_BY")
                    .SetInProperty("DirectedMovies", PropertyType.Collection)
                    .SetOutProperty("Director", PropertyType.Lookup);

                Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTORS")
                    .SetInProperty("ActedInMovies", PropertyType.Collection)
                    .SetOutProperty("Actors", PropertyType.Collection);
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                Assert.IsTrue(Relations["DIRECTED_BY"].InProperty.PropertyType == PropertyType.Collection);
                Assert.IsTrue(Relations["DIRECTED_BY"].OutProperty.PropertyType == PropertyType.Lookup);

                Assert.Throws<NotImplementedException>(() => Relations["DIRECTED_BY"].InProperty.Refactor.ConvertToLookup(ConvertAlgorithm.TakeFirst));
                Relations["DIRECTED_BY"].OutProperty.Refactor.ConvertToCollection();

                Assert.IsTrue(Relations["DIRECTED_BY"].InProperty.PropertyType == PropertyType.Lookup);
                Assert.IsTrue(Relations["DIRECTED_BY"].OutProperty.PropertyType == PropertyType.Collection);
            }

            [Version(0, 0, 2)]
            public void Script_0_0_2()
            {
                Assert.IsTrue(Entities["Person"].Properties["Name"].Nullable == true);
                Assert.IsTrue(Entities["Person"].Properties["NotNullableProperty"].Nullable == false);

                Entities["Person"].Properties["Name"].Refactor.MakeMandatory();
                Entities["Person"].Properties["NotNullableProperty"].Refactor.MakeNullable();

                Assert.IsTrue(Entities["Person"].Properties["Name"].Nullable == false);
                Assert.IsTrue(Entities["Person"].Properties["NotNullableProperty"].Nullable == true);
            }
        }

        [Test]
        public void IRefactorPropertyConvertToCollectionOrLookup()
        {
            DataModelPropertyConvertRel model = new DataModelPropertyConvertRel();
            model.Execute(true);
        }
        #endregion

        #region IRefactorMakeMandatory with values
        private class DataModelPropertyMandatory : DatastoreModel<DataModelPropertyMandatory>
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
                       .SetKey("Uid")
                       .AddProperty("LastModifiedOn", typeof(DateTime))
                       .SetRowVersionField("LastModifiedOn");

                Entities.New("Person", Entities["BaseEntity"])
                       .AddProperty("Name", typeof(string))
                       .AddProperty("MandatoryProperty", typeof(int), false);

                Entities.New("Movie", Entities["BaseEntity"])
                       .AddProperty("Title", typeof(string), IndexType.Unique)
                       .AddProperty("ReleaseDate", typeof(DateTime));

                Relations.New(Entities["Person"], Entities["Movie"], "DIRECTED_BY", "DIRECTED_BY")
                    .SetInProperty("DirectedMovies", PropertyType.Collection)
                    .SetOutProperty("Director", PropertyType.Lookup);

                Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTORS")
                    .SetInProperty("ActedInMovies", PropertyType.Collection)
                    .SetOutProperty("Actors", PropertyType.Collection);

                Entities.New("Genre", Entities["BaseEntity"])
                    .AddProperty("Name", typeof(string), IndexType.Unique)
                    .HasStaticData(true)
                    .SetFullTextProperty("Name");

                Relations.New(Entities["Movie"], Entities["Genre"], "MOVIE_HAS", "MOVIE_HAS")
                    .SetInProperty("MovieGenre", PropertyType.Lookup)
                    .SetOutProperty("Movies", PropertyType.Collection);

                Relations.New(Entities["Person"], Entities["Genre"], "LIKES", "LIKES")
                    .SetInProperty("MovieGenre", PropertyType.Lookup)
                    .SetOutProperty("Person", PropertyType.Collection);

                Entities["Genre"].Refactor.CreateNode(new { Uid = "7", Name = "Action" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "8", Name = "Adventure" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "9", Name = "Comedy" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "10", Name = "Drama" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "11", Name = "Horror" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "12", Name = "Musical" });
                Entities["Genre"].Refactor.CreateNode(new { Uid = "13", Name = "Science Fiction" });
            }

            [Version(0, 0, 1)]
            public void Script_0_0_1()
            {
                Entities["Person"].Properties["Name"].Refactor.MakeMandatory("Mr/Mrs.");
                Relations["MOVIE_HAS"].InProperty.Refactor.MakeMandatory("Action");
                Relations["LIKES"].InProperty.Refactor.MakeMandatory(Entities["Genre"].Refactor.MatchNode("8"));
            }
        }

        [Test]
        public void IRefactorPropertyMakeMandatoryWithValues()
        {
            using (ConsoleOutput output = new ConsoleOutput())
            {
                DataModelPropertyMandatory model = new DataModelPropertyMandatory() { LogToConsole = true };
                model.Execute(true);

                string consoleOutput = output.GetOutput();
                Assert.That(consoleOutput, Contains.Substring("executing SetDefaultConstantValue -> Person.Name = 'Mr/Mrs.'"));
                Assert.That(consoleOutput, Contains.Substring("executing SetDefaultLookupValue -> Set Default Lookup Value for Movie.MovieGenre"));
                Assert.That(consoleOutput, Contains.Substring("executing SetDefaultLookupValue -> Set Default Lookup Value for Person.MovieGenre"));
            }   
        }
        #endregion
    }

    internal class IndexAssert
    {
        private const char MANDATORY = 'M';
        private const bool INDEX     = true;
        private const char UNIQUE    = 'U'; 
        private const char KEY       = 'K';

#if NEO4J
        private const string SHOW_CONSTRAINTS = "SHOW CONSTRAINTS";
        private const string SHOW_INDEXES = "SHOW INDEXES";

        private const string SHOW_CONSTRAINTS_LABEL = "labelsOrTypes";
        private const string SHOW_CONSTRAINTS_PROPERTIES = "properties";
        private const string SHOW_CONSTRAINTS_CONSTRAINT_TYPE = "type";

        private const string SHOW_INDEXES_LABELS = "labelsOrTypes";
        private const string SHOW_INDEXES_PROPERTIES = "properties";


#elif MEMGRAPH
        private const string SHOW_CONSTRAINTS = "SHOW CONSTRAINT INFO";
        private const string SHOW_INDEXES = "SHOW INDEX INFO";

        private const string SHOW_CONSTRAINTS_LABEL = "label";
        private const string SHOW_CONSTRAINTS_PROPERTIES = "properties";
        private const string SHOW_CONSTRAINTS_CONSTRAINT_TYPE = "constraint type";

        private const string SHOW_INDEXES_LABELS = "label";
        private const string SHOW_INDEXES_PROPERTIES = "property";
#endif

        private IndexAssert() 
        {
            using (Session.Begin())
            {   
                var constraints = Session.Current.Run(SHOW_CONSTRAINTS);

                Constraints =
                    constraints.Select(item => 
                        (
                            GetSingleItem(item.Values[SHOW_CONSTRAINTS_LABEL]),
                            GetSingleItem(item.Values[SHOW_CONSTRAINTS_PROPERTIES]),
                            GetConstraintType(item.Values[SHOW_CONSTRAINTS_CONSTRAINT_TYPE].As<string>())
                        )
                    ).ToList();

                var indexes = Session.Current.Run(SHOW_INDEXES);

                Indexes =                   
                    indexes.Where(item => item.Values[SHOW_INDEXES_LABELS] is not null)
                           .Select(item =>
                        (
                            GetSingleItem(item.Values[SHOW_INDEXES_LABELS]),
                            GetSingleItem(item.Values[SHOW_INDEXES_PROPERTIES])
                        )
                    ).ToList();
            }

            char GetConstraintType(string dbType)
            {
#if NEO4J
                if (dbType.Contains("NODE_PROPERTY_EXISTENCE"))
                    return MANDATORY;
                else if (dbType.Contains("UNIQUENESS"))
                    return UNIQUE;
                else if (dbType.Contains("NODE_KEY"))
                    return KEY;
                else
                    throw new NotImplementedException();
#elif MEMGRAPH
                if (dbType.Contains("exists"))
                    return MANDATORY;
                else if (dbType.Contains("unique"))
                    return UNIQUE;
                else
                    throw new NotImplementedException();
#else
                throw new NotImplementedException();
                #error Not Implemented
#endif
            }

            string GetSingleItem(object properties)
            {
                object value = properties;
                Type type = properties.GetType();
                IEnumerable coll = properties as IEnumerable;
                if (properties is not string && coll is not null)
                    value = coll.Cast<object>().FirstOrDefault();

                return value?.ToString(); 
            }
        }

        private readonly IReadOnlyList<(string entity, string property)> Indexes;
        private readonly IReadOnlyList<(string entity, string property, char type)> Constraints;

        public static void None(string actualEntity, string actualProperty, bool mandatory = false)
        {
            IndexAssert assert = new IndexAssert();

            assert.HasIndex(actualEntity, actualProperty);
            if (mandatory)
                assert.HasConstraints(actualEntity, actualProperty, MANDATORY);
            else
                assert.HasConstraints(actualEntity, actualProperty);
        }

        public static void Index(string actualEntity, string actualProperty, bool mandatory = false)
        {
            IndexAssert assert = new IndexAssert();

            assert.HasIndex(actualEntity, actualProperty, INDEX);
            if (mandatory)
                assert.HasConstraints(actualEntity, actualProperty, MANDATORY);
            else
                assert.HasConstraints(actualEntity, actualProperty);
        }

        public static void Unique(string actualEntity, string actualProperty, bool mandatory = false)
        {
            IndexAssert assert = new IndexAssert();
 
            assert.HasIndex(actualEntity, actualProperty, INDEX);
 
            if (mandatory)
                assert.HasConstraints(actualEntity, actualProperty, MANDATORY, UNIQUE);
            else
                assert.HasConstraints(actualEntity, actualProperty, UNIQUE);
        }

        public static void Key(string actualEntity, string actualProperty)
        {
            IndexAssert assert = new IndexAssert();

#if NEO4J
            assert.HasIndex(actualEntity, actualProperty);
            assert.HasConstraints(actualEntity, actualProperty, KEY);
#elif MEMGRAPH
            assert.HasIndex(actualEntity, actualProperty, INDEX);
            assert.HasConstraints(actualEntity, actualProperty, MANDATORY, UNIQUE);
#endif
        }

        private void HasIndex(string actualEntity, string actualProperty, bool exist = false)
        {
            if (!exist)
                Assert.That(!Indexes.Any(index => index.entity == actualEntity && index.property == actualProperty), $"The index {actualEntity}.{actualProperty} should not exist");
            else 
                Assert.That(Indexes.Any(index => index.entity == actualEntity && index.property == actualProperty), $"The index {actualEntity}.{actualProperty} should have existed");
        }

        private void HasConstraints(string actualEntity, string actualProperty, params char[] exist)
        {
            var relevant = Constraints.Where(constraint => constraint.entity == actualEntity && constraint.property == actualProperty).ToList();

            int count = relevant.Where(constraint => exist.Any(item => constraint.type == item)).Count();
            int zero = relevant.Where(constraint => !exist.Any(item => constraint.type == item)).Count();

            Assert.That(count == exist.Length, $"There were unexpected constraints present for {actualEntity}.{actualProperty}.");
            Assert.That(zero == 0, $"There were constraints missing for {actualEntity}.{actualProperty}.");
        }
    }
}
