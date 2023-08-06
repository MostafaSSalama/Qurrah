using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;
using Qurrah.Integration.ServiceWrappers.DTOs.Localization;
using Qurrah.Web.Models;

namespace Qurrah.Web.Areas.Admin.Models
{
    public class FAQTypeVM
    {
        #region Ctor
        public FAQTypeVM()
        {
            FAQType = new();
        }
        #endregion

        #region Properties
        [ValidateNever]
        public List<LocaleInfo> Locales { get; set; }
        
        public FAQType FAQType { get; set; }

        [ValidateNever]
        public List<LocalizedPropertyGroupVM> LocalizedPropertyGroups { get; set; } = new();
        #endregion
    }
}