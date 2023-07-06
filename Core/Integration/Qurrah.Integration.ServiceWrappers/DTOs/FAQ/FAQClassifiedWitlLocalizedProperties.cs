namespace Qurrah.Integration.ServiceWrappers.DTOs.FAQ
{
    public class FAQClassifiedWitlLocalizedProperties
    {
        public FAQTypeWithLocalizedProperties Type { get; set; }
        public IEnumerable<FAQWithLocalizedProperties> FAQs { get; set; }
    }
}