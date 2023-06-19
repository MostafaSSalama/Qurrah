
using Qurrah.Integration.ServiceWrappers.DTOs.Authentication;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface IUserAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO loginRequest);
        Task<T> RegisterAsync<T>(ParentUserRegistrationRequestDTO parentRegistrationRequest);
        Task<T> RegisterAsync<T>(CenterUserRegistrationRequestDTO centerRegistrationRequest);
    }
}
