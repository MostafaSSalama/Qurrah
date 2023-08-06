using Qurrah.Business.Extensions;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.DTOs.Center;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using System.Net;

namespace Qurrah.Business.Center
{
    public class CenterManager : ICenterManager
    {
        #region Fields
        private readonly ICenterService _centerService;
        #endregion

        #region Ctor
        public CenterManager(ICenterService centerService)
        {
            _centerService = centerService;
        }
        #endregion

        public async Task<APIResult> CreateAsync(CenterWithLocalizedProperties centerWithLocalizedProperties, string authToken)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var response = await _centerService.CreateAsync<APIResponse>(centerWithLocalizedProperties, authToken);

                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.Created)
                    apiResult.ActionResult = ActionResult.Success;
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else if (response?.StatusCode == HttpStatusCode.BadRequest)
                {
                    apiResult.ActionResult = ActionResult.BadRequest;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                apiResult.ActionResult = ActionResult.GeneralFailure;
                throw;
            }
            return apiResult;
        }
    }
}