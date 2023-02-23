namespace Plugin.MvvmToolkit.Maui.Extensions;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseMvvmToolkit(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IPopupService, PopupService>();
        return builder;
    }
}
