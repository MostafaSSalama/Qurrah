namespace Qurrah.Entities
{
    public class FAQWithLocalizedProperties
    {
        public FAQ FAQ { get; set; }
        public IEnumerable<LocalizedProperty> LocalizedProperties { get; set; }
    }
}