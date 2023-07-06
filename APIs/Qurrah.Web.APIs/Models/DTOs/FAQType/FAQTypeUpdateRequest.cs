using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Qurrah.Web.APIs.Models.DTOs.Localization;

namespace Qurrah.Web.APIs.Models.DTOs.FAQType
{
    public class FAQTypeUpdateRequest
    {
        public FAQTypeUpdateDTO FAQType { get; set; }

        [ValidateNever]
        public IEnumerable<LocalizedPropertyUpdateDTO> LocalizedProperties { get; set; }
    }
}