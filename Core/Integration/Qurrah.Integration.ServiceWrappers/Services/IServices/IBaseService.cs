using Qurrah.Models.Integration;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface IBaseService
    {
        Task<T> SendAsync<T>(APIRequest request);
    }
}