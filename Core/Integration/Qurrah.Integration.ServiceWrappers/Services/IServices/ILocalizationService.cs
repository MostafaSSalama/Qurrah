
using Qurrah.Entities;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface ILocalizationService
    {
        Task<T> GetLocales<T>(string culture);
    }
}