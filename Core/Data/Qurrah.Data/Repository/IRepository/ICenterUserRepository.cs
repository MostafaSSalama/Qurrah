using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface ICenterUserRepository
    {
        Task<RegistrationResult> RegisterWithSaveAsync(CenterUserRegistrationRequest registrationRequest);
    }
}