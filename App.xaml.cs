using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Infrastructure.Services;
using ResolutionsFlow.Presentation.Services;
using ResolutionsFlow.Presentation.ViewModels;
using ResolutionsFlow.Presentation.ViewModels.Pages;
using ResolutionsFlow.Presentation.Views;

namespace ResolutionsFlow
{
    public partial class App : System.Windows.Application
    {
        private IServiceProvider _sp = null!;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Infrastructure
            services.AddSingleton<ISettingsService, JsonSettingsService>();

            // Presentation services
            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<ILocalizationService, LocalizationService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // ViewModels
            services.AddSingleton<ShellViewModel>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<DocumentsViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<AboutViewModel>();

            // Windows
            services.AddSingleton<ShellWindow>();

            services.AddTransient<SettingsViewModel>();


            _sp = services.BuildServiceProvider();

            await InitializeAsync();

            var window = _sp.GetRequiredService<ShellWindow>();
            window.DataContext = _sp.GetRequiredService<ShellViewModel>();
            window.Show();
        }

        private async Task InitializeAsync()
        {
            var settings = _sp.GetRequiredService<ISettingsService>();
            await settings.LoadAsync();

            var theme = _sp.GetRequiredService<IThemeService>();
            theme.Apply(settings.Current.Theme);

            var loc = _sp.GetRequiredService<ILocalizationService>();
            loc.Apply(settings.Current.Language);

            var nav = _sp.GetRequiredService<INavigationService>();
            nav.NavigateTo<DashboardViewModel>();
        }
    }
}
