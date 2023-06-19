using Qurrah.Entities.NoMapping;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IBaseUserRepository
    {
        Task<bool> IsUniqueAsync(string userName, string email);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}