using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Plugin.MvvmToolkit.Maui.Views;

/// <summary>
/// Base content page to be used as a starting point for pages in the application.
/// </summary>
/// <typeparam name="TViewModel">The type of the associated ViewModel.</typeparam>
public abstract class BaseContentPage<TViewModel> : ContentPage, IDisposable where TViewModel : BaseViewModel<ILogger<TViewModel>>
{
    /// <summary>
    /// Indicates whether the page has been disposed.
    /// </summary>
    protected bool Disposed { get; private set; }

    /// <summary>
    /// The ViewModel associated with this page.
    /// </summary>
    protected TViewModel ViewModel { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseContentPage{T}"/> class.
    /// </summary>
    /// <param name="viewModel">The ViewModel instance.</param>
    protected BaseContentPage(TViewModel viewModel)
    {
        BindingContext = ViewModel = viewModel;

        Unloaded += (s, e) => Dispose();
    }

    /// <summary>
    /// Sets whether to use the safe area on iOS devices.
    /// </summary>
    /// <param name="value">A boolean indicating whether to use the safe area.</param>
    protected void SetUseSafeArea(bool value)
    {
        On<iOS>().SetUseSafeArea(value);
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
}
