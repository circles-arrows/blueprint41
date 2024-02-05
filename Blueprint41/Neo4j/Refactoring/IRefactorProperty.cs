#nullable disable

using Blueprint41.Core;
using Blueprint41.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Refactoring
{
    public partial interface IRefactorProperty
    {
        void Rename(string newName);

        //void Move(string pattern, string newPropertyName);

        /// <summary>
        /// Merges two properties according to the chosen algorithm
        /// </summary>
        [RestrictedTo(PropertyType.Attribute)]
        void Merge(Property target, MergeAlgorithm mergeAlgorithm);

        /// <summary>
        /// Converts the string property to a compressed-string property
        /// </summary>
        [RestrictedTo(PropertyType.Attribute)]
        void ToCompressedString(int batchSize = 100);

        [RestrictedTo(PropertyType.Attribute)]
        void Convert(Type target, bool skipConvertionLogic = false);

        [RestrictedTo(PropertyType.Attribute)]
        void SetIndexType(IndexType indexType);

        /// <summary>
        /// Removes the property from the data model and deletes the existing data in the database
        /// </summary>
        void Deprecate();

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
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void MakeMandatory(object defaultValue);
        
        /// <summary>
        /// Set the default value for instances where the value is currently NULL
        /// </summary>
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void SetDefaultValue(object defaultValue);
    }

    public partial interface IRefactorEntityProperty : IRefactorProperty
    {
        /// <summary>
        /// Moves the property to a base or sub-class
        /// </summary>
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
        [RestrictedTo(PropertyType.Lookup, PropertyType.Collection)]
        void Reroute(string pattern, string newPropertyName, bool strict = true);
        /// <summary>
        /// Move a relationship property to another node and migrate the existing relations according to the supplied pattern
        /// (You can rename the relationship at the same time)
        /// </summary>
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
        [RestrictedTo(PropertyType.Lookup)]
        void ConvertToLookup(string newName, ConvertAlgorithm conversionAlgorithm);

        /// <summary>
        /// Make the property mandatory and set the default lookup value for instances where the value is currently NULL
        /// </summary>
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void MakeMandatory(DynamicEntity defaultValue);
    }
}
