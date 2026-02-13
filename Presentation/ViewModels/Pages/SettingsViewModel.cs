using System.Threading.Tasks;
using System.Windows.Input;
using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Application.Models;
using ResolutionsFlow.Common.MVVM;

namespace ResolutionsFlow.Presentation.ViewModels.Pages
{
    public sealed class SettingsViewModel : ObservableObject
    {
        private readonly ISettingsService _settings;
        private readonly IThemeService _themeService;
        private readonly ILocalizationService _locService;
        private readonly INavigationService _nav;


        public AppTheme[] Themes { get; } = { AppTheme.Light, AppTheme.Dark };
        public AppLanguage[] Languages { get; } = { AppLanguage.UkUA, AppLanguage.EnUS };

        private AppTheme _selectedTheme;
        public AppTheme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (!SetProperty(ref _selectedTheme, value)) return;
                _ = ApplyThemeAsync(value);
            }
        }

        private AppLanguage _selectedLanguage;
        public AppLanguage SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (!SetProperty(ref _selectedLanguage, value)) return;
                _ = ApplyLanguageAsync(value);
            }
        }

        public ICommand ResetToDefaultsCommand { get; }

        public SettingsViewModel(ISettingsService settings, IThemeService themeService, ILocalizationService locService, INavigationService nav)
        {
            _settings = settings;
            _themeService = themeService;
            _locService = locService;
            _nav = nav;

            _selectedTheme = _settings.Current.Theme;
            _selectedLanguage = _settings.Current.Language;
            ResetToDefaultsCommand = new RelayCommand(ResetToDefaults);

        }

        private async Task ApplyThemeAsync(AppTheme theme)
        {
            _themeService.Apply(theme);

            var s = _settings.Current;
            s.Theme = theme;
            await _settings.UpdateAsync(s);
        }

        private async Task ApplyLanguageAsync(AppLanguage language)
        {
            _locService.Apply(language);

            var s = _settings.Current;
            s.Language = language;
            await _settings.UpdateAsync(s);

            // refresh current page (простий спосіб)
            _nav.NavigateTo<SettingsViewModel>();
        }

        private void ResetToDefaults()
        {
            SelectedTheme = AppTheme.Light;
            SelectedLanguage = AppLanguage.UkUA;
        }


    }
}
