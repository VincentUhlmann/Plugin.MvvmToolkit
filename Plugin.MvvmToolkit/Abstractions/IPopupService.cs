namespace Plugin.MvvmToolkit.Abstractions;

public interface IPopupService
{
    Task DisplayPopupAsync(string title, string message, string cancel);

    Task<bool> DisplayPopupAsync(string title, string message, string accept, string cancel);
}
