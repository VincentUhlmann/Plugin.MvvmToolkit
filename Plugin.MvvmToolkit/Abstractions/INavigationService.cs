namespace Plugin.MvvmToolkit.Abstractions;

public interface INavigationService
{
    void AddRoute(Type viewModelType, string viewRoute);

    Task NavigateAsync(Type viewModelType, object? parameter = null);

    Task GoBackAsync();
}
