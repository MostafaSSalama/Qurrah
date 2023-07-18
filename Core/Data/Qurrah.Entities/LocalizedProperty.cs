using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class LocalizedProperty
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string LocaleKeyGroup { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string LocaleKey { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength (int.MaxValue)]
        public string LocaleValue { get; set; }

        [Required(AllowEmptyStrings = false)]
        [ForeignKey(nameof(Language))]
        public LanguageId FKLanguageId { get; set; }

        [Required]
        public long EntityId { get; set; }

        [Required]
        public bool IsMultiLine { get; set; } = false;
        
        [ValidateNever]
        public Language Language { get; set; }
    }
}