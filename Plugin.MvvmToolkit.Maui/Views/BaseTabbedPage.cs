using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Plugin.MvvmToolkit.Maui.Views;

/// <summary>
/// Base content page to be used as a starting point for pages in the application.
/// </summary>
/// <typeparam name="TView">The type of the associated ViewModel.</typeparam>
/// <typeparam name="TViewModel">The type of the associated ViewModel.</typeparam>
public abstract class BaseTabbedPage<TView, TViewModel> : Microsoft.Maui.Controls.TabbedPage, IView<TView, TViewModel>, IDisposable, IQueryAttributable where TView : BaseContentPage<TView, TViewModel> where TViewModel : BaseViewModel<ILogger<TViewModel>>
{
    /// <summary>
    /// The logger instance.
    /// </summary>
    protected ILogger<TView> Logger { get; }

    /// <summary>
    /// The ViewModel associated with this page.
    /// </summary>
    public TViewModel ViewModel { get; }

    /// <summary>
    /// Indicates whether the page has been disposed.
    /// </summary>
    protected bool Disposed { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseTabbedPage{TViewModel}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="viewModel">The ViewModel instance.</param>
    /// <param name="setUseSafeArea">Indicates whether the page should use the safe area on iOS devices.</param>
    protected BaseTabbedPage(ILogger<TView> logger, TViewModel viewModel, bool setUseSafeArea = true)
    {
        Logger = logger;

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

        foreach (var field in typeof(TViewModel).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.IsDefined(typeof(NavigationPropertyAttribute), true))) {
            var propertyName = GetGeneratedPropertyName(field.Name);
            var property = typeof(TViewModel).GetProperty(propertyName) ?? throw new NavigationException($"Property '{propertyName}' not found in '{typeof(TViewModel)}'");

            var defaultValue = field.GetCustomAttribute<NavigationPropertyAttribute>()?.DefaultValue;
            SetPropertyValue(property, query, defaultValue);
        }

        foreach (var property in typeof(TViewModel).GetProperties().Where(x => x.IsDefined(typeof(NavigationPropertyAttribute), true))) {
            var defaultValue = property.GetCustomAttribute<NavigationPropertyAttribute>()?.DefaultValue;
            SetPropertyValue(property, query, defaultValue);
        }
    }

    private void SetPropertyValue(PropertyInfo property, IDictionary<string, object> query, object? defaultValue)
    {
        if (query.TryGetValue(property.Name, out var value)) {
            property.SetMethod?.Invoke(ViewModel, new[] { value });
        } else {
            if (defaultValue is not null)
                property.SetMethod?.Invoke(ViewModel, new[] { defaultValue });
        }
    }

    private static string GetGeneratedPropertyName(string propertyName)
    {
        if (propertyName.StartsWith("m_")) {
            propertyName = propertyName[2..];
        } else if (propertyName.StartsWith("_")) {
            propertyName = propertyName.TrimStart('_');
        }

        return $"{char.ToUpper(propertyName[0], CultureInfo.InvariantCulture)}{propertyName[1..]}";
    }
}
