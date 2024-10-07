#pragma warning disable CS8981 // Names should not be lower type only

using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;
using Blueprint41.UnitTest.DataStore;

using Datastore.Manipulation;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ClientException = Neo4j.Driver.ClientException;
using DatabaseException = Neo4j.Driver.DatabaseException;

namespace Blueprint41.UnitTest.Tests
{
    public partial class TestRelationships
    {
        [Test] // Asserts done
        public void RenameProperty()
        {
            SetupTestDataSet();

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.That(relations.Any(rel => rel.properties.ContainsKey(nameof(PERSON_LIVES_IN.AddressLine1))), "Unexpected test-data");
                Assert.That(relations.All(rel => !rel.properties.ContainsKey("NewName")), "Unexpected test-data");
            }

            Execute(RenameAddrLine1);

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.That(relations.Any(rel => rel.properties.ContainsKey("NewName")), "There should have been 'NewName' properties.");
                Assert.That(relations.All(rel => !rel.properties.ContainsKey(nameof(PERSON_LIVES_IN.AddressLine1))), "There are no 'AddressLine1' properties.");
            }
        }

        [Test] // Asserts done
        public void MergePropertyPreferSource()
        {
            /*

"AddressLine1": "Apt. 56B Whitehaven Mansions",
"AddressLine2": "Sandhurst Square",

"AddressLine1": "1640 Riverside Drive",

            */
            SetupTestDataSet();

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null)?.ToString() == "1640 Riverside Drive"), "Unexpected test-data");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null)?.ToString() == "Apt. 56B Whitehaven Mansions"), "Unexpected test-data");
                Assert.That(!relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null)?.ToString() == "Sandhurst Square"), "Unexpected test-data");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "Sandhurst Square"), "Unexpected test-data");
            }

            Execute(MergeAddrLine1And2IntoAddrLine1);

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null)?.ToString() == "1640 Riverside Drive"), "There should have been 'AddressLine1' properties with value '1640 Riverside Drive'.");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null)?.ToString() == "Sandhurst Square"), "There should have been 'AddressLine1' properties with value 'Sandhurst Square'.");
                Assert.That(!relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null)?.ToString() == "Apt. 56B Whitehaven Mansions"), "There should not have been 'AddressLine1' properties with value 'Apt. 56B Whitehaven Mansions'.");
                Assert.That(!relations.Exists(rel => rel.properties.ContainsKey(nameof(PERSON_LIVES_IN.AddressLine2))), "There should not have been 'AddressLine2' properties.");
            }
        }

        [Test] // Asserts done
        public void MergePropertyPreferTarget()
        {
            /*

"AddressLine1": "Apt. 56B Whitehaven Mansions",
"AddressLine2": "Sandhurst Square",

"AddressLine1": "1640 Riverside Drive",

            */
            SetupTestDataSet();

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null)?.ToString() == "1640 Riverside Drive"), "Unexpected test-data");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null)?.ToString() == "Apt. 56B Whitehaven Mansions"), "Unexpected test-data");
                Assert.That(!relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null)?.ToString() == "Sandhurst Square"), "Unexpected test-data");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "Sandhurst Square"), "Unexpected test-data");
            }

            Execute(MergeAddrLine1And2IntoAddrLine2);

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(!relations.Exists(rel => rel.properties.ContainsKey(nameof(PERSON_LIVES_IN.AddressLine1))), "There should not have been 'AddressLine1' properties.");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "1640 Riverside Drive"), "There should have been 'AddressLine2' properties with value '1640 Riverside Drive'.");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "Sandhurst Square"), "There should have been 'AddressLine2' properties with value 'Sandhurst Square'.");
                Assert.That(!relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "Apt. 56B Whitehaven Mansions"), "There should not have been 'AddressLine2' properties with value 'Apt. 56B Whitehaven Mansions'.");
            }
        }

        [Test] // Asserts done
        public void MergeAddrLine1And2AndFail()
        {
            SetupTestDataSet();

            Assert.Throws<InvalidOperationException>(() => Execute(MergeAddrLine1And2AndThrow), "MergeProperty detected 3 conflicts.");
        }

        [Test] // Asserts done
        public void ToCompressedString()
        {
            SetupTestDataSet();

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null) is string), "Unexpected test-data");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null) is not byte[]), "Unexpected test-data");
            }

#if NEO4J
            Execute(CompressAddrLine1);

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null) is not string), "Conversion failed");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null) is byte[]), "Conversion failed");
            }
#elif MEMGRAPH
            Exception ex = Assert.Throws<InvalidOperationException>(() => Execute(CompressAddrLine1));
            Assert.That(() => ex.Message.Contains("CompressedString is not supported on Memgraph, since ByteArray is not supported on Memgraph."));
#endif
        }

        [Test] // Asserts done
        public void Convert()
        {
            SetupTestDataSet();

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(WATCHED_MOVIE.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(WATCHED_MOVIE.MinutesWatched), null) is long), "Unexpected test-data");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(WATCHED_MOVIE.MinutesWatched), null) is not string), "Unexpected test-data");
            }

            Execute(ConvertMinsWatchedToString);

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(WATCHED_MOVIE.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(WATCHED_MOVIE.MinutesWatched), null) is not long), "Conversion failed");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(WATCHED_MOVIE.MinutesWatched), null) is string), "Conversion failed");
            }
        }

        [Test] // Asserts done
        public void SetIndexTypeToIndex()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            SetupTestDataSet();

            var schema = ((IDatastoreUnitTesting)MockModel.Model).GetSchemaInfo();
            Assert.That(!schema.Indexes.Any(index => index.IsIndexed && !index.IsUnique && index.Entity.Count == 1 && index.Entity[0] == "LIVES_IN" && index.Field.Count == 1 && index.Field[0] == "AddressLine1"));
            Assert.That(!schema.Constraints.Any(constraint => !constraint.IsMandatory && constraint.IsUnique && !constraint.IsKey && constraint.Entity.Count == 1 && constraint.Entity[0] == "LIVES_IN" && constraint.Field.Count == 1 && constraint.Field[0] == "AddressLine1"));

            Execute(IndexAddrLine1);

            schema = ((IDatastoreUnitTesting)MockModel.Model).GetSchemaInfo();
#if NEO4J
            Assert.That(schema.Indexes.Any(index => index.IsIndexed && !index.IsUnique && index.Entity.Count == 1 && index.Entity[0] == "LIVES_IN" && index.Field.Count == 1 && index.Field[0] == "AddressLine1"));
            Assert.That(!schema.Constraints.Any(constraint => !constraint.IsMandatory && constraint.IsUnique && !constraint.IsKey && constraint.Entity.Count == 1 && constraint.Entity[0] == "LIVES_IN" && constraint.Field.Count == 1 && constraint.Field[0] == "AddressLine1"));
#elif MEMGRAPH
            Assert.That(!schema.Indexes.Any(index => index.IsIndexed && !index.IsUnique && index.Entity.Count == 1 && index.Entity[0] == "LIVES_IN" && index.Field.Count == 1 && index.Field[0] == "AddressLine1"));
            Assert.That(!schema.Constraints.Any(constraint => !constraint.IsMandatory && constraint.IsUnique && !constraint.IsKey && constraint.Entity.Count == 1 && constraint.Entity[0] == "LIVES_IN" && constraint.Field.Count == 1 && constraint.Field[0] == "AddressLine1"));
#endif
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [Test] // Asserts done
        public void SetIndexTypeToUnique()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            //var persistenceProvider = PersistenceProvider.CurrentPersistenceProvider as driver.Neo4jPersistenceProvider;
            //if (persistenceProvider is null || !persistenceProvider.VersionGreaterOrEqual(5, 7))
                throw new NotSupportedException("Run this test on Neo4j 5.7 or greater.");

            SetupTestDataSet();

            var schema = ((IDatastoreUnitTesting)MockModel.Model).GetSchemaInfo();
            Assert.That(!schema.Indexes.Any(index => index.IsIndexed && !index.IsUnique && index.Entity.Count == 1 && index.Entity[0] == "LIVES_IN" && index.Field.Count == 1 && index.Field[0] == "AddressLine1"));
            Assert.That(!schema.Constraints.Any(constraint => !constraint.IsMandatory && constraint.IsUnique && !constraint.IsKey && constraint.Entity.Count == 1 && constraint.Entity[0] == "LIVES_IN" && constraint.Field.Count == 1 && constraint.Field[0] == "AddressLine1"));

#if NEO4J
            var ex = Assert.Throws<AggregateException>(() => Execute(UniqueAddrLine1));
#if NET5_0_OR_GREATER
            Assert.That(ex.Message.Contains("Unable to create Constraint( name='LIVES_IN_AddressLine1_UniqueConstraint', type='RELATIONSHIP UNIQUENESS', schema=()-[:LIVES_IN {AddressLine1}]-() )"));
#else
            Assert.That(ex.InnerException.Message.Contains("Unable to create Constraint( name='LIVES_IN_AddressLine1_UniqueConstraint', type='RELATIONSHIP UNIQUENESS', schema=()-[:LIVES_IN {AddressLine1}]-() )"));
#endif
#elif MEMGRAPH
            Assert.DoesNotThrow(() => Execute(UniqueAddrLine1));
            
            schema = ((IDatastoreUnitTesting)MockModel.Model).GetSchemaInfo();
            Assert.That(!schema.Indexes.Any(index => index.IsIndexed && !index.IsUnique && index.Entity.Count == 1 && index.Entity[0] == "LIVES_IN" && index.Field.Count == 1 && index.Field[0] == "AddressLine1"));
            Assert.That(!schema.Constraints.Any(constraint => !constraint.IsMandatory && constraint.IsUnique && !constraint.IsKey && constraint.Entity.Count == 1 && constraint.Entity[0] == "LIVES_IN" && constraint.Field.Count == 1 && constraint.Field[0] == "AddressLine1"));
#endif
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [Test] // Asserts done
        public void Deprecate()
        {
            SetupTestDataSet();

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null) != null), "Unexpected test-data");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine3), null) != null), "Unexpected test-data");
            }

            Execute(DeprecateAddrLine2And3);

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(!relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null) != null), "There should not have been 'AddressLine3' properties.");
                Assert.That(!relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine3), null) != null), "There should not have been 'AddressLine3' properties.");
            }
        }

        [Test] // Asserts done
        public void MakeNullable()
        {
            using (MockModel.BeginTransaction())
            {
                var watched = SampleDataWatchedMovies().First();
                watched.person.WatchedMovies.Add(watched.movie);

#if NEO4J
                Exception ex = Assert.Throws<AggregateException>(() => Transaction.Commit());
#if NET5_0_OR_GREATER
                Assert.That(() => ex.Message.Contains("`WATCHED` must have the property `MinutesWatched`"));
#else
                Assert.That(() => ex.InnerException.Message.Contains("`WATCHED` must have the property `MinutesWatched`"));
#endif
#elif MEMGRAPH
                Assert.DoesNotThrow(() => Transaction.Commit());
#endif
            }

            Execute(MakeMinutesWatchedNullable);

            using (MockModel.BeginTransaction())
            {
                var watched = SampleDataWatchedMovies().First();
                watched.person.WatchedMovies.Add(watched.movie);

                Assert.DoesNotThrow(() => Transaction.Commit());
            }
        }

        [Test] // Asserts done
        public void MakeMandatoryWithoutDefaultNoThrow()
        {
            SetupTestDataSet();

#if NEO4J
            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine1), null)?.ToString() == "1640 Riverside Drive"), "There should have been 'AddressLine1' properties with value '1640 Riverside Drive'.");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "Sandhurst Square"), "There should have been 'AddressLine2' properties with value 'Sandhurst Square'.");
            }

            Execute(MakeAddrLine1Mandatory);

            var schema = ((IDatastoreUnitTesting)MockModel.Model).GetSchemaInfo();
            Assert.That(schema.Constraints.Any(constraint => constraint.IsMandatory && !constraint.IsUnique && !constraint.IsKey && constraint.Entity.Count == 1 && constraint.Entity[0] == "LIVES_IN" && constraint.Field.Count == 1 && constraint.Field[0] == "AddressLine1"));
#elif MEMGRAPH
            Assert.DoesNotThrow(() => Execute(MakeAddrLine1Mandatory));
#endif
        }

        [Test] // Asserts done
        public void MakeMandatoryWithDefaultNoThrow()
        {
            SetupTestDataSet();

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null) == null), "Unexpected test-data");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "Sandhurst Square"), "Unexpected test-data");
            }

            Execute(MakeAddrLine2MandatoryWithDefault);

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(!relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null) == null), "There should not have been 'AddressLine2' properties with value NULL.");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "DEFAULT"), "There should have been 'AddressLine2' properties with value 'DEFAULT'.");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "Sandhurst Square"), "There should have been 'AddressLine2' properties with value 'Sandhurst Square'.");
            }
        }

        [Test] // Asserts done
        public void MakeMandatoryWithoutDefaultAndThrow()
        {
            SetupTestDataSet();

            Exception ex = Assert.Throws<InvalidOperationException>(() => Execute(MakeAddrLine3MandatoryAndThrow));
            Assert.That(ex.Message.Contains("Some nodes in the database contains null values for PERSON_LIVES_IN.AddressLine3."));
        }

        [Test] // Asserts done
        public void SetDefaultValue()
        {
            SetupTestDataSet();

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null) == null), "Unexpected test-data");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "Sandhurst Square"), "Unexpected test-data");
            }

            Execute(SetAddrLine3FromNullToDEFAULT);

            using (MockModel.BeginTransaction())
            {
                var relations = ReadAllRelations(PERSON_LIVES_IN.Relationship);
                Assert.IsNotNull(relations);

                Assert.That(!relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null) == null), "There should not have been 'AddressLine2' properties with value NULL.");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "DEFAULT"), "There should have been 'AddressLine2' properties with value 'DEFAULT'.");
                Assert.That(relations.Exists(rel => rel.properties!.GetValue(nameof(PERSON_LIVES_IN.AddressLine2), null)?.ToString() == "Sandhurst Square"), "There should have been 'AddressLine2' properties with value 'Sandhurst Square'.");
            }
        }
    }
}
