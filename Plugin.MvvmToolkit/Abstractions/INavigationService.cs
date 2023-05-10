namespace Plugin.MvvmToolkit.Abstractions;

/// <summary>
/// Represents a navigation service that provides methods to add routes and navigate between views within an application.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Adds a route for the specified ViewModel to the navigation service.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the ViewModel.</typeparam>
    /// <param name="route">The URI or route associated with the ViewModel.</param>
    void AddRoute<TViewModel>(object route) where TViewModel : BaseViewModel<ILogger<TViewModel>>;

    /// <summary>
    /// Navigates to the View associated with the specified ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the ViewModel to navigate to.</typeparam>
    /// <param name="parameter">An optional parameter to pass to the ViewModel.</param>
    /// <returns>A Task that represents the asynchronous navigation operation.</returns>
    Task NavigateAsync<TViewModel>(object? parameter = null) where TViewModel : BaseViewModel<ILogger<TViewModel>>;

    /// <summary>
    /// Navigates back to the previous View in the navigation stack.
    /// </summary>
    /// <param name="parameter">An optional parameter to pass back to the previous ViewModel.</param>
    /// <returns>A Task that represents the asynchronous navigation operation.</returns>
    Task NavigateBackAsync(object? parameter = null);
}
