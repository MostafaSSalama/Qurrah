using Qurrah.Web.APIs.Models.DTOs.FAQType;

namespace Qurrah.Web.APIs.Models.DTOs.FAQ
{
    public class FAQClassifiedDTO
    {
        public FAQTypeDTO Type { get; set; }
        public IEnumerable<FAQDTO> FAQs { get; set; }
    }
}