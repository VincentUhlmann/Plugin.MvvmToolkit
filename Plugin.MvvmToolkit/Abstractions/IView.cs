namespace Plugin.MvvmToolkit.Abstractions;

/// <summary>
/// Represents a View that is associated with a ViewModel.
/// </summary>
/// <typeparam name="TViewModel"></typeparam>
public interface IView<TViewModel> where TViewModel : BaseViewModel<ILogger<TViewModel>>
{
    /// <summary>
    /// Gets the ViewModel associated with this View.
    /// </summary>
    TViewModel ViewModel { get; }
}
