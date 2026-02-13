namespace ResolutionsFlow.Application.Models
{
    public enum AppLanguage
    {
        UkUA = 0,
        EnUS = 1
    }

    public static class AppLanguageExtensions
    {
        public static string ToCultureName(this AppLanguage lang)
            => lang == AppLanguage.EnUS ? "en-US" : "uk-UA";
    }
}
