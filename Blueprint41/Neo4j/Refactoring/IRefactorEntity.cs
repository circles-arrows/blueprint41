using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Blueprint41.Dynamic;

namespace Blueprint41.Neo4j.Refactoring
{
    public interface IRefactorEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newName"></param>
        void Rename(string newName);

        void SetDefaultValue(Action<dynamic> values);
        void CopyValue(string source, string target);

        void ApplyLabels();
        void ChangeInheritance(Entity entity);
        void ApplyConstraints();
        void CreateIndexes();

        dynamic CreateNode(object node);
        dynamic? MatchNode(object key);
        void DeleteNode(object key);

        void Deprecate();

        /// <summary>
        /// Remove the FunctionalId pattern assigned to the property. Previously assigned primary key values (if any) will not be reset.
        /// </summary>
        void ResetFunctionalId();

        /// <summary>
        /// Assign a new FunctionalId pattern to the property. Previously assigned primary key values (if any) will be set according to the chosen algorithm.
        /// </summary>
        void SetFunctionalId(FunctionalId functionalId, ApplyAlgorithm algorithm = ApplyAlgorithm.DoNotApply);
    }
}
