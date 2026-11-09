using System;
using System.Collections.Generic;

namespace Blueprint41.Events
{
    public interface IRelationshipEvents // TODO: Remove Event
    {
        /// <summary>
        /// This event fires during the commit stage, when a new relationship is about to be created
        /// </summary>
        event EventHandler<RelationshipEventArgs> OnRelationCreate; // TODO: Remove Event
        bool HasRegisteredOnRelationCreateHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, after the relationship was created. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<RelationshipEventArgs> OnRelationCreated; // TODO: Remove Event
        bool HasRegisteredOnRelationCreatedHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing relationship is about to be deleted
        /// </summary>
        event EventHandler<RelationshipEventArgs> OnRelationDelete; // TODO: Remove Event
        bool HasRegisteredOnRelationDeleteHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing relationship was deleted. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<RelationshipEventArgs> OnRelationDeleted; // TODO: Remove Event
        bool HasRegisteredOnRelationDeletedHandlers { get; }
    }
}
