using System;
using System.Collections.Generic;

namespace Blueprint41.Events
{
    public interface IEntityEvents
    {
        /// <summary>
        /// This event fires while instantiating the object
        /// </summary>
        event EventHandler<EntityEventArgs> OnNew;
        /// <summary>
        /// True when a OnNew event is registered
        /// </summary>
        bool HasRegisteredOnNewHandlers { get; }

        /// <summary>
        /// This event fires while calling the save internal method
        /// </summary>
        event EventHandler<EntityEventArgs> OnSave;
        /// <summary>
        /// True when a OnSave event is registered
        /// </summary>
        bool HasRegisteredOnSaveHandlers { get; }

        /// <summary>
        /// This event fires while calling the after save internal method
        /// </summary>
        event EventHandler<EntityEventArgs> OnAfterSave;
        /// <summary>
        /// True when a OnAfterSave event is registered
        /// </summary>
        bool HasRegisteredOnAfterSaveHandlers { get; }

        /// <summary>
        /// This event fires while calling the Delete method
        /// </summary>
        event EventHandler<EntityEventArgs> OnDelete;
        /// <summary>
        /// True when a OnDelete event is registered
        /// </summary>
        bool HasRegisteredOnDeleteHandlers { get; }

        /// <summary>
        /// This event fires when a node is about to be loaded into memory
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeLoading;
        /// <summary>
        /// True when a OnNodeLoading event is registered
        /// </summary>
        bool HasRegisteredOnNodeLoadingHandlers { get; }

        /// <summary>
        /// This event fires after a node was loaded into memory
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeLoaded;
        /// <summary>
        /// True when a OnNodeLoaded event is registered
        /// </summary>
        bool HasRegisteredOnNodeLoadedHandlers { get; }

        /// <summary>
        /// This event fires after a batch command, like loading or deleting a collection, was finished
        /// </summary>
        event EventHandler<NodeEventArgs> OnBatchFinished;
        /// <summary>
        /// True when a OnBatchFinished event is registered
        /// </summary>
        bool HasRegisteredOnBatchFinishedHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when a new node is about to be created
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeCreate;
        /// <summary>
        /// True when a OnNodeCreate event is registered
        /// </summary>
        bool HasRegisteredOnNodeCreateHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, after the node was created. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeCreated;
        /// <summary>
        /// True when a OnNodeCreated event is registered
        /// </summary>
        bool HasRegisteredOnNodeCreatedHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing node is about to be updated
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeUpdate;
        /// <summary>
        /// True when a OnNodeUpdate event is registered
        /// </summary>
        bool HasRegisteredOnNodeUpdateHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing node was updated. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeUpdated;
        /// <summary>
        /// True when a OnNodeUpdated event is registered
        /// </summary>
        bool HasRegisteredOnNodeUpdatedHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing node is about to be deleted
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeDelete;
        /// <summary>
        /// True when a OnNodeDelete event is registered
        /// </summary>
        bool HasRegisteredOnNodeDeleteHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing node was deleted. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeDeleted;
        /// <summary>
        /// True when a OnNodeDeleted event is registered
        /// </summary>
        bool HasRegisteredOnNodeDeletedHandlers { get; }
    }
}