using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;
using Qurrah.Integration.ServiceWrappers.DTOs.Localization;

namespace Qurrah.Web.Areas.Admin.Models
{
    public class FAQVM
    {
        #region Ctor
        public FAQVM()
        {
            FAQ = new();
        }
        #endregion

        #region Properties
        [ValidateNever]
        public List<LocaleInfo> Locales { get; set; }

        public FAQ FAQ { get; set; }

        [ValidateNever]
        public List<LocalizedPropertyGroupVM> LocalizedPropertyGroups { get; set; } = new();

        [ValidateNever]
        public IEnumerable<SelectListItem> FAQTypes { get; set; }
        #endregion
    }
}