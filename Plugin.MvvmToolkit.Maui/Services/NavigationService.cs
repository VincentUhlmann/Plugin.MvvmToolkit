namespace Plugin.MvvmToolkit.Maui.Services;

internal sealed class NavigationService : INavigationService
{
    private readonly ConcurrentDictionary<Type, string> _viewRoutes = new();

    public void AddRoute<TViewModel>(object route) where TViewModel : BaseViewModel<ILogger<TViewModel>>
    {
        if (route is not string viewRoute)
            throw new ArgumentException("Parameter must be a string.");

        var type = typeof(TViewModel);

        if (!_viewRoutes.TryAdd(type, viewRoute))
            throw new DuplicateRouteException($"View model type '{type.Name}' is already mapped to a view route.");
    }

    public async Task NavigateAsync<TViewModel>(object? parameter = null) where TViewModel : BaseViewModel<ILogger<TViewModel>>
    {
        var type = typeof(TViewModel);

        if (!_viewRoutes.TryGetValue(type, out var viewRoute))
            throw new RouteNotFoundException($"View model type '{type.Name}' is not mapped to a view route.");

        try {
            if (parameter is null) {
                await Shell.Current.GoToAsync(viewRoute);
            } else if (parameter is string[] queryParameters) {
                await Shell.Current.GoToAsync(string.Format("{0}?{1}", viewRoute, string.Join("&", queryParameters)));
            } else if (parameter is Dictionary<string, object> objectParameters) {
                await Shell.Current.GoToAsync(viewRoute, objectParameters);
            } else {
                throw new ArgumentException("Parameter must be a string[] or a Dictionary<string, object>.");
            }
        } catch (Exception ex) {
            throw new NavigationException($"An error occurred while navigating to view route '{viewRoute}'.", ex);
        }
    }

    public async Task NavigateBackAsync(object? parameter = null)
    {
        try {
            if (parameter is null) {
                await Shell.Current.GoToAsync("..");
            } else if (parameter is string[] queryParameters) {
                await Shell.Current.GoToAsync(string.Format("{0}?{1}", "..", string.Join("&", queryParameters)));
            } else if (parameter is Dictionary<string, object> objectParameters) {
                await Shell.Current.GoToAsync("..", objectParameters);
            } else {
                throw new ArgumentException("Parameter must be a string[] or a Dictionary<string, object>.");
            }
        } catch (Exception ex) {
            throw new NavigationException($"An error occurred while navigating back.", ex);
        }
    }
}
