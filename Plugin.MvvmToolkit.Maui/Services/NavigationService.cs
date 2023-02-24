namespace Plugin.MvvmToolkit.Maui.Services;

internal sealed class NavigationService : INavigationService, IDisposable
{
    private readonly Shell _shell;
    private readonly ConcurrentDictionary<Type, string> _viewRoutes = new();

    public NavigationService(Shell shell)
    {
        _shell = shell ?? throw new ArgumentNullException(nameof(shell));
    }

    public void AddRoute(Type viewModelType, string viewRoute)
    {
        if (!_viewRoutes.TryAdd(viewModelType, viewRoute))
            throw new DuplicateViewRouteException($"View model type '{viewModelType.Name}' is already mapped to a view route.");
    }

    public async Task NavigateToAsync(Type viewModelType)
    {
        if (!_viewRoutes.TryGetValue(viewModelType, out var viewRoute))
            throw new ViewRouteNotFoundException($"View model type '{viewModelType.Name}' is not mapped to a view route.");

        try {
            await _shell.GoToAsync(viewRoute);
        } catch (Exception ex) {
            // Handle any exceptions that occur during navigation
            throw new NavigationException($"An error occurred while navigating to view route '{viewRoute}'", ex);
        }
    }

    public async Task GoBackAsync()
    {
        await _shell.GoToAsync("..");
    }

    public void Dispose()
    {
        _viewRoutes.Clear();
    }
}
