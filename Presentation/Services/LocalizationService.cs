using System.Globalization;
using System.Threading;
using System.Windows.Markup;
using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Application.Models;

namespace ResolutionsFlow.Presentation.Services
{
    public sealed class LocalizationService : ILocalizationService
    {
        public void Apply(AppLanguage language)
        {
            var cultureName = language.ToCultureName(); // "uk-UA" / "en-US"
            var culture = new CultureInfo(cultureName);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            // Це корисно для форматів дат/чисел у WPF
            System.Windows.FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(System.Windows.FrameworkElement),
                new System.Windows.FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        }
    }
}
