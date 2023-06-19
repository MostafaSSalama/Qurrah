using Microsoft.Extensions.Configuration;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQType;
using Qurrah.Integration.ServiceWrappers.Services;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using static Qurrah.Integration.ServiceWrappers.Constants;

namespace MagicVilla.Web.Services
{
    public class FAQTypeService : BaseService, IFAQTypeService
    {
        #region Fields
        private readonly string serviceURL;
        #endregion

        #region Ctor
        public FAQTypeService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            serviceURL = configuration.GetValue<string>("ServiceURLs:FAQTypeAPI");
        }
        #endregion

        #region Methods
        public async Task<T> CreateAsync<T>(FAQTypeCreateDTO faqTypeCreateDTO)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = faqTypeCreateDTO,
                URL = serviceURL
            });
        }

        public async Task<T> DeleteAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPDelete,
                URL = $"{serviceURL}/{id}"
            });
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPGet,
                URL = serviceURL
            });
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPGet,
                URL = $"{serviceURL}/{id}"
            });
        }

        public async Task<T> UpdateAsync<T>(FAQTypeUpdateDTO faqTypeUpdateDTO)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPut,
                Data = faqTypeUpdateDTO,
                URL = $"{serviceURL}"
            });
        }
        #endregion
    }
}