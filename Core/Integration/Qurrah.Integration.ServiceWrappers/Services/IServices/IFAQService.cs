using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface IFAQService
    {
        Task<T> GetAllAsync<T>(string authToken);
        Task<T> GetAllClassifiedByTypeAsync<T>();
        Task<T> GetAsync<T>(int id, string authToken);
        Task<T> CreateAsync<T>(FAQWithLocalizedProperties faqWithLocalizedProperties, string authToken);
        Task<T> UpdateAsync<T>(FAQWithLocalizedProperties faqWithLocalizedProperties, string authToken);
        Task<T> DeleteAsync<T>(int id, string authToken);
    }
}