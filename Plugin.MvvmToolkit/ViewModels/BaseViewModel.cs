namespace Plugin.MvvmToolkit.ViewModels;

public abstract partial class BaseViewModel<TLogger> : ObservableObject where TLogger : ILogger
{
    protected TLogger Logger { get; }
    protected INavigationService NavigationService { get; }
    protected IPopupService PopupService { get; }
    protected bool Initialized { get; set; }

    protected BaseViewModel(TLogger logger, INavigationService navigationService, IPopupService popupService)
    {
        Logger = logger;
        NavigationService = navigationService;
        PopupService = popupService;
    }

    [RelayCommand]
    protected virtual Task OnAppearing()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    protected virtual Task OnDisappearing()
    {
        return Task.CompletedTask;
    }
}
