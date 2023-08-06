using Qurrah.Integration.ServiceWrappers.DTOs.Center;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface ICenterService
    {
        Task<T> CreateAsync<T>(CenterWithLocalizedProperties centerWithLocalizedProperties, string authToken);
    }
}