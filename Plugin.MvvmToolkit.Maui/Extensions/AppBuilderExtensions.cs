namespace Plugin.MvvmToolkit.Maui.Extensions;

/// <summary>
/// Provides extension methods for configuring a Maui application with the MvvmToolkit library.
/// </summary>
public static class AppBuilderExtensions
{
    /// <summary>
    /// Registers the <see cref="IConnectivityService"/> and <see cref="INavigationService"/> with the application's service collection.
    /// </summary>
    /// <param name="builder">The <see cref="MauiAppBuilder"/> instance.</param>
    /// <returns>The updated <see cref="MauiAppBuilder"/> instance.</returns>
    public static MauiAppBuilder UseMvvmToolkit(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IPopupService, PopupService>();
        return builder;
    }
}
