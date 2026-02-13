using ResolutionsFlow.Application.Models;

namespace ResolutionsFlow.Application.Interfaces
{
    public interface ILocalizationService
    {
        void Apply(AppLanguage language);
    }
}
