using Microsoft.AspNetCore.Mvc.Rendering;
using Qurrah.Business.Extensions;
using Qurrah.Business.Localization;
using Qurrah.Business.Logging;
using LocalizationDTOs = Qurrah.Integration.ServiceWrappers.DTOs.Localization;

namespace Qurrah.Web.Utilities
{
    public class LocalizationUtility : ILocalizationUtility
    {
        #region Fields
        private readonly IExceptionLogging _exceptionLogging;
        private readonly ILocalizatonManager _localizatonManager;
        #endregion

        #region Ctor
        public LocalizationUtility(IExceptionLogging exceptionLogging, ILocalizatonManager localizatonManager)
        {
            _exceptionLogging = exceptionLogging;
            _localizatonManager = localizatonManager;
        }
        #endregion

        #region Methods
        public async Task<List<LocalizationDTOs.LocaleInfo>> GetLocales()
        {
            List<LocalizationDTOs.LocaleInfo> locales = null;
            try
            {
                var apiResult = await _localizatonManager.GetLocales(Thread.CurrentThread.CurrentUICulture.Name);

                if (apiResult?.ActionResult == Business.ActionResult.Success)
                    locales = apiResult.Result as List<LocalizationDTOs.LocaleInfo>;
                else if (apiResult?.ActionResult == Business.ActionResult.InternalServerError && apiResult.ErrorMessages?.Any() == true)
                    _exceptionLogging.Log(apiResult.ErrorMessages.Concatenate());
                else
                    _exceptionLogging.Log("An error occured while getting locales!");
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return locales ?? new List<LocalizationDTOs.LocaleInfo>();
        }
        public List<LocalizationDTOs.LocaleInfo> Locales
        {
            get
            {
                List<LocalizationDTOs.LocaleInfo> locales = null;
                try
                {
                    locales = GetLocales().Result?.ToList();
                }
                catch (Exception ex)
                {
                    _exceptionLogging.Log(ex);
                }
                return locales ?? new List<LocalizationDTOs.LocaleInfo>();
            }
        }
        #endregion
    }
}