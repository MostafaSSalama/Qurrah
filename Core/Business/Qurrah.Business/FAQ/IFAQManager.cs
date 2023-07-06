using Qurrah.Business.Localization;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;

namespace Qurrah.Business.FAQ
{
    public interface IFAQManager : ILocalized
    {
        Task<APIResult> CreateAsync(FAQWithLocalizedProperties faqWithLocalizedProperties, string authToken);
        Task<APIResult> UpdateAsync(FAQWithLocalizedProperties faqWithLocalizedProperties, string authToken);
        Task<APIResult> DeleteAsync(int id, string authToken);
        Task<APIResult> GetAllAsync(string authToken);
        Task<APIResult> GetAllClassifiedByTypeAsync(string currentCulture);
        Task<APIResult> GetAsync(int id, string authToken);
    }
}