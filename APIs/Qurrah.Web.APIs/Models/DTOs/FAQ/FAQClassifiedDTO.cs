using Qurrah.Web.APIs.Models.DTOs.FAQType;

namespace Qurrah.Web.APIs.Models.DTOs.FAQ
{
    public class FAQClassifiedWithLocalizedPropertiesDTO
    {
        public FAQTypeWithLocalizedPropertiesDTO Type { get; set; }
        public IEnumerable<FAQWithLocalizedPropertiesDTO> FAQs { get; set; }
    }
}