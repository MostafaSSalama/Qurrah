using Microsoft.Extensions.Configuration;
using Qurrah.Integration.ServiceWrappers.DTOs.Center;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using static Qurrah.Integration.ServiceWrappers.Constants;

namespace Qurrah.Integration.ServiceWrappers.Services
{
    public class CenterService : BaseService, ICenterService
    {
        #region Fields
        private readonly string serviceURL;
        #endregion

        #region Ctor
        public CenterService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            serviceURL = configuration.GetValue<string>("ServiceURLs:CenterAPI");
        }
        #endregion

        #region Methods
        public async Task<T> CreateAsync<T>(CenterWithLocalizedProperties centerWithLocalizedProperties, string authToken)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = centerWithLocalizedProperties,
                URL = $"{serviceURL}/Create",
                AuthToken = authToken
            });
        }
        #endregion
    }
}