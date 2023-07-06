using Qurrah.Business.Extensions;
using Newtonsoft.Json;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.DTOs.Localization;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using System.Net;

namespace Qurrah.Business.Localization
{
    public class LocalizatonManager : ILocalizatonManager
    {
        #region Fields
        private readonly ILocalizationService _localizationService;
        #endregion

        #region Ctor
        public LocalizatonManager(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }
        #endregion

        #region Methods
        public async Task<APIResult> GetLocales(string inCulture)
        {
            APIResult apiResult = new APIResult();
            var response = await _localizationService.GetLocales<APIResponse>(inCulture);

            if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK && null != response.Result)
            {
                apiResult.Result = JsonConvert.DeserializeObject<List<LocaleInfo>>(Convert.ToString(response.Result));
                apiResult.ActionResult = ActionResult.Success;
            }
            else if (response?.StatusCode == HttpStatusCode.InternalServerError)
            {
                apiResult.ActionResult = ActionResult.InternalServerError;

                List<string[]> errors = JsonConvert.DeserializeObject<List<string[]>>(Convert.ToString(response.Errors));
                apiResult.ErrorMessages = errors.ToFlatList();
            }
            else
                apiResult.ActionResult = ActionResult.GeneralFailure;
            return apiResult;
        }
        #endregion
    }
}