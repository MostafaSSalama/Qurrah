using Qurrah.Business.Localization;
using Qurrah.Integration.ServiceWrappers.DTOs.Center;

namespace Qurrah.Business.Center
{
    public interface ICenterManager
    {
        Task<APIResult> CreateAsync(CenterWithLocalizedProperties centerWithLocalizedProperties, string authToken);
    }
}