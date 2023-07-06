namespace Qurrah.Entities
{
    public class FAQClassifiedWithLocalizedProperties
    {
        public FAQTypeWithLocalizedProperties Type { get; set; }
        public IEnumerable<FAQWithLocalizedProperties> FAQs { get; set; }
    }
}