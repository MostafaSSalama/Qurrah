using Microsoft.Extensions.Configuration;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using static Qurrah.Integration.ServiceWrappers.Constants;

namespace Qurrah.Integration.ServiceWrappers.Services
{
    public class LookupService : BaseService, ILookupService
    {
        #region Fields
        private readonly string serviceURL;
        #endregion

        #region Ctor
        public LookupService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            serviceURL = configuration.GetValue<string>("ServiceURLs:LookupAPI");
        }
        #endregion

        #region Methods
        public async Task<T> GetAllGenders<T>(string culture)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPGet,
                URL = $"{serviceURL}/GetAllGenders?culture={culture}"
            });
        }
        public async Task<T> GetAllUserTypes<T>(string culture)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPGet,
                URL = $"{serviceURL}/GetAllUserTypes?culture={culture}"
            });
        }
        #endregion
    }
}