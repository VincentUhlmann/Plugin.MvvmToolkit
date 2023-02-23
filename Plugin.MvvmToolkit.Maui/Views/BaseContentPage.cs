using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Plugin.MvvmToolkit.Maui.Views;

public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel<ILogger<T>>
{
    protected T ViewModel { get; }

    protected BaseContentPage(T viewModel)
    {
        BindingContext = ViewModel = viewModel;
        On<iOS>().SetUseSafeArea(true);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ViewModel.AppearingCommand.Execute(this);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ViewModel.DisappearingCommand.Execute(this);
    }
}
