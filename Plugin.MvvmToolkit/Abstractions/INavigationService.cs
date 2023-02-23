namespace Plugin.MvvmToolkit.Abstractions;

public interface INavigationService
{
    void AddRoute(Type viewModelType, string viewRoute);
    Task NavigateToAsync(Type viewModelType);
    Task GoBackAsync();
}
