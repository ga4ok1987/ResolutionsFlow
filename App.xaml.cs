using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ResolutionsFlow.Presentation.Resources.Localization;
using ResolutionsFlow.Presentation.Services;
using ResolutionsFlow.Presentation.ViewModels;
using ResolutionsFlow.Presentation.ViewModels.Pages;
using ResolutionsFlow.Presentation.Views;
using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Infrastructure.Services;


namespace ResolutionsFlow;

public partial class App : System.Windows.Application
{
    private ServiceProvider? _services;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        // Navigation (твоя реалізація)
        services.AddSingleton<ResolutionsFlow.Application.Interfaces.INavigationService,
                              ResolutionsFlow.Presentation.Services.NavigationService>();

        // ViewModels
        services.AddSingleton<ShellViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<DocumentsViewModel>();
        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<AboutViewModel>();

        // Window
        services.AddSingleton<ShellWindow>();



        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<ILocalizationService, LocalizationService>();

        services.AddSingleton<INavigationService, NavigationService>();



        _services = services.BuildServiceProvider();

        // Локалізаційний ресурс краще додати програмно (щоб XAML-дизайнер не дурив)
        Resources["Loc"] = new LocalizationProvider();

        try
        {
            var window = _services.GetRequiredService<ShellWindow>();
            window.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "DI error");
            throw;
        }


    }

    protected override void OnExit(ExitEventArgs e)
    {
        _services?.Dispose();
        base.OnExit(e);
    }
}
