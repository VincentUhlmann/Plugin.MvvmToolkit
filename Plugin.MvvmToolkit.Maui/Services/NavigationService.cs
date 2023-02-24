namespace Plugin.MvvmToolkit.Maui.Services;

internal sealed class NavigationService : INavigationService, IDisposable
{
    private readonly ConcurrentDictionary<Type, string> _viewRoutes = new();

    public void AddRoute(Type viewModelType, string viewRoute)
    {
        if (!_viewRoutes.TryAdd(viewModelType, viewRoute))
            throw new DuplicateViewRouteException($"View model type '{viewModelType.Name}' is already mapped to a view route.");
    }

    public async Task NavigateAsync(Type viewModelType, object? parameter = null)
    {
        if (!_viewRoutes.TryGetValue(viewModelType, out var viewRoute))
            throw new ViewRouteNotFoundException($"View model type '{viewModelType.Name}' is not mapped to a view route.");

        try {
            if (parameter is null) {
                await Shell.Current.GoToAsync(viewRoute);
            } else if (parameter is string queryParameters) {
                await Shell.Current.GoToAsync(string.Format("{0}?{1}", viewRoute, string.Join("&", queryParameters)));
            } else if (parameter is Dictionary<string, object> objectParameters) {
                await Shell.Current.GoToAsync(viewRoute, objectParameters);
            } else {
                throw new ArgumentException("Parameter must be a string or a Dictionary<string, object>.");
            }
        } catch (Exception ex) {
            // Handle any exceptions that occur during navigation
            throw new NavigationException($"An error occurred while navigating to view route '{viewRoute}'", ex);
        }
    }

    public async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    public void Dispose()
    {
        _viewRoutes.Clear();
    }
}
