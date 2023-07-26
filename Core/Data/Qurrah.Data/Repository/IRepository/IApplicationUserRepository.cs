using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IApplicationUserRepository
    {
        Task<bool> IsUniqueAsync(string userName, string email);
        Task<LoginResult> LoginAsync(LoginRequest loginRequest);
        Task<ApplicationUserRegistrationResult> RegisterWithSaveAsync(ApplicationUser user, string password);
        Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName);
    }
}