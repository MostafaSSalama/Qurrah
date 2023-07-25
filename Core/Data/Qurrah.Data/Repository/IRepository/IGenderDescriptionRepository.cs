using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IGenderDescriptionRepository
    {
        Task<IEnumerable<LookupInfo>> GetAllGenders(string culture);
    }
}