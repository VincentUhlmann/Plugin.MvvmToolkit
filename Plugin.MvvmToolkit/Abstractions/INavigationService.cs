namespace Plugin.MvvmToolkit.Abstractions;

public interface INavigationService
{
    void AddRoute<TViewModel>(object route) where TViewModel : BaseViewModel<ILogger<TViewModel>>;

    Task NavigateAsync<TViewModel>(object? parameter = null) where TViewModel : BaseViewModel<ILogger<TViewModel>>;

    Task NavigateBackAsync(object? parameter = null);
}
