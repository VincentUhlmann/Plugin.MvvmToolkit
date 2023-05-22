namespace Plugin.MvvmToolkit.Maui.Services;

/// <inheritdoc />
public sealed class NavigationService : INavigationService
{
    /// <summary>
    /// A dictionary of view models and their associated view routes.
    /// </summary>
    private readonly ConcurrentDictionary<Type, string> _viewRoutes = new();

    /// <inheritdoc />
    public void AddRoute<TViewModel>(object route) where TViewModel : BaseViewModel<ILogger<TViewModel>>
    {
        if (route is not string viewRoute)
            throw new ArgumentException("Parameter must be a string.");

        var viewModel = typeof(TViewModel);

        if (!_viewRoutes.TryAdd(viewModel, viewRoute))
            throw new DuplicateRouteException($"View model {viewModel.Name} is already mapped to a view route.");
    }

    /// <inheritdoc />
    public async Task NavigateAsync<TViewModel>(object? parameter = null) where TViewModel : BaseViewModel<ILogger<TViewModel>>
    {
        var viewModel = typeof(TViewModel);

        if (!_viewRoutes.TryGetValue(viewModel, out var viewRoute))
            throw new RouteNotFoundException($"View model '{viewModel.Name}' is not mapped to a view route.");

        await Navigate(viewRoute, parameter);
    }

    /// <inheritdoc />
    public async Task NavigateBackAsync(object? parameter = null)
    {
        await Navigate("..", parameter);
    }

    /// <summary>
    /// Navigates to the specified route.
    /// </summary>
    /// <param name="route"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NavigationException"></exception>
    private static async Task Navigate(string route, object? parameter)
    {
        try {
            if (parameter is null) {
                await Shell.Current.GoToAsync(route);
            } else if (parameter is string[] queryParameters) {
                await Shell.Current.GoToAsync(string.Format("{0}?{1}", route, string.Join("&", queryParameters)));
            } else if (parameter is Dictionary<string, object> objectParameters) {
                await Shell.Current.GoToAsync(route, objectParameters);
            } else {
                throw new ArgumentException("Parameter must be a string[] or a Dictionary<string, object>.");
            }
        } catch (Exception ex) {
            throw new NavigationException($"An error occurred while navigating back.", ex);
        }
    }
}
