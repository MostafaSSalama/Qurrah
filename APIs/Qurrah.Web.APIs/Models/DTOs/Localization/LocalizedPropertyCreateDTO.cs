using Qurrah.Entities;
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Web.APIs.Models.DTOs.Localization
{
    public class LocalizedPropertyCreateDTO
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string LocaleKeyGroup { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string LocaleKey { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LocaleValue { get; set; }

        [Required(AllowEmptyStrings = false)]
        public LanguageId LanguageId { get; set; }

        [Required]
        public bool IsMultiLine { get; set; }
    }
}