using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IUserTypeDescriptionRepository
    {
        Task<IEnumerable<LookupInfo>> GetAllUserTypes(string culture);
    }
}