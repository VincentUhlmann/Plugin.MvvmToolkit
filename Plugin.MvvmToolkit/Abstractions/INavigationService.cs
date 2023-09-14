namespace Plugin.MvvmToolkit.Abstractions;

/// <summary>
/// Represents a navigation service that provides methods to add routes and navigate between views within an application.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Adds a route for the specified ViewModel to the navigation service.
    /// </summary>
    /// <typeparam name="TView">The type of the View.</typeparam>
    /// <typeparam name="TViewModel">The type of the ViewModel.</typeparam>
    /// <param name="route">The route to associate with the ViewModel, or null to use the default route.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DuplicateRouteException"></exception>
    void AddRoute<TView, TViewModel>(string? route = null) where TView : IView<TView, TViewModel> where TViewModel : BaseViewModel<ILogger<TViewModel>>;

    /// <summary>
    /// Navigates to the View associated with the specified ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the ViewModel to navigate to.</typeparam>
    /// <param name="navigationParams"> A dictionary of parameters to pass to the ViewModel.</param>
    /// <returns>A Task that represents the asynchronous navigation operation.</returns>
    /// <exception cref="RouteNotFoundException"></exception>
    Task NavigateAsync<TViewModel>(Dictionary<string, object?>? navigationParams = null) where TViewModel : BaseViewModel<ILogger<TViewModel>>;

    /// <summary>
    /// Navigates to the View associated with the specified ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the ViewModel to navigate to.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="navigationParams"> A dictionary of parameters to pass to the ViewModel.</param>
    /// <returns>A Task that represents the asynchronous navigation operation.</returns>
    /// <exception cref="RouteNotFoundException"></exception>
    Task<TResult> NavigateWithResultAsync<TViewModel, TResult>(Dictionary<string, object?>? navigationParams = null) where TViewModel : BaseResultViewModel<ILogger<TViewModel>, TResult>;

    /// <summary>
    /// Navigates back to the previous View in the navigation stack.
    /// </summary>
    /// <param name="navigationParams"> A dictionary of parameters to pass to the ViewModel.</param>
    /// <returns>A Task that represents the asynchronous navigation operation.</returns>
    Task NavigateBackAsync(Dictionary<string, object?>? navigationParams = null);
}
