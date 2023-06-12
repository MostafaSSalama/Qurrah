using Qurrah.Models.Integration.DTOs.FAQ;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface IFAQService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAllClassifiedByTypeAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(FAQCreateDTO faqCreateDTO);
        Task<T> UpdateAsync<T>(FAQUpdateDTO faqUpdateDTO);
        Task<T> DeleteAsync<T>(int id);
    }
}