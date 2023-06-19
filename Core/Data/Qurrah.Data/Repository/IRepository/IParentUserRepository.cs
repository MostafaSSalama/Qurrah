using Qurrah.Entities.NoMapping;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IParentUserRepository
    {
        Task<RegistrationResponse> RegisterWithSaveAsync(ParentUserRegistrationRequest registrationRequest);
    }
}