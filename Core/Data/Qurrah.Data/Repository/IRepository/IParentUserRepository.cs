using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IParentUserRepository
    {
        Task<RegistrationResponse> RegisterWithSaveAsync(ParentUserRegistrationRequest registrationRequest);
    }
}