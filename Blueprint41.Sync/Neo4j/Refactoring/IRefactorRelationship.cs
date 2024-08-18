#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync.Neo4j.Refactoring
{
    public partial interface IRefactorRelationship
    {
        /// <summary>
        /// Renames the relationship
        /// </summary>
        /// <param name="newName">The new name</param>
        /// <param name="newNeo4JRelationshipType">The new neo4j relationship type</param>
        void Rename(string newName, string newNeo4JRelationshipType);

        /// <summary>
        /// Sets the in-entity
        /// </summary>
        /// <param name="target">The new in-entity</param>
        /// <param name="allowLosingData">Whether to allow losing data</param>
        void SetInEntity(Entity target, bool allowLosingData = false);

        /// <summary>
        /// Sets the out-entity
        /// </summary>
        /// <param name="target">The new out-entity</param>
        /// <param name="allowLosingData">Whether to allow losing data</param>
        void SetOutEntity(Entity target, bool allowLosingData = false);

        /// <summary>
        /// Removes the time dependence
        /// </summary>
        void RemoveTimeDependence();

        /// <summary>
        /// Deprecates the relationship
        /// </summary>
        void Deprecate();
    }
}
