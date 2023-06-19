
using Qurrah.Integration.ServiceWrappers.DTOs.FAQType;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface IFAQTypeService
    {
        Task<T> GetAllAsync<T>(string authToken);
        Task<T> GetAsync<T>(int id, string authToken);
        Task<T> CreateAsync<T>(FAQTypeCreateDTO faqTypeCreateDTO, string authToken);
        Task<T> UpdateAsync<T>(FAQTypeUpdateDTO faqTypeUpdateDTO, string authToken);
        Task<T> DeleteAsync<T>(int id, string authToken);
    }
}