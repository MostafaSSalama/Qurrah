using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IParentUserRepository
    {
        Task<RegistrationResult> RegisterWithSaveAsync(ParentUserRegistrationRequest registrationRequest);
    }
}