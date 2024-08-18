#nullable disable

using Blueprint41.Sync.Core;
using Blueprint41.Sync.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync.Neo4j.Refactoring
{
    public partial interface IRefactorProperty
    {
        /// <summary>
        /// Renames the property
        /// </summary>
        /// <param name="newName">The new name</param>
        void Rename(string newName);

        /// <summary>
        /// Merges two properties according to the chosen algorithm
        /// </summary>
        /// <param name="target">The target property</param>
        /// <param name="mergeAlgorithm">The merge algorithm</param>
        [RestrictedTo(PropertyType.Attribute)]
        void Merge(Property target, MergeAlgorithm mergeAlgorithm);

        /// <summary>
        /// Converts the string property to a compressed-string property
        /// </summary>
        /// <param name="batchSize">The batch size for applying the conversion to the data-store</param>
        [RestrictedTo(PropertyType.Attribute)]
        void ToCompressedString(int batchSize = 100);

        /// <summary>
        /// Converts the property to a different type
        /// </summary>
        /// <param name="target">The target type</param>
        /// <param name="skipConversionLogic">Whether to skip the conversion logic</param>
        [RestrictedTo(PropertyType.Attribute)]
        void Convert(Type target, bool skipConversionLogic = false);

        /// <summary>
        /// Sets the indexing for the property
        /// </summary>
        /// <param name="indexType">The index type</param>
        [RestrictedTo(PropertyType.Attribute)]
        void SetIndexType(IndexType indexType);

        /// <summary>
        /// Removes the property from the data model and deletes the existing data in the database
        /// </summary>
        void Deprecate();

        /// <summary>
        /// Removes the property from the data model, but leaves the current data in the database.
        /// (Use Refactor.Deprecate() if you also want the data to be deleted)
        /// </summary>
        void Remove();

        /// <summary>
        /// Make the property nullable
        /// </summary>
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void MakeNullable();

        /// <summary>
        /// Make the property mandatory, throw an error if there are any instances where the value is currently NULL
        /// </summary>
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void MakeMandatory();

        /// <summary>
        /// Make the property mandatory and set the default value for instances where the value is currently NULL
        /// </summary>
        /// <param name="defaultValue">The default value</param>
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void MakeMandatory(object defaultValue);

        /// <summary>
        /// Set the default value for instances where the value is currently NULL
        /// </summary>
        /// <param name="defaultValue">The default value</param>
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void SetDefaultValue(object defaultValue);
    }

    public partial interface IRefactorEntityProperty : IRefactorProperty
    {
        /// <summary>
        /// Moves the property to a base or sub-class
        /// </summary>
        /// <param name="target">The target class</param>
        [RestrictedTo(PropertyType.Attribute)]
        void Move(Entity target);

        /// <summary>
        /// Moves/duplicates the property to every sub-class of the current class
        /// </summary>
        [RestrictedTo(PropertyType.Attribute)]
        void MoveToSubClasses();

        /// <summary>
        /// Move a relationship property to another node and migrate the existing relations according to the supplied pattern
        /// </summary>
        /// <param name="pattern">A pattern in the format: (from:SourceNode)-[:RELATION]-(to:TargetNode)</param>
        /// <param name="newPropertyName">The new property name</param>
        /// <param name="strict">Whether potential loss of data/relationships is allowed</param>
        [RestrictedTo(PropertyType.Lookup, PropertyType.Collection)]
        void Reroute(string pattern, string newPropertyName, bool strict = true);

        /// <summary>
        /// Move a relationship property to another node and migrate the existing relations according to the supplied pattern
        /// (You can rename the relationship at the same time)
        /// </summary>
        /// <param name="pattern">A pattern in the format: (from:SourceNode)-[:RELATION]-(to:TargetNode)</param>
        /// <param name="newPropertyName">The new property name</param>
        /// <param name="newRelationshipName">The new relationship name</param>
        /// <param name="newNeo4jRelationshipType">The new neo4j relationship type</param>
        /// <param name="strict">Whether potential loss of data/relationships is allowed</param>
        [RestrictedTo(PropertyType.Lookup, PropertyType.Collection)]
        void Reroute(string pattern, string newPropertyName, string newRelationshipName, string newNeo4jRelationshipType = null, bool strict = true);

        /// <summary>
        /// Convert a Lookup to a Collection
        /// </summary>
        [RestrictedTo(PropertyType.Lookup)]
        void ConvertToCollection();

        /// <summary>
        /// Convert a Lookup to a Collection
        /// </summary>
        /// <param name="newName">The new property name</param>
        [RestrictedTo(PropertyType.Lookup)]
        void ConvertToCollection(string newName);

        /// <summary>
        /// Convert a Collection to a Lookup
        /// </summary>
        [RestrictedTo(PropertyType.Lookup)]
        void ConvertToLookup(ConvertAlgorithm conversionAlgorithm);

        /// <summary>
        /// Convert a Collection to a Lookup
        /// </summary>
        /// <param name="newName">The new property name</param>
        /// <param name="conversionAlgorithm">The conversion algorithm</param>
        [RestrictedTo(PropertyType.Lookup)]
        void ConvertToLookup(string newName, ConvertAlgorithm conversionAlgorithm);

        /// <summary>
        /// Make the property mandatory and set the default lookup value for instances where the value is currently NULL
        /// </summary>
        /// <param name="defaultValue">The default value</param>
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void MakeMandatory(DynamicEntity defaultValue);
    }
}
