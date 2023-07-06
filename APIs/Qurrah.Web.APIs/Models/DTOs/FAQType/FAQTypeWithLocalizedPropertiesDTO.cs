using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Qurrah.Web.APIs.Models.DTOs.Localization;

namespace Qurrah.Web.APIs.Models.DTOs.FAQType
{
    public class FAQTypeWithLocalizedPropertiesDTO
    {
        public FAQTypeDTO FAQType { get; set; }

        [ValidateNever]
        public IEnumerable<LocalizedPropertyDTO> LocalizedProperties { get; set; }
    }
}