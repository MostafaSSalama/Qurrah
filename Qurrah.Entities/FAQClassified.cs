namespace Qurrah.Entities
{
    public class FAQClassified
    {
        public FAQType Type { get; set; }
        public IEnumerable<FAQ> FAQs { get; set; }
    }
}
