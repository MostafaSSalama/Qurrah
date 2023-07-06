using Microsoft.Extensions.Configuration;
using Qurrah.Integration.ServiceWrappers.DTOs.Authentication;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using static Qurrah.Integration.ServiceWrappers.Constants;

namespace Qurrah.Integration.ServiceWrappers.Services
{
    public class UserAuthService : BaseService, IUserAuthService
    {
        #region Fields
        private readonly string serviceURL;
        #endregion

        #region Ctor
        public UserAuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            serviceURL = configuration.GetValue<string>("ServiceURLs:UserAuthAPI");
        }
        #endregion

        #region Methods
        public async Task<T> LoginAsync<T>(LoginRequest loginRequest)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = loginRequest,
                URL = $"{serviceURL}/Login"
            });
        }
        public async Task<T> RegisterAsync<T>(ParentUserRegistrationRequest parentRegistrationRequest)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = parentRegistrationRequest,
                URL = $"{serviceURL}/RegisterParentUser"
            });
        }
        public async Task<T> RegisterAsync<T>(CenterUserRegistrationRequest centerRegistrationRequest)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = centerRegistrationRequest,
                URL = $"{serviceURL}/RegisterCenterUser"
            });
        }
        #endregion
    }
}