using System;
using System.Collections.Generic;
using System.Text;

using Blueprint41.Events;
using Blueprint41.Query;

namespace Blueprint41.Core
{
    public interface IEntityAdvancedFeatures
    {
        /// <summary>
        /// Create a new node
        /// </summary>
        /// <param name="eventOptions">Which events should be fired during the creation</param>
        /// <returns>The new node</returns>
        OGM Activator(EventOptions eventOptions = EventOptions.GraphEvents);

        /// <summary>
        /// Map a node loaded via a query into a new node instance
        /// </summary>
        /// <param name="node">The raw cypher node</param>
        /// <param name="mappingMode">The node mapping mode</param>
        /// <returns>The new node</returns>
        OGM? Map(RawNode node, NodeMapping mappingMode);

        /// <summary>
        /// Map a node loaded via a query into a new node instance
        /// </summary>
        /// <param name="node">The raw cypher node</param>
        /// <param name="cypher">The cypher query</param>
        /// <param name="parameters">The cypher query parameters</param>
        /// <param name="mappingMode">The node mapping mode</param>
        /// <returns>The new node</returns>
        OGM? Map(RawNode node, string cypher, Dictionary<string, object?>? parameters, NodeMapping mappingMode);

        /// <summary>
        /// Load an existing node
        /// </summary>
        /// <param name="key">The primary key value for the node to be loaded</param>
        /// <param name="locked">If the node should be locked</param>
        /// <returns>The node</returns>
        OGM? Load(object? key, bool locked = false);

        /// <summary>
        /// Load an existing node from the transaction cache
        /// </summary>
        /// <param name="key">The primary key value for the node to be loaded</param>
        /// <returns>The node</returns>
        OGM? Lookup(object? key);

        /// <summary>
        /// Force-delete an existing node
        /// </summary>
        /// <param name="key">The primary key value for the node to be deleted</param>
        void ForceDelete(object key);

        /// <summary>
        /// Delete an existing node
        /// </summary>
        /// <param name="key">The primary key value for the node to be deleted</param>
        void Delete(object key);
    }
}
