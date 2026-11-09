using System;
using System.Collections.Generic;

namespace Blueprint41.Events
{
    public interface IPropertyEvents // TODO: Remove Event
    {
        /// <summary>
        /// This event fires while setting the property
        /// </summary>
        event EventHandler<PropertyEventArgs> OnChange; // TODO: Remove Event
        /// <summary>
        /// True when a OnChange event is registered
        /// </summary>
        bool HasRegisteredChangeHandlers { get; }
    }
}