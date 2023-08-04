namespace Plugin.MvvmToolkit.ViewModels;

public abstract class BaseResultViewModel<TLogger, TResult> : BaseViewModel<TLogger>, IRecipient<NavigationResultMessage<TResult>> where TLogger : ILogger
{
    private TaskCompletionSource<TResult>? _tcs;

    [NavigationProperty]
    public Guid NavigationId { get; set; }

    public BaseResultViewModel(TLogger logger) : base(logger)
    {
    }

    protected override Task OnAppearing()
    {
        WeakReferenceMessenger.Default.RegisterAll(this, NavigationId);
        return base.OnAppearing();
    }

    protected override Task OnDisappearing()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this, NavigationId);
        return base.OnDisappearing();
    }

    void IRecipient<NavigationResultMessage<TResult>>.Receive(NavigationResultMessage<TResult> message)
    {
        _tcs = new TaskCompletionSource<TResult>();
        message.Reply(_tcs.Task);
    }

    protected void SetResult(TResult result) =>
        _tcs?.SetResult(result);
}