using System.Threading.Tasks;
using ResolutionsFlow.Application.Models;

namespace ResolutionsFlow.Application.Interfaces
{
    public interface ISettingsService
    {
        AppSettings Current { get; }
        Task LoadAsync();
        Task SaveAsync();
        Task UpdateAsync(AppSettings settings);
    }
}
