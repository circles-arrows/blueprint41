using Blueprint41.Core;
using Blueprint41.Response;
using Blueprint41.UnitTest.Base;
using Blueprint41.UnitTest.Misc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest
{
    public class TestIRefactorProperty : Neo4jBase
    {
        public override Type Datastoremodel => typeof(DataModel);

        private class DataModel : DatastoreModel<DataModel>
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
                   .HasStaticData()
                   .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                   .SetKey("Uid", true)
                   .AddProperty("Name", typeof(string), false, IndexType.Unique)
                   .SetFullTextProperty("Name");

                Entities["AccountType"].Refactor.CreateNode(new { Uid = "6", Name = "AccountTypeOne" });
                Entities["AccountType"].Refactor.CreateNode(new { Uid = "81", Name = "AccountTypeTwo" });

                Entities.New("ContactStatus")
                    .Summary("The ContactStatus is describing the status of a Contact")
                    .Example("Active, Inactive, Blacklisted")
                    .AddProperty("OrderBy", typeof(string))
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .SetKey("Uid", true)
                    .AddProperty("Name", typeof(string), false, IndexType.Unique);

                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "1", Name = "Active", OrderBy = "1" });
                Entities["ContactStatus"].Refactor.CreateNode(new { Uid = "2", Name = "Inactive", OrderBy = "5" });

                Entities.New("ReferenceType")
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .SetKey("Uid", true)
                    .AddProperty("Name", typeof(string), false)
                    .AddProperty("Fields", typeof(List<string>));

                Entities["ReferenceType"].Refactor.CreateNode(new { Uid = "1", Name = "Opportunity", Fields = new List<string>() { "Field1" } });
            }

            [Version(0, 0, 1)]
            public void RefactorProperty()
            {
                // Rename
                Entities["AccountType"].Properties["Name"].Refactor.Rename("Label");

                Helper.AssertThrows<ArgumentOutOfRangeException>(delegate ()
                {
                    Property name = Entities["AccountType"].Properties["Name"];
                });

                // Convert
                Helper.AssertThrows<InvalidCastException>(delegate ()
                {
                    Entities["AccountType"].Properties["Label"].Refactor.Convert(typeof(List<string>), true);
                });

                Entities["ContactStatus"].Properties["OrderBy"].Refactor.Convert(typeof(int), true);
                Assert.IsTrue(Entities["ContactStatus"].Properties["OrderBy"].SystemReturnType == typeof(int));

                // SetIndexType
                Entities["ContactStatus"].Properties["OrderBy"].Refactor.SetIndexType(IndexType.Unique);
                Assert.IsTrue(Entities["ContactStatus"].Properties["OrderBy"].IndexType == IndexType.Unique);

                // Nullable
                Entities["AccountType"].Properties["Label"].Refactor.MakeNullable();
                Assert.IsTrue(Entities["AccountType"].Properties["Label"].Nullable);

                Helper.AssertThrows<NotSupportedException>(delegate ()
                {
                    Entities["AccountType"].Properties["Label"].Refactor.MakeNullable();

                }, "The property is already nullable.");

                // Mandatory
                Entities["AccountType"].Properties["Label"].Refactor.MakeMandatory();
                Helper.AssertThrows<NotSupportedException>(delegate ()
                {
                    Entities["AccountType"].Properties["Label"].Refactor.MakeMandatory();

                }, "The property is already mandatory.");


                Entities.New("RelationshipOne")
                     .AddProperty("Name", typeof(string));

                Entities.New("RelationshipTwo")
                    .AddProperty("Name", typeof(string));

                Relations.New(Entities["RelationshipOne"], Entities["RelationshipTwo"], "HAS_RELATIONSHIP", "HAS")
                    .SetInProperty("InRels", PropertyType.Collection)
                    .SetOutProperty("OutRels", PropertyType.Lookup);

                // Relationship IndexType
                Assert.Throws<NotImplementedException>(delegate ()
                {
                    Relations["HAS_RELATIONSHIP"].InProperty.Refactor.SetIndexType(IndexType.Unique);
                });

                // Relationship Rename
                Relations["HAS_RELATIONSHIP"].InProperty.Refactor.Rename("InEntityRels");
                Assert.IsTrue(Relations["HAS_RELATIONSHIP"].InProperty.Name == "InEntityRels");

                // Relaionship ConvertToLookup / ConvertToCollection
                Assert.Throws<NotImplementedException>(delegate ()
                {
                    Relations["HAS_RELATIONSHIP"].InProperty.Refactor.ConvertToLookup(ConvertAlgorithm.TakeFirst);
                });

                Relations["HAS_RELATIONSHIP"].OutProperty.Refactor.ConvertToCollection();
                Assert.IsTrue(Relations["HAS_RELATIONSHIP"].OutProperty.PropertyType == PropertyType.Collection);
            }
        }

        [Test]
        public void RefactorProperties()
        {
            DataModel model = new DataModel();
            model.Execute(false);
        }
    }
}
