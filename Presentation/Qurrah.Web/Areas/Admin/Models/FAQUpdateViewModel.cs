using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;

namespace Qurrah.Web.Areas.Admin.Models
{
    public class FAQUpdateViewModel
    {
        #region Ctor
        public FAQUpdateViewModel()
        {
            FAQ = new();
        }
        #endregion

        #region Properties
        public FAQUpdateDTO FAQ { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> FAQTypes { get; set; }
        #endregion
    }
}