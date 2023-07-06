using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Qurrah.Web.APIs.Models.DTOs.Localization;

namespace Qurrah.Web.APIs.Models.DTOs.FAQ
{
    public class FAQCreateRequest
    {
        public FAQCreateDTO FAQ { get; set; }

        [ValidateNever]
        public IEnumerable<LocalizedPropertyCreateDTO> LocalizedProperties { get; set; }
    }
}