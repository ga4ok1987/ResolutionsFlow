using System.Threading.Tasks;
using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Application.Models;

namespace ResolutionsFlow.Infrastructure.Services;

public sealed class SettingsService : ISettingsService
{
    public AppSettings Current { get; private set; } = new();

    public Task LoadAsync()
    {
        // TODO: зчитування з файлу
        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        // TODO: запис у файл
        return Task.CompletedTask;
    }

    public Task UpdateAsync(AppSettings settings)
    {
        Current = settings;
        return Task.CompletedTask;
    }
}
