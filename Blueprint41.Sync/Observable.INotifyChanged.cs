using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Specialized
{
    //
    // Summary:
    //     Notifies listeners of dynamic changes, such as when an item is added and removed
    //     or the whole list is cleared.
    public interface INotifyChanged<T>
    {
        //
        // Summary:
        //     Occurs before the collection changes.
        event NotifyChangedEventHandler<T> BeforeCollectionChanged;

        //
        // Summary:
        //     Occurs after the collection changed.
        event NotifyChangedEventHandler<T> CollectionChanged;
    }

    //
    // Summary:
    //     Represents the method that handles the System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged
    //     event.
    //
    // Parameters:
    //   sender:
    //     The object that raised the event.
    //
    //   e:
    //     Information about the event.
    public delegate void NotifyChangedEventHandler<T>(object sender, NotifyChangedEventArgs<T> e);

    //
    // Summary:
    //     Provides data for the System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged&lt;T&gt;
    //     event.
    public class NotifyChangedEventArgs<T> : EventArgs
    {
        private const string ERROR_MESSAGE = "Constructor supports only the '{0}' action.";

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a System.Collections.Specialized.NotifyCollectionChangedAction.Reset
        //     change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This must be set to System.Collections.Specialized.NotifyCollectionChangedAction.Reset.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action)
        {
            if (action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException(string.Format(ERROR_MESSAGE, nameof(NotifyCollectionChangedAction.Reset)), "action");
            }
            this.InitializeAdd(action, null, -1);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a multi-item change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can be set to System.Collections.Specialized.NotifyCollectionChangedAction.Reset,
        //     System.Collections.Specialized.NotifyCollectionChangedAction.Add, or System.Collections.Specialized.NotifyCollectionChangedAction.Remove.
        //
        //   changedItems:
        //     The items that are affected by the change.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action, T[] changedItems)
        {
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor", "action");
            }
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItems is not null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem", "action");
                }
                this.InitializeAdd(action, null, -1);
                return;
            }
            else
            {
                if (changedItems is null)
                {
                    throw new ArgumentNullException("changedItems");
                }
                this.InitializeAddOrRemove(action, changedItems, -1);
                return;
            }
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a one-item change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can be set to System.Collections.Specialized.NotifyCollectionChangedAction.Reset,
        //     System.Collections.Specialized.NotifyCollectionChangedAction.Add, or System.Collections.Specialized.NotifyCollectionChangedAction.Remove.
        //
        //   changedItem:
        //     The item that is affected by the change.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If action is not Reset, Add, or Remove, or if action is Reset and changedItem
        //     is not null.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action, T changedItem)
        {
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor", "action");
            }
            if (action != NotifyCollectionChangedAction.Reset)
            {
                this.InitializeAddOrRemove(action, new T[]
                {
                    changedItem
                }, -1);
                return;
            }
            if (changedItem is not null)
            {
                throw new ArgumentException("ResetActionRequiresNullItem", "action");
            }
            this.InitializeAdd(action, null, -1);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a multi-item System.Collections.Specialized.NotifyCollectionChangedAction.Replace
        //     change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can only be set to System.Collections.Specialized.NotifyCollectionChangedAction.Replace.
        //
        //   newItems:
        //     The new items that are replacing the original items.
        //
        //   oldItems:
        //     The original items that are replaced.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If action is not Replace.
        //
        //   T:System.ArgumentNullException:
        //     If oldItems or newItems is null.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action, T[] newItems, T[] oldItems)
        {
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException(string.Format(ERROR_MESSAGE, nameof(NotifyCollectionChangedAction.Replace)), "action");
            }
            if (newItems is null)
            {
                throw new ArgumentNullException("newItems");
            }
            if (oldItems is null)
            {
                throw new ArgumentNullException("oldItems");
            }
            this.InitializeMoveOrReplace(action, newItems, oldItems, -1, -1);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a multi-item change or a System.Collections.Specialized.NotifyCollectionChangedAction.Reset
        //     change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can be set to System.Collections.Specialized.NotifyCollectionChangedAction.Reset,
        //     System.Collections.Specialized.NotifyCollectionChangedAction.Add, or System.Collections.Specialized.NotifyCollectionChangedAction.Remove.
        //
        //   changedItems:
        //     The items affected by the change.
        //
        //   startingIndex:
        //     The index where the change occurred.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If action is not Reset, Add, or Remove, if action is Reset and either changedItems
        //     is not null or startingIndex is not -1, or if action is Add or Remove and startingIndex
        //     is less than -1.
        //
        //   T:System.ArgumentNullException:
        //     If action is Add or Remove and changedItems is null.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action, T[] changedItems, int startingIndex)
        {
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor", "action");
            }
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItems is not null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem", "action");
                }
                if (startingIndex != -1)
                {
                    throw new ArgumentException("ResetActionRequiresIndexMinus1", "action");
                }
                this.InitializeAdd(action, null, -1);
                return;
            }
            else
            {
                if (changedItems is null)
                {
                    throw new ArgumentNullException("changedItems");
                }
                if (startingIndex < -1)
                {
                    throw new ArgumentException("IndexCannotBeNegative", "startingIndex");
                }
                this.InitializeAddOrRemove(action, changedItems, startingIndex);
                return;
            }
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a one-item change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can be set to System.Collections.Specialized.NotifyCollectionChangedAction.Reset,
        //     System.Collections.Specialized.NotifyCollectionChangedAction.Add, or System.Collections.Specialized.NotifyCollectionChangedAction.Remove.
        //
        //   changedItem:
        //     The item that is affected by the change.
        //
        //   index:
        //     The index where the change occurred.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If action is not Reset, Add, or Remove, or if action is Reset and either changedItems
        //     is not null or index is not -1.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action, T changedItem, int index)
        {
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor", "action");
            }
            if (action != NotifyCollectionChangedAction.Reset)
            {
                this.InitializeAddOrRemove(action, new T[]
                {
                    changedItem
                }, index);
                return;
            }
            if (changedItem is not null)
            {
                throw new ArgumentException("ResetActionRequiresNullItem", "action");
            }
            if (index != -1)
            {
                throw new ArgumentException("ResetActionRequiresIndexMinus1", "action");
            }
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a one-item System.Collections.Specialized.NotifyCollectionChangedAction.Replace
        //     change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can only be set to System.Collections.Specialized.NotifyCollectionChangedAction.Replace.
        //
        //   newItem:
        //     The new item that is replacing the original item.
        //
        //   oldItem:
        //     The original item that is replaced.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If action is not Replace.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action, T newItem, T oldItem)
        {
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException(string.Format(ERROR_MESSAGE, nameof(NotifyCollectionChangedAction.Replace)), "action");
            }
            this.InitializeMoveOrReplace(action, new T[]
            {
                newItem
            }, new T[]
            {
                oldItem
            }, -1, -1);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a multi-item System.Collections.Specialized.NotifyCollectionChangedAction.Replace
        //     change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can only be set to System.Collections.Specialized.NotifyCollectionChangedAction.Replace.
        //
        //   newItems:
        //     The new items that are replacing the original items.
        //
        //   oldItems:
        //     The original items that are replaced.
        //
        //   startingIndex:
        //     The index of the first item of the items that are being replaced.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If action is not Replace.
        //
        //   T:System.ArgumentNullException:
        //     If oldItems or newItems is null.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action, T[] newItems, T[] oldItems, int startingIndex)
        {
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException(string.Format(ERROR_MESSAGE, nameof(NotifyCollectionChangedAction.Replace)), "action");
            }
            if (newItems is null)
            {
                throw new ArgumentNullException("newItems");
            }
            if (oldItems is null)
            {
                throw new ArgumentNullException("oldItems");
            }
            this.InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a multi-item System.Collections.Specialized.NotifyCollectionChangedAction.Move
        //     change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can only be set to System.Collections.Specialized.NotifyCollectionChangedAction.Move.
        //
        //   changedItems:
        //     The items affected by the change.
        //
        //   index:
        //     The new index for the changed items.
        //
        //   oldIndex:
        //     The old index for the changed items.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If action is not Move or index is less than 0.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action, T[] changedItems, int index, int oldIndex)
        {
            if (action != NotifyCollectionChangedAction.Move)
            {
                throw new ArgumentException(string.Format(ERROR_MESSAGE, nameof(NotifyCollectionChangedAction.Move)), "action");
            }
            if (index < 0)
            {
                throw new ArgumentException("IndexCannotBeNegative", "index");
            }
            this.InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a one-item System.Collections.Specialized.NotifyCollectionChangedAction.Move
        //     change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can only be set to System.Collections.Specialized.NotifyCollectionChangedAction.Move.
        //
        //   changedItem:
        //     The item affected by the change.
        //
        //   index:
        //     The new index for the changed item.
        //
        //   oldIndex:
        //     The old index for the changed item.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If action is not Move or index is less than 0.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action, T changedItem, int index, int oldIndex)
        {
            if (action != NotifyCollectionChangedAction.Move)
            {
                throw new ArgumentException(string.Format(ERROR_MESSAGE, nameof(NotifyCollectionChangedAction.Move)), "action");
            }
            if (index < 0)
            {
                throw new ArgumentException("IndexCannotBeNegative", "index");
            }
            T[] array = new T[]
            {
                changedItem
            };
            this.InitializeMoveOrReplace(action, array, array, index, oldIndex);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Specialized.NotifyCollectionChangedEventArgs&lt;T&gt;
        //     class that describes a one-item System.Collections.Specialized.NotifyCollectionChangedAction.Replace
        //     change.
        //
        // Parameters:
        //   action:
        //     The action that caused the event. This can be set to System.Collections.Specialized.NotifyCollectionChangedAction.Replace.
        //
        //   newItem:
        //     The new item that is replacing the original item.
        //
        //   oldItem:
        //     The original item that is replaced.
        //
        //   index:
        //     The index of the item being replaced.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     If action is not Replace.
        public NotifyChangedEventArgs(NotifyCollectionChangedAction action, T newItem, T oldItem, int index)
        {
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException(string.Format(ERROR_MESSAGE, nameof(NotifyCollectionChangedAction.Replace)), "action");
            }
            this.InitializeMoveOrReplace(action, new T[]
            {
                newItem
            }, new T[]
            {
                oldItem
            }, index, index);
        }

        internal NotifyChangedEventArgs(NotifyCollectionChangedAction action, T[] newItems, T[] oldItems, int newIndex, int oldIndex)
        {
            this._action = action;
            this._newItems = newItems;
            this._oldItems = oldItems;
            this._newStartingIndex = newIndex;
            this._oldStartingIndex = oldIndex;
        }
        private void InitializeAddOrRemove(NotifyCollectionChangedAction action, T[] changedItems, int startingIndex)
        {
            if (action == NotifyCollectionChangedAction.Add)
            {
                this.InitializeAdd(action, changedItems, startingIndex);
                return;
            }
            if (action == NotifyCollectionChangedAction.Remove)
            {
                this.InitializeRemove(action, changedItems, startingIndex);
            }
        }
        private void InitializeAdd(NotifyCollectionChangedAction action, T[]? newItems, int newStartingIndex)
        {
            this._action = action;
            this._newItems = newItems;
            this._newStartingIndex = newStartingIndex;
        }
        private void InitializeRemove(NotifyCollectionChangedAction action, T[] oldItems, int oldStartingIndex)
        {
            this._action = action;
            this._oldItems = oldItems;
            this._oldStartingIndex = oldStartingIndex;
        }
        private void InitializeMoveOrReplace(NotifyCollectionChangedAction action, T[] newItems, T[] oldItems, int startingIndex, int oldStartingIndex)
        {
            this.InitializeAdd(action, newItems, startingIndex);
            this.InitializeRemove(action, oldItems, oldStartingIndex);
        }

        private NotifyCollectionChangedAction _action;
        private T[] _emptyItems = new T[0];
        private T[]? _newItems;
        private T[]? _oldItems;
        private int _newStartingIndex = -1;
        private int _oldStartingIndex = -1;

        //
        // Summary:
        //     Gets the action that caused the event.
        //
        // Returns:
        //     A System.Collections.Specialized.NotifyCollectionChangedAction value that describes
        //     the action that caused the event.
        public NotifyCollectionChangedAction Action { get { return _action; } }
        //
        // Summary:
        //     Gets the list of new items involved in the change.
        //
        // Returns:
        //     The list of new items involved in the change.
        public IReadOnlyList<T> NewItems { get { return _newItems ?? _emptyItems; } }
        //
        // Summary:
        //     Gets the index at which the change occurred.
        //
        // Returns:
        //     The zero-based index at which the change occurred.
        public int NewStartingIndex { get { return _newStartingIndex; } }
        //
        // Summary:
        //     Gets the list of items affected by a System.Collections.Specialized.NotifyCollectionChangedAction.Replace,
        //     Remove, or Move action.
        //
        // Returns:
        //     The list of items affected by a System.Collections.Specialized.NotifyCollectionChangedAction.Replace,
        //     Remove, or Move action.
        public IReadOnlyList<T> OldItems { get { return _oldItems ?? _emptyItems; } }
        //
        // Summary:
        //     Gets the index at which a System.Collections.Specialized.NotifyCollectionChangedAction.Move,
        //     Remove, or Replace action occurred.
        //
        // Returns:
        //     The zero-based index at which a System.Collections.Specialized.NotifyCollectionChangedAction.Move,
        //     Remove, or Replace action occurred.
        public int OldStartingIndex { get { return _oldStartingIndex; } }
    }
}
