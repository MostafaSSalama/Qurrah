namespace Qurrah.Integration.ServiceWrappers.DTOs.FAQ
{
    public class FAQClassified
    {
        public string FAQType { get; set; }
        public IEnumerable<FAQ> FAQs { get; set; }
    }
}