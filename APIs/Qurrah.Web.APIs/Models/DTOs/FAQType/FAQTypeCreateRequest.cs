using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Qurrah.Web.APIs.Models.DTOs.Localization;

namespace Qurrah.Web.APIs.Models.DTOs.FAQType
{
    public class FAQTypeCreateRequest
    {
        public FAQTypeCreateDTO FAQType { get; set; }

        [ValidateNever]
        public IEnumerable<LocalizedPropertyCreateDTO> LocalizedProperties { get; set; }
    }
}