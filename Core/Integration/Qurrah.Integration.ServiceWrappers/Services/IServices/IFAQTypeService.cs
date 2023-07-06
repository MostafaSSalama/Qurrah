
using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface IFAQTypeService
    {
        Task<T> GetAllAsync<T>(string authToken);
        Task<T> GetAsync<T>(int id, string authToken);
        Task<T> CreateAsync<T>(FAQTypeWithLocalizedProperties faqTypeWithLocalizedProperties, string authToken);
        Task<T> UpdateAsync<T>(FAQTypeWithLocalizedProperties faqTypeWithLocalizedProperties, string authToken);
        Task<T> DeleteAsync<T>(int id, string authToken);
    }
}