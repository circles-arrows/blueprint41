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

        void Move(Entity target);
        //void Move(string pattern, string newPropertyName);

        void Merge(Property target, MergeAlgorithm mergeAlgorithm);


        void ToCompressedString(int batchSize = 100);
        void Convert(Type target, bool skipConvertionLogic = false);

        void SetIndexType(IndexType indexType);

        void Deprecate();

        void Reroute(string pattern, string newPropertyName, bool strict = true);
        void Reroute(string pattern, string newPropertyName, string newRelationshipName, string newNeo4jRelationshipType = null, bool strict = true);

        void ConvertToCollection();
        void ConvertToCollection(string newName);
        void ConvertToLookup(ConvertAlgorithm conversionAlgorithm);
        void ConvertToLookup(string newName, ConvertAlgorithm conversionAlgorithm);

        void MakeNullable();
        void MakeMandatory();
        void MakeMandatory(object defaultValue);
        void MakeMandatory(DynamicEntity defaultValue);
    }
}
