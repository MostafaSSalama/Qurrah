using Microsoft.AspNetCore.Mvc.Rendering;
using LocalizationDTOs = Qurrah.Integration.ServiceWrappers.DTOs.Localization;

namespace Qurrah.Web.Utilities
{
    public interface ILocalizationUtility
    {
        #region Methods
        Task<List<LocalizationDTOs.LocaleInfo>> GetLocales();
        List<LocalizationDTOs.LocaleInfo> Locales { get; }
        #endregion
    }
}