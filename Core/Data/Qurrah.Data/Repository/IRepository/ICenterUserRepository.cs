using Qurrah.Entities.NoMapping;

namespace Qurrah.Data.Repository.IRepository
{
    public interface ICenterUserRepository
    {
        Task<RegistrationResponse> RegisterWithSaveAsync(CenterUserRegistrationRequest registrationRequest);
    }
}