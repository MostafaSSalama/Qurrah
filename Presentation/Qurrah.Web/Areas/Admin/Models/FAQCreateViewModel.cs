using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Qurrah.Models.Integration.DTOs.FAQ;

namespace Qurrah.Web.Areas.Admin.Models
{
    public class FAQCreateViewModel
    {
        #region Ctor
        public FAQCreateViewModel()
        {
            FAQ = new();
        }
        #endregion

        #region Properties
        public FAQCreateDTO FAQ { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> FAQTypes { get; set; }
        #endregion
    }
}