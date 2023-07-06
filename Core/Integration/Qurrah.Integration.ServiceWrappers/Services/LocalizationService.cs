using Microsoft.Extensions.Configuration;
using Qurrah.Entities;
using Qurrah.Integration.ServiceWrappers.DTOs.Authentication;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using static Qurrah.Integration.ServiceWrappers.Constants;

namespace Qurrah.Integration.ServiceWrappers.Services
{
    public class LocalizationService : BaseService, ILocalizationService
    {
        #region Fields
        private readonly string serviceURL;
        #endregion

        #region Ctor
        public LocalizationService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            serviceURL = configuration.GetValue<string>("ServiceURLs:LocalizationAPI");
        }
        #endregion

        #region Methods
        public async Task<T> GetLocales<T>(string culture)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPGet,
                URL = $"{serviceURL}/GetLocales?culture={culture}"
            });
        }
        #endregion
    }
}