using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public interface IEntityEvents
    {
        /// <summary>
        /// This event fires while instantiating the object
        /// </summary>
        event EventHandler<EntityEventArgs> OnNew;
        bool HasRegisteredOnNewHandlers { get; }

        /// <summary>
        /// This event fires while calling the Delete method
        /// </summary>
        event EventHandler<EntityEventArgs> OnDelete;
        bool HasRegisteredOnDeleteHandlers { get; }

        /// <summary>
        /// This event fires while calling the save internal method
        /// </summary>
        event EventHandler<EntityEventArgs> OnSave;
        bool HasRegisteredOnSaveHandlers { get; }

        /// <summary>
        /// This event fires while calling the after save internal method
        /// </summary>
        event EventHandler<EntityEventArgs> OnAfterSave;
        bool HasRegisteredOnAfterSaveHandlers { get; }

        /// <summary>
        /// This event fires when a node is about to be loaded into memory
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeLoading;
        bool HasRegisteredOnNodeLoadingHandlers { get; }

        /// <summary>
        /// This event fires after a node was loaded into memory
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeLoaded;
        bool HasRegisteredOnNodeLoadedHandlers { get; }

        /// <summary>
        /// This event fires after a batch of nodes finished loading
        /// </summary>
        event EventHandler<NodeEventArgs> OnBatchFinished;
        bool HasRegisteredOnBatchFinishedHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when a new node is about to be created
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeCreate;
        bool HasRegisteredOnNodeCreateHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, after the node was created. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeCreated;
        bool HasRegisteredOnNodeCreatedHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing node is about to be updated
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeUpdate;
        bool HasRegisteredOnNodeUpdateHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing node was updated. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeUpdated;
        bool HasRegisteredOnNodeUpdatedHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing node is about to be deleted
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeDelete;
        bool HasRegisteredOnNodeDeleteHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing node was deleted. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<NodeEventArgs> OnNodeDeleted;
        bool HasRegisteredOnNodeDeletedHandlers { get; }
    }

    public interface IPropertyEvents
    {
        /// <summary>
        /// This event fires while setting the property
        /// </summary>
        event EventHandler<PropertyEventArgs> OnChange;
        bool HasRegisteredChangeHandlers { get; }
    }
}