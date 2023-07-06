namespace Qurrah.Business.Localization.Entities
{
    public class LocalizedPropertyGroup
    {
        public int LocaleId { get; set; }
        public List<LocalizedProperty> LocalizedProperties { get; set; }
    }
}