namespace ResolutionsFlow.Application.Models
{
    public sealed class AppSettings
    {
        public AppTheme Theme { get; set; } = AppTheme.Light;
        public AppLanguage Language { get; set; } = AppLanguage.UkUA;

        // Далі сюди додаватимеш інші налаштування:
        // public string? DefaultExportFolder { get; set; }
        // public bool AutoSave { get; set; } = true;
    }
}
