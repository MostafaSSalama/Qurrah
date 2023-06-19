using Qurrah.Integration.Models.DTOs.FAQType;

namespace Qurrah.Integration.Models.DTOs.FAQ
{
    public class FAQClassifiedDTO
    {
        public FAQTypeDTO Type { get; set; }
        public IEnumerable<FAQDTO> FAQs { get; set; }
    }
}