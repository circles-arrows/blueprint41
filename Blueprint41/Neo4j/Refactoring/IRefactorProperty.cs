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
    public interface IRefactorProperty
    {
        void Rename(string newName);

        [RestrictedTo(PropertyType.Attribute)]
        void Move(Entity target);
        //void Move(string pattern, string newPropertyName);

        [RestrictedTo(PropertyType.Attribute)]
        void Merge(Property target, MergeAlgorithm mergeAlgorithm);

        [RestrictedTo(PropertyType.Attribute)]
        void ToCompressedString(int batchSize = 100);

        [RestrictedTo(PropertyType.Attribute)]
        void Convert(Type target, bool skipConvertionLogic = false);

        [RestrictedTo(PropertyType.Attribute)]
        void SetIndexType(IndexType indexType);

        void Deprecate();

        [RestrictedTo(PropertyType.Lookup, PropertyType.Collection)]
        void Reroute(string pattern, string newPropertyName, bool strict = true);
        [RestrictedTo(PropertyType.Lookup, PropertyType.Collection)]
        void Reroute(string pattern, string newPropertyName, string newRelationshipName, string newNeo4jRelationshipType = null, bool strict = true);

		[RestrictedTo(PropertyType.Lookup)]
        void ConvertToCollection();
		[RestrictedTo(PropertyType.Lookup)]
        void ConvertToCollection(string newName);
		[RestrictedTo(PropertyType.Lookup)]
        void ConvertToLookup(ConvertAlgorithm conversionAlgorithm);
		[RestrictedTo(PropertyType.Lookup)]
        void ConvertToLookup(string newName, ConvertAlgorithm conversionAlgorithm);

        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void MakeNullable();
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void MakeMandatory();
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void MakeMandatory(object defaultValue);
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void MakeMandatory(DynamicEntity defaultValue);
        [RestrictedTo(PropertyType.Attribute, PropertyType.Lookup)]
        void SetDefaultValue(object defaultValue);
    }
}
