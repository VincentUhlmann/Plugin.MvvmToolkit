namespace Plugin.MvvmToolkit.Maui.Views;

/// <summary>
/// Base content page to be used as a starting point for pages in the application.
/// </summary>
/// <typeparam name="TViewModel">The type of the associated ViewModel.</typeparam>
public abstract class BaseContentPage<TViewModel> : ContentPage, IView<TViewModel>, IDisposable, IQueryAttributable where TViewModel : BaseViewModel<ILogger<TViewModel>>
{
    /// <summary>
    /// The ViewModel associated with this page.
    /// </summary>
    public TViewModel ViewModel { get; }

    /// <summary>
    /// Indicates whether the page has been disposed.
    /// </summary>
    protected bool Disposed { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseContentPage{TViewModel}"/> class.
    /// </summary>
    /// <param name="viewModel">The ViewModel instance.</param>
    /// <param name="setUseSafeArea">Indicates whether the page should use the safe area on iOS devices.</param>
    protected BaseContentPage(TViewModel viewModel, bool setUseSafeArea = true)
    {
        BindingContext = ViewModel = viewModel;

        On<iOS>().SetUseSafeArea(setUseSafeArea);

        Unloaded += (s, e) => Dispose();
    }

    /// <summary>
    /// Invoked when the Page is about to appear.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        ViewModel.AppearingCommand.Execute(this);
    }

    /// <summary>
    /// Invoked when the Page is about to disappear.
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ViewModel.DisappearingCommand.Execute(this);
    }

    /// <summary>
    /// Invoked when the Page is unloaded.
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (Disposed)
            return;

        if (disposing)
            ViewModel.Dispose();

        Disposed = true;
    }

    /// <summary>
    /// Invoked when the Page is unloaded.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Applies the query attributes to the ViewModel.
    /// </summary>
    /// <param name="query"></param>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query is null)
            return;

        foreach (var property in typeof(TViewModel).GetProperties().Where(x => x.IsDefined(typeof(NavigationPropertyAttribute), true))) {
            if (query.TryGetValue(property.Name, out var value)) {
                property.SetMethod?.Invoke(ViewModel, new[] { value });
            } else {
                var defaultValue = property.GetCustomAttribute<NavigationPropertyAttribute>()?.DefaultValue;

                if (defaultValue is not null)
                    property.SetMethod?.Invoke(ViewModel, new[] { defaultValue });
            }
        }
    }
}