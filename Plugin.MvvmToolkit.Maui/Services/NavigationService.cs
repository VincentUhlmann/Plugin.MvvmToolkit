﻿namespace Plugin.MvvmToolkit.Maui.Services;

/// <inheritdoc />
public sealed class NavigationService : INavigationService
{
    /// <summary>
    /// A dictionary of view models and their associated view routes.
    /// </summary>
    private readonly ConcurrentDictionary<Type, string> _viewRoutes = new();

    /// <inheritdoc />
    public void AddRoute<TView, TViewModel>(string? route = null) where TView : IView<TView, TViewModel> where TViewModel : BaseViewModel<ILogger<TViewModel>>
    {
        var viewModel = typeof(TViewModel);

        route ??= typeof(TView).Name;

        if (!_viewRoutes.TryAdd(viewModel, route))
            throw new DuplicateRouteException($"View model {viewModel.Name} is already mapped to a view route.");

        Routing.RegisterRoute(route, typeof(TView));
    }

    /// <inheritdoc />
    public async Task NavigateAsync<TViewModel>(Dictionary<string, object?>? navigationParams = null) where TViewModel : BaseViewModel<ILogger<TViewModel>>
    {
        var viewModel = typeof(TViewModel);

        if (!_viewRoutes.TryGetValue(viewModel, out var viewRoute))
            throw new RouteNotFoundException($"View model '{viewModel.Name}' is not mapped to a view route.");

        await Navigate(viewRoute, navigationParams);
    }

    /// <inheritdoc />
    public async Task<TResult> NavigateWithResultAsync<TViewModel, TResult>(Dictionary<string, object?>? navigationParams = null) where TViewModel : BaseResultViewModel<ILogger<TViewModel>, TResult>
    {
        navigationParams ??= [];

        var guid = Guid.NewGuid();

        navigationParams.Add(nameof(BaseResultViewModel<ILogger<TViewModel>, TResult>.NavigationId), guid);

        await NavigateAsync<TViewModel>(navigationParams);
        var result = await WeakReferenceMessenger.Default.Send(new NavigationResultMessage<TResult>(), guid);
        await NavigateBackAsync();

        return result;
    }

    /// <inheritdoc />
    public async Task NavigateBackAsync(Dictionary<string, object?>? navigationParams = null)
    {
        await Navigate("..", navigationParams);
    }

    /// <summary>
    /// Navigates to the specified route.
    /// </summary>
    /// <param name="route"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NavigationException"></exception>
    private static async Task Navigate(string route, Dictionary<string, object?>? navigationParams = null)
    {
        try {
            if (navigationParams is null) {
                await Shell.Current.GoToAsync(route);
            } else {
                await Shell.Current.GoToAsync(route, navigationParams);
            }
        } catch (Exception ex) {
            throw new NavigationException($"An error occurred while navigating to route '{route}'.", ex);
        }
    }
}
