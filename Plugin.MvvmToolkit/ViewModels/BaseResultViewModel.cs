namespace Plugin.MvvmToolkit.ViewModels;

/// <summary>
/// A ViewModel that can receive a result from a navigation.
/// </summary>
/// <typeparam name="TLogger">The type of logger to use.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public abstract class BaseResultViewModel<TLogger, TResult> : BaseViewModel<TLogger>, IRecipient<NavigationResultMessage<TResult>> where TLogger : ILogger
{
    /// <summary>
    /// Determines whether the ViewModel can be navigated away from.
    /// </summary>
    private TaskCompletionSource<TResult>? _tcs;

    /// <summary>
    /// Gets or sets the navigation id.
    /// </summary>
    [NavigationProperty]
    public Guid NavigationId { get; set; }

    /// <summary>
    /// Initializes a new instance of the BaseResultViewModel class with the specified logger instance.
    /// </summary>
    /// <param name="logger"></param>
    public BaseResultViewModel(TLogger logger) : base(logger)
    {
    }

    /// <summary>
    /// Handles the appearing event of the associated view.
    /// </summary>
    /// <returns></returns>
    protected override Task OnAppearing()
    {
        WeakReferenceMessenger.Default.RegisterAll(this, NavigationId);
        return base.OnAppearing();
    }

    /// <summary>
    /// Handles the disappearing event of the associated view.
    /// </summary>
    /// <returns></returns>
    protected override Task OnDisappearing()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this, NavigationId);
        return base.OnDisappearing();
    }

    /// <summary>
    /// Receives the result of a navigation.
    /// </summary>
    /// <param name="message"></param>
    void IRecipient<NavigationResultMessage<TResult>>.Receive(NavigationResultMessage<TResult> message)
    {
        _tcs = new TaskCompletionSource<TResult>();
        message.Reply(_tcs.Task);
    }

    /// <summary>
    /// Sets the result of the navigation.
    /// </summary>
    /// <param name="result"></param>
    protected void SetResult(TResult result) =>
        _tcs?.SetResult(result);
}
