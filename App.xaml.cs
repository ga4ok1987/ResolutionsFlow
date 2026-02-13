using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Application.Services;
using ResolutionsFlow.Infrastructure.Services;
using ResolutionsFlow.Presentation.Services;
using ResolutionsFlow.Presentation.ViewModels;
using ResolutionsFlow.Presentation.ViewModels.Pages;
using ResolutionsFlow.Presentation.Views;
using Wpf.Ui.Controls;

namespace ResolutionsFlow;

public partial class App : Application
{
    private IHost? _host;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                // Window
                services.AddSingleton<ShellWindow>();
                services.AddSingleton<IWindow>(sp => sp.GetRequiredService<ShellWindow>());

                // Navigation (твоя реалізація)
                services.AddSingleton<INavigationService, NavigationService>();

                // Services (з твого репо)
                services.AddSingleton<ISettingsService, SettingsService>();
                services.AddSingleton<IThemeService, ThemeService>();
                services.AddSingleton<ILocalizationService, LocalizationService>();

                // Shell VM
                services.AddSingleton<ShellViewModel>();

                // Page VMs (VM-навігація)
                services.AddSingleton<DashboardViewModel>();
                services.AddSingleton<DocumentsViewModel>();
                services.AddSingleton<SettingsViewModel>();
                services.AddSingleton<AboutViewModel>();
            })
            .Build();

        await _host.StartAsync();

        // ВАЖЛИВО: завантажити settings до показу вікна, щоб тема/мова застосувались одразу
        var settings = _host.Services.GetRequiredService<ISettingsService>();
        await settings.LoadAsync();

        // Показати Shell
        var window = _host.Services.GetRequiredService<IWindow>();
        window.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (_host is not null)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(2));
            _host.Dispose();
        }

        base.OnExit(e);
    }
}
