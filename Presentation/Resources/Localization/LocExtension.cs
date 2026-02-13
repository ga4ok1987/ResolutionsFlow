using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace ResolutionsFlow.Presentation.Resources.Localization;

[MarkupExtensionReturnType(typeof(object))]
public sealed class LocExtension : MarkupExtension
{
    public string Key { get; set; } = "";

    public LocExtension() { }
    public LocExtension(string key) => Key = key;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrWhiteSpace(Key))
            return string.Empty;

        return new Binding($"[{Key}]")
        {
            Source = System.Windows.Application.Current.Resources["Loc"],
            Mode = BindingMode.OneWay
        };
    }
}
