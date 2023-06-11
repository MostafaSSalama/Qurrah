using Qurrah.Models.Integration.DTOs.FAQType;

namespace Qurrah.Models.Integration.DTOs.FAQ
{
    public class FAQClassifiedDTO
    {
        public FAQTypeDTO Type { get; set; }
        public IEnumerable<FAQDTO> FAQs { get; set; }
    }
}