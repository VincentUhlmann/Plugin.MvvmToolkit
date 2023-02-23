namespace Plugin.MvvmToolkit.Abstractions;

public interface IPopupService
{
    Task DisplayPopupAsync(string title, string message, string cancel);

    Task<bool> DisplayPopupAsync(string title, string message, string accept, string cancel);

    Task<string> DisplayPopupAsync(string title, string[] buttons, string? cancel = null, string? destruction = null);
}
