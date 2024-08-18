using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync.Core
{
    public interface IRelationshipEvents
    {
        /// <summary>
        /// This event fires during the commit stage, when a new relationship is about to be created
        /// </summary>
        event EventHandler<RelationshipEventArgs> OnRelationCreate;
        bool HasRegisteredOnRelationCreateHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, after the relationship was created. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<RelationshipEventArgs> OnRelationCreated;
        bool HasRegisteredOnRelationCreatedHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing relationship is about to be deleted
        /// </summary>
        event EventHandler<RelationshipEventArgs> OnRelationDelete;
        bool HasRegisteredOnRelationDeleteHandlers { get; }

        /// <summary>
        /// This event fires during the commit stage, when an existing relationship was deleted. The transaction is read-only at this moment.
        /// </summary>
        event EventHandler<RelationshipEventArgs> OnRelationDeleted;
        bool HasRegisteredOnRelationDeletedHandlers { get; }
    }
}
