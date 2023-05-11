using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Plugin.MvvmToolkit.Maui.Views;

/// <summary>
/// Base content page to be used as a starting point for pages in the application.
/// </summary>
/// <typeparam name="TViewModel">The type of the associated ViewModel.</typeparam>
public abstract class BaseContentPage<TViewModel> : ContentPage where TViewModel : BaseViewModel<ILogger<TViewModel>>
{
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
        On<iOS>().SetUseSafeArea(true);
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
}
