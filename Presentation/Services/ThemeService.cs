using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Application.Models;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace ResolutionsFlow.Presentation.Services
{
    public sealed class ThemeService : IThemeService
    {
        public void Apply(AppTheme theme)
        {
            var wpfuiTheme = theme == AppTheme.Dark
                ? ApplicationTheme.Dark
                : ApplicationTheme.Light;

            // можна поставити Mica як у доках
            ApplicationThemeManager.Apply(wpfuiTheme, WindowBackdropType.Mica);
        }
    }
}
