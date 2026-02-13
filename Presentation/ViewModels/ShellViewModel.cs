using System.Windows.Input;
using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Application.Models;
using ResolutionsFlow.Common.MVVM;
using ResolutionsFlow.Infrastructure.Paths;
using ResolutionsFlow.Presentation.ViewModels.Pages;

namespace ResolutionsFlow.Presentation.ViewModels;

public sealed class ShellViewModel : ObservableObject
{
    private readonly ISettingsService _settings;
    private readonly IThemeService _theme;
    private readonly ILocalizationService _loc;

    public INavigationService Navigation { get; }

    public string SettingsPath => AppPaths.SettingsFile;

    public ICommand NavigateDashboardCommand { get; }
    public ICommand NavigateDocumentsCommand { get; }
    public ICommand NavigateSettingsCommand { get; }
    public ICommand NavigateAboutCommand { get; }

    private bool _isDarkTheme;
    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set
        {
            if (!SetProperty(ref _isDarkTheme, value))
                return;

            var newTheme = _isDarkTheme ? AppTheme.Dark : AppTheme.Light;
            _settings.Current.Theme = newTheme;
            _theme.Apply(newTheme);
            _ = _settings.SaveAsync();
        }
    }

    private string _selectedCulture = "uk-UA";
    public string SelectedCulture
    {
        get => _selectedCulture;
        set
        {
            if (!SetProperty(ref _selectedCulture, value))
                return;

            var newLang = _selectedCulture == "uk-UA" ? AppLanguage.Uk : AppLanguage.En;
            _settings.Current.Language = newLang;
            _loc.Apply(newLang);

            // важливо: тригеримо оновлення локалізованих рядків у UI
            Resources.Localization.LocalizationProvider.NotifyCultureChanged();

            _ = _settings.SaveAsync();
        }
    }

    public ShellViewModel(
        INavigationService navigation,
        ISettingsService settings,
        IThemeService theme,
        ILocalizationService loc)
    {
        Navigation = navigation;
        _settings = settings;
        _theme = theme;
        _loc = loc;

        NavigateDashboardCommand = new RelayCommand(() => Navigation.NavigateTo<DashboardViewModel>());
        NavigateDocumentsCommand = new RelayCommand(() => Navigation.NavigateTo<DocumentsViewModel>());
        NavigateSettingsCommand = new RelayCommand(() => Navigation.NavigateTo<SettingsViewModel>());
        NavigateAboutCommand = new RelayCommand(() => Navigation.NavigateTo<AboutViewModel>());

        // ініціалізація значень з settings (після LoadAsync це буде актуально)
        _isDarkTheme = settings.Current.Theme == AppTheme.Dark;
        _selectedCulture = settings.Current.Language == AppLanguage.Uk ? "uk-UA" : "en-US";
    }
}
