namespace Plugin.MvvmToolkit.Abstractions;

public interface IView<TViewModel> where TViewModel : BaseViewModel<ILogger<TViewModel>>
{
    TViewModel ViewModel { get; }
}