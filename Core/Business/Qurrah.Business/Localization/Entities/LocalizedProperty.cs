namespace Qurrah.Business.Localization.Entities
{
    public class LocalizedProperty
    {
        public long Id { get; set; }
        public string LocaleKeyGroup { get; set; }
        public string LocaleKey { get; set; }
        public string LocaleValue { get; set; }
        public int LanguageId { get; set; }
        public long EntityId { get; set; }
        public string Label { get; set; }
        public bool IsMultiLine { get; set; }
        public bool IsReadonly { get; set; }
    }
}