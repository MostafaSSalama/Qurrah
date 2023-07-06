using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface ICenterUserRepository
    {
        Task<RegistrationResponse> RegisterWithSaveAsync(CenterUserRegistrationRequest registrationRequest);
    }
}