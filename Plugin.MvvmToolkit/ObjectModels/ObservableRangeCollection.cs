namespace Plugin.MvvmToolkit.ObjectModels;

/// <summary>
/// Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ObservableRangeCollection<T> : ObservableCollection<T>
{
    /// <summary>
    /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class.
    /// </summary>
    public ObservableRangeCollection() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class that contains elements copied from the specified collection.
    /// </summary>
    /// <param name="collection">collection: The collection from which the elements are copied.</param>
    /// <exception cref="ArgumentNullException">The collection parameter cannot be null.</exception>
    public ObservableRangeCollection(IEnumerable<T> collection) : base(collection)
    {
    }

    /// <summary>
    /// Adds the elements of the specified collection to the end of the ObservableCollection(Of T).
    /// </summary>
    public void AddRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
    {
        if (notificationMode != NotifyCollectionChangedAction.Add && notificationMode != NotifyCollectionChangedAction.Reset)
            throw new ArgumentException("Mode must be either Add or Reset for AddRange.", nameof(notificationMode));

        ArgumentNullException.ThrowIfNull(collection);

        CheckReentrancy();

        var startIndex = Count;

        var itemsAdded = AddArrangeCore(collection);

        if (!itemsAdded)
            return;

        if (notificationMode == NotifyCollectionChangedAction.Reset) {
            RaiseChangeNotificationEvents(action: NotifyCollectionChangedAction.Reset);
            return;
        }

        var changedItems = collection is List<T> list ? list : new List<T>(collection);

        RaiseChangeNotificationEvents(action: NotifyCollectionChangedAction.Add, changedItems: changedItems, startingIndex: startIndex);
    }

    /// <summary>
    /// Inserts the elements of a collection into the ObservableCollection(Of T) at the specified index.
    /// </summary>
    public void InsertRange(int index, IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
    {
        if (notificationMode != NotifyCollectionChangedAction.Add && notificationMode != NotifyCollectionChangedAction.Reset)
            throw new ArgumentException("Mode must be either Add or Reset for InsertRange.", nameof(notificationMode));

        ArgumentNullException.ThrowIfNull(collection);

        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        CheckReentrancy();

        var itemsInserted = InsertArrangeCore(index, collection);

        if (!itemsInserted)
            return;

        if (notificationMode == NotifyCollectionChangedAction.Reset) {
            RaiseChangeNotificationEvents(action: NotifyCollectionChangedAction.Reset);
            return;
        }

        var changedItems = collection is List<T> list ? list : new List<T>(collection);

        RaiseChangeNotificationEvents(action: NotifyCollectionChangedAction.Add, changedItems: changedItems, startingIndex: index);
    }

    /// <summary>
    /// Removes the first occurence of each item in the specified collection from ObservableCollection(Of T). NOTE: with notificationMode = Remove, removed items starting index is not set because items are not guaranteed to be consecutive.
    /// </summary>
    public void RemoveRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Reset)
    {
        if (notificationMode != NotifyCollectionChangedAction.Remove && notificationMode != NotifyCollectionChangedAction.Reset)
            throw new ArgumentException("Mode must be either Remove or Reset for RemoveRange.", nameof(notificationMode));

        ArgumentNullException.ThrowIfNull(collection);

        CheckReentrancy();

        if (notificationMode == NotifyCollectionChangedAction.Reset) {
            var raiseEvents = false;
            foreach (var item in collection) {
                Items.Remove(item);
                raiseEvents = true;
            }

            if (raiseEvents)
                RaiseChangeNotificationEvents(action: NotifyCollectionChangedAction.Reset);

            return;
        }

        var changedItems = new List<T>(collection);
        for (var i = 0; i < changedItems.Count; i++) {
            if (!Items.Remove(changedItems[i])) {
                changedItems.RemoveAt(i); // Can't use a foreach because changedItems is intended to be (carefully) modified
                i--;
            }
        }

        if (changedItems.Count == 0)
            return;

        RaiseChangeNotificationEvents(action: NotifyCollectionChangedAction.Remove, changedItems: changedItems);
    }

    /// <summary>
    /// Clears the current collection and replaces it with the specified item.
    /// </summary>
    public void Replace(T item) => ReplaceRange([item]);

    /// <summary>
    /// Clears the current collection and replaces it with the specified collection.
    /// </summary>
    public void ReplaceRange(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        CheckReentrancy();

        var previouslyEmpty = Items.Count == 0;

        Items.Clear();

        AddArrangeCore(collection);

        var currentlyEmpty = Items.Count == 0;

        if (previouslyEmpty && currentlyEmpty)
            return;

        RaiseChangeNotificationEvents(action: NotifyCollectionChangedAction.Reset);
    }

    /// <summary>
    /// Adds the items from the specified collection to the ObservableCollection(Of T) and returns whether any item was added.
    /// </summary>
    /// <param name="collection">The collection of items to add.</param>
    /// <returns>True if any item was added; otherwise, false.</returns>
    private bool AddArrangeCore(IEnumerable<T> collection)
    {
        var itemAdded = false;
        foreach (var item in collection) {
            Items.Add(item);
            itemAdded = true;
        }
        return itemAdded;
    }

    /// <summary>
    /// Inserts the items from the specified collection into the ObservableCollection(Of T) at the specified index and returns whether any item was added.
    /// </summary>
    /// <param name="index">The index at which to insert the items.</param>
    /// <param name="collection">The collection of items to insert.</param>
    /// <returns>True if any item was added; otherwise, false.</returns>
    private bool InsertArrangeCore(int index, IEnumerable<T> collection)
    {
        var itemAdded = false;
        foreach (var item in collection) {
            Items.Insert(index++, item);
            itemAdded = true;
        }
        return itemAdded;
    }

    /// <summary>
    /// Raises the necessary change notification events based on the specified action and optional parameters.
    /// </summary>
    /// <param name="action">The action that triggered the change.</param>
    /// <param name="changedItems">The list of changed items. Null if not applicable.</param>
    /// <param name="startingIndex">The starting index of the change. Default is -1.</param>
    private void RaiseChangeNotificationEvents(NotifyCollectionChangedAction action, List<T>? changedItems = null, int startingIndex = -1)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
        OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));

        if (changedItems == null)
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action));
        else
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, changedItems: changedItems, startingIndex: startingIndex));
    }
}
