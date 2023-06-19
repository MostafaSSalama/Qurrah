
using Qurrah.Integration.ServiceWrappers.DTOs.FAQType;

namespace Qurrah.Integration.ServiceWrappers.DTOs.FAQ
{
    public class FAQClassifiedDTO
    {
        public FAQTypeDTO Type { get; set; }
        public IEnumerable<FAQDTO> FAQs { get; set; }
    }
}