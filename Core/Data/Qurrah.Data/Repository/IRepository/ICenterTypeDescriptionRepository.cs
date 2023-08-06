using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface ICenterTypeDescriptionRepository
    {
        Task<IEnumerable<LookupInfo>> GetAllCenterTypes(string culture);
    }
}