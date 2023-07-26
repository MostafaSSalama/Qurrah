using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface ICenterRepository
    {
        Task AddWithLocalizedPropertiesWithSaveAsync(Center center, List<LocalizedProperty> localizedProperties);
    }
}