using Microsoft.Extensions.Configuration;
using Qurrah.Integration.ServiceWrappers.Services;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using Qurrah.Models.Integration;
using Qurrah.Models.Integration.DTOs.FAQ;

namespace MagicVilla.Web.Services
{
    public class FAQService : BaseService, IFAQService
    {
        #region Fields
        private readonly string serviceURL;
        #endregion

        #region Ctor
        public FAQService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            serviceURL = configuration.GetValue<string>("ServiceURLs:FAQAPI");
        }
        #endregion

        #region Methods
        public async Task<T> CreateAsync<T>(FAQCreateDTO faqCreateDTO)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = Constants.APIType.HTTPPost,
                Data = faqCreateDTO,
                URL = serviceURL
            });
        }

        public async Task<T> DeleteAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = Constants.APIType.HTTPDelete,
                URL = $"{serviceURL}/{id}"
            });
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = Constants.APIType.HTTPGet,
                URL = serviceURL
            });
        }

        public async Task<T> GetAllClassifiedByTypeAsync<T>()
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = Constants.APIType.HTTPGet,
                URL = $"{serviceURL}/GetAllClassifiedByType"
            });
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = Constants.APIType.HTTPGet,
                URL = $"{serviceURL}/{id}"
            });
        }

        public async Task<T> UpdateAsync<T>(FAQUpdateDTO faqUpdateDTO)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = Constants.APIType.HTTPPut,
                Data = faqUpdateDTO,
                URL = $"{serviceURL}"
            });
        }
        #endregion
    }
}