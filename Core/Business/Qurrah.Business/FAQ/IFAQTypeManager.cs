using Qurrah.Business.Localization;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;

namespace Qurrah.Business.FAQ
{
    public interface IFAQTypeManager : ILocalized
    {
        Task<APIResult> CreateAsync(FAQTypeWithLocalizedProperties faqTypeWithLocalizedProperties, string authToken);
        Task<APIResult> UpdateAsync(FAQTypeWithLocalizedProperties faqTypeWithLocalizedProperties, string authToken);
        Task<APIResult> GetAllAsync(string authToken);
        Task<APIResult> GetAsync(int id, string authToken);
        Task<APIResult> DeleteAsync(int id, string authToken);
    }
}