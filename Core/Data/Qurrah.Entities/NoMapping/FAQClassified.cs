namespace Qurrah.Entities
{
    public class FAQClassified
    {
        public FAQType Type { get; set; }
        public IEnumerable<FAQWithLocalizedProperties> FAQs { get; set; }
    }
}