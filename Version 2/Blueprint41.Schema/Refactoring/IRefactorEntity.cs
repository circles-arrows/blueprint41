using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Blueprint41.Dynamic;

namespace Blueprint41.Refactoring
{
    public partial interface IRefactorEntity
    {
        /// <summary>
        /// Rename the entity
        /// </summary>
        /// <param name="newName">The new entity name</param>
        void Rename(string newName);

        /// <summary>
        /// Set default property values for the entity
        /// </summary>
        /// <param name="values">The default property values</param>
        void SetDefaultValue(Action<dynamic> values);
        
        /// <summary>
        /// Copy data from one property to another
        /// </summary>
        /// <param name="sourceProperty">The source property</param>
        /// <param name="targetProperty">The target property</param>
        void CopyValue(string sourceProperty, string targetProperty);

        /// <summary>
        /// (Re-)apply labels for the entity to the data-store
        /// </summary>
        void ApplyLabels();

        /// <summary>
        /// Change the entity inheritance chain
        /// </summary>
        /// <param name="newParentEntity">The new parent entity</param>
        void ChangeInheritance(Entity? newParentEntity);

        /// <summary>
        /// Create a new node
        /// </summary>
        /// <param name="node">The node content</param>
        /// <returns>The newly created node</returns>
        dynamic CreateNode(object node);

        /// <summary>
        /// Load an existing node
        /// </summary>
        /// <param name="key">The primary key value for the node to be loaded</param>
        /// <returns>The node</returns>
        dynamic? MatchNode(object key);

        /// <summary>
        /// Delete an existing node
        /// </summary>
        /// <param name="key">The primary key value for the node to be deleted</param>
        void DeleteNode(object key);

        /// <summary>
        /// Deprecate the entity
        /// </summary>
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
