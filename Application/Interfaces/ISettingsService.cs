using ResolutionsFlow.Application.Models;

public interface ISettingsService
{
    AppSettings Current { get; }
    Task LoadAsync();
    Task SaveAsync();
    Task UpdateAsync(AppSettings settings);
}
