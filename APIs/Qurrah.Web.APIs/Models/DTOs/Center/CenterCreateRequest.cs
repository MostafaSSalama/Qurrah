using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Qurrah.Web.APIs.Models.DTOs.Localization;

namespace Qurrah.Web.APIs.Models.DTOs.Center
{
    public class CenterCreateRequest
    {
        public CenterCreateDTO Center { get; set; }

        [ValidateNever]
        public IEnumerable<LocalizedPropertyCreateDTO> LocalizedProperties { get; set; }
    }
}