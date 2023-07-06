using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.Localization
{
    public class LocalizedProperty
    {
        [Required]
        public long Id { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string LocaleKeyGroup { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string LocaleKey { get; set; }

        public string LocaleValue { get; set; }

        [Required]
        public int LanguageId { get; set; }
        
        public long EntityId { get; set; }

        public string Label { get; set; }

        public bool IsMultiLine { get; set; }

        public bool IsReadonly { get; set; }

        public LocaleInfo Language { get; set; }
    }
}