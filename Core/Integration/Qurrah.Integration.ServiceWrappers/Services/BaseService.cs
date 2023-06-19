using Qurrah.Integration.ServiceWrappers.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using static Qurrah.Integration.ServiceWrappers.Constants;

namespace Qurrah.Integration.ServiceWrappers.Services
{
    public class BaseService : IBaseService
    {
        #region Fields
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        #region Ctor
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Methods
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                if (null == apiRequest || string.IsNullOrWhiteSpace(apiRequest.URL))
                    throw new Exception("Request is invalid");

                HttpClient httpClient = _httpClientFactory.CreateClient("MagicVilla");

                //1- Request
                HttpRequestMessage requestMessage = new HttpRequestMessage();

                //URL
                requestMessage.RequestUri = new Uri(apiRequest.URL);
                
                //Headers
                requestMessage.Headers.Add("Accept", "application/json");
                
                //Data
                if (null != apiRequest.Data)
                {
                    string requestContent = JsonConvert.SerializeObject(apiRequest.Data);
                    requestMessage.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
                }

                //Verb
                requestMessage.Method = GetVerb(apiRequest.APIType);

                //2-Response
                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);
                
                //Deserialize response content
                string responseContent = await responseMessage.Content.ReadAsStringAsync();
                var responseContentDeserialized = JsonConvert.DeserializeObject<T>(responseContent);
                
                return responseContentDeserialized;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Utilities
        private static HttpMethod GetVerb(APIType apiType)
        {
            switch (apiType)
            {
                case APIType.HTTPPost:
                     return HttpMethod.Post;
                case APIType.HTTPPut:
                    return HttpMethod.Put;
                case APIType.HTTPDelete:
                    return HttpMethod.Delete;
                default:
                    return HttpMethod.Get;
            }
        }
        #endregion
    }
}