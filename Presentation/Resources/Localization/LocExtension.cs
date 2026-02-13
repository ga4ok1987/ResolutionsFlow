using System;
using System.Globalization;
using System.Windows.Markup;

namespace ResolutionsFlow.Presentation.Resources.Localization
{
    [MarkupExtensionReturnType(typeof(string))]
    public sealed class LocExtension : MarkupExtension
    {
        public string Key { get; set; } = "";

        public LocExtension() { }
        public LocExtension(string key) => Key = key;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(Key))
                return string.Empty;

            return Strings.ResourceManager.GetString(Key, CultureInfo.CurrentUICulture)
                   ?? $"!{Key}!";
        }
    }
}
