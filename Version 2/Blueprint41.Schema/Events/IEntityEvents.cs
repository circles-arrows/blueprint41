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
    }
}
