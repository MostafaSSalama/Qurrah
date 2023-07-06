namespace Qurrah.Entities
{
    public class FAQTypeWithLocalizedProperties
    {
        public FAQType FAQType { get; set; }
        public IEnumerable<LocalizedProperty> LocalizedProperties { get; set; }
    }
}