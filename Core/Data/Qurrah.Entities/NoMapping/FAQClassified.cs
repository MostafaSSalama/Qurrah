using Qurrah.Entities;

namespace Qurrah.Entities.NoMapping
{
    public class FAQClassified
    {
        public FAQType Type { get; set; }
        public IEnumerable<FAQ> FAQs { get; set; }
    }
}
