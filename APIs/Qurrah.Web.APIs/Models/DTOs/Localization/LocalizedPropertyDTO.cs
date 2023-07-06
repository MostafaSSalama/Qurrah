using Qurrah.Entities;

namespace Qurrah.Web.APIs.Models.DTOs.Localization
{
    public class LocalizedPropertyDTO
    {
        public long Id { get; set; }
        public string LocaleKeyGroup { get; set; }
        public string LocaleKey { get; set; }
        public string LocaleValue { get; set; }
        public LanguageId LanguageId { get; set; }
        public long EntityId { get; set; }
        public bool IsMultiLine { get; set; }
        public LocaleDTO Language { get; set; }
    }
}