using Newtonsoft.Json;
using Qurrah.Business.Extensions;
using Qurrah.Business.Logging;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.DTOs.Lookup;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using System.Net;

namespace Qurrah.Business.Lookup
{
    public class LookupManager : ILookupManager
    {
        #region Fields
        private readonly ILookupService _lookupService;
        private readonly IExceptionLogging _exceptionLogging;
        #endregion

        #region Ctor
        public LookupManager(ILookupService lookupService, IExceptionLogging exceptionLogging)
        {
            _lookupService = lookupService;
            _exceptionLogging = exceptionLogging;
        }
        #endregion
        
        #region Methods
        public async Task<APIResult> GetAllGendersAsync(string culture)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var response = await _lookupService.GetAllGenders<APIResponse>(culture);

                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK)
                {
                    apiResult.ActionResult = ActionResult.Success;
                    apiResult.Result = JsonConvert.DeserializeObject<IEnumerable<LookupInfo>>(Convert.ToString(response.Result));
                }
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                apiResult.ActionResult = ActionResult.GeneralFailure;
                _exceptionLogging.Log(ex);
                throw;
            }
            return apiResult;
        }

        public async Task<APIResult> GetAllUserTypesAsync(string culture)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var response = await _lookupService.GetAllUserTypes<APIResponse>(culture);

                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK)
                {
                    apiResult.ActionResult = ActionResult.Success;
                    apiResult.Result = JsonConvert.DeserializeObject<IEnumerable<LookupInfo>>(Convert.ToString(response.Result));
                }
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                apiResult.ActionResult = ActionResult.GeneralFailure;
                _exceptionLogging.Log(ex);
                throw;
            }
            return apiResult;
        }

        public async Task<APIResult> GetAllCenterTypesAsync(string culture)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var response = await _lookupService.GetAllCenterTypes<APIResponse>(culture);

                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK)
                {
                    apiResult.ActionResult = ActionResult.Success;
                    apiResult.Result = JsonConvert.DeserializeObject<IEnumerable<LookupInfo>>(Convert.ToString(response.Result));
                }
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                apiResult.ActionResult = ActionResult.GeneralFailure;
                _exceptionLogging.Log(ex);
                throw;
            }
            return apiResult;
        }
        #endregion
    }
}