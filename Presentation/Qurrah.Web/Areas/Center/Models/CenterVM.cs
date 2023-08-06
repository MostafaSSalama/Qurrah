using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Qurrah.Integration.ServiceWrappers.DTOs.Localization;
using Qurrah.Web.Models;
using WrapperDTOs = Qurrah.Integration.ServiceWrappers.DTOs.Center;

namespace Qurrah.Web.Areas.Center.Models
{
    public class CenterVM
    {
        #region Ctor
        public CenterVM()
        {
            Center = new();
            Center.CenterLicenses = new List<WrapperDTOs.CenterLicense> { new WrapperDTOs.CenterLicense() };
            LocalizedPropertyGroups = new();
            Locales = new();
        }
        #endregion

        #region Properties
        public WrapperDTOs.Center Center { get; set; }

        [ValidateNever]
        public List<LocaleInfo> Locales { get; set; }

        [ValidateNever]
        public List<LocalizedPropertyGroupVM> LocalizedPropertyGroups { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CenterTypes { get; set; }
        #endregion
    }
}