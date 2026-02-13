using System.ComponentModel;
using System.Globalization;

namespace ResolutionsFlow.Presentation.Resources.Localization;

public sealed class LocalizationProvider : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public string this[string key]
        => Strings.ResourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? $"!{key}!";

    private void RaiseAll()
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));

    private static LocalizationProvider? _instance;
    public static LocalizationProvider Instance
        => _instance ??= (LocalizationProvider)System.Windows.Application.Current.Resources["Loc"];

    public static void NotifyCultureChanged()
        => Instance.RaiseAll();
}
