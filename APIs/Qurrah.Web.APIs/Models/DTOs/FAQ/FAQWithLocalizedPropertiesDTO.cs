using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Qurrah.Web.APIs.Models.DTOs.Localization;

namespace Qurrah.Web.APIs.Models.DTOs.FAQ
{
    public class FAQWithLocalizedPropertiesDTO
    {
        public FAQDTO FAQ { get; set; }

        [ValidateNever]
        public IEnumerable<LocalizedPropertyDTO> LocalizedProperties { get; set; }
    }
}