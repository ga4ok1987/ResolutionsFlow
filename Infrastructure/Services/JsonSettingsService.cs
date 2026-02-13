using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Application.Models;
using ResolutionsFlow.Infrastructure.Paths;

namespace ResolutionsFlow.Infrastructure.Services
{
    public sealed class JsonSettingsService : ISettingsService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        public AppSettings Current { get; private set; } = new();

        public async Task LoadAsync()
        {
            Directory.CreateDirectory(AppPaths.AppDataDir);

            if (!File.Exists(AppPaths.SettingsFile))
            {
                Current = new AppSettings();
                await SaveAsync();
                return;
            }

            var json = await File.ReadAllTextAsync(AppPaths.SettingsFile);
            var loaded = JsonSerializer.Deserialize<AppSettings>(json, JsonOptions);

            Current = loaded ?? new AppSettings();
        }

        public async Task SaveAsync()
        {
            Directory.CreateDirectory(AppPaths.AppDataDir);
            var json = JsonSerializer.Serialize(Current, JsonOptions);
            await File.WriteAllTextAsync(AppPaths.SettingsFile, json);
        }

        public async Task UpdateAsync(AppSettings settings)
        {
            Current = settings;
            await SaveAsync();
        }
    }
}
