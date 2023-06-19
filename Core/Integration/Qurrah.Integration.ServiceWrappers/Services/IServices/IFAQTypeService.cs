
using Qurrah.Integration.ServiceWrappers.DTOs.FAQType;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface IFAQTypeService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(FAQTypeCreateDTO faqTypeCreateDTO);
        Task<T> UpdateAsync<T>(FAQTypeUpdateDTO faqTypeUpdateDTO);
        Task<T> DeleteAsync<T>(int id);
    }
}