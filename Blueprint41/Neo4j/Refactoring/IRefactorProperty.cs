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

        void CopyValue(Property target);

        void Move(Entity target);
        //void Move(string pattern, string newPropertyName);

        void Merge(Property target, MergeAlgorithm mergeAlgorithm);


        void Convert(Type target);

        void SetIndexType(IndexType indexType);

        void Deprecate();

        void Reroute(string pattern, string newPropertyName);
        void Reroute(string pattern, string newPropertyName, string newRelationshipName, string newNeo4jRelationshipType = null);

        void ConvertToCollection();
        void ConvertToLookup(ConvertAlgorithm conversionAlgorithm);

        void MakeNullable();
        void MakeMandatory();
        void MakeMandatory(object defaultValue);
        void MakeMandatory(DynamicEntity defaultValue);
    }
}
