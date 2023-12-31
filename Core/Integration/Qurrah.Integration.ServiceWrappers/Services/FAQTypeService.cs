﻿using Microsoft.Extensions.Configuration;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;
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
        public async Task<T> GetAllAsync<T>(string authToken)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPGet,
                URL = serviceURL,
                AuthToken = authToken
            });
        }
        public async Task<T> GetAsync<T>(int id, string authToken)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPGet,
                URL = $"{serviceURL}/{id}",
                AuthToken = authToken
            });
        }
        public async Task<T> CreateAsync<T>(FAQTypeWithLocalizedProperties faqTypeWithLocalizedProperties, string authToken)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = faqTypeWithLocalizedProperties,
                URL = serviceURL,
                AuthToken = authToken
            });
        }
        public async Task<T> UpdateAsync<T>(FAQTypeWithLocalizedProperties fAQTypeWithLocalizedProperties, string authToken)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPut,
                Data = fAQTypeWithLocalizedProperties,
                URL = $"{serviceURL}",
                AuthToken = authToken
            });
        }
        public async Task<T> DeleteAsync<T>(int id, string authToken)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPDelete,
                URL = $"{serviceURL}/{id}",
                AuthToken = authToken
            });
        }
        #endregion
    }
}