using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Qurrah.Web.APIs.Models.DTOs.Localization;

namespace Qurrah.Web.APIs.Models.DTOs.FAQ
{
    public class FAQUpdateRequest
    {
        public FAQUpdateDTO FAQ { get; set; }

        [ValidateNever]
        public IEnumerable<LocalizedPropertyUpdateDTO> LocalizedProperties { get; set; }
    }
}