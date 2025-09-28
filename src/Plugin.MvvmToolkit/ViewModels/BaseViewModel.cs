namespace Plugin.MvvmToolkit.ViewModels;

/// <summary>
/// Represents a base class for view models that provides basic functionality for handling lifecycle events and logging.
/// </summary>
/// <typeparam name="TLogger">The type of logger used by the view model.</typeparam>
public abstract partial class BaseViewModel<TLogger> : ObservableObject, IDisposable where TLogger : ILogger
{
    /// <summary>
    /// Gets the logger instance used by the view model.
    /// </summary>
    protected TLogger Logger { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the view model has been initialized.
    /// </summary>
    protected bool Initialized { get; set; }

    /// <summary>
    /// Indicates whether the view model has been disposed.
    /// </summary>
    protected bool Disposed { get; private set; }

    /// <summary>
    /// Initializes a new instance of the BaseViewModel class with the specified logger instance.
    /// </summary>
    /// <param name="logger">The logger instance used by the view model.</param>
    protected BaseViewModel(TLogger logger)
    {
        Logger = logger;
    }

    /// <summary>
    /// Handles the appearing event of the associated view.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    [RelayCommand]
    protected virtual Task OnAppearing()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles the disappearing event of the associated view.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    [RelayCommand]
    protected virtual Task OnDisappearing()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Invoked when the corresponding Page is unloaded.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Invoked when the corresponding Page is unloaded.
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (Disposed)
            return;

        Disposed = true;
    }
}
