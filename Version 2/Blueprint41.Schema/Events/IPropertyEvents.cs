using System;
using System.Collections.Generic;

namespace Blueprint41.Events
{
    public interface IPropertyEvents
    {
        /// <summary>
        /// This event fires while setting the property
        /// </summary>
        event EventHandler<PropertyEventArgs> OnChange;
        /// <summary>
        /// True when a OnChange event is registered
        /// </summary>
        bool HasRegisteredChangeHandlers { get; }
    }
}