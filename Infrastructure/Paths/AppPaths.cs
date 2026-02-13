using System;
using System.IO;

namespace ResolutionsFlow.Infrastructure.Paths
{
    public static class AppPaths
    {
        public static string AppDataDir =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ResolutionsFlow");

        public static string SettingsFile =>
            Path.Combine(AppDataDir, "settings.json");
    }
}
